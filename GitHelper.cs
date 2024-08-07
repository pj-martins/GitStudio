﻿using PaJaMa.WinControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public class GitHelper
	{
		public static string GitLocation { get; set; }
		public string WorkingDirectory { get; private set; }
		public SSHConnection SSHConnection { get; private set; }
        private static object _lock = new object();
		public GitHelper() { }
		public GitHelper(GitRepository repository)
		{
			this.WorkingDirectory = repository.LocalPath;
			this.SSHConnection = repository.SSHConnection;
		}

		private string[] runCommand(string[] arguments, bool showProgress, bool includeBlankLines, bool checkForErrors, BackgroundWorker worker, ref bool hasError)
		{
			var lines = new List<Tuple<string, bool>>();
			var action = new Action(() =>
			{
				int i = 1;
				if (SSHConnection != null)
				{
					if (SSHConnection.UseCMD)
					{
                        // var rawLines = SSHHelper.RunCommandAsLines(SSHConnection, $"cd {SSHConnection.Path} && git config color.ui false --replace-all && {string.Join(" && ", arguments.Select(a => $"git {a.Replace("\"", "\\\"")}"))} && git config color.ui true --replace-all");
                        // var rawLines = SSHHelper.RunCommandAsLines(SSHConnection, $"git config color.ui false --replace-all && {string.Join(" && ", arguments.Select(a => $"git {a.Replace("\"", "\\\"")}"))} && git config color.ui true --replace-all");
                        var rawLines = SSHHelper.RunCommandAsLines(SSHConnection,
							$"git config color.ui false --replace-all && {string.Join(" && ", arguments.Select(a => $"git --no-pager {a}"))} && git config color.ui true --replace-all",
							// string.Join(" && ", arguments.Select(a => $"git {a}")),
							false, 100);
                        // File.WriteAllText(Path.Combine("Output", $"raw_{DateTime.Now:yyyyMMddHHmmssfff}.txt"), String.Join("\n", rawLines));
                        // rawLines.RemoveAt(rawLines.Count - 1);
                        // var maxInd = rawLines.Where(r => r.Contains("--replace-all")).Select(l => rawLines.IndexOf(l) + 1).Max();
                        // rawLines = rawLines.Skip(maxInd).ToList();
                        foreach (var rl in rawLines)
						{
							if (includeBlankLines || !string.IsNullOrEmpty(rl))
							{
								var illegals = new List<string>()
								{
									"[?1l",
									"[?1h",
									"[?11"
								};
								if (!illegals.Any(x => rl.Contains(x)) && !rl.Contains("Connection "))
								{
									lock (_lock)
									{
										lines.Add(new Tuple<string, bool>(rl, false));
										if (worker != null)
											worker.ReportProgress(50, rl);
									}
								}
							}
						}
						/*
						if (!File.Exists("ssh.exe"))
						{
							File.WriteAllBytes("ssh.exe", Resources.ssh);
						}
						var args = $"{SSHConnection.UserName}@{SSHConnection.Host} -t \"cd {SSHConnection.Path} && git config color.ui false --replace-all && {string.Join(" && ", arguments.Select(a => $"git {a.Replace("\"", "\\\"")}"))} && git config color.ui true --replace-all\"";
						var inf = new ProcessStartInfo("ssh", args);
						inf.UseShellExecute = false;
						inf.RedirectStandardOutput = true;
						inf.RedirectStandardError = true;
						inf.StandardOutputEncoding = Encoding.ASCII;
						inf.StandardErrorEncoding = Encoding.ASCII;
						inf.WindowStyle = ProcessWindowStyle.Hidden;
						inf.CreateNoWindow = true;
						if (WorkingDirectory != null)
							inf.WorkingDirectory = WorkingDirectory;
						var p = new Process();
						p.StartInfo = inf;
						p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
						{
							if (includeBlankLines || !string.IsNullOrEmpty(e.Data))
							{
								var illegals = new List<string>()
								{
									"[?1l",
									"[?1h",
									"[?11"
								};
								if (e.Data != null && !illegals.Any(x => e.Data.Contains(x)) && !e.Data.Contains("Connection "))
								{
									lock (_lock)
									{
										lines.Add(new Tuple<string, bool>(e.Data, false));
										if (worker != null)
											worker.ReportProgress(50, e.Data);
									}
								}
							}
						});
						p.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
						{
							if (!string.IsNullOrEmpty(e.Data))
							{
								if (e.Data != null && !e.Data.Contains("[?1h") && !e.Data.Contains("[?11") && !e.Data.Contains("Connection "))
								{
									lock (_lock)
									{
										lines.Add(new Tuple<string, bool>(e.Data, true));
										if (worker != null)
											worker.ReportProgress(50, e.Data);
									}
								}
							}
						});
						p.Start();
						p.BeginOutputReadLine();
						p.BeginErrorReadLine();
						bool completed = p.WaitForExit(5000);
						if (!completed)
						{
							// TODO: lines.Add(new Tuple<string, bool>("TIMEDOUT", true));
						}
						*/
					}
					else
					{
						var methods = new List<Renci.SshNet.AuthenticationMethod>();
						if (!string.IsNullOrEmpty(SSHConnection.Password))
						{
							methods.Add(new Renci.SshNet.PasswordAuthenticationMethod(SSHConnection.UserName, SSHConnection.Password));
						}
						else if (!string.IsNullOrEmpty(SSHConnection.KeyFile))
						{
							methods.Add(new Renci.SshNet.PrivateKeyAuthenticationMethod(SSHConnection.UserName, new Renci.SshNet.PrivateKeyFile(SSHConnection.KeyFile)));
						}
						else
						{
							methods.Add(new Renci.SshNet.NoneAuthenticationMethod(SSHConnection.UserName));
						}
						var client = new Renci.SshNet.SshClient(new Renci.SshNet.ConnectionInfo(SSHConnection.Host, SSHConnection.UserName, methods.ToArray()));
						client.Connect();
						var cmd = client.RunCommand($"cd {SSHConnection.Path} && {string.Join(" && ", arguments.Select(a => $"git {a}"))}");
						client.Disconnect();
						client.Dispose();
						client = null;
						if (string.IsNullOrEmpty(cmd.Result) && !string.IsNullOrEmpty(cmd.Error))
						{
							lines = cmd.Error.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new Tuple<string, bool>(x.Trim(), true)).ToList();
						}
						else
						{
							lines = cmd.Result.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => new Tuple<string, bool>(x.Trim(), false)).ToList();
						}
					}
				}
				else
				{
					foreach (var argument in arguments)
					{
						var gitLocation = GitLocation ?? "git";
						var inf = new ProcessStartInfo(gitLocation, argument);
						inf.UseShellExecute = false;
						inf.RedirectStandardOutput = true;
						inf.RedirectStandardError = true;
						inf.StandardOutputEncoding = Encoding.UTF8;
						inf.StandardErrorEncoding = Encoding.UTF8;
						inf.WindowStyle = ProcessWindowStyle.Hidden;
						inf.CreateNoWindow = true;
						if (WorkingDirectory != null)
							inf.WorkingDirectory = WorkingDirectory;
						var p = new Process();
						p.StartInfo = inf;
						p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
						{
							if (includeBlankLines || !string.IsNullOrEmpty(e.Data))
							{
								lock (_lock)
								{
									lines.Add(new Tuple<string, bool>(e.Data, false));
									if (worker != null)
										worker.ReportProgress(100 * i / argument.Length, e.Data);
								}
							}
						});
						p.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
						{
							if (!string.IsNullOrEmpty(e.Data))
							{
								lock (_lock)
								{
									lines.Add(new Tuple<string, bool>(e.Data, true));
									if (worker != null)
										worker.ReportProgress(100 * i / argument.Length, e.Data);
								}
							}
						});
						p.Start();
						p.BeginOutputReadLine();
						p.BeginErrorReadLine();
						p.WaitForExit();
					}
				}
			});
			if (showProgress)
			{
				if (worker == null)
					worker = new BackgroundWorker();
				worker.DoWork += (object sender, DoWorkEventArgs e) => action.Invoke();
				new frmOutput().ShowProgress(worker, "Running command(s)\r\n" + string.Join("\r\n", arguments));
			}
			else
			{
				action.Invoke();
			}

			if (lines.Any(l => l.Item2) && checkForErrors)
			{
				hasError = true;
				File.AppendAllLines($"error_{DateTime.Now.ToString("yyyyMMdd")}.log", lines.Where(l => l.Item2).Select(x => x.Item1));
			}

			return lines.Select(l => l.Item1).ToArray();
		}

		public string[] RunCommand(string[] arguments, bool showProgress = false)
		{
			bool hasError = false;
			return runCommand(arguments, showProgress, false, false, null, ref hasError);
		}

		public string[] RunCommand(string arguments, bool includeBlankLines, bool showProgress, ref bool hasError)
		{
			return runCommand(new string[] { arguments }, showProgress, includeBlankLines, true, null, ref hasError);
		}

		public string[] RunCommand(string arguments, bool showProgress, ref bool hasError)
		{
			return runCommand(new string[] { arguments }, showProgress, false, true, null, ref hasError);
		}

		public string[] RunCommand(string arguments, bool showProgress = false)
		{
			bool hasError = false;
			return runCommand(new string[] { arguments }, showProgress, false, false, null, ref hasError);
		}

		public string[] RunCommand(string[] arguments, BackgroundWorker worker)
		{
			bool hasError = false;
			return runCommand(arguments, false, false, false, worker, ref hasError);
		}

		public string[] RunCommand(string arguments, BackgroundWorker worker)
		{
			bool hasError = false;
			return runCommand(new string[] { arguments }, false, false, false, worker, ref hasError);
		}

		public List<Branch> GetBranches(bool showProgress, int retry = 0)
		{
			bool remote = true;
			bool somethingWentWrong = false;
			List<Branch> branches = new List<Branch>();
			var action = new Action(() =>
			{
				while (true)
				{
					bool error = false;
					var branchLines = RunCommand("branch " + (remote ? "-r" : "-l") + " -vv", false, ref error);
					//if (error)
					//{
					//	branches = null;
					//	break;
					//}
					foreach (var b in branchLines.Select(bl => bl.Trim()))
					{
						if (b.Contains("[?1l")) continue;
						var branchParts = b.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
						if (branchParts.Count < 2) continue;
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
										lb.Behind = string.IsNullOrEmpty(match3.Groups[1].Value) ? 0 : Convert.ToInt16(match3.Groups[1].Value);
								}
								if (!lb.RemoteIsGone)
								{
									var remoteBranchName = match2.Success ? match2.Groups[1].Value : match.Groups[1].Value;
									lb.TracksBranch = branches.OfType<RemoteBranch>().FirstOrDefault(rb => rb.BranchName == remoteBranchName);
									if (lb.TracksBranch == null)
									{
										// TODO: something went wrong, try again
										somethingWentWrong = true;
										retry++;
										break;
									}
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
				backgroundWorker.DoWork += (object sender, DoWorkEventArgs e) =>
				{
					action.Invoke();
					if (somethingWentWrong && retry < 2)
					{
						branches = GetBranches(false);
					}
				};
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
				if (!diffParts.Any()) continue;
				diff.DifferenceType = Helpers.GetDifferenceType(diffParts[0]);
				if (diffParts[0].Contains("U") || diffParts[0] == "AA")
					diff.IsConflict = true;

				diffParts.RemoveAt(0);

				var remaining = string.Join(" ", diffParts.ToArray());
				if (remaining.StartsWith("\"") && remaining.EndsWith("\""))
				{
					remaining = remaining.Substring(1, remaining.Length - 2);
				}
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

				var lines = new GitHelper().RunCommand("clone " + url + " " + dlgOpenFolder.SelectedPath +
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
			if (SSHConnection != null)
			{

			}
			else
			{
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
