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
    public partial class frmSshConnection : Form
    {
        public SshConnection SshConnection { get; set; }
        public frmSshConnection()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            this.SshConnection = new SshConnection();
            this.SshConnection.Host = txtHost.Text;
            this.SshConnection.UserName = txtUser.Text;
            this.SshConnection.Password = txtPassword.Text;
            this.SshConnection.Path = txtPath.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
