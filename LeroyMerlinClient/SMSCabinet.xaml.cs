using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml;

namespace LeroyMerlinClient
{
	public partial class SMSCabinet : Window
	{
		public SMSCabinet()
		{
			InitializeComponent();
			Owner = Win.mainWindow;
		}

		public SMSCabinet(Window owner)
		{
			InitializeComponent();
			Owner = owner;
		}

		private void SMSCabinet_Load(object sender, RoutedEventArgs e)
		{
			PasstextBox.Password = Win.settings.pass;
			richTextBox1.Text = "";
			try
			{
				var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
							"<SMS>\n" +
							"<operations>\n" +
							"<operation>GETSTATUS</operation>\n" +
							"</operations>\n" +
							"<authentification>\n" +
							"<username>" + Win.log + "</username>\n" +
							"<password>" + PasstextBox.Password + "</password>\n" +
							"</authentification>\n" +
							"<statistics>\n" +
							"<messageid>msg15</messageid>\n" +
							"<messageid>msg16</messageid>\n" +
							"</statistics>\n" +
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
					string s = new StreamReader(response.GetResponseStream()).ReadToEnd();
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(s);
					XmlNode root_node = doc.DocumentElement;
					XmlNodeList nodes = root_node.ChildNodes;
					foreach (XmlNode n in nodes)
						if (n.Name == "message")
							richTextBox1.Text = "[" + n.Attributes["id"].Value + "] | [" + n.Attributes["sentdate"].Value + "] | " + n.Attributes["status"].Value.Replace("0", "Идёт отправка") + "\n" + richTextBox1.Text;


					richTextBox1.Text = richTextBox1.Text.Replace("msg15", "Приход товара");
					richTextBox1.Text = richTextBox1.Text.Replace("msg16", "Внесение в список");
					richTextBox1.Text = richTextBox1.Text.Replace("msg17", "Изменение даты");
					richTextBox1.Text = richTextBox1.Text.Replace("SENT", "Отослано");
					richTextBox1.Text = richTextBox1.Text.Replace("NOT_DELIVERED", "Не доставлено");
					richTextBox1.Text = richTextBox1.Text.Replace("DELIVERED", "Доставлено");
					richTextBox1.Text = richTextBox1.Text.Replace("NOT_ALLOWED", "Оператор не обслуживается");
					richTextBox1.Text = richTextBox1.Text.Replace("INVALID_DESTINATION_ADDRESS", "Неверный адрес для доставки");
					richTextBox1.Text = richTextBox1.Text.Replace("INVALID_SOURCE_ADDRESS", "Неправильное имя «От кого»");
					richTextBox1.Text = richTextBox1.Text.Replace("NOT_ENOUGH_CREDITS", "Недостаточно кредитов");
					richTextBox1.Text = richTextBox1.Text.Replace("0000-00-00 00:00:00", "Идёт Отправка");
				}
			}
			catch (Exception s)
			{
				MessageBox.Show(s.Message);
			}
			if (Win.settings.today != "")
				label1.Content = "Ваш лимит сообщений: " + Win.settings.today + " СМС";
			else
				label1.Content = "";
		}

