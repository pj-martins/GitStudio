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
	public partial class frmAccounts : Form
	{
		public frmAccounts()
		{
			InitializeComponent();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (new frmManageAccount().ShowDialog() == DialogResult.OK)
			{
				refreshGrid();
			}
		}

		private void frmAccounts_Load(object sender, EventArgs e)
		{
			refreshGrid();
		}

		private void refreshGrid()
		{
			var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
			gridAccounts.DataSource = settings.Accounts.Select(a => new
			{
				UserName = a.UserName
			}).ToList();
		}

		private void gridAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == Delete.Index)
			{
				var acct = gridAccounts.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
				if (MessageBox.Show("Are you sure you want to delete " + acct + "? ", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
					var s = settings.Accounts.First(a => a.UserName == acct);
					settings.Accounts.Remove(s);
					SettingsHelper.SaveUserSettings<GitUserSettings>(settings);
					gridAccounts.DataSource = settings.Accounts.Select(a => new
					{
						UserName = a.UserName
					}).ToList();
				}
			}
			else if (e.ColumnIndex == Edit.Index)
			{
				var settings = SettingsHelper.GetUserSettings<GitUserSettings>();
				var s = settings.Accounts.First(a => a.UserName == gridAccounts.Rows[e.RowIndex].Cells["UserName"].Value.ToString());
				var frm = new frmManageAccount();
				frm.InitialUserName = s.UserName;
				frm.Show();
			}
		}
	}
}
