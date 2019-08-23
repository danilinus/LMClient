using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace LeroyMerlinClient
{
    public partial class MainWindow : Window
    {
        public Thread Updater;
        BinaryFormatter formatter = new BinaryFormatter();

        public MainWindow() => InitializeComponent();

        void Upid()
        {
            while (true)
            {
                float m = 0;
                for (int i = 0; i < Win.program.listTables.Count; i++)
                {
                    m += Win.program.listTables[i].Деньга * Win.program.listTables[i].Количество / 100;
                    Thread.Sleep(50);
                }
                Dispatcher.Invoke(() => Title = "LM Client - " + Win.program.listTables.Count.ToString() + " клиентов. " + "Общая стоймость: " + m.ToString() + "RUR");

                while (Dispatcher.Invoke(() => Win.program.listTables.Count != Components.Children.Count))
                {
                    if (Dispatcher.Invoke(() => Win.program.listTables.Count > Components.Children.Count))
                        Dispatcher.Invoke(() => Components.Children.Add(new Table()));
                    else
                        Dispatcher.Invoke(() => Components.Children.RemoveAt(Components.Children.Count - 1));
                }

                Dispatcher.Invoke(() => Win.OpenThisBase());

                for (int i = 0; i < Dispatcher.Invoke(() => Win.program.listTables.Count); i++)
                {
                    try
                    {
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).Дата = Win.program.listTables[i].Дата.ToString("dd.MM.yyyy"));
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).ДатаПрихода = Win.program.listTables[i].ДатаПрихода.ToString("dd.MM.yyyy"));
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).Артикул = Win.program.listTables[i].Артикул.ToString());
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).ИмяКлиента = Win.program.listTables[i].ИмяКлиента);
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).ИмяТовара = Win.program.listTables[i].ИмяТовара);
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).Статус = Win.program.listTables[i].Статус.ToString());
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).НомерКлиента = Win.program.listTables[i].НомерКлиента.ToString());
                        Dispatcher.Invoke(() => ((Table)Components.Children[i]).SizeChang());
                        Thread.Sleep(50);
                    }
                    catch { }
                }
                
                Thread.Sleep(3000);
            }
        }

        //Закрытие основного окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Updater.Abort();
            Win.SaveThisBase();
            Win.SaveSettings();
            Environment.Exit(0);
        }
      
        //Открыть окно разработчиков
        private void MenuItem_Click(object sender, RoutedEventArgs e) => new Razrabi().Show();
        
        //Открыть окно добавление клиента
        private void MenuItem_Click_1(object sender, RoutedEventArgs e) => new AddClient().Show();
        
        //Откроть окно настроек
        private void MenuItem_Click_2(object sender, RoutedEventArgs e) => new SettingsWindow().Show();        

        //Создать новую базу
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Win.SaveThisBase();
            
            SaveFileDialog fD = new SaveFileDialog();
            fD.Filter = "Leroy Base (*.lmb)|*.lmb|All files (*.*)|*.*";
            if (fD.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(fD.FileName, FileMode.Create))
                    formatter.Serialize(fs, new Program());
                Win.settings.path = fD.FileName;
                Win.program = new Program();
                ClearList();
            }
        }

        //Открыть новую базу
        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Win.SaveThisBase();
            
            OpenFileDialog fD = new OpenFileDialog();
            fD.Filter = "Leroy Base (*.lmb)|*.lmb|All files (*.*)|*.*";
            if (fD.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(fD.FileName, FileMode.OpenOrCreate))
                    Win.program = (Program)formatter.Deserialize(fs);
                Win.settings.path = fD.FileName;
                ClearList();
            }
        }

        //Очистить таблицу
        public void ClearList() => Components.Children.Clear();
        
        //Сохранить базу как
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Win.SaveThisBase();
            
            SaveFileDialog fD = new SaveFileDialog();
            fD.Filter = "Leroy Base (*.lmb)|*.lmb|All files (*.*)|*.*";
            if (fD.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(fD.FileName, FileMode.Create))
                    formatter.Serialize(fs, Win.program);
                Win.settings.path = fD.FileName;
            }
        }

        //Открыть окно поиска
        private void MenuItem_Click_6(object sender, RoutedEventArgs e) => new Find().Show();

        //Открыть оналйн поддержку
        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            if (File.Exists("ln.dlb"))
                if (File.Exists("ps.dlb"))
                    if (File.Exists("nns.dlb"))
                        System.Diagnostics.Process.Start("shari.exe");
                    else new VLog().Show();
                else new VLog().Show();
            else new VLog().Show();
        }

        //Открыть СМС кабинет
        private void MenuItem_Click_9(object sender, RoutedEventArgs e) => new SMSCabinet().Show();
        

        Microsoft.Office.Interop.Excel.Application excelapp;
        Microsoft.Office.Interop.Excel.Workbooks excelappworkbooks;
        Microsoft.Office.Interop.Excel.Worksheet excelworksheet;
        public int m, n, b = 0, ICint = 0, impTimer, impTimerMax, timer1Int, timer1Int2;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<int> alist = new List<int>();
            List<int> alist2 = new List<int>();
            for (int i = 0; i < Win.program.listTables.Count; i++)
            {
                if (Win.program.listTables[i].ДатаПрихода <= DateTime.Now && Win.program.listTables[i].Статус != StatusE.Оповещён)
                {
                    Win.program.listTables[i].Статус = StatusE.НеОповещён;
                    alist.Add(i);
                }
                if (Win.program.listTables[i].ДатаПрихода.AddDays(2) <= DateTime.Now && Win.program.listTables[i].Статус == StatusE.Оповещён)
                    alist2.Add(i);
            }
            bool b = false;
            if (alist.Count > 0 && alist2.Count > 0)
                b = true;
                if (alist.Count > 0) new StartProgram(alist.ToArray(), StartPrg.Calling,b).Show();
            if (alist2.Count > 0) new StartProgram(alist2.ToArray(), StartPrg.Removing,b).Show();
            Updater = new Thread(new ThreadStart(Upid));
            Updater.Start();

            if (DateTime.Now.Month != Win.settings.статистика.Month)
            {
                Win.settings.мдобавленоклиентов = Win.settings.мнапечатано = Win.settings.мобзвонено = Win.settings.мсмсразослано = 0;
                Win.settings.мзаработано = 0;
                Win.settings.статистика = DateTime.Now;
            }
        }

        private void Tablet_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            for (int i = 0; i < Components.Children.Count; i++)
                ((Table)Components.Children[i]).SizeChang();
        }

        private void Tablet_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            for (int i = 0; i < Components.Children.Count; i++)
                ((Table)Components.Children[i]).SizeChang();
        }

        private void G1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            for (int i = 0; i < Components.Children.Count; i++)
                ((Table)Components.Children[i]).SizeChang();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            for (int i = 0; i < Components.Children.Count; i++)
                ((Table)Components.Children[i]).SizeChang();
        }

        //Открыть окно сверки товаров
        private void MenuItem_Click_12(object sender, RoutedEventArgs e) => new OpenTovar().Show();
        
        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
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
                new LoadExcel(ofd.FileName, impTimerMax).Show();
            }
        }
    }
}