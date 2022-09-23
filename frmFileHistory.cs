using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PaJaMa.WinControls.Imaging;

namespace PaJaMa.GitStudio
{
    public partial class frmFileHistory : Form
    {
        public GitHelper Helper { get; set; }

        public frmFileHistory()
        {
            InitializeComponent();
        }

        private void frmFileHistory_Load(object sender, EventArgs e)
        {
            PaJaMa.WinControls.FormSettings.LoadSettings(this);
            if (Helper.SshConnection != null)
            {
                refreshSshFiles(Helper.SshConnection.Path, tvFiles.Nodes);
            }
            else
            {
                refreshFiles(new DirectoryInfo(Helper.WorkingDirectory), tvFiles.Nodes);
            }
        }

        private void frmCompareBranches_FormClosing(object sender, FormClosingEventArgs e)
        {
            PaJaMa.WinControls.FormSettings.SaveSettings(this);
        }

        private void refreshFiles(DirectoryInfo dinf, TreeNodeCollection nodes)
        {
            foreach (var dinf2 in dinf.GetDirectories())
            {
                var dirNode = nodes.Add(dinf2.Name);
                dirNode.Tag = dinf2;
                dirNode.Nodes.Add("__");
            }

            foreach (var finf in dinf.GetFiles())
            {
                var fileNode = nodes.Add(finf.Name);
                fileNode.Tag = finf.FullName.Replace(Helper.WorkingDirectory + "\\", "");
            }
        }

        private void refreshSshFiles(string parentPath, TreeNodeCollection nodes)
        {
            var lines = SshHelper.RunCommandAsLines(Helper.SshConnection, $"cd {parentPath} && ls -l -a -F");
            foreach (var line in lines)
            {
                var parts = line.Split(' ').Where(x => !string.IsNullOrEmpty(x.Trim())).ToList();
                if (parts.Count >= 9)
                {
                    var lastParts = parts.Skip(8).ToList();
                    if (lastParts[0] == "./" || lastParts[0] == "../") continue;
                    var ind = lastParts.IndexOf("->");
                    string symbolicLink = string.Empty;
                    if (ind > 0)
                    {
                        symbolicLink = string.Join(" ", lastParts.Skip(ind));
                        lastParts = lastParts.Take(ind).ToList();
                    }

                    var sub = string.Join(" ", lastParts);
                    if (sub.EndsWith("*"))
                    {
                        sub = sub.Substring(0, sub.Length - 1);
                        var fileNode = nodes.Add(sub);
                        fileNode.Tag = $"{parentPath}/{sub}";
                    }
                    else if (sub.EndsWith("/") || symbolicLink.EndsWith("/"))
                    {
                        if (sub.EndsWith("/")) sub = sub.Substring(0, sub.Length - 1);
                        var dirNode = nodes.Add(sub);
                        dirNode.Nodes.Add("__");
                        dirNode.Tag = string.IsNullOrEmpty(symbolicLink) ? $"{parentPath}/{sub}" : symbolicLink;
                    }
                    else
                    {

                    }
                }
            }
        }

        private void selectFile(string fileName)
        {
            var logs = Helper.RunCommand("--no-pager log " + fileName);
            var commits = new List<Commit>();
            //commits.Add(new Commit()
            //{
            //	CommitID = "HEAD",
            //	Author = "HEAD",
            //	Index = 1,
            //});

            // int index = 2;
            int index = 1;
            Commit current = null;
            foreach (var log in logs)
            {
                if (log.StartsWith("commit "))
                {
                    if (current != null) current.Comment = current.Comment.Trim();
                    current = new Commit();
                    current.Index = index++;
                    commits.Add(current);
                    current.CommitID = log.Substring(7);
                }
                else if (log.StartsWith("Author:"))
                    current.Author = log.Substring(7);
                else if (log.StartsWith("Date:"))
                    current.Date = log.Substring(5);
                else if (log.StartsWith("    "))
                    current.Comment += log.Trim() + "\r\n";
            }
            _refreshing = true;
            gridCommits.DataSource = commits;
            _refreshing = false;
            gridCommits_SelectionChanged(gridCommits, new EventArgs());
        }

