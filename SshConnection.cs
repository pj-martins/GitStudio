using Newtonsoft.Json;
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

        private Process _sshProcess;
        private bool _beginReceive = false;
        private List<string> _receiveLines = new List<string>();
        private DateTime? _lastReceive;
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
            _lastReceive = DateTime.Now;
            if (_beginReceive)
            {
                _receiveLines.Add(e.Data);
            }
        }

        private void waitForBreak(bool initing, int waitTime)
        {
            int tries = 100;
            while (
                _lastReceive == null || (DateTime.Now - _lastReceive.Value).TotalMilliseconds < waitTime
            )
            {
                if (!initing && _lastReceive == null && tries <= 0)
                {
                    throw new Exception("Error receiving");
                }
                Thread.Sleep(waitTime);
                if (!initing && _lastReceive == null)
                {
                    tries--;
                }
            }
        }

        public List<string> RunCommand(string command, int waitTime = 50)
        {
            lock (_lock)
            {
                if (_sshProcess == null || _sshProcess.HasExited)
                {
                    initProcess();
                    Thread.Sleep(100);
                    waitForBreak(true, waitTime);
                    _lastReceive = null;
                    _sshProcess.StandardInput.WriteLine($"cd {Path}");
                    Thread.Sleep(100);
                    _sshProcess.StandardInput.WriteLine("clear");
                    waitForBreak(true, waitTime);
                    Thread.Sleep(1000);
                    _lastReceive = null;
                    _beginReceive = true;
                }

                _receiveLines.Clear();
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
                _receiveLines.Clear();
                _lastReceive = null;
                return lines;
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
            _lastReceive = null;
            _beginReceive = false;
        }
    }
}
