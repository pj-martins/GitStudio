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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class frmStashes : Form
	{
		public GitHelper Helper { get; set; }
		public GitRepository Repository { get; set; }
		public event EventHandler BranchCreated;

		public frmStashes()
		{
			InitializeComponent();
		}

		private void frmStashes_Load(object sender, EventArgs e)
		{
			PaJaMa.WinControls.FormSettings.LoadSettings(this);
			refreshStashes();
		}

		private void frmStashes_FormClosing(object sender, FormClosingEventArgs e)
		{
			PaJaMa.WinControls.FormSettings.SaveSettings(this);
		}

		private void refreshStashes()
		{
			string[] items = null;
			var worker = new BackgroundWorker();
			worker.DoWork += (object sender2, DoWorkEventArgs e2) =>
			{
				items = Helper.RunCommand("stash list", false);
			};
			WinStatusBox.ShowProgress(worker, "Stashing", progressBarStyle: ProgressBarStyle.Marquee);
			var stashes = new List<Stash>();
			foreach (var item in items.Where(i => !string.IsNullOrWhiteSpace(i)))
			{
				var stash = new Stash();
				var parts = item.Split(':');
				stash.StashID = parts[0];
				stash.Branch = parts[1];
				stash.Comment = string.Join(":", parts.Skip(2));
				stashes.Add(stash);
			}
			gridStashes.DataSource = stashes;
		}

		private void gridStashes_SelectionChanged(object sender, EventArgs e)
		{
			var selectedRows = gridStashes.SelectedRows.OfType<DataGridViewRow>();
			if (selectedRows.Count() < 1)
			{
				gridDetails.DataSource = null;
				ucDifferences.ClearDifferences();
				return;
			}

			var stashID = (selectedRows.First().DataBoundItem as Stash).StashID;

			var diffs = Helper.RunCommand("stash show " + stashID + " --name-status", false);
			var details = new Dictionary<string, DifferenceType>();
			foreach (var diff in diffs)
			{
				var diffparts = diff.Split('\t');
				if (diffparts.Length > 1)
				{
					details.Add(diffparts[1], Helpers.GetDifferenceType(diffparts[0]));
				}
			}
			gridDetails.DataSource = details.Select(d => new
			{
				File = d.Key,
				Action = d.Value,
				StashID = stashID
			}).ToList();
		}

		private void gridDetails_SelectionChanged(object sender, EventArgs e)
		{
			var selectedRow = gridDetails.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedRow == null)
			{
				ucDifferences.ClearDifferences();
				return;
			}

			var selectedStash = gridStashes.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedStash == null)
			{
				ucDifferences.ClearDifferences();
				return;
			}

			var diffs = Helper.RunCommand("diff " + (selectedStash.DataBoundItem as Stash).StashID + " -- " + selectedRow.Cells["File"].Value.ToString());
			ucDifferences.SetDifferences(diffs, (DifferenceType)selectedRow.Cells["Action"].Value);
		}

		private void gridDetails_DoubleClick(object sender, EventArgs e)
		{
			externalCompareToolStripMenuItem_Click(sender, e);
		}

		private void mnuStashes_Opening(object sender, CancelEventArgs e)
		{
			applyToolStripMenuItem.Enabled = createBranchToolStripMenuItem.Enabled = popToolStripMenuItem.Enabled = gridStashes.SelectedRows.Count == 1;
		}

		private void externalCompareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var settings = Common.SettingsHelper.GetUserSettings<GitUserSettings>();
			if (string.IsNullOrEmpty(settings.ExternalDiffApplication))
			{
				MessageBox.Show("No external diff application has been setup!");
				return;
			}

			foreach (var detailRow in gridDetails.SelectedRows.OfType<DataGridViewRow>())
			{
				var action = detailRow.Cells["Action"].Value.ToString();
				if (action != "Modify") return;
				var fileName = detailRow.Cells["File"].Value.ToString();

				var stashID = detailRow.Cells["StashID"].Value.ToString();
				var currFile = Path.Combine(Repository.LocalPath, fileName);
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
				var tmpFile = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
				bool error = false;
				var oldContent = Helper.RunCommand("show " + stashID + ":\"" + fileName + "\"", false, ref error);
				if (error) return;
				File.WriteAllLines(tmpFile, oldContent);
				Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, currFile, tmpFile));
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var stashRows = gridStashes.SelectedRows.OfType<DataGridViewRow>();
			if (stashRows.Count() > 0)
			{
				if (MessageBox.Show("Are you sure you want to delete the following stashes?\r\n" +
					string.Join("\r\n", stashRows.Select(r => r.Cells["StashID"].Value.ToString())), "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
					return;
			}
			foreach (var stashRow in stashRows)
			{
				var stashID = stashRow.Cells["StashID"].Value.ToString();
				Helper.RunCommand("stash drop " + stashID, false);
			}
			refreshStashes();
		}

		private void applyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var stashRow = gridStashes.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (stashRow == null) return;
			if (MessageBox.Show("Are you sure you want to apply the following stash?\r\n" +
				stashRow.Cells["StashID"].Value.ToString(), "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			Helper.RunCommand("stash apply " + stashRow.Cells["StashID"].Value.ToString(), true);
			refreshStashes();
		}

		private void popToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var stashRow = gridStashes.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (stashRow == null) return;
			if (MessageBox.Show("Are you sure you want to pop the following stash?\r\n" +
				stashRow.Cells["StashID"].Value.ToString(), "Warning!", MessageBoxButtons.YesNo) != DialogResult.Yes)
				return;

			bool hasError = false;
			Helper.RunCommand("stash pop " + stashRow.Cells["StashID"].Value.ToString(), true, ref hasError);
			refreshStashes();
		}

		private void createBranchToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var stashRow = gridStashes.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (stashRow == null) return;

			var result = InputBox.Show("Branch Name");
			if (result.Result != DialogResult.OK) return;

			Helper.RunCommand("stash branch " + result.Text + " " + stashRow.Cells["StashID"].Value.ToString(), true);

			BranchCreated?.Invoke(this, new EventArgs());
			refreshStashes();
		}
	}

	public class Stash
	{
		public string StashID { get; set; }
		public string Branch { get; set; }
		public string Comment { get; set; }
	}
}
