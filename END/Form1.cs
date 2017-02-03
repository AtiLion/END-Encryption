using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace END
{
    public partial class Form1 : Form
    {
        private Cryption c;

        public Form1()
        {
            c = new Cryption();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = saveFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = File.ReadAllText(textBox1.Text);
            text = c.encrypt(text);
            File.WriteAllText(textBox2.Text, text);
            MessageBox.Show("Encrypted!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string text = File.ReadAllText(textBox1.Text);
            text = c.decrypt(text);
            File.WriteAllText(textBox2.Text, text);
            MessageBox.Show("Decrypted!");
        }
    }
}
