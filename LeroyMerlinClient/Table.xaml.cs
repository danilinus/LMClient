using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LeroyMerlinClient
{
	[Serializable]
	public partial class Table : UserControl, INotifyPropertyChanged
	{

		public string Дата
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Дата.ToString("dd.MM.yyyy");
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Дата = DateTime.Parse(value);
				Win.SaveThisBase();
				OnPropertyChanged("Дата");
			}
		}
		public string ДатаПрихода
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ДатаПрихода.ToString("dd.MM.yyyy");
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ДатаПрихода = DateTime.Parse(value);
				Win.SaveThisBase();
				OnPropertyChanged("ДатаПрихода");
			}
		}
		public string Артикул
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Артикул.ToString();
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Артикул = ulong.Parse(value);
				Win.SaveThisBase();
				OnPropertyChanged("Артикул");
			}
		}
		public string ИмяКлиента
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ИмяКлиента;
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ИмяКлиента = value;
				Win.SaveThisBase();
				OnPropertyChanged("ИмяКлиента");
			}
		}
		public string ИмяТовара
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ИмяТовара;
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ИмяТовара = value;
				Win.SaveThisBase();
				OnPropertyChanged("ИмяТовара");
			}
		}
		public string НомерКлиента
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].НомерКлиента.ToString();
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].НомерКлиента = value;
				Win.SaveThisBase();
				OnPropertyChanged("НомерКлиента");
			}
		}
		public string Статус
		{
			get
			{
				if (Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус == StatusE.Оповещён)
				{
					GridDataR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));
					GridArtikulR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));
					GridNameTovarR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));
					GridNomberR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));
					GridNameClientR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));
					GridDataEndR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));
					GridStatusR.Fill = new SolidColorBrush(Color.FromArgb(60, 34, 105, 0));

					GridDataR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
					GridArtikulR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
					GridNameTovarR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
					GridNomberR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
					GridNameClientR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
					GridDataEndR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
					GridStatusR.Stroke = new SolidColorBrush(Color.FromArgb(150, 54, 105, 0));
				}
				else
				if (Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].AVS == null)
				{
					GridDataR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					GridArtikulR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					GridNameTovarR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					GridNomberR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					GridNameClientR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					GridDataEndR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					GridStatusR.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
				}
				else
				{
					GridDataR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					GridArtikulR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					GridNameTovarR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					GridNomberR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					GridNameClientR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					GridDataEndR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					GridStatusR.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
				}
				if (Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус == StatusE.Оповещён)
					return "Оповещён";
				else
				if (Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус == StatusE.НеОповещён)
					return "Не Оповещён";
				else
				if (Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус == StatusE.ВПроцессе)
					return "В Процессе";
				else
					return "";
			}
			set
			{
				if (value == "Оповещён")
					Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус = StatusE.Оповещён;
				else
					if (value == "Не Оповещён" || value == "НеОповещён")
					Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус = StatusE.НеОповещён;
				else
					Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Статус = StatusE.ВПроцессе;
				Win.SaveThisBase();
				OnPropertyChanged("Статус");
			}
		}
		public string НомерЗаказа
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].НомерЗаказа.ToString();
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].НомерЗаказа = int.Parse(value);
				OnPropertyChanged("НомерЗаказа");
			}
		}
		public string Поставщик
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Поставщик;
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Поставщик = value;
				OnPropertyChanged("Поставщик");
			}
		}
		public string Количество
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Количество.ToString();
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Количество = int.Parse(value);
				OnPropertyChanged("Количество");
			}
		}
		public string ИмяПродавца
		{
			get
			{
				return Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ИмяПродавца;
			}
			set
			{
				Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].ИмяПродавца = value;
				OnPropertyChanged("ИмяПродавца");
			}
		}

		public Table()
		{
			InitializeComponent();
			DataContext = this;
		}

		private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		public event PropertyChangedEventHandler PropertyChanged;
		private void ThisGrid_SizeChanged(object sender, SizeChangedEventArgs e) => SizeChang();

		public void SizeChang()
		{
			try
			{
				GridData.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[0].ActualWidth - 2.5f;
				GridArtikul.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[1].ActualWidth - 5f;
				GridNameTovar.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[2].ActualWidth - 5f;
				GridNomber.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[3].ActualWidth - 5f;
				GridNameClient.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[4].ActualWidth - 5f;
				GridDataEnd.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[5].ActualWidth - 5f;
				GridStatus.Width = ((Grid)((Grid)((ScrollViewer)((StackPanel)Parent).Parent).Parent).Children[0]).ColumnDefinitions[6].ActualWidth - 2.5f;
			}
			catch { }
		}

		private void Tablet_Loaded(object sender, RoutedEventArgs e)
		{
			Label ToolTipLabel = new Label()
			{
				Content =
				"Номер заказа: " + Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].НомерЗаказа + '\n' +
				"   Количество: " + Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Количество + '\n' +
				"    Поставщик: " + Win.program.listTables[Win.mainWindow.Components.Children.IndexOf(this)].Поставщик
			};
			GridData.ToolTip = ToolTipLabel;
			GridArtikul.ToolTip = ToolTipLabel;
			GridNameTovar.ToolTip = ToolTipLabel;
			GridNomber.ToolTip = ToolTipLabel;
			GridNameClient.ToolTip = ToolTipLabel;
			GridDataEnd.ToolTip = ToolTipLabel;
			GridStatus.ToolTip = ToolTipLabel;
		}

		private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) => new InfoAboutClient(Win.mainWindow.Components.Children.IndexOf(this)).Show();
	}

	[Serializable]
	public class Taga
	{
		public DateTime Дата, ДатаПрихода;
		public string AVS;
		public ulong Артикул;
		public int НомерЗаказа, Количество, indxK = 0;
		public float Деньга;
		public string ИмяТовара,
					  ИмяПродавца,
					  НомерКлиента,
					  ИмяКлиента,
					  Поставщик;
		public StatusE Статус;

		public Taga(DateTime Дата, DateTime ДатаПрихода, ulong Артикул, int НомерЗаказа, int Количество, string ИмяТовара,
					string НомерКлиента, string ИмяКлиента, StatusE Статус, string Поставщик, string ИмяПродавца)
		{
			this.Дата = Дата;
			this.ДатаПрихода = ДатаПрихода;
			this.Артикул = Артикул;
			this.НомерЗаказа = НомерЗаказа;
			this.Количество = Количество;
			this.ИмяТовара = ИмяТовара;
			this.НомерКлиента = НомерКлиента;
			this.ИмяКлиента = ИмяКлиента;
			this.Статус = Статус;
			this.Поставщик = Поставщик;
			this.ИмяПродавца = ИмяПродавца;
		}

		public Taga(Taga t)
		{
			Дата = t.Дата;
			ДатаПрихода = t.ДатаПрихода;
			Артикул = t.Артикул;
			НомерЗаказа = t.НомерЗаказа;
			Количество = t.Количество;
			ИмяТовара = t.ИмяТовара;
			НомерКлиента = t.НомерКлиента;
			ИмяКлиента = t.ИмяКлиента;
			Статус = t.Статус;
			Поставщик = t.Поставщик;
			ИмяПродавца = t.ИмяПродавца;
		}

		public static bool operator ==(Taga left, Taga right)
		{
			bool b = false;
			if (left.AVS == right.AVS &&
			   left.Артикул == right.Артикул &&
			   left.Дата == right.Дата &&
			   left.ДатаПрихода == right.ДатаПрихода &&
			   left.ИмяКлиента == right.ИмяКлиента &&
			   left.ИмяПродавца == right.ИмяПродавца &&
			   left.ИмяТовара == right.ИмяТовара &&
			   left.Количество == right.Количество &&
			   left.НомерЗаказа == right.НомерЗаказа &&
			   left.НомерКлиента == right.НомерКлиента &&
			   left.Поставщик == right.Поставщик &&
			   left.Статус == right.Статус)
				b = true;
			return b;
		}

		public static bool operator !=(Taga left, Taga right)
		{
			bool b = true;
			if (left.AVS == right.AVS &&
			   left.Артикул == right.Артикул &&
			   left.Дата == right.Дата &&
			   left.ДатаПрихода == right.ДатаПрихода &&
			   left.ИмяКлиента == right.ИмяКлиента &&
			   left.ИмяПродавца == right.ИмяПродавца &&
			   left.ИмяТовара == right.ИмяТовара &&
			   left.Количество == right.Количество &&
			   left.НомерЗаказа == right.НомерЗаказа &&
			   left.НомерКлиента == right.НомерКлиента &&
			   left.Поставщик == right.Поставщик &&
			   left.Статус == right.Статус)
				b = false;
			return b;
		}

		public override bool Equals(object o)
		{
			bool b = false;
			if (AVS == ((Taga)o).AVS &&
			   Артикул == ((Taga)o).Артикул &&
			   Дата == ((Taga)o).Дата &&
			   ДатаПрихода == ((Taga)o).ДатаПрихода &&
			   ИмяКлиента == ((Taga)o).ИмяКлиента &&
			   ИмяПродавца == ((Taga)o).ИмяПродавца &&
			   ИмяТовара == ((Taga)o).ИмяТовара &&
			   Количество == ((Taga)o).Количество &&
			   НомерЗаказа == ((Taga)o).НомерЗаказа &&
			   НомерКлиента == ((Taga)o).НомерКлиента &&
			   Поставщик == ((Taga)o).Поставщик &&
			   Статус == ((Taga)o).Статус)
				b = true;
			return b;
		}

		public override int GetHashCode()
		{
			var hashCode = -1420007036;
			hashCode = hashCode * -1521134295 + Дата.GetHashCode();
			hashCode = hashCode * -1521134295 + ДатаПрихода.GetHashCode();
			hashCode = hashCode * -1521134295 + AVS.GetHashCode();
			hashCode = hashCode * -1521134295 + Артикул.GetHashCode();
			hashCode = hashCode * -1521134295 + НомерЗаказа.GetHashCode();
			hashCode = hashCode * -1521134295 + Количество.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ИмяТовара);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ИмяПродавца);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(НомерКлиента);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ИмяКлиента);
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Поставщик);
			hashCode = hashCode * -1521134295 + Статус.GetHashCode();
			return hashCode;
		}
	}

	[Serializable]
	public enum StatusE
	{
		ВПроцессе,
		НеОповещён,
		Оповещён
	}
}