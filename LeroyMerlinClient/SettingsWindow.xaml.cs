using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LeroyMerlinClient
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            Owner = Win.mainWindow;
            try
            {
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
                string[] k = key.GetValueNames();
                bool be = false;
                for (int i = 0; i < k.Length; i++)
                    if (k[i] == "LMClient")
                    {
                        be = true;
                        OpenStart.IsChecked = true;
                        break;
                    }
                if (!be)
                    OpenStart.IsChecked = false;
            }
            catch
            {
                OpenStart.IsEnabled = false;
            }
            DelStart.IsChecked = Win.settings.delStart;
            Gorod.Text = Win.settings.GorodM;
            Adress.Text = Win.settings.AdresM;
            Telephone.Text = Win.settings.TelephoneM;
            Statistic.Text = "  Общая статистика:\n" +
                             "Отправлено сообщений: " + Win.settings.смсразослано + "шт.\n" +
                             "Обзвонено клиентов: " + Win.settings.обзвонено + "шт.\n" +
                             "Добавлено клиентов: " + Win.settings.добавленоклиентов + "шт.\n" +
                             "Напечатано бланков: " + Win.settings.напечатано + "шт.";
            StatisticMouth.Text = "  Статистика за месяц:\n" +
                             "Отправлено сообщений: " + Win.settings.мсмсразослано + "шт.\n" +
                             "Обзвонено клиентов: " + Win.settings.мобзвонено + "шт.\n" +
                             "Добавлено клиентов: " + Win.settings.мдобавленоклиентов + "шт.\n" +
                             "Напечатано бланков: " + Win.settings.мнапечатано + "шт.\n" +
                             "Заработано: " + Win.settings.мзаработано / 100 + "RUR";
            int max = 0;
            for (int i = 0; i < Win.settings.listExcel.Count; i++)
                if (Win.settings.listExcel[max].Куплено < Win.settings.listExcel[i].Куплено)
                    max = i;

            if (Win.settings.listExcel.Count > 0)
                TovarP.Content = "Самый продаваемый товар: " +
                            Win.settings.listExcel[max].ИмяТовара + " (" +
                            Win.settings.listExcel[max].Артикул + ")";
            else
                TovarP.Content = "База товаров не подключена";
        }
        public SettingsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            try
            {
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
                string[] k = key.GetValueNames();
                bool be = false;
                for (int i = 0; i < k.Length; i++)
                    if (k[i] == "LMClient")
                    {
                        be = true;
                        OpenStart.IsChecked = true;
                        break;
                    }
                if (!be)
                    OpenStart.IsChecked = false;
            }
            catch
            {
                OpenStart.IsEnabled = false;
            }
            DelStart.IsChecked = Win.settings.delStart;
            Gorod.Text = Win.settings.GorodM;
            Adress.Text = Win.settings.AdresM;
            Telephone.Text = Win.settings.TelephoneM;
            Statistic.Text = "  Общая статистика:\n" +
                             "Отправлено сообщений: " + Win.settings.смсразослано + "шт.\n" +
                             "Обзвонено клиентов: " + Win.settings.обзвонено + "шт.\n" +
                             "Добавлено клиентов: " + Win.settings.добавленоклиентов + "шт.\n" +
                             "Напечатано бланков: " + Win.settings.напечатано + "шт.";
            StatisticMouth.Text = "  Статистика за месяц:\n" +
                             "Отправлено сообщений: " + Win.settings.мсмсразослано + "шт.\n" +
                             "Обзвонено клиентов: " + Win.settings.мобзвонено + "шт.\n" +
                             "Добавлено клиентов: " + Win.settings.мдобавленоклиентов + "шт.\n" +
                             "Напечатано бланков: " + Win.settings.мнапечатано + "шт.\n" +
                             "Заработано: " + Win.settings.мзаработано / 100 + "RUR";
            int max = 0;
            for (int i = 0; i < Win.settings.listExcel.Count; i++)
                if (Win.settings.listExcel[max].Куплено < Win.settings.listExcel[i].Куплено)
                    max = i;

            if (Win.settings.listExcel.Count > 0)
                TovarP.Content = "Самый продаваемый товар: " +
                            Win.settings.listExcel[max].ИмяТовара + " (" +
                            Win.settings.listExcel[max].Артикул + ")";
            else
                TovarP.Content = "База товаров не подключена";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Win.settings.listExcel.Clear();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Win.settings.delStart = DelStart.IsChecked.Value;
            Win.settings.AdresM = Adress.Text;
            Win.settings.GorodM = Gorod.Text;
            Win.settings.TelephoneM = Telephone.Text;
            if (OpenStart.IsEnabled)
            {
                var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
                string[] k = key.GetValueNames();
                if (OpenStart.IsChecked == true)
                {
                    bool be = false;
                    for (int i = 0; i < k.Length; i++)
                        if (k[i] == "LMClient")
                        {
                            be = true;
                            break;
                        }
                    if (!be)
                        key.SetValue("LMClient", "LeroyMerlinClient.exe");
                }
                else
                    for (int i = 0; i < k.Length; i++)
                        if (k[i] == "LMClient")
                        {
                            key.DeleteValue("LMClient");
                            break;
                        }
            }

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("S.settings", FileMode.Create))
            {
                formatter.Serialize(fs, Win.settings);
            }
            Close();
            //SendingSMSToBeeline();
        }

        public void SendingSMSToBeeline()
        {
            var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                        "[user] => 768900.60\n" +
                        "[pass] => 4Aj_|/sb\n" +
                        "[action] => post_sms\n" +
                        "[message] => Привет\n" +
                        "[target] => +79142017117\n" +
                        "[CLIENTADR] => 127.0.0.1\n" +
                        "[HTTP_ACCEPT_LANGUAGE] => ru - ru,ru; q = 0.8,en - us; q = 0.5,en; q = 0.3\n";
            HttpWebRequest request = WebRequest.Create("http://beeline.amega-inform.ru/sendsms/") as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(XML);
            request.ContentLength = data.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(data, 0, data.Length);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format(
                    "Server error (HTTP {0}: {1}).",
                    response.StatusCode,
                    response.StatusDescription));
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string s = reader.ReadToEnd();
                MessageBox.Show(s);
            }
        }
    }
}
