namespace PaJaMa.GitStudio
{
	partial class frmIgnorePath
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
			this.cboPathParts = new System.Windows.Forms.ComboBox();
			this.btnIgnore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cboPathParts
			// 
			this.cboPathParts.FormattingEnabled = true;
			this.cboPathParts.Location = new System.Drawing.Point(12, 12);
			this.cboPathParts.Name = "cboPathParts";
			this.cboPathParts.Size = new System.Drawing.Size(324, 21);
			this.cboPathParts.TabIndex = 0;
			// 
			// btnIgnore
			// 
			this.btnIgnore.Location = new System.Drawing.Point(261, 61);
			this.btnIgnore.Name = "btnIgnore";
			this.btnIgnore.Size = new System.Drawing.Size(75, 23);
			this.btnIgnore.TabIndex = 1;
			this.btnIgnore.Text = "button1";
			this.btnIgnore.UseVisualStyleBackColor = true;
			this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
			// 
			// frmIgnorePath
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(348, 96);
			this.Controls.Add(this.btnIgnore);
			this.Controls.Add(this.cboPathParts);
			this.Name = "frmIgnorePath";
			this.Text = "frmIgnorePath";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cboPathParts;
		private System.Windows.Forms.Button btnIgnore;
	}
}