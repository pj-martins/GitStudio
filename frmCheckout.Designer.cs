namespace PaJaMa.GitStudio
{
	partial class frmCheckout
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
			this.txtRemote = new System.Windows.Forms.TextBox();
			this.txtLocal = new System.Windows.Forms.TextBox();
			this.btnBranch = new System.Windows.Forms.Button();
			this.chkTrack = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtRemote
			// 
			this.txtRemote.Location = new System.Drawing.Point(32, 25);
			this.txtRemote.Name = "txtRemote";
			this.txtRemote.ReadOnly = true;
			this.txtRemote.Size = new System.Drawing.Size(582, 20);
			this.txtRemote.TabIndex = 0;
			// 
			// txtLocal
			// 
			this.txtLocal.Location = new System.Drawing.Point(32, 51);
			this.txtLocal.Name = "txtLocal";
			this.txtLocal.Size = new System.Drawing.Size(582, 20);
			this.txtLocal.TabIndex = 1;
			// 
			// btnBranch
			// 
			this.btnBranch.Location = new System.Drawing.Point(539, 177);
			this.btnBranch.Name = "btnBranch";
			this.btnBranch.Size = new System.Drawing.Size(75, 23);
			this.btnBranch.TabIndex = 2;
			this.btnBranch.Text = "button1";
			this.btnBranch.UseVisualStyleBackColor = true;
			this.btnBranch.Click += new System.EventHandler(this.btnBranch_Click);
			// 
			// chkTrack
			// 
			this.chkTrack.AutoSize = true;
			this.chkTrack.Location = new System.Drawing.Point(534, 231);
			this.chkTrack.Name = "chkTrack";
			this.chkTrack.Size = new System.Drawing.Size(80, 17);
			this.chkTrack.TabIndex = 3;
			this.chkTrack.Text = "checkBox1";
			this.chkTrack.UseVisualStyleBackColor = true;
			// 
			// frmBranch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(666, 378);
			this.Controls.Add(this.chkTrack);
			this.Controls.Add(this.btnBranch);
			this.Controls.Add(this.txtLocal);
			this.Controls.Add(this.txtRemote);
			this.Name = "frmBranch";
			this.Text = "frmBranch";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtRemote;
		private System.Windows.Forms.TextBox txtLocal;
		private System.Windows.Forms.Button btnBranch;
		private System.Windows.Forms.CheckBox chkTrack;
	}
}