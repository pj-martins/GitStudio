using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.GitStudio
{
	public class FormSettings
	{
		public int MainFormLeft { get; set; }
		public int MainFormTop { get; set; }
		public bool MainFormMaximized { get; set; }
		public int MainFormWidth { get; set; }
		public int MainFormHeight { get; set; }
		public string FocusedRepository { get; set; }
	}
}
