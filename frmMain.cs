﻿using PaJaMa.Common;
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
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}

		private void accountsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new frmAccounts().Show();
		}

		private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmClone();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				createRepository(frm.ClonedRepo);
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			foreach (var repo in settings.Repositories)
			{
				createRepository(repo);
			}
			(tabMain.SelectedTab.Controls[0] as ucRepository).Init();
		}

		private void createRepository(GitRepository repo)
		{
			var uc = new ucRepository();
			uc.Repository = repo;
			uc.Dock = DockStyle.Fill;
			var tab = new TabPage();
			tab.Controls.Add(uc);
			tab.Text = repo.LocalPath;
			tabMain.TabPages.Add(tab);
			tab.Focus();
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dlg = new FolderBrowserDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				var remote = GitHelper.RunCommand("config --get remote.origin.url", dlg.SelectedPath)[0];
				var username = GitHelper.RunCommand("config user.name", dlg.SelectedPath)[0];
				var repo = new GitRepository()
				{
					LocalPath = dlg.SelectedPath,
					RemoteURL = remote,
					UserName = username
				};
				createRepository(repo);
				settings.Repositories.Add(repo);
				SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
			}
		}

		private void tabMain_TabIndexChanged(object sender, EventArgs e)
		{
			(tabMain.SelectedTab.Controls[0] as ucRepository).Init();
		}
	}
}
