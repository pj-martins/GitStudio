using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
    public partial class frmOutput : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private BackgroundWorker _worker;
        private string _command;
        private bool _closeOnComplete;

        public frmOutput()
        {
            InitializeComponent();
        }

        public void ShowProgress(BackgroundWorker worker, string command, bool closeOnComplete)
        {
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _worker = worker;
            _command = command;
            _closeOnComplete = closeOnComplete;
            this.ShowDialog();
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_closeOnComplete)
            {
                this.Close();
            }
            else
            {
                progressMain.Visible = false;
                btnOK.Visible = true;
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            txtOutput.Text += "\r\n" + e.UserState.ToString();
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOutput_Load(object sender, EventArgs e)
        {
            txtOutput.Text = _command;
            txtOutput.SelectionStart = txtOutput.Text.Length;
            new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(200);
                this.Invoke(new Action(() => HideCaret(txtOutput.Handle)));
            })).Start();
            _worker.RunWorkerAsync();
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            HideCaret(txtOutput.Handle);
        }
    }
}
