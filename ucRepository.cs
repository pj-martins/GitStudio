using PaJaMa.WinControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class ucRepository : UserControl
	{
		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int GetScrollPos(IntPtr hWnd, int nBar);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

		private const int SB_VERT = 0x1;

		private GitHelper _helper;
		private LocalBranch _currentBranch;
		private List<RemoteBranch> _remoteBranches;

		private GitRepository _repository;
		public GitRepository Repository
		{
			get { return _repository; }
			set
			{
				_repository = value;
				_helper = new GitHelper(value);
			}
		}

		public Form MainForm { get; set; }

		private bool _inited = false;
		private FileSystemWatcher _selectedWatcher;
		private List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

		public ucRepository()
		{
			InitializeComponent();
		}

		public void Init()
		{
			if (!_inited)
			{
				if (!RefreshBranches(true)) return;
				_previousDifferences = null;
				_inited = true;
				if (!Repository.SuspendWatchingFiles && Repository.SSHConnection == null)
				{
					progMain.Visible = true;
					lblStatus.Text = "Watching files...";
					Common.Common.RunInThread(() =>
					{
						resetWatchers();
						// this.Invoke(new Action(() => refreshPage()));
					});
				}
				this.MainForm.FormClosing += (object sender, FormClosingEventArgs e) =>
				{
					Repository.SSHConnection?.Terminate();
				};
			}
		}

		public void Deactivate()
		{
			_inited = false;
			removeWatchers();
			//if (Repository.SSHConnection != null)
			//{
			//	Repository.SSHConnection.Terminate();
			//}
		}

		public bool RefreshBranches(bool initial = false, int tries = 0)
		{
			var branches = _helper.GetBranches(initial);
			if (branches == null) return false;
			if (branches.Count < 1) return true;

			if (Repository.SSHConnection != null && tries < 3)
			{
				// TODO: yuck!
				if (branches.All(b => b.BranchName == "git"))
				{
					return RefreshBranches(initial, tries++);
				}
				else
				{

				}
			}

			_currentBranch = branches.OfType<LocalBranch>().FirstOrDefault(b => b.IsCurrent);
			_remoteBranches = branches.OfType<RemoteBranch>().ToList();

			bool remote = true;
			while (true)
			{
				bool nodeSelected = false;
				var tv = remote ? tvRemoteBranches : tvLocalBranches;
				int scrollY;
				try
				{
					scrollY = GetScrollPos(tv.Handle, SB_VERT);
				}
				catch
				{
					// MONO?
					scrollY = -1;
				}
				tv.BeginUpdate();
				tv.Nodes.Clear();
				tv.SelectedNodes.Clear();
				foreach (var branch in branches.Where(b => remote ? b is RemoteBranch : b is LocalBranch))
				{
					var parts = branch.BranchName.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
					TreeNode node = null;
					foreach (var part in parts)
					{
						var nodeCollection = node == null ? tv.Nodes : node.Nodes;

						var foundNode = nodeCollection.OfType<TreeNode>().FirstOrDefault(n => n.Text == part);
						if (foundNode == null)
							foundNode = nodeCollection.Add(part);
						node = foundNode;
					}
					node.Tag = branch;
					if (!nodeSelected)
					{
						tv.SelectedNode = node;
						tv.SelectedNodes.Add(node);
						nodeSelected = true;
					}
					if (branch == _currentBranch)
						node.NodeFont = new Font(node.NodeFont ?? tv.Font, FontStyle.Bold);

					if (!remote)
					{
						var lb = branch as LocalBranch;
						if (lb.RemoteIsGone)
							node.Text += " [REMOTE DELETED!!!]";
						else if (lb.TracksBranch != null)
						{
							node.Text += " [" + ((LocalBranch)branch).TracksBranch.BranchName;
							if (lb.Ahead > 0)
								node.Text += " A:" + lb.Ahead.ToString();
							if (lb.Behind > 0)
								node.Text += " B:" + lb.Behind.ToString();
							node.Text += "]";
						}
					}
				}
				tv.ExpandAll();
				tv.EndUpdate();
				if (scrollY >= 0)
				{
					SetScrollPos(tv.Handle, SB_VERT, scrollY, true);
				}
				else
				{
					// TODO
				}
				if (remote) remote = false;
				else break;
			}

			btnPull.Enabled = _currentBranch != null && _currentBranch.TracksBranch != null;
			_previousDifferences = null;
			refreshPage();
			return true;
		}

		private object _lockWatchers = new object();
		private void removeWatchers()
		{
			lock (_lockWatchers)
			{
				if (_watchers != null)
				{
					for (int i = _watchers.Count - 1; i >= 0; i--)
					{
						var watcher = _watchers[i];
						_watchers.RemoveAt(i);
						watcher.EnableRaisingEvents = false;
						watcher.Dispose();
					}
				}
			}
		}

		private void resetWatchers()
		{
			removeWatchers();
			addWatchers();
		}

		private void addWatchers()
		{
			if (this.Repository.SSHConnection != null) return;
			lock (_lockWatchers)
			{
				var lst = _helper.RunCommand("ls-files");

				var listedDirectories = new List<string>();
				foreach (var l in lst)
				{
					var directory = Path.GetDirectoryName(Path.Combine(_repository.LocalPath, l.Replace("/", "\\")));
					// if (directory == _repository.LocalPath) continue;
					if (listedDirectories.Contains(l)) continue;
					if (!Directory.Exists(directory)) continue;
					var watcher = new FileSystemWatcher(directory);
					watcher.EnableRaisingEvents = true;
					watcher.Deleted += Watcher_Changed;
					watcher.Changed += Watcher_Changed;
					watcher.Created += Watcher_Changed;
					watcher.Renamed += Watcher_Changed;
					_watchers.Add(watcher);
				}
			}
		}

		private bool _wait = false;
		private List<string> _changedFiles = new List<string>();
		private object _lockObject = new object();
		private void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			if (e.Name == ".git") return;
			if (_lockChange) return;
			lock (_lockObject)
			{
				if (!_changedFiles.Contains(e.FullPath))
					_changedFiles.Add(e.FullPath);
				_wait = true;
			}
		}

		private void timDebounce_Tick(object sender, EventArgs e)
		{
			lock (_lockObject)
			{
				if (_wait)
				{
					_wait = false;
					return;
				}

				if (_changedFiles.Any())
				{
					this.Invoke(new Action(() =>
					{
						var arr = _changedFiles.ToArray();
						_changedFiles.Clear();
						this.refreshPage(arr);
					}));
				}
			}
		}

		private void branchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmBranch();
			frm.BranchFrom = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			frm.Repository = Repository;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				RefreshBranches();
			}
		}

		private void checkoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tvLocalBranches.SelectedNode == null || tvLocalBranches.SelectedNode.Tag == null) return;
			_lockChange = true;
			removeWatchers();
			_helper.RunCommand("checkout " + tvLocalBranches.SelectedNode.Tag.ToString(), true);
			RefreshBranches();
			addWatchers();
			_lockChange = false;
		}

		private void mnuLocal_Opening(object sender, CancelEventArgs e)
		{
			checkoutToolStripMenuItem.Enabled = renameToolStripMenuItem.Enabled = tvLocalBranches.SelectedNodes.Count == 1;
			var branch = tvLocalBranches.SelectedNode == null ? null : tvLocalBranches.SelectedNode.Tag as LocalBranch;
			mergeFromLocalToolStripMenuItem.Enabled = branch != null;
			trackRemoteToolStripMenuItem.Enabled = branch != null && branch.TracksBranch == null && _remoteBranches.Count > 0;
			deleteToolStripMenuItem.Enabled = getSelectedNodeTags<LocalBranch>(tvLocalBranches).Any();
			enableDisableCompare();
		}


		private void mnuRemote_Opening(object sender, CancelEventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode == null ? null : tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			pullIntoToolStripMenuItem.Enabled = branchToolStripMenuItem.Enabled = downloadToToolStripMenuItem.Enabled = branch != null;
			deleteToolStripMenuItem.Enabled = getSelectedNodeTags<RemoteBranch>(tvRemoteBranches).Any();
			enableDisableCompare();
		}

		private void enableDisableCompare()
		{
			var tv = tvLocalBranches.Focused ? tvLocalBranches : tvRemoteBranches;
			compareToolStripMenuItem.Enabled = compareToolStripMenuItem1.Enabled =
				tvLocalBranches.SelectedNodes.Count + tvRemoteBranches.SelectedNodes.Count == 2 ||
				tv.SelectedNodes.Count == 2;
		}

		private void fetchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_helper.RunCommand("fetch " + tvRemoteBranches.SelectedNode.Text, true);
			RefreshBranches();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = getSelectedNodeTags<LocalBranch>(tvLocalBranches);
			if (MessageBox.Show("Are you sure you want to delete the following branches?\r\n" +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				, "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				List<string> arguments = new List<string>();
				foreach (var s in selected)
				{
					arguments.Add("branch -D " + s.BranchName);
				}

				_helper.RunCommand(arguments.ToArray(), true);
				RefreshBranches();
			}
		}

		private void deleteRemoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = getSelectedNodeTags<RemoteBranch>(tvRemoteBranches);
			if (MessageBox.Show("Are you sure you want to delete the following remote branches?\r\n" +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				, "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				List<string> arguments = new List<string>();
				foreach (var s in selected)
				{
					var branchName = s.BranchName;
					if (branchName.StartsWith("origin/"))
						branchName = branchName.Substring(7);

					arguments.Add("push -d origin " + branchName);
				}
				_helper.RunCommand(arguments.ToArray(), true);
				RefreshBranches();
			}
		}

		private void ucRepository_Load(object sender, EventArgs e)
		{
		}

		private bool _lockChange = false;
		private List<Difference> _previousDifferences;

		private void refreshPage(string[] forFiles = null)
		{
			progMain.Visible = true;
			lblStatus.Text = "Loading differences...";

			var bw = new BackgroundWorker();
			bw.DoWork += (object sender, DoWorkEventArgs e) =>
			{
				var diffs = _helper.GetDifferences();
				if (diffs == null) return;

				if (_previousDifferences != null)
				{
					if (diffs.All(x => _previousDifferences.Any(y => y.IsStaged == x.IsStaged && y.IsConflict == x.IsConflict && y.DifferenceType == x.DifferenceType && y.FileName == x.FileName))
					 && _previousDifferences.All(x => diffs.Any(y => y.IsStaged == x.IsStaged && y.IsConflict == x.IsConflict && y.DifferenceType == x.DifferenceType && y.FileName == x.FileName)))
						return;
				}

				e.Result = diffs;
			};
			bw.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
			{
				progMain.Visible = false;
				lblStatus.Text = string.Empty;

				TreeNode selectedNode = null;

				int scrollStagedY;
				int scrollUnStagedY;
				try
				{
					scrollStagedY = GetScrollPos(tvStaged.Handle, SB_VERT);
					scrollUnStagedY = GetScrollPos(tvUnStaged.Handle, SB_VERT);
				}
				catch
				{
					// MONO?
					scrollStagedY = -1;
					scrollUnStagedY = -1;
				}

				var selectedDiff = tvUnStaged.SelectedNode == null ? null : tvUnStaged.SelectedNode.Tag as Difference;
				var selectedStaged = tvStaged.SelectedNode == null ? null : tvStaged.SelectedNode.Tag as Difference;
				if (selectedDiff != null) refreshDifferences(selectedDiff);

				var diffs = e.Result as List<Difference>;

				var checkForStaged = diffs ?? _previousDifferences;

				if (checkForStaged != null && forFiles != null)
				{
					var changedDiff = checkForStaged.FirstOrDefault(d => d.IsStaged && !d.FileName.Contains(" -> ")
						&& forFiles.Any(f => new FileInfo(Path.Combine(_repository.LocalPath, d.FileName)).FullName == f));
					if (changedDiff != null)
					{
						_lockChange = true;
						if (!changedDiff.IsConflict)
						{
							_helper.RunCommand("reset -- " + getEscapedFile(changedDiff.FileName));
							_helper.RunCommand("add " + getEscapedFile(changedDiff.FileName));
						}
						refreshPage();
						if (selectedStaged != null && changedDiff.FileName == selectedStaged.FileName)
						{
							refreshDifferences(changedDiff);
						}
						_lockChange = false;
						return;
					}
				}

				if (diffs == null) return;
				_previousDifferences = diffs;

				tvUnStaged.BeginUpdate();
				tvStaged.BeginUpdate();
				tvUnStaged.Nodes.Clear();
				tvStaged.Nodes.Clear();

				List<TreeNode> expandedNodes = new List<TreeNode>();
				foreach (var diff in diffs.OrderBy(d => d.FileName))
				{
					var tv = diff.IsStaged ? tvStaged : tvUnStaged;
					var parts = diff.FileName.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
					TreeNode node = null;
					string runningPath = string.Empty;
					for (int i = 0; i < parts.Length; i++)
					{
						var part = parts[i];
						var nodeCollection = node == null ? tv.Nodes : node.Nodes;

						var foundNode = nodeCollection.OfType<TreeNode>().FirstOrDefault(n => n.Text == part);
						if (foundNode == null)
						{
							var nodeText = part;
							bool isConflict = false;
							if (i == parts.Length - 1)
							{
								switch (diff.DifferenceType)
								{
									case DifferenceType.Add:
										nodeText = "A: " + nodeText;
										break;
									case DifferenceType.Modify:
										nodeText = "M: " + nodeText;
										break;
									case DifferenceType.Delete:
										nodeText = "D: " + nodeText;
										break;
									case DifferenceType.Rename:
										nodeText = "R: " + nodeText;
										break;

								}
								isConflict = diff.IsConflict;
							}
							foundNode = nodeCollection.Add(nodeText);
							if (isConflict)
							{
								foundNode.NodeFont = new Font(foundNode.NodeFont ?? tv.Font, FontStyle.Bold);
								foundNode.ForeColor = Color.Red;
							}
						}
						node = foundNode;
						runningPath = node.Text + runningPath;
						if (_collapsed == null || !_collapsed.Contains(runningPath))
							expandedNodes.Add(node);
					}
					node.Tag = diff;
					if (selectedDiff != null && diff.FileName == selectedDiff.FileName)
						tv.SelectedNode = node;
					if (selectedStaged != null && diff.FileName == selectedStaged.FileName)
						tv.SelectedNode = node;
					if (tv.SelectedNode != null)
						selectedNode = tv.SelectedNode;
				}
				//if (_expandeds == null)
				//{
				//	tvUnStaged.ExpandAll();
				//	tvStaged.ExpandAll();
				//}
				//else
				//{
				foreach (var exp in expandedNodes)
				{
					exp.Expand();
				}
				//}
				tvUnStaged.EndUpdate();
				tvStaged.EndUpdate();

				if (scrollStagedY >= 0)
				{
					SetScrollPos(tvStaged.Handle, SB_VERT, scrollStagedY, true);
				}
				else
				{
					// TODO
				}

				if (scrollUnStagedY >= 0)
				{
					SetScrollPos(tvUnStaged.Handle, SB_VERT, scrollUnStagedY, true);
				}
				else
				{
					// TODO
				}

				btnStash.Enabled = true; // TODO: conditional
				progMain.Visible = false;
				lblStatus.Text = string.Empty;

				if (selectedNode != null)
					selectedNode.EnsureVisible();
			};

			bw.RunWorkerAsync();
		}

		private void viewExternalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var settings = Common.SettingsHelper.GetUserSettings<GitUserSettings>();
			if (string.IsNullOrEmpty(settings.ExternalDiffApplication))
			{
				MessageBox.Show("No external diff application has been setup!");
				return;
			}

			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			foreach (var node in tv.GetSelectedFlattenedNodes())
			{
				if (node.Tag == null) continue;
				var diff = node.Tag as Difference;
				if (diff.DifferenceType != DifferenceType.Modify) continue;
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
				var tmpFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				String currFile = String.Empty;
				if (_repository.SSHConnection != null)
				{
					currFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
					var repoPath = _repository.SSHConnection.Path;
					if (repoPath.EndsWith("/"))
					{
						repoPath = repoPath.TrimEnd('/');
					}
					var content = SSHHelper.RunCommand(_repository.SSHConnection, $"cat {repoPath}/{diff.FileName.Replace("\"", "")}", true);
					File.WriteAllText(currFile, content);
				}
				else
				{
					currFile = Path.Combine(Repository.LocalPath, diff.FileName.Replace("\"", ""));
				}
				bool error = false;
				var oldContent = _helper.RunCommand("show " + _currentBranch + ":" + getEscapedFile(diff.FileName), true, false, ref error);
				if (error) return;
				File.WriteAllLines(tmpFile, oldContent);

				if (diff.IsConflict)
				{
					var currContent = File.ReadAllLines(currFile);
					List<string> parsedContent = new List<string>();

					bool inHead = false;
					bool inBranch = false;
					foreach (var line in currContent)
					{
						if (line.StartsWith("<<<<<<< "))
						{
							inHead = true;
						}
						else if (inHead)
						{
							if (line == "=======")
							{
								inBranch = true;
								inHead = false;
							}
						}
						else
						{
							// TODO: parse branch
							if (inBranch && line.StartsWith(">>>>>>> "))
							{
								inBranch = false;
							}
							else
							{
								parsedContent.Add(line);
							}
						}
					}
					currFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
					File.WriteAllLines(currFile, parsedContent.ToArray());
				}

				Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, currFile, tmpFile));
			}
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to undo selected files?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var differences = getSelectedNodeTags<Difference>(tv);
			var worker = new BackgroundWorker();
			worker.DoWork += (object sender2, DoWorkEventArgs e2) =>
			{
				int i = 1;
				foreach (var selectedItem in differences)
				{
					worker.ReportProgress(100 * i++ / differences.Count, "Undoing " + selectedItem.FileName);
					if (tv == tvStaged)
						_helper.RunCommand("reset -- " + getEscapedFile(selectedItem.FileName));
					if (selectedItem.DifferenceType == DifferenceType.Add)
					{
						if (_repository.SSHConnection != null)
						{
							SSHHelper.RunCommand(_repository.SSHConnection, $"rm {_repository.SSHConnection.Path}/{selectedItem.FileName}");
						}
						else
						{
							var path = Path.Combine(Repository.LocalPath, selectedItem.FileName);
							if (selectedItem.FileName.EndsWith("/"))
								Directory.Delete(path, true);
							else if (File.Exists(path))
								File.Delete(path);
						}
					}
					else
					{
						_helper.RunCommand("checkout -- " + getEscapedFile(selectedItem.FileName));
					}
				}
			};
			WinControls.WinProgressBox.ShowProgress(worker, "Undoing changes");
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void ignoreToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to ignore selected files?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var nodes = tv.SelectedNodes;
			List<string> selectedItems = new List<string>();
			foreach (var node in nodes)
			{
				if (tv == tvStaged && node.Tag is Difference)
					_helper.RunCommand("reset " + getEscapedFile((node.Tag as Difference).FileName));

				var runningNode = node;
				if (node.Tag is Difference)
				{
					selectedItems.Add((node.Tag as Difference).FileName);
				}
				else
				{
					var runningText = string.Empty;
					while (runningNode != null)
					{
						runningText = runningNode.Text + (string.IsNullOrEmpty(runningText) && runningNode.Nodes.Count <= 0 ? "" : "/") + runningText;
						runningNode = runningNode.Parent;
					}
					selectedItems.Add(runningText);
				}
			}
			if (Repository.SSHConnection != null)
			{
				SSHHelper.RunCommand(_repository.SSHConnection, $"echo \"{string.Join("\n", selectedItems)}\" >> {_repository.SSHConnection.Path}/.gitignore");
			}
			else
			{
				File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), selectedItems);
			}
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void mnuDiffs_Opening(object sender, CancelEventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var selectedItems = getSelectedNodeTags<Difference>(tv);
			var diff = tv.SelectedNode == null ? null : tv.SelectedNode.Tag as Difference;
			resolveConflictToolStripMenuItem.Enabled =
			resolveUsingMineToolStripMenuItem.Enabled =
			resolveUsingTheirsToolStripMenuItem.Enabled =
				diff != null && diff.IsConflict;
			fileHistoryToolStripMenuItem.Enabled = diff != null && diff.DifferenceType != DifferenceType.Add;
			stageAllToolStripMenuItem.Visible = stageToolStripMenuItem.Visible = tvUnStaged.Focused;
			unstageAllToolStripMenuItem.Visible = unStageToolStripMenuItem.Visible = tvStaged.Focused;
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (_selectedWatcher != null)
			{
				_selectedWatcher.EnableRaisingEvents = false;
				_selectedWatcher.Dispose();
				_selectedWatcher = null;
			}

			if (sender == tvStaged)
			{
				if (tvUnStaged.SelectedNode != null || tvUnStaged.SelectedNodes.Any())
				{
					tvUnStaged.SelectedNode = null;
					tvUnStaged.SelectedNodes.Clear();
					tvUnStaged.Invalidate();
				}
			}
			else
			{
				if (tvStaged.SelectedNode != null || tvStaged.SelectedNodes.Any())
				{
					tvStaged.SelectedNode = null;
					tvStaged.SelectedNodes.Clear();
					tvStaged.Invalidate();
				}
			}
			if (e.Node.Tag == null)
			{
				ucDiff.ClearDifferences();
				return;
			}

			var diff = e.Node.Tag as Difference;
			if (Repository.SuspendWatchingFiles)
			{
				var fileToWatch = Path.GetDirectoryName(Path.Combine(_repository.LocalPath, diff.FileName.Replace("/", "\\")));
				_selectedWatcher = new FileSystemWatcher(fileToWatch);
				_selectedWatcher.EnableRaisingEvents = true;
				_selectedWatcher.Changed += (object sender2, FileSystemEventArgs e2) =>
				{
					this.Invoke(new Action(() => refreshDifferences(diff)));
				};

			}
			refreshDifferences(diff);
		}

		private void refreshDifferences(Difference diff)
		{
			progMain.Visible = true;
			lblStatus.Text = "Retrieving difference for " + diff.FileName;
			Common.Common.RunInThread(new Action(() =>
			{
				var diffs = new string[0];
				if (diff != null && diff.DifferenceType == DifferenceType.Modify)
				{
					diffs = _helper.RunCommand("diff " + (diff.IsStaged ? "--cached " : "") + (chkIgnoreWhiteSpace.Checked ? "-w " : "") + getEscapedFile(diff.FileName));
				}
				else if (diff != null && diff.DifferenceType == DifferenceType.Add)
				{
					if (_repository.SSHConnection != null)
					{
						var content = SSHHelper.RunCommand(_repository.SSHConnection, $"cat {_repository.SSHConnection.Path}/{diff.FileName.Replace("\"", "")}", true);
						diffs = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
					}
					else
					{
						var path = Path.Combine(Repository.LocalPath, diff.FileName.Replace("\"", ""));
						if (File.Exists(path))
						{
							var content = File.ReadAllText(Path.Combine(Repository.LocalPath, diff.FileName.Replace("\"", "")));
							diffs = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
						}
					}
				}
				this.Invoke(new Action(() =>
				{
					ucDiff.SetDifferences(diffs, diff.DifferenceType);
					progMain.Visible = false;
					lblStatus.Text = string.Empty;
				}));
			}));
		}

		private List<string> _collapsed = null;
		private void tv_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			if (_collapsed == null) _collapsed = new List<string>();
			var running = string.Empty;
			var node = e.Node;
			while (node != null)
			{
				running += node.Text;
				node = node.Parent;
			}

			if (!_collapsed.Contains(running))
				_collapsed.Add(running);
		}

		private void tv_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (_collapsed == null) _collapsed = new List<string>();
			var running = string.Empty;
			var node = e.Node;
			while (node != null)
			{
				running += node.Text;
				node = node.Parent;
			}

			if (_collapsed.Contains(running))
				_collapsed.Remove(running);
		}

		private void branchLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmBranch();
			frm.BranchFrom = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			frm.Repository = Repository;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				RefreshBranches();
			}
		}

		private void btnCommit_Click(object sender, EventArgs e)
		{
			var frm = new frmCommit();
			frm.RemoteBranches = _remoteBranches;
			frm.LocalBranch = _currentBranch;
			frm.Repository = _repository;
			frm.ShowDialog();
			RefreshBranches();
			ucDiff.ClearDifferences();
			_previousDifferences = null;
			refreshPage();
		}

		private void btnStash_Click(object sender, EventArgs e)
		{
			var frm = new frmStash();
			frm.Repository = _repository;
			frm.ShowDialog();
			RefreshBranches();
			ucDiff.ClearDifferences();
			_previousDifferences = null;
			refreshPage();
		}

		private void mergeFromLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("merge " + branch.BranchName, true);
				if (Repository.SSHConnection != null)
				{
					RefreshBranches(true);
					refreshPage();
				}
			}
		}

		private void mergeFromToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("merge " + branch.BranchName, true);
				if (Repository.SSHConnection != null)
				{
					RefreshBranches(true);
					refreshPage();
				}
			}
		}

		private void rebaseFromLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			if (MessageBox.Show("Are you sure you want to rebase " + branch.BranchName + " onto " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("rebase " + branch.BranchName, true);
				if (Repository.SSHConnection != null)
				{
					RefreshBranches(true);
					refreshPage();
				}
			}
		}

		private void rebaseFromToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			if (MessageBox.Show("Are you sure you want to rebase " + branch.BranchName + " onto " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("rebase " + branch.BranchName, true);
				if (Repository.SSHConnection != null)
				{
					RefreshBranches(true);
					refreshPage();
				}
			}
		}

		private void resolveConflictToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var diff = tv.SelectedNode.Tag as Difference;
			_helper.RunCommand("add " + getEscapedFile(diff.FileName), false);
			_previousDifferences = null;
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void NextConflictToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var flattened = tv.GetFlattenedNodes();
			foreach (var node in flattened)
			{
				if (node.Tag is Difference diff && diff.IsConflict)
				{
					tv.SelectedNode = node;
					node.EnsureVisible();
					return;
				}
			}

			MessageBox.Show("No conflicts!");
		}

		private void stageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvUnStaged))
			{
				_helper.RunCommand("add " + getEscapedFile(selectedItem.FileName));
			}
			_previousDifferences = null;
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void unStageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvStaged))
			{
				_helper.RunCommand("reset -- " + getEscapedFile(selectedItem.FileName));
			}
			_previousDifferences = null;
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void ignoreExtensionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to ignore selected files?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var differences = getSelectedNodeTags<Difference>(tv);
			List<string> selectedItems = new List<string>();
			foreach (var diff in differences)
			{
				if (tv == tvStaged)
					_helper.RunCommand("reset " + getEscapedFile(diff.FileName));

				var finf = new FileInfo(Path.Combine(Repository.LocalPath, diff.FileName));
				if (finf.Exists)
				{
					selectedItems.Add("**/*" + finf.Extension);
				}
			}
			File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), selectedItems);
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void abortMergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to abort merge for " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("merge --abort");
			}
		}

		private void concludeMergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to conclude merge for " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("merge --continue");
			}
		}

		private void abortRebaseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to abort rebase for " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("rebase --abort");
			}
		}

		private object _draggingTreeView;
		private void tv_NodesDrag(object sender, DragEventArgs e)
		{
			_draggingTreeView = sender;
		}

		private void tv_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(List<TreeNode>).FullName) && sender != _draggingTreeView)
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void tv_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(List<TreeNode>).FullName) && sender != _draggingTreeView)
			{
				var items = MultiSelectTreeView.GetFlattenedNodes(e.Data.GetData(typeof(List<TreeNode>).FullName) as List<TreeNode>)
					.Select(n => n.Tag as Difference)
					.Where(d => d != null);
				foreach (var selectedItem in items)
				{
					var cmd = _draggingTreeView == tvUnStaged ? "add " : "reset -- ";
					_helper.RunCommand(cmd + getEscapedFile(selectedItem.FileName));
				}
				_previousDifferences = null;
				ucDiff.ClearDifferences();
				refreshPage();
			}
		}

		private void btnStatus_Click(object sender, EventArgs e)
		{
			ScrollableMessageBox.Show(_helper.RunCommand("status"), "Status");
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			RefreshBranches(true);
			refreshPage();
		}

		private void compareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvLocalBranches.Focused ? tvLocalBranches : tvRemoteBranches;
			Branch branch1 = null;
			Branch branch2 = null;
			if (tv.SelectedNodes.Count == 2)
			{
				branch1 = tv.SelectedNodes.First().Tag as Branch;
				branch2 = tv.SelectedNodes.Last().Tag as Branch;
			}
			else
			{
				var selectedNodes = tvLocalBranches.SelectedNodes.Union(tvRemoteBranches.SelectedNodes);
				branch1 = selectedNodes.First().Tag as Branch;
				branch2 = selectedNodes.Last().Tag as Branch;
			}
			var frm = new frmCompareBranches();
			frm.Helper = _helper;
			frm.FromBranch = branch1;
			frm.ToBranch = branch2;
			frm.Show();
		}

		private void historyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvLocalBranches.Focused ? tvLocalBranches : tvRemoteBranches;
			if (tv.SelectedNode == null || tv.SelectedNode.Tag == null) return;
			var branch = tv.SelectedNode.Tag as Branch;
			var frm = new frmCommitHistory();
			frm.BranchCreated += (object s, EventArgs e2) => RefreshBranches();
			frm.Helper = _helper;
			frm.Branch = branch;
			frm.Show();
		}

		private List<TTagType> getSelectedNodeTags<TTagType>(MultiSelectTreeView tv)
		{
			var flattened = tv.GetSelectedFlattenedNodes();
			return flattened.Where(f => f.Tag is TTagType).Select(f => (TTagType)f.Tag).ToList();
		}

		private void btnViewStashes_Click(object sender, EventArgs e)
		{
			var frm = new frmStashes();
			frm.Helper = _helper;
			frm.Repository = Repository;
			frm.BranchCreated += (sender2, e2) =>
			{
				this.RefreshBranches();
			};
			frm.Show();
		}

		private void tvLocalBranches_DoubleClick(object sender, EventArgs e)
		{
			checkoutToolStripMenuItem_Click(sender, e);
		}

		private void tv_DoubleClick(object sender, EventArgs e)
		{
			viewExternalToolStripMenuItem_Click(sender, e);
		}

		private void stageAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> arguments = new List<string>();
			var flattened = MultiSelectTreeView.GetFlattenedNodes(tvUnStaged.Nodes.OfType<TreeNode>())
					.Where(f => f.Tag is Difference).Select(f => f.Tag as Difference).ToList();
			foreach (var flat in flattened)
			{
				arguments.Add("add " + getEscapedFile(flat.FileName));
			}
			_helper.RunCommand(arguments.ToArray(), true);
			_previousDifferences = null;
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void unstageAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> arguments = new List<string>();
			var flattened = MultiSelectTreeView.GetFlattenedNodes(tvStaged.Nodes.OfType<TreeNode>())
				.Where(f => f.Tag is Difference).Select(f => f.Tag as Difference).ToList();
			foreach (var flat in flattened)
			{
				arguments.Add("reset -- " + getEscapedFile(flat.FileName));
			}
			_helper.RunCommand(arguments.ToArray(), true);
			_previousDifferences = null;
			ucDiff.ClearDifferences();
			refreshPage();
		}

		private void pruneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_helper.RunCommand("remote prune " + tvRemoteBranches.SelectedNode.Text, true);
			RefreshBranches();
		}

		private void btnPull_Click(object sender, EventArgs e)
		{
			var branchName = _currentBranch.TracksBranch.BranchName;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);
			removeWatchers();
			_helper.RunCommand("pull origin " + branchName, true);
			addWatchers();
			RefreshBranches();
			// TEMP ugly workaround
			if (this.Repository.SSHConnection != null) RefreshBranches();

		}

		private void btnPush_Click(object sender, EventArgs e)
		{
			var frm = new frmPush();
			frm.RemoteBranches = _remoteBranches;
			frm.LocalBranch = _currentBranch;
			frm.Repository = _repository;
			frm.ShowDialog();
			RefreshBranches();
		}

		private void pullIntoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			var branchName = branch.BranchName;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);
			_helper.RunCommand("pull origin " + branchName, true);
			RefreshBranches();
		}

		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNodes[0].Tag as LocalBranch;
			var result = WinControls.InputBox.Show("Enter new branch name:", "Rename Branch", branch.BranchName);
			if (result.Result == DialogResult.OK)
			{
				_helper.RunCommand("branch -m " + branch.BranchName + " " + result.Text, true);
				RefreshBranches();
			}
		}

		private void resolveUsingMineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tv))
			{
				_helper.RunCommand("checkout --ours " + selectedItem.FileName);
				_helper.RunCommand("add " + selectedItem.FileName, false);
			}
			_previousDifferences = null;
			refreshPage();
		}

		private void resolveUsingTheirsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tv))
			{
				_helper.RunCommand("checkout --theirs " + selectedItem.FileName);
				_helper.RunCommand("add " + selectedItem.FileName, false);
			}

			_previousDifferences = null;
			refreshPage();
		}

		private void fileHistoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var diff = tv.SelectedNode == null ? null : tv.SelectedNode.Tag as Difference;
			var frm = new frmCommitHistory();
			frm.BranchCreated += (object s, EventArgs e2) => RefreshBranches();
			frm.Helper = _helper;
			frm.FileName = diff.FileName;
			frm.Show();
		}

		private void fileHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			var frm = new frmFileHistory();
			frm.Helper = _helper;
			frm.Show();
		}

		private void trackRemoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmTrackRemote();
			frm.BranchFrom = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			frm.Repository = Repository;
			frm.RemoteBranches = _remoteBranches;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				RefreshBranches();
			}
		}

		private void downloadToToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			_helper.DownloadBranch(branch, _repository.RemoteURL);
		}

		private void CheckoutForceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tvLocalBranches.SelectedNode == null || tvLocalBranches.SelectedNode.Tag == null) return;
			_lockChange = true;
			removeWatchers();
			_helper.RunCommand("checkout -f " + tvLocalBranches.SelectedNode.Tag.ToString(), true);
			RefreshBranches();
			addWatchers();
			_lockChange = false;
		}

		private void pullAndMergeFromToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("pull origin " + branch.BranchName, true);
				_helper.RunCommand("merge " + branch.BranchName, true);
			}
		}

		private void chkIgnoreWhiteSpace_CheckedChanged(object sender, EventArgs e)
		{
			refreshPage();
		}

		private void mergeFromSquashToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			if (MessageBox.Show("Are you sure you want to merge and squash " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("merge --squash " + branch.BranchName, true);
			}
		}

		private string getEscapedFile(string file)
		{
			var escaped = file;
			if (escaped.Contains(" "))
			{
				escaped = "\"" + escaped + "\"";
			}
			return escaped;
		}
	}
}
