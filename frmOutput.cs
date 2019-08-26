﻿using System;
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
	public partial class frmOutput : Form
	{
		public frmOutput()
		{
			InitializeComponent();
		}

		private void BtnClear_Click(object sender, EventArgs e)
		{
			txtOutput.Text = string.Empty;
		}

		public void AppendText(string text)
		{
			this.Invoke(new Action(() =>
			{
				txtOutput.Text += text + "\r\n\r\n-----------------------------------";
			}));
		}
	}
}
