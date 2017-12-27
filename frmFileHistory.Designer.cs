namespace PaJaMa.GitStudio
{
	partial class frmFileHistory
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
			this.mnuDetails = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.externalCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.txtDifferences = new System.Windows.Forms.TextBox();
			this.tvFiles = new PaJaMa.WinControls.MultiSelectTreeView();
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.mnuDetails.SuspendLayout();
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
			this.gridCommits.Size = new System.Drawing.Size(363, 640);
			this.gridCommits.TabIndex = 0;
			this.gridCommits.SelectionChanged += new System.EventHandler(this.gridCommits_SelectionChanged);
			this.gridCommits.DoubleClick += new System.EventHandler(this.gridCommits_DoubleClick);
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
			this.splitContainer2.Panel1.Controls.Add(this.tvFiles);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.gridCommits);
			this.splitContainer2.Size = new System.Drawing.Size(787, 640);
			this.splitContainer2.SplitterDistance = 420;
			this.splitContainer2.TabIndex = 4;
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
			this.txtDifferences.Size = new System.Drawing.Size(269, 640);
			this.txtDifferences.TabIndex = 0;
			// 
			// tvFiles
			// 
			this.tvFiles.AllowDragNodes = false;
			this.tvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvFiles.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.tvFiles.Location = new System.Drawing.Point(0, 0);
			this.tvFiles.Name = "tvFiles";
			this.tvFiles.Size = new System.Drawing.Size(420, 640);
			this.tvFiles.TabIndex = 0;
			this.tvFiles.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvFiles_BeforeExpand);
			this.tvFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvFiles_AfterSelect);
			// 
			// frmFileHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 640);
			this.Controls.Add(this.splitContainer1);
			this.Name = "frmFileHistory";
			this.ShowIcon = false;
			this.Text = "File History";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCompareBranches_FormClosing);
			this.Load += new System.EventHandler(this.frmFileHistory_Load);
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
			this.mnuDetails.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridCommits;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtDifferences;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ContextMenuStrip mnuDetails;
		private System.Windows.Forms.ToolStripMenuItem externalCompareToolStripMenuItem;
		private WinControls.MultiSelectTreeView tvFiles;
	}
}