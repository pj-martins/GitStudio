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
			this.txtMessage = new System.Windows.Forms.TextBox();
			this.chkPush = new System.Windows.Forms.CheckBox();
			this.chkAmend = new System.Windows.Forms.CheckBox();
			this.cboRemote = new System.Windows.Forms.ComboBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtMessage
			// 
			this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMessage.Location = new System.Drawing.Point(12, 25);
			this.txtMessage.Multiline = true;
			this.txtMessage.Name = "txtMessage";
			this.txtMessage.Size = new System.Drawing.Size(576, 242);
			this.txtMessage.TabIndex = 0;
			// 
			// chkPush
			// 
			this.chkPush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkPush.AutoSize = true;
			this.chkPush.Location = new System.Drawing.Point(12, 277);
			this.chkPush.Name = "chkPush";
			this.chkPush.Size = new System.Drawing.Size(66, 17);
			this.chkPush.TabIndex = 1;
			this.chkPush.Text = "Push To";
			this.chkPush.UseVisualStyleBackColor = true;
			this.chkPush.CheckedChanged += new System.EventHandler(this.chkPush_CheckedChanged);
			// 
			// chkAmend
			// 
			this.chkAmend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.chkAmend.AutoSize = true;
			this.chkAmend.Location = new System.Drawing.Point(309, 277);
			this.chkAmend.Name = "chkAmend";
			this.chkAmend.Size = new System.Drawing.Size(66, 17);
			this.chkAmend.TabIndex = 1;
			this.chkAmend.Text = "Amend";
			this.chkAmend.UseVisualStyleBackColor = true;
			// 
			// cboRemote
			// 
			this.cboRemote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cboRemote.Enabled = false;
			this.cboRemote.FormattingEnabled = true;
			this.cboRemote.Location = new System.Drawing.Point(84, 275);
			this.cboRemote.Name = "cboRemote";
			this.cboRemote.Size = new System.Drawing.Size(219, 21);
			this.cboRemote.TabIndex = 2;
			// 
			// btnGo
			// 
			this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGo.Location = new System.Drawing.Point(382, 273);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 3;
			this.btnGo.Text = "OK";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Message";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(463, 273);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// frmCommit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(600, 308);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.cboRemote);
			this.Controls.Add(this.chkPush);
			this.Controls.Add(this.chkAmend);
			this.Controls.Add(this.txtMessage);
			this.Name = "frmCommit";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Commit";
			this.Load += new System.EventHandler(this.frmCommit_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtMessage;
		private System.Windows.Forms.CheckBox chkPush;
		private System.Windows.Forms.CheckBox chkAmend;
		private System.Windows.Forms.ComboBox cboRemote;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
	}
}