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
		private Dictionary<bool, List<string>> _checkedNodes = new Dictionary<bool, List<string>>();

		public ucRepository()
		{
			_checkedNodes.Add(false, new List<string>());
			_checkedNodes.Add(true, new List<string>());
			InitializeComponent();
		}

		public void Init()
		{
			if (!_inited)
			{
				refreshBranches();
				_previousDifferences = null;
				timDiff_Tick(this, new EventArgs());
				timDiff.Enabled = true;
				_inited = true;
			}
		}

		private void refreshBranches()
		{
			var branches = _helper.GetBranches();
			if (branches.Count < 1) return;

			_currentBranch = branches.OfType<LocalBranch>().First(b => b.IsCurrent);
			_remoteBranches = branches.OfType<RemoteBranch>().ToList();

			bool remote = true;
			while (true)
			{
				var tv = remote ? tvRemoteBranches : tvLocalBranches;
				tv.BeginUpdate();
				tv.Nodes.Clear();
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
			string error = string.Empty;
			_helper.RunCommand("checkout " + tvLocalBranches.SelectedNode.Tag.ToString(), ref error);
			refreshBranches();
		}

		private void mnuLocal_Opening(object sender, CancelEventArgs e)
		{
			// checkoutToolStripMenuItem.Enabled = lstLocalBranches.SelectedItems.Count == 1;
			var branch = tvLocalBranches.SelectedNode == null ? null : tvLocalBranches.SelectedNode.Tag as LocalBranch;
			pullToolStripMenuItem.Visible = branch != null && branch.TracksBranch != null;
			pushToolStripMenuItem.Visible = branch != null;
			mergeFromLocalToolStripMenuItem.Visible = branch != null;
			deleteToolStripMenuItem.Visible = getSelectedNodeTags<LocalBranch>(tvLocalBranches).Any();
			enableDisableCompare();
		}


		private void mnuRemote_Opening(object sender, CancelEventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode == null ? null : tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			deleteToolStripMenuItem.Visible = getSelectedNodeTags<RemoteBranch>(tvRemoteBranches).Any();
			enableDisableCompare();
		}

		private void enableDisableCompare()
		{
			compareToolStripMenuItem.Enabled = compareToolStripMenuItem1.Enabled =
				tvLocalBranches.SelectedNodes.Count + tvRemoteBranches.SelectedNodes.Count == 2;
		}

		private void fetchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string error = string.Empty;
			_helper.RunCommand("fetch " + tvRemoteBranches.SelectedNode.Text, ref error);
			if (!string.IsNullOrEmpty(error)) return;
			refreshBranches();
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = getSelectedNodeTags<LocalBranch>(tvLocalBranches);
			if (MessageBox.Show("Are you sure you want to delete " +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				+ "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				foreach (var s in selected)
				{
					string error = string.Empty;
					_helper.RunCommand("branch -D " + s.BranchName, ref error);
				}
				refreshBranches();
			}
		}

		private void deleteRemoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = getSelectedNodeTags<RemoteBranch>(tvRemoteBranches);
			if (MessageBox.Show("Are you sure you want to delete " +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				+ "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				foreach (var s in selected)
				{
					string error = string.Empty;
					_helper.RunCommand("branch -d -r " + s.BranchName, ref error);
				}
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
			_lockCheck = true;
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
					if (_expandeds != null && _expandeds.Contains(part))
						expandedNodes.Add(node);
					runningPath += node.Text;
					if (_checkedNodes[diff.IsStaged].Contains(runningPath))
						node.Checked = true;
				}
				node.Tag = diff;
				if (selectedDiff != null && diff.FileName == selectedDiff.FileName)
					tv.SelectedNode = node;
				if (selectedStaged != null && diff.FileName == selectedStaged.FileName)
					tv.SelectedNode = node;
			}
			if (_expandeds == null)
			{
				tvUnStaged.ExpandAll();
				tvStaged.ExpandAll();
			}
			else
			{
				foreach (var exp in expandedNodes)
				{
					exp.Expand();
				}
			}
			tvUnStaged.EndUpdate();
			tvStaged.EndUpdate();
			btnCommit.Enabled = true; // tvStaged.Nodes.Count > 0;
			_lockCheck = false;
		}

		private void viewExternalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			if (tv.SelectedNode == null || tv.SelectedNode.Tag == null) return;
			var currFile = Path.Combine(Repository.LocalPath, (tv.SelectedNode.Tag as Difference).FileName);
			var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
			if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
			var tmpFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
			string error = string.Empty;
			var oldContent = _helper.RunCommand("--no-pager show " + _currentBranch + ":\"" + (tv.SelectedNode.Tag as Difference).FileName + "\"", ref error);
			if (!string.IsNullOrEmpty(error)) return;
			File.WriteAllLines(tmpFile, oldContent);
			Process.Start("WinMerge", currFile + " " + tmpFile);
		}

		private void pullToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			string error = string.Empty;
			var branchName = branch.TracksBranch.BranchName;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);
			_helper.RunCommand("pull origin " + branchName, ref error);
			// if (!string.IsNullOrEmpty(error)) return;
			refreshBranches();
		}

		private void pushToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmPush();
			frm.RemoteBranches = _remoteBranches;
			frm.LocalBranch = _currentBranch;
			frm.Repository = _repository;
			frm.ShowDialog();
			refreshBranches();
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to undo selected files?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tv))
			{
				string error = string.Empty;
				if (tv == tvStaged)
					_helper.RunCommand("reset -- " + selectedItem.FileName, ref error);
				if (selectedItem.DifferenceType == DifferenceType.Add)
				{
					if (selectedItem.FileName.EndsWith("/"))
						Directory.Delete(Path.Combine(Repository.LocalPath, selectedItem.FileName), true);
					else
						File.Delete(Path.Combine(Repository.LocalPath, selectedItem.FileName));
				}
				else
				{
					_helper.RunCommand("checkout -- " + selectedItem.FileName, ref error);
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
				string error = string.Empty;
				if (tv == tvStaged && node.Tag is Difference)
					_helper.RunCommand("reset " + (node.Tag as Difference).FileName, ref error);

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
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			string error = string.Empty;
			if (e.Node.Tag == null)
			{
				txtDiffText.Text = string.Empty;
				return;
			}

			var diff = e.Node.Tag as Difference;
			var diffs = diff == null || diff.DifferenceType != DifferenceType.Modify ? new string[0] : _helper.RunCommand("--no-pager diff " + (diff.IsStaged ? "--cached " : "") + "\"" + diff.FileName + "\"", ref error);
			// if (!string.IsNullOrEmpty(error)) return;
			txtDiffText.Text = string.Join("\r\n", diffs);
		}

		private List<string> _expandeds = null;
		private void tv_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			_expandeds.Remove(e.Node.Text);
		}

		private void tv_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (_expandeds == null) _expandeds = new List<string>();
			if (!_expandeds.Contains(e.Node.Text))
				_expandeds.Add(e.Node.Text);
		}

		private bool _lockCheck = false;
		private void tv_AfterCheck(object sender, TreeViewEventArgs e)
		{
			if (_lockCheck) return;
			string runningPath = string.Empty;
			var runningNode = e.Node;
			while (runningNode != null)
			{
				runningPath = runningNode.Text + runningPath;
				runningNode = runningNode.Parent;
			}
			bool isStaged = sender == tvStaged;
			if (e.Node.Checked)
			{
				if (!_checkedNodes[isStaged].Contains(runningPath)) _checkedNodes[isStaged].Add(runningPath);
				foreach (TreeNode node in e.Node.Nodes)
				{
					node.Checked = true;
				}
			}
			else
			{
				if (e.Node.Parent != null && e.Node.Parent.Checked)
					e.Node.Parent.Checked = false;
				if (_checkedNodes[isStaged].Contains(runningPath))
				{
					_checkedNodes[isStaged].Remove(runningPath);
				}
			}
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
			_previousDifferences = null;
			timDiff_Tick(this, new EventArgs());
		}

		private void mergeFromLocalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvLocalBranches.SelectedNode.Tag as LocalBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				string error = string.Empty;
				_helper.RunCommand("merge " + branch.BranchName, ref error);
			}
		}

		private void mergeFromToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			if (MessageBox.Show("Are you sure you want to merge " + branch.BranchName + " into " + _currentBranch.BranchName + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				string error = string.Empty;
				_helper.RunCommand("merge " + branch.BranchName, ref error);
			}
		}

		private void resolveConflictToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var diff = tv.SelectedNode.Tag as Difference;
			string error = string.Empty;
			_helper.RunCommand("add " + diff.FileName, ref error);
			_previousDifferences = null;
		}

		private void stageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvUnStaged))
			{
				string error = string.Empty;
				_helper.RunCommand("add " + selectedItem.FileName, ref error);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void unStageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvStaged))
			{
				string error = string.Empty;
				_helper.RunCommand("reset -- " + selectedItem.FileName, ref error);
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
				string error = string.Empty;
				if (tv == tvStaged)
					_helper.RunCommand("reset " + diff.FileName, ref error);

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
				string error = string.Empty;
				_helper.RunCommand("merge --abort", ref error);
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
				var items = (sender as MultiSelectTreeView).GetFlattenedNodes(e.Data.GetData(typeof(List<TreeNode>).FullName) as List<TreeNode>)
					.Select(n => n.Tag as Difference)
					.Where(d => d != null);
				foreach (var selectedItem in items)
				{
					string error = string.Empty;
					var cmd = _draggingTreeView == tvUnStaged ? "add " : "reset -- ";
					_helper.RunCommand(cmd + selectedItem.FileName, ref error);
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

		private void tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			viewExternalToolStripMenuItem_Click(sender, e);
		}

		private void compareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selectedNodes = tvLocalBranches.SelectedNodes.Union(tvRemoteBranches.SelectedNodes);
			var branch1 = selectedNodes.First().Tag as Branch;
			var branch2 = selectedNodes.Last().Tag as Branch;
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
	}
}
