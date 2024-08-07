﻿using Newtonsoft.Json;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PaJaMa.GitStudio
{
	public class SSHConnection
	{
		private const string PASSWORD = "SS4CONN";

		public string Host { get; set; }
		public string UserName { get; set; }
		public string Path { get; set; }
		public string PasswordEncrypted { get; set; }
		public string KeyFile { get; set; }
		public bool UseCMD { get; set; }
		public bool RemoteCommand { get; set; }

		private Process _sshProcess;
		private bool _beginReceive = false;
		private List<string> _receiveLines = new List<string>();
		private object _lock = new object();

		[JsonIgnore]
		public string Password
		{
			get
			{
				return string.IsNullOrEmpty(PasswordEncrypted)
					? string.Empty
					: Common.EncrypterDecrypter.Decrypt(PasswordEncrypted, PASSWORD);
			}
			set
			{
				PasswordEncrypted = string.IsNullOrEmpty(value)
					? value
					: Common.EncrypterDecrypter.Encrypt(value, PASSWORD);
			}
		}

		private string getProcsPath()
		{
			var tmpDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "GitStudio");
			if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
			var procsPath = System.IO.Path.Combine(tmpDir, "ActiveProcesses.json");
			return procsPath;
		}

		private List<int> getExistingProcs()
		{
			var procsPath = getProcsPath();
			var procs = new List<int>();
			if (File.Exists(procsPath))
			{
				procs = JsonConvert.DeserializeObject<List<int>>(File.ReadAllText(procsPath));
			}
			return procs;
		}

		private void initProcess()
		{
			if (!File.Exists("ssh.exe"))
			{
				File.WriteAllBytes("ssh.exe", Resources.ssh);
			}

			_sshProcess = new Process();
			_sshProcess.StartInfo.FileName = "cmd.exe";
			_sshProcess.StartInfo.Arguments = $"/c \"ssh.exe -tt {UserName}@{Host}\"";
			_sshProcess.StartInfo.UseShellExecute = false;
			_sshProcess.StartInfo.RedirectStandardOutput = true;
			_sshProcess.StartInfo.RedirectStandardInput = true;
			_sshProcess.StartInfo.RedirectStandardError = true;
			_sshProcess.StartInfo.CreateNoWindow = true;
			_sshProcess.StartInfo.StandardOutputEncoding = Encoding.UTF8;
			_sshProcess.OutputDataReceived += new DataReceivedEventHandler(dataReceived);
			_sshProcess.ErrorDataReceived += new DataReceivedEventHandler(dataReceived);
			_sshProcess.Start();
			_sshProcess.BeginOutputReadLine();
			_sshProcess.BeginErrorReadLine();

			var procs = getExistingProcs();
			procs.Add(_sshProcess.Id);
			File.WriteAllText(getProcsPath(), JsonConvert.SerializeObject(procs));
		}

		private void dataReceived(object sender, DataReceivedEventArgs e)
		{
			// File.AppendAllText("debugssh.txt", $"RECEIVING {e.Data.Replace("\r", "").Replace("\n", "|")}\n");
			if (_beginReceive)
			{
				lock (_receiveLines)
				{
					_receiveLines.Add(e.Data);
				}
			}
		}

		private void waitForBreak(bool initing, int waitTime)
		{
			var path = this.Path;
			if (path.EndsWith("/"))
			{
				path = path.Substring(0, path.Length - 1);
			}
			var lastLength = _receiveLines.Count;
			int tries = 100;
			while (!_receiveLines.ToList().Any(x => x.Contains($"[{path}]")))
			{
				Thread.Sleep(waitTime);
				if (!initing && lastLength == _receiveLines.Count)
				{
					tries--;
					if (tries == 0)
					{
						throw new Exception("Error receiving");
					}
				}
				lastLength = _receiveLines.Count;
			}
		}

		public List<string> RunCommand(string command, int waitTime = 50)
		{
			if (RemoteCommand)
			{
				var lines = new List<string>();
				if (!File.Exists("ssh.exe"))
				{
					File.WriteAllBytes("ssh.exe", Resources.ssh);
				}
				var args = $"{UserName}@{Host} -t \"cd {Path} && {command}";
				var inf = new ProcessStartInfo("ssh", args);
				inf.UseShellExecute = false;
				inf.RedirectStandardOutput = true;
				inf.RedirectStandardError = true;
				inf.StandardOutputEncoding = Encoding.ASCII;
				inf.StandardErrorEncoding = Encoding.ASCII;
				inf.WindowStyle = ProcessWindowStyle.Hidden;
				inf.CreateNoWindow = true;
				var p = new Process();
				p.StartInfo = inf;
				p.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
				{
					if (!string.IsNullOrEmpty(e.Data))
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
								lines.Add(e.Data);
							}
						}
					}
				});
				//p.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
				//{
				//	if (!string.IsNullOrEmpty(e.Data))
				//	{
				//		if (e.Data != null && !e.Data.Contains("[?1h") && !e.Data.Contains("[?11") && !e.Data.Contains("Connection "))
				//		{
				//			lock (_lock)
				//			{
				//				lines.Add(new Tuple<string, bool>(e.Data, true));
				//				if (worker != null)
				//					worker.ReportProgress(50, e.Data);
				//			}
				//		}
				//	}
				//});
				p.Start();
				p.BeginOutputReadLine();
				p.BeginErrorReadLine();
				bool completed = p.WaitForExit(5000);
				if (!completed)
				{
					// TODO: lines.Add(new Tuple<string, bool>("TIMEDOUT", true));
				}
				return lines;
			}
			else
			{
				lock (_lock)
				{
					if (_sshProcess == null || _sshProcess.HasExited)
					{
						initProcess();
						_sshProcess.StandardInput.WriteLine($"cd {Path}");
						_sshProcess.StandardInput.WriteLine("clear");
						_beginReceive = true;
					}

					lock (_receiveLines) _receiveLines.Clear();
					// File.AppendAllText("debugssh.txt", $"__SENDING {command}\n");
					_sshProcess.StandardInput.WriteLine(command);
					Thread.Sleep(100);
					waitForBreak(false, waitTime);
					var lines = _receiveLines.Where(l => l != null && !l.StartsWith("\u001b[")).ToList();
					if (lines.Count > 0)
					{
						var ind = lines.FindIndex(l => l.EndsWith("\u001b[?2004l"));
						if (ind > 0)
						{
							lines = lines.Skip(ind + 1).ToList();
						}

						var firstInd = _receiveLines.FindIndex(l => l.Contains(" % "));
						if (firstInd >= 0)
						{
							var percentInd = _receiveLines[firstInd].IndexOf(" % ");
							var skipPart = _receiveLines[firstInd].Substring(0, percentInd);
							if (skipPart.Length > 0)
							{
								var lastInd = lines.FindIndex(l => l.StartsWith(skipPart));
								if (lastInd > 1)
								{
									lines = lines.Take(lastInd - 1).ToList();
								}
							}
						}
					}
					lock (_receiveLines) _receiveLines.Clear();
					return lines;
				}
			}
		}

		public void Terminate()
		{
			if (_sshProcess != null)
			{
				var procs = getExistingProcs();
				if (procs.Contains(_sshProcess.Id))
				{
					procs.Remove(_sshProcess.Id);
					File.WriteAllText(getProcsPath(), JsonConvert.SerializeObject(procs));
				}

				_sshProcess.StandardInput.WriteLine("exit");
				_sshProcess.Dispose();
				_sshProcess = null;
			}
			_beginReceive = false;
		}
	}
}
