using System.Windows;

namespace LeroyMerlinClient
{
	public partial class VLog : Window
	{
		public VLog()
		{
			InitializeComponent();
			Owner = Win.mainWindow;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			new LoginIn().Show();
			Close();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			new LogAdmin().Show();
			Close();
		}
	}
}
