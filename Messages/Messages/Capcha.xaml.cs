using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Messages
{
	public partial class Capcha : Window
	{
		public Capcha(Uri uri)
		{
			InitializeComponent();
			Capi.Source = new BitmapImage(uri);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			new MainWindow(sK.Text).Show();
			Close();
		}
	}
}