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
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(343, 137);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "button1";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// cboAccount
			// 
			this.cboAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAccount.FormattingEnabled = true;
			this.cboAccount.Location = new System.Drawing.Point(12, 12);
			this.cboAccount.Name = "cboAccount";
			this.cboAccount.Size = new System.Drawing.Size(406, 21);
			this.cboAccount.TabIndex = 1;
			// 
			// frmAccount
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(430, 172);
			this.Controls.Add(this.cboAccount);
			this.Controls.Add(this.btnOK);
			this.Name = "frmAccount";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "frmAccount";
			this.Load += new System.EventHandler(this.frmAccount_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cboAccount;
	}
}