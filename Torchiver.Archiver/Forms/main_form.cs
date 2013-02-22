using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArachNGIN.Files.Strings;
using MonoTorrent;
using MonoTorrent.Common;
using MySql.Data.MySqlClient;
using MySql.Data.Types;


namespace Torchiver.Archiver.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"c:\xx\");
            FileInfo[] fi2 = di.GetFiles("*.torrent", SearchOption.AllDirectories);

            foreach (FileInfo fi in fi2)
            {
                this.Text=fi.FullName;
                Application.DoEvents();
                InsertTorrent(fi.FullName);
                Application.DoEvents();
            }

            MessageBox.Show("doneeee");
        }

        private static bool InsertTorrent(string torrentfile)
        {
            bool r = false;
            Stream fs = new FileStream(torrentfile, FileMode.Open, FileAccess.Read);
            byte[] rawdata = new byte[fs.Length];
            fs.Read(rawdata, 0, (int)fs.Length);
            fs.Close();
            MonoTorrent.Common.Torrent t = MonoTorrent.Common.Torrent.Load(torrentfile);

            using (MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection())
            {
                //nejdriv info
                MySqlCommand cmd_info = new MySql.Data.MySqlClient.MySqlCommand();
                string SQL = "INSERT INTO `torchiver`.`torrent_info`\n";
                SQL += "(`info_name`,`info_file`, `info_created_date`, `info_created_program`, `info_block_size`, `info_block_count`, `info_sha`, `info_ED2K`, `info_magneturl`, `info_filecount`, `info_totalsize`, `info_comment`, `info_is_private`)\n";
                SQL += "VALUES (@info_name, @info_file, @info_created_date, @info_created_program, @info_block_size, @info_block_count, @info_sha, @info_ed2k, @info_magneturl, @info_filecount, @info_totalsize, @info_comment, @info_is_private)\n";
                //
                MySqlParameter file = new MySqlParameter("@info_name", t.Name);
                cmd_info.Parameters.Add(file);
                MySqlParameter dir = new MySqlParameter("@info_file", Path.GetFileName(t.TorrentPath));
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_created_program", t.CreatedBy);
                cmd_info.Parameters.Add(dir);
                MySqlDateTime tm = new MySqlDateTime(t.CreationDate);
                dir = new MySqlParameter("@info_created_date", t.CreationDate.ToString("yyyy-MM-dd HH:mm"));
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_block_size", t.PieceLength);
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_block_count", t.Pieces.Count);
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_sha", StringUtils.ByteArrayToString(t.SHA1));
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_ed2k", StringUtils.ByteArrayToString(t.ED2K));
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_magneturl", "no magnet url bytch!");
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_filecount", t.Files.Length);
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_totalsize", t.Size);
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_comment", t.Comment);
                cmd_info.Parameters.Add(dir);
                dir = new MySqlParameter("@info_is_private", t.IsPrivate);
                cmd_info.Parameters.Add(dir);
                
                conn.ConnectionString = Properties.Settings.Default.torchiverConnectionString;
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
                catch (MySql.Data.MySqlClient.MySqlException ex)
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
                MySqlCommand cmd_blob = new MySql.Data.MySqlClient.MySqlCommand();
                SQL = "INSERT INTO `torchiver`.`torrent_blobs` (`info_id`, `blob_name`,`blob_blob`) VALUES (@info_id, @blob_name, @blob_blob);";
                cmd_blob.Parameters.Clear();
                MySqlParameter p = new MySqlParameter("@info_id", info_id);
                cmd_blob.Parameters.Add(p);
                p = new MySqlParameter("@blob_name", Path.GetFileName(t.TorrentPath));
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
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    r = false;
                    transakce.Rollback();
                    conn.Close();
                    MessageBox.Show(ex.ErrorCode + "\n" + ex.Message);
                    return r;
                }

                // a ted si dame soubory
                foreach (TorrentFile singlefile in t.Files)
                {
                    MySqlCommand cmd_file = new MySqlCommand();
                    cmd_file.CommandText = "INSERT INTO `torchiver`.`torrent_files` (`info_id`, `file_ED2K`, `file_startpiece`, `file_endpiece`, `file_path`, `file_length`, `file_MD5`, `file_SHA1`)\n";
                    cmd_file.CommandText += "VALUES (@info_id, @file_ed2k, @file_startpiece, @file_endpiece, @file_path, @file_length, @file_md5, @file_sha1)";
                    cmd_file.Parameters.Add(new MySqlParameter("@info_id", info_id));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_ed2k", StringUtils.ByteArrayToString(singlefile.ED2K)));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_startpiece", singlefile.StartPieceIndex));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_endpiece", singlefile.EndPieceIndex));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_path", singlefile.Path));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_length", singlefile.Length));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_md5", StringUtils.ByteArrayToString(singlefile.MD5)));
                    cmd_file.Parameters.Add(new MySqlParameter("@file_sha1", StringUtils.ByteArrayToString(singlefile.SHA1)));
                    cmd_file.Connection = conn;
                    cmd_file.Prepare();
                    try
                    {
                        cmd_file.ExecuteNonQuery();
                        r = true;
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
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
    }
}
