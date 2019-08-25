using System.Windows.Controls;
using System.Windows.Media;

namespace LeroyMerlinClient
{
	public partial class StartTaga : UserControl
	{
		public int index;
		public StartTaga(int index)
		{
			InitializeComponent();
			this.index = index;
			for (int i = 0; i < Win.program.listTables.Count; i++)
				if (Win.program.listTables[i].НомерЗаказа == index)
				{
					TextAr.Text = Win.program.listTables[i].ИмяКлиента + " | " + Win.program.listTables[i].НомерКлиента;
					if (Win.program.listTables[i].AVS == null)
						Rec.Fill = new SolidColorBrush(Color.FromArgb(79, 154, 205, 50));
					else
						Rec.Fill = new SolidColorBrush(Color.FromArgb(79, 205, 50, 50));
					break;
				}
		}
	}
}
