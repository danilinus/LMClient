using System;
using System.IO;
using System.Windows.Forms;

namespace LMClient
{
	public partial class LogAdmin : Form
	{
		public LogAdmin()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text != "" && textBox2.Text != "" && textBox1.Text != " "
				&& textBox2.Text != " ")
			{
				StreamWriter sw = new StreamWriter(Application.StartupPath + @"\ln.dlb");
				sw.WriteLine(textBox1.Text);
				sw.Close();
				sw = new StreamWriter(Application.StartupPath + @"\ps.dlb");
				sw.WriteLine(textBox2.Text);
				sw.Close();
				System.Diagnostics.Process.Start(Application.StartupPath + @"\shari.exe");
				Close();
			}
		}
	}
}