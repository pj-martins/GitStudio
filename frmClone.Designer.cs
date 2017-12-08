namespace PaJaMa.GitStudio
{
	partial class frmClone
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
			this.txtURL = new System.Windows.Forms.TextBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.cboAccount = new System.Windows.Forms.ComboBox();
			this.btnClone = new System.Windows.Forms.Button();
			this.cboBranches = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// txtURL
			// 
			this.txtURL.Location = new System.Drawing.Point(68, 12);
			this.txtURL.Name = "txtURL";
			this.txtURL.Size = new System.Drawing.Size(623, 20);
			this.txtURL.TabIndex = 0;
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(68, 38);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(623, 20);
			this.txtPath.TabIndex = 1;
			// 
			// cboAccount
			// 
			this.cboAccount.FormattingEnabled = true;
			this.cboAccount.Location = new System.Drawing.Point(68, 64);
			this.cboAccount.Name = "cboAccount";
			this.cboAccount.Size = new System.Drawing.Size(623, 21);
			this.cboAccount.TabIndex = 2;
			// 
			// btnClone
			// 
			this.btnClone.Location = new System.Drawing.Point(686, 256);
			this.btnClone.Name = "btnClone";
			this.btnClone.Size = new System.Drawing.Size(75, 23);
			this.btnClone.TabIndex = 3;
			this.btnClone.Text = "button1";
			this.btnClone.UseVisualStyleBackColor = true;
			this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
			// 
			// cboBranches
			// 
			this.cboBranches.FormattingEnabled = true;
			this.cboBranches.Location = new System.Drawing.Point(335, 91);
			this.cboBranches.Name = "cboBranches";
			this.cboBranches.Size = new System.Drawing.Size(356, 21);
			this.cboBranches.TabIndex = 4;
			this.cboBranches.DropDown += new System.EventHandler(this.cboBranches_DropDown);
			// 
			// frmClone
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(773, 291);
			this.Controls.Add(this.cboBranches);
			this.Controls.Add(this.btnClone);
			this.Controls.Add(this.cboAccount);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.txtURL);
			this.Name = "frmClone";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "frmClone";
			this.Load += new System.EventHandler(this.frmClone_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtURL;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.ComboBox cboAccount;
		private System.Windows.Forms.Button btnClone;
		private System.Windows.Forms.ComboBox cboBranches;
	}
}