        private Tuple<Commit, Commit> getCommitsToCompare()
        {
            var selectedRows = gridCommits.SelectedRows.OfType<DataGridViewRow>()
                    .OrderBy(c => (c.DataBoundItem as Commit).Index);
            if (selectedRows.Count() < 1)
            {
                return null;
            }

            var toRow = selectedRows.First();
            DataGridViewRow fromRow = null;
            if (selectedRows.Count() > 1)
            {
                fromRow = selectedRows.Last();
            }
            else
            {
                var rowIndex = gridCommits.Rows.IndexOf(toRow);
                if (rowIndex + 1 < gridCommits.Rows.Count)
                    fromRow = gridCommits.Rows[rowIndex + 1];
            }

            return new Tuple<Commit, Commit>(fromRow == null ? null : fromRow.DataBoundItem as Commit, toRow.DataBoundItem as Commit);
        }

        private bool _refreshing = false;
        private void gridCommits_SelectionChanged(object sender, EventArgs e)
        {
            if (_refreshing) return;

            var commitsToCompare = getCommitsToCompare();
            if (commitsToCompare == null)
            {
                txtDifferences.Text = string.Empty;
                return;
            }

            var selectedFile = tvFiles.SelectedNode == null ? null : tvFiles.SelectedNode.Tag;
            if (selectedFile == null)
            {
                txtDifferences.Text = string.Empty;
                return;
            }

            var diffs = Helper.RunCommand("--no-pager diff " +
                (commitsToCompare.Item1 != null ? commitsToCompare.Item1.CommitID.Split(' ').First() + " " : "") +
                commitsToCompare.Item2.CommitID.Split(' ').First() + " -- " + selectedFile.ToString());
            txtDifferences.Text = string.Join("\r\n", diffs);
        }

        private void gridDetails_DoubleClick(object sender, EventArgs e)
        {
            externalCompareToolStripMenuItem_Click(sender, e);
        }

        private void externalCompareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settings = Common.SettingsHelper.GetUserSettings<GitUserSettings>();
            if (string.IsNullOrEmpty(settings.ExternalDiffApplication))
            {
                MessageBox.Show("No external diff application has been setup!");
                return;
            }

            var selectedFile = tvFiles.SelectedNode == null || tvFiles.SelectedNode.Tag == null ? null : tvFiles.SelectedNode.Tag.ToString().Replace("\\", "/");
            if (Helper.SshConnection != null) selectedFile = selectedFile.Substring(Helper.SshConnection.Path.Length + 1);
            var commitsToCompare = getCommitsToCompare();
            if (commitsToCompare == null || commitsToCompare.Item1 == null)
            {
                txtDifferences.Text = string.Empty;
                return;
            }

            var content1 = Helper.RunCommand("--no-pager show " + commitsToCompare.Item2.CommitID.Split(' ').First() + ":\"" + selectedFile + "\"");
            var content2 = Helper.RunCommand("--no-pager show " + commitsToCompare.Item1.CommitID.Split(' ').First() + ":\"" + selectedFile + "\"");

            var tmpDir = Path.Combine(Path.GetTempPath(), "GitStudio");
            if (!Directory.Exists(tmpDir)) Directory.CreateDirectory(tmpDir);
            var tmpFile1 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
            var tmpFile2 = Path.Combine(tmpDir, Guid.NewGuid() + ".tmp");
            File.WriteAllLines(tmpFile1, content1);
            File.WriteAllLines(tmpFile2, content2);
            Process.Start(settings.ExternalDiffApplication, string.Format(settings.ExternalDiffArgumentsFormat, tmpFile1, tmpFile2));
        }

        private void gridCommits_DoubleClick(object sender, EventArgs e)
        {
            externalCompareToolStripMenuItem_Click(sender, e);
        }

        private void tvFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (e.Node.Tag != null)
            //	selectFile(e.Node.Tag.ToString());
        }

        private void tvFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "__")
            {
                e.Node.Nodes.Clear();
                if (this.Helper.SshConnection != null)
                {
                    refreshSshFiles(e.Node.Tag.ToString(), e.Node.Nodes);
                }
                else
                {
                    refreshFiles(e.Node.Tag as DirectoryInfo, e.Node.Nodes);
                }
            }
        }

        private void lineHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvFiles.SelectedNode == null) return;
            var lineHistory = new frmLineHistory();
            lineHistory.Helper = this.Helper;
            lineHistory.SelectedFile = tvFiles.SelectedNode.Tag.ToString();
            lineHistory.Show();
        }

        private void HistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvFiles.SelectedNode == null) return;
            selectFile(tvFiles.SelectedNode.Tag.ToString());
        }
    }
}
