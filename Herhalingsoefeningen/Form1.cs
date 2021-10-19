using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Herhalingsoefeningen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void SetTextBox1Value(string text)
        {
            textBox1.Text = text;
        }

        public void SetTextBox2(string text)
        {
            textBox2.Text = text;
            textBox2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Show();
        }
    }
}
