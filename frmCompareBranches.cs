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
			PaJaMa.Common.FormSettings.LoadSettings(this);
			refreshDifferences();
		}

		private void frmCompareBranches_FormClosing(object sender, FormClosingEventArgs e)
		{
			PaJaMa.Common.FormSettings.SaveSettings(this);
		}

		private void refreshDifferences()
		{
			lblDirection.Text = FromBranch.BranchName + " -> " + ToBranch.BranchName;
			string error = string.Empty;
			var diffs = Helper.RunCommand("--no-pager diff --name-status " + FromBranch.BranchName + " " + ToBranch.BranchName, ref error);
			gridMain.DataSource = diffs.Select(d => new
			{
				File = d.Split('\t')[1],
				Action = d.Split('\t')[0] == "A" ? "Add" : (d.Split('\t')[0] == "M" ? "Modify" : "Delete")
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
			btnCompare_Click(sender, e);
		}

		private void btnCompare_Click(object sender, EventArgs e)
		{
			var selectedRow = gridMain.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			string error = string.Empty;
			var content1 = Helper.RunCommand("--no-pager show " + FromBranch.BranchName + ":" + selectedRow.Cells["File"].Value.ToString(), ref error);
			var content2 = Helper.RunCommand("--no-pager show " + ToBranch.BranchName + ":" + selectedRow.Cells["File"].Value.ToString(), ref error);
			
			var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
			if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
			var tmpFile1 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
			var tmpFile2 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
			File.WriteAllLines(tmpFile1, content1);
			File.WriteAllLines(tmpFile2, content2);
			Process.Start("WinMerge", tmpFile1 + " " + tmpFile2);
		}

		private void gridMain_SelectionChanged(object sender, EventArgs e)
		{
			var selectedRow = gridMain.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
			if (selectedRow == null)
			{
				btnCompare.Enabled = false;
				txtDifferences.Text = string.Empty;
				return;
			}

			btnCompare.Enabled = selectedRow.Cells["Action"].Value.ToString() == "Modify";
			string error = string.Empty;
			var diffs = Helper.RunCommand("--no-pager diff " + FromBranch.BranchName + " " + ToBranch.BranchName + " -- " + selectedRow.Cells["File"].Value.ToString(), ref error);
			txtDifferences.Text = string.Join("\r\n", diffs);
		}
	}
}
