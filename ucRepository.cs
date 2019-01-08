using PaJaMa.WinControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class ucRepository : UserControl
	{
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
				_helper = new GitHelper(value.LocalPath);
			}
		}

		private bool _inited = false;
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
				resetWatchers();
				refreshPage();
				_inited = true;
			}
		}

		public void Deactivate()
		{
			_inited = false;
			removeWatchers();
		}

		public bool RefreshBranches(bool initial = false)
		{
			var branches = _helper.GetBranches(initial);
			if (branches == null) return false;
			if (branches.Count < 1) return true;

			_currentBranch = branches.OfType<LocalBranch>().First(b => b.IsCurrent);
			_remoteBranches = branches.OfType<RemoteBranch>().ToList();

			bool remote = true;
			while (true)
			{
				bool nodeSelected = false;
				var tv = remote ? tvRemoteBranches : tvLocalBranches;
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
				if (remote) remote = false;
				else break;
			}

			btnPull.Enabled = _currentBranch.TracksBranch != null;
			_previousDifferences = null;
			refreshPage();
			return true;
		}

		private void removeWatchers()
		{
			for (int i = _watchers.Count - 1; i >= 0; i--)
			{
				var watcher = _watchers[i];
				_watchers.RemoveAt(i);
				watcher.EnableRaisingEvents = false;
				watcher.Dispose();
				watcher = null;
			}
		}

		private void resetWatchers()
		{
			removeWatchers();
			for (int i = _watchers.Count - 1; i >= 0; i--)
			{
				var watcher = _watchers[i];
				_watchers.RemoveAt(i);
				watcher.EnableRaisingEvents = false;
				watcher.Dispose();
				watcher = null;
			}

			var lst = _helper.RunCommand("ls-files");

			var listedDirectories = new List<string>();
			foreach (var l in lst)
			{
				var directory = Path.GetDirectoryName(Path.Combine(_repository.LocalPath, l.Replace("/", "\\")));
				// if (directory == _repository.LocalPath) continue;
				if (listedDirectories.Contains(l)) continue;
				var watcher = new FileSystemWatcher(directory);
				watcher.EnableRaisingEvents = true;
				watcher.Deleted += Watcher_Changed;
				watcher.Changed += Watcher_Changed;
				watcher.Created += Watcher_Changed;
				watcher.Renamed += Watcher_Changed;
				_watchers.Add(watcher);
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
						Console.WriteLine("REFRESHING");
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
			_helper.RunCommand("checkout " + tvLocalBranches.SelectedNode.Tag.ToString(), true);
			RefreshBranches();
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
			var diffs = _helper.GetDifferences();
			if (diffs == null) return;

			var selectedDiff = tvUnStaged.SelectedNode == null ? null : tvUnStaged.SelectedNode.Tag as Difference;
			var selectedStaged = tvStaged.SelectedNode == null ? null : tvStaged.SelectedNode.Tag as Difference;
			if (selectedDiff != null) refreshDifferences(selectedDiff);

			if (forFiles != null)
			{
				var changedDiff = diffs.FirstOrDefault(d => d.IsStaged
					&& forFiles.Any(f => new FileInfo(Path.Combine(_repository.LocalPath, d.FileName)).FullName == f));
				if (changedDiff != null)
				{
					_lockChange = true;
					_helper.RunCommand("reset -- " + changedDiff.FileName);
					_helper.RunCommand("add " + changedDiff.FileName);
					refreshPage();
					if (selectedStaged != null && changedDiff.FileName == selectedStaged.FileName)
					{
						refreshDifferences(changedDiff);
					}
					_lockChange = false;
					return;
				}
			}

			if (_previousDifferences != null)
			{
				if (diffs.All(x => _previousDifferences.Any(y => y.IsStaged == x.IsStaged && y.IsConflict == x.IsConflict && y.DifferenceType == x.DifferenceType && y.FileName == x.FileName))
				 && _previousDifferences.All(x => diffs.Any(y => y.IsStaged == x.IsStaged && y.IsConflict == x.IsConflict && y.DifferenceType == x.DifferenceType && y.FileName == x.FileName)))
					return;
			}

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
			btnCommit.Enabled = true; // TODO: tvStaged.Nodes.Count > 0;
			btnStash.Enabled = true; // TODO: conditional
			btnCommit.Enabled = tvStaged.Nodes.Count > 0;
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
				var currFile = Path.Combine(Repository.LocalPath, diff.FileName);
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
				var tmpFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				bool error = false;
				var oldContent = _helper.RunCommand("--no-pager show " + _currentBranch + ":\"" + diff.FileName + "\"", false, ref error);
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
						_helper.RunCommand("reset -- " + selectedItem.FileName);
					if (selectedItem.DifferenceType == DifferenceType.Add)
					{
						if (selectedItem.FileName.EndsWith("/"))
							Directory.Delete(Path.Combine(Repository.LocalPath, selectedItem.FileName), true);
						else
							File.Delete(Path.Combine(Repository.LocalPath, selectedItem.FileName));
					}
					else
					{
						_helper.RunCommand("checkout -- " + selectedItem.FileName);
					}
				}
			};
			WinControls.WinProgressBox.ShowProgress(worker, "Undoing changes");
			clearDifferences();
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
					_helper.RunCommand("reset " + (node.Tag as Difference).FileName);

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
						runningText = runningNode.Text + (string.IsNullOrEmpty(runningText) ? "" : "/") + runningText;
						runningNode = runningNode.Parent;
					}
					selectedItems.Add(runningText);
				}
			}
			File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), selectedItems);
			clearDifferences();
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
				clearDifferences();
				return;
			}

			var diff = e.Node.Tag as Difference;
			refreshDifferences(diff);
		}

		private void refreshDifferences(Difference diff)
		{
			var diffs = diff == null || diff.DifferenceType != DifferenceType.Modify ? new string[0] :
				_helper.RunCommand("--no-pager diff " + (diff.IsStaged ? "--cached " : "") + "\"" + diff.FileName + "\"");
			// if (error) return;
			txtDiffText.Text = string.Join("\r\n", diffs);
		}

		private void clearDifferences()
		{
			txtDiffText.Text = string.Empty;
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
			clearDifferences();
			_previousDifferences = null;
			refreshPage();
		}

		private void btnStash_Click(object sender, EventArgs e)
		{
			var frm = new frmStash();
			frm.Repository = _repository;
			frm.ShowDialog();
			RefreshBranches();
			clearDifferences();
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
			}
		}

		private void mergeFromToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				_helper.RunCommand("merge " + branch.BranchName, true);
			}
		}

		private void resolveConflictToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var diff = tv.SelectedNode.Tag as Difference;
			_helper.RunCommand("add " + diff.FileName, false);
			_previousDifferences = null;
			clearDifferences();
			refreshPage();
		}

		private void stageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvUnStaged))
			{
				_helper.RunCommand("add " + selectedItem.FileName);
			}
			_previousDifferences = null;
			clearDifferences();
			refreshPage();
		}

		private void unStageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvStaged))
			{
				_helper.RunCommand("reset -- " + selectedItem.FileName);
			}
			_previousDifferences = null;
			clearDifferences();
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
					_helper.RunCommand("reset " + diff.FileName);

				var finf = new FileInfo(Path.Combine(Repository.LocalPath, diff.FileName));
				if (finf.Exists)
				{
					selectedItems.Add("**/*" + finf.Extension);
				}
			}
			File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), selectedItems);
			clearDifferences();
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
					_helper.RunCommand(cmd + selectedItem.FileName);
				}
				_previousDifferences = null;
				clearDifferences();
				refreshPage();
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			_inited = false;
			Init();
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
				arguments.Add("add " + flat.FileName);
			}
			_helper.RunCommand(arguments.ToArray(), true);
			_previousDifferences = null;
			clearDifferences();
			refreshPage();
		}

		private void unstageAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> arguments = new List<string>();
			var flattened = MultiSelectTreeView.GetFlattenedNodes(tvStaged.Nodes.OfType<TreeNode>())
				.Where(f => f.Tag is Difference).Select(f => f.Tag as Difference).ToList();
			foreach (var flat in flattened)
			{
				arguments.Add("reset -- " + flat.FileName);
			}
			_helper.RunCommand(arguments.ToArray(), true);
			_previousDifferences = null;
			clearDifferences();
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
			_helper.RunCommand("pull origin " + branchName, true);
			RefreshBranches();
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
			var diff = tv.SelectedNode == null ? null : tv.SelectedNode.Tag as Difference;
			_helper.RunCommand("checkout --ours " + diff.FileName);
			_previousDifferences = null;
			refreshPage();
		}

		private void resolveUsingTheirsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var diff = tv.SelectedNode == null ? null : tv.SelectedNode.Tag as Difference;
			_helper.RunCommand("checkout --theirs " + diff.FileName);
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
	}
}
