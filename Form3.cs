using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyEdit.io
{
    public partial class Form3 : Form
    {
        Form1 Form1;
        Property Property;
        public Form3(Form1 f1, Property p)
        {
            InitializeComponent();
            Form1 = f1;
            Property = p;
            if (Property.Key.Equals(""))
            {
                textBox1.ReadOnly = false;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            textBox1.Text = Property.Key.Replace("/", "");
            textBox2.Text = Property.Value;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Property.Key.Equals(""))
            {
                Form1.Properties.Add(new Property('/' + textBox1.Text, textBox2.Text));
                Close();
                return;
            }
            foreach (Property p in Form1.Properties)
            {
                if (p.Equals(Property))
                {
                    p.Key = '/' + textBox1.Text;
                    p.Value = textBox2.Text;
                }
            }
            Close();
        }
    }
}
