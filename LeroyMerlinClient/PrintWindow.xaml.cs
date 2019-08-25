using System;
using System.Drawing.Printing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LeroyMerlinClient
{
	public partial class PrintWindow : Window
	{
		public int index;
		public DrawingImage image;
		public PrintWindow(int index)
		{
			InitializeComponent();
			try
			{
				this.index = index;
				BitmapImage bi = new BitmapImage();
				bi.BeginInit();
				MemoryStream ms = new MemoryStream();
				DrawWatermark(System.Drawing.Image.FromFile(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("LeroyMerlinClient.exe", "BlanckSleep.png"))).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
				ms.Seek(0, SeekOrigin.Begin);
				bi.StreamSource = ms;
				bi.EndInit();
				ListA.Source = bi;
			}
			catch { }
			Owner = Win.mainWindow;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			PrintDocument myPrintDocument1 = new PrintDocument();
			myPrintDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
			new System.Windows.Forms.PrintDialog().Document = myPrintDocument1;
			if (new System.Windows.Forms.PrintDialog().ShowDialog() == System.Windows.Forms.DialogResult.OK)
				myPrintDocument1.Print();
			Win.settings.мнапечатано++;
			Win.settings.напечатано++;
			Close();
		}

		private System.Drawing.Image DrawWatermark(System.Drawing.Image originalImage)
		{
			System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(originalImage.Width, originalImage.Height);
			using (System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(bitmap))
			{
				gr.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, originalImage.Width, originalImage.Height));

				gr.DrawString("№" + Win.program.listTables[index].НомерЗаказа.ToString(), new System.Drawing.Font("Arial", 80, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, 1180, 1025);
				gr.DrawString(Win.program.listTables[index].ИмяПродавца.ToString(), new System.Drawing.Font("Arial", 38, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, 2010, 1303);
				gr.DrawString(Win.program.listTables[index].Дата.ToString("dd MMMMMMMM"), new System.Drawing.Font("Arial", 38, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black, 2010, 1368);

				int szT = 50;
				while (gr.MeasureString(Win.program.listTables[index].Артикул.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width > 494 - 53 - 10)
					szT--;
				while (gr.MeasureString(Win.program.listTables[index].ИмяТовара, new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width > 1573 - 494 - 10)
					szT--;
				while (gr.MeasureString(Win.program.listTables[index].Количество.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width > 1789 - 1573 - 10)
					szT--;
				while (gr.MeasureString(Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMM"), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width > 2416 - 1789 - 10)
					szT--;

				gr.DrawString(Win.program.listTables[index].Артикул.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black,
					268 - gr.MeasureString(Win.program.listTables[index].Артикул.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width / 2,
					1747 - gr.MeasureString(Win.program.listTables[index].Артикул.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Height / 2);
				gr.DrawString(Win.program.listTables[index].ИмяТовара, new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black,
					1028 - gr.MeasureString(Win.program.listTables[index].ИмяТовара, new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width / 2,
					1747 - gr.MeasureString(Win.program.listTables[index].ИмяТовара, new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Height / 2);
				gr.DrawString(Win.program.listTables[index].Количество.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black,
					1676 - gr.MeasureString(Win.program.listTables[index].Количество.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width / 2,
					1747 - gr.MeasureString(Win.program.listTables[index].Количество.ToString(), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Height / 2);
				gr.DrawString(Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMM"), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular), System.Drawing.Brushes.Black,
					2097 - gr.MeasureString(Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMM"), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Width / 2,
					1747 - gr.MeasureString(Win.program.listTables[index].ДатаПрихода.ToString("dd MMMMMMMM"), new System.Drawing.Font("Arial", szT, System.Drawing.FontStyle.Regular)).Height / 2);

				gr.DrawString("Наш телефон: " + Win.settings.TelephoneM + "\nНаш адресс: г. " + Win.settings.GorodM + "\n" + Win.settings.AdresM, new System.Drawing.Font("Arial", 34, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Black, 1550, 2400);

				return bitmap;
			}
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			try
			{
				e.Graphics.DrawImage(DrawWatermark(System.Drawing.Image.FromFile(System.Reflection.Assembly.GetExecutingAssembly().Location.Replace("LeroyMerlinClient.exe", "") + @"BlanckSleep.png")), 0, 0, 800, 1120);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Win.mainWindow.Activate();
	}
}