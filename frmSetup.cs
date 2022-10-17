using PaJaMa.Common;
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
	public partial class frmSetup : Form
	{
		private GitUserSettings _settings;
		public frmSetup()
		{
			InitializeComponent();
		}

		private void frmSetup_Load(object sender, EventArgs e)
		{
			_settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			txtExternalDiffApplication.Text = _settings.ExternalDiffApplication;
			txtArgumentsFormat.Text = _settings.ExternalDiffArgumentsFormat;
			txtGitHubUserName.Text = _settings.GitHubUserName;
			txtGitLocation.Text = _settings.GitLocation;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			_settings.ExternalDiffApplication = txtExternalDiffApplication.Text;
			_settings.ExternalDiffArgumentsFormat = txtArgumentsFormat.Text;
			_settings.GitHubUserName = txtGitHubUserName.Text;
			_settings.GitLocation = txtGitLocation.Text;
			SettingsHelper.SaveUserSettings<GitUserSettings>(_settings);
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				txtExternalDiffApplication.Text = dlg.FileName;
		}

		private void btnGitLocationBrowse_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			dlg.Filter = "git.exe";
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				txtGitLocation.Text = dlg.FileName;
		}
	}
}
