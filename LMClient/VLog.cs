using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LMClient
{
    public partial class VLog : Form
    {
        public VLog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoginIn LA = new LoginIn();
            LA.Show();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogAdmin LA = new LogAdmin();
            LA.Show();
            Close();
        }
    }
}
