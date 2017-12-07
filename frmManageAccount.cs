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
		public frmManageAccount()
		{
			InitializeComponent();
		}

		private void frmManageAccount_Load(object sender, EventArgs e)
		{
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			var acct = settings.Accounts.FirstOrDefault(a => a.UserName == txtUserName.Text);
			if (acct == null)
			{
				acct = new GitAccount() { UserName = txtUserName.Text };
				settings.Accounts.Add(acct);
			}

			acct.PasswordDecrypted = txtPassword.Text;
			SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
			this.Close();
		}
	}
}
