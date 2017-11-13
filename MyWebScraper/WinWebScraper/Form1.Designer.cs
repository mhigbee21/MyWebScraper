namespace WinWebScraper
{
    partial class Form1
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveUrlBtn = new System.Windows.Forms.Button();
            this.urlInput = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadDirLbl = new System.Windows.Forms.Label();
            this.selectDownloadDirBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveUrlBtn
            // 
            this.saveUrlBtn.Location = new System.Drawing.Point(408, 60);
            this.saveUrlBtn.Name = "saveUrlBtn";
            this.saveUrlBtn.Size = new System.Drawing.Size(167, 23);
            this.saveUrlBtn.TabIndex = 0;
            this.saveUrlBtn.Text = "Save Url";
            this.saveUrlBtn.UseVisualStyleBackColor = true;
            this.saveUrlBtn.Click += new System.EventHandler(this.saveUrlBtn_Click);
            // 
            // urlInput
            // 
            this.urlInput.Location = new System.Drawing.Point(41, 62);
            this.urlInput.Name = "urlInput";
            this.urlInput.Size = new System.Drawing.Size(361, 20);
            this.urlInput.TabIndex = 1;
            this.urlInput.Text = "http://";
            // 
            // downloadDirLbl
            // 
            this.downloadDirLbl.AutoSize = true;
            this.downloadDirLbl.Location = new System.Drawing.Point(38, 31);
            this.downloadDirLbl.Name = "downloadDirLbl";
            this.downloadDirLbl.Size = new System.Drawing.Size(111, 13);
            this.downloadDirLbl.TabIndex = 2;
            this.downloadDirLbl.Text = "No Directory Selected";
            // 
            // selectDownloadDirBtn
            // 
            this.selectDownloadDirBtn.Location = new System.Drawing.Point(408, 31);
            this.selectDownloadDirBtn.Name = "selectDownloadDirBtn";
            this.selectDownloadDirBtn.Size = new System.Drawing.Size(167, 23);
            this.selectDownloadDirBtn.TabIndex = 3;
            this.selectDownloadDirBtn.Text = "Select Download Directory";
            this.selectDownloadDirBtn.UseVisualStyleBackColor = true;
            this.selectDownloadDirBtn.Click += new System.EventHandler(this.selectDownloadDirBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 189);
            this.Controls.Add(this.selectDownloadDirBtn);
            this.Controls.Add(this.downloadDirLbl);
            this.Controls.Add(this.urlInput);
            this.Controls.Add(this.saveUrlBtn);
            this.Name = "Form1";
            this.Text = "Web Scraper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button saveUrlBtn;
        private System.Windows.Forms.TextBox urlInput;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label downloadDirLbl;
        private System.Windows.Forms.Button selectDownloadDirBtn;
    }
}

