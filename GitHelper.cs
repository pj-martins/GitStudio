using PaJaMa.WinControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		private string[] runCommand(string[] arguments, bool showProgress, bool checkForErrors, ref bool hasError)
		{
			var lines = new List<string>();
			var errorLines = new List<string>();
			BackgroundWorker worker = null;
			var action = new Action(() =>
			{
				int i = 1;
				foreach (var argument in arguments)
				{
					var inf = new ProcessStartInfo("git.exe", argument);
					inf.UseShellExecute = false;
					inf.RedirectStandardOutput = true;
					inf.RedirectStandardError = true;
					inf.WindowStyle = ProcessWindowStyle.Hidden;
					inf.CreateNoWindow = true;
					if (WorkingDirectory != null)
						inf.WorkingDirectory = WorkingDirectory;
					var p = Process.Start(inf);
					string line = string.Empty;
					while ((line = p.StandardOutput.ReadLine()) != null)
					{
						if (worker != null) worker.ReportProgress(100 * i / argument.Length, line);
						lines.Add(line);
					}

					while ((line = p.StandardError.ReadLine()) != null)
					{
						if (worker != null) worker.ReportProgress(100 * i / argument.Length, line);
						errorLines.Add(line);
					}
					p.WaitForExit();
				}
			});
			if (showProgress)
			{
				worker = new BackgroundWorker();
				worker.DoWork += (object sender, DoWorkEventArgs e) => action.Invoke();
				WinControls.WinProgressBox.ShowProgress(worker, "Running command " + arguments, progressBarStyle: ProgressBarStyle.Marquee);
			}
			else
			{
				action.Invoke();
			}
			if (errorLines.Count > 0)
			{
				if (!checkForErrors)
					lines.AddRange(errorLines);
				else
				{
					hasError = true;
					ScrollableMessageBox.ShowDialog(errorLines.ToArray());
				}
			}
			return lines.ToArray();
		}

		public string[] RunCommand(string[] arguments, bool showProgress, ref bool hasError)
		{
			return runCommand(arguments, showProgress, true, ref hasError);
		}

		public string[] RunCommand(string[] arguments, bool showProgress = false)
		{
			bool hasError = false;
			return runCommand(arguments, showProgress, false, ref hasError);
		}

		public string[] RunCommand(string arguments, bool showProgress, ref bool hasError)
		{
			return runCommand(new string[] { arguments }, showProgress, true, ref hasError);
		}

		public string[] RunCommand(string arguments, bool showProgress = false)
		{
			bool hasError = false;
			return runCommand(new string[] { arguments }, showProgress, false, ref hasError);
		}

		public List<Branch> GetBranches(bool showProgress = false)
		{
			bool remote = true;
			List<Branch> branches = new List<Branch>();
			var action = new Action(() =>
			{
				while (true)
				{
					bool error = false;
					var branchLines = RunCommand("branch " + (remote ? "-r" : "-l") + " -vv", false, ref error);
					if (error)
					{
						branches = null;
						break;
					}
					foreach (var b in branchLines.Select(bl => bl.Trim()))
					{
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
									lb.TracksBranch = branches.OfType<RemoteBranch>().FirstOrDefault(rb => rb.BranchName == remoteBranchName);
								}
							}
						}
					}
					if (remote) remote = false;
					else break;
				}
			});

			if (showProgress)
			{
				var backgroundWorker = new BackgroundWorker();
				backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) => action.Invoke();
				WinControls.WinProgressBox.ShowProgress(backgroundWorker, "Retrieving branches.");
			}
			else
				action.Invoke();

			return branches;
		}

		public List<Difference> GetDifferences()
		{
			bool error = false;
			var diffLines = RunCommand("status --short", false, ref error);
			if (error) return null;

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
					case "AM":
						diff.DifferenceType = DifferenceType.Modify;
						break;
					case "D":
						diff.DifferenceType = DifferenceType.Delete;
						break;
					case "R":
						diff.DifferenceType = DifferenceType.Rename;
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

		public void DownloadBranch(RemoteBranch branch, string url)
		{
			var dlgOpenFolder = new FolderBrowserDialog();
			if (dlgOpenFolder.ShowDialog() == DialogResult.OK)
			{
				bool hasError = false;
				string branchName = branch.BranchName;
				if (branchName.StartsWith("origin/"))
					branchName = branchName.Substring(7);

				var lines = new GitHelper(null).RunCommand("clone " + url + " " + dlgOpenFolder.SelectedPath +
					" -b " + branchName, true, ref hasError);
				if (hasError && lines.Length > 0)
				{
					ScrollableMessageBox.Show(lines, "ERROR");
				}
			}
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
