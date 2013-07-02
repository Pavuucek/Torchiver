namespace Torchiver.Archiver.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Vyžadovaná proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolnit všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by měl být spravovaný prostředek odstraněn; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem formulářů

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody s editorem kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ConnectMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectionInfoMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTorrentsMnu = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTorrentsMnuFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTorrentsMnuFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.testbuttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PageData = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TrackersTxt = new System.Windows.Forms.TextBox();
            this.FilesTree = new System.Windows.Forms.TreeView();
            this.PageLog = new System.Windows.Forms.TabPage();
            this.TextLog = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PageData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.PageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMnu,
            this.ConnectionInfoMnu,
            this.ImportTorrentsMnu,
            this.testbuttonToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1176, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ConnectMnu
            // 
            this.ConnectMnu.Name = "ConnectMnu";
            this.ConnectMnu.Size = new System.Drawing.Size(64, 20);
            this.ConnectMnu.Text = "Connect";
            this.ConnectMnu.Click += new System.EventHandler(this.ConnectMnuClick);
            // 
            // ConnectionInfoMnu
            // 
            this.ConnectionInfoMnu.Name = "ConnectionInfoMnu";
            this.ConnectionInfoMnu.Size = new System.Drawing.Size(105, 20);
            this.ConnectionInfoMnu.Text = "Connection Info";
            this.ConnectionInfoMnu.Click += new System.EventHandler(this.ConnectionInfoMnuClick);
            // 
            // ImportTorrentsMnu
            // 
            this.ImportTorrentsMnu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportTorrentsMnuFiles,
            this.ImportTorrentsMnuFolder});
            this.ImportTorrentsMnu.Name = "ImportTorrentsMnu";
            this.ImportTorrentsMnu.Size = new System.Drawing.Size(102, 20);
            this.ImportTorrentsMnu.Text = "Import Torrents";
            this.ImportTorrentsMnu.Click += new System.EventHandler(this.ImportTorrentsMnuClick);
            // 
            // ImportTorrentsMnuFiles
            // 
            this.ImportTorrentsMnuFiles.Name = "ImportTorrentsMnuFiles";
            this.ImportTorrentsMnuFiles.Size = new System.Drawing.Size(148, 22);
            this.ImportTorrentsMnuFiles.Text = "Select files...";
            this.ImportTorrentsMnuFiles.Click += new System.EventHandler(this.ImportTorrentsMnuFilesClick);
            // 
            // ImportTorrentsMnuFolder
            // 
            this.ImportTorrentsMnuFolder.Name = "ImportTorrentsMnuFolder";
            this.ImportTorrentsMnuFolder.Size = new System.Drawing.Size(148, 22);
            this.ImportTorrentsMnuFolder.Text = "Select folder...";
            this.ImportTorrentsMnuFolder.Click += new System.EventHandler(this.ImportTorrentsMnuFolderClick);
            // 
            // testbuttonToolStripMenuItem
            // 
            this.testbuttonToolStripMenuItem.Name = "testbuttonToolStripMenuItem";
            this.testbuttonToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.testbuttonToolStripMenuItem.Text = "testbutton";
            this.testbuttonToolStripMenuItem.Click += new System.EventHandler(this.TestbuttonToolStripMenuItemClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 478);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1176, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1176, 454);
            this.panel1.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PageData);
            this.tabControl1.Controls.Add(this.PageLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1176, 454);
            this.tabControl1.TabIndex = 5;
            // 
            // PageData
            // 
            this.PageData.Controls.Add(this.splitContainer1);
            this.PageData.Location = new System.Drawing.Point(4, 22);
            this.PageData.Name = "PageData";
            this.PageData.Padding = new System.Windows.Forms.Padding(3);
            this.PageData.Size = new System.Drawing.Size(1168, 428);
            this.PageData.TabIndex = 0;
            this.PageData.Text = "Data";
            this.PageData.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1162, 422);
            this.splitContainer1.SplitterDistance = 241;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1162, 241);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellContentClick1);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1RowEnter);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.TrackersTxt);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.FilesTree);
            this.splitContainer2.Size = new System.Drawing.Size(1162, 177);
            this.splitContainer2.SplitterDistance = 386;
            this.splitContainer2.TabIndex = 0;
            // 
            // TrackersTxt
            // 
            this.TrackersTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackersTxt.Location = new System.Drawing.Point(0, 0);
            this.TrackersTxt.Multiline = true;
            this.TrackersTxt.Name = "TrackersTxt";
            this.TrackersTxt.ReadOnly = true;
            this.TrackersTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TrackersTxt.Size = new System.Drawing.Size(386, 177);
            this.TrackersTxt.TabIndex = 0;
            // 
            // FilesTree
            // 
            this.FilesTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesTree.Location = new System.Drawing.Point(0, 0);
            this.FilesTree.Name = "FilesTree";
            this.FilesTree.Size = new System.Drawing.Size(772, 177);
            this.FilesTree.TabIndex = 0;
            // 
            // PageLog
            // 
            this.PageLog.Controls.Add(this.TextLog);
            this.PageLog.Location = new System.Drawing.Point(4, 22);
            this.PageLog.Name = "PageLog";
            this.PageLog.Padding = new System.Windows.Forms.Padding(3);
            this.PageLog.Size = new System.Drawing.Size(1168, 428);
            this.PageLog.TabIndex = 1;
            this.PageLog.Text = "Log";
            this.PageLog.UseVisualStyleBackColor = true;
            // 
            // TextLog
            // 
            this.TextLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TextLog.Location = new System.Drawing.Point(3, 3);
            this.TextLog.Multiline = true;
            this.TextLog.Name = "TextLog";
            this.TextLog.ReadOnly = true;
            this.TextLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextLog.Size = new System.Drawing.Size(1162, 422);
            this.TextLog.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Torrents|*.torrent|All Files|*.*";
            this.openFileDialog1.InitialDirectory = global::Torchiver.Archiver.Properties.Settings.Default.ImportPath;
            this.openFileDialog1.Multiselect = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.SelectedPath = global::Torchiver.Archiver.Properties.Settings.Default.ImportPath;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 500);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.PageData.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.PageLog.ResumeLayout(false);
            this.PageLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConnectMnu;
        private System.Windows.Forms.ToolStripMenuItem ConnectionInfoMnu;
        private System.Windows.Forms.ToolStripMenuItem testbuttonToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PageData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox TrackersTxt;
        private System.Windows.Forms.TreeView FilesTree;
        private System.Windows.Forms.TabPage PageLog;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox TextLog;
        private System.Windows.Forms.ToolStripMenuItem ImportTorrentsMnu;
        private System.Windows.Forms.ToolStripMenuItem ImportTorrentsMnuFiles;
        private System.Windows.Forms.ToolStripMenuItem ImportTorrentsMnuFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

