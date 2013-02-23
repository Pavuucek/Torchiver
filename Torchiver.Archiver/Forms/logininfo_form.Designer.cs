namespace Torchiver.Archiver.Forms
{
    partial class logininfo_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoginInfoTB = new System.Windows.Forms.TextBox();
            this.CreateBTN = new System.Windows.Forms.Button();
            this.OkBTN = new System.Windows.Forms.Button();
            this.CancelBTN = new System.Windows.Forms.Button();
            this.InfoLBL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoginInfoTB
            // 
            this.LoginInfoTB.Location = new System.Drawing.Point(12, 43);
            this.LoginInfoTB.Multiline = true;
            this.LoginInfoTB.Name = "LoginInfoTB";
            this.LoginInfoTB.Size = new System.Drawing.Size(260, 110);
            this.LoginInfoTB.TabIndex = 0;
            // 
            // CreateBTN
            // 
            this.CreateBTN.Location = new System.Drawing.Point(12, 159);
            this.CreateBTN.Name = "CreateBTN";
            this.CreateBTN.Size = new System.Drawing.Size(75, 23);
            this.CreateBTN.TabIndex = 2;
            this.CreateBTN.Text = "Create DB";
            this.CreateBTN.UseVisualStyleBackColor = true;
            this.CreateBTN.Click += new System.EventHandler(this.CreateBTN_Click);
            // 
            // OkBTN
            // 
            this.OkBTN.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkBTN.Location = new System.Drawing.Point(104, 159);
            this.OkBTN.Name = "OkBTN";
            this.OkBTN.Size = new System.Drawing.Size(75, 23);
            this.OkBTN.TabIndex = 3;
            this.OkBTN.Text = "OK";
            this.OkBTN.UseVisualStyleBackColor = true;
            // 
            // CancelBTN
            // 
            this.CancelBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBTN.Location = new System.Drawing.Point(197, 159);
            this.CancelBTN.Name = "CancelBTN";
            this.CancelBTN.Size = new System.Drawing.Size(75, 23);
            this.CancelBTN.TabIndex = 4;
            this.CancelBTN.Text = "Cancel";
            this.CancelBTN.UseVisualStyleBackColor = true;
            // 
            // InfoLBL
            // 
            this.InfoLBL.Location = new System.Drawing.Point(12, 9);
            this.InfoLBL.Name = "InfoLBL";
            this.InfoLBL.Size = new System.Drawing.Size(260, 31);
            this.InfoLBL.TabIndex = 5;
            this.InfoLBL.Text = "Write your connection parameters, instead of semicolons use line breaks.";
            // 
            // logininfo_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 192);
            this.Controls.Add(this.InfoLBL);
            this.Controls.Add(this.CancelBTN);
            this.Controls.Add(this.OkBTN);
            this.Controls.Add(this.CreateBTN);
            this.Controls.Add(this.LoginInfoTB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "logininfo_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Connection Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateBTN;
        private System.Windows.Forms.Button OkBTN;
        private System.Windows.Forms.Button CancelBTN;
        private System.Windows.Forms.Label InfoLBL;
        public System.Windows.Forms.TextBox LoginInfoTB;
    }
}