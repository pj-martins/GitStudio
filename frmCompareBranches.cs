using PaJaMa.Common;
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
	public partial class frmCompareBranches : Form
	{
		public GitHelper Helper { get; set; }
		public Branch FromBranch { get; set; }
		public Branch ToBranch { get; set; }

		public frmCompareBranches()
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
			lblDirection.Text = ToBranch.BranchName + " -> " + FromBranch.BranchName;
			var diffs = Helper.RunCommand("--no-pager diff --name-status " + FromBranch.BranchName + " " + ToBranch.BranchName);
			gridMain.DataSource = diffs.Where(d => !string.IsNullOrWhiteSpace(d)).Select(d => d.Contains('\t')
			?
			new
			{
				File = d.Split('\t')[1],
				Action = Helpers.GetDifferenceType(d.Split('\t')[0])
			}
			:
			new
			{
				File = d,
				Action = DifferenceType.Unknown
			}).ToList();
		}

		private void btnSwitch_Click(object sender, EventArgs e)
		{
			var fromBranch = FromBranch;
			FromBranch = ToBranch;
			ToBranch = fromBranch;
			refreshDifferences();
		}

		private void gridMain_DoubleClick(object sender, EventArgs e)
		{
			externalCompareToolStripMenuItem_Click(sender, e);
		}

		private string getEscapedFile(string fileName)
		{
			var outFile = fileName;
            if (outFile.Contains(" "))
            {
                outFile = "\"" + outFile + "\"";
            }
			return outFile;
        }

		private void gridMain_SelectionChanged(object sender, EventArgs e)
		{
			var selectedRow = gridMain.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedRow == null)
			{
				ucDifferences.ClearDifferences();
				return;
			}

            var diffs = Helper.RunCommand("--no-pager diff " + FromBranch.BranchName + " " + ToBranch.BranchName + " -- " + getEscapedFile(selectedRow.Cells["File"].Value.ToString()));
			ucDifferences.SetDifferences(diffs, (DifferenceType)selectedRow.Cells["Action"].Value);
		}

		private void externalCompareToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var settings = Common.SettingsHelper.GetUserSettings<GitUserSettings>();
			if (string.IsNullOrEmpty(settings.ExternalDiffApplication))
			{
				MessageBox.Show("No external diff application has been setup!");
				return;
			}
			foreach (var selectedRow in gridMain.SelectedRows.OfType<DataGridViewRow>())
			{
				if (selectedRow.Cells["Action"].Value.ToString() != "Modify") continue;
				bool hasError = false;
				var content1 = Helper.RunCommand("--no-pager show " + FromBranch.BranchName + ":" + getEscapedFile(selectedRow.Cells["File"].Value.ToString()), true, false, ref hasError);
				var content2 = Helper.RunCommand("--no-pager show " + ToBranch.BranchName + ":" + getEscapedFile(selectedRow.Cells["File"].Value.ToString()), true, false, ref hasError);

				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);

				var tmpFile1 = Path.Combine(tmpDir, FromBranch.BranchName.Replace("/", "_").FileSafeName() + "_" + 
					Path.GetFileName(selectedRow.Cells["File"].Value.ToString()).Replace(".", "_") + "_" +
					Guid.NewGuid() + ".tmp");
				var tmpFile2 = Path.Combine(tmpDir, ToBranch.BranchName.Replace("/", "_").FileSafeName() + "_" +
					Path.GetFileName(selectedRow.Cells["File"].Value.ToString()).Replace(".", "_") + "_" +
					Guid.NewGuid() + ".tmp");
				File.WriteAllLines(tmpFile1, content1);
				File.WriteAllLines(tmpFile2, content2);
				Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, tmpFile2, tmpFile1));
			}
		}
	}
}
