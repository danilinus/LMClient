using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LeroyMerlinClient
{
	public partial class InfoAboutClient : Window, INotifyPropertyChanged
	{
		public int index;
		public string Дата
		{
			get
			{
				return "Дата:\n" + Win.program.listTables[index].Дата.ToString("dd.MM.yyyy");
			}
			set
			{
				OnPropertyChanged("Дата");
			}
		}
		public string Количество
		{
			get
			{
				if (Win.program.listTables[index].indxK == 0)
					return Win.program.listTables[index].Количество.ToString() + " шт.";
				else
				if (Win.program.listTables[index].indxK == 1)
					return Win.program.listTables[index].Количество.ToString() + " м.";
				else
					return Win.program.listTables[index].Количество.ToString() + " уп.";
			}
			set
			{
				OnPropertyChanged("Количество");
			}
		}
		public string НомерЗаказа
		{
			get
			{
				return "№" + Win.program.listTables[index].НомерЗаказа.ToString();
			}
			set
			{
				OnPropertyChanged("НомерЗаказа");
			}
		}
		public string Поставщик
		{
			get
			{
				return "Поставщик: " + Win.program.listTables[index].Поставщик.ToString();
			}
			set
			{
				OnPropertyChanged("Поставщик");
			}
		}
		public string ИмяПродавца
		{
			get
			{
				return "Продавец: " + Win.program.listTables[index].ИмяПродавца.ToString();
			}
			set
			{
				OnPropertyChanged("Продавец");
			}
		}
		public string ДатаПрихода
		{
			get
			{
				return "Дата Прихода:\n" + Win.program.listTables[index].ДатаПрихода.ToString("dd.MM.yyyy");
			}
			set
			{
				OnPropertyChanged("ДатаПрихода");
			}
		}
		public string Артикул
		{
			get
			{
				return "Артикул: " + Win.program.listTables[index].Артикул.ToString();
			}
			set
			{
				OnPropertyChanged("Артикул");
			}
		}
		public string ИмяКлиента
		{
			get
			{
				return Win.program.listTables[index].ИмяКлиента;
			}
			set
			{
				OnPropertyChanged("ИмяКлиента");
			}
		}
		public string ИмяТовара
		{
			get
			{
				return Win.program.listTables[index].ИмяТовара;
			}
			set
			{
				OnPropertyChanged("ИмяТовара");
			}
		}
		public string НомерКлиента
		{
			get
			{
				return Win.program.listTables[index].НомерКлиента.ToString();
			}
			set
			{
				OnPropertyChanged("НомерКлиента");
			}
		}
		public string Статус
		{
			get
			{
				if (((Table)Win.mainWindow.Components.Children[index]).Статус == "В Процессе")
					return "В Процессе";
				if (((Table)Win.mainWindow.Components.Children[index]).Статус == "Не Оповещён")
					return "Не Оповещён";
				else
					return "Оповещён";
			}
			set
			{
				OnPropertyChanged("Статус");
			}
		}

		private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		public event PropertyChangedEventHandler PropertyChanged;

		public InfoAboutClient(int index)
		{
			InitializeComponent();
			this.index = index;
			DataContext = this;
			if (((Table)Win.mainWindow.Components.Children[index]).Статус == "В Процессе")
			{
				BitmapImage bi3 = new BitmapImage();
				bi3.BeginInit();
				bi3.UriSource = new Uri("Images/Truck_48px.png", UriKind.Relative);
				bi3.EndInit();
				Status.Source = bi3;
			}
			if (((Table)Win.mainWindow.Components.Children[index]).Статус == "Не Оповещён")
			{
				BitmapImage bi3 = new BitmapImage();
				bi3.BeginInit();
				bi3.UriSource = new Uri("Images/Disapprove_48px.png", UriKind.Relative);
				bi3.EndInit();
				Status.Source = bi3;
				TovarGood.Click -= new RoutedEventHandler(TovarGood_Click);
				TovarGood.Click += new RoutedEventHandler(Call_Click);
				TovarGood.Content = "Оповестить";
				TovarGood.ToolTip = new Label()
				{
					Content = "Нажмите если хотите оповестить клиента"
				};
				Status.ToolTip = new Label()
				{
					Content = "Товар прибыл на склад, но клиент не оповещён"
				}; ;
			}
			if (((Table)Win.mainWindow.Components.Children[index]).Статус == "Оповещён")
			{
				BitmapImage bi3 = new BitmapImage();
				bi3.BeginInit();
				bi3.UriSource = new Uri("Images/Approve_48px.png", UriKind.Relative);
				bi3.EndInit();
				Status.Source = bi3;
				Status.ToolTip =
				new Label()
				{
					Content = "Клиент оповещён"
				};
				Alarm.Visibility = Visibility.Hidden;
				TovarGood.Visibility = Visibility.Hidden;
			}
			for (int i = 0; i < Win.IAC.Count; i++)
				if (index == Win.IAC[i].index)
				{
					Win.IAC[i].Close();
					Win.IAC.RemoveAt(i);
					break;
				}
			Win.IAC.Add(this);
			Owner = Win.mainWindow;

			if (System.Windows.Forms.TextRenderer.MeasureText(ИмяКлиента, new System.Drawing.Font("Arial", 24)).Width > Width)
				Width = Width + (System.Windows.Forms.TextRenderer.MeasureText(ИмяКлиента, new System.Drawing.Font("Arial", 24)).Width - Width) + 10;

		}

		private void TovarGood_Click(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i] == Win.program.listTables[index])
				{
					((Table)Win.mainWindow.Components.Children[i]).Статус = "Не Оповещён";
					((Table)Win.mainWindow.Components.Children[i]).ДатаПрихода = DateTime.Now.ToString("dd.MM.yyyy");
					break;
				}
			BitmapImage bi3 = new BitmapImage();
			bi3.BeginInit();
			bi3.UriSource = new Uri("Images/Disapprove_48px.png", UriKind.Relative);
			bi3.EndInit();
			Status.Source = bi3;
			TovarGood.Click -= new RoutedEventHandler(TovarGood_Click);
			TovarGood.Click += new RoutedEventHandler(Call_Click);
			TovarGood.Content = "Оповестить";
			TovarGood.ToolTip = new Label()
			{
				Content = "Нажмите если хотите оповестить клиента"
			};
			Status.ToolTip = new Label()
			{
				Content = "Товар прибыл на склад, но клиент не оповещён"
			};
		}
		private void Call_Click(object sender, RoutedEventArgs e)
		{
			new ThreeButtons(index, SendSMS.ServiceSMS.ПриходТовара).ShowDialog();
			Close();
		}

		private void Edit_Click(object sender, RoutedEventArgs e)
		{
			new AddClient(Win.program.listTables[index]).ShowDialog();
			Close();
		}
		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			System.Windows.Forms.DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Вы уверены что хотите удалить клиента из базы?", "Внимание!", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Warning);
			if (dialogResult == System.Windows.Forms.DialogResult.Yes)
			{
				Win.RemoveClient(index);
				Close();
			}
		}

		private void Print_Click(object sender, RoutedEventArgs e)
		{
			new PrintWindow(index).Show();
			Close();
		}

		private void Message_Click(object sender, RoutedEventArgs e)
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
					string str = new StreamReader(response.GetResponseStream()).ReadToEnd();
					string[] arr = Regex.Matches(str, @"\>(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
					string[] arr2 = Regex.Matches(str, @"\>(\w+)\.").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
					string[] arr3 = Regex.Matches(str, @"\.(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
					if (arr.Length < 1)
						MessageBox.Show("Ошибка: Неверно введён логин и/или пароль", "Ошибка");
					if (arr[0] == "0")
					{
						if (int.Parse(arr2[0]) > 3)
						{
							new SendSMS(index, SendSMS.ServiceSMS.ПриходТовара).Show();
							Close();
						}
						else
							MessageBox.Show("СМС сервис временно не работает,\nповторите попытку позже.", "Ошибка");
					}
				}
			}
			catch (Exception s)
			{
				MessageBox.Show(s.Message);
			}

		}

		private void MouseRBU(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			((TextBox)sender).TextDecorations = null;
			if (((TextBox)sender).Text.ToString().Contains("Артикул: "))
				Clipboard.SetText(((TextBox)sender).Text.ToString().Replace("Артикул: ", ""));
			else
				Clipboard.SetText(((TextBox)sender).Text.ToString());
		}

		private void MouseRBD(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			((TextBox)sender).TextDecorations = TextDecorations.Underline;
		}

		private void Alarm_Click(object sender, RoutedEventArgs e)
		{
			Nomber.Visibility = Visibility.Hidden;
			Alarm.Visibility = Visibility.Hidden;
			TovarGood.Visibility = Visibility.Hidden;
			DateEnd.Visibility = Visibility.Hidden;
			NewDate.Visibility = Visibility.Visible;
			NewDatePicker.DisplayDateStart = DateTime.Now;
			NewDatePicker.SelectedDate = DateTime.Now;
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			Nomber.Visibility = Visibility.Visible;
			Alarm.Visibility = Visibility.Visible;
			TovarGood.Visibility = Visibility.Visible;
			DateEnd.Visibility = Visibility.Visible;
			NewDate.Visibility = Visibility.Hidden;
		}

		private void OkeyBtn_Click(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i] == Win.program.listTables[index])
				{
					((Table)Win.mainWindow.Components.Children[i]).Статус = "В Процессе";
					((Table)Win.mainWindow.Components.Children[i]).ДатаПрихода = NewDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy");
					break;
				}
			if (MessageBox.Show("Оповестить клиента\nо изменении даты прихода?", "Оповестить?", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
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
						string str = new StreamReader(response.GetResponseStream()).ReadToEnd();
						string[] arr = Regex.Matches(str, @"\>(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
						string[] arr2 = Regex.Matches(str, @"\>(\w+)\.").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
						string[] arr3 = Regex.Matches(str, @"\.(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
						if (arr.Length < 1)
							MessageBox.Show("Ошибка: Неверно введён логин и/или пароль", "Ошибка");
						if (arr[0] == "0")
						{
							if (int.Parse(arr2[0]) > 3)
							{
								new InfoAboutClient(index).Show();
								new SendSMS(index, SendSMS.ServiceSMS.ОповещениеИзменаДаты).Show();
								Close();
							}
							else
								MessageBox.Show("СМС сервис временно не работает,\nповторите попытку позже.", "Ошибка");
						}
					}
				}
				catch (Exception s)
				{
					MessageBox.Show(s.Message);
				}
			}
			else
			{
				new InfoAboutClient(index).Show();
				Close();
			}
		}

		private void WinPI_Closing(object sender, CancelEventArgs e) => Win.mainWindow.Activate();
	}
}