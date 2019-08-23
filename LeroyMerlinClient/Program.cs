using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace LeroyMerlinClient
{
    [Serializable]
    public class Program
    {
        public string Продавец = "";
        public List<Taga> listTables = new List<Taga>();
    }

    public static class Win
    {
        // Массив открытых окон информации о клиентах
        public static List<InfoAboutClient> IAC = new List<InfoAboutClient>();
        public static Program program = new Program();
        public const string log = "apbx2@mail.ru",
            shablonsend = "Уважаемый клиент! Вашему заказу на товар ( <obj> ) присвоен №<num>. Как только товар поступит к нам в магазин, Вы получите оповещение в виде СМС или телефонного звонка нашего сотрудника. Удачных покупок в Leroy Merlin! :)",
            shablondata = "Здравствуйте <name>! К сожалению ваш заказ №<num> ( <obj> ) еще не поступил к нам в магазин. Предполагаемая дата поступления товара <dateE>",
            richObzvon2 = @"Позвоните клиенту по телефону указаному выше используя следующий скрипт диалога с клиентом:

• Консультант: 
- 'Добрый день <name>, меня зовут (имя сотрудника ЛМ).
Я звоню вам по поводу вашего заказа №<num> (<obj>).
Хотел бы у вас поинтересоваться, заинтересованы ли вы еще в приобритении
товара?'

• КЛИЕНТА: 'ДА'

• Консультант:
- 'Отлично, тогда с радостью сообщаю вам о том, что ваш товар прибыл к нам в магазин и уже ждет вас.
Товар будет закреплен за номером заказа в течении двух дней, по истечению двух дней товар снимается с резерва.
Спасибо за ожидание товара, всего доброго!'

• КЛИЕНТ: 'НЕТ'
- 'Хорошо, могу ли я поинтересоваться?
Вы приобрели аналог товара или же нашли данный товар в другом магазине?
Спасибо!
Тогда прошу прощение за беспокойство, всего доброго и хорошего дня!'";
        public static string shablon
        {
            get
            {
                return "Здравствуйте <name>! Ваш заказ №<num> ( <obj> ) прибыл в наш магазин! Товар будет ожидать Вас в течении 2-х суток. Большое спасибо за ожидание! :) Наш телефон: " + settings.TelephoneM;
            }
        }
        //Файл настроек
        public static SettingsProgram settings = new SettingsProgram();
        // Основное окно
        public static MainWindow mainWindow;
        static BinaryFormatter formatter = new BinaryFormatter();

        /// <summary>
        /// Сохраняет текущуюю базу
        /// </summary>
        public static void SaveThisBase()
        {
            if (settings.path != null && settings.path != "")
                try
                {
                    using (FileStream fs = new FileStream(settings.path, FileMode.Create))
                        formatter.Serialize(fs, program);
                }
                catch { }
            else
                try
                {
                    using (FileStream fs = new FileStream("Becap.lmb", FileMode.Create))
                        formatter.Serialize(fs, program);
                }
                catch { }
        }

        /// <summary>
        /// Открывает текущую базу
        /// </summary>
        public static void OpenThisBase()
        {
            if (settings.path != null)
                try
                {
                    using (FileStream fs = new FileStream(settings.path, FileMode.OpenOrCreate))
                        program = (Program)formatter.Deserialize(fs);
                }
                catch
                {
                    try
                    {
                        using (FileStream fs = new FileStream("Becap.lmb", FileMode.OpenOrCreate))
                            program = (Program)formatter.Deserialize(fs);
                    }
                    catch
                    { program = new Program(); }
                }
            else
                try
                {
                    if (File.Exists("Becap.lmb"))
                        using (FileStream fs = new FileStream("Becap.lmb", FileMode.OpenOrCreate))
                            program = (Program)formatter.Deserialize(fs);
                    else
                        using (FileStream fs = new FileStream("Becap.lmb", FileMode.Create))
                            formatter.Serialize(fs, program);
                }
                catch { program = new Program(); }
        }

        /// <summary>
        /// Возращает только открытую базу
        /// </summary>
        /// <returns>class program</returns>
        public static Program GetOpenThisBase()
        {
            if (settings.path != null)
                try
                {
                    using (FileStream fs = new FileStream(settings.path, FileMode.OpenOrCreate))
                        return (Program)formatter.Deserialize(fs);
                }
                catch
                {
                    try
                    {
                        using (FileStream fs = new FileStream("Becap.lmb", FileMode.OpenOrCreate))
                            return (Program)formatter.Deserialize(fs);
                    }
                    catch
                    { return null; }
                }
            else
                try
                {
                    using (FileStream fs = new FileStream("Becap.lmb", FileMode.OpenOrCreate))
                        return (Program)formatter.Deserialize(fs);
                }
                catch { return null; }
        }

        /// <summary>
        /// Сохранение настроек
        /// </summary>
        public static void SaveSettings()
        {
            settings.state = mainWindow.WindowState;
            settings.posGrid[0] = mainWindow.Tablet.ColumnDefinitions[0].Width.Value;
            settings.posGrid[1] = mainWindow.Tablet.ColumnDefinitions[1].Width.Value;
            settings.posGrid[2] = mainWindow.Tablet.ColumnDefinitions[2].Width.Value;
            settings.posGrid[3] = mainWindow.Tablet.ColumnDefinitions[3].Width.Value;
            settings.posGrid[4] = mainWindow.Tablet.ColumnDefinitions[4].Width.Value;
            settings.posGrid[5] = mainWindow.Tablet.ColumnDefinitions[5].Width.Value;
            settings.position = new Point(mainWindow.Left, mainWindow.Top);
            settings.size = new Point(mainWindow.Width, mainWindow.Height);
            using (FileStream fs = new FileStream("S.settings", FileMode.Create))
                formatter.Serialize(fs, settings);
        }

        public static void OpenSettings()
        {
            if (File.Exists("S.settings"))
                try
                {
                    using (FileStream fs = new FileStream("S.settings", FileMode.OpenOrCreate))
                        settings = (SettingsProgram)formatter.Deserialize(fs);
                    mainWindow.WindowState = settings.state;
                    for (int i = 0; i < 6; i++)
                        mainWindow.Tablet.ColumnDefinitions[i].Width = new GridLength(Win.settings.posGrid[i], GridUnitType.Star);
                    mainWindow.Top = settings.position.Y;
                    mainWindow.Left = settings.position.X;
                    mainWindow.Width = settings.size.X;
                    mainWindow.Height = settings.size.Y;
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString(), "Непорядок"); }
        }
        
        /// <summary>
        /// Удаляет клиента из базы
        /// </summary>
        /// <param name="index">Номер клиента</param>
        public static void RemoveClient(int index)
        {
            OpenThisBase();
            mainWindow.Components.Children.RemoveAt(index);
            program.listTables.RemoveAt(index);
            SaveThisBase();
        }
    }
}