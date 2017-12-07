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
	public partial class frmCheckout : Form
	{
		public string RemoteBranch
		{
			get { return txtRemote.Text; }
			set { txtRemote.Text = value; }
		}

		public string LocalBranch
		{
			get { return txtLocal.Text; }
			set { txtLocal.Text = value; }
		}

		public bool TrackRemote
		{
			get { return chkTrack.Checked; }
		}

		public frmCheckout()
		{
			InitializeComponent();
		}

		private void btnBranch_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
