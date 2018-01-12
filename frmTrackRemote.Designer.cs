namespace PaJaMa.GitStudio
{
	partial class frmTrackRemote
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
			this.cboTo = new System.Windows.Forms.ComboBox();
			this.btnTrack = new System.Windows.Forms.Button();
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
			this.txtFrom.TabIndex = 6;
			// 
			// cboTo
			// 
			this.cboTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTo.Location = new System.Drawing.Point(62, 38);
			this.cboTo.Name = "cboTo";
			this.cboTo.Size = new System.Drawing.Size(426, 21);
			this.cboTo.TabIndex = 0;
			this.cboTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyUp);
			// 
			// btnTrack
			// 
			this.btnTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTrack.Location = new System.Drawing.Point(332, 76);
			this.btnTrack.Name = "btnTrack";
			this.btnTrack.Size = new System.Drawing.Size(75, 23);
			this.btnTrack.TabIndex = 2;
			this.btnTrack.Text = "OK";
			this.btnTrack.UseVisualStyleBackColor = true;
			this.btnTrack.Click += new System.EventHandler(this.btnTrack_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 41);
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
			this.label2.Location = new System.Drawing.Point(12, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Local";
			// 
			// frmTrackRemote
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 111);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnTrack);
			this.Controls.Add(this.cboTo);
			this.Controls.Add(this.txtFrom);
			this.Name = "frmTrackRemote";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Track Remote";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.ComboBox cboTo;
		private System.Windows.Forms.Button btnTrack;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
	}
}