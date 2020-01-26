using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EasyEdit.io
{
    public partial class Form1 : Form
    {
        string Path = "";
        FileStream Stream;
        public bool Updating = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "EasyEdit.io - Not for commercial use";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                Path = openFileDialog1.FileName;
                Text += String.Format(" - File opened: {0}", Path);
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                LoadData();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void LoadData()
        {
            dateTimePicker1.Value = File.GetCreationTime(Path);
            dateTimePicker2.Value = File.GetLastWriteTime(Path);
            dateTimePicker3.Value = File.GetLastAccessTime(Path);
            GetAttributes(File.GetAttributes(Path));
        }

        private void SaveData()
        {
            try
            {
                File.SetAttributes(Path, SetAttributes());
                File.SetCreationTime(Path, dateTimePicker1.Value);
                File.SetLastWriteTime(Path, dateTimePicker2.Value);
                File.SetLastAccessTime(Path, dateTimePicker3.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error while saving: {0}", ex.Message), "EasyEdit.io", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button1_Click(sender, e);
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button2_Click(sender, e);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^Z");
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("^Z");
        }

        private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EasyEdit.io is under GNU Gpl v3.0 (or newer at your own choice).\nThis program comes with NO warranty or liability of any type.\nThis program is free and open-source and will ever be so.\n\nInfo\nWith this program  you can edit all the attributes of any file.\nThanks for downloading - Adryzz - https://github.com/adryzz/", String.Format("EasyEdit.io v{0}", ProductVersion), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CheckForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("This feature hasn't been implemented yet.", "EasyEdit.io", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Updating = true;
            Enabled = false;
            Form2 f2 = new Form2(this);
            f2.Show(this);
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
        }

        private FileAttributes SetAttributes()
        {
            FileAttributes attributes = FileAttributes.Normal;
            if (checkBox1.Checked)
            {
                attributes = attributes | FileAttributes.ReadOnly;
            }
            if (checkBox2.Checked)
            {
                attributes = attributes | FileAttributes.Hidden;
            }
            if (checkBox3.Checked)
            {
                attributes = attributes | FileAttributes.System;
            }
            if (checkBox4.Checked)
            {
                attributes = attributes | FileAttributes.Device;
            }
            if (checkBox5.Checked)
            {
                attributes = attributes | FileAttributes.Compressed;
            }
            if (checkBox6.Checked)
            {
                attributes = attributes | FileAttributes.Encrypted;
            }
            if (checkBox7.Checked)
            {
                attributes = attributes | FileAttributes.Temporary;
            }
            if (checkBox8.Checked)
            {
                attributes = attributes | FileAttributes.Archive;
            }
            if (checkBox9.Checked)
            {
                attributes = attributes | FileAttributes.Offline;
            }
            if (checkBox10.Checked)
            {
                attributes = attributes | FileAttributes.NotContentIndexed;
            }
            if (checkBox11.Checked)
            {
                attributes = attributes | FileAttributes.NoScrubData;
            }
            if (checkBox12.Checked)
            {
                attributes = attributes | FileAttributes.Directory;
            }
            return attributes;
        }

        private void GetAttributes(FileAttributes attributes)
        {
            if (attributes.HasFlag(FileAttributes.ReadOnly))
            {
                checkBox1.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Hidden))
            {
                checkBox2.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.System))
            {
                checkBox3.Checked = true;
                checkBox2.Checked = true;
                checkBox2.Enabled = false;
            }
            if (attributes.HasFlag(FileAttributes.Device))
            {
                checkBox4.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Compressed))
            {
                checkBox5.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Encrypted))
            {
                checkBox6.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Temporary))
            {
                checkBox7.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Archive))
            {
                checkBox8.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Offline))
            {
                checkBox9.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.NotContentIndexed))
            {
                checkBox10.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.NoScrubData))
            {
                checkBox11.Checked = true;
            }
            if (attributes.HasFlag(FileAttributes.Directory))
            {
                checkBox12.Checked = true;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //close logic here
            e.Cancel = Updating;
            base.OnClosing(e);
        }
    }
}
