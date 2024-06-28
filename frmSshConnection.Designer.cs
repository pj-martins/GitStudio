namespace PaJaMa.GitStudio
{
    partial class frmSSHConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.txtHost = new System.Windows.Forms.TextBox();
			this.btnOpen = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtKeyFile = new System.Windows.Forms.TextBox();
			this.chkOpenSSH = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.chkRemoteCommand = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtHost
			// 
			this.txtHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHost.Location = new System.Drawing.Point(109, 48);
			this.txtHost.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtHost.Name = "txtHost";
			this.txtHost.Size = new System.Drawing.Size(535, 22);
			this.txtHost.TabIndex = 0;
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Location = new System.Drawing.Point(545, 251);
			this.btnOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(100, 28);
			this.btnOpen.TabIndex = 5;
			this.btnOpen.Text = "Open";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 52);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Host";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 116);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(36, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "User";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(109, 112);
			this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(397, 22);
			this.txtUser.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 148);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(67, 16);
			this.label3.TabIndex = 6;
			this.label3.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(109, 144);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(397, 22);
			this.txtPassword.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 84);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(34, 16);
			this.label4.TabIndex = 8;
			this.label4.Text = "Path";
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(109, 80);
			this.txtPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(535, 22);
			this.txtPath.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 180);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(55, 16);
			this.label5.TabIndex = 10;
			this.label5.Text = "Key File";
			// 
			// txtKeyFile
			// 
			this.txtKeyFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtKeyFile.Location = new System.Drawing.Point(109, 176);
			this.txtKeyFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtKeyFile.Name = "txtKeyFile";
			this.txtKeyFile.PasswordChar = '*';
			this.txtKeyFile.Size = new System.Drawing.Size(535, 22);
			this.txtKeyFile.TabIndex = 4;
			// 
			// chkOpenSSH
			// 
			this.chkOpenSSH.AutoSize = true;
			this.chkOpenSSH.Location = new System.Drawing.Point(109, 208);
			this.chkOpenSSH.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chkOpenSSH.Name = "chkOpenSSH";
			this.chkOpenSSH.Size = new System.Drawing.Size(93, 20);
			this.chkOpenSSH.TabIndex = 11;
			this.chkOpenSSH.Text = "Open SSH";
			this.chkOpenSSH.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(16, 18);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(33, 16);
			this.label6.TabIndex = 13;
			this.label6.Text = "Title";
			// 
			// txtTitle
			// 
			this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTitle.Location = new System.Drawing.Point(109, 15);
			this.txtTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(535, 22);
			this.txtTitle.TabIndex = 12;
			// 
			// chkRemoteCommand
			// 
			this.chkRemoteCommand.AutoSize = true;
			this.chkRemoteCommand.Location = new System.Drawing.Point(210, 208);
			this.chkRemoteCommand.Margin = new System.Windows.Forms.Padding(4);
			this.chkRemoteCommand.Name = "chkRemoteCommand";
			this.chkRemoteCommand.Size = new System.Drawing.Size(142, 20);
			this.chkRemoteCommand.TabIndex = 14;
			this.chkRemoteCommand.Text = "Remote Command";
			this.chkRemoteCommand.UseVisualStyleBackColor = true;
			// 
			// frmSSHConnection
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(661, 294);
			this.Controls.Add(this.chkRemoteCommand);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtTitle);
			this.Controls.Add(this.chkOpenSSH);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtKeyFile);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.txtHost);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "frmSSHConnection";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SSH Connection";
			this.Load += new System.EventHandler(this.frmSSHConnection_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtKeyFile;
		private System.Windows.Forms.CheckBox chkOpenSSH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.CheckBox chkRemoteCommand;
	}
}