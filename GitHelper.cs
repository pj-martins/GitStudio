using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.GitStudio
{
	public class GitHelper
	{
		const string REFS_HEAD = "refs/heads/";
		public static string[] RunCommand(string arguments, string workingDirectory = null)
		{
			var inf = new ProcessStartInfo("git.exe", arguments);
			inf.UseShellExecute = false;
			inf.RedirectStandardOutput = true;
			inf.RedirectStandardError = true;
			if (workingDirectory != null)
				inf.WorkingDirectory = workingDirectory;
			var p = Process.Start(inf);
			string line = string.Empty;
			var lines = new List<string>();
			while ((line = p.StandardOutput.ReadLine()) != null)
			{
				lines.Add(line);
			}
			p.WaitForExit();
			// string error = p.StandardError.ReadToEnd();
			// string output = p.StandardOutput.ReadToEnd();
			//if (string.IsNullOrEmpty(output) && !string.IsNullOrEmpty(error))
			//	throw new Exception(error);got 
			// return output.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
			return lines.ToArray();
		}

		public static List<string> GetRemoteBranches(string url = "", string workingDirectory = null)
		{
			var repos = new List<string>();
			var lines = RunCommand("ls-remote " + url, workingDirectory);
			foreach (var line in lines)
			{
				var repo = line.Split('\t')[1];
				if (repo.StartsWith(REFS_HEAD))
				{
					repos.Add(repo.Substring(REFS_HEAD.Length));
				}
				
			}
			return repos;
		}
	}
}
