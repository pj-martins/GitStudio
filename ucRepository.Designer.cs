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
			this.pullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.branchLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mergeFromLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tvRemoteBranches = new System.Windows.Forms.TreeView();
			this.mnuRemote = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.branchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.fetchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mergeFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.splitContainer4 = new System.Windows.Forms.SplitContainer();
			this.tvDifferences = new System.Windows.Forms.TreeView();
			this.mnuDiffs = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.viewExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ignoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ignorePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.btnStage = new System.Windows.Forms.Button();
			this.tvStaged = new System.Windows.Forms.TreeView();
			this.btnUnStage = new System.Windows.Forms.Button();
			this.txtDiffText = new System.Windows.Forms.RichTextBox();
			this.timDiff = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnCommit = new System.Windows.Forms.Button();
			this.resolveConflictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
			this.splitContainer4.Panel1.SuspendLayout();
			this.splitContainer4.Panel2.SuspendLayout();
			this.splitContainer4.SuspendLayout();
			this.mnuDiffs.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.splitContainer1.Size = new System.Drawing.Size(909, 618);
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
			this.splitContainer2.Size = new System.Drawing.Size(303, 618);
			this.splitContainer2.SplitterDistance = 300;
			this.splitContainer2.TabIndex = 0;
			// 
			// tvLocalBranches
			// 
			this.tvLocalBranches.CheckBoxes = true;
			this.tvLocalBranches.ContextMenuStrip = this.mnuLocal;
			this.tvLocalBranches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvLocalBranches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.tvLocalBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tvLocalBranches.Location = new System.Drawing.Point(0, 0);
			this.tvLocalBranches.Name = "tvLocalBranches";
			this.tvLocalBranches.Size = new System.Drawing.Size(303, 300);
			this.tvLocalBranches.TabIndex = 2;
			this.tvLocalBranches.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvLocalBranches_DrawNode);
			// 
			// mnuLocal
			// 
			this.mnuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkoutToolStripMenuItem,
            this.pullToolStripMenuItem,
            this.pushToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.branchLocalToolStripMenuItem,
            this.mergeFromLocalToolStripMenuItem});
			this.mnuLocal.Name = "mnuLocal";
			this.mnuLocal.Size = new System.Drawing.Size(157, 136);
			this.mnuLocal.Opening += new System.ComponentModel.CancelEventHandler(this.mnuLocal_Opening);
			// 
			// checkoutToolStripMenuItem
			// 
			this.checkoutToolStripMenuItem.Name = "checkoutToolStripMenuItem";
			this.checkoutToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.checkoutToolStripMenuItem.Text = "&Checkout";
			this.checkoutToolStripMenuItem.Click += new System.EventHandler(this.checkoutToolStripMenuItem_Click);
			// 
			// pullToolStripMenuItem
			// 
			this.pullToolStripMenuItem.Name = "pullToolStripMenuItem";
			this.pullToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.pullToolStripMenuItem.Text = "&Pull";
			this.pullToolStripMenuItem.Click += new System.EventHandler(this.pullToolStripMenuItem_Click);
			// 
			// pushToolStripMenuItem
			// 
			this.pushToolStripMenuItem.Name = "pushToolStripMenuItem";
			this.pushToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.pushToolStripMenuItem.Text = "&Push";
			this.pushToolStripMenuItem.Click += new System.EventHandler(this.pushToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.deleteToolStripMenuItem.Text = "&Delete Checked";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// branchLocalToolStripMenuItem
			// 
			this.branchLocalToolStripMenuItem.Name = "branchLocalToolStripMenuItem";
			this.branchLocalToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.branchLocalToolStripMenuItem.Text = "&Branch";
			this.branchLocalToolStripMenuItem.Click += new System.EventHandler(this.branchLocalToolStripMenuItem_Click);
			// 
			// mergeFromLocalToolStripMenuItem
			// 
			this.mergeFromLocalToolStripMenuItem.Name = "mergeFromLocalToolStripMenuItem";
			this.mergeFromLocalToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
			this.mergeFromLocalToolStripMenuItem.Text = "&Merge From";
			this.mergeFromLocalToolStripMenuItem.Click += new System.EventHandler(this.mergeFromLocalToolStripMenuItem_Click);
			// 
			// tvRemoteBranches
			// 
			this.tvRemoteBranches.ContextMenuStrip = this.mnuRemote;
			this.tvRemoteBranches.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvRemoteBranches.Location = new System.Drawing.Point(0, 0);
			this.tvRemoteBranches.Name = "tvRemoteBranches";
			this.tvRemoteBranches.Size = new System.Drawing.Size(303, 314);
			this.tvRemoteBranches.TabIndex = 1;
			// 
			// mnuRemote
			// 
			this.mnuRemote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.branchToolStripMenuItem,
            this.fetchToolStripMenuItem,
            this.mergeFromToolStripMenuItem});
			this.mnuRemote.Name = "mnuRemote";
			this.mnuRemote.Size = new System.Drawing.Size(140, 70);
			// 
			// branchToolStripMenuItem
			// 
			this.branchToolStripMenuItem.Name = "branchToolStripMenuItem";
			this.branchToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.branchToolStripMenuItem.Text = "&Branch";
			this.branchToolStripMenuItem.Click += new System.EventHandler(this.branchToolStripMenuItem_Click);
			// 
			// fetchToolStripMenuItem
			// 
			this.fetchToolStripMenuItem.Name = "fetchToolStripMenuItem";
			this.fetchToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.fetchToolStripMenuItem.Text = "&Fetch";
			this.fetchToolStripMenuItem.Click += new System.EventHandler(this.fetchToolStripMenuItem_Click);
			// 
			// mergeFromToolStripMenuItem
			// 
			this.mergeFromToolStripMenuItem.Name = "mergeFromToolStripMenuItem";
			this.mergeFromToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
			this.mergeFromToolStripMenuItem.Text = "&Merge From";
			this.mergeFromToolStripMenuItem.Click += new System.EventHandler(this.mergeFromToolStripMenuItem_Click);
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
			this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.txtDiffText);
			this.splitContainer3.Size = new System.Drawing.Size(602, 618);
			this.splitContainer3.SplitterDistance = 336;
			this.splitContainer3.TabIndex = 0;
			// 
			// splitContainer4
			// 
			this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer4.Location = new System.Drawing.Point(0, 0);
			this.splitContainer4.Name = "splitContainer4";
			// 
			// splitContainer4.Panel1
			// 
			this.splitContainer4.Panel1.Controls.Add(this.tvDifferences);
			this.splitContainer4.Panel1.Controls.Add(this.btnStage);
			// 
			// splitContainer4.Panel2
			// 
			this.splitContainer4.Panel2.Controls.Add(this.tvStaged);
			this.splitContainer4.Panel2.Controls.Add(this.btnUnStage);
			this.splitContainer4.Size = new System.Drawing.Size(602, 336);
			this.splitContainer4.SplitterDistance = 283;
			this.splitContainer4.TabIndex = 1;
			// 
			// tvDifferences
			// 
			this.tvDifferences.CheckBoxes = true;
			this.tvDifferences.ContextMenuStrip = this.mnuDiffs;
			this.tvDifferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvDifferences.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.tvDifferences.FullRowSelect = true;
			this.tvDifferences.Indent = 10;
			this.tvDifferences.Location = new System.Drawing.Point(0, 0);
			this.tvDifferences.Name = "tvDifferences";
			this.tvDifferences.ShowLines = false;
			this.tvDifferences.Size = new System.Drawing.Size(283, 313);
			this.tvDifferences.TabIndex = 0;
			this.tvDifferences.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
			this.tvDifferences.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCollapse);
			this.tvDifferences.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterExpand);
			this.tvDifferences.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tv_DrawNode);
			this.tvDifferences.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// mnuDiffs
			// 
			this.mnuDiffs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewExternalToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.ignoreToolStripMenuItem,
            this.ignorePathToolStripMenuItem,
            this.selectAllToolStripMenuItem,
            this.resolveConflictToolStripMenuItem});
			this.mnuDiffs.Name = "mnuDiffs";
			this.mnuDiffs.Size = new System.Drawing.Size(160, 158);
			this.mnuDiffs.Opening += new System.ComponentModel.CancelEventHandler(this.mnuDiffs_Opening);
			// 
			// viewExternalToolStripMenuItem
			// 
			this.viewExternalToolStripMenuItem.Name = "viewExternalToolStripMenuItem";
			this.viewExternalToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.viewExternalToolStripMenuItem.Text = "&View External";
			this.viewExternalToolStripMenuItem.Click += new System.EventHandler(this.viewExternalToolStripMenuItem_Click);
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.undoToolStripMenuItem.Text = "&Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
			// 
			// ignoreToolStripMenuItem
			// 
			this.ignoreToolStripMenuItem.Name = "ignoreToolStripMenuItem";
			this.ignoreToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.ignoreToolStripMenuItem.Text = "I&gnore";
			this.ignoreToolStripMenuItem.Click += new System.EventHandler(this.ignoreToolStripMenuItem_Click);
			// 
			// ignorePathToolStripMenuItem
			// 
			this.ignorePathToolStripMenuItem.Name = "ignorePathToolStripMenuItem";
			this.ignorePathToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.ignorePathToolStripMenuItem.Text = "Ignore &Path";
			this.ignorePathToolStripMenuItem.Click += new System.EventHandler(this.ignorePathToolStripMenuItem_Click);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// btnStage
			// 
			this.btnStage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnStage.Location = new System.Drawing.Point(0, 313);
			this.btnStage.Name = "btnStage";
			this.btnStage.Size = new System.Drawing.Size(283, 23);
			this.btnStage.TabIndex = 1;
			this.btnStage.Text = ">>";
			this.btnStage.UseVisualStyleBackColor = true;
			this.btnStage.Click += new System.EventHandler(this.btnStage_Click);
			// 
			// tvStaged
			// 
			this.tvStaged.CheckBoxes = true;
			this.tvStaged.ContextMenuStrip = this.mnuDiffs;
			this.tvStaged.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvStaged.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
			this.tvStaged.FullRowSelect = true;
			this.tvStaged.Indent = 10;
			this.tvStaged.Location = new System.Drawing.Point(0, 0);
			this.tvStaged.Name = "tvStaged";
			this.tvStaged.ShowLines = false;
			this.tvStaged.Size = new System.Drawing.Size(315, 313);
			this.tvStaged.TabIndex = 2;
			this.tvStaged.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
			this.tvStaged.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCollapse);
			this.tvStaged.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tv_DrawNode);
			this.tvStaged.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
			// 
			// btnUnStage
			// 
			this.btnUnStage.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnUnStage.Location = new System.Drawing.Point(0, 313);
			this.btnUnStage.Name = "btnUnStage";
			this.btnUnStage.Size = new System.Drawing.Size(315, 23);
			this.btnUnStage.TabIndex = 3;
			this.btnUnStage.Text = "<<";
			this.btnUnStage.UseVisualStyleBackColor = true;
			this.btnUnStage.Click += new System.EventHandler(this.btnUnStage_Click);
			// 
			// txtDiffText
			// 
			this.txtDiffText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDiffText.Location = new System.Drawing.Point(0, 0);
			this.txtDiffText.Name = "txtDiffText";
			this.txtDiffText.ReadOnly = true;
			this.txtDiffText.Size = new System.Drawing.Size(602, 278);
			this.txtDiffText.TabIndex = 0;
			this.txtDiffText.Text = "";
			// 
			// timDiff
			// 
			this.timDiff.Interval = 5000;
			this.timDiff.Tick += new System.EventHandler(this.timDiff_Tick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnCommit);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 618);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(909, 34);
			this.panel1.TabIndex = 4;
			// 
			// btnCommit
			// 
			this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCommit.Enabled = false;
			this.btnCommit.Location = new System.Drawing.Point(787, 6);
			this.btnCommit.Name = "btnCommit";
			this.btnCommit.Size = new System.Drawing.Size(119, 23);
			this.btnCommit.TabIndex = 1;
			this.btnCommit.Text = "Commit";
			this.btnCommit.UseVisualStyleBackColor = true;
			this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
			// 
			// resolveConflictToolStripMenuItem
			// 
			this.resolveConflictToolStripMenuItem.Name = "resolveConflictToolStripMenuItem";
			this.resolveConflictToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
			this.resolveConflictToolStripMenuItem.Text = "Resolve &Conflict";
			this.resolveConflictToolStripMenuItem.Click += new System.EventHandler(this.resolveConflictToolStripMenuItem_Click);
			// 
			// ucRepository
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel1);
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
			this.splitContainer4.Panel1.ResumeLayout(false);
			this.splitContainer4.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
			this.splitContainer4.ResumeLayout(false);
			this.mnuDiffs.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
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
		private System.Windows.Forms.TreeView tvDifferences;
		private System.Windows.Forms.RichTextBox txtDiffText;
		private System.Windows.Forms.ContextMenuStrip mnuDiffs;
		private System.Windows.Forms.ToolStripMenuItem viewExternalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pullToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private System.Windows.Forms.Button btnStage;
		private System.Windows.Forms.TreeView tvStaged;
		private System.Windows.Forms.Button btnUnStage;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.ToolStripMenuItem pushToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ignoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ignorePathToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem branchLocalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mergeFromLocalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mergeFromToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resolveConflictToolStripMenuItem;
	}
}