using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageConverter
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void About_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
