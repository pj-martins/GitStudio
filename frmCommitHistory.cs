﻿using System;
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
	public partial class frmCommitHistory : Form
	{
		public GitHelper Helper { get; set; }
		public Branch Branch { get; set; }
		public string FileName { get; set; }
		public event EventHandler BranchCreated;

		public frmCommitHistory()
		{
			InitializeComponent();
		}

		private void frmCompareBranches_Load(object sender, EventArgs e)
		{
			PaJaMa.WinControls.FormSettings.LoadSettings(this);
			refreshDifferences();
		}

		private void frmCompareBranches_FormClosing(object sender, FormClosingEventArgs e)
		{
			PaJaMa.WinControls.FormSettings.SaveSettings(this);
		}

		private void refreshDifferences()
		{
			splitContainer2.Panel2Collapsed = !string.IsNullOrEmpty(FileName);
			var logs = Helper.RunCommand("log " + (string.IsNullOrEmpty(FileName) ? Branch.BranchName : " -- " + FileName));
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

		private Tuple<Commit, Commit> getCommitsToCompare(bool includeNext)
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
			else if (includeNext)
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

			var commitsToCompare = getCommitsToCompare(false);
			if (commitsToCompare == null)
			{
				gridDetails.DataSource = null;
				ucDifferences.ClearDifferences();
				return;
			}

			var diffs = new string[0];

			if (commitsToCompare.Item1 != null)
			{
				lblCommits.Text = commitsToCompare.Item1.CommitID + " - " + commitsToCompare.Item2.CommitID;
				diffs = Helper.RunCommand("diff --name-status " + commitsToCompare.Item1.CommitID.Split(' ').First()
					+ " " + commitsToCompare.Item2.CommitID.Split(' ').First(), false);
			}
			else
			{
				lblCommits.Text = commitsToCompare.Item2.CommitID;
				diffs = Helper.RunCommand("show --name-status -r " + commitsToCompare.Item2.CommitID.Split(' ').First());
			}
			var details = new Dictionary<string, DifferenceType>();
			foreach (var diff in diffs)
			{
				var diffparts = diff.Split('\t');
				if (diffparts.Length > 1)
				{
					if (!string.IsNullOrEmpty(FileName) && diffparts[1] != FileName) continue;
					details.Add(diffparts[1], Helpers.GetDifferenceType(diffparts[0]));
				}
			}
			gridDetails.DataSource = details.Select(d => new
			{
				File = d.Key,
				Action = d.Value
			}).ToList();
		}

		private void gridDetails_SelectionChanged(object sender, EventArgs e)
		{
			var commitsToCompare = getCommitsToCompare(true);
			if (commitsToCompare == null)
			{
				ucDifferences.ClearDifferences();
				return;
			}

			var selectedRow = gridDetails.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedRow == null)
			{
				ucDifferences.ClearDifferences();
				return;
			}

			var diffs = Helper.RunCommand("diff " +
				(commitsToCompare.Item1 != null ? commitsToCompare.Item1.CommitID.Split(' ').First() + " " : "") +
				commitsToCompare.Item2.CommitID.Split(' ').First() + " -- " + selectedRow.Cells["File"].Value.ToString());
			ucDifferences.SetDifferences(diffs, (DifferenceType)selectedRow.Cells["Action"].Value);
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
			foreach (var selectedRow in gridDetails.SelectedRows.OfType<DataGridViewRow>())
			{
				if (selectedRow.Cells["Action"].Value.ToString() != "Modify") continue;

				var commitsToCompare = getCommitsToCompare(true);
				if (commitsToCompare == null || commitsToCompare.Item1 == null)
				{
					ucDifferences.ClearDifferences();
					return;
				}

				bool hasError = false;
                var content1 = Helper.RunCommand("show " + commitsToCompare.Item2.CommitID.Split(' ').First() + ":\"" + selectedRow.Cells["File"].Value.ToString() + "\"", true, false, ref hasError);
				var content2 = Helper.RunCommand("show " + commitsToCompare.Item1.CommitID.Split(' ').First() + ":\"" + selectedRow.Cells["File"].Value.ToString() + "\"", true, false, ref hasError);

				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
				var tmpFile1 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				var tmpFile2 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				File.WriteAllLines(tmpFile1, content1);
				File.WriteAllLines(tmpFile2, content2);
				Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, tmpFile1, tmpFile2));
			}
		}

		private void gridCommits_DoubleClick(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(FileName))
				externalCompareToolStripMenuItem_Click(sender, e);
		}

		private void getToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var result = WinControls.InputBox.Show("Enter branch name");
			if (result.Result == DialogResult.OK)
			{
				var commit = gridCommits.SelectedRows[0].DataBoundItem as Commit;
				Helper.RunCommand(new string[] {
				"checkout " + commit.CommitID,
				"checkout -b " + result.Text
				}, true);
				BranchCreated?.Invoke(this, new EventArgs());
			}
		}

		private void mnuCommits_Opening(object sender, CancelEventArgs e)
		{
			getToolStripMenuItem.Enabled = gridCommits.SelectedRows.Count == 1;
		}
	}
}
