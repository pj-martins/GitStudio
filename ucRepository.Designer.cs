namespace PaJaMa.GitStudio
{
	partial class ucRepository
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.tvLocalBranches = new System.Windows.Forms.TreeView();
			this.mnuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.checkoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tvRemoteBranches = new System.Windows.Forms.TreeView();
			this.mnuRemote = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.branchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fetchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.timDiff = new System.Windows.Forms.Timer(this.components);
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.lstDifferences = new System.Windows.Forms.ListBox();
			this.txtDiffText = new System.Windows.Forms.RichTextBox();
			this.mnuDiffs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.mnuLocal.SuspendLayout();
			this.mnuRemote.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.mnuDiffs.SuspendLayout();
			this.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(909, 652);
			this.splitContainer1.SplitterDistance = 303;
			this.splitContainer1.TabIndex = 0;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.tvLocalBranches);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.tvRemoteBranches);
			this.splitContainer2.Size = new System.Drawing.Size(303, 652);
			this.splitContainer2.SplitterDistance = 317;
			this.splitContainer2.TabIndex = 0;
			// 
			// tvLocalBranches
			// 
			this.tvLocalBranches.ContextMenuStrip = this.mnuLocal;
			this.tvLocalBranches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvLocalBranches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.tvLocalBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvLocalBranches.Location = new System.Drawing.Point(0, 0);
			this.tvLocalBranches.Name = "tvLocalBranches";
			this.tvLocalBranches.Size = new System.Drawing.Size(303, 317);
			this.tvLocalBranches.TabIndex = 2;
			this.tvLocalBranches.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvLocalBranches_DrawNode);
			// 
			// mnuLocal
			// 
			this.mnuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkoutToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.mnuLocal.Name = "mnuLocal";
			this.mnuLocal.Size = new System.Drawing.Size(126, 48);
			this.mnuLocal.Opening += new System.ComponentModel.CancelEventHandler(this.mnuLocal_Opening);
			// 
			// checkoutToolStripMenuItem
			// 
			this.checkoutToolStripMenuItem.Name = "checkoutToolStripMenuItem";
			this.checkoutToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.checkoutToolStripMenuItem.Text = "&Checkout";
			this.checkoutToolStripMenuItem.Click += new System.EventHandler(this.checkoutToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.deleteToolStripMenuItem.Text = "&Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// tvRemoteBranches
			// 
			this.tvRemoteBranches.ContextMenuStrip = this.mnuRemote;
			this.tvRemoteBranches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvRemoteBranches.Location = new System.Drawing.Point(0, 0);
			this.tvRemoteBranches.Name = "tvRemoteBranches";
			this.tvRemoteBranches.Size = new System.Drawing.Size(303, 331);
			this.tvRemoteBranches.TabIndex = 1;
			// 
			// mnuRemote
			// 
			this.mnuRemote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.branchToolStripMenuItem,
            this.fetchToolStripMenuItem});
			this.mnuRemote.Name = "mnuRemote";
			this.mnuRemote.Size = new System.Drawing.Size(126, 48);
			// 
			// branchToolStripMenuItem
			// 
			this.branchToolStripMenuItem.Name = "branchToolStripMenuItem";
			this.branchToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.branchToolStripMenuItem.Text = "&Checkout";
			this.branchToolStripMenuItem.Click += new System.EventHandler(this.branchToolStripMenuItem_Click);
			// 
			// fetchToolStripMenuItem
			// 
			this.fetchToolStripMenuItem.Name = "fetchToolStripMenuItem";
			this.fetchToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.fetchToolStripMenuItem.Text = "&Fetch";
			this.fetchToolStripMenuItem.Click += new System.EventHandler(this.fetchToolStripMenuItem_Click);
			// 
			// timDiff
			// 
			this.timDiff.Interval = 5000;
			this.timDiff.Tick += new System.EventHandler(this.timDiff_Tick);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.lstDifferences);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.txtDiffText);
			this.splitContainer3.Size = new System.Drawing.Size(602, 652);
			this.splitContainer3.SplitterDistance = 355;
			this.splitContainer3.TabIndex = 0;
			// 
			// lstDifferences
			// 
			this.lstDifferences.ContextMenuStrip = this.mnuDiffs;
			this.lstDifferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstDifferences.FormattingEnabled = true;
			this.lstDifferences.Location = new System.Drawing.Point(0, 0);
			this.lstDifferences.Name = "lstDifferences";
			this.lstDifferences.Size = new System.Drawing.Size(602, 355);
			this.lstDifferences.TabIndex = 0;
			this.lstDifferences.SelectedIndexChanged += new System.EventHandler(this.lstDifferences_SelectedIndexChanged);
			// 
			// txtDiffText
			// 
			this.txtDiffText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDiffText.Location = new System.Drawing.Point(0, 0);
			this.txtDiffText.Name = "txtDiffText";
			this.txtDiffText.Size = new System.Drawing.Size(602, 293);
			this.txtDiffText.TabIndex = 0;
			this.txtDiffText.Text = "";
			// 
			// mnuDiffs
			// 
			this.mnuDiffs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewExternalToolStripMenuItem});
			this.mnuDiffs.Name = "mnuDiffs";
			this.mnuDiffs.Size = new System.Drawing.Size(153, 48);
			// 
			// viewExternalToolStripMenuItem
			// 
			this.viewExternalToolStripMenuItem.Name = "viewExternalToolStripMenuItem";
			this.viewExternalToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.viewExternalToolStripMenuItem.Text = "&View External";
			this.viewExternalToolStripMenuItem.Click += new System.EventHandler(this.viewExternalToolStripMenuItem_Click);
			// 
			// ucRepository
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Name = "ucRepository";
			this.Size = new System.Drawing.Size(909, 652);
			this.Load += new System.EventHandler(this.ucRepository_Load);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.mnuLocal.ResumeLayout(false);
			this.mnuRemote.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.mnuDiffs.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.TreeView tvRemoteBranches;
		private System.Windows.Forms.ContextMenuStrip mnuRemote;
		private System.Windows.Forms.ToolStripMenuItem branchToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip mnuLocal;
		private System.Windows.Forms.ToolStripMenuItem checkoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fetchToolStripMenuItem;
		private System.Windows.Forms.TreeView tvLocalBranches;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.Timer timDiff;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.ListBox lstDifferences;
		private System.Windows.Forms.RichTextBox txtDiffText;
		private System.Windows.Forms.ContextMenuStrip mnuDiffs;
		private System.Windows.Forms.ToolStripMenuItem viewExternalToolStripMenuItem;
	}
}