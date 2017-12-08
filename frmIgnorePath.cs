using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class frmIgnorePath : Form
	{
		private string _fullPath;
		public string FullPath
		{
			get { return _fullPath; }
			set
			{
				_fullPath = value;
				var parts = value.Split('/').ToList();
				parts.RemoveAt(parts.Count - 1);
				var runningPath = string.Empty;
				while (parts.Count > 0)
				{
					runningPath += (string.IsNullOrEmpty(runningPath) ? "" : "/") + parts[0];
					cboPathParts.Items.Add(runningPath);
					parts.RemoveAt(0);
				}
				cboPathParts.SelectedIndex = cboPathParts.Items.Count - 1;
			}
		}

		public string IgnorePath
		{
			get { return cboPathParts.Text; }
		}

		public frmIgnorePath()
		{
			InitializeComponent();
		}

		private void btnIgnore_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
