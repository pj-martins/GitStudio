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
			bool error = false;
			_helper.RunCommand("checkout " + tvLocalBranches.SelectedNode.Tag.ToString(), ref error);
			refreshBranches();
		}

		private void mnuLocal_Opening(object sender, CancelEventArgs e)
		{
			// checkoutToolStripMenuItem.Enabled = lstLocalBranches.SelectedItems.Count == 1;
			var branch = tvLocalBranches.SelectedNode == null ? null : tvLocalBranches.SelectedNode.Tag as LocalBranch;
			pullToolStripMenuItem.Enabled = branch != null && branch.Behind > 0 && branch == _currentBranch;
			pushToolStripMenuItem.Enabled = branch != null;
			deleteToolStripMenuItem.Enabled = getCheckedNodes<LocalBranch>(tvLocalBranches.Nodes).Any();
		}

		private void fetchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool error = false;
			_helper.RunCommand("fetch " + tvRemoteBranches.SelectedNode.Text, ref error);
			if (error) return;
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
			var selected = getCheckedNodes<LocalBranch>(tvLocalBranches.Nodes);
			if (MessageBox.Show("Are you sure you want to delete " +
				string.Join("\r\n", selected.Select(s => s.ToString()).ToArray())
				+ "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				foreach (var s in selected)
				{
					bool error = false;
					_helper.RunCommand("branch -D " + s.BranchName, ref error);
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
			if (_previousDifferences != null)
			{
				if (diffs.All(d => _previousDifferences.Any(pd => pd.FileName == d.FileName && pd.DifferenceType == d.DifferenceType && pd.FileName == d.FileName))
				 && _previousDifferences.All(d => diffs.Any(pd => pd.FileName == d.FileName && pd.DifferenceType == d.DifferenceType && pd.FileName == d.FileName)))
					return;
			}

			_previousDifferences = diffs;
			if (diffs.Count < 1) return;

			var selectedDiff = tvDifferences.SelectedNode == null ? null : tvDifferences.SelectedNode.Tag as Difference;
			var selectedStaged = tvStaged.SelectedNode == null ? null : tvStaged.SelectedNode.Tag as Difference;

			tvDifferences.BeginUpdate();
			tvStaged.BeginUpdate();
			tvDifferences.Nodes.Clear();
			tvStaged.Nodes.Clear();
			List<TreeNode> expandedNodes = new List<TreeNode>();
			_lockCheck = true;
			foreach (var diff in diffs.OrderBy(d => d.FileName))
			{
				var tv = diff.IsStaged ? tvStaged : tvDifferences;
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
				tvDifferences.ExpandAll();
				tvStaged.ExpandAll();
			}
			else
			{
				foreach (var exp in expandedNodes)
				{
					exp.Expand();
				}
			}
			tvDifferences.EndUpdate();
			tvStaged.EndUpdate();
			btnCommit.Enabled = tvStaged.Nodes.Count > 0;
			_lockCheck = false;
		}

		private void viewExternalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvDifferences.Focused ? tvDifferences : tvStaged;
			var currFile = Path.Combine(Repository.LocalPath, (tv.SelectedNode.Tag as Difference).FileName);
			var tmpFile = Path.GetTempFileName();
			bool error = false;
			var oldContent = _helper.RunCommand("--no-pager show " + _currentBranch + ":\"" + (tv.SelectedNode.Tag as Difference).FileName + "\"", ref error);
			if (error) return;
			File.WriteAllLines(tmpFile, oldContent);
			Process.Start("WinMerge", currFile + " " + tmpFile);
		}

		private void pullToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool error = false;
			_helper.RunCommand("pull", ref error);
			if (error) return;
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

		private void btnStage_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getCheckedNodes<Difference>(tvDifferences.Nodes))
			{
				bool error = false;
				_helper.RunCommand("add " + selectedItem.FileName, ref error);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void btnUnStage_Click(object sender, EventArgs e)
		{
			foreach (var selectedItem in getCheckedNodes<Difference>(tvStaged.Nodes))
			{
				bool error = false;
				_helper.RunCommand("reset -- " + selectedItem.FileName, ref error);
			}
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to undo selected files?", "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			var tv = tvDifferences.Focused ? tvDifferences : tvStaged;
			foreach (var selectedItem in getCheckedNodes<Difference>(tv.Nodes))
			{
				bool error = false;
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

			var tv = tvDifferences.Focused ? tvDifferences : tvStaged;
			var selectedItems = getCheckedNodes<Difference>(tv.Nodes);
			foreach (var selectedItem in selectedItems)
			{
				bool error = false;
				if (tv == tvStaged)
					_helper.RunCommand("reset " + selectedItem.FileName, ref error);
			}
			File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), selectedItems.Select(i => i.FileName));
			txtDiffText.Text = string.Empty;
			timDiff_Tick(this, new EventArgs());
		}

		private void mnuDiffs_Opening(object sender, CancelEventArgs e)
		{
			var tv = tvDifferences.Focused ? tvDifferences : tvStaged;
			var selectedItems = getCheckedNodes<Difference>(tv.Nodes);
			ignorePathToolStripMenuItem.Enabled = selectedItems.Count() == 1 && selectedItems.First().FileName.Contains("/");
		}

		private void ignorePathToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvDifferences.Focused ? tvDifferences : tvStaged;
			var selectedItem = getCheckedNodes<Difference>(tv.Nodes).First();
			var frm = new frmIgnorePath();
			frm.FullPath = selectedItem.FileName;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				File.AppendAllLines(Path.Combine(Repository.LocalPath, ".gitignore"), new string[] { frm.IgnorePath });
				txtDiffText.Text = string.Empty;
				timDiff_Tick(this, new EventArgs());
			}
		}

		private void tv_AfterSelect(object sender, TreeViewEventArgs e)
		{
			bool error = false;
			if (e.Node.Tag == null)
			{
				txtDiffText.Text = string.Empty;
				return;
			}

			var diff = e.Node.Tag as Difference;
			var diffs = diff == null || diff.DifferenceType != DifferenceType.Modify ? new string[0] : _helper.RunCommand("--no-pager diff " + (diff.IsStaged ? "--cached " : "") + "\"" + diff.FileName + "\"", ref error);
			if (error) return;
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
			if (e.Node.Tag is Difference)
			{
				switch ((e.Node.Tag as Difference).DifferenceType)
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
			}

			TextRenderer.DrawText(e.Graphics, text, e.Node.NodeFont, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 10, e.Bounds.Height),
				e.Node.ForeColor, e.Node.BackColor, TextFormatFlags.GlyphOverhangPadding);
		}

		private List<TTagType> getCheckedNodes<TTagType>(TreeNodeCollection nodes)
		{
			List<TTagType> differences = new List<TTagType>();
			foreach (TreeNode node in nodes)
			{
				if (node.Checked && node.Tag is TTagType)
				{
					differences.Add((TTagType)node.Tag);
				}
				if (node.Nodes.Count > 0)
				{
					differences.AddRange(getCheckedNodes<TTagType>(node.Nodes));
				}
			}
			return differences;
		}

		private List<TreeNode> getLowLevelNodes(TreeNodeCollection nodes)
		{
			List<TreeNode> lowLevels = new List<TreeNode>();
			foreach (TreeNode node in nodes)
			{
				if (node.Tag != null)
				{
					lowLevels.Add(node);
				}
				if (node.Nodes.Count > 0)
				{
					lowLevels.AddRange(getLowLevelNodes(node.Nodes));
				}
			}
			return lowLevels;
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

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var tv = tvDifferences.Focused ? tvDifferences : tvStaged;
			foreach (var node in getLowLevelNodes(tv.Nodes))
			{
				node.Checked = true;
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
		}
	}
}
