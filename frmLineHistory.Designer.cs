namespace PaJaMa.GitStudio
{
	partial class frmLineHistory
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
			this.mnuDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.externalCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.ucDifferences = new ucDifferences();
			this.gridLines = new System.Windows.Forms.DataGridView();
			this.Num = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).BeginInit();
			this.mnuDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).BeginInit();
			this.SuspendLayout();
			// 
			// gridCommits
			// 
			this.gridCommits.AllowUserToAddRows = false;
			this.gridCommits.AllowUserToDeleteRows = false;
			this.gridCommits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridCommits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridCommits.ContextMenuStrip = this.mnuDetails;
			this.gridCommits.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridCommits.Location = new System.Drawing.Point(0, 0);
			this.gridCommits.Name = "gridCommits";
			this.gridCommits.ReadOnly = true;
			this.gridCommits.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridCommits.Size = new System.Drawing.Size(363, 640);
			this.gridCommits.TabIndex = 0;
			this.gridCommits.SelectionChanged += new System.EventHandler(this.gridCommits_SelectionChanged);
			this.gridCommits.DoubleClick += new System.EventHandler(this.gridCommits_DoubleClick);
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
			this.splitContainer1.Panel2.Controls.Add(this.ucDifferences);
			this.splitContainer1.Size = new System.Drawing.Size(1060, 640);
			this.splitContainer1.SplitterDistance = 787;
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
			this.splitContainer2.Panel1.Controls.Add(this.gridLines);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gridCommits);
			this.splitContainer2.Size = new System.Drawing.Size(787, 640);
			this.splitContainer2.SplitterDistance = 420;
			this.splitContainer2.TabIndex = 4;
			// 
			// ucDifferences
			// 
			this.ucDifferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDifferences.Location = new System.Drawing.Point(0, 0);
			this.ucDifferences.Name = "ucDifferences";
			this.ucDifferences.Size = new System.Drawing.Size(269, 640);
			this.ucDifferences.TabIndex = 0;
			// 
			// gridLines
			// 
			this.gridLines.AllowUserToAddRows = false;
			this.gridLines.AllowUserToDeleteRows = false;
			this.gridLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridLines.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			this.gridLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Num,
            this.Line});
			this.gridLines.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridLines.Location = new System.Drawing.Point(0, 0);
			this.gridLines.Name = "gridLines";
			this.gridLines.ReadOnly = true;
			this.gridLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridLines.Size = new System.Drawing.Size(420, 640);
			this.gridLines.TabIndex = 1;
			this.gridLines.SelectionChanged += new System.EventHandler(this.gridLines_SelectionChanged);
			// 
			// Num
			// 
			this.Num.DataPropertyName = "Number";
			this.Num.HeaderText = "#";
			this.Num.Name = "Num";
			this.Num.ReadOnly = true;
			this.Num.Width = 39;
			// 
			// Line
			// 
			this.Line.DataPropertyName = "Text";
			this.Line.HeaderText = "Line";
			this.Line.Name = "Line";
			this.Line.ReadOnly = true;
			this.Line.Width = 52;
			// 
			// frmLineHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 640);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmLineHistory";
			this.ShowIcon = false;
			this.Text = "Line History";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCompareBranches_FormClosing);
			this.Load += new System.EventHandler(this.frmLineHistory_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).EndInit();
			this.mnuDetails.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridLines)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridCommits;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ucDifferences ucDifferences;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ContextMenuStrip mnuDetails;
		private System.Windows.Forms.ToolStripMenuItem externalCompareToolStripMenuItem;
		private System.Windows.Forms.DataGridView gridLines;
		private System.Windows.Forms.DataGridViewTextBoxColumn Num;
		private System.Windows.Forms.DataGridViewTextBoxColumn Line;
	}
}