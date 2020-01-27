using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using Octokit;
using System.Net;

namespace EasyEdit.io
{
    public partial class Form2 : Form
    {
        Form1 Form1;
        bool AllowClosing = false;
        public Form2(Form1 f1)
        {
            InitializeComponent();
            Form1 = f1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            Delay();
        }

        async Task PutTaskDelay()
        {
            await Task.Delay(5000);
            UpdateGitHub();
        }

        private async void Delay()
        {
            await PutTaskDelay();
        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                AllowClosing = true;
                Form1.Enabled = true;
                ControlBox = true;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //close logic here
            e.Cancel = !AllowClosing;
            if (!e.Cancel)
            {
                Form1.Updating = false;
            }
            base.OnClosing(e);
        }

        private void UpdateGitHub()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("adryzz"));
                var releases = client.Repository.Release.GetAll("adryzz", "EasyEdit.io");
                Release latest = releases.Result[0];
                if (latest.Name.Equals(ProductVersion.Remove(ProductVersion.Length -2)))
                {
                    MessageBox.Show("You are running the latest version!", "EasyEdit.io", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AllowClosing = true;
                    Form1.Enabled = true;
                    Form1.Updating = false;
                    ControlBox = true;
                    Close();
                }
                using (WebClient wc = new WebClient())
                {
                    progressBar1.Style = ProgressBarStyle.Continuous;
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                    wc.DownloadFileAsync(new Uri(latest.Assets.First().BrowserDownloadUrl), Path.Combine(System.Windows.Forms.Application.StartupPath, "update.exe"));
                    label1.Text = "Downloading update...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "EasyEdit.io", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AllowClosing = true;
                Form1.Enabled = true;
                Form1.Updating = false;
                ControlBox = true;
                Close();
            }
        }

        private void Wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            AllowClosing = true;
            Form1.Enabled = true;
            Form1.Updating = false;
            ControlBox = true;
            label1.Text = "Download completed!";
            MessageBox.Show("Download completed!\nNow replace this '.exe' with 'update.exe'", "EasyEdit.io", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
    }
}
