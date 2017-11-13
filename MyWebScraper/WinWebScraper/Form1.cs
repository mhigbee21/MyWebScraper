using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyWebScraper;


namespace WinWebScraper
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        private void saveUrlBtn_Click(object sender, EventArgs e)
        {
            WebScraper w = new WebScraper();
            string url = urlInput.Text;
            var result = w.GetUrl(url);

            if (result.html != string.Empty)
            {
                if (downloadDirLbl.Text.Equals("No Directory Selected"))
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        downloadDirLbl.Text = folderBrowserDialog1.SelectedPath;
                        w.HtmlFilePath = folderBrowserDialog1.SelectedPath;
                    }
                }
                else
                {
                    w.HtmlFilePath = downloadDirLbl.Text;
                }
                saveUrlBtn.Enabled = false;
                try
                {
                    bool saved = w.SaveUrl();
                }
                catch (Exception ex)
                {
                    saveUrlBtn.Enabled = true;
                    MessageBox.Show("There was an error: " + ex.Message);
                }
                saveUrlBtn.Enabled = true;
            }
        }

        private void saveUrl(object sender, DoWorkEventArgs e)
        {
            WebScraper w = new WebScraper();
            string url = urlInput.Text;
            var result = w.GetUrl(url);

            string html = result.html;

            if (html != string.Empty)
            {
                if (downloadDirLbl.Text.Equals("No Directory Selected"))
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        downloadDirLbl.Text = folderBrowserDialog1.SelectedPath;
                        w.HtmlFilePath = folderBrowserDialog1.SelectedPath;
                    }
                }
                else
                {
                    w.HtmlFilePath = downloadDirLbl.Text;
                }
            }

            try
            {
                bool saved = w.SaveUrl();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message);
            }

        }

        private void saveUrlCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            saveUrlBtn.Enabled = true;
        }

        private void selectDownloadDirBtn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                downloadDirLbl.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
