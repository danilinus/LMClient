using System;
using System.Windows;

namespace LeroyMerlinClient
{
	public partial class CallWindow : Window
	{
		public int index;
		public CallWindow(int index)
		{
			InitializeComponent();
			this.index = index;
			Nomber.Content = "Номер: " + Win.program.listTables[index].НомерКлиента;
			TextObz.Text = Win.richObzvon2;
			TextObz.Text = TextObz.Text.Replace("<num>", Win.program.listTables[index].НомерЗаказа.ToString());
			TextObz.Text = TextObz.Text.Replace("<time>", DateTime.Now.ToString("hh:mm"));
			TextObz.Text = TextObz.Text.Replace("<date>", DateTime.Now.ToString("dd MMMMMMMMMMM"));
			TextObz.Text = TextObz.Text.Replace("<dateE>", Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMMMMM"));
			TextObz.Text = TextObz.Text.Replace("<name>", Win.program.listTables[index].ИмяКлиента);
			TextObz.Text = TextObz.Text.Replace("<nomber>", Win.program.listTables[index].НомерКлиента);
			TextObz.Text = TextObz.Text.Replace("<obj>", Win.program.listTables[index].ИмяТовара);
			TextObz.Text = TextObz.Text.Replace("<qua>", Win.program.listTables[index].Количество.ToString());
			TextObz.Text = TextObz.Text.Replace("<art>", Win.program.listTables[index].Артикул.ToString());
			TextObz.Text = TextObz.Text.Replace("<prod>", Win.program.listTables[index].Поставщик);
			Owner = Win.mainWindow;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Win.OpenThisBase();
			((Table)Win.mainWindow.Components.Children[index]).Статус = "Оповещён";

			Win.SaveThisBase();
			Win.settings.мобзвонено++;
			Win.settings.обзвонено++;
			Close();
		}
	}
}
