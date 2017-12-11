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
	public partial class frmManageAccount : Form
	{
		public string InitialUserName { get; set; }

		public frmManageAccount()
		{
			InitializeComponent();
		}

		private void frmManageAccount_Load(object sender, EventArgs e)
		{
			txtUserName.Text = InitialUserName;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (txtPassword.Text != txtPasswordConfirm.Text)
			{
				MessageBox.Show("Passwords do not match!");
				return;
			}
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var acct = settings.Accounts.FirstOrDefault(a => a.UserName == txtUserName.Text);
			if (acct == null)
			{
				acct = new GitAccount() { UserName = txtUserName.Text };
				settings.Accounts.Add(acct);
			}

			acct.PasswordDecrypted = txtPassword.Text;
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
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
