﻿using PaJaMa.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
			try
			{
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (Directory.Exists(tmpDir))
					Directory.Delete(tmpDir, true);
			}
			catch { }
			FormSettings.LoadSettings(this);

			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			TabPage selectedTab = null;
			List<GitRepository> missing = new List<GitRepository>();
			foreach (var repo in settings.Repositories)
			{
				if (!Directory.Exists(repo.LocalPath))
				{
					missing.Add(repo);
					continue;
				}
				var tab = createRepository(repo);
				if (tab.Text == settings.FocusedRepository)
					selectedTab = tab;
			}
			if (missing.Any())
			{
				var result = MessageBox.Show("The following repositories are missing:\r\n" +
					string.Join("\r\n", missing.Select(m => m.LocalPath)) + "\r\n\r\nWould you like to remove them?", "Error!", MessageBoxButtons.YesNo);
				if (result == DialogResult.Yes)
				{
					foreach (var m in missing)
					{
						settings.Repositories.Remove(m);
					}
					SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				}
			}

			if (selectedTab != null)
				tabMain.SelectedTab = selectedTab;
			if (tabMain.SelectedTab != null)
				(tabMain.SelectedTab.Controls[0] as ucRepository).Init();
		}

		private TabPage createRepository(GitRepository repo)
		{
			var uc = new ucRepository();
			uc.Repository = repo;
			uc.Dock = DockStyle.Fill;
			var tab = new TabPage();
			tab.Controls.Add(uc);
			tab.Text = repo.LocalPath;
			tabMain.TabPages.Add(tab);
			return tab;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dlg = new FolderBrowserDialog();
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			if (!string.IsNullOrEmpty(settings.LastBrowsedFolder) && Directory.Exists(settings.LastBrowsedFolder))
				dlg.SelectedPath = settings.LastBrowsedFolder;

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				settings.LastBrowsedFolder = dlg.SelectedPath;
				bool error = false;
				var helper = new GitHelper(dlg.SelectedPath);
				var remote = helper.RunCommand("config --get remote.origin.url", true, ref error).FirstOrDefault();
				if (error) return;
				var repo = new GitRepository()
				{
					LocalPath = dlg.SelectedPath,
					RemoteURL = remote,
				};
				var tab = createRepository(repo);
				settings.Repositories.Add(repo);
				SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				tabMain.SelectedTab = tab;
				(tab.Controls[0] as ucRepository).Init();
			}
		}

		private void tabMain_TabIndexChanged(object sender, EventArgs e)
		{
			(tabMain.SelectedTab.Controls[0] as ucRepository).Init();
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			settings.FocusedRepository = tabMain.SelectedTab.Text;
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void openInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start(tabMain.SelectedTab.Text);
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized) return;

			try
			{
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (Directory.Exists(tmpDir))
					Directory.Delete(tmpDir, true);
			}
			catch { }

			FormSettings.SaveSettings(this);
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var tab = tabMain.SelectedTab;
			var repo = settings.Repositories.First(r => r.LocalPath == tab.Text);
			settings.Repositories.Remove(repo);
			tabMain.TabPages.Remove(tab);
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void setupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new frmSetup().ShowDialog();
		}
	}
}
