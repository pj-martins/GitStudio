namespace PaJaMa.GitStudio
{
	partial class frmStashes
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
			this.components = new System.ComponentModel.Container();
			this.gridStashes = new System.Windows.Forms.DataGridView();
			this.mnuStashes = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.createBranchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.applyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.popToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gridDetails = new System.Windows.Forms.DataGridView();
			this.mnuDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.externalCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtDifferences = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.gridStashes)).BeginInit();
			this.mnuStashes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
			this.mnuDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridStashes
			// 
			this.gridStashes.AllowUserToAddRows = false;
			this.gridStashes.AllowUserToDeleteRows = false;
			this.gridStashes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridStashes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridStashes.ContextMenuStrip = this.mnuStashes;
			this.gridStashes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridStashes.Location = new System.Drawing.Point(0, 0);
			this.gridStashes.MultiSelect = false;
			this.gridStashes.Name = "gridStashes";
			this.gridStashes.ReadOnly = true;
			this.gridStashes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridStashes.Size = new System.Drawing.Size(437, 640);
			this.gridStashes.TabIndex = 0;
			this.gridStashes.SelectionChanged += new System.EventHandler(this.gridStashes_SelectionChanged);
			// 
			// mnuStashes
			// 
			this.mnuStashes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.createBranchToolStripMenuItem,
            this.applyToolStripMenuItem,
            this.popToolStripMenuItem});
			this.mnuStashes.Name = "mnuStashes";
			this.mnuStashes.Size = new System.Drawing.Size(149, 92);
			this.mnuStashes.Opening += new System.ComponentModel.CancelEventHandler(this.mnuStashes_Opening);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.deleteToolStripMenuItem.Text = "&Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// createBranchToolStripMenuItem
			// 
			this.createBranchToolStripMenuItem.Name = "createBranchToolStripMenuItem";
			this.createBranchToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.createBranchToolStripMenuItem.Text = "&Create Branch";
			this.createBranchToolStripMenuItem.Click += new System.EventHandler(this.createBranchToolStripMenuItem_Click);
			// 
			// applyToolStripMenuItem
			// 
			this.applyToolStripMenuItem.Name = "applyToolStripMenuItem";
			this.applyToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.applyToolStripMenuItem.Text = "&Apply";
			this.applyToolStripMenuItem.Click += new System.EventHandler(this.applyToolStripMenuItem_Click);
			// 
			// popToolStripMenuItem
			// 
			this.popToolStripMenuItem.Name = "popToolStripMenuItem";
			this.popToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.popToolStripMenuItem.Text = "&Pop";
			this.popToolStripMenuItem.Click += new System.EventHandler(this.popToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.txtDifferences);
			this.splitContainer1.Size = new System.Drawing.Size(1060, 640);
			this.splitContainer1.SplitterDistance = 727;
			this.splitContainer1.TabIndex = 3;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.gridStashes);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gridDetails);
			this.splitContainer2.Size = new System.Drawing.Size(727, 640);
			this.splitContainer2.SplitterDistance = 437;
			this.splitContainer2.TabIndex = 4;
			// 
			// gridDetails
			// 
			this.gridDetails.AllowUserToAddRows = false;
			this.gridDetails.AllowUserToDeleteRows = false;
			this.gridDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDetails.ContextMenuStrip = this.mnuDetails;
			this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridDetails.Location = new System.Drawing.Point(0, 0);
			this.gridDetails.Name = "gridDetails";
			this.gridDetails.ReadOnly = true;
			this.gridDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridDetails.Size = new System.Drawing.Size(286, 640);
			this.gridDetails.TabIndex = 1;
			this.gridDetails.SelectionChanged += new System.EventHandler(this.gridDetails_SelectionChanged);
			this.gridDetails.DoubleClick += new System.EventHandler(this.gridDetails_DoubleClick);
			// 
			// mnuDetails
			// 
			this.mnuDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.externalCompareToolStripMenuItem});
			this.mnuDetails.Name = "mnuDetails";
			this.mnuDetails.Size = new System.Drawing.Size(168, 26);
			// 
			// externalCompareToolStripMenuItem
			// 
			this.externalCompareToolStripMenuItem.Name = "externalCompareToolStripMenuItem";
			this.externalCompareToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.externalCompareToolStripMenuItem.Text = "&External Compare";
			this.externalCompareToolStripMenuItem.Click += new System.EventHandler(this.externalCompareToolStripMenuItem_Click);
			// 
			// txtDifferences
			// 
			this.txtDifferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDifferences.Location = new System.Drawing.Point(0, 0);
			this.txtDifferences.Multiline = true;
			this.txtDifferences.Name = "txtDifferences";
			this.txtDifferences.ReadOnly = true;
			this.txtDifferences.Size = new System.Drawing.Size(329, 640);
			this.txtDifferences.TabIndex = 0;
			// 
			// frmStashes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 640);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmStashes";
			this.ShowIcon = false;
			this.Text = "Stashes";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStashes_FormClosing);
			this.Load += new System.EventHandler(this.frmStashes_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridStashes)).EndInit();
			this.mnuStashes.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridDetails)).EndInit();
			this.mnuDetails.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridStashes;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtDifferences;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.DataGridView gridDetails;
		private System.Windows.Forms.ContextMenuStrip mnuStashes;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem createBranchToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem applyToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip mnuDetails;
		private System.Windows.Forms.ToolStripMenuItem externalCompareToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem popToolStripMenuItem;
	}
}