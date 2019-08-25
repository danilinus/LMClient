using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace LeroyMerlinClient
{
	public partial class LogAdmin : Window
	{
		public LogAdmin()
		{
			InitializeComponent();
			Owner = Win.mainWindow;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			if (textBox1.Text != "" && textBox2.Password != "" && textBox1.Text != " "
				&& textBox2.Password != " ")
			{
				BinaryFormatter formatter = new BinaryFormatter();
				using (FileStream fs = new FileStream("Mes.mf", FileMode.Create))
				{
					MessageSer a = new MessageSer
					{
						ln = textBox1.Text,
						ps = textBox2.Password
					};
					formatter.Serialize(fs, a);
				}
				System.Diagnostics.Process.Start("shari.exe");
				Close();
			}
		}
	}
}
