using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ArachNGIN.Files.Streams;
using MonoTorrent;
using MonoTorrent.Common;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using Torchiver.Archiver.Properties;

namespace Torchiver.Archiver.Forms
{
    public partial class MainForm : Form
    {
        private bool is_connected;

        public MainForm()
        {
            InitializeComponent();
        }

        public bool IsConnected
        {
            get { return is_connected; }
            set
            {
                is_connected = value;
                if (!is_connected) ConnectMNU.Text = "Connect";
                else ConnectMNU.Text = "Disconnect";
            }
        }

        public static void Log(string logstr, bool withtime = false)
        {
            string s = logstr;
            if (withtime) s = DateTime.Now.ToString() + " " + s;
            Program.mainform.TextLOG.BeginInvoke(
                (MethodInvoker) delegate { Program.mainform.TextLOG.AppendText(s + Environment.NewLine); }
                );
        }

        private static bool InsertTorrentToDB(string torrentfile)
        {
            using (var conn = new MySqlConnection())
            {
                bool r = false;
                Stream fs = new FileStream(torrentfile, FileMode.Open, FileAccess.Read);
                var rawdata = new byte[fs.Length];
                fs.Read(rawdata, 0, (int) fs.Length);
                fs.Close();
                Torrent torrent = Torrent.Load(torrentfile);

                //nejdriv info
                var cmd_info = new MySqlCommand();
                string SQL = "set autocommit=0;\n";
                SQL += "INSERT INTO `torchiver`.`torrent_info`\n";
                SQL +=
                    "(`info_name`,`info_file`, `info_created_date`, `info_created_program`, `info_block_size`, `info_block_count`, `info_sha`, `info_ED2K`, `info_magneturl`, `info_filecount`, `info_totalsize`, `info_comment`, `info_is_private`, `info_trackers`)\n";
                SQL +=
                    "VALUES (@info_name, @info_file, @info_created_date, @info_created_program, @info_block_size, @info_block_count, @info_sha, @info_ed2k, @info_magneturl, @info_filecount, @info_totalsize, @info_comment, @info_is_private, @info_trackers)\n";
                //
                cmd_info.Parameters.Add(new MySqlParameter("@info_name", torrent.Name));
                cmd_info.Parameters.Add(new MySqlParameter("@info_file", Path.GetFileName(torrent.TorrentPath)));
                cmd_info.Parameters.Add(new MySqlParameter("@info_created_program", torrent.CreatedBy));
                var tm = new MySqlDateTime(torrent.CreationDate);
                cmd_info.Parameters.Add(new MySqlParameter("@info_created_date",
                                                           torrent.CreationDate.ToString("yyyy-MM-dd HH:mm")));
                cmd_info.Parameters.Add(new MySqlParameter("@info_block_size", torrent.PieceLength));
                cmd_info.Parameters.Add(new MySqlParameter("@info_block_count", torrent.Pieces.Count));
                cmd_info.Parameters.Add(new MySqlParameter("@info_sha", StringUtils.ByteArrayToString(torrent.SHA1)));
                cmd_info.Parameters.Add(new MySqlParameter("@info_ed2k", StringUtils.ByteArrayToString(torrent.ED2K)));
                cmd_info.Parameters.Add(new MySqlParameter("@info_magneturl", "no magnet url bytch!"));
                cmd_info.Parameters.Add(new MySqlParameter("@info_filecount", torrent.Files.Length));
                cmd_info.Parameters.Add(new MySqlParameter("@info_totalsize", torrent.Size));
                cmd_info.Parameters.Add(new MySqlParameter("@info_comment", torrent.Comment));
                cmd_info.Parameters.Add(new MySqlParameter("@info_is_private", torrent.IsPrivate));
                cmd_info.Parameters.Add(new MySqlParameter("@info_trackers",
                                                           StringCollections.StringCollectionToString(
                                                               GetTrackers(torrent))));

                conn.ConnectionString = Settings.Default.torchiverConnectionString;
                conn.Open();
                // otevrit transakci
                MySqlTransaction transakce = conn.BeginTransaction();
                cmd_info.CommandText = SQL;
                cmd_info.Connection = conn;
                cmd_info.Prepare();

                try
                {
                    cmd_info.ExecuteNonQuery();
                    r = true;
                }
                catch (MySqlException ex)
                {
                    r = false;
                    transakce.Rollback();
                    conn.Close();
                    MessageBox.Show(ex.ErrorCode + "\n" + ex.Message);
                    return r;
                }

                // zjistit info_id. to bude HODNE potreba
                long info_id = cmd_info.LastInsertedId;

                // blob
                var cmd_blob = new MySqlCommand();
                SQL =
                    "INSERT INTO `torchiver`.`torrent_blobs` (`info_id`, `blob_name`,`blob_blob`) VALUES (@info_id, @blob_name, @blob_blob);";
                cmd_blob.Parameters.Clear();
                var p = new MySqlParameter("@info_id", info_id);
                cmd_blob.Parameters.Add(p);
                p = new MySqlParameter("@blob_name", Path.GetFileName(torrent.TorrentPath));
                cmd_blob.Parameters.Add(p);
                p = new MySqlParameter("@blob_blob", rawdata);
                cmd_blob.Parameters.Add(p);
                cmd_blob.CommandText = SQL;
                cmd_blob.Connection = conn;
                cmd_blob.Prepare();
                try
                {
                    cmd_blob.ExecuteNonQuery();
                    r = true;
                }
                catch (MySqlException ex)
                {
                    r = false;
                    transakce.Rollback();
                    conn.Close();
                    MessageBox.Show(ex.ErrorCode + "\n" + ex.Message);
                    return r;
                }

                // a ted si dame soubory
                foreach (TorrentFile singlefile in torrent.Files)
                {
                    var cmd_file = new MySqlCommand();
                    cmd_file.CommandText =
                        "INSERT INTO `torchiver`.`torrent_files` (`info_id`, `file_ED2K`, `file_startpiece`, `file_endpiece`, `file_path`, `file_length`, `file_MD5`, `file_SHA1`)\n";
                    cmd_file.CommandText +=
                        "VALUES (@info_id, @file_ed2k, @file_startpiece, @file_endpiece, @file_path, @file_length, @file_md5, @file_sha1)";
                    cmd_file.Parameters.Add(new MySqlParameter("@info_id", info_id));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_ed2k",
                                                               StringUtils.ByteArrayToString(singlefile.ED2K)));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_startpiece", singlefile.StartPieceIndex));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_endpiece", singlefile.EndPieceIndex));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_path", singlefile.Path));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_length", singlefile.Length));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_md5",
                                                               StringUtils.ByteArrayToString(singlefile.MD5)));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_sha1",
                                                               StringUtils.ByteArrayToString(singlefile.SHA1)));
                    cmd_file.Connection = conn;
                    cmd_file.Prepare();
                    try
                    {
                        cmd_file.ExecuteNonQuery();
                        r = true;
                    }
                    catch (MySqlException ex)
                    {
                        r = false;
                        transakce.Rollback();
                        conn.Close();
                        MessageBox.Show(ex.ErrorCode + "\n" + ex.Message);
                        return r;
                    }
                }

                if (r) transakce.Commit();
                else transakce.Rollback();
                conn.Close();
                return r;
            }
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

        private void ConnectionInfoMNU_Click(object sender, EventArgs e)
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: Tento řádek načte data do tabulky 'torchiverDataSet.torrent_info'. Můžete jej přesunout nebo jej odstranit podle potřeby.
            //this.torrent_infoTableAdapter.Fill(this.torchiverDataSet.torrent_info);
            // TODO: Tento řádek načte data do tabulky 'torchiverDataSet.DataTable1'. Můžete jej přesunout nebo jej odstranit podle potřeby.
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void testbuttonToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void InsertTorrentDir(string dir)
        {
            var di = new DirectoryInfo(dir);
            FileInfo[] fi2 = di.GetFiles("*.torrent", SearchOption.AllDirectories);
            InsertTorrentsOnBackground(fi2);
        }

        private void InsertTorrentsOnBackground(FileInfo[] fi2)
        {
            var bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            var lbl = new ToolStripLabel();
            var bar = new ToolStripProgressBar();
            lbl.Text = string.Format("Importing {0}...", fi2.Length);
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
                                     InsertTorrentToDB(fi.FullName);
                                     p++;
                                     bw.ReportProgress((int) ((p/(double) fi2.Length)*100));
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


        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LoadFilesFromTorrent((int) dataGridView1.Rows[e.RowIndex].Cells.GetCellValueFromColumnHeader("info_id"));
            }
            catch
            {
            }
        }

        private void LoadFilesFromTorrent(int info_id)
        {
            using (var con = new MySqlConnection(Settings.Default.torchiverConnectionString))
            {
                FilesTREE.Nodes.Clear();
                con.Open();
                using (
                    var cmd = new MySqlCommand("select * from torrent_files where info_id = " + info_id.ToString(), con)
                    )
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StringUtils.PopulateTreeViewByFiles(FilesTREE,
                                                                new List<string> {reader.GetString("file_path")}, '\\');
                        }
                        reader.Close();
                    }
                    FilesTREE.ExpandAll();
                }
                con.Close();
            }
        }


        private bool RefreshDatagrid()
        {
            bool result = false;
            try
            {
                var adapter = new MySqlDataAdapter("select * from torrent_info",
                                                   Settings.Default.torchiverConnectionString);
                var dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                /*using (MySqlConnection conn = new MySqlConnection())
                 */
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message);
                Log(ex.Message, true);
            }
            return result;
        }

        private void ConnectMNU_Click(object sender, EventArgs e)
        {
            IsConnected = RefreshDatagrid();
            // not really disconnecting...
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void ImportTorrentsMNUFiles_Click(object sender, EventArgs e)
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

        private void ImportTorrentsMNUFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                InsertTorrentDir(folderBrowserDialog1.SelectedPath);
            }
        }

        private void ImportTorrentsMNU_Click(object sender, EventArgs e)
        {
        }
    }

    public static class datagridhelper
    {
        public static object GetCellValueFromColumnHeader(this DataGridViewCellCollection CellCollection,
                                                          string HeaderText)
        {
            return CellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == HeaderText).Value;
        }
    }
}