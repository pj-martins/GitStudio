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
	public partial class frmSelectAccount : Form
	{
		public GitAccount SelectedAccount { get; private set; }
		public frmSelectAccount()
		{
			InitializeComponent();
		}

		private void frmSelectAccount_Load(object sender, EventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			foreach (var acct in settings.Accounts)
			{
				cboAccount.Items.Add(acct);
			}
			cboAccount.SelectedIndex = 0;
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			SelectedAccount = cboAccount.SelectedItem as GitAccount;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
