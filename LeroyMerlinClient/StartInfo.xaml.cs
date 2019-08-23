using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Office.Interop;
using System.Threading;

namespace LeroyMerlinClient
{
    public partial class StartInfo : Window
    {
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;

        bool one, two, three;
        Thread t;

        public StartInfo()
        {
            InitializeComponent();
            if (Win.settings.AdresM != "" && Win.settings.GorodM != "" && Win.settings.TelephoneM != "" &&
                Win.settings.AdresM != null && Win.settings.GorodM != null && Win.settings.TelephoneM != null)
            {
                Btn3.Visibility = Visibility.Collapsed;
                Ok3.Visibility = Visibility.Visible;
                three = true;
            }
            if (Win.settings.pass != "" && Win.settings.pass != null)
            {
                Btn2.Visibility = Visibility.Collapsed;
                Ok2.Visibility = Visibility.Visible;
                two = true;
            }
            if (Win.settings.listExcel.Count > 0)
            {
                Btn1.Visibility = Visibility.Collapsed;
                Ok1.Visibility = Visibility.Visible;
                one = true;
            }
            if (one == true && two == true && three == true)
            {
                Win.mainWindow.Show();
                Close();
            }
            else
            {
                t = new Thread(new ThreadStart(Upid));
                t.Start();
            }
        }

        void Upid()
        {
            while (true)
            {
                if (Dispatcher.Invoke(() => one == true && two == true && three == true))
                    break;
                if (Win.settings.pass != "" && Win.settings.pass != null && !two)
                {
                    Dispatcher.Invoke(() => Btn2.Visibility = Visibility.Collapsed);
                    Dispatcher.Invoke(() => Ok2.Visibility = Visibility.Visible);
                    Dispatcher.Invoke(() => two = true);
                }
                if (Win.settings.listExcel.Count > 0)
                {
                    Dispatcher.Invoke(() => Btn1.Visibility = Visibility.Collapsed);
                    Dispatcher.Invoke(() => Ok1.Visibility = Visibility.Visible);
                    Dispatcher.Invoke(() => one = true);
                }
                Thread.Sleep(1000);
            }
            Dispatcher.Invoke(() => Win.mainWindow.Show());
            Dispatcher.Invoke(() => Close());
            t.Abort();
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            { t.Abort(); }
            catch { }
            Environment.Exit(0);
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) =>       
            PostMessage(new WindowInteropHelper(this).Handle, WM_SYSCOMMAND, DOMOVE, 0);

        private void Btn2_Click(object sender, RoutedEventArgs e) => new SMSCabinet(this).Show();

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow a = new SettingsWindow(this);
            a.Tabler.SelectedIndex = 1;
            a.ShowDialog();
            if (Win.settings.AdresM != "" && Win.settings.GorodM != "" && Win.settings.TelephoneM != "" &&
                Win.settings.AdresM != null && Win.settings.GorodM != null && Win.settings.TelephoneM != null)
            {
                Btn3.Visibility = Visibility.Collapsed;
                Ok3.Visibility = Visibility.Visible;
                three = true;
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelapp;
            Microsoft.Office.Interop.Excel.Workbooks excelappworkbooks;
            Microsoft.Office.Interop.Excel.Worksheet excelworksheet;
            int impTimerMax;
            List<string> list_row = new List<string>();
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                excelapp = new Microsoft.Office.Interop.Excel.Application();
                excelappworkbooks = excelapp.Workbooks;
                excelworksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelapp.Workbooks.Open(ofd.FileName,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                 Type.Missing, Type.Missing).Worksheets.get_Item(1);

                impTimerMax = excelapp.Cells.Cells.SpecialCells(Microsoft.Office.Interop.Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row - 1;
                new LoadExcel(ofd.FileName, impTimerMax, null).Show();
            }
        }
    }
}
