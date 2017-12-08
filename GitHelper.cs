using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public string[] RunCommand(string arguments, ref bool error)
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
				error = true;
				MessageBox.Show(string.Join("\r\n", errorLines.ToArray()));
			}
			return lines.ToArray();
		}

		public List<Branch> GetBranches()
		{
			bool remote = true;
			List<Branch> branches = new List<Branch>();
			while (true)
			{
				bool error = false;
				var branchLines = RunCommand("branch " + (remote ? "-r" : "-l") + " -vv", ref error);
				if (error) return new List<Branch>();

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
			bool error = false;
			var diffLines = RunCommand("status --short", ref error);
			if (error) return new List<Difference>();

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
						diff.DifferenceType = DifferenceType.Modify;
						break;
					case "D":
						diff.DifferenceType = DifferenceType.Delete;
						break;
				}

				diffParts.RemoveAt(0);

				var remaining = string.Join(" ", diffParts.ToArray());
				diff.FileName = remaining;
				diffs.Add(diff);
			}

			return diffs;
		}
	}
}
