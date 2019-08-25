using System;
using System.Windows;
using System.Windows.Controls;

namespace LeroyMerlinClient
{
	public partial class AddClient : Window
	{
		System.Windows.Controls.Image[] images = new System.Windows.Controls.Image[7];
		public AddClient()
		{
			InitializeComponent();
			DatePick.SelectedDate = DateTime.Now.AddDays(3);
			DatePick.DisplayDateStart = DateTime.Now;
			images[0] = _1;
			images[1] = _2;
			images[2] = _3;
			images[3] = _4;
			images[4] = _5;
			images[5] = _6;
			images[6] = _7;
			if (Win.program.Продавец != "")
				NameProd.Text = Win.program.Продавец;
			Owner = Win.mainWindow;
		}

		public AddClient(Taga tages)
		{
			InitializeComponent();
			images[0] = _1;
			images[1] = _2;
			images[2] = _3;
			images[3] = _4;
			images[4] = _5;
			images[5] = _6;
			images[6] = _7;
			Title = "Изменение клиента";
			Add.Content = "Сохранить";
			NowTime = tages.Дата;
			DatePick.SelectedDate = tages.ДатаПрихода;
			Artikul.Text = tages.Артикул.ToString();
			Nomb = tages.НомерЗаказа;
			Nomber.Text = tages.Количество.ToString();
			NomberClient.Text = tages.НомерКлиента.ToString().Remove(0, 2);
			NameClient.Text = tages.ИмяКлиента;
			NameProd.Text = tages.ИмяПродавца;
			NameTov.Text = tages.ИмяТовара;
			Postav.Text = tages.Поставщик;
			Add.Click -= new RoutedEventHandler(Button_Click);
			Add.Click += new RoutedEventHandler(Save_Click);
			ttt = tages;
			Owner = Win.mainWindow;
		}

