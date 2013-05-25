using ArachNGIN.Files.Streams;
using MonoTorrent;
using MonoTorrent.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Torchiver.Archiver.DBModel;
using Torchiver.Archiver.Properties;

namespace Torchiver.Archiver.Forms
{
    public partial class MainForm : Form
    {
        private bool _isConnected;

        public MainForm()
        {
            InitializeComponent();
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                if (!_isConnected) ConnectMNU.Text = "Connect";
                else ConnectMNU.Text = "Disconnect";
            }
        }

        public static void Log(string logstr, bool withtime = false)
        {
            var s = logstr;
            if (withtime) s = DateTime.Now.ToString() + " " + s;
            Program.Mainform.TextLOG.BeginInvoke(
                (MethodInvoker) delegate { Program.Mainform.TextLOG.AppendText(s + Environment.NewLine); }
                );
        }

        private static bool InsertTorrentToDb(string torrentfile)
        {
            var r = false;
            var fs = new FileStream(torrentfile, FileMode.Open, FileAccess.Read);
            var rawdata = new byte[fs.Length];
            fs.Read(rawdata, 0, (int) fs.Length);
            fs.Close();
            var torrent = Torrent.Load(rawdata);
            var dbt = new TorrentInfo
                          {
                              Name = torrent.Name,
                              FileName = Path.GetFileName(torrent.TorrentPath),
                              CreatedBy = torrent.CreatedBy,
                              CreatedDate = torrent.CreationDate,
                              BlockSize = torrent.PieceLength,
                              BlockCount = torrent.Pieces.Count,
                              Sha = StringUtils.ByteArrayToString(torrent.SHA1),
                              Ed2K = StringUtils.ByteArrayToString(torrent.ED2K),
                              MagnetUrl = "no magnet url!",
                              FileCount = torrent.Files.Length,
                              TotalSize = torrent.Size,
                              IsPrivate = torrent.IsPrivate
                          };
            //
            Program.Data.Infos.Add(dbt);
            //
            foreach (var b in GetTrackers(torrent))
            {
                var tracker = new TorrentTrackers();
                tracker.Torrent = dbt;
                Program.Data.Trackers.Add(tracker);
            }
            //
            var bl = new TorrentBlobs();
            bl.Torrent = dbt;
            bl.Blob = rawdata;
            bl.Name = Path.GetFileName(torrent.TorrentPath);
            Program.Data.Blobs.Add(bl);
            // a ted si dame soubory
            foreach (var singlefile in torrent.Files)
            {
                var tf = new TorrentFiles
                             {
                                 Torrent = dbt,
                                 Ed2K = StringUtils.ByteArrayToString(singlefile.ED2K),
                                 StartPiece = singlefile.StartPieceIndex,
                                 EndPiece = singlefile.EndPieceIndex,
                                 Path = singlefile.Path,
                                 Size = singlefile.Length,
                                 Md5 = StringUtils.ByteArrayToString(singlefile.MD5),
                                 Sha = StringUtils.ByteArrayToString(singlefile.SHA1)
                             };
                Program.Data.Files.Add(tf);
            }
            return true;
        }

        private bool CheckConnected()
        {
            if (IsConnected) return true;
            else throw new Exception("Not Connected!");
        }

        private static StringCollection GetTrackers(Torrent t)
        {
            var result = new StringCollection();
            result.Clear();
            foreach (RawTrackerTier tier in t.AnnounceUrls)
            {
                foreach (string tt in tier)
                {
                    result.Add(tt);
                }
            }
            return result;
        }

