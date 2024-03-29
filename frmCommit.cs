﻿using System;
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
			else if (_localBranch != null)
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
			string branchName = cboRemote.Text;
			if (branchName.StartsWith("origin/"))
				branchName = branchName.Substring(7);
			bool push = chkPush.Checked;

			var worker = new BackgroundWorker();
			worker.DoWork += (object sender2, DoWorkEventArgs e2) =>
			{
				var helper = new GitHelper(Repository);
				var lines = helper.RunCommand("commit" + (chkAmend.Checked ? " --amend" : "") + " -m \"" + txtMessage.Text + "\"", worker).ToList();
				if (lines.Any(l => l.StartsWith("error")))
					return;

				if (push)
					lines.AddRange(helper.RunCommand("push -u origin " + branchName, worker));
			};
			new frmOutput().ShowProgress(worker, "Committing");

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
