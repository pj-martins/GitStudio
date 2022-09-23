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
    public partial class frmSSHConnection : Form
    {
        public SSHConnection SSHConnection { get; set; }
        public frmSSHConnection()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.SSHConnection == null) this.SSHConnection = new SSHConnection();
            this.SSHConnection.Host = txtHost.Text;
            this.SSHConnection.UserName = txtUser.Text;
            this.SSHConnection.Password = txtPassword.Text;
            this.SSHConnection.Path = txtPath.Text;
            this.SSHConnection.KeyFile = txtKeyFile.Text;
            this.SSHConnection.UseCMD = chkUseCMD.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void frmSSHConnection_Load(object sender, EventArgs e)
        {
            if (this.SSHConnection != null)
            {
                txtHost.Text = this.SSHConnection.Host;
                txtUser.Text = this.SSHConnection.UserName;
                txtPassword.Text = this.SSHConnection.Password;
                txtPath.Text = this.SSHConnection.Path;
                txtKeyFile.Text = this.SSHConnection.KeyFile;
                chkUseCMD.Checked = this.SSHConnection.UseCMD;
                btnOpen.Text = "Save";
            }
        }
    }
}
