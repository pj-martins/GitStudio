using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaJaMa.GitStudio
{
    public class SshHelper
    {
        private static object _lock = new object();
        public static string RunCommand(SshConnection connection, string command)
        {
            var lines = new List<string>();
            if (!File.Exists("ssh.exe"))
            {
                File.WriteAllBytes("ssh.exe", Resources.ssh);
            }
            var args = $"{connection.UserName}@{connection.Host} -t \"{command}\"";
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
                    if (e.Data != null && !e.Data.Contains("Connection "))
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
            //    if (!string.IsNullOrEmpty(e.Data))
            //    {
            //        if (e.Data != null && !e.Data.Contains("[?1h") && !e.Data.Contains("[?11") && !e.Data.Contains("Connection "))
            //        {
            //            lock (_lock)
            //            {
            //                lines.Add(new Tuple<string, bool>(e.Data, true));
            //                if (worker != null)
            //                    worker.ReportProgress(50, e.Data);
            //            }
            //        }
            //    }
            //});
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            return string.Join("\r\n", lines);
        }
    }
}
