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
	public partial class frmCommitHistory : Form
	{
		public GitHelper Helper { get; set; }
		public Branch Branch { get; set; }

		public frmCommitHistory()
		{
			InitializeComponent();
		}

		private void frmCompareBranches_Load(object sender, EventArgs e)
		{
			PaJaMa.Common.FormSettings.LoadSettings(this);
			refreshDifferences();
		}

		private void frmCompareBranches_FormClosing(object sender, FormClosingEventArgs e)
		{
			PaJaMa.Common.FormSettings.SaveSettings(this);
		}

		private void refreshDifferences()
		{
			var logs = Helper.RunCommand("--no-pager log " + Branch.BranchName);
			var commits = new List<Commit>();
			Commit current = null;
			foreach (var log in logs)
			{
				if (log.StartsWith("commit "))
				{
					if (current != null) current.Comment = current.Comment.Trim();
					current = new Commit();
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
			gridCommits.DataSource = commits;
		}

		private void gridCommits_SelectionChanged(object sender, EventArgs e)
		{
			var selectedRows = gridCommits.SelectedRows.OfType<DataGridViewRow>();
			if (selectedRows.Count() < 1)
			{
				gridDetails.DataSource = null;
				txtDifferences.Text = string.Empty;
				return;
			}

			var diffs = new string[0];
			if (selectedRows.Count() == 2)
			{
				diffs = Helper.RunCommand("--no-pager diff --name-status " + (selectedRows.Last().DataBoundItem as Commit).CommitID
					+ " " + (selectedRows.First().DataBoundItem as Commit).CommitID);
			}
			else
			{
				diffs = Helper.RunCommand("--no-pager show --name-status -r " + (selectedRows.First().DataBoundItem as Commit).CommitID);
			}
			var details = new Dictionary<string, DifferenceType>();
			foreach (var diff in diffs)
			{
				var diffparts = diff.Split('\t');
				if (diffparts.Length > 1)
				{
					if (diffparts[0] == "M")
					{
						details.Add(diffparts[1], DifferenceType.Modify);
					}
					else if (diffparts[0] == "A")
					{
						details.Add(diffparts[1], DifferenceType.Add);
					}
					else if (diffparts[0] == "D")
					{
						details.Add(diffparts[1], DifferenceType.Delete);
					}
				}
			}
			gridDetails.DataSource = details.Select(d => new
			{
				File = d.Key,
				Action = d.Value.ToString()
			}).ToList();
		}

		private void gridDetails_SelectionChanged(object sender, EventArgs e)
		{
			var selectedRows = gridCommits.SelectedRows.OfType<DataGridViewRow>();
			if (selectedRows.Count() < 1)
			{
				txtDifferences.Text = string.Empty;
				return;
			}

			var selectedRow = gridDetails.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedRow == null)
			{
				txtDifferences.Text = string.Empty;
				return;
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
				if (rowIndex + 1 >= gridCommits.Rows.Count) return;
				fromRow = gridCommits.Rows[rowIndex + 1];
			}

			var diffs = Helper.RunCommand("--no-pager diff " +
	(selectedRows.Count() == 2 ? (toRow.DataBoundItem as Commit).CommitID + " " : "") +
		(fromRow.DataBoundItem as Commit).CommitID + " -- " + selectedRow.Cells["File"].Value.ToString());
			txtDifferences.Text = string.Join("\r\n", diffs);
		}

		private void gridDetails_DoubleClick(object sender, EventArgs e)
		{
			externalCompareToolStripMenuItem_Click(sender, e);
		}

		private void externalCompareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (var selectedRow in gridDetails.SelectedRows.OfType<DataGridViewRow>())
			{
				if (selectedRow.Cells["Action"].Value.ToString() != "Modify") continue;
				var selectedRows = gridCommits.SelectedRows.OfType<DataGridViewRow>();
				if (selectedRows.Count() < 1)
				{
					txtDifferences.Text = string.Empty;
					return;
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
					if (rowIndex + 1 >= gridCommits.Rows.Count) return;
					fromRow = gridCommits.Rows[rowIndex + 1];
				}

				var content1 = Helper.RunCommand("--no-pager show " + (fromRow.DataBoundItem as Commit).CommitID + ":" + selectedRow.Cells["File"].Value.ToString());
				var content2 = Helper.RunCommand("--no-pager show " + (toRow.DataBoundItem as Commit).CommitID + ":" + selectedRow.Cells["File"].Value.ToString());

				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
				var tmpFile1 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				var tmpFile2 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				File.WriteAllLines(tmpFile1, content1);
				File.WriteAllLines(tmpFile2, content2);
				Process.Start("WinMerge", tmpFile1 + " " + tmpFile2);
			}
		}
	}

	public class Commit
	{
		[Browsable(false)]
		public string CommitID { get; set; }
		public string Author { get; set; }
		public string Date { get; set; }
		public string Comment { get; set; }

		public Commit()
		{
			Comment = "";
		}
	}
}
