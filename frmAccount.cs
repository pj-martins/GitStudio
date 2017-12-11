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
	public partial class frmAccount : Form
	{
		public GitAccount SelectedAccount
		{
			get { return cboAccount.SelectedItem as GitAccount; }
		}

		public frmAccount()
		{
			InitializeComponent();
		}

		private void frmAccount_Load(object sender, EventArgs e)
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			cboAccount.Items.AddRange(settings.Accounts.ToArray());
			cboAccount.SelectedIndex = 0;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
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
