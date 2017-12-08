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
				chkTrack.Enabled = value is RemoteBranch;
			}
		}

		public GitRepository Repository { get; set; }

		public frmBranch()
		{
			InitializeComponent();
		}

		private void btnBranch_Click(object sender, EventArgs e)
		{
			bool error = false;
			new GitHelper(Repository.LocalPath).RunCommand((chkCheckout.Checked ? "checkout -b " : "branch ") + txtTo.Text
				+ (chkTrack.Checked ? " --track " : " --no-track ") + txtFrom.Text, ref error);
			if (error) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
