using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ArachNGIN.Files.Strings;

namespace Torchiver.TempMover.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtPath.Text = Program.TempM.TempDir;
            if (Program.aSettings.ScanPath != String.Empty) txtPath.Text = Program.aSettings.ScanPath;
            //txtPath.Text=StringUtils.strAddSlash(txtPath.Text);
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            if (txtPath.Text != String.Empty) Program.aSettings.ScanPath = txtPath.Text;
        }
    }
}
