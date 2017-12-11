using PaJaMa.WinControls.MWTreeView;
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
			deleteToolStripMenuItem.Visible = getSelectedNodeTags<LocalBranch>(tvLocalBranches, false).Any();
		}


		private void mnuRemote_Opening(object sender, CancelEventArgs e)
		{
			var branch = tvRemoteBranches.SelectedNode == null ? null : tvRemoteBranches.SelectedNode.Tag as RemoteBranch;
			deleteToolStripMenuItem.Visible = getSelectedNodeTags<RemoteBranch>(tvRemoteBranches, false).Any();
		}

		private void fetchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string error = string.Empty;
			_helper.RunCommand("fetch " + tvRemoteBranches.SelectedNode.Text, ref error);
			if (!string.IsNullOrEmpty(error)) return;
			refreshBranches();
		}

		private void tvLocalBranches_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
			font = new Font(font, e.Node.Tag != null && e.Node.Tag == _currentBranch ? FontStyle.Bold : FontStyle.Regular);

			TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, e.Node.ForeColor, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var selected = getSelectedNodeTags<LocalBranch>(tvLocalBranches, false);
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
			var selected = getSelectedNodeTags<RemoteBranch>(tvRemoteBranches, false);
			if (MessageBox.Show("Are you sure you want to delete " +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				+ "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				foreach (var s in selected)
				{
					string error = string.Empty;
					_helper.RunCommand("push origin --delete " + s.BranchName, ref error);
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
				foreach (var part in parts)
				{
					var nodeCollection = node == null ? tv.Nodes : node.Nodes;

					var foundNode = nodeCollection.OfType<TreeNode>().FirstOrDefault(n => n.Text == part);
					if (foundNode == null)
					{
						foundNode = nodeCollection.Add(part);
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
			if (tv.SelectedNode == null) return;
			var currFile = Path.Combine(Repository.LocalPath, (tv.SelectedNode.Tag as Difference).FileName);
			var tmpFile = Path.GetTempFileName();
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
			_helper.RunCommand("pull origin " + branch.TracksBranch.BranchName, ref error);
			if (!string.IsNullOrEmpty(error)) return;
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
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tv, true))
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
				var runningText = string.Empty;
				while (runningNode != null)
				{
					runningText = runningNode.Text + (string.IsNullOrEmpty(runningText) ? "" : "/") + runningText;
					runningNode = runningNode.Parent;
				}
				selectedItems.Add(runningText);
			}
			File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), selectedItems);
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void mnuDiffs_Opening(object sender, CancelEventArgs e)
		{
			var tv = tvUnStaged.Focused ? tvUnStaged : tvStaged;
			var selectedItems = getSelectedNodeTags<Difference>(tv, false);
			var diff = tv.SelectedNode == null ? null : tv.SelectedNode.Tag as Difference;
			resolveConflictToolStripMenuItem.Visible = diff != null && diff.IsConflict;
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
			if (!string.IsNullOrEmpty(error)) return;
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

		private void tv_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			var text = e.Node.Text;
			var font = e.Node.NodeFont ?? (sender as TreeView).Font;
			var color = e.Node.ForeColor;
			if (e.Node.Tag is Difference)
			{
				var diff = e.Node.Tag as Difference;
				switch (diff.DifferenceType)
				{
					case DifferenceType.Add:
						text = "A: " + text;
						break;
					case DifferenceType.Modify:
						text = "M: " + text;
						break;
					case DifferenceType.Delete:
						text = "D: " + text;
						break;
				}
				if (diff.IsConflict)
				{
					font = new Font(font, FontStyle.Bold);
					color = Color.Red;
				}
			}

			TextRenderer.DrawText(e.Graphics, text, font, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 10, e.Bounds.Height),
				color, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding);
		}

		private List<TTagType> getSelectedNodeTags<TTagType>(MWTreeView tv, bool andChildren)
		{
			List<TTagType> selected = new List<TTagType>();
			var nodes = tv.SelectedNodes.ToList();
			if (andChildren)
			{
				foreach (var n in tv.SelectedNodes)
				{
					nodes.AddRange(recursivelyGetChildren(n));
				}
				nodes = nodes.Distinct().ToList();
			}

			foreach (TreeNode node in nodes)
			{
				if (node.Tag is TTagType)
				{
					var tag = ((TTagType)node.Tag);
					if (!selected.Contains(tag))
						selected.Add(tag);
				}
			}
			return selected;
		}

		private List<TreeNode> recursivelyGetChildren(TreeNode parent)
		{
			List<TreeNode> nodes = new List<TreeNode>();
			foreach (TreeNode node in parent.Nodes)
			{
				nodes.Add(node);
				foreach (TreeNode child in node.Nodes)
				{
					nodes.AddRange(recursivelyGetChildren(child));
				}
			}
			return nodes;
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
				_helper.RunCommand("merge origin/" + branch.BranchName, ref error);
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
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvUnStaged, true))
			{
				string error = string.Empty;
				_helper.RunCommand("add " + selectedItem.FileName, ref error);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void unStageToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getSelectedNodeTags<Difference>(tvStaged, true))
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
			var differences = getSelectedNodeTags<Difference>(tv, false);
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
		private void tv_ItemDrag(object sender, ItemDragEventArgs e)
		{
			_draggingTreeView = sender;
			DoDragDrop(getSelectedNodeTags<Difference>(sender as MWTreeView, true), DragDropEffects.Move);
		}

		private void tv_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(List<Difference>).FullName) && sender != _draggingTreeView)
			{
				e.Effect = DragDropEffects.Move;
			}
		}

		private void tv_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(List<Difference>).FullName) && sender != _draggingTreeView)
			{
				var items = e.Data.GetData(typeof(List<Difference>).FullName) as List<Difference>;
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
	}
}
