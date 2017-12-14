namespace PaJaMa.GitStudio
{
	partial class frmCommitHistory
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
			this.gridCommits = new System.Windows.Forms.DataGridView();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gridDetails = new System.Windows.Forms.DataGridView();
			this.mnuDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.externalCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtDifferences = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblBranches = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).BeginInit();
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
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridCommits
			// 
			this.gridCommits.AllowUserToAddRows = false;
			this.gridCommits.AllowUserToDeleteRows = false;
			this.gridCommits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridCommits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridCommits.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridCommits.Location = new System.Drawing.Point(0, 0);
			this.gridCommits.Name = "gridCommits";
			this.gridCommits.ReadOnly = true;
			this.gridCommits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridCommits.Size = new System.Drawing.Size(437, 640);
			this.gridCommits.TabIndex = 0;
			this.gridCommits.SelectionChanged += new System.EventHandler(this.gridCommits_SelectionChanged);
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
			this.splitContainer2.Panel1.Controls.Add(this.gridCommits);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gridDetails);
			this.splitContainer2.Panel2.Controls.Add(this.panel1);
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
			this.gridDetails.Location = new System.Drawing.Point(0, 28);
			this.gridDetails.Name = "gridDetails";
			this.gridDetails.ReadOnly = true;
			this.gridDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridDetails.Size = new System.Drawing.Size(286, 612);
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
			// panel1
			// 
			this.panel1.Controls.Add(this.lblBranches);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(286, 28);
			this.panel1.TabIndex = 2;
			// 
			// lblBranches
			// 
			this.lblBranches.AutoSize = true;
			this.lblBranches.Location = new System.Drawing.Point(3, 9);
			this.lblBranches.Name = "lblBranches";
			this.lblBranches.Size = new System.Drawing.Size(0, 13);
			this.lblBranches.TabIndex = 0;
			// 
			// frmCommitHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 640);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmCommitHistory";
			this.ShowIcon = false;
			this.Text = "Commit History";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCompareBranches_FormClosing);
			this.Load += new System.EventHandler(this.frmCompareBranches_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).EndInit();
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
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridCommits;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtDifferences;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.DataGridView gridDetails;
		private System.Windows.Forms.ContextMenuStrip mnuDetails;
		private System.Windows.Forms.ToolStripMenuItem externalCompareToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblBranches;
	}
}