using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LeroyMerlinClient
{
	public partial class Find : Window
	{
		public Find()
		{
			InitializeComponent();
			Owner = Win.mainWindow;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			PanelX.Children.Clear();
			string[] tagsS = Input.Text.Split(',');
			List<int> t = new List<int>();


			for (int i = 0; i < tagsS.Length; i++)
			{
				for (int j = 0; j < Win.program.listTables.Count; j++)
				{
					bool b = false;
					for (int k = 0; k < t.Count; k++)
					{
						if (j == t[k])
						{
							b = true;
							break;
						}
					}

					if (!b)
						if (Win.program.listTables[j].Артикул.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].ИмяКлиента.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].ИмяПродавца.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].ИмяТовара.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].Количество.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].НомерЗаказа.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].НомерКлиента.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].Поставщик.ToString().Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].Дата.ToString("dd.MM.yyyy").Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].ДатаПрихода.ToString("dd.MM.yyyy").Contains(tagsS[i]))
						{
							t.Add(j);
							b = true;
						}

					if (!b)
						if (Win.program.listTables[j].AVS != null)
							if (Win.program.listTables[j].AVS.Contains(tagsS[i]))
							{
								t.Add(j);
								b = true;
							}
				}
			}

			for (int i = 0; i < t.Count; i++)
			{
				PanelX.Children.Add(new StartTaga(t[i]));
				PanelX.Children[i].MouseLeftButtonUp += new MouseButtonEventHandler(Cl);
			}
		}

		private void Cl(object sender, EventArgs e)
		{
			InfoAboutClient a = new InfoAboutClient(((StartTaga)sender).index);
			a.Show();
		}

		private void Input_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Input.Text == "" || Input.Text == " " || Input.Text == "  ")
				BG.IsEnabled = false;
			else
				BG.IsEnabled = true;
		}

		private void Grid_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (BG.IsEnabled)
				{
					PanelX.Children.Clear();
					string[] tagsS = Input.Text.Split(',');
					List<int> t = new List<int>();


					for (int i = 0; i < tagsS.Length; i++)
					{
						for (int j = 0; j < Win.program.listTables.Count; j++)
						{
							bool b = false;
							for (int k = 0; k < t.Count; k++)
							{
								if (j == t[k])
								{
									b = true;
									break;
								}
							}

							if (!b)
								if (Win.program.listTables[j].Артикул.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].ИмяКлиента.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].ИмяПродавца.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].ИмяТовара.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].Количество.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].НомерЗаказа.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].НомерКлиента.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].Поставщик.ToString().Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].Дата.ToString("dd.MM.yyyy").Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].ДатаПрихода.ToString("dd.MM.yyyy").Contains(tagsS[i]))
								{
									t.Add(j);
									b = true;
								}

							if (!b)
								if (Win.program.listTables[j].AVS != null)
									if (Win.program.listTables[j].AVS.Contains(tagsS[i]))
									{
										t.Add(j);
										b = true;
									}
						}
					}

					for (int i = 0; i < t.Count; i++)
					{
						PanelX.Children.Add(new StartTaga(i));
						PanelX.Children[i].MouseLeftButtonUp += new MouseButtonEventHandler(Cl);
					}
				}
			}
		}
	}
}