		private void Reboot_Click(object sender, RoutedEventArgs e)
		{
			richTextBox1.Text = "";
			Win.settings.pass = PasstextBox.Password;
			Win.SaveSettings();
			try
			{
				var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
							"<SMS>\n" +
							"<operations>\n" +
							"<operation>GETSTATUS</operation>\n" +
							"</operations>\n" +
							"<authentification>\n" +
							"<username>" + Win.log + "</username>\n" +
							"<password>" + PasstextBox.Password + "</password>\n" +
							"</authentification>\n" +
							"<statistics>\n" +
							"<messageid>msg15</messageid>\n" +
							"</statistics>\n" +
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

					string s = new StreamReader(response.GetResponseStream()).ReadToEnd();
					if (s.Length < 255)
						MessageBox.Show("Пароль неверный", "Ошибочка");
					XmlDocument doc = new XmlDocument();
					doc.LoadXml(s);
					XmlNode root_node = doc.DocumentElement;
					XmlNodeList nodes = root_node.ChildNodes;
					foreach (XmlNode n in nodes)
					{
						if (n.Name == "message")
							richTextBox1.Text = "[" + n.Attributes["sentdate"].Value + "] | " + n.Attributes["status"].Value.Replace("0", "Идёт отправка") + "\n" + richTextBox1.Text;

					}

					richTextBox1.Text = richTextBox1.Text.Replace("SENT", "Отослано");
					richTextBox1.Text = richTextBox1.Text.Replace("NOT_DELIVERED", "Не доставлено");
					richTextBox1.Text = richTextBox1.Text.Replace("DELIVERED", "Доставлено");
					richTextBox1.Text = richTextBox1.Text.Replace("NOT_ALLOWED", "Оператор не обслуживается");
					richTextBox1.Text = richTextBox1.Text.Replace("INVALID_DESTINATION_ADDRESS", "Неверный адрес для доставки");
					richTextBox1.Text = richTextBox1.Text.Replace("INVALID_SOURCE_ADDRESS", "Неправильное имя «От кого»");
					richTextBox1.Text = richTextBox1.Text.Replace("NOT_ENOUGH_CREDITS", "Недостаточно кредитов");
					richTextBox1.Text = richTextBox1.Text.Replace("0000-00-00 00:00:00", "Идёт Отправка");
				}
			}
			catch (Exception s)
			{
				MessageBox.Show(s.ToString());
			}

			if (Win.settings.today != "")
				label1.Content = "Ваш лимит сообщений: " + Win.settings.today + " СМС";
			else
				label1.Content = "";
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			textBox1.Text = "";
			textBox2.Text = "";

			if (Win.settings.today != "")
				label1.Content = "Ваш лимит сообщений: " + Win.settings.today + " СМС";
			else
				label1.Content = "";
		}

		private void button3_Click(object sender, RoutedEventArgs e)
		{
			if (textBox1.Text == Win.settings.passa)
			{
				Win.settings.yestoday = DateTime.Now.AddDays(1);
				Win.settings.yestoday2 = int.Parse(textBox2.Text);
				Win.settings.reboot = int.Parse(textBox2.Text);
				MessageBox.Show("Лимит изменен", "Ограничение");
			}
			else
				MessageBox.Show("Пароль неверный", "Ограничение");
			textBox1.Text = "";
			textBox2.Text = "";
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			if (textBox1.Text == Win.settings.passa)
				try
				{
					var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
								"<SMS>\n" +
								"<operations>\n" +
								"<operation>BALANCE</operation>\n" +
								"</operations>\n" +
								"<authentification>\n" +
								"<username>" + Win.log + "</username>\n" +
								"<password>" + PasstextBox.Password + "</password>\n" +
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
						string str = new StreamReader(response.GetResponseStream()).ReadToEnd();
						string[] arr = Regex.Matches(str, @"\>(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
						string[] arr2 = Regex.Matches(str, @"\>(\w+)\.").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
						string[] arr3 = Regex.Matches(str, @"\.(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
						if (arr.Length < 1)
							MessageBox.Show("Ошибка: Неверно введён логин и/или пароль", "Ошибка");
						if (arr[0] == "0")
							MessageBox.Show("Баланс: " + arr2[0] + "." + arr3[0] + arr[2], "Ваш баланс");
					}
				}
				catch (Exception s)
				{
					MessageBox.Show(s.Message);
				}
			else
			{
				MessageBox.Show("Пароль неверный", "Пароль");
			}
			textBox1.Text = "";
			textBox2.Text = "";
		}

		private void button4_Click(object sender, RoutedEventArgs e)
		{
			if (textBox1.Text == Win.settings.passa)
			{
				Win.settings.passa = textBox2.Text;
				MessageBox.Show("Пароль успешно изменен", "Пароль");
			}
			else
				MessageBox.Show("Пароль неверный", "Пароль");

			textBox1.Text = "";
			textBox2.Text = "";
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Win.mainWindow.Activate();
	}
}