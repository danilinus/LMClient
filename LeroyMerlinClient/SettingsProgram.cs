using System;
using System.Collections.Generic;
using System.Windows;

namespace LeroyMerlinClient
{
	[Serializable]
	public class SettingsProgram
	{
		public string path = null;
		public int yestoday2, reboot;
		public WindowState state = WindowState.Normal;
		public Point position;
		public Point size;
		public bool delStart = true;
		public double[] posGrid = new double[6];
		public string pass = "", today = "", passa = "", GorodM = "", AdresM = "", TelephoneM = "";
		public DateTime yestoday, статистика;
		public List<ExcelTaga> listExcel = new List<ExcelTaga>();

		//Переменные статистики
		//Общие
		public long обзвонено, напечатано, смсразослано, добавленоклиентов;
		//Месячные данные
		public long мобзвонено, мнапечатано, мсмсразослано, мдобавленоклиентов;
		public float мзаработано;
	}

	[Serializable]
	public class ExcelTaga
	{
		public string AVS;
		public string Артикул, Штрихкод;
		public string Деньга;
		public string ИмяТовара, Поставщик;
		public long Куплено;

		public ExcelTaga(string AVS, string Артикул, string Штрихкод, string ИмяТовара, string Поставщик, string Деньга)
		{
			this.AVS = AVS;
			this.ИмяТовара = ИмяТовара;
			this.Артикул = Артикул;
			this.Штрихкод = Штрихкод;
			this.Поставщик = Поставщик;
			this.Деньга = Деньга;
		}

		public ExcelTaga(ExcelTaga t)
		{
			AVS = t.AVS;
			ИмяТовара = t.ИмяТовара;
			Штрихкод = t.Штрихкод;
			Артикул = t.Артикул;
			Деньга = t.Деньга;
			Поставщик = t.Поставщик;
		}
	}
}