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
	public partial class frmPush : Form
	{
		private List<RemoteBranch> _remoteBranches;
		public List<RemoteBranch> RemoteBranches
		{
			get { return _remoteBranches; }
			set
			{
				_remoteBranches = value;
				cboRemote.Items.Clear();
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

		public frmPush()
		{
			InitializeComponent();
		}

		private void frmPush_Load(object sender, EventArgs e)
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

		private void btnGo_Click(object sender, EventArgs e)
		{
			var helper = new GitHelper(Repository.LocalPath);
			string error = string.Empty;
			helper.RunCommand("push -u origin " + cboRemote.Text, ref error);
			if (!string.IsNullOrEmpty(error)) return;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
