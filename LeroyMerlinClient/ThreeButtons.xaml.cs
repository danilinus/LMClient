using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace LeroyMerlinClient
{
    public partial class ThreeButtons : Window
    {
        public int index;
        SendSMS.ServiceSMS s;
        public ThreeButtons(int index, SendSMS.ServiceSMS s)
        {
            InitializeComponent();
            Owner = Win.mainWindow;
            this.index = index;
            this.s = s;
            if (s == SendSMS.ServiceSMS.ВнесениеВСписок)
            {
                TextPop.Text = "Выберите тип оповещение клиента о добавлении в базу ожидания товара:";
                TextPop.FontSize = 14;
                FirstBtn.Content = "Печать";
                FirstBtn.Click += new RoutedEventHandler(PrintBtn_Click);
                TwBtn.Content = "СМС";
                TwBtn.Click += new RoutedEventHandler(SMSBtn_Click);
            }
            if (s == SendSMS.ServiceSMS.ПриходТовара)
            {
                TextPop.Text = "Выберите тип оповещение клиента о приходе товара:";
                FirstBtn.Content = "Обзвон";
                TextPop.FontSize = 18;
                FirstBtn.Click += new RoutedEventHandler(CallBtn_Click);
                TwBtn.Content = "СМС";
                TwBtn.Click += new RoutedEventHandler(SMSBtn_Click);
            }
        }

        private void SMSBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                            "<SMS>\n" +
                            "<operations>\n" +
                            "<operation>BALANCE</operation>\n" +
                            "</operations>\n" +
                            "<authentification>\n" +
                            "<username>" + Win.log + "</username>\n" +
                            "<password>" + Win.settings.pass + "</password>\n" +
                            "</authentification>\n" +
                            "</SMS>\n";
                HttpWebRequest request = WebRequest.Create("http://api.myatompark.com/members/sms/xml.php") as HttpWebRequest;
                request.Method = "Post";
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
                    string str = reader.ReadToEnd();
                    string[] arr = Regex.Matches(str, @"\>(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
                    string[] arr2 = Regex.Matches(str, @"\>(\w+)\.").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
                    string[] arr3 = Regex.Matches(str, @"\.(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
                    if (arr.Length < 1)
                        MessageBox.Show("Ошибка: Неверно введён логин и/или пароль", "Ошибка");
                    if (arr[0] == "0")
                        if (int.Parse(arr2[0]) > 3)
                        {
                            new SendSMS(index, s).Show();
                            Close();
                        }
                        else
                            if(s == SendSMS.ServiceSMS.ВнесениеВСписок)
                                MessageBox.Show("СМС сервис временно не работает,\nповторите попытку позже.\nВы можете распечатать бланк", "Ошибка"); else
                                MessageBox.Show("СМС сервис временно не работает,\nповторите попытку позже.\nВы можете обзвонить клиента", "Ошибка");
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }
        }

        private void PrintBtn_Click(object sender, RoutedEventArgs e)
        {
            new PrintWindow(index).Show();
            Close();
        }

        private void CallBtn_Click(object sender, RoutedEventArgs e)
        {
            new CallWindow(index, s).Show();
            Close();
        }
    }
}