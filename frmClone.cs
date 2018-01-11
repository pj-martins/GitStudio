using PaJaMa.Common;
using PaJaMa.WinControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

		}

		private void cboBranches_DropDown(object sender, EventArgs e)
		{
			if (cboBranches.Items.Count < 1)
			{
				bool error = false;
				var remotes = new GitHelper(null).RunCommand("ls-remote " + txtURL.Text, false, ref error);
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
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var lines = new GitHelper(null).RunCommand("clone " + txtURL.Text + " " + txtPath.Text +
			(string.IsNullOrEmpty(cboBranches.Text) ? "" : " -b " + cboBranches.Text), true);

			if (lines.Length == 1 && lines[0].StartsWith("Cloning into"))
			{
				ClonedRepo = new GitRepository()
				{
					LocalPath = txtPath.Text,
					RemoteURL = txtURL.Text
				};
				settings.Repositories.Add(ClonedRepo);
				SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			else
			{
				ScrollableMessageBox.Show(lines, "ERROR!");
			}

		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			if (dlgOpenFolder.ShowDialog() == DialogResult.OK)
			{
				var selectedPath = dlgOpenFolder.SelectedPath;
				if (!selectedPath.EndsWith("\\"))
					selectedPath += "\\";
				var urlparts = txtURL.Text.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
				if (urlparts.Any())
					selectedPath += urlparts.Last();
				txtPath.Text = selectedPath;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void txtURL_DropDown(object sender, EventArgs e)
		{
			if (txtURL.Items.Count < 1)
			{
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				if (!string.IsNullOrEmpty(settings.GitHubUserName))
				{
					using (var wc = new WebClient())
					{
						wc.Headers.Add("User-Agent", "GitStudio");
						try
						{
							var repostring = wc.DownloadString($"https://api.github.com/users/{settings.GitHubUserName}/repos");
							var matches = Regex.Matches(repostring, "\"clone_url\":\"(.*?)\"");
							foreach (Match match in matches)
							{
								txtURL.Items.Add(match.Groups[1].Value);
							}
						}
						catch
						{

						}
					}
				}
			}
		}
	}
}
