using PaJaMa.WinControls.MultiSelectTreeView;
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

		public ucRepository()
		{
			InitializeComponent();
		}

		public void Init()
		{
			if (!_inited)
			{
				refreshBranches(true);
				_previousDifferences = null;
				timDiff_Tick(this, new EventArgs());
				timDiff.Enabled = true;
				_inited = true;
			}
		}

		private void refreshBranches(bool initial = false)
		{
			var branches = _helper.GetBranches();
			if (branches.Count < 1) return;

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
		}

		private void branchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmBranch();
			frm.BranchFrom = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			frm.Repository = Repository;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				refreshBranches();
			}
		}

		private void checkoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tvLocalBranches.SelectedNode == null || tvLocalBranches.SelectedNode.Tag == null) return;
			bool error = false;
			_helper.RunCommand("checkout " + tvLocalBranches.SelectedNode.Tag.ToString(), ref error);
			refreshBranches();
		}

		private void mnuLocal_Opening(object sender, CancelEventArgs e)
		{
			checkoutToolStripMenuItem.Enabled = renameToolStripMenuItem.Enabled = tvLocalBranches.SelectedNodes.Count == 1;
			var branch = tvLocalBranches.SelectedNode == null ? null : tvLocalBranches.SelectedNode.Tag as LocalBranch;
			mergeFromLocalToolStripMenuItem.Enabled = branch != null;
			deleteToolStripMenuItem.Enabled = getSelectedNodeTags<LocalBranch>(tvLocalBranches).Any();
			enableDisableCompare();
		}


		private void mnuRemote_Opening(object sender, CancelEventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode == null ? null : tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			pullIntoToolStripMenuItem.Enabled = branchToolStripMenuItem.Enabled = branch != null;
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
			bool error = false;
			_helper.RunCommand("fetch " + tvRemoteBranches.SelectedNode.Text, ref error);
			// if (error) return;
			refreshBranches();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = getSelectedNodeTags<LocalBranch>(tvLocalBranches);
			if (MessageBox.Show("Are you sure you want to delete the following branches?\r\n" +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				, "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				List<string> lines = new List<string>();
				foreach (var s in selected)
				{
					lines.AddRange(_helper.RunCommand("branch -D " + s.BranchName));
				}
				if (lines.Any())
					MessageBox.Show(string.Join("\r\n", lines));
				refreshBranches();
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
				List<string> lines = new List<string>();
				foreach (var s in selected)
				{
					var branchName = s.BranchName;
					if (branchName.StartsWith("origin/"))
						branchName = branchName.Substring(7);

					lines.AddRange(_helper.RunCommand("push -d origin " + branchName));
				}
				if (lines.Any())
					MessageBox.Show(string.Join("\r\n", lines));
				refreshBranches();
			}
		}

		private void ucRepository_Load(object sender, EventArgs e)
		{
		}

		private List<Difference> _previousDifferences;
		private void timDiff_Tick(object sender, EventArgs e)
		{
			if (!this.Parent.Visible)
				return;
			var diffs = _helper.GetDifferences();
			if (diffs == null) return;

			if (_previousDifferences != null)
			{
				if (diffs.All(x => _previousDifferences.Any(y => y.IsStaged == x.IsStaged && y.IsConflict == x.IsConflict && y.DifferenceType == x.DifferenceType && y.FileName == x.FileName))
				 && _previousDifferences.All(x => diffs.Any(y => y.IsStaged == x.IsStaged && y.IsConflict == x.IsConflict && y.DifferenceType == x.DifferenceType && y.FileName == x.FileName)))
					return;
			}

			_previousDifferences = diffs;

			var selectedDiff = tvUnStaged.SelectedNode == null ? null : tvUnStaged.SelectedNode.Tag as Difference;
			var selectedStaged = tvStaged.SelectedNode == null ? null : tvStaged.SelectedNode.Tag as Difference;

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
				if (node.Tag == null) return;
				var currFile = Path.Combine(Repository.LocalPath, (node.Tag as Difference).FileName);
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
				var tmpFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				bool error = false;
				var oldContent = _helper.RunCommand("--no-pager show " + _currentBranch + ":\"" + (node.Tag as Difference).FileName + "\"", ref error);
				if (error) return;
				File.WriteAllLines(tmpFile, oldContent);
				Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, currFile, tmpFile));
			}
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to undo selected files?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tv))
			{
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
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
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
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void mnuDiffs_Opening(object sender, CancelEventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var selectedItems = getSelectedNodeTags<Difference>(tv);
			var diff = tv.SelectedNode == null ? null : tv.SelectedNode.Tag as Difference;
			resolveConflictToolStripMenuItem.Enabled = diff != null && diff.IsConflict;
			viewExternalToolStripMenuItem.Enabled = diff != null && diff.DifferenceType == DifferenceType.Modify;
			stageAllToolStripMenuItem.Enabled = stageToolStripMenuItem.Enabled = tvUnStaged.Focused;
			unstageAllToolStripMenuItem.Enabled = unStageToolStripMenuItem.Enabled = tvStaged.Focused;
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag == null)
			{
				txtDiffText.Text = string.Empty;
				return;
			}

			var diff = e.Node.Tag as Difference;
			var diffs = diff == null || diff.DifferenceType != DifferenceType.Modify ? new string[0] : _helper.RunCommand("--no-pager diff " + (diff.IsStaged ? "--cached " : "") + "\"" + diff.FileName + "\"");
			// if (error) return;
			txtDiffText.Text = string.Join("\r\n", diffs);
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
				refreshBranches();
			}
		}

		private void btnCommit_Click(object sender, EventArgs e)
		{
			var frm = new frmCommit();
			frm.RemoteBranches = _remoteBranches;
			frm.LocalBranch = _currentBranch;
			frm.Repository = _repository;
			frm.ShowDialog();
			refreshBranches();
			txtDiffText.Text = string.Empty;
			_previousDifferences = null;
			timDiff_Tick(this, new EventArgs());
		}

		private void btnStash_Click(object sender, EventArgs e)
		{
			var frm = new frmStash();
			frm.Repository = _repository;
			frm.ShowDialog();
			refreshBranches();
			txtDiffText.Text = string.Empty;
			_previousDifferences = null;
			timDiff_Tick(this, new EventArgs());
		}

		private void mergeFromLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				bool error = false;
				_helper.RunCommand("merge " + branch.BranchName, ref error);
			}
		}

		private void mergeFromToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				bool error = false;
				_helper.RunCommand("merge " + branch.BranchName, ref error);
			}
		}

		private void resolveConflictToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var diff = tv.SelectedNode.Tag as Difference;
			bool error = false;
			_helper.RunCommand("add " + diff.FileName, ref error);
			_previousDifferences = null;
			txtDiffText.Text = string.Empty;
		}

		private void stageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvUnStaged))
			{
				_helper.RunCommand("add " + selectedItem.FileName);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void unStageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvStaged))
			{
				_helper.RunCommand("reset -- " + selectedItem.FileName);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
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
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
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
				txtDiffText.Text = string.Empty;
				timDiff_Tick(this, new EventArgs());
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
				this.refreshBranches();
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
			var flattened = MultiSelectTreeView.GetFlattenedNodes(tvUnStaged.Nodes.OfType<TreeNode>());
			foreach (var flat in flattened.Where(f => f.Tag is Difference).Select(f => f.Tag as Difference))
			{
				_helper.RunCommand("add " + flat.FileName);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void unstageAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var flattened = MultiSelectTreeView.GetFlattenedNodes(tvStaged.Nodes.OfType<TreeNode>());
			foreach (var flat in flattened.Where(f => f.Tag is Difference).Select(f => f.Tag as Difference))
			{
				_helper.RunCommand("reset -- " + flat.FileName);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void pruneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool error = false;
			_helper.RunCommand("remote prune " + tvRemoteBranches.SelectedNode.Text, ref error);
			// if (error) return;
			refreshBranches();
		}

		private void btnPull_Click(object sender, EventArgs e)
		{
			var branchName = _currentBranch.TracksBranch.BranchName;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);
			var lines = _helper.RunCommand("pull origin " + branchName);
			if (lines.Length > 0)
				MessageBox.Show(string.Join("\r\n", lines));
			// if (error) return;
			refreshBranches();
		}

		private void btnPush_Click(object sender, EventArgs e)
		{
			var frm = new frmPush();
			frm.RemoteBranches = _remoteBranches;
			frm.LocalBranch = _currentBranch;
			frm.Repository = _repository;
			frm.ShowDialog();
			refreshBranches();
		}

		private void pullIntoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			var branchName = branch.BranchName;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);
			var lines = _helper.RunCommand("pull origin " + branchName);
			if (lines.Length > 0)
				MessageBox.Show(string.Join("\r\n", lines));
			// if (error) return;
			refreshBranches();
		}

		private void renameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNodes[0].Tag as LocalBranch;
			var result = WinControls.InputBox.Show("Enter new branch name:", "Rename Branch");
			if (result.Result == DialogResult.OK)
			{
				_helper.RunCommand("branch -m " + branch.BranchName + " " + result.Text);
				refreshBranches();
			}
		}
	}
}
