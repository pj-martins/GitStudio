using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class frmFileHistory : Form
	{
		public GitHelper Helper { get; set; }

		public frmFileHistory()
		{
			InitializeComponent();
		}

		private void frmFileHistory_Load(object sender, EventArgs e)
		{
			PaJaMa.Common.FormSettings.LoadSettings(this);
			refreshFiles(new DirectoryInfo(Helper.WorkingDirectory), tvFiles.Nodes);
		}

		private void frmCompareBranches_FormClosing(object sender, FormClosingEventArgs e)
		{
			PaJaMa.Common.FormSettings.SaveSettings(this);
		}

		private void refreshFiles(DirectoryInfo dinf, TreeNodeCollection nodes)
		{
			foreach (var dinf2 in dinf.GetDirectories())
			{
				var dirNode = nodes.Add(dinf2.Name);
				dirNode.Tag = dinf2;
				dirNode.Nodes.Add("__");
			}

			foreach (var finf in dinf.GetFiles())
			{
				var fileNode = nodes.Add(finf.Name);
				fileNode.Tag = finf.FullName.Replace(Helper.WorkingDirectory + "\\", "");
			}
		}

		private void selectFile(string fileName)
		{
			var logs = Helper.RunCommand("--no-pager log " + fileName);
			var commits = new List<Commit>();
			//commits.Add(new Commit()
			//{
			//	CommitID = "HEAD",
			//	Author = "HEAD",
			//	Index = 1,
			//});

			// int index = 2;
			int index = 1;
			Commit current = null;
			foreach (var log in logs)
			{
				if (log.StartsWith("commit "))
				{
					if (current != null) current.Comment = current.Comment.Trim();
					current = new Commit();
					current.Index = index++;
					commits.Add(current);
					current.CommitID = log.Substring(7);
				}
				else if (log.StartsWith("Author:"))
					current.Author = log.Substring(7);
				else if (log.StartsWith("Date:"))
					current.Date = log.Substring(5);
				else if (log.StartsWith("    "))
					current.Comment += log.Trim() + "\r\n";
			}
			_refreshing = true;
			gridCommits.DataSource = commits;
			_refreshing = false;
			gridCommits_SelectionChanged(gridCommits, new EventArgs());
		}

		private Tuple<Commit, Commit> getCommitsToCompare()
		{
			var selectedRows = gridCommits.SelectedRows.OfType<DataGridViewRow>()
					.OrderBy(c => (c.DataBoundItem as Commit).Index);
			if (selectedRows.Count() < 1)
			{
				return null;
			}

			var toRow = selectedRows.First();
			DataGridViewRow fromRow = null;
			if (selectedRows.Count() > 1)
			{
				fromRow = selectedRows.Last();
			}
			else
			{
				var rowIndex = gridCommits.Rows.IndexOf(toRow);
				if (rowIndex + 1 < gridCommits.Rows.Count)
					fromRow = gridCommits.Rows[rowIndex + 1];
			}

			return new Tuple<Commit, Commit>(fromRow == null ? null : fromRow.DataBoundItem as Commit, toRow.DataBoundItem as Commit);
		}

		private bool _refreshing = false;
		private void gridCommits_SelectionChanged(object sender, EventArgs e)
		{
			if (_refreshing) return;

			var commitsToCompare = getCommitsToCompare();
			if (commitsToCompare == null)
			{
				txtDifferences.Text = string.Empty;
				return;
			}

			var selectedFile = tvFiles.SelectedNode == null ? null : tvFiles.SelectedNode.Tag;
			if (selectedFile == null)
			{
				txtDifferences.Text = string.Empty;
				return;
			}

			var diffs = Helper.RunCommand("--no-pager diff " +
				(commitsToCompare.Item1 != null ? commitsToCompare.Item1.CommitID + " " : "") +
				commitsToCompare.Item2.CommitID + " -- " + selectedFile.ToString());
			txtDifferences.Text = string.Join("\r\n", diffs);
		}

		private void gridDetails_DoubleClick(object sender, EventArgs e)
		{
			externalCompareToolStripMenuItem_Click(sender, e);
		}

		private void externalCompareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var settings = Common.SettingsHelper.GetUserSettings<GitUserSettings>();
			if (string.IsNullOrEmpty(settings.ExternalDiffApplication))
			{
				MessageBox.Show("No external diff application has been setup!");
				return;
			}

			var selectedFile = tvFiles.SelectedNode == null || tvFiles.SelectedNode.Tag == null ? null : tvFiles.SelectedNode.Tag.ToString().Replace("\\", "/");
			var commitsToCompare = getCommitsToCompare();
			if (commitsToCompare == null || commitsToCompare.Item1 == null)
			{
				txtDifferences.Text = string.Empty;
				return;
			}

			var content1 = Helper.RunCommand("--no-pager show " + commitsToCompare.Item2.CommitID + ":" + selectedFile);
			var content2 = Helper.RunCommand("--no-pager show " + commitsToCompare.Item1.CommitID + ":" + selectedFile);

			var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
			if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
			var tmpFile1 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
			var tmpFile2 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
			File.WriteAllLines(tmpFile1, content1);
			File.WriteAllLines(tmpFile2, content2);
			Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, tmpFile1, tmpFile2));
		}

		private void gridCommits_DoubleClick(object sender, EventArgs e)
		{
			externalCompareToolStripMenuItem_Click(sender, e);
		}

		private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Tag != null)
				selectFile(e.Node.Tag.ToString());
		}

		private void tvFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "__")
			{
				e.Node.Nodes.Clear();
				refreshFiles(e.Node.Tag as DirectoryInfo, e.Node.Nodes);
			}
		}
	}
}
