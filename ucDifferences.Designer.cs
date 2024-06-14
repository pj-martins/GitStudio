using System.Drawing;

namespace PaJaMa.GitStudio
{
	partial class ucDifferences
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private ScintillaNET.Scintilla txtDiffText;

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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			this.txtDiffText = new ScintillaNET.Scintilla();
			// 
			// ucDifferences
			// 
			this.Name = "ucDifferences";
			this.Size = new System.Drawing.Size(1003, 666);
			this.ResumeLayout(false);
			// 
			// txtDiffText
			// 
			this.txtDiffText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDiffText.Location = new System.Drawing.Point(0, 17);
			this.txtDiffText.Name = "txtDiffText";
			this.txtDiffText.ReadOnly = true;
			this.txtDiffText.Size = new System.Drawing.Size(773, 266);
			this.txtDiffText.TabIndex = 0;
			this.txtDiffText.Text = "";
			this.txtDiffText.Markers[0].SetBackColor(Color.LightPink);
			this.txtDiffText.Markers[1].SetBackColor(Color.LightGreen);
			this.txtDiffText.Markers[2].SetBackColor(Color.LightGoldenrodYellow);
			this.txtDiffText.LexerName = "markdown";
			this.txtDiffText.Margins[0].Type = ScintillaNET.MarginType.Text;
			this.txtDiffText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Controls.Add(this.txtDiffText);
		}

		#endregion
	}
}
