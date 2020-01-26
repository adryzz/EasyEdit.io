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
            var client = new GitHubClient(new ProductHeaderValue("adryzz"));
            var releases = client.Repository.Release.GetAll("adryzz", "EasyEdit.io");
            var latest = releases[0];

        }
    }
}
