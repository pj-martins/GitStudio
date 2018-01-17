namespace PaJaMa.GitStudio
{
	partial class frmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTab = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.openInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabMain = new PaJaMa.WinControls.TabControl.TabControl();
			this.setRemoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.mnuTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(984, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.cloneToolStripMenuItem,
            this.setupToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "&New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// cloneToolStripMenuItem
			// 
			this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
			this.cloneToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.cloneToolStripMenuItem.Text = "&Clone";
			this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
			// 
			// setupToolStripMenuItem
			// 
			this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
			this.setupToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.setupToolStripMenuItem.Text = "&Setup";
			this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
			// 
			// mnuTab
			// 
			this.mnuTab.ImageScalingSize = new System.Drawing.Size(40, 40);
			this.mnuTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInExplorerToolStripMenuItem,
            this.setRemoteToolStripMenuItem});
			this.mnuTab.Name = "mnuTab";
			this.mnuTab.Size = new System.Drawing.Size(162, 70);
			// 
			// openInExplorerToolStripMenuItem
			// 
			this.openInExplorerToolStripMenuItem.Name = "openInExplorerToolStripMenuItem";
			this.openInExplorerToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.openInExplorerToolStripMenuItem.Text = "&Open In Explorer";
			this.openInExplorerToolStripMenuItem.Click += new System.EventHandler(this.openInExplorerToolStripMenuItem_Click);
			// 
			// tabMain
			// 
			this.tabMain.AllowRemove = true;
			this.tabMain.ContextMenuStrip = this.mnuTab;
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(0, 24);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedTab = null;
			this.tabMain.Size = new System.Drawing.Size(984, 471);
			this.tabMain.TabIndex = 1;
			this.tabMain.TabClosing += new PaJaMa.WinControls.TabControl.TabEventHandler(this.tabMain_TabClosing);
			this.tabMain.TabChanged += new PaJaMa.WinControls.TabControl.TabEventHandler(this.tabMain_TabChanged);
			this.tabMain.TabOrderChanged += new PaJaMa.WinControls.TabControl.TabEventHandler(this.tabMain_TabOrderChanged);
			// 
			// setRemoteToolStripMenuItem
			// 
			this.setRemoteToolStripMenuItem.Name = "setRemoteToolStripMenuItem";
			this.setRemoteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
			this.setRemoteToolStripMenuItem.Text = "Set &Remote";
			this.setRemoteToolStripMenuItem.Click += new System.EventHandler(this.setRemoteToolStripMenuItem_Click);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(984, 495);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.menuStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmMain";
			this.Text = "Git Studio";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.mnuTab.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
		private WinControls.TabControl.TabControl tabMain;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip mnuTab;
		private System.Windows.Forms.ToolStripMenuItem openInExplorerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem setRemoteToolStripMenuItem;
	}
}

