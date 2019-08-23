using System;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Messages
{
    /// <summary>
    /// Логика взаимодействия для Capcha.xaml
    /// </summary>
    public partial class Capcha : Window
    {
        public Capcha(Uri uri)
        {
            InitializeComponent();
            BitmapImage bitmap = new BitmapImage(uri);
            Capi.Source = bitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow(sK.Text);
            mw.Show();
            Close();
        }
    }
}
