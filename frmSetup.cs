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
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			_settings.ExternalDiffApplication = txtExternalDiffApplication.Text;
			_settings.ExternalDiffArgumentsFormat = txtArgumentsFormat.Text;
			SettingsHelper.SaveUserSettings<GitUserSettings>(_settings);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			var dlg = new OpenFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
				txtExternalDiffApplication.Text = dlg.FileName;
		}
	}
}