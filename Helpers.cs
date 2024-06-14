using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PaJaMa.GitStudio
{
	public class Helpers
	{
		public static DifferenceType GetDifferenceType(string input)
		{
			switch (input.ToUpper())
			{
				case "A":
				case "C":
				case "AA":
				case "??":
					return DifferenceType.Add;
				case "M":
				case "MM":
				case "AM":
				case "UU":
				case "T":
				case "RM":
					return DifferenceType.Modify;
				case "AD":
				case "UD":
				case "DU":
				case "DD":
				case "MD":
				case "RD":
				case "D":
					return DifferenceType.Delete;
				case "R":
					return DifferenceType.Rename;
				default:
					return DifferenceType.Unknown;
			}
		}
	}
}
