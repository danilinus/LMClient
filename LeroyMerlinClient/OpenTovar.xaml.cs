using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using ex = Microsoft.Office.Interop.Excel;

namespace LeroyMerlinClient
{
	public partial class OpenTovar : Window
	{
		public StartProgram a;
		public OpenTovar()
		{
			InitializeComponent();
			Owner = Win.mainWindow;

		}
		Thread Updater;
		private readonly System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
		public List<string> Keys = new List<string>();

		public void Upid()
		{
			ex.Workbook excelappworkbook;
			ex.Worksheet excelworksheet;

			excelappworkbook = new ex.Application().Workbooks.Open(ofd.FileName);
			int count = excelappworkbook.Worksheets.Count;
			for (int j = 1; j <= count; j++)
			{
				excelworksheet = (ex.Worksheet)excelappworkbook.Worksheets.get_Item(j);
				int i = 0; bool b = false;
				string one = Convert.ToString(((ex.Range)excelworksheet.Cells[12, 3]).Value2);
				string two = Convert.ToString(((ex.Range)excelworksheet.Cells[11, 3]).Value2);
				if (two != null && two != "")
					b = true;
				while (true)
				{
					if (!b)
					{
						string Item = Convert.ToString(((ex.Range)excelworksheet.Cells[12 + i * 2, 3]).Value2);
						if (Item != null && Item != "" && Item != "Дата печати")
							Keys.Add(Item + "|" + Convert.ToString(((ex.Range)excelworksheet.Cells[13 + i * 2, 6]).Value2));
						else
							break;
					}
					else
					{
						string Item = Convert.ToString(((ex.Range)excelworksheet.Cells[11 + i * 2, 3]).Value2);
						if (Item != null && Item != "" && Item != "Дата печати")
							Keys.Add(Item + "|" + Convert.ToString(((ex.Range)excelworksheet.Cells[12 + i * 2, 6]).Value2));
						else
							break;
					}
					i++;
					Thread.Sleep(1);
				}
			}
			excelappworkbook.Close();
			Dispatcher.Invoke(() => Second.Visibility = Visibility.Collapsed);
			Dispatcher.Invoke(() => Threed.Visibility = Visibility.Visible);
			List<int> ts = new List<int>();
			for (int j = 0; j < Keys.Count; j++)
				for (int i = 0; i < Dispatcher.Invoke(() => Win.program.listTables.Count); i++)
					if ((Dispatcher.Invoke(() => Win.program.listTables[i].Артикул.ToString()) == Keys[j].Split('|')[0] || Dispatcher.Invoke(() => Win.program.listTables[i].Артикул.ToString()) == Keys[j].Split('|')[1]) && Dispatcher.Invoke(() => Win.program.listTables[i].Статус) != StatusE.Оповещён)
					{
						Dispatcher.Invoke(() => Win.program.listTables[i].ДатаПрихода = DateTime.Now);
						Dispatcher.Invoke(() => ((Table)Win.mainWindow.Components.Children[i]).Статус = "Не Оповещён");
						ts.Add(Dispatcher.Invoke(() => i));
						break;
					}
			if (ts.Count > 0)
			{
				Dispatcher.Invoke(() => a = new StartProgram(ts.ToArray(), StartPrg.Calling, false));
				Dispatcher.Invoke(() => a.Lab.Content = "Найдены клиенты:");
				Dispatcher.Invoke(() => a.Show());
			}
			else
				MessageBox.Show("Программа поиска клиентов не выявила клиентов соответствующих excel документу");
			Dispatcher.Invoke(() => Close());
			Updater.Abort();
		}

		private void OpenFileExcel_Click(object sender, RoutedEventArgs e)
		{
			ofd.Filter = "All files (*.*)|*.*";
			if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				OpenFileExcel.Visibility = Visibility.Collapsed;
				First.Visibility = Visibility.Collapsed;
				Second.Visibility = Visibility.Visible;
				Updater = new Thread(new ThreadStart(Upid));
				Updater.Start();
			}
		}
	}
}