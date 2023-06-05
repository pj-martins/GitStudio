using PaJaMa.Common;
using PaJaMa.GitStudio.Properties;
using PaJaMa.WinControls;
using PaJaMa.WinControls.TabControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime;
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

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text += " - " + version;
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
            GitHelper.GitLocation = settings.GitLocation;
            WinControls.TabControl.TabPage selectedTab = null;
            List<GitRepository> missing = new List<GitRepository>();
            foreach (var repo in settings.Repositories)
            {
                if (repo.SSHConnection == null && !Directory.Exists(repo.LocalPath))
                {
                    missing.Add(repo);
                    continue;
                }
                var tab = createRepository(repo);
                if (tab.Tag.ToString() == settings.FocusedRepository)
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
            else if (tabMain.TabPages.Count > 0)
            {
                selectedTab = tabMain.TabPages[0];
                tabMain.SelectedTab = selectedTab;
                _lastPage = selectedTab;
            }
            if (tabMain.SelectedTab != null)
                (tabMain.SelectedTab.Controls[0] as ucRepository).Init();
        }

        private Tuple<string, string> getTabText(GitRepository repo)
        {
            string tabText;
            string tipText;
            
            if (repo.SSHConnection == null)
			{
                tabText = repo.LocalPath;
                tipText = repo.LocalPath;
			}
            else
            {
                tabText = "SSH - ";
                if (repo.SSHConnection.Host.Length > 30)
                {
                    tabText += repo.SSHConnection.Host.Substring(0, 30) + "...";
                }
                else
                {
                    tabText += repo.SSHConnection.Host;
                }

                tabText += ":";
                if (repo.SSHConnection.Path.Length > 30)
                {
                    tabText += "..." + repo.SSHConnection.Path.Substring(repo.SSHConnection.Path.Length - 30);
                }
                else
                {
                    tabText += repo.SSHConnection.Path;
                }

                tipText = $"{repo.SSHConnection.Host}:{repo.SSHConnection.Path}";
            }

            if (!string.IsNullOrEmpty(repo.Title))
            {
                tabText = repo.Title;
            }

            return new Tuple<string, string>(tabText, tipText);
        }

        private WinControls.TabControl.TabPage createRepository(GitRepository repo)
        {
            var uc = new ucRepository();
            uc.Repository = repo;
            uc.Dock = DockStyle.Fill;
            var tabText = getTabText(repo);
            var tab = new WinControls.TabControl.TabPage(tabText.Item1);
            tab.TooltipText = tabText.Item2;
            tab.Controls.Add(uc);
            tab.ContextMenuStrip = new ContextMenuStrip();
            tab.ContextMenuStrip.Items.Add("Edit &Title", null, new EventHandler(this.editTitleToolStripMenuItem_Click));
            if (repo.SSHConnection == null)
            {
                tab.ContextMenuStrip.Items.Add("&Open In Explorer", null, new EventHandler(this.openInExplorerToolStripMenuItem_Click));
            }
            tab.ContextMenuStrip.Items.Add("Set &Remote", null, new EventHandler(this.setRemoteToolStripMenuItem_Click));
            if (repo.SSHConnection == null)
            {
                tab.ContextMenuStrip.Items.Add("&Enable File Watching", null, new EventHandler(this.EnableFileWatchingToolStripMenuItem_Click));
                tab.ContextMenuStrip.Items.Add("&Disable File Watching", null, new EventHandler(this.DisableFileWatchingToolStripMenuItem_Click));
            }
            else
            {
                tab.ContextMenuStrip.Items.Add("&Set Writeable", null, new EventHandler(this.SetWriteableToolStripMenuItem_Click));
                tab.ContextMenuStrip.Items.Add("&Edit SSH Settings", null, new EventHandler(this.EditSSHSettingsToolStripMenuItem_Click));
                tab.ContextMenuStrip.Items.Add("&Clone SSH Repo", null, new EventHandler(this.CloneSSHRepoToolStripMenuItem_Click));
            }
            tab.Tag = repo;
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
                var repo = new GitRepository()
                {
                    LocalPath = dlg.SelectedPath
                };
                var helper = new GitHelper(repo);
                var remote = helper.RunCommand("config --get remote.origin.url", true, ref error).FirstOrDefault();
                if (error) return;
                repo.RemoteURL = remote;
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
                var repo = new GitRepository()
                {
                    LocalPath = dlg.SelectedPath,
                };
                var helper = new GitHelper(repo);
                helper.RunCommand("init", true, ref error);
                if (error) return;
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

        private void editTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var input = InputBox.Show("Title");
            if (input.Result == DialogResult.OK)
            {
                var tab = ((sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip).SourceControl as Tab;
                tab.TabPage.Text = input.Text;
                var repo = tab.TabPage.Tag as GitRepository;
                repo.Title = input.Text;
                var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
                var curr = settings.Repositories.First(x => x.SSHConnection?.Host == repo.SSHConnection.Host && x.SSHConnection?.Path == repo.SSHConnection.Path);
                curr.Title = input.Text;
                SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
            }
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
            var repo = settings.Repositories.First(r => getTabText(r).Item1 == tab.Text);
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
            settings.FocusedRepository = tabMain.SelectedTab.Tag.ToString();
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
            var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
            var tab = tabMain.SelectedTab;
            var repo = settings.Repositories.First(r => r.ToString() == tab.Tag.ToString());
            var result = WinControls.InputBox.Show("Enter remote URL", "Remote URL", repo.RemoteURL);
            if (result.Result == System.Windows.Forms.DialogResult.OK)
            {
                bool error = false;
                var helper = new GitHelper(repo);
                helper.RunCommand("remote add origin " + result.Text, true, ref error);
                if (error) return;
                repo.RemoteURL = result.Text;
                SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
                (tab.Controls[0] as ucRepository).RefreshBranches(true);
            }
        }

        private void EnableFileWatchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
            var tab = tabMain.SelectedTab;
            var repo = settings.Repositories.First(r => r.ToString() == tab.Tag.ToString());
            repo.SuspendWatchingFiles = false;
            SettingsHelper.SaveUserSettings<GitUserSettings>(settings);

        }

        private void DisableFileWatchingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
            var tab = tabMain.SelectedTab;
            var repo = settings.Repositories.First(r => r.ToString() == tab.Tag.ToString());
            repo.SuspendWatchingFiles = true;
            SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
        }

        private void SetWriteableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var repo = tabMain.SelectedTab.Tag as GitRepository;
            SSHHelper.RunCommand(repo.SSHConnection, $"sudo chmod 777 -R {repo.SSHConnection.Path}");
        }

        private void EditSSHSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new frmSSHConnection();
            var repo = tabMain.SelectedTab.Tag as GitRepository;
            dlg.GitRepository = repo;
            var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
            var curr = settings.Repositories.First(x => x.SSHConnection?.Host == repo.SSHConnection.Host && x.SSHConnection?.Path == repo.SSHConnection.Path);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                curr.SSHConnection = dlg.GitRepository.SSHConnection;
                curr.Title = dlg.GitRepository.Title;
                bool error = false;
                var helper = new GitHelper(repo);
                var remote = helper.RunCommand("config --get remote.origin.url", true, ref error).FirstOrDefault();
                repo.RemoteURL = remote;
                repo.SSHConnection = curr.SSHConnection;
                if (error) return;
                settings.FocusedRepository = repo.ToString();
                SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
                var tipAndText = getTabText(repo);
                tabMain.SelectedTab.Text = tipAndText.Item1;
                tabMain.SelectedTab.TooltipText = tipAndText.Item2;
                (tabMain.SelectedTab.Controls[0] as ucRepository).Init();
            }
        }

        private void CloneSSHRepoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new frmSSHConnection();
            var repo = tabMain.SelectedTab.Tag as GitRepository;
            dlg.GitRepository = new GitRepository();
            dlg.GitRepository.Title = repo.Title + " (Copy)";
            dlg.GitRepository.SSHConnection = new SSHConnection()
            {
                Host = repo.SSHConnection.Host,
                UserName = repo.SSHConnection.UserName,
                Path = repo.SSHConnection.Path,
                PasswordEncrypted = repo.SSHConnection.PasswordEncrypted,
                KeyFile = repo.SSHConnection.KeyFile,
                UseCMD = repo.SSHConnection.UseCMD,
            };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool error = false;
                var helper = new GitHelper(dlg.GitRepository);
                var remote = helper.RunCommand("config --get remote.origin.url", true, ref error).FirstOrDefault();
                dlg.GitRepository.RemoteURL = remote;
                if (error) return;
                var tab = createRepository(dlg.GitRepository);
                var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
                settings.Repositories.Add(dlg.GitRepository);
                SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
                tabMain.SelectedTab = tab;
                (tab.Controls[0] as ucRepository).Init();
            }
        }

        private void openSSHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new frmSSHConnection();
            dlg.GitRepository = new GitRepository();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bool error = false;
                var helper = new GitHelper(dlg.GitRepository);
                var remote = helper.RunCommand("config --get remote.origin.url", true, ref error).FirstOrDefault();
                dlg.GitRepository.RemoteURL = remote;
                if (error) return;
                var tab = createRepository(dlg.GitRepository);
                var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
                settings.Repositories.Add(dlg.GitRepository);
                SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
                tabMain.SelectedTab = tab;
                (tab.Controls[0] as ucRepository).Init();
            }
        }
    }
}
