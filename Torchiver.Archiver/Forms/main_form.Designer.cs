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
            this.ConnectMNU = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectionInfoMNU = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTorrentsMNU = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTorrentsMNUFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.ImportTorrentsMNUFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.testbuttonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PageData = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TrackersTXT = new System.Windows.Forms.TextBox();
            this.FilesTREE = new System.Windows.Forms.TreeView();
            this.PageLog = new System.Windows.Forms.TabPage();
            this.TextLOG = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PageData.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.PageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectMNU,
            this.ConnectionInfoMNU,
            this.ImportTorrentsMNU,
            this.testbuttonToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1074, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ConnectMNU
            // 
            this.ConnectMNU.Name = "ConnectMNU";
            this.ConnectMNU.Size = new System.Drawing.Size(64, 20);
            this.ConnectMNU.Text = "Connect";
            this.ConnectMNU.Click += new System.EventHandler(this.ConnectMNU_Click);
            // 
            // ConnectionInfoMNU
            // 
            this.ConnectionInfoMNU.Name = "ConnectionInfoMNU";
            this.ConnectionInfoMNU.Size = new System.Drawing.Size(105, 20);
            this.ConnectionInfoMNU.Text = "Connection Info";
            this.ConnectionInfoMNU.Click += new System.EventHandler(this.ConnectionInfoMNU_Click);
            // 
            // ImportTorrentsMNU
            // 
            this.ImportTorrentsMNU.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportTorrentsMNUFiles,
            this.ImportTorrentsMNUFolder});
            this.ImportTorrentsMNU.Name = "ImportTorrentsMNU";
            this.ImportTorrentsMNU.Size = new System.Drawing.Size(102, 20);
            this.ImportTorrentsMNU.Text = "Import Torrents";
            this.ImportTorrentsMNU.Click += new System.EventHandler(this.ImportTorrentsMNU_Click);
            // 
            // ImportTorrentsMNUFiles
            // 
            this.ImportTorrentsMNUFiles.Name = "ImportTorrentsMNUFiles";
            this.ImportTorrentsMNUFiles.Size = new System.Drawing.Size(152, 22);
            this.ImportTorrentsMNUFiles.Text = "Select files...";
            this.ImportTorrentsMNUFiles.Click += new System.EventHandler(this.ImportTorrentsMNUFiles_Click);
            // 
            // ImportTorrentsMNUFolder
            // 
            this.ImportTorrentsMNUFolder.Name = "ImportTorrentsMNUFolder";
            this.ImportTorrentsMNUFolder.Size = new System.Drawing.Size(152, 22);
            this.ImportTorrentsMNUFolder.Text = "Select folder...";
            this.ImportTorrentsMNUFolder.Click += new System.EventHandler(this.ImportTorrentsMNUFolder_Click);
            // 
            // testbuttonToolStripMenuItem
            // 
            this.testbuttonToolStripMenuItem.Name = "testbuttonToolStripMenuItem";
            this.testbuttonToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.testbuttonToolStripMenuItem.Text = "testbutton";
            this.testbuttonToolStripMenuItem.Click += new System.EventHandler(this.testbuttonToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1074, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1074, 408);
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
            this.tabControl1.Size = new System.Drawing.Size(1074, 408);
            this.tabControl1.TabIndex = 5;
            // 
            // PageData
            // 
            this.PageData.Controls.Add(this.splitContainer1);
            this.PageData.Location = new System.Drawing.Point(4, 22);
            this.PageData.Name = "PageData";
            this.PageData.Padding = new System.Windows.Forms.Padding(3);
            this.PageData.Size = new System.Drawing.Size(1066, 382);
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
            this.splitContainer1.Size = new System.Drawing.Size(1060, 376);
            this.splitContainer1.SplitterDistance = 215;
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
            this.dataGridView1.Size = new System.Drawing.Size(1060, 215);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.TrackersTXT);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.FilesTREE);
            this.splitContainer2.Size = new System.Drawing.Size(1060, 157);
            this.splitContainer2.SplitterDistance = 353;
            this.splitContainer2.TabIndex = 0;
            // 
            // TrackersTXT
            // 
            this.TrackersTXT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TrackersTXT.Location = new System.Drawing.Point(0, 0);
            this.TrackersTXT.Multiline = true;
            this.TrackersTXT.Name = "TrackersTXT";
            this.TrackersTXT.ReadOnly = true;
            this.TrackersTXT.Size = new System.Drawing.Size(353, 157);
            this.TrackersTXT.TabIndex = 0;
            // 
            // FilesTREE
            // 
            this.FilesTREE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FilesTREE.Location = new System.Drawing.Point(0, 0);
            this.FilesTREE.Name = "FilesTREE";
            this.FilesTREE.Size = new System.Drawing.Size(703, 157);
            this.FilesTREE.TabIndex = 0;
            // 
            // PageLog
            // 
            this.PageLog.Controls.Add(this.TextLOG);
            this.PageLog.Location = new System.Drawing.Point(4, 22);
            this.PageLog.Name = "PageLog";
            this.PageLog.Padding = new System.Windows.Forms.Padding(3);
            this.PageLog.Size = new System.Drawing.Size(1066, 382);
            this.PageLog.TabIndex = 1;
            this.PageLog.Text = "Log";
            this.PageLog.UseVisualStyleBackColor = true;
            // 
            // TextLOG
            // 
            this.TextLOG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextLOG.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.TextLOG.Location = new System.Drawing.Point(3, 3);
            this.TextLOG.Multiline = true;
            this.TextLOG.Name = "TextLOG";
            this.TextLOG.ReadOnly = true;
            this.TextLOG.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextLOG.Size = new System.Drawing.Size(1060, 376);
            this.TextLOG.TabIndex = 0;
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
            this.ClientSize = new System.Drawing.Size(1074, 454);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.PageData.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.PageLog.ResumeLayout(false);
            this.PageLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConnectMNU;
        private System.Windows.Forms.ToolStripMenuItem ConnectionInfoMNU;
        private System.Windows.Forms.ToolStripMenuItem testbuttonToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PageData;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox TrackersTXT;
        private System.Windows.Forms.TreeView FilesTREE;
        private System.Windows.Forms.TabPage PageLog;
        private System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.TextBox TextLOG;
        private System.Windows.Forms.ToolStripMenuItem ImportTorrentsMNU;
        private System.Windows.Forms.ToolStripMenuItem ImportTorrentsMNUFiles;
        private System.Windows.Forms.ToolStripMenuItem ImportTorrentsMNUFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

