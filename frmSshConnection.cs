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
        public GitRepository GitRepository { get; set; }
        public frmSSHConnection()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.GitRepository.SSHConnection == null) this.GitRepository.SSHConnection = new SSHConnection();
            this.GitRepository.Title = txtTitle.Text;
            this.GitRepository.SSHConnection.Host = txtHost.Text;
            this.GitRepository.SSHConnection.UserName = txtUser.Text;
            this.GitRepository.SSHConnection.Password = txtPassword.Text;
            this.GitRepository.SSHConnection.Path = txtPath.Text;
            this.GitRepository.SSHConnection.KeyFile = txtKeyFile.Text;
            this.GitRepository.SSHConnection.UseCMD = chkOpenSSH.Checked;
			this.GitRepository.SSHConnection.RemoteCommand = chkRemoteCommand.Checked;
			this.DialogResult = DialogResult.OK;
        }

        private void frmSSHConnection_Load(object sender, EventArgs e)
        {
            txtTitle.Text = this.GitRepository.Title;
            if (this.GitRepository.SSHConnection != null)
            {
                txtHost.Text = this.GitRepository.SSHConnection.Host;
                txtUser.Text = this.GitRepository.SSHConnection.UserName;
                txtPassword.Text = this.GitRepository.SSHConnection.Password;
                txtPath.Text = this.GitRepository.SSHConnection.Path;
                txtKeyFile.Text = this.GitRepository.SSHConnection.KeyFile;
                chkOpenSSH.Checked = this.GitRepository.SSHConnection.UseCMD;
                chkRemoteCommand.Checked = this.GitRepository.SSHConnection.RemoteCommand;
                btnOpen.Text = "Save";
            }
        }
    }
}
