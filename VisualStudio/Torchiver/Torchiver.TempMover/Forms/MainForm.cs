﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ArachNGIN.Files.Strings;

namespace Torchiver.TempMover.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Visible = false;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (dlgBrowse.ShowDialog()==DialogResult.OK)
            {
                txtPath1.Text = StringUtils.strAddSlash(dlgBrowse.SelectedPath);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtPath1.Text = Program.TempM.TempDir;
            txtPath2.Text = Program.aSettings.MovePath;
            if (Program.aSettings.ScanPath != String.Empty) txtPath1.Text = Program.aSettings.ScanPath;
            txtPath1.Text = StringUtils.strAddSlash(txtPath1.Text);
            txtPath2.Text = StringUtils.strAddSlash(txtPath2.Text);
            dlgBrowse.SelectedPath = txtPath1.Text;
            this.txtPath1.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
            this.txtPath2.TextChanged += new System.EventHandler(this.txtPath2_TextChanged);
            trayIcon.Icon = Properties.Resources.Torchiver;
            this.Icon = Properties.Resources.Torchiver;
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (txtPath1.Text != String.Empty) Program.aSettings.ScanPath = StringUtils.strAddSlash(txtPath1.Text);
            fsWatcher.Path = Program.aSettings.ScanPath;
            fsWatcher.EnableRaisingEvents = true;
        }

        private void mnuClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuShow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                this.ShowInTaskbar = true;
            }
        }

        private void MainForm_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            if (dlgBrowse.ShowDialog() == DialogResult.OK)
            {
                txtPath2.Text = StringUtils.strAddSlash(dlgBrowse.SelectedPath);
            }
        }

        private void txtPath2_TextChanged(object sender, EventArgs e)
        {
            if (txtPath2.Text != String.Empty) Program.aSettings.MovePath = StringUtils.strAddSlash(txtPath2.Text);
            fsWatcher.EnableRaisingEvents = true;
        }
    
    }
}
