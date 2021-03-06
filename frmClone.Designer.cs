﻿namespace PaJaMa.GitStudio
{
	partial class frmClone
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
			this.txtURL = new System.Windows.Forms.ComboBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.dlgOpenFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.btnClone = new System.Windows.Forms.Button();
			this.cboBranches = new System.Windows.Forms.ComboBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtURL
			// 
			this.txtURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtURL.Location = new System.Drawing.Point(68, 12);
			this.txtURL.Name = "txtURL";
			this.txtURL.Size = new System.Drawing.Size(502, 21);
			this.txtURL.TabIndex = 0;
			this.txtURL.DropDown += new System.EventHandler(this.txtURL_DropDown);
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.Location = new System.Drawing.Point(68, 38);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(421, 20);
			this.txtPath.TabIndex = 1;
			// 
			// btnClone
			// 
			this.btnClone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClone.Location = new System.Drawing.Point(414, 95);
			this.btnClone.Name = "btnClone";
			this.btnClone.Size = new System.Drawing.Size(75, 23);
			this.btnClone.TabIndex = 3;
			this.btnClone.Text = "OK";
			this.btnClone.UseVisualStyleBackColor = true;
			this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
			// 
			// cboBranches
			// 
			this.cboBranches.FormattingEnabled = true;
			this.cboBranches.Location = new System.Drawing.Point(68, 64);
			this.cboBranches.Name = "cboBranches";
			this.cboBranches.Size = new System.Drawing.Size(271, 21);
			this.cboBranches.TabIndex = 4;
			this.cboBranches.DropDown += new System.EventHandler(this.cboBranches_DropDown);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(495, 95);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "URL";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(496, 37);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(75, 22);
			this.btnBrowse.TabIndex = 7;
			this.btnBrowse.Text = "Browse";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(38, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Target";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 67);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Branch";
			// 
			// frmClone
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(582, 130);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.cboBranches);
			this.Controls.Add(this.btnClone);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.txtURL);
			this.Name = "frmClone";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Clone Repository";
			this.Load += new System.EventHandler(this.frmClone_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox txtURL;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.FolderBrowserDialog dlgOpenFolder;
		private System.Windows.Forms.Button btnClone;
		private System.Windows.Forms.ComboBox cboBranches;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
    }
}