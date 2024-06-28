using Newtonsoft.Json;
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
using System.Management;
using System.Runtime;
using System.Security.Cryptography;
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

		[DebuggerNonUserCode()]
		private void KillProcessAndChildren(int pid, List<int> killed)
		{
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
			ManagementObjectCollection moc = searcher.Get();
			foreach (ManagementObject mo in moc)
			{
				KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]), killed);
			}
			if (killed.Contains(pid)) return;
			try
			{
				Process proc = Process.GetProcessById(pid);
				proc.Kill();
				killed.Add(pid);
			}
			catch (ArgumentException)
			{
				// Process already exited.
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			terminateLingering();
			FormSettings.LoadSettings(this);

			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			GitHelper.GitLocation = settings.GitLocation;
			WinControls.TabControl.TabPage selectedTab = null;
			List<GitRepository> missing = new List<GitRepository>();
			var nonGroups = settings.Repositories.Where(r => string.IsNullOrEmpty(r.GroupName));
			selectedTab = loadRepositories(settings, nonGroups, tabMain, missing);
			var groups = settings.Repositories.Where(r => !string.IsNullOrEmpty(r.GroupName));
			foreach (var g in groups)
			{
				var tab = createUpdateGroup(g);
				if (selectedTab == null && tab.Tag.ToString() == settings.FocusedRepository)
				{
					selectedTab = tab;
				}
				if (selectedTab == null && !string.IsNullOrEmpty(settings.FocusedRepository))
				{
					foreach (var page in (tab.Controls[0] as WinControls.TabControl.TabControl).TabPages)
					{
						if (page.Tag.ToString() == settings.FocusedRepository)
						{
							selectedTab = tab;
							break;

							// TODO: child selected
						}
					}
				}
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
			{
				if (tabMain.SelectedTab.Controls[0] is WinControls.TabControl.TabControl)
				{
					var c = tabMain.SelectedTab.Controls[0] as PaJaMa.WinControls.TabControl.TabControl;
					c.SelectedTab = c.TabPages[0];
					(c.TabPages[0].Controls[0] as ucRepository).Init();
				}
				else
				{
					(tabMain.SelectedTab.Controls[0] as ucRepository).Init();
				}
			}
		}

		private WinControls.TabControl.TabPage loadRepositories(GitUserSettings settings, IEnumerable<GitRepository> repositories, WinControls.TabControl.TabControl parent, List<GitRepository> missing)
		{
			WinControls.TabControl.TabPage selectedTab = null;
			foreach (var repo in repositories)
			{
				if (repo.SSHConnection == null && !Directory.Exists(repo.LocalPath))
				{
					missing.Add(repo);
					continue;
				}
				var tab = createRepository(repo, parent);
				if (tab.Tag.ToString() == settings.FocusedRepository)
					selectedTab = tab;
			}
			return selectedTab;
		}

		[DebuggerNonUserCode()]
		private void terminateLingering()
		{
			try
			{
				var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
				if (Directory.Exists(tmpDir))
				{
					var procsPath = Path.Combine(tmpDir, "ActiveProcesses.json");
					if (File.Exists(procsPath))
					{
						List<int> procs = new List<int>();
						try
						{
							procs = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(procsPath));
						}
						catch { }
						foreach (var p in procs)
						{
							try
							{
								var proc = Process.GetProcessById(p);
								if (proc.ProcessName == "cmd")
								{
									KillProcessAndChildren(p, new List<int>());
								}
							}
							catch { }
						}
						File.Delete(procsPath);
					}
					Directory.Delete(tmpDir, true);
				}
			}
			catch { }
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

		private WinControls.TabControl.TabPage createRepository(GitRepository repo, WinControls.TabControl.TabControl parent = null)
		{
			var uc = new ucRepository();
			uc.MainForm = this;
			uc.Repository = repo;
			uc.Dock = DockStyle.Fill;
			var tabText = getTabText(repo);
			var tab = new WinControls.TabControl.TabPage(tabText.Item1);
			tab.TooltipText = tabText.Item2;
			tab.Controls.Add(uc);
			tab.ContextMenuStrip = new ContextMenuStrip();
			tab.ContextMenuStrip.Items.Add("Edit &Title", null, new EventHandler(this.editTitleToolStripMenuItem_Click));
			tab.ContextMenuStrip.Items.Add("Set &Group", null, new EventHandler(this.setGroupToolStripMenuItem_Click));
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
			if (parent == null)
			{
				parent = tabMain;
			}
			foreach (ToolStripItem item in tab.ContextMenuStrip.Items)
			{
				item.Tag = parent;
			}
			parent.TabPages.Add(tab);
			return tab;
		}

		private WinControls.TabControl.TabPage createUpdateGroup(GitRepository grp)
		{
			var groupTab = tabMain.TabPages.FirstOrDefault(p => p.Tag?.ToString() == grp.GroupName);
			var groupTabControl = groupTab == null ? null : groupTab.Controls[0] as WinControls.TabControl.TabControl;
			if (groupTabControl == null)
			{
				groupTabControl = new WinControls.TabControl.TabControl();
				groupTabControl.TabClosing += tab_TabClosing;
				groupTabControl.TabChanged += tab_TabChanged;
				groupTabControl.Dock = DockStyle.Fill;
				groupTabControl.AllowRemove = true;
				groupTab = new WinControls.TabControl.TabPage("* " + grp.GroupName);
				groupTab.Tag = grp;
				groupTab.Controls.Add(groupTabControl);
				tabMain.TabPages.Add(groupTab);
			}
			
			groupTabControl.TabPages.Clear();
			foreach (var repo in grp.Children)
			{
				createRepository(repo, groupTabControl);
			}
			return groupTab;
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
			var tab = ((sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip).SourceControl as Tab;
			var repo = tab.TabPage.Tag as GitRepository;
			var input = InputBox.Show("Title", string.Empty, repo.Title);
			if (input.Result == DialogResult.OK)
			{
				tab.TabPage.Text = input.Text;
				repo.Title = input.Text;
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				var curr = settings.Repositories.OfType<GitRepository>().First(x => x.AreEqual(repo));
				curr.Title = input.Text;
				SettingsHelper.SaveUserSettings(settings);
			}
		}

		private void setGroupToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO: listbox
			var input = InputBox.Show("Group");
			if (input.Result == DialogResult.OK)
			{
				var tab = ((sender as ToolStripMenuItem).GetCurrentParent() as ContextMenuStrip).SourceControl as Tab;
				var repo = tab.TabPage.Tag as GitRepository;
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				if (string.IsNullOrEmpty(input.Text))
				{
					var curr = settings.Repositories.FirstOrDefault((g) => g.Children.Any(c => c.AreEqual(repo)));
					curr.Children = curr.Children.Where(c => !c.AreEqual(repo)).ToList();
					// TODO:
				}
				else
				{
					var grp = settings.Repositories.FirstOrDefault(g => g.GroupName == input.Text);
					if (grp == null)
					{
						grp = new GitRepository();
						grp.GroupName = input.Text;
						settings.Repositories.Add(grp);
					}
					var settingsRepo = settings.Repositories.Find(x => x.AreEqual(repo));
					settings.Repositories.Remove(settingsRepo);
					grp.Children.Add(settingsRepo);
					tab.TabControl.TabPages.Remove(tab.TabPage);
					createUpdateGroup(grp);
				}
				SettingsHelper.SaveUserSettings(settings);
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

		private void tab_TabClosing(object sender, WinControls.TabControl.TabEventArgs e)
		{
			var tabControl = sender as WinControls.TabControl.TabControl;
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var tab = e.TabPage;
			(tab.Controls[0] as ucRepository).Deactivate();
			if (tab == _lastPage) _lastPage = null;
			var repo = settings.Repositories.First(r => getTabText(r).Item1 == tab.Text);
			settings.Repositories.Remove(repo);
			tabControl.TabPages.Remove(tab);
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void tab_TabChanged(object sender, WinControls.TabControl.TabEventArgs e)
		{
			var tabControl = sender as WinControls.TabControl.TabControl;
			if (_lastPage != null)
			{
				if (_lastPage.Controls[0] is WinControls.TabControl.TabControl)
				{
					var tabCtrl = _lastPage.Controls[0] as WinControls.TabControl.TabControl;
					foreach (var p in tabCtrl.TabPages)
					{
						(p.Controls[0] as ucRepository).Deactivate();
					}
				}
				else
				{
					(_lastPage.Controls[0] as ucRepository).Deactivate();
				}
			}
			_lastPage = e.TabPage;
			if (e.TabPage.Controls[0] is WinControls.TabControl.TabControl)
			{
				var c = e.TabPage.Controls[0] as PaJaMa.WinControls.TabControl.TabControl;
				c.SelectedTab = c.TabPages[0];
				(c.TabPages[0].Controls[0] as ucRepository).Init();
			}
			else
			{
				(e.TabPage.Controls[0] as ucRepository).Init();
			}
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			settings.FocusedRepository = tabControl.SelectedTab.Tag.ToString();
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
		}

		private void tab_TabOrderChanged(object sender, WinControls.TabControl.TabEventArgs e)
		{
			var tabControl = sender as WinControls.TabControl.TabControl;
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			settings.Repositories = new List<GitRepository>();
			foreach (var page in tabControl.TabPages)
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
			var tsi = sender as ToolStripItem;
			var tabCtrl = tsi.Tag as WinControls.TabControl.TabControl;
			var dlg = new frmSSHConnection();
			var repo = tabCtrl.SelectedTab.Tag as GitRepository;
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
				RemoteCommand = repo.SSHConnection.RemoteCommand,
			};
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				bool error = false;
				var helper = new GitHelper(dlg.GitRepository);
				var remote = helper.RunCommand("config --get remote.origin.url", true, ref error).FirstOrDefault();
				dlg.GitRepository.RemoteURL = remote;
				if (error) return;
				var tab = createRepository(dlg.GitRepository, tabCtrl);
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				settings.Repositories.Add(dlg.GitRepository);
				SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
				tabCtrl.SelectedTab = tab;
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
