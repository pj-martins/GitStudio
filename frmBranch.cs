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
			if ((BranchFrom is LocalBranch) && (BranchFrom as LocalBranch).TracksBranch != null && txtFrom.Text == txtTo.Text)
				new GitHelper(Repository.LocalPath).RunCommand("branch --unset-upstream " + txtTo.Text, true);
			else
				new GitHelper(Repository.LocalPath).RunCommand((chkCheckout.Checked ? "checkout -b " : "branch ") + txtTo.Text
					+ (chkTrack.Checked ? " --track " : " --no-track ") + txtFrom.Text, true);
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
