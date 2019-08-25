using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LeroyMerlinClient
{
	public partial class SendSMS : Window
	{
		public int index;
		public ServiceSMS s;

		public SendSMS(int index, ServiceSMS s)
		{
			InitializeComponent();
			Owner = Win.mainWindow;
			Nomber.Text = Win.program.listTables[index].НомерКлиента.Replace("+7", "");
			this.index = index;
			this.s = s;
			if (s == ServiceSMS.ВнесениеВСписок)
			{
				rich.Text = Win.shablonsend;
				Title = "Внесении в базу";
			}
			if (s == ServiceSMS.ПриходТовара)
			{
				rich.Text = Win.Shablon;
				Title = "Приход товара";
			}
			if (s == ServiceSMS.ОповещениеИзменаДаты)
			{
				rich.Text = Win.shablondata;
				Title = "Оповещение изменения даты";
			}

			rich.Text = rich.Text.Replace("<num>", Win.program.listTables[index].НомерЗаказа.ToString());
			rich.Text = rich.Text.Replace("<time>", DateTime.Now.ToString("hh:mm"));
			rich.Text = rich.Text.Replace("<date>", DateTime.Now.ToString("dd MMMMMMMMMMM"));
			rich.Text = rich.Text.Replace("<dateE>", Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMMMMM"));
			rich.Text = rich.Text.Replace("<name>", Win.program.listTables[index].ИмяКлиента);
			rich.Text = rich.Text.Replace("<nomber>", Win.program.listTables[index].НомерКлиента);
			rich.Text = rich.Text.Replace("<obj>", Win.program.listTables[index].ИмяТовара);
			rich.Text = rich.Text.Replace("<qua>", Win.program.listTables[index].Количество.ToString());
			rich.Text = rich.Text.Replace("<art>", Win.program.listTables[index].Артикул.ToString());
			rich.Text = rich.Text.Replace("<prod>", Win.program.listTables[index].Поставщик);
		}

		private void Nomber_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Nomber.Text.Length == 10 && Nomber.Text != "          ")
			{
				try
				{
					Nomber.Text = long.Parse(Nomber.Text).ToString();
					Warning.Visibility = Visibility.Hidden;
					Send.IsEnabled = true;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
					Warning.Visibility = Visibility.Visible;
					Send.IsEnabled = false;
				}
			}
			else
			{
				Warning.Visibility = Visibility.Visible;
				Send.IsEnabled = false;
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Send_Click(object sender, RoutedEventArgs e)
		{
			rich.Text = rich.Text.Replace("<num>", Win.program.listTables[index].НомерЗаказа.ToString());
			rich.Text = rich.Text.Replace("<time>", DateTime.Now.ToString("hh:mm"));
			rich.Text = rich.Text.Replace("<date>", DateTime.Now.ToString("dd MMMMMMMMMMM"));
			rich.Text = rich.Text.Replace("<dateE>", Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMMMMM"));
			rich.Text = rich.Text.Replace("<name>", Win.program.listTables[index].ИмяКлиента);
			rich.Text = rich.Text.Replace("<nomber>", Win.program.listTables[index].НомерКлиента);
			rich.Text = rich.Text.Replace("<obj>", Win.program.listTables[index].ИмяТовара);
			rich.Text = rich.Text.Replace("<qua>", Win.program.listTables[index].Количество.ToString());
			rich.Text = rich.Text.Replace("<art>", Win.program.listTables[index].Артикул.ToString());
			rich.Text = rich.Text.Replace("<prod>", Win.program.listTables[index].Поставщик);

			try
			{
				var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
								"<SMS>\n" +
								"<operations>\n" +
								"<operation>SEND</operation>\n" +
								"</operations>\n" +
								"<authentification>\n" +
								"<username>" + Win.log + "</username>\n" +
								"<password>" + Win.settings.pass + "</password>\n" +
								"</authentification>\n" +
								"<message>\n" +
								"<sender>LEROYMERLIN</sender>\n" +
								"<text>" + rich.Text + "</text>\n" +
								"</message>\n" +
								"<numbers>\n";
				if (s == ServiceSMS.ОповещениеИзменаДаты)
					XML += "<number messageID=\"msg17\">+7" + Nomber.Text + "</number>\n" +
						   "</numbers>\n" +
						   "</SMS>\n";
				if (s == ServiceSMS.ВнесениеВСписок)
					XML += "<number messageID=\"msg16\">+7" + Nomber.Text + "</number>\n" +
						   "</numbers>\n" +
						   "</SMS>\n";
				if (s == ServiceSMS.ПриходТовара)
					XML += "<number messageID=\"msg15\">+7" + Nomber.Text + "</number>\n" +
						   "</numbers>\n" +
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
				}
				Win.settings.смсразослано++;
				Win.settings.мсмсразослано++;
			}
			catch (Exception s)
			{
				MessageBox.Show(s.Message);
			}
			if (s == ServiceSMS.ПриходТовара)
			{
				Win.OpenThisBase();
				for (int i = 0; i < Win.program.listTables.Count; i++)
					if (Win.program.listTables[i] == Win.program.listTables[index])
					{
						((Table)Win.mainWindow.Components.Children[i]).Статус = "Оповещён";
						Win.program.listTables[i] = new Taga(Win.program.listTables[index]);
						break;
					}
				Win.SaveThisBase();
			}
			Close();
		}

		public enum ServiceSMS
		{
			ПриходТовара,
			ВнесениеВСписок,
			ОповещениеИзменаДаты
		}

		private void richtextbox_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (richtextbox.IsReadOnly == true)
			{
				richtextbox.IsReadOnly = false;
				richtextbox.BorderBrush = new SolidColorBrush(Color.FromRgb(154, 205, 50));
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Win.mainWindow.Activate();
	}
}
