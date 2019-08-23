using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace Client
{
    public partial class SMSForm : Form
    {
        public SMSForm()
        {
            InitializeComponent();
        }

        private void SMSForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\log.dlb"))
            {
                StreamReader ardasr = new StreamReader(Application.StartupPath + @"\log.dlb");
                LogintextBox.Text = ardasr.ReadLine();
                ardasr.Close();
            }

            if (File.Exists(Application.StartupPath + @"\pass.dlb"))
            {
                StreamReader ardasr = new StreamReader(Application.StartupPath + @"\pass.dlb");
                PasstextBox.Text = ardasr.ReadLine();
                ardasr.Close();
            }

            if (File.Exists(Application.StartupPath + @"\shablon.dlb"))
            {
                StreamReader ardasr = new StreamReader(Application.StartupPath + @"\shablon.dlb");
                ShablonTextBox.Text = ardasr.ReadLine();
                ardasr.Close();
            }

            if (File.Exists(Application.StartupPath + @"\shablonsend.dlb"))
            {
                StreamReader ardasr = new StreamReader(Application.StartupPath + @"\shablonsend.dlb");
                Shablon2.Text = ardasr.ReadLine();
                ardasr.Close();
            }

            richTextBox1.Text = "";

            StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\log.dlb");
            ardsr.WriteLine(LogintextBox.Text);
            ardsr.Close();

            ardsr = new StreamWriter(Application.StartupPath + @"\pass.dlb");
            ardsr.WriteLine(PasstextBox.Text);
            ardsr.Close();

            ardsr = new StreamWriter(Application.StartupPath + @"\shablon.dlb");
            ardsr.WriteLine(ShablonTextBox.Text);
            ardsr.Close();

            ardsr = new StreamWriter(Application.StartupPath + @"\shablonsend.dlb");
            ardsr.WriteLine(Shablon2.Text);
            ardsr.Close();

            try
            {
                var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                            "<SMS>\n" +
                            "<operations>\n" +
                            "<operation>GETSTATUS</operation>\n" +
                            "</operations>\n" +
                            "<authentification>\n" +
                            "<username>" + LogintextBox.Text + "</username>\n" +
                            "<password>" + PasstextBox.Text + "</password>\n" +
                            "</authentification>\n" +
                            "<statistics>\n" +
                            "<messageid>msg15</messageid>\n" +
                            "<messageid>msg16</messageid>\n" +
                            "</statistics>\n" +
                            "</SMS>\n";
                HttpWebRequest request = WebRequest.Create("http://api.myatompark.com/members/sms/xml.php") as HttpWebRequest;
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded";
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(XML);
                request.ContentLength = data.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string s = reader.ReadToEnd();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(s);
                    XmlNode root_node = doc.DocumentElement;
                    XmlNodeList nodes = root_node.ChildNodes;
                    foreach (XmlNode n in nodes)
                    {
                        if (n.Name == "message")
                        {
                            richTextBox1.Text += "["+ n.Attributes["id"].Value + "] | [" + n.Attributes["sentdate"].Value + "] | " + n.Attributes["status"].Value.Replace("0", "Идёт отправка") + "\n";
                        }
                    }
                    
                    richTextBox1.Text = richTextBox1.Text.Replace("msg15","Приход товара");
                    richTextBox1.Text = richTextBox1.Text.Replace("msg16", "Внесение в список");
                    richTextBox1.Text = richTextBox1.Text.Replace("SENT", "Отослано");
                    richTextBox1.Text = richTextBox1.Text.Replace("NOT_DELIVERED", "Не доставлено");
                    richTextBox1.Text = richTextBox1.Text.Replace("DELIVERED", "Доставлено");
                    richTextBox1.Text = richTextBox1.Text.Replace("NOT_ALLOWED", "Оператор не обслуживается");
                    richTextBox1.Text = richTextBox1.Text.Replace("INVALID_DESTINATION_ADDRESS", "Неверный адрес для доставки");
                    richTextBox1.Text = richTextBox1.Text.Replace("INVALID_SOURCE_ADDRESS", "Неправильное имя «От кого»");
                    richTextBox1.Text = richTextBox1.Text.Replace("NOT_ENOUGH_CREDITS", "Недостаточно кредитов");
                    richTextBox1.Text = richTextBox1.Text.Replace("0000-00-00 00:00:00", "Идёт Отправка");
                    
                    string[] arra = richTextBox1.Lines;

                    richTextBox1.Text = "";

                    for (int i = arra.Length - 1; i >= 0; i--)
                    {
                        richTextBox1.Text += arra[i] + "\n";
                    }
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }

            if (File.Exists(Application.StartupPath + @"\today.dlb"))
            {
                StreamReader sda = new StreamReader(Application.StartupPath + @"\today.dlb");
                label1.Text = "Ваш лимит сообщений: " + sda.ReadLine() + " СМС";
                sda.Close();
            }
        }

        private void Reboot_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\log.dlb");
            ardsr.WriteLine(LogintextBox.Text);
            ardsr.Close();

            ardsr = new StreamWriter(Application.StartupPath + @"\pass.dlb");
            ardsr.WriteLine(PasstextBox.Text);
            ardsr.Close();

            try
            {
                var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                            "<SMS>\n" +
                            "<operations>\n" +
                            "<operation>GETSTATUS</operation>\n" +
                            "</operations>\n" +
                            "<authentification>\n" +
                            "<username>" + LogintextBox.Text + "</username>\n" +
                            "<password>" + PasstextBox.Text + "</password>\n" +
                            "</authentification>\n" +
                            "<statistics>\n" +
                            "<messageid>msg15</messageid>\n" +
                            "</statistics>\n" +
                            "</SMS>\n";
                HttpWebRequest request = WebRequest.Create("http://api.myatompark.com/members/sms/xml.php") as HttpWebRequest;
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded";
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(XML);
                request.ContentLength = data.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string s = reader.ReadToEnd();

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(s);
                    XmlNode root_node = doc.DocumentElement;
                    XmlNodeList nodes = root_node.ChildNodes;
                    foreach (XmlNode n in nodes)
                    {
                        if (n.Name == "message")
                        {
                            richTextBox1.Text += "[" + n.Attributes["sentdate"].Value + "] | " + n.Attributes["status"].Value.Replace("0", "Идёт отправка") + "\n";
                        }
                    }

                    richTextBox1.Text = richTextBox1.Text.Replace("SENT", "Отослано");
                    richTextBox1.Text = richTextBox1.Text.Replace("NOT_DELIVERED", "Не доставлено");
                    richTextBox1.Text = richTextBox1.Text.Replace("DELIVERED", "Доставлено");
                    richTextBox1.Text = richTextBox1.Text.Replace("NOT_ALLOWED", "Оператор не обслуживается");
                    richTextBox1.Text = richTextBox1.Text.Replace("INVALID_DESTINATION_ADDRESS", "Неверный адрес для доставки");
                    richTextBox1.Text = richTextBox1.Text.Replace("INVALID_SOURCE_ADDRESS", "Неправильное имя «От кого»");
                    richTextBox1.Text = richTextBox1.Text.Replace("NOT_ENOUGH_CREDITS", "Недостаточно кредитов");
                    richTextBox1.Text = richTextBox1.Text.Replace("0000-00-00 00:00:00", "Идёт Отправка");

                    string[] arra = richTextBox1.Lines;

                    richTextBox1.Text = "";

                    for (int i = arra.Length - 1; i >= 0; i--)
                    {
                        richTextBox1.Text += arra[i] + "\n";
                    }
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }

            if (File.Exists(Application.StartupPath + @"\today.dlb"))
            {
                StreamReader sda = new StreamReader(Application.StartupPath + @"\today.dlb");
                label1.Text = "Ваш лимит сообщений: " + sda.ReadLine() + " СМС";
                sda.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\shablon.dlb");
            ardsr.WriteLine(ShablonTextBox.Text);
            ardsr.Close();
            ardsr = new StreamWriter(Application.StartupPath + @"\shablonsend.dlb");
            ardsr.WriteLine(Shablon2.Text);
            ardsr.Close();

            textBox1.Text = "";
            textBox2.Text = "";

            if (File.Exists(Application.StartupPath + @"\today.dlb"))
            {
                StreamReader sda = new StreamReader(Application.StartupPath + @"\today.dlb");
                label1.Text = "Ваш лимит сообщений: " + sda.ReadLine() + " СМС";
                sda.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\passa.dlb"))
            {
                StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\passa.dlb");
                ardsr.WriteLine("");
                ardsr.Close();
            }

            StreamReader sda = new StreamReader(Application.StartupPath + @"\passa.dlb");
            if (File.Exists(Application.StartupPath + @"\passa.dlb") && textBox1.Text == sda.ReadLine())
            {
                sda.Close();
                try
                {
                    StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\yestoday.dlb");
                    ardsr.WriteLine(DateTime.Now.AddDays(1).ToString("dd MMMMMMMMMMM."));
                    ardsr.Close();
                    StreamWriter ardsra = new StreamWriter(Application.StartupPath + @"\yestoday2.dlb");
                    ardsra.WriteLine(int.Parse(textBox2.Text));
                    ardsra.Close();
                    ardsra = new StreamWriter(Application.StartupPath + @"\reboot.dlb");
                    ardsra.WriteLine(int.Parse(textBox2.Text));
                    ardsra.Close();
                    MessageBox.Show("Лимит изменен", "Ограничение");
                }
                catch (Exception s)
                {
                    MessageBox.Show(s.Message);
                }
            }
            else
            {
                MessageBox.Show("Пароль неверный", "Ограничение");
            }
            sda.Close();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\passa.dlb"))
            {
                StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\passa.dlb");
                ardsr.WriteLine("");
                ardsr.Close();
            }

            StreamReader sda = new StreamReader(Application.StartupPath + @"\passa.dlb");
            if (File.Exists(Application.StartupPath + @"\passa.dlb") && textBox1.Text == sda.ReadLine())
            {
                sda.Close();
                try
                {
                    var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                                "<SMS>\n" +
                                "<operations>\n" +
                                "<operation>BALANCE</operation>\n" +
                                "</operations>\n" +
                                "<authentification>\n" +
                                "<username>" + LogintextBox.Text + "</username>\n" +
                                "<password>" + PasstextBox.Text + "</password>\n" +
                                "</authentification>\n" +
                                "</SMS>\n";
                    HttpWebRequest request = WebRequest.Create("http://api.myatompark.com/members/sms/xml.php") as HttpWebRequest;
                    request.Method = "Post";
                    request.ContentType = "application/x-www-form-urlencoded";
                    UTF8Encoding encoding = new UTF8Encoding();
                    byte[] data = encoding.GetBytes(XML);
                    request.ContentLength = data.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(data, 0, data.Length);
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                            throw new Exception(String.Format(
                            "Server error (HTTP {0}: {1}).",
                            response.StatusCode,
                            response.StatusDescription));
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string str = reader.ReadToEnd();
                        string[] arr = Regex.Matches(str, @"\>(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
                        string[] arr2 = Regex.Matches(str, @"\>(\w+)\.").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
                        string[] arr3 = Regex.Matches(str, @"\.(\w+)\<").Cast<Match>().Select(m => m.Groups[1].Value).ToArray();
                        if (arr.Length < 1)
                            MessageBox.Show("Ошибка: Неверно введён логин и/или пароль", "Ошибка");
                        if (arr[0] == "0")
                        {
                            MessageBox.Show("Баланс: " + arr2[0] + "." + arr3[0] + arr[2], "Ваш баланс");
                        }
                    }
                }
                catch (Exception s)
                {
                    MessageBox.Show(s.Message);
                }
            }
            else
            {
                MessageBox.Show("Пароль неверный", "Пароль");
            }
            sda.Close();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Application.StartupPath + @"\passa.dlb"))
            {
                StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\passa.dlb");
                ardsr.WriteLine("");
                ardsr.Close();
            }
            StreamReader sda = new StreamReader(Application.StartupPath + @"\passa.dlb");
            if (File.Exists(Application.StartupPath + @"\passa.dlb") && textBox1.Text == sda.ReadLine())
            {
                sda.Close();
                try
                {
                    StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\passa.dlb");
                    ardsr.WriteLine(textBox2.Text);
                    ardsr.Close();
                    MessageBox.Show("Пароль успешно изменен", "Пароль");
                }
                catch (Exception s)
                {
                    MessageBox.Show(s.Message);
                }
            }
            else
            {
                MessageBox.Show("Пароль неверный", "Пароль");
            }
            sda.Close();
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
