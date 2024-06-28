using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public partial class ucDifferences : UserControl
	{
		public ucDifferences()
		{
			InitializeComponent();
		}

		public void ClearDifferences()
		{
			txtDiffText.ReadOnly = false;
			txtDiffText.Text = string.Empty;
			txtDiffText.Margins[0].Width = 0;
			txtDiffText.ReadOnly = true;
		}

		public void SetDifferences(string[] allLines, DifferenceType diffType)
		{
			if (allLines.Length == 0)
			{
				this.ClearDifferences();
				return;
			}
			var ind = 0;
			if (allLines.Any(x => x.StartsWith("diff --git")))
			{
				var firstLine = allLines.FirstOrDefault(l => l.StartsWith("@@ "));
				if (firstLine != null)
				{
					ind = allLines.ToList().IndexOf(firstLine);
				}
			}
			var lines = allLines.Skip(ind).ToArray();
			txtDiffText.ReadOnly = false;
			txtDiffText.Margins[0].Width = 70;
			var adds = new Dictionary<int, int>();
			var removes = new Dictionary<int, int>();
			var lineIndicators = new List<int>();
			var actualLines = new List<string>();
			int removeStart = 0;
			int addStart = 0;
			int addIndex = 0;
			int removeIndex = 0;
			for (int i = 0; i < lines.Length; i++)
			{
				var match = Regex.Match(lines[i], "^@@ \\-(\\d*),\\d* \\+(\\d*),\\d* @@");
				if (match.Success)
				{
					removeStart = Convert.ToInt32(match.Groups[1].Value);
					addStart = Convert.ToInt32(match.Groups[2].Value);
					addIndex = 0;
					removeIndex = 0;
					lineIndicators.Add(i);
				}
				if (lines[i].StartsWith("-"))
				{
					removes.Add(i, removeStart + removeIndex);
					actualLines.Add(" " + lines[i].Substring(1));
					removeIndex++;
				}
				else if (lines[i].StartsWith("+"))
				{
					adds.Add(i, addStart + addIndex);
					actualLines.Add(" " + lines[i].Substring(1));
					addIndex++;
				}
				else
				{
					actualLines.Add(lines[i]);
					addIndex++;
					removeIndex++;
				}
			}
			txtDiffText.Text = string.Join("\r\n", actualLines);

			for (int i = 0; i < lines.Length; i++)
			{
				if (lineIndicators.Contains(i))
				{
					txtDiffText.Lines[i].MarkerAdd(2);
				}
				else if (adds.ContainsKey(i))
				{
					txtDiffText.Lines[i].MarkerAdd(1);
					txtDiffText.Lines[i].MarginText = $"    {adds[i]}";
				}
				else if (removes.ContainsKey(i))
				{
					txtDiffText.Lines[i].MarkerAdd(0);
					txtDiffText.Lines[i].MarginText = $" {removes[i]}";
				}
			}

			txtDiffText.ReadOnly = true;
		}
	}
}
