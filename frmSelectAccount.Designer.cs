namespace PaJaMa.GitStudio
{
	partial class frmSelectAccount
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
			this.btnSelect = new System.Windows.Forms.Button();
			this.cboAccount = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(521, 293);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 5;
			this.btnSelect.Text = "button1";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// cboAccount
			// 
			this.cboAccount.FormattingEnabled = true;
			this.cboAccount.Location = new System.Drawing.Point(62, 41);
			this.cboAccount.Name = "cboAccount";
			this.cboAccount.Size = new System.Drawing.Size(477, 21);
			this.cboAccount.TabIndex = 4;
			// 
			// frmSelectAccount
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 328);
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.cboAccount);
			this.Name = "frmSelectAccount";
			this.Text = "frmSelectAccount";
			this.Load += new System.EventHandler(this.frmSelectAccount_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.ComboBox cboAccount;
	}
}