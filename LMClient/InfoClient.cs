using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Drawing.Printing;

namespace Client
{
    public partial class InfoClient : Form
    {
        public bool REM;
        public int AS;
        public string dir;

        public Add_Client_Form ACF = new Add_Client_Form();

        public InfoClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
            Size = MinimumSize;
            panel3.Visible = true;
            panel3.Size = MinimumSize;
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            FormBorderStyle = FormBorderStyle.None;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            label6.Visible = false;
            label5.Visible = false;
            buttonOK.Visible = true;
            dateTimePicker1.Visible = true;
            label9.Visible = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
            File.WriteAllText(dir + @"\" + AS + @"\SL.dbm", "В процессе");
            File.WriteAllText(dir + @"\" + AS + @"\DEL.dbm", dateTimePicker1.Value.ToString("dd MMMMMMMMMMM."));
            button1.Visible = true;
            button2.Visible = true;
            label6.Visible = true;
            label5.Visible = true;
            buttonOK.Visible = false;
            dateTimePicker1.Visible = false;
            label9.Visible = false;
            if (Owner.Name == "Find")
            {
                ((Form1)((Find)Owner).Owner).DrawExel();
                ((Find)Owner).DrawExel();
            }
            if (Owner.Name == "StartInfo")
            {
                ((StartInfo)Owner).DrawExel();
                ((Form1)((StartInfo)Owner).Owner).DrawExel();
            }
            if (Owner.Name == "Form1")
                ((Form1)Owner).DrawExel();
            Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            button1.Visible = true;
            button2.Visible = true;
            label6.Visible = true;
            label5.Visible = true;
            buttonOK.Visible = false;
            dateTimePicker1.Visible = false;
            label9.Visible = false;
            Hide();
        }

        private void InfoClient_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();

            dateTimePicker1.MinDate = DateTime.Today.AddDays(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            if (Owner.Name == "Find")
            {
                ((Form1)((Find)Owner).Owner).DeleteRemove(AS);
                ((Find)Owner).DrawExel();
            }
            if (Owner.Name == "StartInfo")
            {
                ((Form1)((StartInfo)Owner).Owner).DeleteRemove(AS);
                ((StartInfo)Owner).DrawExel();
            }

            if (Owner.Name == "Form1")
            {
                ((Form1)Owner).DeleteRemove(AS);
            }
            Size = MinimumSize;
            FormBorderStyle = FormBorderStyle.Sizable;
            panel1.Visible = false;
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            StreamWriter ardsr = new StreamWriter(dir + @"\" + AS + @"\SL.dbm");
            ardsr.WriteLine("Клиент оповещен");
            ardsr.Close();
            if (Owner.Name == "Find")
            {
                ((Form1)((Find)Owner).Owner).DrawExel();
                ((Find)Owner).DrawExel();
            }
            if (Owner.Name == "StartInfo")
            {
                ((StartInfo)Owner).DrawExel();
                ((Form1)((StartInfo)Owner).Owner).DrawExel();
            }
            if (Owner.Name == "Form1")
                ((Form1)Owner).DrawExel();
            Size = MinimumSize;
            FormBorderStyle = FormBorderStyle.Sizable;
            panel1.Visible = false;
            Close();
        }

        private void InfoClient_Resize(object sender, EventArgs e)
        {
            groupBox1.Size = new Size(Width - 40, groupBox1.Size.Height);
        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            
        }

        private void InfoClient_Activated(object sender, EventArgs e)
        {
            if (File.Exists(dir + @"/" + AS + @"/ZL.dbm"))
            {
                StreamReader asws = new StreamReader(dir + @"/" + AS + @"/ZL.dbm");
                label14.Text = asws.ReadToEnd();
                asws.Close();
            }
            else
                label14.Text = "";

            if (File.Exists(dir + @"/" + AS + @"/Num.dbm"))
            {
                StreamReader asws = new StreamReader(dir + @"/" + AS + @"/Num.dbm");
                label100.Text = asws.ReadToEnd();
                asws.Close();
            }
            else
                label100.Text = "";

            if (File.Exists(dir + @"/" + AS + @"/AVL.dbm"))
            {
                panel2.Size = new Size(panel2.Width, 5);
                StreamReader asws = new StreamReader(dir + @"/" + AS + @"/AVL.dbm");
                try
                {
                    label15.Text = "AVS: " + DateTime.FromOADate(int.Parse(asws.ReadToEnd())).ToString("dd MMMMMMMMMMM.");
                    asws.Close();
                }
                catch
                {
                    asws.Close();
                    panel2.Size = new Size(panel2.Width, 0);
                    File.Delete(dir + @"\" + AS + @"\AVL.dbm");
                }
                asws.Close();
            }
            else
            {
                panel2.Size = new Size(panel2.Width, 0);
                label15.Text = "";
            }
        }

        private void panel2_MouseEnter(object sender, EventArgs e)
        {
            op.Enabled = true;
            spro.Enabled = false;
        }

        private void panel2_MouseLeave(object sender, EventArgs e)
        {
            spro.Enabled = true;
        }

        private void spro_Tick(object sender, EventArgs e)
        {
            cls.Enabled = true;
            spro.Enabled = false;
        }

        private void op_Tick(object sender, EventArgs e)
        {
            if (panel2.Height < 25)
                panel2.Size = new Size(panel2.Width, (int)(panel2.Height*1.5));
            
            if (panel2.Height > 25)
                panel2.Size = new Size(panel2.Width, 25);

            if (panel2.Height == 25)
                op.Enabled = false;
        }

        private void cls_Tick(object sender, EventArgs e)
        {
            if (panel2.Height > 5)
                panel2.Size = new Size(panel2.Width, (int)(panel2.Height/1.5));

            if (panel2.Height < 5)
                panel2.Size = new Size(panel2.Width, 5);

            if (panel2.Height == 5)
                cls.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
            Size = MinimumSize;
            panel1.Visible = true;
            panel1.Location = new Point(0, 0);
            panel1.Size = MinimumSize;
            panel3.Visible = false;
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            FormBorderStyle = FormBorderStyle.None;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            panel4.Location = new Point(0, 0);
            panel4.Size = MinimumSize;
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            panel3.Visible = false;
            if (File.Exists(Application.StartupPath + @"\shablon.dlb"))
            {
                StreamReader ardsr = new StreamReader(Application.StartupPath + @"\shablon.dlb");
                richTextBox1.Text = ardsr.ReadLine();
                ardsr.Close();
            }

            if (File.Exists(Application.StartupPath + @"\today.dlb"))
            {
                StreamReader ardsr = new StreamReader(Application.StartupPath + @"\today.dlb");
                if(int.Parse(ardsr.ReadLine()) <= 0)
                {
                    ardsr.Close();
                    button9.Enabled = false;
                }
                else
                    button9.Enabled = true;
                ardsr.Close();
            }

            if (label100.Text == "")
                richTextBox1.ForeColor = Color.Red;
            else
                richTextBox1.Text = richTextBox1.Text.Replace("<num>", label100.Text);
            richTextBox1.Text = richTextBox1.Text.Replace("<time>", DateTime.Now.ToString("mm:ss"));
            richTextBox1.Text = richTextBox1.Text.Replace("<date>", DateTime.Now.ToString("dd MMMMMMMMMMM."));
            richTextBox1.Text = richTextBox1.Text.Replace("<dateZ>", label7.Text);
            richTextBox1.Text = richTextBox1.Text.Replace("<name>", label1.Text);
            richTextBox1.Text = richTextBox1.Text.Replace("<nomber>", label2.Text);
            richTextBox1.Text = richTextBox1.Text.Replace("<obj>", label3.Text);
            richTextBox1.Text = richTextBox1.Text.Replace("<qua>", label12.Text);

            Size = MinimumSize;
            StreamReader rdsr = new StreamReader(dir + @"\" + AS + @"\NoL.dbm");
            textBox1.Text = rdsr.ReadLine();
            rdsr.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + @"\today.dlb"))
            {
                StreamReader ardsra = new StreamReader(Application.StartupPath + @"\today.dlb");
                int a = int.Parse(ardsra.ReadLine());
                ardsra.Close();
                StreamWriter ardssr = new StreamWriter(Application.StartupPath + @"\today.dlb");
                ardssr.WriteLine(a-1);
                ardssr.Close();
            }

            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ReadOnly = true;
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            if (File.Exists(Application.StartupPath + @"\log.dlb"))
            {
                StreamReader ardssr = new StreamReader(Application.StartupPath + @"\log.dlb");
                Program.login = ardssr.ReadLine();
                ardssr.Close();
            }

            if (File.Exists(Application.StartupPath + @"\pass.dlb"))
            {
                StreamReader ardssr = new StreamReader(Application.StartupPath + @"\pass.dlb");
                Program.pass = ardssr.ReadLine();
                ardssr.Close();
            }

            try
            {
                var XML = "XML=<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                                "<SMS>\n" +
                                "<operations>\n" +
                                "<operation>SEND</operation>\n" +
                                "</operations>\n" +
                                "<authentification>\n" +
                                "<username>" + Program.login + "</username>\n" +
                                "<password>" + Program.pass + "</password>\n" +
                                "</authentification>\n" +
                                "<message>\n" +
                                "<sender>LEROYMERLIN</sender>\n" +
                                "<text>" + richTextBox1.Text + "</text>\n" +
                                "</message>\n" +
                                "<numbers>\n" +
                                "<number messageID=\"msg15\">" + textBox1.Text + "</number>\n" +
                                "</numbers>\n" +
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
                }
            }
            catch (Exception s)
            {
                MessageBox.Show(s.Message);
            }

            panel4.Visible = false;
            panel1.Visible = true;
            panel1.Location = new Point(0, 0);
            panel1.Size = MinimumSize;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Sizable;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ReadOnly = true;
            richTextBox1.ForeColor = Color.Black;
            panel4.Visible = false;
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            Close();
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox1.BorderStyle = BorderStyle.Fixed3D;
            richTextBox1.ReadOnly = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel2.Size = new Size(MinimumSize.Width, panel2.Height);
            if (Owner.Name == "Find")
            {
                ((Form1)((Find)Owner).Owner).DeleteRemove(AS);
                ((Find)Owner).DrawExel();
            }
            if (Owner.Name == "StartInfo")
            {
                ((Form1)((StartInfo)Owner).Owner).DeleteRemove(AS);
                ((StartInfo)Owner).DrawExel();
            }

            if (Owner.Name == "Form1")
            {
                ((Form1)Owner).DeleteRemove(AS);
            }
            Size = MinimumSize;
            FormBorderStyle = FormBorderStyle.Sizable;
            panel1.Visible = false;
            Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            PrintDocument myPrintDocument1 = new PrintDocument();
            PrintDialog myPrinDialog1 = new PrintDialog();
            myPrintDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            myPrinDialog1.Document = myPrintDocument1;
            if (myPrinDialog1.ShowDialog() == DialogResult.OK)
            {
                myPrintDocument1.Print();
            }
        }

        private Image DrawWatermark(Image originalImage)
        {
            Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
            using (Graphics gr = Graphics.FromImage(bitmap))
            {
                gr.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

                gr.DrawString(label100.Text, new Font("Arial", 84, FontStyle.Bold), Brushes.Black, 1188, 1225);
                gr.DrawString(label4.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 67, 1889);
                gr.DrawString(label3.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 507, 1889);
                gr.DrawString(label12.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 1588, 1889);
                gr.DrawString(label6.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 1800, 1889);
                gr.DrawString(DateTime.Now.ToString("dd MMMMMMMMMMM"), new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 2030, 1465);
                gr.DrawString(label11.Text, new Font("Arial", 42, FontStyle.Bold), Brushes.Black, 1980, 1527);

                return bitmap;
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(DrawWatermark(Image.FromFile("BlanckSleep.png")), 0, 0, 800, 1120);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ACF.Owner = this;
            ACF.dateTimePicker1.Value = DateTime.Today.AddDays(3);
            ACF.textBox2.Text = label4.Text;
            ACF.textBox3.Text = label3.Text;
            ACF.textBox4.Text = label2.Text;
            ACF.textBox5.Text = label1.Text;
            ACF.textBox1.Text = label11.Text;
            ACF.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ACF.create)
            {
                Open();
            }
        }

        public void Open()
        {

            StreamWriter ardsr = new StreamWriter(dir + @"\" + AS + @"\AL.dbm");
            ardsr.WriteLine(ACF.textBox2.Text);
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\TL.dbm");
            ardsr.WriteLine(ACF.textBox3.Text);
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\NoL.dbm");
            ardsr.WriteLine(ACF.textBox4.Text);
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\NaL.dbm");
            ardsr.WriteLine(ACF.textBox5.Text);
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\NSL.dbm");
            ardsr.WriteLine(ACF.textBox1.Text);
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\KL.dbm");
            ardsr.WriteLine(ACF.textBox6.Text);
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\DEL.dbm");
            ardsr.WriteLine(ACF.dateTimePicker1.Value.ToString("dd MMMMMMMMMMM."));
            ardsr.Close();

            ardsr = new StreamWriter(dir + @"\" + AS + @"\SL.dbm");
            ardsr.WriteLine("В процессе");
            ardsr.Close();


            ACF.Hide();
            ACF.create = false;
            if (Owner.Name == "Find")
            {
                ((Form1)((Find)Owner).Owner).DrawExel();
                ((Find)Owner).DrawExel();
            }
            if (Owner.Name == "StartInfo")
            {
                ((StartInfo)Owner).DrawExel();
                ((Form1)((StartInfo)Owner).Owner).DrawExel();
            }
            if (Owner.Name == "Form1")
                ((Form1)Owner).DrawExel();
            Hide();
        }
    }
}
