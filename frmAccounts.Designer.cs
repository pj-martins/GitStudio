namespace PaJaMa.GitStudio
{
	partial class frmAccounts
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
			this.gridAccounts = new System.Windows.Forms.DataGridView();
			this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridAccounts
			// 
			this.gridAccounts.AllowUserToAddRows = false;
			this.gridAccounts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridAccounts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserName,
            this.Delete,
            this.Edit});
			this.gridAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridAccounts.Location = new System.Drawing.Point(0, 24);
			this.gridAccounts.Name = "gridAccounts";
			this.gridAccounts.ReadOnly = true;
			this.gridAccounts.Size = new System.Drawing.Size(491, 379);
			this.gridAccounts.TabIndex = 0;
			this.gridAccounts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridAccounts_CellContentClick);
			// 
			// UserName
			// 
			this.UserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.UserName.DataPropertyName = "UserName";
			this.UserName.HeaderText = "User Name";
			this.UserName.Name = "UserName";
			this.UserName.ReadOnly = true;
			this.UserName.Width = 85;
			// 
			// Delete
			// 
			this.Delete.HeaderText = "";
			this.Delete.Name = "Delete";
			this.Delete.ReadOnly = true;
			this.Delete.Text = "Delete";
			this.Delete.UseColumnTextForButtonValue = true;
			// 
			// Edit
			// 
			this.Edit.HeaderText = "";
			this.Edit.Name = "Edit";
			this.Edit.ReadOnly = true;
			this.Edit.Text = "Edit";
			this.Edit.UseColumnTextForButtonValue = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(491, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// frmAccounts
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(491, 403);
			this.Controls.Add(this.gridAccounts);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmAccounts";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Accounts";
			this.Load += new System.EventHandler(this.frmAccounts_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridAccounts)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView gridAccounts;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
		private System.Windows.Forms.DataGridViewButtonColumn Delete;
		private System.Windows.Forms.DataGridViewButtonColumn Edit;
	}
}