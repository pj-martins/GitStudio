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
	public partial class frmTrackRemote : Form
	{
		private LocalBranch _branchFrom;
		public LocalBranch BranchFrom
		{
			get { return _branchFrom; }
			set
			{
				_branchFrom = value;
				txtFrom.Text = value.BranchName;
				tryFindRemote();
			}
		}

		public GitRepository Repository { get; set; }

		private List<RemoteBranch> _remoteBranches;
		public List<RemoteBranch> RemoteBranches
		{
			get { return _remoteBranches; }
			set
			{
				_remoteBranches = value;
				cboTo.Items.AddRange(value.ToArray());
				tryFindRemote();
			}
		}

		public frmTrackRemote()
		{
			InitializeComponent();
		}

		private void tryFindRemote()
		{
			if (cboTo.Items.Count < 1 || string.IsNullOrEmpty(txtFrom.Text)) return;
			cboTo.SelectedItem = cboTo.Items.OfType<RemoteBranch>().FirstOrDefault(r => r.BranchName == txtFrom.Text);
		}

		private void btnTrack_Click(object sender, EventArgs e)
		{
			if (cboTo.SelectedItem == null)
			{
				MessageBox.Show("Please select a remote branch!");
				return;
			}
			var remoteBranchName = (cboTo.SelectedItem as RemoteBranch).BranchName;
			new GitHelper(Repository).RunCommand(string.Format("branch --set-upstream-to={0} {1}",
				remoteBranchName, txtFrom.Text), true);
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
				btnTrack_Click(sender, e);
		}
	}
}
