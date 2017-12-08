using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public class GitHelper
	{
		public string WorkingDirectory { get; private set; }
		public GitHelper(string workingDirectory)
		{
			this.WorkingDirectory = workingDirectory;
		}

		public string[] RunCommand(string arguments, ref string errorMessage)
		{
			var inf = new ProcessStartInfo("git.exe", arguments);
			inf.UseShellExecute = false;
			inf.RedirectStandardOutput = true;
			inf.RedirectStandardError = true;
			inf.WindowStyle = ProcessWindowStyle.Hidden;
			inf.CreateNoWindow = true;
			if (WorkingDirectory != null)
				inf.WorkingDirectory = WorkingDirectory;
			var p = Process.Start(inf);
			string line = string.Empty;
			var lines = new List<string>();
			while ((line = p.StandardOutput.ReadLine()) != null)
			{
				lines.Add(line);
			}

			var errorLines = new List<string>();
			while ((line = p.StandardError.ReadLine()) != null)
			{
				errorLines.Add(line);
			}
			p.WaitForExit();
			if (errorLines.Count > 0)
			{
				errorMessage = string.Join("\r\n", errorLines.ToArray());
				MessageBox.Show(errorMessage);
			}
			return lines.ToArray();
		}

		public List<Branch> GetBranches()
		{
			bool remote = true;
			List<Branch> branches = new List<Branch>();
			while (true)
			{
				string error = string.Empty;
				var branchLines = RunCommand("branch " + (remote ? "-r" : "-l") + " -vv", ref error);
				if (!string.IsNullOrEmpty(error)) return new List<Branch>();

				foreach (var b in branchLines)
				{
					if (b.Contains("origin/HEAD ->"))
						continue;
					var branchParts = b.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
					Branch branch = remote ? (Branch)new RemoteBranch() : new LocalBranch();
					branches.Add(branch);
					if (!remote && branchParts[0] == "*")
					{
						((LocalBranch)branch).IsCurrent = true;
						branchParts.RemoveAt(0);
					}
					branch.BranchName = branchParts[0];
					if (remote && branch.BranchName.StartsWith("origin/"))
					{
						branch.BranchName = branch.BranchName.Substring(7);
					}
					branch.BranchID = branchParts[1];
					branchParts.RemoveRange(0, 2);
					var remainingParts = string.Join(" ", branchParts.ToArray());
					if (!remote)
					{
						var match = Regex.Match(remainingParts, "\\[(.*?)\\]");
						if (match.Success)
						{
							var lb = branch as LocalBranch;
							var match2 = Regex.Match(match.Groups[1].Value, "(.*?):(.*)");
							if (match2.Success)
							{
								if (match2.Groups[2].Value.Contains("gone"))
									lb.RemoteIsGone = true;
								var match3 = Regex.Match(match2.Groups[2].Value, "ahead (\\d*)");
								if (match3.Success)
									lb.Ahead = Convert.ToInt16(match3.Groups[1].Value);
								match3 = Regex.Match(match2.Groups[2].Value, "behind (\\d*)");
								if (match3.Success)
									lb.Behind = Convert.ToInt16(match3.Groups[1].Value);
							}
							if (!lb.RemoteIsGone)
							{
								var remoteBranchName = match2.Success ? match2.Groups[1].Value : match.Groups[1].Value;
								if (remoteBranchName.StartsWith("origin/"))
								{
									remoteBranchName = remoteBranchName.Substring(7);
								}
								lb.TracksBranch = branches.OfType<RemoteBranch>().First(rb => rb.BranchName == remoteBranchName);
							}
						}
					}
				}
				if (remote) remote = false;
				else break;
			}

			return branches;
		}

		public List<Difference> GetDifferences()
		{
			string error = string.Empty;
			var diffLines = RunCommand("status --short", ref error);
			if (!string.IsNullOrEmpty(error)) return null;

			var diffs = new List<Difference>();

			foreach (var d in diffLines)
			{
				var diff = new Difference();
				if (!d.StartsWith(" ") && !d.StartsWith("??"))
				{
					diff.IsStaged = true;
				}

				var diffParts = d.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
				switch (diffParts[0])
				{
					case "A":
					case "??":
						diff.DifferenceType = DifferenceType.Add;
						break;
					case "M":
					case "MM":
						diff.DifferenceType = DifferenceType.Modify;
						break;
					case "D":
						diff.DifferenceType = DifferenceType.Delete;
						break;
					case "UU":
						diff.DifferenceType = DifferenceType.Modify;
						diff.IsConflict = true;
						break;
					default:
						throw new Exception(diffParts[0]);
				}

				diffParts.RemoveAt(0);

				var remaining = string.Join(" ", diffParts.ToArray());
				diff.FileName = remaining;
				diffs.Add(diff);
				if (diff.DifferenceType == DifferenceType.Add && !diff.IsStaged)
					checkSubdirectoryAddDifferences(diff, diffs);
			}

			return diffs;
		}

		private void checkSubdirectoryAddDifferences(Difference difference, List<Difference> diffs)
		{
			string[] ignoreLines = new string[0];
			var ignoreFile = Path.Combine(WorkingDirectory, ".gitignore");
			if (File.Exists(ignoreFile))
				ignoreLines = File.ReadAllLines(ignoreFile);
			var dir = Path.Combine(WorkingDirectory, difference.FileName);
			if (Directory.Exists(dir))
			{
				var files = recursivelyGetFiles(dir, ignoreLines);
				foreach (var file in files)
				{
					var diff = new Difference();
					diff.DifferenceType = DifferenceType.Add;
					diff.FileName = file.Substring(WorkingDirectory.Length + 1).Replace("\\", "/");
					diffs.Add(diff);
				}
			}
		}

		private List<string> recursivelyGetFiles(string parentDirectory, string[] ignoreLines)
		{
			List<string> files = new List<string>();
			var checkDirectory = parentDirectory.Substring(WorkingDirectory.Length + 1);
			if (ignoreLines.Contains(checkDirectory.Replace("\\", "/")) || ignoreLines.Contains(checkDirectory.Replace("\\", "/") + "/"))
				return new List<string>();

			var dinf = new DirectoryInfo(parentDirectory);
			foreach (var f in dinf.GetFiles())
			{
				files.Add(f.FullName);
			}
			foreach (var d in dinf.GetDirectories())
			{
				files.AddRange(recursivelyGetFiles(d.FullName, ignoreLines));
			}
			return files;
		}
	}
}