        private void ConnectionInfoMnuClick(object sender, EventArgs e)
        {
            using (var lfm = new logininfo_form())
            {
                lfm.LoginInfoTB.Lines = Settings.Default.torchiverConnectionString.Split(';');
                lfm.LoginInfoTB.SelectionStart = 0;
                lfm.LoginInfoTB.SelectionLength = 0;
                if (lfm.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default["torchiverConnectionString"] = string.Join(";", lfm.LoginInfoTB.Lines);
                }
            }
        }

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            // TODO: Tento řádek načte data do tabulky 'torchiverDataSet.torrent_info'. Můžete jej přesunout nebo jej odstranit podle potřeby.
            //this.torrent_infoTableAdapter.Fill(this.torchiverDataSet.torrent_info);
            // TODO: Tento řádek načte data do tabulky 'torchiverDataSet.DataTable1'. Můžete jej přesunout nebo jej odstranit podle potřeby.
        }

        private void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void TestbuttonToolStripMenuItemClick(object sender, EventArgs e)
        {
        }

        private void InsertTorrentDir(string dir)
        {
            var di = new DirectoryInfo(dir);
            var fi2 = di.GetFiles("*.torrent", SearchOption.AllDirectories);
            InsertTorrentsOnBackground(fi2);
        }

        private void InsertTorrentsOnBackground(ICollection<FileInfo> fi2)
        {
            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            var lbl = new ToolStripLabel();
            var bar = new ToolStripProgressBar();
            lbl.Text = string.Format("Importing {0}...", fi2.Count);
            bar.Width = 70;
            bar.Minimum = 0;
            bar.Maximum = 100;
            statusStrip1.Items.AddRange(new ToolStripItem[] {lbl, bar});
            statusStrip1.PerformLayout();
            bw.DoWork += delegate
                             {
                                 Log("inserting torrents start", true);
                                 int p = 0;
                                 foreach (FileInfo fi in fi2)
                                 {
                                     InsertTorrentToDb(fi.FullName);
                                     p++;
                                     bw.ReportProgress((int) ((p/(double) fi2.Count)*100));
                                 }
                                 Log("inserting torrents stop", true);
                             };
            bw.RunWorkerCompleted += delegate
                                         {
                                             Log("inserting torrents stop 2", true);
                                             try
                                             {
                                                 lbl.Dispose();
                                                 bar.Dispose();
                                             }
                                             catch
                                             {
                                             }
                                             MessageBox.Show("done");
                                         };
            bw.ProgressChanged +=
                delegate(object o, ProgressChangedEventArgs args) { bar.Value = args.ProgressPercentage; };
            bw.RunWorkerAsync();
        }


        private void DataGridView1RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LoadFilesFromTorrent((int) dataGridView1.Rows[e.RowIndex].Cells.GetCellValueFromColumnHeader("info_id"));
            }
            catch
            {
            }
        }

        private void LoadFilesFromTorrent(int infoId)
        {
            var tlist = from l in Program.Data.Files where l.Torrent.InfoId == infoId select l;
            var li = new List<string>();
            foreach (var t in tlist)
            {
                li.Add(t.Path);
            }
            StringUtils.PopulateTreeViewByFiles(FilesTREE,li,'\\');
            FilesTREE.ExpandAll();
        }


        private bool RefreshDatagrid()
        {
            dataGridView1.DataSource = Program.Data.Infos.ToList();
            return true;
        }

        private void ConnectMnuClick(object sender, EventArgs e)
        {
            IsConnected = RefreshDatagrid();
            // not really disconnecting...
        }

        private void DataGridView1CellContentClick1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ImportTorrentsMnuFilesClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fi = new FileInfo[openFileDialog1.FileNames.Length];
                for (int i = 0; i < openFileDialog1.FileNames.Length; i++)
                {
                    fi[i] = new FileInfo(openFileDialog1.FileNames[i]);
                }
                openFileDialog1.InitialDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                InsertTorrentsOnBackground(fi);
            }
        }

        private void ImportTorrentsMnuFolderClick(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                InsertTorrentDir(folderBrowserDialog1.SelectedPath);
            }
        }

        private void ImportTorrentsMnuClick(object sender, EventArgs e)
        {
        }
    }
}