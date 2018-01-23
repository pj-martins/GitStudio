using PaJaMa.WinControls;
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
	public partial class frmStash : Form
	{
		public GitRepository Repository { get; set; }

		public frmStash()
		{
			InitializeComponent();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			var helper = new GitHelper(Repository.LocalPath);
			var lines = helper.RunCommand("stash save \"" + txtMessage.Text + "\"", true);
			if (lines.Any(l => l.Contains("error"))) return;
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
