using PaJaMa.Common;
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
		private PaJaMa.WinControls.TabControl.TabPage _lastPage;
		public frmMain()
		{
			InitializeComponent();
		}

		private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var frm = new frmClone();
			if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
			WinControls.TabControl.TabPage selectedTab = null;
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
				if (result == System.Windows.Forms.DialogResult.Yes)
				{
					foreach (var m in missing)
					{
						settings.Repositories.Remove(m);
					}
					SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				}
			}

			if (selectedTab != null)
			{
				tabMain.SelectedTab = selectedTab;
				_lastPage = selectedTab;
			}
			if (tabMain.SelectedTab != null)
				(tabMain.SelectedTab.Controls[0] as ucRepository).Init();
		}

		private WinControls.TabControl.TabPage createRepository(GitRepository repo)
		{
			var uc = new ucRepository();
			uc.Repository = repo;
			uc.Dock = DockStyle.Fill;
			var tab = new WinControls.TabControl.TabPage(repo.LocalPath);
			tab.Controls.Add(uc);
			tabMain.TabPages.Add(tab);
			return tab;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dlg = new FolderBrowserDialog();
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			if (!string.IsNullOrEmpty(settings.LastBrowsedFolder) && Directory.Exists(settings.LastBrowsedFolder))
				dlg.SelectedPath = settings.LastBrowsedFolder;

			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var dlg = new FolderBrowserDialog();
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			if (!string.IsNullOrEmpty(settings.LastBrowsedFolder) && Directory.Exists(settings.LastBrowsedFolder))
				dlg.SelectedPath = settings.LastBrowsedFolder;

			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				settings.LastBrowsedFolder = dlg.SelectedPath;
				bool error = false;
				var helper = new GitHelper(dlg.SelectedPath);
				helper.RunCommand("init", true, ref error);
				if (error) return;
				var repo = new GitRepository()
				{
					LocalPath = dlg.SelectedPath,
				};
				var tab = createRepository(repo);
				settings.Repositories.Add(repo);
				SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				tabMain.SelectedTab = tab;
				(tab.Controls[0] as ucRepository).Init();
			}
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

		private void setupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new frmSetup().ShowDialog();
		}

		private void tabMain_TabClosing(object sender, WinControls.TabControl.TabEventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var tab = e.TabPage;
			(tab.Controls[0] as ucRepository).Deactivate();
			if (tab == _lastPage) _lastPage = null;
			var repo = settings.Repositories.First(r => r.LocalPath == tab.Text);
			settings.Repositories.Remove(repo);
			tabMain.TabPages.Remove(tab);
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void tabMain_TabChanged(object sender, WinControls.TabControl.TabEventArgs e)
		{
			if (_lastPage != null)
			{
				(_lastPage.Controls[0] as ucRepository).Deactivate();
			}
			_lastPage = e.TabPage;
			(e.TabPage.Controls[0] as ucRepository).Init();
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			settings.FocusedRepository = tabMain.SelectedTab.Text;
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void tabMain_TabOrderChanged(object sender, WinControls.TabControl.TabEventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			settings.Repositories = new List<GitRepository>();
			foreach (var page in tabMain.TabPages)
			{
				settings.Repositories.Add((page.Controls[0] as ucRepository).Repository);
			}
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void setRemoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var result = WinControls.InputBox.Show("Enter remote URL", "Remote URL");
			if (result.Result == System.Windows.Forms.DialogResult.OK)
			{
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				var tab = tabMain.SelectedTab;
				var repo = settings.Repositories.First(r => r.LocalPath == tab.Text);
				bool error = false;
				var helper = new GitHelper(repo.LocalPath);
				helper.RunCommand("remote add origin " + result.Text, true, ref error);
				if (error) return;
				repo.RemoteURL = result.Text;
				SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				(tab.Controls[0] as ucRepository).RefreshBranches(true);
			}
		}
	}
}
