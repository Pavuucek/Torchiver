using System;
using System.IO;
using System.Windows.Forms;
using ArachNGIN.ClassExtensions;
using Torchiver.TempMover.Properties;

namespace Torchiver.TempMover.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Visible = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgBrowse.ShowDialog() == DialogResult.OK) txtPath1.Text = dlgBrowse.SelectedPath.AddSlash();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtPath1.Text = Program.TempM.TempDir;
            txtPath2.Text = Program.aSettings.MovePath;
            if (Program.aSettings.ScanPath != string.Empty) txtPath1.Text = Program.aSettings.ScanPath;
            txtPath1.Text = txtPath1.Text.AddSlash();
            txtPath2.Text = txtPath2.Text.AddSlash();
            dlgBrowse.SelectedPath = txtPath1.Text;
            txtPath1.TextChanged += txtPath_TextChanged;
            txtPath2.TextChanged += txtPath2_TextChanged;
            Program.aSettings.ScanPath = txtPath1.Text;
            Program.aSettings.MovePath = txtPath2.Text;
            numInterval.Value = Program.aSettings.Interval;
            trayIcon.Icon = Resources.Torchiver;
            Icon = Resources.Torchiver;
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            tmrMonitor.Enabled = true;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (txtPath1.Text != string.Empty) Program.aSettings.ScanPath = txtPath1.Text.AddSlash();
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuShow_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Show();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                ShowInTaskbar = false;
            }
            else if (FormWindowState.Normal == WindowState)
            {
                ShowInTaskbar = true;
            }
        }

        private void MainForm_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            if (dlgBrowse.ShowDialog() == DialogResult.OK) txtPath2.Text = dlgBrowse.SelectedPath.AddSlash();
        }

        private void txtPath2_TextChanged(object sender, EventArgs e)
        {
            if (txtPath2.Text != string.Empty) Program.aSettings.MovePath = txtPath2.Text.AddSlash();
        }

        private void tmrMonitor_Tick(object sender, EventArgs e)
        {
            var torrents = Directory.GetFiles(Program.aSettings.ScanPath, "*.torrent");
            if (torrents.Length > 0)
                foreach (var torrent in torrents)
                    try
                    {
                        Directory.Move(torrent, Program.aSettings.MovePath + Path.GetFileName(torrent));
                    }
                    catch
                    {
                    }
        }

        private void numInterval_ValueChanged(object sender, EventArgs e)
        {
            if (numInterval.Value <= 0) numInterval.Value = 1;
            tmrMonitor.Interval = (int) numInterval.Value * 60 * 1000;
            Program.aSettings.Interval = (int) numInterval.Value;
        }
    }
}