using LeroyMerlinClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;

namespace Messages
{

    public partial class MainWindow : Window
    {
        Thread myThread;
        List<string> mes = new List<string>();
        List<bool> I = new List<bool>();
        List<SmmS> lblList = new List<SmmS>();
        string[] mesA;
        bool[] il;
        bool connected = false, goo = true;

        async void Count()
        {            
            while (true)
            {
                await Task.Delay(1000);
                if (mes.Count > 0)
                {
                    mes.Clear();
                    I.Clear();
                }
                try
                {
                    var getHistory = Program.vk.Messages.GetHistory(new MessagesGetHistoryParams
                    {
                        Count = 200,
                        UserId = Program.chatID,
                    });
                    for (int i = 0; i < getHistory.Messages.Count; i++)
                    {

                        if (getHistory.Messages[i].Body != "" &&
                            getHistory.Messages[i].Body != " " &&
                            getHistory.Messages[i].Body != "  ")
                        {
                            if (Program.chatID == getHistory.Messages[i].FromId)
                            {
                                mes.Add(getHistory.Messages[i].Body);
                                I.Add(false);
                            }
                            else
                            {
                                mes.Add(getHistory.Messages[i].Body);
                                I.Add(true);
                            }
                        }
                    }
                }
                catch
                {
                    connected = false;
                    try
                    {
                        Program.vk.Authorize(new ApiAuthParams
                        {
                            ApplicationId = Program.appIP,
                            Login = Program.phone,
                            Password = Program.pass,
                            Settings = Settings.Messages,
                            AccessToken = "386ba6aa386ba6aa386ba6aaa938352fd13386b386ba6aa61ac353feb45c4731d3698d7"
                        });
                        connected = true;
                    }
                    catch (CaptchaNeededException cEx)
                    {
                        Program.cSid = cEx.Sid;
                        Uri captchaUrl = cEx.Img;
                        Capcha c = new Capcha(captchaUrl);
                        c.Show();
                        SaveMesSet();
                        Close();
                    }
                    Dispatcher.Invoke(() => ConectedR.Fill = new SolidColorBrush(Color.FromRgb(174, 70, 61)));
                    break;
                }
                if (mesA != null) {
                    Array.Reverse(mesA);
                    if (mesA.Length != mes.Count)
                    {
                        if (Dispatcher.Invoke(() => !fi))
                        {
                            Dispatcher.Invoke(() => Title = "Новое сообщение");
                            Dispatcher.Invoke(() => Icon = MF);
                        }
                        mesA = mes.ToArray();
                    } }else
                    mesA = mes.ToArray();
                il = I.ToArray();
                Array.Reverse(mesA);
                Array.Reverse(il);

                if (Dispatcher.Invoke(() => fi))
                {
                    Dispatcher.Invoke(() => Title = "Онлайн-чат помощь");
                    Dispatcher.Invoke(() => Icon = haba);
                }

                for (int i = lblList.Count; i < il.Length; i++)
                {
                    Dispatcher.Invoke(() => lblList.Add(new SmmS()));
                    if (mesA[i].Contains("♦"))
                    {
                        Dispatcher.Invoke(() => lblList[i].GetContentText(mesA[i].Split('♦')[1]));
                        Dispatcher.Invoke(() => lblList[i].GetContentName(mesA[i].Split('♦')[0]));
                    }
                    else
                        Dispatcher.Invoke(() => lblList[i].GetContentText(mesA[i]));
                    int sik = mesA[i].Length * 12;

                    Dispatcher.Invoke(() => lblList[i].MaxWidth = sik);
                    if (il[i]) {
                        Dispatcher.Invoke(() => lblList[i].GetBackColor(Color.FromArgb(127, 154, 205, 50)));
                        Dispatcher.Invoke(() => lblList[i].Griding.Background.Opacity = 0.5f);
                        Dispatcher.Invoke(() => lblList[i].HorizontalAlignment = HorizontalAlignment.Right);
                        Dispatcher.Invoke(() => lblList[i].txtName.HorizontalAlignment = HorizontalAlignment.Right);
                        if (Program.chatID == 448866327)
                            Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(255, 0, 0)));
                        else
                            Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(0, 0, 255)));
                    }
                    else {
                        Dispatcher.Invoke(() => lblList[i].GetBackColor(Color.FromArgb(178, 158, 158, 158)));
                        Dispatcher.Invoke(() => lblList[i].Griding.Background.Opacity = 0.7f);
                        Dispatcher.Invoke(() => lblList[i].HorizontalAlignment = HorizontalAlignment.Left);
                        Dispatcher.Invoke(() => lblList[i].txtName.HorizontalAlignment = HorizontalAlignment.Left);
                        if (Program.chatID != 448866327)
                            Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(255, 0, 0)));
                        else
                            Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(0, 0, 255)));
                    }
                    Dispatcher.Invoke(() => PanelX.Children.Add(lblList[i]));
                }

                if(goo)
                    Dispatcher.Invoke(() => Scroll.ScrollToBottom());

                if (connected)
                    if (Dispatcher.Invoke(() => ConectedR.Fill != Brushes.YellowGreen))
                        Dispatcher.Invoke(() => ConectedR.Fill = Brushes.YellowGreen);
                    else;
                else
                    if (Dispatcher.Invoke(() => ConectedR.Fill != new SolidColorBrush(Color.FromRgb(174, 70, 61))))
                    Dispatcher.Invoke(() => ConectedR.Fill = new SolidColorBrush(Color.FromRgb(174, 70, 61)));
            }
        }


        void Repersting()
        {
            for (int i = 0; i < lblList.Count; i++)
            {
                if (mesA[i].Contains("♦"))
                {
                    Dispatcher.Invoke(() => lblList[i].GetContentText(mesA[i].Split('♦')[1]));
                    Dispatcher.Invoke(() => lblList[i].GetContentName(mesA[i].Split('♦')[0]));
                }
                else
                    Dispatcher.Invoke(() => lblList[i].GetContentText(mesA[i]));
                int sik = mesA[i].Length * 12;

                Dispatcher.Invoke(() => lblList[i].MaxWidth = sik);
                if (il[i])
                {
                    Dispatcher.Invoke(() => lblList[i].GetBackColor(Color.FromArgb(127, 154, 205, 50)));
                    Dispatcher.Invoke(() => lblList[i].Griding.Background.Opacity = 0.5f);
                    Dispatcher.Invoke(() => lblList[i].HorizontalAlignment = HorizontalAlignment.Right);

                    if (Program.chatID == 448866327)
                        Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(255, 0, 0)));
                    else
                        Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(0, 0, 255)));
                }
                else
                {
                    Dispatcher.Invoke(() => lblList[i].GetBackColor(Color.FromArgb(178, 158, 158, 158)));
                    Dispatcher.Invoke(() => lblList[i].Griding.Background.Opacity = 0.7f);
                    Dispatcher.Invoke(() => lblList[i].HorizontalAlignment = HorizontalAlignment.Left);

                    if (Program.chatID != 448866327)
                        Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(255, 0, 0)));
                    else
                        Dispatcher.Invoke(() => lblList[i].GetNameColor(Color.FromRgb(0, 0, 255)));
                }
            }
        }

        BitmapImage haba = new BitmapImage();
        BitmapImage MF = new BitmapImage();
        public MainWindow(string cK)
        {
            InitializeComponent();
            haba.BeginInit();
            haba.UriSource = new Uri("haba.ico", UriKind.Relative);
            haba.EndInit();
            MF.BeginInit();
            MF.UriSource = new Uri("Message Filled_40px.png", UriKind.Relative);
            MF.EndInit();
            try
            {
                Program.vk.Authorize(new ApiAuthParams
                {
                    ApplicationId = Program.appIP,
                    Login = Program.phone,
                    Password = Program.pass,
                    Settings = Settings.Messages,
                    CaptchaSid = Program.cSid,
                    CaptchaKey = cK
                });
                connected = true;
            }
            catch (CaptchaNeededException cEx)
            {
                Uri captchaUrl = cEx.Img;
                Capcha c = new Capcha(captchaUrl);
                c.Show();
                SaveMesSet();
                Close();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Mes.mf", FileMode.OpenOrCreate))
            {
                Program.settings = (MessageSer)formatter.Deserialize(fs);
            }
            haba.BeginInit();
            haba.UriSource = new Uri("haba.ico", UriKind.Relative);
            haba.EndInit();
            MF.BeginInit();
            MF.UriSource = new Uri("Message Filled_40px.png", UriKind.Relative);
            MF.EndInit();
            try
            {
                ulong appID = 6195579;
                string email = "";
                string pass = "";
                Settings scope = Settings.Messages;

                if (Program.settings.ln != "" && Program.settings.ps != "")
                {
                    email = Program.settings.ln;
                    pass = Program.settings.ps;
                    Program.chatID = 448866327;
                }
                else
                {
                    email = "+79098777117";
                    pass = "pk9uli63gsmz103";
                }
                Program.name = Program.settings.nns;
                Program.appIP = appID;
                Program.phone = email;
                Program.pass = pass;
                try
                {
                    Program.vk.Authorize(new ApiAuthParams
                    {
                        ApplicationId = appID,
                        Login = email,
                        Password = pass,
                        Settings = scope
                    });
                    connected = true;
                }
                catch (CaptchaNeededException cEx)
                {
                    Program.cSid = cEx.Sid;
                    Uri captchaUrl = cEx.Img;
                    Capcha c = new Capcha(captchaUrl);
                    c.Show();
                    SaveMesSet();
                    Close();
                }
                if (Program.chatID != 448866327)
                {
                    NameLabel.Content = "Leroy Help";
                    Program.vk.Messages.DeleteDialog(22409448, false);
                }
                else
                {
                    Program.name = "Leroy Help";
                    NameLabel.Content = "Коллега";
                }
            }
            catch (Exception ex)
            {
                connected = false;
                Program.settings.ln = "";
                Program.settings.ps = "";
                Program.settings.nns = "";
                MessageBox.Show(ex.ToString(), "Ошибка." + " Неверный логин или пароль");
                SaveMesSet();
                Close();
            }

            myThread = new Thread(new ThreadStart(Count));
            myThread.Start(); // запускаем поток
        }

        private void LogOut_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Program.settings.ln = "";
            Program.settings.ps = "";
            Program.settings.nns = "";
            SaveMesSet();
            Close();            
        }

        private void Scroll_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            goo = false;
        }

        bool fi = true;

        private void Window_Activated(object sender, EventArgs e)
        {
            fi = true;
            Title = "Онлайн-чат помощь";
            Icon = haba;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            fi = false;
        }

        private void txtSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtSend.Text != "" && txtSend.Text != " " && txtSend.Text != "  "
                && txtSend.Text != "   " && txtSend.Text != "    " && txtSend.Text != "     ")
                {
                    var send = Program.vk.Messages.Send(new MessagesSendParams
                    {
                        UserId = Program.chatID,
                        Message = Program.name + "♦" + txtSend.Text
                    });
                }
                txtSend.Text = "";
                goo = true;
                Dispatcher.Invoke(() => Scroll.ScrollToBottom());
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (txtSend.Text != "" && txtSend.Text != " " && txtSend.Text != "  "
                && txtSend.Text != "   " && txtSend.Text != "    " && txtSend.Text != "     ")
            {
                var send = Program.vk.Messages.Send(new MessagesSendParams
                {
                    UserId = Program.chatID,
                    Message = Program.name + "♦" + txtSend.Text
                });
            }
            txtSend.Text = "";
            goo = true;
            Dispatcher.Invoke(() => Scroll.ScrollToBottom());
        }

        void SaveMesSet()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Mes.mf", FileMode.Create))
            {
                formatter.Serialize(fs, Program.settings);
            }
        }
    }
}
