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
			this.gridCommits = new System.Windows.Forms.DataGridView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnCompare = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.gridDetails = new System.Windows.Forms.DataGridView();
			this.txtDifferences = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridDetails)).BeginInit();
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
			this.gridCommits.Size = new System.Drawing.Size(437, 609);
			this.gridCommits.TabIndex = 0;
			this.gridCommits.SelectionChanged += new System.EventHandler(this.gridCommits_SelectionChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.btnCompare);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 609);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1060, 31);
			this.panel2.TabIndex = 2;
			// 
			// btnCompare
			// 
			this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCompare.Location = new System.Drawing.Point(908, 5);
			this.btnCompare.Name = "btnCompare";
			this.btnCompare.Size = new System.Drawing.Size(149, 23);
			this.btnCompare.TabIndex = 0;
			this.btnCompare.Text = "External Compare";
			this.btnCompare.UseVisualStyleBackColor = true;
			this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
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
			this.splitContainer1.Size = new System.Drawing.Size(1060, 609);
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
			this.splitContainer2.Size = new System.Drawing.Size(727, 609);
			this.splitContainer2.SplitterDistance = 437;
			this.splitContainer2.TabIndex = 4;
			// 
			// gridDetails
			// 
			this.gridDetails.AllowUserToAddRows = false;
			this.gridDetails.AllowUserToDeleteRows = false;
			this.gridDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.gridDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridDetails.Location = new System.Drawing.Point(0, 0);
			this.gridDetails.Name = "gridDetails";
			this.gridDetails.ReadOnly = true;
			this.gridDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.gridDetails.Size = new System.Drawing.Size(286, 609);
			this.gridDetails.TabIndex = 1;
			this.gridDetails.SelectionChanged += new System.EventHandler(this.gridDetails_SelectionChanged);
			this.gridDetails.DoubleClick += new System.EventHandler(this.gridDetails_DoubleClick);
			// 
			// txtDifferences
			// 
			this.txtDifferences.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDifferences.Location = new System.Drawing.Point(0, 0);
			this.txtDifferences.Multiline = true;
			this.txtDifferences.Name = "txtDifferences";
			this.txtDifferences.ReadOnly = true;
			this.txtDifferences.Size = new System.Drawing.Size(329, 609);
			this.txtDifferences.TabIndex = 0;
			// 
			// frmCommitHistory
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 640);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.panel2);
			this.Name = "frmCommitHistory";
			this.ShowIcon = false;
			this.Text = "Commit History";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCompareBranches_FormClosing);
			this.Load += new System.EventHandler(this.frmCompareBranches_Load);
			((System.ComponentModel.ISupportInitialize)(this.gridCommits)).EndInit();
			this.panel2.ResumeLayout(false);
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView gridCommits;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnCompare;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TextBox txtDifferences;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.DataGridView gridDetails;
	}
}