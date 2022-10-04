namespace PaJaMa.GitStudio
{
	partial class frmSetup
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
			this.txtExternalDiffApplication = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtArgumentsFormat = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtGitHubUserName = new System.Windows.Forms.TextBox();
			this.btnGitLocationBrowse = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.txtGitLocation = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtExternalDiffApplication
			// 
			this.txtExternalDiffApplication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtExternalDiffApplication.Location = new System.Drawing.Point(137, 41);
			this.txtExternalDiffApplication.Name = "txtExternalDiffApplication";
			this.txtExternalDiffApplication.ReadOnly = true;
			this.txtExternalDiffApplication.Size = new System.Drawing.Size(406, 20);
			this.txtExternalDiffApplication.TabIndex = 1;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Location = new System.Drawing.Point(468, 144);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "OK";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(549, 144);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(119, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "External Diff Application";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(549, 39);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnBrowse.TabIndex = 8;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtArgumentsFormat
			// 
			this.txtArgumentsFormat.Location = new System.Drawing.Point(137, 67);
			this.txtArgumentsFormat.Name = "txtArgumentsFormat";
			this.txtArgumentsFormat.Size = new System.Drawing.Size(165, 20);
			this.txtArgumentsFormat.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 70);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 13);
			this.label1.TabIndex = 10;
			this.label1.Text = "Arguments Format";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "GitHub User Name";
			// 
			// txtGitHubUserName
			// 
			this.txtGitHubUserName.Location = new System.Drawing.Point(137, 93);
			this.txtGitHubUserName.Name = "txtGitHubUserName";
			this.txtGitHubUserName.Size = new System.Drawing.Size(165, 20);
			this.txtGitHubUserName.TabIndex = 11;
			// 
			// btnGitLocationBrowse
			// 
			this.btnGitLocationBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGitLocationBrowse.Location = new System.Drawing.Point(549, 10);
			this.btnGitLocationBrowse.Name = "btnGitLocationBrowse";
			this.btnGitLocationBrowse.Size = new System.Drawing.Size(75, 23);
			this.btnGitLocationBrowse.TabIndex = 15;
			this.btnGitLocationBrowse.Text = "Browse";
			this.btnGitLocationBrowse.UseVisualStyleBackColor = true;
			this.btnGitLocationBrowse.Click += new System.EventHandler(this.btnGitLocationBrowse_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Git location";
			// 
			// txtGitLocation
			// 
			this.txtGitLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtGitLocation.Location = new System.Drawing.Point(137, 12);
			this.txtGitLocation.Name = "txtGitLocation";
			this.txtGitLocation.ReadOnly = true;
			this.txtGitLocation.Size = new System.Drawing.Size(406, 20);
			this.txtGitLocation.TabIndex = 13;
			// 
			// frmSetup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(636, 179);
			this.Controls.Add(this.btnGitLocationBrowse);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtGitLocation);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtGitHubUserName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtArgumentsFormat);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.txtExternalDiffApplication);
			this.Name = "frmSetup";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create Branch";
			this.Load += new System.EventHandler(this.frmSetup_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox txtExternalDiffApplication;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtArgumentsFormat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtGitHubUserName;
		private System.Windows.Forms.Button btnGitLocationBrowse;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtGitLocation;
	}
}