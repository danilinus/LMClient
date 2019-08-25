using System;
using System.Windows.Forms;

namespace LMClient
{
	public partial class VLog : Form
	{
		public VLog() => InitializeComponent();


		private void button1_Click(object sender, EventArgs e)
		{
			new LoginIn().Show();
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{	
			new LogAdmin().Show();
			Close();
		}
	}
}