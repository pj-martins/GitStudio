namespace PaJaMa.GitStudio
{
	partial class frmPush
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
			this.cboRemote = new System.Windows.Forms.ComboBox();
			this.btnGo = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cboRemote
			// 
			this.cboRemote.FormattingEnabled = true;
			this.cboRemote.Location = new System.Drawing.Point(12, 12);
			this.cboRemote.Name = "cboRemote";
			this.cboRemote.Size = new System.Drawing.Size(312, 21);
			this.cboRemote.TabIndex = 2;
			// 
			// btnGo
			// 
			this.btnGo.Location = new System.Drawing.Point(330, 12);
			this.btnGo.Name = "btnGo";
			this.btnGo.Size = new System.Drawing.Size(75, 23);
			this.btnGo.TabIndex = 3;
			this.btnGo.Text = "button1";
			this.btnGo.UseVisualStyleBackColor = true;
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// frmPush
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 52);
			this.Controls.Add(this.btnGo);
			this.Controls.Add(this.cboRemote);
			this.Name = "frmPush";
			this.Text = "frmCommit";
			this.Load += new System.EventHandler(this.frmPush_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cboRemote;
		private System.Windows.Forms.Button btnGo;
	}
}