		private readonly Taga ttt;
		private readonly DateTime NowTime;
		private readonly int Nomb;

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i] == ttt)
				{
					((Table)Win.mainWindow.Components.Children[i]).Дата = NowTime.ToString("dd.MM.yyyy");
					((Table)Win.mainWindow.Components.Children[i]).ДатаПрихода = DatePick.SelectedDate.Value.ToString("dd.MM.yyyy");
					((Table)Win.mainWindow.Components.Children[i]).Артикул = Artikul.Text;
					((Table)Win.mainWindow.Components.Children[i]).НомерЗаказа = Nomb.ToString();
					((Table)Win.mainWindow.Components.Children[i]).Количество = Nomber.Text;
					((Table)Win.mainWindow.Components.Children[i]).ИмяТовара = NameTov.Text;
					((Table)Win.mainWindow.Components.Children[i]).НомерКлиента = "+7" + NomberClient.Text;
					((Table)Win.mainWindow.Components.Children[i]).ИмяКлиента = NameClient.Text;
					((Table)Win.mainWindow.Components.Children[i]).Статус = "В Процессе";
					((Table)Win.mainWindow.Components.Children[i]).Поставщик = Postav.Text;
					((Table)Win.mainWindow.Components.Children[i]).ИмяПродавца = NameProd.Text;

					new InfoAboutClient(i).Show();
					break;
				}
			Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Taga tas = new Taga(DateTime.Now, DatePick.SelectedDate.Value, ulong.Parse(Artikul.Text), new Random().Next(100, 10000), int.Parse(Nomber.Text), NameTov.Text, "+7" + NomberClient.Text, NameClient.Text, StatusE.ВПроцессе, Postav.Text, NameProd.Text)
			{
				AVS = tss,
				Деньга = money,
				indxK = indexComboBox.SelectedIndex
			};

			Win.OpenThisBase();
			Win.program.listTables.Add(tas);
			Win.SaveThisBase();

			Win.program.Продавец = NameProd.Text;
			new ThreeButtons(Win.program.listTables.Count - 1, SendSMS.ServiceSMS.ВнесениеВСписок).Show();
			Win.settings.добавленоклиентов++;
			Win.settings.мдобавленоклиентов++;
			Win.settings.мзаработано += money / 100;
			for (int i = 0; i < Win.settings.listExcel.Count; i++)
				if (Artikul.Text == Win.settings.listExcel[i].Артикул ||
					Artikul.Text == Win.settings.listExcel[i].Штрихкод)
				{
					Win.settings.listExcel[i].Куплено += int.Parse(Nomber.Text);
					break;
				}
			Close();
		}

		private void int_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((TextBox)sender).Text != "" && ((TextBox)sender).Text != " " && ((TextBox)sender).Text != "  ")
			{
				try
				{
					((TextBox)sender).Text = int.Parse(((TextBox)sender).Text).ToString();
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Hidden;
				}
				catch
				{
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				}

				if (images[0].Visibility == Visibility.Hidden &&
					images[1].Visibility == Visibility.Hidden &&
					images[2].Visibility == Visibility.Hidden &&
					images[3].Visibility == Visibility.Hidden &&
					images[4].Visibility == Visibility.Hidden &&
					images[5].Visibility == Visibility.Hidden &&
					images[6].Visibility == Visibility.Hidden)
					Add.IsEnabled = true;
				else
					Add.IsEnabled = false;
			}
			else
			{
				images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				Add.IsEnabled = false;
			}
			if (tss != null)
			{
				AvsE.Visibility = Visibility.Visible;
				AvsLbl.Visibility = Visibility.Visible; AvsLbl.Content = "AVS: " + tss;
			}
			else
			{
				AvsE.Visibility = Visibility.Hidden;
				AvsLbl.Visibility = Visibility.Hidden;
			}
		}

		private void ulong_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((TextBox)sender).Text != "" && ((TextBox)sender).Text != " " && ((TextBox)sender).Text != "  ")
			{
				try
				{
					((TextBox)sender).Text = ulong.Parse(((TextBox)sender).Text).ToString();
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Hidden;
				}
				catch
				{
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				}

				if (images[0].Visibility == Visibility.Hidden &&
					images[1].Visibility == Visibility.Hidden &&
					images[2].Visibility == Visibility.Hidden &&
					images[3].Visibility == Visibility.Hidden &&
					images[4].Visibility == Visibility.Hidden &&
					images[5].Visibility == Visibility.Hidden &&
					images[6].Visibility == Visibility.Hidden)
					Add.IsEnabled = true;
				else
					Add.IsEnabled = false;
			}
			else
			{
				images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				Add.IsEnabled = false;
			}
			if (tss != null)
			{
				AvsE.Visibility = Visibility.Visible;
				AvsLbl.Visibility = Visibility.Visible; AvsLbl.Content = "AVS: " + tss;
			}
			else
			{
				AvsE.Visibility = Visibility.Hidden;
				AvsLbl.Visibility = Visibility.Hidden;
			}
		}

		private void Artickul_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((TextBox)sender).Text != "" && ((TextBox)sender).Text != " " && ((TextBox)sender).Text != "  ")
			{
				try
				{
					((TextBox)sender).Text = ulong.Parse(((TextBox)sender).Text).ToString();
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Hidden;
				}
				catch
				{
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				}

				if (images[0].Visibility == Visibility.Hidden &&
					images[1].Visibility == Visibility.Hidden &&
					images[2].Visibility == Visibility.Hidden &&
					images[3].Visibility == Visibility.Hidden &&
					images[4].Visibility == Visibility.Hidden &&
					images[5].Visibility == Visibility.Hidden &&
					images[6].Visibility == Visibility.Hidden)
					Add.IsEnabled = true;
				else
					Add.IsEnabled = false;

				for (int i = 0; i < Win.settings.listExcel.Count; i++)
					if (((TextBox)sender).Text == Win.settings.listExcel[i].Артикул ||
						((TextBox)sender).Text == Win.settings.listExcel[i].Штрихкод)
					{
						NameTov.Text = Win.settings.listExcel[i].ИмяТовара;
						Postav.Text = Win.settings.listExcel[i].Поставщик;
						money = float.Parse(Win.settings.listExcel[i].Деньга);
						tss = Win.settings.listExcel[i].AVS;
						break;
					}

			}
			else
			{
				images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				Add.IsEnabled = false;
			}
			if (tss != null)
			{
				AvsE.Visibility = Visibility.Visible;
				AvsLbl.Visibility = Visibility.Visible; AvsLbl.Content = "AVS: " + tss;
			}
			else
			{
				AvsE.Visibility = Visibility.Hidden;
				AvsLbl.Visibility = Visibility.Hidden;
			}
		}

		string tss = null;
		float money = 0;

		private void string_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((TextBox)sender).Text != "" && ((TextBox)sender).Text != " " && ((TextBox)sender).Text != "  ")
			{
				images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Hidden;
				if (images[0].Visibility == Visibility.Hidden &&
					images[1].Visibility == Visibility.Hidden &&
					images[2].Visibility == Visibility.Hidden &&
					images[3].Visibility == Visibility.Hidden &&
					images[4].Visibility == Visibility.Hidden &&
					images[5].Visibility == Visibility.Hidden &&
					images[6].Visibility == Visibility.Hidden)
					Add.IsEnabled = true;
				else
					Add.IsEnabled = false;
			}
			else
			{
				images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				Add.IsEnabled = false;
			}
			if (tss != null)
			{
				AvsE.Visibility = Visibility.Visible;
				AvsLbl.Visibility = Visibility.Visible; AvsLbl.Content = "AVS: " + tss; AvsLbl.Content = "AVS: " + tss;
			}
			else
			{
				AvsE.Visibility = Visibility.Hidden;
				AvsLbl.Visibility = Visibility.Hidden;
			}
		}

		private void nomber_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (((TextBox)sender).Text.Length == 10 && ((TextBox)sender).Text != "          ")
			{
				try
				{
					((TextBox)sender).Text = ulong.Parse(((TextBox)sender).Text).ToString();
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Hidden;
				}
				catch
				{
					images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				}
				if (images[0].Visibility == Visibility.Hidden &&
					images[1].Visibility == Visibility.Hidden &&
					images[2].Visibility == Visibility.Hidden &&
					images[3].Visibility == Visibility.Hidden &&
					images[4].Visibility == Visibility.Hidden &&
					images[5].Visibility == Visibility.Hidden &&
					images[6].Visibility == Visibility.Hidden)
					Add.IsEnabled = true;
				else
					Add.IsEnabled = false;
			}
			else
			{
				images[int.Parse(((TextBox)sender).Tag.ToString()) - 1].Visibility = Visibility.Visible;
				Add.IsEnabled = false;
			}
			if (tss != null)
			{
				AvsE.Visibility = Visibility.Visible;
				AvsLbl.Visibility = Visibility.Visible; AvsLbl.Content = "AVS: " + tss;
			}
			else
			{
				AvsE.Visibility = Visibility.Hidden;
				AvsLbl.Visibility = Visibility.Hidden;
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Win.mainWindow.Activate();
	}
}
