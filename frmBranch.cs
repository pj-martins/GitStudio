using PaJaMa.WinControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class frmBranch : Form
	{
		private Branch _branchFrom;
		public Branch BranchFrom
		{
			get { return _branchFrom; }
			set
			{
				_branchFrom = value;
				txtFrom.Text = value.BranchName;
				txtTo.Text = value.BranchName;
				if (txtTo.Text.StartsWith("origin/"))
					txtTo.Text = txtTo.Text.Substring(7);
				chkTrack.Enabled = value is RemoteBranch;
				if (!chkTrack.Enabled) chkTrack.Checked = false;
			}
		}

		public GitRepository Repository { get; set; }

		public frmBranch()
		{
			InitializeComponent();
		}

		private void btnBranch_Click(object sender, EventArgs e)
		{
			string[] lines = null;
			if ((BranchFrom is LocalBranch) && (BranchFrom as LocalBranch).TracksBranch != null && txtFrom.Text == txtTo.Text)
				lines = new GitHelper(Repository.LocalPath).RunCommand("branch --unset-upstream", true);
			else
				lines = new GitHelper(Repository.LocalPath).RunCommand((chkCheckout.Checked ? "checkout -b " : "branch ") + txtTo.Text
					+ (chkTrack.Checked ? " --track " : " --no-track ") + txtFrom.Text, true);
			//if (lines.Any(l => !l.StartsWith("Switched to")))
			//{
			if (lines.Any()) ScrollableMessageBox.Show(lines);
			//	return;
			//}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void txtTo_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				btnBranch_Click(sender, e);
		}
	}
}
