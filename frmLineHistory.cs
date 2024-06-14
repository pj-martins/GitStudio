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
using static System.Windows.Forms.LinkLabel;

namespace PaJaMa.GitStudio
{
	public partial class frmLineHistory : Form
	{
		public GitHelper Helper { get; set; }
		public string SelectedFile { get; set; }

		public frmLineHistory()
		{
			InitializeComponent();
		}

		private void frmLineHistory_Load(object sender, EventArgs e)
		{
			PaJaMa.WinControls.FormSettings.LoadSettings(this);
			if (Helper.SSHConnection != null)
			{
				refreshSSHLines(Helper.SSHConnection.Path);
			}
			else
			{
				refreshLines(new DirectoryInfo(Helper.WorkingDirectory));
			}
		}

		private void frmCompareBranches_FormClosing(object sender, FormClosingEventArgs e)
		{
			PaJaMa.WinControls.FormSettings.SaveSettings(this);
		}

		private void refreshLines(DirectoryInfo dinf)
		{
			var lines = File.ReadAllLines(Path.Combine(dinf.FullName, SelectedFile));
			var ds = new List<Line>();
			for (int i = 0; i < lines.Length; i++)
			{
				ds.Add(new GitStudio.Line() { Number = i + 1, Text = lines[i] });
			}
			gridLines.DataSource = ds;
		}

		private void refreshSSHLines(string path)
		{
			var lines = SSHHelper.RunCommandAsLines(Helper.SSHConnection, $"cat {SelectedFile.Replace("\"", "")}", true);
			var ds = new List<Line>();
			for (int i = 0; i < lines.Count; i++)
			{
				ds.Add(new GitStudio.Line() { Number = i + 1, Text = lines[i] });
			}
			gridLines.DataSource = ds;
		}

		private void selectLines()
		{
			int start = 0;
			int stop = 0;
			for (int i = 1; i <= gridLines.Rows.Count; i++)
			{
				var row = gridLines.Rows[i - 1];
				if (row.Selected)
				{
					if (start == 0 || i < start)
						start = i;
					if (stop == 0 || i > stop)
						stop = i;
				}
			}
			var logs = Helper.RunCommand($"--no-pager log -L {start},{stop}:{SelectedFile.Replace("\\", "/")}");
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
			var runningLines = new List<string>();
			foreach (var log in logs)
			{
				if (log.StartsWith("commit "))
				{
					if (current != null)
					{
						current.Comment = current.Comment.Trim();
						current.DiffLines = runningLines;
						runningLines.Clear();
					}

					current = new Commit();
					current.Index = index++;
					commits.Add(current);
					current.CommitID = log.Substring(7);
				}
				else if (log.StartsWith("Author:") && string.IsNullOrEmpty(current.Author))
					current.Author = log.Substring(7);
				else if (log.StartsWith("Date:") && string.IsNullOrEmpty(current.Date))
					current.Date = log.Substring(5);
				else if (log.StartsWith("    ") && string.IsNullOrEmpty(current.Comment))
					current.Comment += log.Trim() + "\r\n";
				runningLines.Add(log.Trim());
			}
			if (current != null)
			{
				current.DiffLines = runningLines;
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
				ucDifferences.ClearDifferences();
				return;
			}

			ucDifferences.SetDifferences(commitsToCompare.Item2.DiffLines.ToArray(), DifferenceType.Modify);
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

			var commitsToCompare = getCommitsToCompare();
			if (commitsToCompare == null || commitsToCompare.Item1 == null)
			{
				ucDifferences.ClearDifferences();
				return;
			}

			var content1 = Helper.RunCommand("--no-pager show " + commitsToCompare.Item2.CommitID.Split(' ').First() + ":\"" + SelectedFile.Replace("\\", "/") + "\"");
			var content2 = Helper.RunCommand("--no-pager show " + commitsToCompare.Item1.CommitID.Split(' ').First() + ":\"" + SelectedFile.Replace("\\", "/") + "\"");

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

		private void gridLines_SelectionChanged(object sender, EventArgs e)
		{
			selectLines();
		}
	}
}
