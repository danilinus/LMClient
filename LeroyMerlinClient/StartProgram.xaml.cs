using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace LeroyMerlinClient
{
	public partial class StartProgram : Window
	{
		private readonly List<int> t = new List<int>();
		private readonly StartPrg prgType;
		private readonly bool two;
		public StartProgram(int[] t, StartPrg prgType, bool two)
		{
			InitializeComponent();
			this.prgType = prgType;
			this.two = two;
			Owner = Win.mainWindow;
			this.t.AddRange(t);
			if (prgType == StartPrg.Calling)
				for (int i = 0; i < t.Length; i++)
				{
					PanelX.Children.Add(new StartTaga(Win.program.listTables[t[i]].НомерЗаказа));
					((StartTaga)PanelX.Children[i]).MouseLeftButtonUp += new MouseButtonEventHandler(Ci);
				}
			else
			{
				Lab.Content = "Вам следует удалить клиентов:";
				Lab.ToolTip = "Что бы удалить клиента из списка нажмите на его ПКМ";
				Ok.Click -= new RoutedEventHandler(Button_Click);
				Ok.Click += new RoutedEventHandler(Remove);
				Ok.Content = "Удалить всех";

				for (int i = 0; i < t.Length; i++)
				{
					PanelX.Children.Add(new StartTaga(Win.program.listTables[t[i]].НомерЗаказа));
					((StartTaga)PanelX.Children[i]).MouseLeftButtonUp += new MouseButtonEventHandler(LKM);
					((StartTaga)PanelX.Children[i]).MouseRightButtonUp += new MouseButtonEventHandler(PKM);
				}
			}
		}
		private void Remove(object sender, EventArgs e)
		{
			for (int i = 0; i < t.Count; i++)
				for (int j = 0; j < Win.program.listTables.Count; j++)
					if (Win.program.listTables[j].НомерЗаказа == ((StartTaga)PanelX.Children[i]).index)
					{
						Win.program.listTables.RemoveAt(j);
						break;
					}
			Win.SaveThisBase();
			Close();
		}
		private void PKM(object sender, EventArgs e)
		{
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i].НомерЗаказа == ((StartTaga)sender).index)
				{
					new InfoAboutClient(i).Show();
					break;
				}
		}
		private void LKM(object sender, EventArgs e)
		{
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i].НомерЗаказа == ((StartTaga)sender).index)
				{
					Win.program.listTables.RemoveAt(i);
					break;
				}
			Win.SaveThisBase();
			t.Remove(((StartTaga)sender).index);
			PanelX.Children.RemoveAt(PanelX.Children.IndexOf(((StartTaga)sender)));
			if (PanelX.Children.Count == 0)
				Close();
		}
		private void Ci(object sender, EventArgs e)
		{
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i].НомерЗаказа == ((StartTaga)sender).index)
				{
					new InfoAboutClient(i).Show();
					break;
				}
			if (Win.settings.delStart)
			{
				PanelX.Children.RemoveAt(PanelX.Children.IndexOf(((StartTaga)sender)));
				if (PanelX.Children.Count == 0)
					Close();
			}
		}
		private void Button_Click(object sender, RoutedEventArgs e) => Close();

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (two)
				if (prgType == StartPrg.Calling) Top -= Height / 2;
				else
					Top += Height / 2;
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Win.mainWindow.Activate();
	}

	public enum StartPrg
	{
		Calling,
		Removing
	}
}