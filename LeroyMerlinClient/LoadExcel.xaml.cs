using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeroyMerlinClient
{
    public partial class LoadExcel : Window
    {
        Thread th;
        int maxS = 0;
        string fns = "";
        public LoadExcel(string path, int max)
        {
            InitializeComponent();
            fns = path;
            maxS = max;
            th = new Thread(new ThreadStart(Upid));
            th.Start();
            Owner = Win.mainWindow;
        }

        public LoadExcel(string path, int max, Window owner)
        {
            InitializeComponent();
            fns = path;
            maxS = max;
            th = new Thread(new ThreadStart(Upid));
            th.Start();
            Owner = owner;
        }

        Microsoft.Office.Interop.Excel.Workbooks excelappworkbooks;
        Microsoft.Office.Interop.Excel.Worksheet excelworksheet;

        void Upid()
        {
            excelappworkbooks = new Microsoft.Office.Interop.Excel.Application().Workbooks;
            excelworksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelappworkbooks.Application.Workbooks.Open(fns,
             Type.Missing, Type.Missing, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing, Type.Missing, Type.Missing,
             Type.Missing, Type.Missing).Worksheets.get_Item(1);

            for (int i = 2; i <= maxS; i++)
            {
                Thread.Sleep(1);
                try
                {
                    Dispatcher.Invoke(() => Win.settings.listExcel.Add(new ExcelTaga((DateTime.FromOADate(int.Parse(Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 11]).Value2)))).ToString("dd.MM.yyyy"),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 3]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 4]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 5]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 10]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 13]).Value2))));
                }
                catch
                {
                    Dispatcher.Invoke(() => Win.settings.listExcel.Add(new ExcelTaga(Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 11]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 3]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 4]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 5]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 10]).Value2),
                        Convert.ToString(((Microsoft.Office.Interop.Excel.Range)excelworksheet.Cells[i, 13]).Value2))));
                }
                Dispatcher.Invoke(() => Kol.Content = i + " из " + maxS);
            }
            Dispatcher.Invoke(() => Title = "Готово!");
            Dispatcher.Invoke(() => Oke.Visibility = Visibility.Visible);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            { excelappworkbooks.Application.Quit(); } catch { }
            try
            { if (th != null) th.Abort(); } catch { }
        }

        private void Oke_Click(object sender, RoutedEventArgs e) => Close();
        
    }
}