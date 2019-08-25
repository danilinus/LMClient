using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Client
{
	public partial class Add_Client_Form : Form
	{
		public bool create, now;
		private Point mouseOffset, locOffset;
		private bool isMouseDown = false;

		public List<string> A = new List<string>();
		public List<string> B = new List<string>();
		public List<string> C = new List<string>();
		public List<string> D = new List<string>();
		public List<string> E = new List<string>();

		public string zll, N;

		public Add_Client_Form()
		{
			InitializeComponent();
			now = true;

			dateTimePicker1.MinDate = DateTime.Today.AddDays(1);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Hide();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ActFirst(false);
			ActDouble(true);
			Random rnd = new Random();
			N = "№" + rnd.Next(100, 9999);
		}

		void ActDouble(bool b)
		{
			SMSSend.Visible = b;
			PrintBlank.Visible = b;
			LabelNon.Visible = b;
		}

		void ActFirst(bool b)
		{
			textBox1.Visible = b;
			textBox2.Visible = b;
			textBox3.Visible = b;
			textBox4.Visible = b;
			textBox5.Visible = b;
			textBox6.Visible = b;
			textBox7.Visible = b;
			label1.Visible = b;
			label2.Visible = b;
			dateTimePicker1.Visible = b;
			button1.Visible = b;
			button2.Visible = b;
		}

		private void Add_Client_Form_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isMouseDown = false;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (textBox2.Text == "Артикул")
				textBox2.ForeColor = Color.Gray;
			else
				textBox2.ForeColor = Color.Black;

			if (textBox1.Text == "Имя продавца")
				textBox1.ForeColor = Color.Gray;
			else
				textBox1.ForeColor = Color.Black;

			if (textBox3.Text == "Наименование товара")
				textBox3.ForeColor = Color.Gray;
			else
				textBox3.ForeColor = Color.Black;

			if (textBox4.Text == "Номер клиента")
				textBox4.ForeColor = Color.Gray;
			else
				textBox4.ForeColor = Color.Black;

			if (textBox5.Text == "Имя клиента")
				textBox5.ForeColor = Color.Gray;
			else
				textBox5.ForeColor = Color.Black;

			if (textBox6.Text == "Количество")
				textBox6.ForeColor = Color.Gray;
			else
				textBox6.ForeColor = Color.Black;

			if (textBox7.Text == "Поставщик")
				textBox7.ForeColor = Color.Gray;
			else
				textBox7.ForeColor = Color.Black;
		}

		private void textBox2_Click(object sender, EventArgs e)
		{
			if (textBox2.Text == "Артикул")
				textBox2.Text = "";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void textBox3_Click(object sender, EventArgs e)
		{
			if (textBox3.Text == "Наименование товара")
				textBox3.Text = "";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void textBox4_Click(object sender, EventArgs e)
		{
			if (textBox4.Text == "Номер клиента")
				textBox4.Text = "";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void textBox5_Click(object sender, EventArgs e)
		{
			if (textBox5.Text == "Имя клиента")
				textBox5.Text = "";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void Add_Client_Form_Load(object sender, EventArgs e)
		{
			ActFirst(true);
			ActDouble(false);
			SendSMS.Visible = false;
		}

		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), MousePosition.Y - (mouseOffset.Y - locOffset.Y));

		}

		private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
				isMouseDown = false;
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(Location.X, Location.Y);
				isMouseDown = true;
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void textBox1_Click(object sender, EventArgs e)
		{
			if (textBox1.Text == "Имя продавца")
				textBox1.Text = "";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void textBox6_Click(object sender, EventArgs e)
		{
			if (textBox6.Text == "Количество")
				textBox6.Text = "";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void textBox7_MouseClick(object sender, MouseEventArgs e)
		{
			if (textBox7.Text == "Поставщик")
				textBox7.Text = "";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";
		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{
			textBox2.BackColor = Color.FromArgb(191, 255, 191);
			textBox3.BackColor = Color.FromArgb(191, 255, 191);
			textBox7.BackColor = Color.FromArgb(191, 255, 191);
			zll = "null";

			for (int i = 0; i < A.Count; i++)
			{
				if (E[i] != "")
				{
					if ((textBox2.Text == A[i] || textBox2.Text == B[i]) && textBox2.Text != "")
					{
						textBox3.Text = C[i];
						textBox7.Text = D[i];
						textBox2.BackColor = Color.FromArgb(255, 191, 191);
						textBox3.BackColor = Color.FromArgb(255, 191, 191);
						textBox7.BackColor = Color.FromArgb(255, 191, 191);
						zll = E[i];
					}
				}
				else
				{
					if ((textBox2.Text == A[i] || textBox2.Text == B[i]) && textBox2.Text != "")
					{
						textBox3.Text = C[i];
						textBox7.Text = D[i];
					}
				}
			}
		}

		private void Add_Client_Form_Activated(object sender, EventArgs e)
		{
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.ReadOnly = true;
			textBox2.BackColor = Color.FromArgb(191, 255, 191);
			textBox3.BackColor = Color.FromArgb(191, 255, 191);
			textBox7.BackColor = Color.FromArgb(191, 255, 191);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.ReadOnly = true;
			SendSMS.Visible = false;
			ActFirst(true);
			Hide();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.ReadOnly = true;
			SendSMS.Visible = false;
			ActFirst(true);
			create = true;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			richTextBox1.BorderStyle = BorderStyle.None;
			richTextBox1.ReadOnly = true;
			richTextBox1.Text.Replace("<num>", N.ToString());
			richTextBox1.Text.Replace("<time>", DateTime.Now.ToString("hh:mm"));
			richTextBox1.Text.Replace("<date>", DateTime.Now.ToString("dd MMMMMMMMMMM"));
			richTextBox1.Text.Replace("<dateE>", dateTimePicker1.Value.ToString("dd MMMMMMMMMMM"));
			richTextBox1.Text.Replace("<name>", textBox5.Text);
			richTextBox1.Text.Replace("<nomber>", textBox4.Text);
			richTextBox1.Text.Replace("<obj>", textBox3.Text);
			richTextBox1.Text.Replace("<qua>", textBox6.Text);
			richTextBox1.Text.Replace("<art>", textBox2.Text);
			richTextBox1.Text.Replace("<prod>", textBox1.Text);

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
								"<number messageID=\"msg16\">" + textBox4.Text + "</number>\n" +
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
				}
			}
			catch (Exception s)
			{
				MessageBox.Show(s.Message);
			}
			SendSMS.Visible = false;
			create = true;
			ActFirst(true);
		}

		private void richTextBox1_DoubleClick(object sender, EventArgs e)
		{
			richTextBox1.BorderStyle = BorderStyle.Fixed3D;
			richTextBox1.ReadOnly = false;
		}

		private void SMSSend_Click(object sender, EventArgs e)
		{
			if (textBox2.Text == "Артикул" || textBox2.Text == "" || textBox2.Text == " ")
				textBox2.BackColor = Color.FromArgb(255, 192, 192);
			else
			   if (textBox3.Text == "Наименование товара" || textBox3.Text == "" || textBox3.Text == " ")
				textBox3.BackColor = Color.FromArgb(255, 192, 192);
			else
			   if (textBox4.Text == "Номер клиента" || textBox4.Text == "" || textBox4.Text == " ")
				textBox4.BackColor = Color.FromArgb(255, 192, 192);
			else
			   if (textBox5.Text == "Имя клиента" || textBox5.Text == "" || textBox5.Text == " ")
				textBox5.BackColor = Color.FromArgb(255, 192, 192);
			else
			   if (textBox1.Text == "Имя продавца" || textBox1.Text == "" || textBox1.Text == " ")
				textBox1.BackColor = Color.FromArgb(255, 192, 192);
			else
			   if (textBox6.Text == "Количество" || textBox6.Text == "" || textBox6.Text == " ")
				textBox6.BackColor = Color.FromArgb(255, 192, 192);



			if (textBox2.Text != "Артикул" && textBox2.Text != "" && textBox2.Text != " " &&
				textBox3.Text != "Наименование товара" && textBox3.Text != "" && textBox3.Text != " " &&
				textBox4.Text != "Номер клиента" && textBox4.Text != "" && textBox4.Text != " " &&
				textBox5.Text != "Имя клиента" && textBox5.Text != "" && textBox5.Text != " " &&
				textBox1.Text != "Имя продавца" && textBox1.Text != "" && textBox1.Text != " " &&
				textBox6.Text != "Количество" && textBox6.Text != "" && textBox6.Text != " ")
			{
				if (textBox7.Text == "Поставщик")
					textBox7.Text = "";
				textBox4.Text = textBox4.Text.Insert(0, "7");


				textBox4.Text = textBox4.Text.Replace("+", "");
				textBox8.Text = textBox4.Text;

				if (File.Exists(Application.StartupPath + @"\shablonsend.dlb"))
				{
					StreamReader read = new StreamReader(Application.StartupPath + @"\shablonsend.dlb");
					richTextBox1.Text = read.ReadToEnd();
					read.Close();
				}
				else
					richTextBox1.Text = "Вашему заказу на товар <obj> присвоен заказ <num>. Ориентировочная дата поступления товара <date>";

				string su = richTextBox1.Text;
				su = su.Replace("<num>", N.ToString());
				su = su.Replace("<time>", DateTime.Now.ToString("hh:mm"));
				su = su.Replace("<date>", DateTime.Now.ToString("dd MMMMMMMMMMM"));
				su = su.Replace("<dateE>", dateTimePicker1.Value.ToString("dd MMMMMMMMMMM"));
				su = su.Replace("<name>", textBox5.Text);
				su = su.Replace("<nomber>", textBox4.Text);
				su = su.Replace("<obj>", textBox3.Text);
				su = su.Replace("<qua>", textBox6.Text);
				su = su.Replace("<art>", textBox2.Text);
				su = su.Replace("<prod>", textBox1.Text);
				richTextBox1.Text = su;
				SendSMS.Visible = true;
				SendSMS.Location = new Point(0, 0);
				SendSMS.Size = new Size(Width, Height);
				ActDouble(false);
			}
		}

		private void PrintBlank_Click(object sender, EventArgs e)
		{
			PrintDocument myPrintDocument1 = new PrintDocument();
			using (PrintDialog myPrinDialog1 = new PrintDialog())
			{
				myPrintDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
				myPrinDialog1.Document = myPrintDocument1;
				if (myPrinDialog1.ShowDialog() == DialogResult.OK)
					myPrintDocument1.Print();
			}
			SendSMS.Visible = false;
			create = true;
			ActFirst(true);
			ActDouble(false);
		}

		private Image DrawWatermark(Image originalImage)
		{
			Bitmap bitmap = new Bitmap(originalImage.Width, originalImage.Height);
			using (Graphics gr = Graphics.FromImage(bitmap))
			{
				gr.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height));

				gr.DrawString(N, new Font("Arial", 84, FontStyle.Bold), Brushes.Black, 1188, 1225);
				gr.DrawString(textBox2.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 67, 1889);
				gr.DrawString(textBox3.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 507, 1889);
				gr.DrawString(textBox6.Text, new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 1588, 1889);
				gr.DrawString(dateTimePicker1.Value.ToString("dd MMMMMMMMMMM"), new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 1800, 1889);
				gr.DrawString(DateTime.Now.ToString("dd MMMMMMMMMMM"), new Font("Arial", 48, FontStyle.Bold), Brushes.Black, 2030, 1465);
				gr.DrawString(textBox1.Text, new Font("Arial", 42, FontStyle.Bold), Brushes.Black, 1980, 1527);

				return bitmap;
			}
		}

		private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
		{
			e.Graphics.DrawImage(DrawWatermark(Image.FromFile("BlanckSleep.png")), 0, 0, 800, 1120);
		}

		private void textBox8_Click(object sender, EventArgs e)
		{
			if (textBox4.Text == "")
				textBox4.Text = "Номер клиента";

			if (textBox3.Text == "")
				textBox3.Text = "Наименование товара";

			if (textBox2.Text == "")
				textBox2.Text = "Артикул";

			if (textBox5.Text == "")
				textBox5.Text = "Имя клиента";

			if (textBox1.Text == "")
				textBox1.Text = "Имя продавца";

			if (textBox6.Text == "")
				textBox6.Text = "Количество";

			if (textBox7.Text == "")
				textBox7.Text = "Поставщик";
		}

		private void Add_Client_Form_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), MousePosition.Y - (mouseOffset.Y - locOffset.Y));

		}

		private void Add_Client_Form_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(Location.X, Location.Y);
				isMouseDown = true;
			}
		}
	}
}