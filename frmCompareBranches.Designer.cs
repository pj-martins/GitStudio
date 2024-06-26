﻿namespace PaJaMa.GitStudio
{
	partial class frmCompareBranches
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
			this.gridMain = new System.Windows.Forms.DataGridView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblDirection = new System.Windows.Forms.Label();
			this.btnSwitch = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ucDifferences = new ucDifferences();
			this.mnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.externalCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.gridMain)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.mnuGrid.SuspendLayout();
			this.SuspendLayout();
			// 
			// gridMain
			// 
			this.gridMain.AllowUserToAddRows = false;
			this.gridMain.AllowUserToDeleteRows = false;
			this.gridMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridMain.ContextMenuStrip = this.mnuGrid;
			this.gridMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridMain.Location = new System.Drawing.Point(0, 0);
			this.gridMain.Name = "gridMain";
			this.gridMain.ReadOnly = true;
			this.gridMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridMain.Size = new System.Drawing.Size(373, 505);
			this.gridMain.TabIndex = 0;
			this.gridMain.SelectionChanged += new System.EventHandler(this.gridMain_SelectionChanged);
			this.gridMain.DoubleClick += new System.EventHandler(this.gridMain_DoubleClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lblDirection);
			this.panel2.Controls.Add(this.btnSwitch);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 505);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(740, 31);
			this.panel2.TabIndex = 2;
			// 
			// lblDirection
			// 
			this.lblDirection.AutoSize = true;
			this.lblDirection.Location = new System.Drawing.Point(12, 10);
			this.lblDirection.Name = "lblDirection";
			this.lblDirection.Size = new System.Drawing.Size(35, 13);
			this.lblDirection.TabIndex = 2;
			this.lblDirection.Text = "label1";
			// 
			// btnSwitch
			// 
			this.btnSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSwitch.Location = new System.Drawing.Point(662, 3);
			this.btnSwitch.Name = "btnSwitch";
			this.btnSwitch.Size = new System.Drawing.Size(75, 23);
			this.btnSwitch.TabIndex = 1;
			this.btnSwitch.Text = "Switch";
			this.btnSwitch.UseVisualStyleBackColor = true;
			this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.gridMain);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ucDifferences);
			this.splitContainer1.Size = new System.Drawing.Size(740, 505);
			this.splitContainer1.SplitterDistance = 373;
			this.splitContainer1.TabIndex = 3;
			// 
			// ucDifferences
			// 
			this.ucDifferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDifferences.Location = new System.Drawing.Point(0, 0);
			this.ucDifferences.Name = "ucDifferences";
			this.ucDifferences.Size = new System.Drawing.Size(363, 505);
			this.ucDifferences.TabIndex = 0;
			// 
			// mnuGrid
			// 
			this.mnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.externalCompareToolStripMenuItem});
			this.mnuGrid.Name = "mnuDetails";
			this.mnuGrid.Size = new System.Drawing.Size(168, 48);
			// 
			// externalCompareToolStripMenuItem
			// 
			this.externalCompareToolStripMenuItem.Name = "externalCompareToolStripMenuItem";
			this.externalCompareToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.externalCompareToolStripMenuItem.Text = "&External Compare";
			this.externalCompareToolStripMenuItem.Click += new System.EventHandler(this.externalCompareToolStripMenuItem_Click);
			// 
			// frmCompareBranches
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(740, 536);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel2);
			this.Name = "frmCompareBranches";
			this.ShowIcon = false;
			this.Text = "Compare Branches";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCompareBranches_FormClosing);
			this.Load += new System.EventHandler(this.frmCompareBranches_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridMain)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.mnuGrid.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridMain;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnSwitch;
		private System.Windows.Forms.Label lblDirection;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ucDifferences ucDifferences;
		private System.Windows.Forms.ContextMenuStrip mnuGrid;
		private System.Windows.Forms.ToolStripMenuItem externalCompareToolStripMenuItem;
	}
}