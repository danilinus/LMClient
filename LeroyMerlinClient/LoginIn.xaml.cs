using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace LeroyMerlinClient
{
	public partial class LoginIn : Window
	{
		public LoginIn()
		{
			InitializeComponent();
			Owner = Win.mainWindow;
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			if (textBox1.Text != "" && textBox2.Text != "" && textBox1.Text != " "
				&& textBox2.Text != " ")
			{
				BinaryFormatter formatter = new BinaryFormatter();
				using (FileStream fs = new FileStream("Mes.mf", FileMode.Create))
				{
					MessageSer a = new MessageSer { nns = textBox1.Text + " (Магазин: " + textBox2.Text + ")" };
					formatter.Serialize(fs, a);
				}
				System.Diagnostics.Process.Start("shari.exe");
				Close();
			}
		}
	}
}
