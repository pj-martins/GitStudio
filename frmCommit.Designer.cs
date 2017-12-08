namespace PaJaMa.GitStudio
{
	partial class frmCommit
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
			this.txtComment = new System.Windows.Forms.TextBox();
			this.chkPush = new System.Windows.Forms.CheckBox();
			this.cboRemote = new System.Windows.Forms.ComboBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtComment
			// 
			this.txtComment.Location = new System.Drawing.Point(20, 12);
			this.txtComment.Multiline = true;
			this.txtComment.Name = "txtComment";
			this.txtComment.Size = new System.Drawing.Size(488, 146);
			this.txtComment.TabIndex = 0;
			// 
			// chkPush
			// 
			this.chkPush.AutoSize = true;
			this.chkPush.Location = new System.Drawing.Point(20, 261);
			this.chkPush.Name = "chkPush";
			this.chkPush.Size = new System.Drawing.Size(50, 17);
			this.chkPush.TabIndex = 1;
			this.chkPush.Text = "Push";
			this.chkPush.UseVisualStyleBackColor = true;
			this.chkPush.CheckedChanged += new System.EventHandler(this.chkPush_CheckedChanged);
			// 
			// cboRemote
			// 
			this.cboRemote.Enabled = false;
			this.cboRemote.FormattingEnabled = true;
			this.cboRemote.Location = new System.Drawing.Point(124, 257);
			this.cboRemote.Name = "cboRemote";
			this.cboRemote.Size = new System.Drawing.Size(312, 21);
			this.cboRemote.TabIndex = 2;
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(442, 257);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 3;
			this.btnGo.Text = "button1";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// frmCommit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(529, 292);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.cboRemote);
			this.Controls.Add(this.chkPush);
			this.Controls.Add(this.txtComment);
			this.Name = "frmCommit";
			this.Text = "frmCommit";
			this.Load += new System.EventHandler(this.frmCommit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtComment;
		private System.Windows.Forms.CheckBox chkPush;
		private System.Windows.Forms.ComboBox cboRemote;
		private System.Windows.Forms.Button btnGo;
	}
}