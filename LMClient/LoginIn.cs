using System;
using System.IO;
using System.Windows.Forms;

namespace LMClient
{
    public partial class LoginIn : Form
    {
        public LoginIn()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "" && textBox3.Text != " "
                && textBox4.Text != " ")
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + @"\nns.dlb");
                sw.WriteLine(textBox3.Text + " (Магазин: " + textBox4.Text + ")");
                sw.Close();
                System.Diagnostics.Process.Start(Application.StartupPath + @"\shari.exe");
                Close();
            }
        }
    }
}
