using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class frmClone : Form
	{
		const string REFS_HEAD = "refs/heads/";
		public GitRepository ClonedRepo { get; private set; }

		public frmClone()
		{
			InitializeComponent();
		}

		private void frmClone_Load(object sender, EventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			foreach (var acct in settings.Accounts)
			{
				cboAccount.Items.Add(acct);
			}
			cboAccount.SelectedIndex = 0;
		}

		private void cboBranches_DropDown(object sender, EventArgs e)
		{
			if (cboBranches.Items.Count < 1)
			{
				string error = string.Empty;
				var remotes = new GitHelper(null).RunCommand("ls-remote " + txtURL.Text, ref error);
				if (!string.IsNullOrEmpty(error)) return;
				foreach (var remote in remotes)
				{
					var repo = remote.Split('\t')[1];
					if (repo.StartsWith(REFS_HEAD))
					{
						cboBranches.Items.Add(repo.Substring(REFS_HEAD.Length));
					}
				}
			}
		}

		private void btnClone_Click(object sender, EventArgs e)
		{
			var parts = txtURL.Text.Split(new string[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var acct = cboAccount.SelectedItem as GitAccount;
			var pwd = acct.PasswordDecrypted;
			var url = parts[0] + "//" + cboAccount.Text + ":" +
				System.Web.HttpUtility.UrlEncode(pwd)
				+ "@" + parts[1];

			var inf = new ProcessStartInfo("git.exe", "clone " + url + " " + txtPath.Text);
			inf.UseShellExecute = false;
			inf.RedirectStandardOutput = true;
			var p = Process.Start(inf);
			p.WaitForExit();

			ClonedRepo = new GitRepository()
			{
				UserName = acct.UserName,
				LocalPath = txtPath.Text,
				RemoteURLDecrypted = txtURL.Text
			};
			settings.Repositories.Add(ClonedRepo);
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
