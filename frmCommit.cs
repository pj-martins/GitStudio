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
	public partial class frmCommit : Form
	{
		private List<RemoteBranch> _remoteBranches;
		public List<RemoteBranch> RemoteBranches
		{
			get { return _remoteBranches; }
			set
			{
				_remoteBranches = value;
				cboRemote.Items.Clear();
				if (value != null)
					cboRemote.Items.AddRange(value.ToArray());
				//selectRemote();
			}
		}

		private LocalBranch _localBranch;
		public LocalBranch LocalBranch
		{
			get { return _localBranch; }
			set
			{
				_localBranch = value;
				//selectRemote();
			}

		}

		public GitRepository Repository { get; set; }

		public frmCommit()
		{
			InitializeComponent();
		}

		private void chkPush_CheckedChanged(object sender, EventArgs e)
		{
			cboRemote.Enabled = chkPush.Checked;
			if (!cboRemote.Enabled)
			{
				cboRemote.SelectedItem = null;
				cboRemote.Text = string.Empty;
			}
			else
			{
				if (_localBranch.TracksBranch != null)
				{
					cboRemote.SelectedItem = _localBranch.TracksBranch;
				}
				else
				{
					cboRemote.Text = _localBranch.BranchName;
				}
			}
		}

		private void frmCommit_Load(object sender, EventArgs e)
		{

		}

		//private void selectRemote()
		//{
		//	cboRemote.Enabled = chkPush.Checked;
		//	if (_localBranch != null && _localBranch.TracksBranch != null)
		//	{
		//		cboRemote.SelectedItem = _localBranch.TracksBranch;
		//		cboRemote.Enabled = false;
		//	}
		//}

		private void btnGo_Click(object sender, EventArgs e)
		{
			var helper = new GitHelper(Repository.LocalPath);
			var lines = helper.RunCommand("commit -m \"" + txtMessage.Text + "\"", true).ToList();
			if (lines.Any(l => l.StartsWith("error")))
			{
				ScrollableMessageBox.Show(lines.ToArray());
				return;
			}

			var branchName = cboRemote.Text;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);

			if (chkPush.Checked)
				lines.AddRange(helper.RunCommand("push -u origin " + branchName, true));

			ScrollableMessageBox.Show(lines.ToArray());

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
