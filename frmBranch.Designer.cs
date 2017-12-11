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
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtFrom
			// 
			this.txtFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFrom.Location = new System.Drawing.Point(62, 12);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.ReadOnly = true;
			this.txtFrom.Size = new System.Drawing.Size(426, 20);
			this.txtFrom.TabIndex = 0;
			// 
			// txtTo
			// 
			this.txtTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTo.Location = new System.Drawing.Point(62, 38);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(426, 20);
			this.txtTo.TabIndex = 1;
			// 
			// btnBranch
			// 
			this.btnBranch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBranch.Location = new System.Drawing.Point(332, 76);
			this.btnBranch.Name = "btnBranch";
			this.btnBranch.Size = new System.Drawing.Size(75, 23);
			this.btnBranch.TabIndex = 2;
			this.btnBranch.Text = "OK";
			this.btnBranch.UseVisualStyleBackColor = true;
			this.btnBranch.Click += new System.EventHandler(this.btnBranch_Click);
			// 
			// chkTrack
			// 
			this.chkTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkTrack.AutoSize = true;
			this.chkTrack.Checked = true;
			this.chkTrack.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkTrack.Location = new System.Drawing.Point(145, 80);
			this.chkTrack.Name = "chkTrack";
			this.chkTrack.Size = new System.Drawing.Size(94, 17);
			this.chkTrack.TabIndex = 3;
			this.chkTrack.Text = "Track Remote";
			this.chkTrack.UseVisualStyleBackColor = true;
			// 
			// chkCheckout
			// 
			this.chkCheckout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkCheckout.AutoSize = true;
			this.chkCheckout.Checked = true;
			this.chkCheckout.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCheckout.Location = new System.Drawing.Point(254, 80);
			this.chkCheckout.Name = "chkCheckout";
			this.chkCheckout.Size = new System.Drawing.Size(72, 17);
			this.chkCheckout.TabIndex = 4;
			this.chkCheckout.Text = "Checkout";
			this.chkCheckout.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Remote";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(413, 76);
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
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Local";
			// 
			// frmBranch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 111);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.chkCheckout);
			this.Controls.Add(this.chkTrack);
			this.Controls.Add(this.btnBranch);
			this.Controls.Add(this.txtTo);
			this.Controls.Add(this.txtFrom);
			this.Name = "frmBranch";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create Branch";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.Button btnBranch;
		private System.Windows.Forms.CheckBox chkTrack;
		private System.Windows.Forms.CheckBox chkCheckout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
	}
}