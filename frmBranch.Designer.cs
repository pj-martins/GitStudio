namespace PaJaMa.GitStudio
{
	partial class frmBranch
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
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.btnBranch = new System.Windows.Forms.Button();
			this.chkTrack = new System.Windows.Forms.CheckBox();
			this.chkCheckout = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(32, 25);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.ReadOnly = true;
			this.txtFrom.Size = new System.Drawing.Size(582, 20);
			this.txtFrom.TabIndex = 0;
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(32, 51);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(582, 20);
			this.txtTo.TabIndex = 1;
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
			this.chkTrack.Size = new System.Drawing.Size(94, 17);
			this.chkTrack.TabIndex = 3;
			this.chkTrack.Text = "Track Remote";
			this.chkTrack.UseVisualStyleBackColor = true;
			// 
			// chkCheckout
			// 
			this.chkCheckout.AutoSize = true;
			this.chkCheckout.Location = new System.Drawing.Point(534, 254);
			this.chkCheckout.Name = "chkCheckout";
			this.chkCheckout.Size = new System.Drawing.Size(72, 17);
			this.chkCheckout.TabIndex = 4;
			this.chkCheckout.Text = "Checkout";
			this.chkCheckout.UseVisualStyleBackColor = true;
			// 
			// frmBranch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(666, 378);
			this.Controls.Add(this.chkCheckout);
			this.Controls.Add(this.chkTrack);
			this.Controls.Add(this.btnBranch);
			this.Controls.Add(this.txtTo);
			this.Controls.Add(this.txtFrom);
			this.Name = "frmBranch";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "frmBranch";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.Button btnBranch;
		private System.Windows.Forms.CheckBox chkTrack;
		private System.Windows.Forms.CheckBox chkCheckout;
	}
}