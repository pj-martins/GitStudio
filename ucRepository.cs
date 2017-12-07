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
		private string _currentBranch;
		public GitRepository Repository { get; set; }

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
				refreshBranches(false);
				lstDifferences.Items.Clear();
				timDiff_Tick(this, new EventArgs());
				timDiff.Enabled = true;
				_inited = true;
			}
		}

		private void refreshBranches(bool remote)
		{
			TreeView tv = remote ? tvRemoteBranches : tvLocalBranches;
			var branches = GitHelper.RunCommand("branch " + (remote ? "-r" : "-l"), Repository.LocalPath);
			tv.Nodes.Clear();
			foreach (var b in branches)
			{
				if (b.Contains("origin/HEAD ->"))
					continue;
				var branch = b;
				if (branch.StartsWith("*"))
				{
					branch = branch.Substring(1);
					_currentBranch = branch.Trim();
					
				}
				var parts = branch.Trim().Split('/');
				TreeNode node = null;
				foreach (var part in parts)
				{
					var nodeCollection = node == null ? tv.Nodes : node.Nodes;

					var foundNode = nodeCollection.OfType<TreeNode>().FirstOrDefault(n => n.Text == part);
					if (foundNode == null)
						foundNode = nodeCollection.Add(part);
					node = foundNode;
				}
				node.Tag = branch.Trim();
				tv.ExpandAll();
			}
		}

		private void branchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmCheckout();
			frm.RemoteBranch = tvRemoteBranches.SelectedNode.Tag.ToString();
			frm.LocalBranch = tvRemoteBranches.SelectedNode.Tag.ToString();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				GitHelper.RunCommand("checkout -b " + frm.LocalBranch
					+ (frm.TrackRemote ? " --track " : " --no-track ") + frm.RemoteBranch, Repository.LocalPath);

				refreshBranches(false);
			}
		}

		private void checkoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GitHelper.RunCommand("checkout " + tvLocalBranches.SelectedNode.Tag.ToString(), Repository.LocalPath);
			_currentBranch = tvLocalBranches.SelectedNode.Tag.ToString();
			tvLocalBranches.Invalidate();
		}

		private void mnuLocal_Opening(object sender, CancelEventArgs e)
		{
			// checkoutToolStripMenuItem.Enabled = lstLocalBranches.SelectedItems.Count == 1;
		}

		private void fetchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			GitHelper.RunCommand("fetch " + tvRemoteBranches.SelectedNode.Text);
			refreshBranches(true);
		}

		private void tvLocalBranches_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
			font = new Font(font, e.Node.Tag != null && e.Node.Tag.ToString() == _currentBranch ? FontStyle.Bold : FontStyle.Regular);

			TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, e.Node.ForeColor, e.Node.ForeColor, TextFormatFlags.GlyphOverhangPadding);
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to delete " + tvLocalBranches.SelectedNode.Tag.ToString() + "?", "Warning!",
				MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				GitHelper.RunCommand("branch -D " + tvLocalBranches.SelectedNode.Tag.ToString(), Repository.LocalPath);
				refreshBranches(false);
			}
		}

		private void ucRepository_Load(object sender, EventArgs e)
		{
		}

		private void timDiff_Tick(object sender, EventArgs e)
		{
			if (!this.Parent.Visible) 
				return;
			var diffs = GitHelper.RunCommand("diff --name-only", Repository.LocalPath);
			foreach (var diff in diffs)
			{
				if (!lstDifferences.Items.Contains(diff))
					lstDifferences.Items.Add(diff);
			}
		}

		private void lstDifferences_SelectedIndexChanged(object sender, EventArgs e)
		{
			var diffs = GitHelper.RunCommand("--no-pager diff \"" + lstDifferences.SelectedItem.ToString() + "\"", Repository.LocalPath);
			txtDiffText.Text = string.Join("\r\n", diffs);
		}

		private void viewExternalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var currFile = Path.Combine(Repository.LocalPath, lstDifferences.SelectedItem.ToString());
			var tmpFile = Path.GetTempFileName();
			var oldContent = GitHelper.RunCommand("--no-pager show " + _currentBranch + ":\"" + lstDifferences.SelectedItem.ToString() + "\"", Repository.LocalPath);
			File.WriteAllLines(tmpFile, oldContent);
			Process.Start("WinMerge", currFile + " " + tmpFile);
		}
	}
}
