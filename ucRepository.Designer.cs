﻿using System.Drawing;

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
            this.tvLocalBranches = new PaJaMa.WinControls.MultiSelectTreeView();
            this.mnuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.branchLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeFromLocalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pullAndMergeFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebaseFromToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.abortMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.concludeMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortRebaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileHistoryToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.trackRemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkoutForceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tvRemoteBranches = new PaJaMa.WinControls.MultiSelectTreeView();
            this.mnuRemote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.branchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fetchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pullIntoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pruneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mergeFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rebaseFromToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteRemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.tvUnStaged = new PaJaMa.WinControls.MultiSelectTreeView();
            this.mnuDiffs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewExternalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolveConflictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolveUsingMineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resolveUsingTheirsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nextConflictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stageAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unstageAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.tvStaged = new PaJaMa.WinControls.MultiSelectTreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.chkIgnoreWhiteSpace = new System.Windows.Forms.CheckBox();
            this.ucDiff = new ucDifferences();
            this.progMain = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnPush = new System.Windows.Forms.Button();
            this.btnPull = new System.Windows.Forms.Button();
            this.btnViewStashes = new System.Windows.Forms.Button();
            this.btnStash = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnCommit = new System.Windows.Forms.Button();
            this.timDebounce = new System.Windows.Forms.Timer(this.components);
            this.mergeFromSquashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.pnlButtons.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1163, 627);
            this.splitContainer1.SplitterDistance = 386;
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
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tvRemoteBranches);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(386, 627);
            this.splitContainer2.SplitterDistance = 304;
            this.splitContainer2.TabIndex = 0;
            // 
            // tvLocalBranches
            // 
            this.tvLocalBranches.AllowDragNodes = false;
            this.tvLocalBranches.ContextMenuStrip = this.mnuLocal;
            this.tvLocalBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvLocalBranches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvLocalBranches.Location = new System.Drawing.Point(0, 17);
            this.tvLocalBranches.Name = "tvLocalBranches";
            this.tvLocalBranches.Size = new System.Drawing.Size(386, 287);
            this.tvLocalBranches.TabIndex = 2;
            this.tvLocalBranches.DoubleClick += new System.EventHandler(this.tvLocalBranches_DoubleClick);
            // 
            // mnuLocal
            // 
            this.mnuLocal.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkoutToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.branchLocalToolStripMenuItem,
            this.mergeFromLocalToolStripMenuItem,
            this.mergeFromSquashToolStripMenuItem,
            this.pullAndMergeFromToolStripMenuItem,
            this.rebaseFromToolStripMenuItem1,
            this.abortMergeToolStripMenuItem,
            this.concludeMergeToolStripMenuItem,
            this.abortRebaseToolStripMenuItem,
            this.compareToolStripMenuItem,
            this.historyToolStripMenuItem1,
            this.renameToolStripMenuItem,
            this.fileHistoryToolStripMenuItem1,
            this.trackRemoteToolStripMenuItem,
            this.checkoutForceToolStripMenuItem});
            this.mnuLocal.Name = "mnuLocal";
            this.mnuLocal.Size = new System.Drawing.Size(188, 334);
            this.mnuLocal.Opening += new System.ComponentModel.CancelEventHandler(this.mnuLocal_Opening);
            // 
            // checkoutToolStripMenuItem
            // 
            this.checkoutToolStripMenuItem.Name = "checkoutToolStripMenuItem";
            this.checkoutToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.checkoutToolStripMenuItem.Text = "&Checkout";
            this.checkoutToolStripMenuItem.Click += new System.EventHandler(this.checkoutToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.deleteToolStripMenuItem.Text = "&Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // branchLocalToolStripMenuItem
            // 
            this.branchLocalToolStripMenuItem.Name = "branchLocalToolStripMenuItem";
            this.branchLocalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.branchLocalToolStripMenuItem.Text = "&Branch";
            this.branchLocalToolStripMenuItem.Click += new System.EventHandler(this.branchLocalToolStripMenuItem_Click);
            // 
            // mergeFromLocalToolStripMenuItem
            // 
            this.mergeFromLocalToolStripMenuItem.Name = "mergeFromLocalToolStripMenuItem";
            this.mergeFromLocalToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.mergeFromLocalToolStripMenuItem.Text = "&Merge From";
            this.mergeFromLocalToolStripMenuItem.Click += new System.EventHandler(this.mergeFromLocalToolStripMenuItem_Click);
            // 
            // pullAndMergeFromToolStripMenuItem
            // 
            this.pullAndMergeFromToolStripMenuItem.Name = "pullAndMergeFromToolStripMenuItem";
            this.pullAndMergeFromToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.pullAndMergeFromToolStripMenuItem.Text = "&Pull And Merge From";
            this.pullAndMergeFromToolStripMenuItem.Click += new System.EventHandler(this.pullAndMergeFromToolStripMenuItem_Click);
            // 
            // rebaseFromToolStripMenuItem1
            // 
            this.rebaseFromToolStripMenuItem1.Name = "rebaseFromToolStripMenuItem1";
            this.rebaseFromToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.rebaseFromToolStripMenuItem1.Text = "&Rebase From";
            this.rebaseFromToolStripMenuItem1.Click += new System.EventHandler(this.rebaseFromLocalToolStripMenuItem_Click);
            // 
            // abortMergeToolStripMenuItem
            // 
            this.abortMergeToolStripMenuItem.Name = "abortMergeToolStripMenuItem";
            this.abortMergeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.abortMergeToolStripMenuItem.Text = "&Abort Merge";
            this.abortMergeToolStripMenuItem.Click += new System.EventHandler(this.abortMergeToolStripMenuItem_Click);
            // 
            // concludeMergeToolStripMenuItem
            // 
            this.concludeMergeToolStripMenuItem.Name = "concludeMergeToolStripMenuItem";
            this.concludeMergeToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.concludeMergeToolStripMenuItem.Text = "&Conclude Merge";
            this.concludeMergeToolStripMenuItem.Click += new System.EventHandler(this.concludeMergeToolStripMenuItem_Click);
            // 
            // abortRebaseToolStripMenuItem
            // 
            this.abortRebaseToolStripMenuItem.Name = "abortRebaseToolStripMenuItem";
            this.abortRebaseToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.abortRebaseToolStripMenuItem.Text = "A&bort Rebase";
            this.abortRebaseToolStripMenuItem.Click += new System.EventHandler(this.abortRebaseToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            this.compareToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.compareToolStripMenuItem.Text = "&Compare";
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem1
            // 
            this.historyToolStripMenuItem1.Name = "historyToolStripMenuItem1";
            this.historyToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.historyToolStripMenuItem1.Text = "&History";
            this.historyToolStripMenuItem1.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.renameToolStripMenuItem.Text = "&Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // fileHistoryToolStripMenuItem1
            // 
            this.fileHistoryToolStripMenuItem1.Name = "fileHistoryToolStripMenuItem1";
            this.fileHistoryToolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
            this.fileHistoryToolStripMenuItem1.Text = "&File History";
            this.fileHistoryToolStripMenuItem1.Click += new System.EventHandler(this.fileHistoryToolStripMenuItem1_Click);
            // 
            // trackRemoteToolStripMenuItem
            // 
            this.trackRemoteToolStripMenuItem.Name = "trackRemoteToolStripMenuItem";
            this.trackRemoteToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.trackRemoteToolStripMenuItem.Text = "&Track Remote";
            this.trackRemoteToolStripMenuItem.Click += new System.EventHandler(this.trackRemoteToolStripMenuItem_Click);
            // 
            // checkoutForceToolStripMenuItem
            // 
            this.checkoutForceToolStripMenuItem.Name = "checkoutForceToolStripMenuItem";
            this.checkoutForceToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.checkoutForceToolStripMenuItem.Text = "Checkout F&orce";
            this.checkoutForceToolStripMenuItem.Click += new System.EventHandler(this.CheckoutForceToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(2);
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Local Branches";
            // 
            // tvRemoteBranches
            // 
            this.tvRemoteBranches.AllowDragNodes = false;
            this.tvRemoteBranches.ContextMenuStrip = this.mnuRemote;
            this.tvRemoteBranches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvRemoteBranches.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvRemoteBranches.Location = new System.Drawing.Point(0, 17);
            this.tvRemoteBranches.Name = "tvRemoteBranches";
            this.tvRemoteBranches.Size = new System.Drawing.Size(386, 302);
            this.tvRemoteBranches.TabIndex = 1;
            // 
            // mnuRemote
            // 
            this.mnuRemote.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuRemote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.branchToolStripMenuItem,
            this.fetchToolStripMenuItem,
            this.pullIntoToolStripMenuItem,
            this.pruneToolStripMenuItem,
            this.mergeFromToolStripMenuItem,
            this.rebaseFromToolStripMenuItem,
            this.deleteRemoteToolStripMenuItem,
            this.compareToolStripMenuItem1,
            this.historyToolStripMenuItem,
            this.downloadToToolStripMenuItem});
            this.mnuRemote.Name = "mnuRemote";
            this.mnuRemote.Size = new System.Drawing.Size(162, 224);
            this.mnuRemote.Opening += new System.ComponentModel.CancelEventHandler(this.mnuRemote_Opening);
            // 
            // branchToolStripMenuItem
            // 
            this.branchToolStripMenuItem.Name = "branchToolStripMenuItem";
            this.branchToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.branchToolStripMenuItem.Text = "&Branch";
            this.branchToolStripMenuItem.Click += new System.EventHandler(this.branchToolStripMenuItem_Click);
            // 
            // fetchToolStripMenuItem
            // 
            this.fetchToolStripMenuItem.Name = "fetchToolStripMenuItem";
            this.fetchToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.fetchToolStripMenuItem.Text = "&Fetch";
            this.fetchToolStripMenuItem.Click += new System.EventHandler(this.fetchToolStripMenuItem_Click);
            // 
            // pullIntoToolStripMenuItem
            // 
            this.pullIntoToolStripMenuItem.Name = "pullIntoToolStripMenuItem";
            this.pullIntoToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.pullIntoToolStripMenuItem.Text = "&Pull Into Current";
            this.pullIntoToolStripMenuItem.Click += new System.EventHandler(this.pullIntoToolStripMenuItem_Click);
            // 
            // pruneToolStripMenuItem
            // 
            this.pruneToolStripMenuItem.Name = "pruneToolStripMenuItem";
            this.pruneToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.pruneToolStripMenuItem.Text = "&Prune";
            this.pruneToolStripMenuItem.Click += new System.EventHandler(this.pruneToolStripMenuItem_Click);
            // 
            // mergeFromToolStripMenuItem
            // 
            this.mergeFromToolStripMenuItem.Name = "mergeFromToolStripMenuItem";
            this.mergeFromToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.mergeFromToolStripMenuItem.Text = "&Merge From";
            this.mergeFromToolStripMenuItem.Click += new System.EventHandler(this.mergeFromToolStripMenuItem_Click);
            // 
            // rebaseFromToolStripMenuItem
            // 
            this.rebaseFromToolStripMenuItem.Name = "rebaseFromToolStripMenuItem";
            this.rebaseFromToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.rebaseFromToolStripMenuItem.Text = "&Rebase From";
            this.rebaseFromToolStripMenuItem.Click += new System.EventHandler(this.rebaseFromToolStripMenuItem_Click);
            // 
            // deleteRemoteToolStripMenuItem
            // 
            this.deleteRemoteToolStripMenuItem.Name = "deleteRemoteToolStripMenuItem";
            this.deleteRemoteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.deleteRemoteToolStripMenuItem.Text = "&Delete";
            this.deleteRemoteToolStripMenuItem.Click += new System.EventHandler(this.deleteRemoteToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem1
            // 
            this.compareToolStripMenuItem1.Name = "compareToolStripMenuItem1";
            this.compareToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.compareToolStripMenuItem1.Text = "&Compare";
            this.compareToolStripMenuItem1.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.historyToolStripMenuItem.Text = "&History";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // downloadToToolStripMenuItem
            // 
            this.downloadToToolStripMenuItem.Name = "downloadToToolStripMenuItem";
            this.downloadToToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.downloadToToolStripMenuItem.Text = "D&ownload To";
            this.downloadToToolStripMenuItem.Click += new System.EventHandler(this.downloadToToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(2);
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Remote Branches";
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
            this.splitContainer3.Panel2.Controls.Add(this.ucDiff);
            this.splitContainer3.Panel2.Controls.Add(this.label5);
            this.splitContainer3.Size = new System.Drawing.Size(773, 627);
            this.splitContainer3.SplitterDistance = 340;
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
            this.splitContainer4.Panel1.Controls.Add(this.tvUnStaged);
            this.splitContainer4.Panel1.Controls.Add(this.label3);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.tvStaged);
            this.splitContainer4.Panel2.Controls.Add(this.label4);
            this.splitContainer4.Size = new System.Drawing.Size(773, 340);
            this.splitContainer4.SplitterDistance = 362;
            this.splitContainer4.TabIndex = 1;
            // 
            // tvUnStaged
            // 
            this.tvUnStaged.AllowDragNodes = true;
            this.tvUnStaged.AllowDrop = true;
            this.tvUnStaged.ContextMenuStrip = this.mnuDiffs;
            this.tvUnStaged.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvUnStaged.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvUnStaged.Indent = 10;
            this.tvUnStaged.Location = new System.Drawing.Point(0, 17);
            this.tvUnStaged.Name = "tvUnStaged";
            this.tvUnStaged.Size = new System.Drawing.Size(362, 323);
            this.tvUnStaged.TabIndex = 0;
            this.tvUnStaged.NodesDrag += new System.Windows.Forms.DragEventHandler(this.tv_NodesDrag);
            this.tvUnStaged.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCollapse);
            this.tvUnStaged.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterExpand);
            this.tvUnStaged.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tvUnStaged.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
            this.tvUnStaged.DragEnter += new System.Windows.Forms.DragEventHandler(this.tv_DragEnter);
            this.tvUnStaged.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
            // 
            // mnuDiffs
            // 
            this.mnuDiffs.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnuDiffs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewExternalToolStripMenuItem,
            this.undoToolStripMenuItem,
            this.ignoreToolStripMenuItem,
            this.ignoreExtensionToolStripMenuItem,
            this.resolveConflictToolStripMenuItem,
            this.resolveUsingMineToolStripMenuItem,
            this.resolveUsingTheirsToolStripMenuItem,
            this.nextConflictToolStripMenuItem,
            this.stageToolStripMenuItem,
            this.unStageToolStripMenuItem,
            this.stageAllToolStripMenuItem,
            this.unstageAllToolStripMenuItem,
            this.fileHistoryToolStripMenuItem});
            this.mnuDiffs.Name = "mnuDiffs";
            this.mnuDiffs.Size = new System.Drawing.Size(182, 290);
            this.mnuDiffs.Opening += new System.ComponentModel.CancelEventHandler(this.mnuDiffs_Opening);
            // 
            // viewExternalToolStripMenuItem
            // 
            this.viewExternalToolStripMenuItem.Name = "viewExternalToolStripMenuItem";
            this.viewExternalToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.viewExternalToolStripMenuItem.Text = "&View External";
            this.viewExternalToolStripMenuItem.Click += new System.EventHandler(this.viewExternalToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // ignoreToolStripMenuItem
            // 
            this.ignoreToolStripMenuItem.Name = "ignoreToolStripMenuItem";
            this.ignoreToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ignoreToolStripMenuItem.Text = "I&gnore";
            this.ignoreToolStripMenuItem.Click += new System.EventHandler(this.ignoreToolStripMenuItem_Click);
            // 
            // ignoreExtensionToolStripMenuItem
            // 
            this.ignoreExtensionToolStripMenuItem.Name = "ignoreExtensionToolStripMenuItem";
            this.ignoreExtensionToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.ignoreExtensionToolStripMenuItem.Text = "Ignore &Extension";
            this.ignoreExtensionToolStripMenuItem.Click += new System.EventHandler(this.ignoreExtensionToolStripMenuItem_Click);
            // 
            // resolveConflictToolStripMenuItem
            // 
            this.resolveConflictToolStripMenuItem.Name = "resolveConflictToolStripMenuItem";
            this.resolveConflictToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.resolveConflictToolStripMenuItem.Text = "Resolve &Conflict";
            this.resolveConflictToolStripMenuItem.Click += new System.EventHandler(this.resolveConflictToolStripMenuItem_Click);
            // 
            // resolveUsingMineToolStripMenuItem
            // 
            this.resolveUsingMineToolStripMenuItem.Name = "resolveUsingMineToolStripMenuItem";
            this.resolveUsingMineToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.resolveUsingMineToolStripMenuItem.Text = "Resolve Using &Mine";
            this.resolveUsingMineToolStripMenuItem.Click += new System.EventHandler(this.resolveUsingMineToolStripMenuItem_Click);
            // 
            // resolveUsingTheirsToolStripMenuItem
            // 
            this.resolveUsingTheirsToolStripMenuItem.Name = "resolveUsingTheirsToolStripMenuItem";
            this.resolveUsingTheirsToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.resolveUsingTheirsToolStripMenuItem.Text = "Resolve Using &Theirs";
            this.resolveUsingTheirsToolStripMenuItem.Click += new System.EventHandler(this.resolveUsingTheirsToolStripMenuItem_Click);
            // 
            // nextConflictToolStripMenuItem
            // 
            this.nextConflictToolStripMenuItem.Name = "nextConflictToolStripMenuItem";
            this.nextConflictToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.nextConflictToolStripMenuItem.Text = "&Next Conflict";
            this.nextConflictToolStripMenuItem.Click += new System.EventHandler(this.NextConflictToolStripMenuItem_Click);
            // 
            // stageToolStripMenuItem
            // 
            this.stageToolStripMenuItem.Name = "stageToolStripMenuItem";
            this.stageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.stageToolStripMenuItem.Text = "&Stage";
            this.stageToolStripMenuItem.Click += new System.EventHandler(this.stageToolStripMenuItem_Click);
            // 
            // unStageToolStripMenuItem
            // 
            this.unStageToolStripMenuItem.Name = "unStageToolStripMenuItem";
            this.unStageToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.unStageToolStripMenuItem.Text = "&Unstage";
            this.unStageToolStripMenuItem.Click += new System.EventHandler(this.unStageToolStripMenuItem_Click);
            // 
            // stageAllToolStripMenuItem
            // 
            this.stageAllToolStripMenuItem.Name = "stageAllToolStripMenuItem";
            this.stageAllToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.stageAllToolStripMenuItem.Text = "S&tage All";
            this.stageAllToolStripMenuItem.Click += new System.EventHandler(this.stageAllToolStripMenuItem_Click);
            // 
            // unstageAllToolStripMenuItem
            // 
            this.unstageAllToolStripMenuItem.Name = "unstageAllToolStripMenuItem";
            this.unstageAllToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.unstageAllToolStripMenuItem.Text = "U&nstage All";
            this.unstageAllToolStripMenuItem.Click += new System.EventHandler(this.unstageAllToolStripMenuItem_Click);
            // 
            // fileHistoryToolStripMenuItem
            // 
            this.fileHistoryToolStripMenuItem.Name = "fileHistoryToolStripMenuItem";
            this.fileHistoryToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.fileHistoryToolStripMenuItem.Text = "&History";
            this.fileHistoryToolStripMenuItem.Click += new System.EventHandler(this.fileHistoryToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(2);
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Changes";
            // 
            // tvStaged
            // 
            this.tvStaged.AllowDragNodes = true;
            this.tvStaged.AllowDrop = true;
            this.tvStaged.ContextMenuStrip = this.mnuDiffs;
            this.tvStaged.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvStaged.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvStaged.Indent = 10;
            this.tvStaged.Location = new System.Drawing.Point(0, 17);
            this.tvStaged.Name = "tvStaged";
            this.tvStaged.Size = new System.Drawing.Size(407, 323);
            this.tvStaged.TabIndex = 2;
            this.tvStaged.NodesDrag += new System.Windows.Forms.DragEventHandler(this.tv_NodesDrag);
            this.tvStaged.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCollapse);
            this.tvStaged.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterSelect);
            this.tvStaged.DragDrop += new System.Windows.Forms.DragEventHandler(this.tv_DragDrop);
            this.tvStaged.DragEnter += new System.Windows.Forms.DragEventHandler(this.tv_DragEnter);
            this.tvStaged.DoubleClick += new System.EventHandler(this.tv_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(2);
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Stage Changes";
            // 
			// label5
			// 
			this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(2);
            this.label5.Size = new System.Drawing.Size(83, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Change Details";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.chkIgnoreWhiteSpace);
            this.pnlButtons.Controls.Add(this.progMain);
            this.pnlButtons.Controls.Add(this.lblStatus);
            this.pnlButtons.Controls.Add(this.btnPush);
            this.pnlButtons.Controls.Add(this.btnPull);
            this.pnlButtons.Controls.Add(this.btnViewStashes);
            this.pnlButtons.Controls.Add(this.btnStash);
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnStatus);
            this.pnlButtons.Controls.Add(this.btnCommit);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 627);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1163, 34);
            this.pnlButtons.TabIndex = 4;
            // 
            // chkIgnoreWhiteSpace
            // 
            this.chkIgnoreWhiteSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIgnoreWhiteSpace.AutoSize = true;
            this.chkIgnoreWhiteSpace.Location = new System.Drawing.Point(303, 10);
            this.chkIgnoreWhiteSpace.Name = "chkIgnoreWhiteSpace";
            this.chkIgnoreWhiteSpace.Size = new System.Drawing.Size(116, 17);
            this.chkIgnoreWhiteSpace.TabIndex = 10;
            this.chkIgnoreWhiteSpace.Text = "Ignore Whitespace";
            this.chkIgnoreWhiteSpace.UseVisualStyleBackColor = true;
            this.chkIgnoreWhiteSpace.CheckedChanged += new System.EventHandler(this.chkIgnoreWhiteSpace_CheckedChanged);
			// 
			// ucDiff
			// 
			this.ucDiff.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ucDiff.Location = new System.Drawing.Point(0, 17);
			this.ucDiff.Name = "ucDiff";
			// 
			// progMain
			// 
			this.progMain.Location = new System.Drawing.Point(3, 6);
            this.progMain.Name = "progMain";
            this.progMain.Size = new System.Drawing.Size(269, 23);
            this.progMain.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progMain.TabIndex = 9;
            this.progMain.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(278, 11);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 7;
            // 
            // btnPush
            // 
            this.btnPush.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPush.Location = new System.Drawing.Point(962, 6);
            this.btnPush.Name = "btnPush";
            this.btnPush.Size = new System.Drawing.Size(93, 23);
            this.btnPush.TabIndex = 6;
            this.btnPush.Text = "Push";
            this.btnPush.UseVisualStyleBackColor = true;
            this.btnPush.Click += new System.EventHandler(this.btnPush_Click);
            // 
            // btnPull
            // 
            this.btnPull.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPull.Enabled = false;
            this.btnPull.Location = new System.Drawing.Point(871, 6);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(85, 23);
            this.btnPull.TabIndex = 5;
            this.btnPull.Text = "Pull";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.btnPull_Click);
            // 
            // btnViewStashes
            // 
            this.btnViewStashes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewStashes.Location = new System.Drawing.Point(665, 6);
            this.btnViewStashes.Name = "btnViewStashes";
            this.btnViewStashes.Size = new System.Drawing.Size(95, 23);
            this.btnViewStashes.TabIndex = 4;
            this.btnViewStashes.Text = "View Stashes";
            this.btnViewStashes.UseVisualStyleBackColor = true;
            this.btnViewStashes.Click += new System.EventHandler(this.btnViewStashes_Click);
            // 
            // btnStash
            // 
            this.btnStash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStash.Enabled = false;
            this.btnStash.Location = new System.Drawing.Point(766, 6);
            this.btnStash.Name = "btnStash";
            this.btnStash.Size = new System.Drawing.Size(99, 23);
            this.btnStash.TabIndex = 3;
            this.btnStash.Text = "Stash";
            this.btnStash.UseVisualStyleBackColor = true;
            this.btnStash.Click += new System.EventHandler(this.btnStash_Click);
            // 
            // btnStatus
            // 
            this.btnStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatus.Location = new System.Drawing.Point(457, 6);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(98, 23);
            this.btnStatus.TabIndex = 2;
            this.btnStatus.Text = "Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(561, 6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.Location = new System.Drawing.Point(1061, 6);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(99, 23);
            this.btnCommit.TabIndex = 1;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // timDebounce
            // 
            this.timDebounce.Enabled = true;
            this.timDebounce.Interval = 300;
            this.timDebounce.Tick += new System.EventHandler(this.timDebounce_Tick);
            // 
            // mergeFromSquashToolStripMenuItem
            // 
            this.mergeFromSquashToolStripMenuItem.Name = "mergeFromSquashToolStripMenuItem";
            this.mergeFromSquashToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.mergeFromSquashToolStripMenuItem.Text = "Merge From &Squash";
            this.mergeFromSquashToolStripMenuItem.Click += new System.EventHandler(this.mergeFromSquashToolStripMenuItem_Click);
            // 
            // ucRepository
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlButtons);
            this.Name = "ucRepository";
            this.Size = new System.Drawing.Size(1163, 661);
            this.Load += new System.EventHandler(this.ucRepository_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.mnuLocal.ResumeLayout(false);
            this.mnuRemote.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.mnuDiffs.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlButtons.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private PaJaMa.WinControls.MultiSelectTreeView tvRemoteBranches;
		private System.Windows.Forms.ContextMenuStrip mnuRemote;
		private System.Windows.Forms.ToolStripMenuItem branchToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip mnuLocal;
		private System.Windows.Forms.ToolStripMenuItem checkoutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fetchToolStripMenuItem;
		private PaJaMa.WinControls.MultiSelectTreeView tvLocalBranches;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private PaJaMa.WinControls.MultiSelectTreeView tvUnStaged;
		private System.Windows.Forms.ContextMenuStrip mnuDiffs;
		private System.Windows.Forms.ToolStripMenuItem viewExternalToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer4;
		private PaJaMa.WinControls.MultiSelectTreeView tvStaged;
		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.Button btnCommit;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ignoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem branchLocalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mergeFromLocalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mergeFromToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resolveConflictToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unStageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ignoreExtensionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem abortMergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem concludeMergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortRebaseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteRemoteToolStripMenuItem;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnStatus;
		private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem compareToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem1;
		private System.Windows.Forms.Button btnStash;
		private System.Windows.Forms.Button btnViewStashes;
		private System.Windows.Forms.ToolStripMenuItem stageAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem unstageAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pruneToolStripMenuItem;
		private System.Windows.Forms.Button btnPull;
		private System.Windows.Forms.Button btnPush;
		private System.Windows.Forms.ToolStripMenuItem pullIntoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resolveUsingMineToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resolveUsingTheirsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileHistoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileHistoryToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem trackRemoteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem downloadToToolStripMenuItem;
		private System.Windows.Forms.Timer timDebounce;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ToolStripMenuItem rebaseFromToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rebaseFromToolStripMenuItem1;
		private System.Windows.Forms.ProgressBar progMain;
		private System.Windows.Forms.ToolStripMenuItem nextConflictToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem checkoutForceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pullAndMergeFromToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkIgnoreWhiteSpace;
        private System.Windows.Forms.ToolStripMenuItem mergeFromSquashToolStripMenuItem;
        private ucDifferences ucDiff;
    }
}