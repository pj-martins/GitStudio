namespace PaJaMa.GitStudio
{
	partial class frmAccount
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
			this.btnOK = new System.Windows.Forms.Button();
			this.cboAccount = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(714, 156);
			this.btnOK.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(200, 55);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// cboAccount
			// 
			this.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAccount.FormattingEnabled = true;
			this.cboAccount.Location = new System.Drawing.Point(39, 81);
			this.cboAccount.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
			this.cboAccount.Name = "cboAccount";
			this.cboAccount.Size = new System.Drawing.Size(1091, 39);
			this.cboAccount.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(33, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(118, 32);
			this.label1.TabIndex = 2;
			this.label1.Text = "Account";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(930, 156);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(200, 55);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmAccount
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1147, 227);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cboAccount);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
			this.Name = "frmAccount";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Select Account";
			this.Load += new System.EventHandler(this.frmAccount_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cboAccount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
	}
}