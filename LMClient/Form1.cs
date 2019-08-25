using LMClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Client
{
	public partial class Form1 : Form
	{
		//public bool hook = true;
		int width, height, width_new, height_new, kola;
		public int m, n, b = 0, ICint = 0, impTimer, impTimerMax, timer1Int, timer1Int2;
		private readonly Add_Client_Form f2 = new Add_Client_Form();
		private readonly StartInfo SI = new StartInfo();
		private readonly InfoClient IC = new InfoClient();
		private StreamReader FS, FE;

		Excel.Application excelapp;
		Excel.Workbooks excelappworkbooks;
		Excel.Worksheet excelworksheet;

		private Point mouseOffset, locOffset;
		private bool isMouseDown = false;

		public List<Panel> panels = new List<Panel>();

		public List<int> DatelNomer = new List<int>();
		public List<Label> DataLables = new List<Label>();
		public List<Label> ArtikulLables = new List<Label>();
		public List<Label> TovarLables = new List<Label>();
		public List<Label> NomberLables = new List<Label>();
		public List<Label> NameLables = new List<Label>();
		public List<Label> DataEndLables = new List<Label>();
		public List<Label> StatusLables = new List<Label>();
		//public List<Button> RemoveButton = new List<Button>();

		public List<string> A = new List<string>();
		public List<string> B = new List<string>();
		public List<string> C = new List<string>();
		public List<string> D = new List<string>();
		public List<string> E = new List<string>();

		public string dir;

		public bool CL;

		public FindError FError = new FindError();

		public Form1()
		{
			InitializeComponent();
			width = Width;
			height = Height;
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			if (!File.Exists(Application.StartupPath + @"\BBD.dlb"))
				if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
				{
					var z = File.Create(Application.StartupPath + @"\BBD.dlb");
					z.Close();
					File.WriteAllText(Application.StartupPath + @"\BBD.dlb", folderBrowserDialog1.SelectedPath);
					Application.Restart();
				}
				else
				{
					Close();
				}

			try
			{
				var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
				string[] k = key.GetValueNames();
				bool be = false;
				for (int i = 0; i < k.Length; i++)
					if (k[i] == "LMClient")
					{
						be = true;
						Автозагрузкавклвыкл.Text = "Автозагрузка выкл";
						break;
					}

				if (!be)
					Автозагрузкавклвыкл.Text = "Автозагрузка вкл";
			}
			catch
			{

			}

			if (!File.Exists(Application.StartupPath + @"\S1.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S1.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S1.dlb", "74");
			}

			if (!File.Exists(Application.StartupPath + @"\S2.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S2.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S2.dlb", "166");
			}

			if (!File.Exists(Application.StartupPath + @"\S3.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S3.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S3.dlb", "312");
			}

			if (!File.Exists(Application.StartupPath + @"\S4.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S4.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S4.dlb", "420");
			}

			if (!File.Exists(Application.StartupPath + @"\S5.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S5.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S5.dlb", "582");
			}
			if (!File.Exists(Application.StartupPath + @"\S6.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S6.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S6.dlb", "720");
			}
			if (!File.Exists(Application.StartupPath + @"\S7.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\S7.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\S7.dlb", "837");
			}

			if (!File.Exists(Application.StartupPath + @"\A.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\A.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\A.dlb", "");
			}
			if (!File.Exists(Application.StartupPath + @"\B.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\B.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\B.dlb", "");
			}
			if (!File.Exists(Application.StartupPath + @"\C.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\C.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\C.dlb", "");
			}
			if (!File.Exists(Application.StartupPath + @"\D.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\D.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\D.dlb", "");
			}
			if (!File.Exists(Application.StartupPath + @"\E.dlb"))
			{
				var z = File.Create(Application.StartupPath + @"\E.dlb");
				z.Close();
				File.WriteAllText(Application.StartupPath + @"\E.dlb", "");
			}

			using (var sr = new StreamReader(Application.StartupPath + @"\A.dlb"))
			{
				sr.ReadLine();
				while (sr.Peek() >= 0)
					A.Add(sr.ReadLine());
			}

			using (var sr = new StreamReader(Application.StartupPath + @"\B.dlb"))
			{
				sr.ReadLine();
				while (sr.Peek() >= 0)
					B.Add(sr.ReadLine());
			}

			using (var sr = new StreamReader(Application.StartupPath + @"\C.dlb"))
			{
				sr.ReadLine();
				while (sr.Peek() >= 0)
					C.Add(sr.ReadLine());
			}

			using (var sr = new StreamReader(Application.StartupPath + @"\D.dlb"))
			{
				sr.ReadLine();
				while (sr.Peek() >= 0)
					D.Add(sr.ReadLine());
			}

			using (var sr = new StreamReader(Application.StartupPath + @"\E.dlb"))
			{
				sr.ReadLine();
				while (sr.Peek() >= 0)
					E.Add(sr.ReadLine());
			}

			FS = new StreamReader(Application.StartupPath + @"\S1.dlb");
			panelS1.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			FS.Close();
			FS = new StreamReader(Application.StartupPath + @"\S2.dlb");
			panelS2.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			FS.Close();
			FS = new StreamReader(Application.StartupPath + @"\S3.dlb");
			panelS3.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			FS.Close();
			FS = new StreamReader(Application.StartupPath + @"\S4.dlb");
			panelS4.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			FS.Close();
			FS = new StreamReader(Application.StartupPath + @"\S5.dlb");
			panelS5.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			FS.Close();
			FS = new StreamReader(Application.StartupPath + @"\S6.dlb");
			panelS6.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			FS.Close();
			FS = new StreamReader(Application.StartupPath + @"\S7.dlb");
			panelS7.Location = new Point(int.Parse(FS.ReadToEnd()), panelS1.Location.Y);
			dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();

			timer1.Enabled = true;

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);


			m = new DirectoryInfo(dir).GetDirectories().Length;

			for (int i = 0; i < m; i++)
			{
				FS = new StreamReader(dir + @"\" + i + @"\DEL.dbm");
				FE = new StreamReader(dir + @"\" + i + @"\SL.dbm");
				if (FS.ReadLine() == DateTime.Today.ToString("dd MMMMMMMMMMM.") && FE.ReadLine() != "Клиент оповещен")
				{
					FE.Close();
					File.WriteAllText(dir + @"\" + i + @"\SL.dbm", "Не оповещен");
					SI.ASin.Add(i);
					SI.Owner = this;
					b++;
				}
				FE.Close();
				FS.Close();

			}

			SI.A = A;
			SI.B = B;
			SI.E = E;

			if (!File.Exists(Application.StartupPath + @"\today.dlb"))
			{
				StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\today.dlb");
				ardsr.WriteLine("15");
				ardsr.Close();
			}

			if (!File.Exists(Application.StartupPath + @"\reboot.dlb"))
			{
				StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\reboot.dlb");
				ardsr.WriteLine("15");
				ardsr.Close();
			}

			if (!File.Exists(Application.StartupPath + @"\today2.dlb"))
			{
				StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\today2.dlb");
				ardsr.WriteLine(DateTime.Now.ToString("dd MMMMMMMMMMM."));
				ardsr.Close();
			}

			if (File.Exists(Application.StartupPath + @"\yestoday.dlb"))
			{
				StreamReader sda = new StreamReader(Application.StartupPath + @"\yestoday.dlb");
				if (sda.ReadLine() == DateTime.Now.ToString("dd MMMMMMMMMMM."))
				{
					sda.Close();
					StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\today.dlb");
					sda = new StreamReader(Application.StartupPath + @"\yestoday2.dlb");
					ardsr.WriteLine(sda.ReadLine());
					sda.Close();
					ardsr.Close();
					File.Delete(Application.StartupPath + @"\yestoday.dlb");
					File.Delete(Application.StartupPath + @"\yestoday2.dlb");
				}
			}
			else if (File.Exists(Application.StartupPath + @"\today2.dlb") && File.Exists(Application.StartupPath + @"\reboot.dlb"))
			{
				StreamReader sda = new StreamReader(Application.StartupPath + @"\today2.dlb");
				if (sda.ReadLine() != DateTime.Now.ToString("dd MMMMMMMMMMM."))
				{
					sda.Close();
					StreamWriter ardsr = new StreamWriter(Application.StartupPath + @"\today2.dlb");
					ardsr.WriteLine(DateTime.Now.ToString("dd MMMMMMMMMMM."));
					ardsr.Close();
					sda = new StreamReader(Application.StartupPath + @"\reboot.dlb");
					ardsr = new StreamWriter(Application.StartupPath + @"\today.dlb");
					ardsr.WriteLine(sda.ReadLine());
					ardsr.Close();
					sda.Close();
				}
			}

			if (b > 0) SI.Show();

			m = new DirectoryInfo(dir).GetDirectories().Length;
			for (int i = 0; i < m; i++) DatelNomer.Add(i);

			DrawExeaaal();
			DrawExeNes();
			DrawExelNes();
			DrawExeaaal();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			f2.Owner = this;
			f2.SendSMS.Visible = false;
			f2.A = A;
			f2.B = B;
			f2.C = C;
			f2.D = D;
			f2.E = E;
			f2.dateTimePicker1.Value = DateTime.Today.AddDays(3);
			f2.textBox2.Text = "Артикул";
			f2.textBox3.Text = "Наименование товара";
			f2.textBox4.Text = "Номер клиента";
			f2.textBox5.Text = "Имя клиента";
			f2.textBox6.Text = "Количество";
			f2.textBox7.Text = "Поставщик";
			f2.Show();
		}

		public void AddExel(int num)
		{
			m = num * 7;
			for (int i = 0; i < 7; i++)
				panels.Add(new Panel());

			DatelNomer.Add(num);

			if (m > 0)
			{
				panels[m].Location = new Point(3, panels[m - 1].Location.Y + 27);
				panels[m].Size = new Size(panel2.Size.Width, 24);

				panels[m + 1].Location = new Point(panel1.Location.X - 9, panels[m - 1].Location.Y + 27);
				panels[m + 1].Size = new Size(panel1.Size.Width, 24);

				panels[m + 2].Location = new Point(panel3.Location.X - 9, panels[m - 1].Location.Y + 27);
				panels[m + 2].Size = new Size(panel3.Size.Width, 24);

				panels[m + 3].Location = new Point(panel4.Location.X - 9, panels[m - 1].Location.Y + 27);
				panels[m + 3].Size = new Size(panel4.Size.Width, 24);

				panels[m + 4].Location = new Point(panel5.Location.X - 9, panels[m - 1].Location.Y + 27);
				panels[m + 4].Size = new Size(panel5.Size.Width, 24);

				panels[m + 5].Location = new Point(panel6.Location.X - 9, panels[m - 1].Location.Y + 27);
				panels[m + 5].Size = new Size(panel6.Size.Width, 24);

				panels[m + 6].Location = new Point(panel7.Location.X - 9, panels[m - 1].Location.Y + 27);
				panels[m + 6].Size = new Size(panel7.Size.Width, 24);
			}
			else
			{
				panels[m].Location = new Point(3, 3);
				panels[m].Size = new Size(panel2.Size.Width, 24);

				panels[m + 1].Location = new Point(panel1.Location.X - 9, 3);
				panels[m + 1].Size = new Size(panel1.Size.Width, 24);

				panels[m + 2].Location = new Point(panel3.Location.X - 9, 3);
				panels[m + 2].Size = new Size(panel3.Size.Width, 24);

				panels[m + 3].Location = new Point(panel4.Location.X - 9, 3);
				panels[m + 3].Size = new Size(panel4.Size.Width, 24);

				panels[m + 4].Location = new Point(panel5.Location.X - 9, 3);
				panels[m + 4].Size = new Size(panel5.Size.Width, 24);

				panels[m + 5].Location = new Point(panel6.Location.X - 9, 3);
				panels[m + 5].Size = new Size(panel6.Size.Width, 24);

				panels[m + 6].Location = new Point(panel7.Location.X - 9, 3);
				panels[m + 6].Size = new Size(panel7.Size.Width, 24);
			}

			for (int i = 0; i < 7; i++)
			{
				panels[m + i].BackColor = Color.FromArgb(191, 255, 191);
				if (File.Exists(dir + @"\" + i + @"\AVL.dbm"))
					panels[m + i].BackColor = Color.FromArgb(255, 191, 191);
				panels[m + i].BorderStyle = BorderStyle.FixedSingle;
				panels[m + i].ContextMenuStrip = contextMenuStrip1;
				panel8.Controls.Add(panels[m + i]);
			}

			DataLables.Add(new Label());
			StreamReader ardsr = new StreamReader(dir + @"\" + num + @"\DL.dbm");
			DataLables[num].Text = ardsr.ReadToEnd();
			DataLables[num].Location = new Point(3, 3);
			DataLables[num].Dock = DockStyle.Fill;
			DataLables[num].Click += new EventHandler(ClickO);
			panels[m].Controls.Add(DataLables[num]);
			ardsr.Close();


			ArtikulLables.Add(new Label());
			ardsr = new StreamReader(dir + @"\" + num + @"\AL.dbm");
			ArtikulLables[num].Text = ardsr.ReadToEnd();
			ArtikulLables[num].Location = new Point(3, 3);
			ArtikulLables[num].Dock = DockStyle.Fill;
			ArtikulLables[num].Click += new EventHandler(ClickO);
			panels[m + 1].Controls.Add(ArtikulLables[num]);
			ardsr.Close();


			TovarLables.Add(new Label());
			ardsr = new StreamReader(dir + @"\" + num + @"\TL.dbm");
			TovarLables[num].Text = ardsr.ReadToEnd();
			TovarLables[num].Location = new Point(3, 3);
			TovarLables[num].Dock = DockStyle.Fill;
			TovarLables[num].Click += new EventHandler(ClickO);
			panels[m + 2].Controls.Add(TovarLables[num]);
			ardsr.Close();


			NomberLables.Add(new Label());
			ardsr = new StreamReader(dir + @"\" + num + @"\NoL.dbm");
			NomberLables[num].Text = ardsr.ReadToEnd();
			NomberLables[num].Location = new Point(3, 3);
			NomberLables[num].Dock = DockStyle.Fill;
			NomberLables[num].Click += new EventHandler(ClickO);
			panels[m + 3].Controls.Add(NomberLables[num]);
			ardsr.Close();


			NameLables.Add(new Label());
			ardsr = new StreamReader(dir + @"\" + num + @"\NaL.dbm");
			NameLables[num].Text = ardsr.ReadToEnd();
			NameLables[num].Location = new Point(3, 3);
			NameLables[num].Dock = DockStyle.Fill;
			NameLables[num].Click += new EventHandler(ClickO);
			panels[m + 4].Controls.Add(NameLables[num]);
			ardsr.Close();


			DataEndLables.Add(new Label());
			FS = new StreamReader(dir + @"\" + num + @"\DEL.dbm");
			DataEndLables[num].Text = FS.ReadLine();
			DataEndLables[num].Location = new Point(3, 3);
			DataEndLables[num].Dock = DockStyle.Fill;
			DataEndLables[num].Click += new EventHandler(ClickO);
			panels[m + 5].Controls.Add(DataEndLables[num]);
			FS.Close();


			StatusLables.Add(new Label());
			FS = new StreamReader(dir + @"\" + num + @"\SL.dbm");
			StatusLables[num].Text = FS.ReadLine();
			StatusLables[num].Location = new Point(3, 3);
			StatusLables[num].Dock = DockStyle.Fill;
			StatusLables[num].Click += new EventHandler(ClickO);
			panels[m + 6].Controls.Add(StatusLables[num]);
			FS.Close();

			for (int j = 0; j < A.Count; j++)
			{
				if (ArtikulLables[num].Text.Contains(A[j]) || ArtikulLables[num].Text.Contains(B[j]))
					if (E[j] != "")
					{
						panels[m].BackColor = Color.FromArgb(255, 191, 191);
						panels[m + 1].BackColor = Color.FromArgb(255, 191, 191);
						panels[m + 2].BackColor = Color.FromArgb(255, 191, 191);
						panels[m + 3].BackColor = Color.FromArgb(255, 191, 191);
						panels[m + 4].BackColor = Color.FromArgb(255, 191, 191);
						panels[m + 5].BackColor = Color.FromArgb(255, 191, 191);
						panels[m + 6].BackColor = Color.FromArgb(255, 191, 191);
					}
					else
					{
						panels[m].BackColor = Color.FromArgb(191, 255, 191);
						panels[m + 1].BackColor = Color.FromArgb(191, 255, 191);
						panels[m + 2].BackColor = Color.FromArgb(191, 255, 191);
						panels[m + 3].BackColor = Color.FromArgb(191, 255, 191);
						panels[m + 4].BackColor = Color.FromArgb(191, 255, 191);
						panels[m + 5].BackColor = Color.FromArgb(191, 255, 191);
						panels[m + 6].BackColor = Color.FromArgb(191, 255, 191);
					}
			}

		}

		public void ClickO(object sender, EventArgs e)
		{
			for (int k = 0; k < ArtikulLables.Count; k++)
			{
				if (sender == DataLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}

				if (sender == ArtikulLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}

				if (sender == TovarLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}

				if (sender == NomberLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}

				if (sender == NameLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}

				if (sender == DataEndLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}

				if (sender == StatusLables[k])
				{
					IC.Hide();
					IC.AS = DatelNomer[k];
					IC.Owner = this;
					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();
					rdsr = new StreamReader(dir + @"\" + DatelNomer[k] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();
					IC.Show();
				}
			}
		}

		public void DrawExeaaal()
		{
			for (int i = 0; i < DataLables.Count; i++)
				DataLables[i].Dispose();
			DataLables.Clear();

			for (int i = 0; i < ArtikulLables.Count; i++)
				ArtikulLables[i].Dispose();
			ArtikulLables.Clear();

			for (int i = 0; i < TovarLables.Count; i++)
				TovarLables[i].Dispose();
			TovarLables.Clear();

			for (int i = 0; i < NomberLables.Count; i++)
				NomberLables[i].Dispose();
			NomberLables.Clear();

			for (int i = 0; i < NameLables.Count; i++)
				NameLables[i].Dispose();
			NameLables.Clear();

			for (int i = 0; i < DataEndLables.Count; i++)
				DataEndLables[i].Dispose();
			DataEndLables.Clear();

			for (int i = 0; i < StatusLables.Count; i++)
				StatusLables[i].Dispose();
			StatusLables.Clear();

			for (int i = 0; i < panels.Count; i++)
				panels[i].Dispose();
			panels.Clear();

			m = new DirectoryInfo(dir).GetDirectories().Length;
			for (int i = 0; i < m; i++)
			{
				DatelNomer[i] = i;
			}

			/*for (int i = 0; i < RemoveButton.Count; i++)
                RemoveButton[i].Dispose();
            RemoveButton.Clear();*/

			for (int j = 0; j < new DirectoryInfo(dir).GetDirectories().Length; j++)
			{
				m = j * 7;
				for (int i = 0; i < 7; i++)
					panels.Add(new Panel());

				panels[m].Location = new Point(3, 24 * j + 3 * j + 3);
				panels[m].Size = new Size(panel2.Size.Width, 24);

				panels[m + 1].Location = new Point(panel1.Location.X - 9, 24 * j + 3 * j + 3);
				panels[m + 1].Size = new Size(panel1.Size.Width, 24);

				panels[m + 2].Location = new Point(panel3.Location.X - 9, 24 * j + 3 * j + 3);
				panels[m + 2].Size = new Size(panel3.Size.Width, 24);

				panels[m + 3].Location = new Point(panel4.Location.X - 9, 24 * j + 3 * j + 3);
				panels[m + 3].Size = new Size(panel4.Size.Width, 24);

				panels[m + 4].Location = new Point(panel5.Location.X - 9, 24 * j + 3 * j + 3);
				panels[m + 4].Size = new Size(panel5.Size.Width, 24);

				panels[m + 5].Location = new Point(panel6.Location.X - 9, 24 * j + 3 * j + 3);
				panels[m + 5].Size = new Size(panel6.Size.Width, 24);

				panels[m + 6].Location = new Point(panel7.Location.X - 9, 24 * j + 3 * j + 3);
				panels[m + 6].Size = new Size(panel7.Size.Width, 24);

				for (int i = 0; i < 7; i++)
				{
					panels[m + i].BackColor = Color.FromArgb(191, 255, 191);
					if (File.Exists(dir + @"\" + j + @"\AVL.dbm"))
						panels[m + i].BackColor = Color.FromArgb(255, 191, 191);
					panels[m + i].BorderStyle = BorderStyle.FixedSingle;
					panels[m + i].ContextMenuStrip = contextMenuStrip1;
					panel8.Controls.Add(panels[m + i]);
				}

				DataLables.Add(new Label());
				StreamReader ardsr = new StreamReader(dir + @"\" + j + @"\DL.dbm");
				DataLables[j].Text = ardsr.ReadToEnd();
				DataLables[j].Location = new Point(3, 3);
				DataLables[j].Dock = DockStyle.Fill;
				DataLables[j].Click += new EventHandler(ClickO);
				panels[m].Controls.Add(DataLables[j]);
				ardsr.Close();


				ArtikulLables.Add(new Label());
				ardsr = new StreamReader(dir + @"\" + j + @"\AL.dbm");
				ArtikulLables[j].Text = ardsr.ReadToEnd();
				ArtikulLables[j].Location = new Point(3, 3);
				ArtikulLables[j].Dock = DockStyle.Fill;
				ArtikulLables[j].Click += new EventHandler(ClickO);
				panels[m + 1].Controls.Add(ArtikulLables[j]);
				ardsr.Close();


				TovarLables.Add(new Label());
				ardsr = new StreamReader(dir + @"\" + j + @"\TL.dbm");
				TovarLables[j].Text = ardsr.ReadToEnd();
				TovarLables[j].Location = new Point(3, 3);
				TovarLables[j].Dock = DockStyle.Fill;
				TovarLables[j].Click += new EventHandler(ClickO);
				panels[m + 2].Controls.Add(TovarLables[j]);
				ardsr.Close();


				NomberLables.Add(new Label());
				ardsr = new StreamReader(dir + @"\" + j + @"\NoL.dbm");
				NomberLables[j].Text = ardsr.ReadToEnd();
				NomberLables[j].Location = new Point(3, 3);
				NomberLables[j].Dock = DockStyle.Fill;
				NomberLables[j].Click += new EventHandler(ClickO);
				panels[m + 3].Controls.Add(NomberLables[j]);
				ardsr.Close();


				NameLables.Add(new Label());
				ardsr = new StreamReader(dir + @"\" + j + @"\NaL.dbm");
				NameLables[j].Text = ardsr.ReadToEnd();
				NameLables[j].Location = new Point(3, 3);
				NameLables[j].Dock = DockStyle.Fill;
				NameLables[j].Click += new EventHandler(ClickO);
				panels[m + 4].Controls.Add(NameLables[j]);
				ardsr.Close();


				DataEndLables.Add(new Label());
				FS = new StreamReader(dir + @"\" + j + @"\DEL.dbm");
				DataEndLables[j].Text = FS.ReadToEnd();
				DataEndLables[j].Location = new Point(3, 3);
				DataEndLables[j].Dock = DockStyle.Fill;
				DataEndLables[j].Click += new EventHandler(ClickO);
				panels[m + 5].Controls.Add(DataEndLables[j]);
				FS.Close();


				StatusLables.Add(new Label());
				FS = new StreamReader(dir + @"\" + j + @"\SL.dbm");
				StatusLables[j].Text = FS.ReadToEnd();
				StatusLables[j].Location = new Point(3, 3);
				StatusLables[j].Dock = DockStyle.Fill;
				StatusLables[j].Click += new EventHandler(ClickO);
				panels[m + 6].Controls.Add(StatusLables[j]);
				FS.Close();
			}
		}

		public void Open()
		{
			m = new DirectoryInfo(dir).GetDirectories().Length;
			Directory.CreateDirectory(dir + @"\" + m);

			StreamWriter ardsr = new StreamWriter(dir + @"\" + m + @"\DL.dbm");
			ardsr.WriteLine(DateTime.Today.ToString("dd MMMMMMMMMMM."));
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\AL.dbm");
			ardsr.WriteLine(f2.textBox2.Text);
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\TL.dbm");
			ardsr.WriteLine(f2.textBox3.Text);
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\NoL.dbm");
			ardsr.WriteLine(f2.textBox4.Text);
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\NaL.dbm");
			ardsr.WriteLine(f2.textBox5.Text);
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\NSL.dbm");
			ardsr.WriteLine(f2.textBox1.Text);
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\KL.dbm");
			ardsr.WriteLine(f2.textBox6.Text);
			ardsr.Close();

			ardsr = new StreamWriter(dir + @"\" + m + @"\ZL.dbm");
			ardsr.WriteLine(f2.textBox7.Text);
			ardsr.Close();

			ardsr = new StreamWriter((dir + @"\" + m + @"\DEL.dbm"));
			ardsr.WriteLine(f2.dateTimePicker1.Value.ToString("dd MMMMMMMMMMM."));
			ardsr.Close();

			ardsr = new StreamWriter((dir + @"\" + m + @"\SL.dbm"));
			ardsr.WriteLine("В процессе");
			ardsr.Close();

			ardsr = new StreamWriter((dir + @"\" + m + @"\Num.dbm"));
			ardsr.WriteLine(f2.N);
			ardsr.Close();

			for (int i = 0; i < E.Count; i++)
				if (f2.zll == E[i])
				{
					ardsr = new StreamWriter((dir + @"\" + m + @"\AVL.dbm"));
					ardsr.WriteLine(E[i]);
					ardsr.Close();
				}

			f2.Hide();
			f2.create = false;
			AddExel(m);
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int k = 0; k < panels.Count; k++)
				if (contextMenuStrip1.SourceControl == panels[k])
				{
					int i = k / 7;
					m = new DirectoryInfo(dir).GetDirectories().Length;
					Directory.Delete(dir + @"\" + i, true);
					for (int j = i; j < m - 1; j++)
					{
						b = j + 1;
						Directory.Move(dir + @"\" + b, dir + @"\" + j);
					}
					DrawExel();
				}
		}

		private void выбратьПутьКБазеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				File.WriteAllText(Application.StartupPath + @"\BBD.dlb", folderBrowserDialog1.SelectedPath.ToString());
				Application.Restart();
			}
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			width_new = Width;
			height_new = Height;

			double ow, oh;

			ow = (double)width_new / width;
			oh = (double)height_new / height;

			panelS1.Location = new Point(Convert.ToInt32(panelS1.Location.X * ow), panelS1.Location.Y);
			panelS2.Location = new Point(Convert.ToInt32(panelS2.Location.X * ow), panelS2.Location.Y);
			panelS3.Location = new Point(Convert.ToInt32(panelS3.Location.X * ow), panelS3.Location.Y);
			panelS4.Location = new Point(Convert.ToInt32(panelS4.Location.X * ow), panelS4.Location.Y);
			panelS5.Location = new Point(Convert.ToInt32(panelS5.Location.X * ow), panelS5.Location.Y);
			panelS6.Location = new Point(Convert.ToInt32(panelS6.Location.X * ow), panelS6.Location.Y);
			panelS7.Location = new Point(Convert.ToInt32(panelS7.Location.X * ow), panelS7.Location.Y);

			width = width_new;
			height = height_new;
			panel8.Focus();
		}

		Size sL;

		private void разработчикиToolStripMenuItem_Click(object sender, EventArgs e) => new Razrabi().Show();

		private void Form1_ResizeEnd(object sender, EventArgs e)
		{
			panel8.Focus();
			if (sL != Size)
			{
				panel8.Size = new Size(this.Size.Width - 32, this.Size.Height - 140);
				DrawExeNes();
				DrawExelNes();
			}
			CL = false;
		}

		private void Form1_SizeChanged(object sender, EventArgs e)
		{
			panel8.Focus();
			if (WindowState == FormWindowState.Maximized)
			{
				panel8.Size = new Size(this.Size.Width - 32, this.Size.Height - 140);
				DrawExeNes();
				DrawExelNes();
			}

			if (WindowState == FormWindowState.Normal && !CL)
			{
				panel8.Size = new Size(this.Size.Width - 32, this.Size.Height - 140);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void Form1_ResizeBegin(object sender, EventArgs e)
		{
			panel8.Focus();
			CL = true;
			sL = Size;
		}

		private void поискИИсправлениеОшибокToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FError.Owner = this;
			FError.Show();
		}

		private void поискToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Find FIND = new Find();
			FIND.Owner = this;
			FIND.Show();
		}

		private void panelS1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS1.Location.X, panelS1.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel2.Size = new Size(panelS1.Location.X - panel2.Location.X - 1, panel2.Size.Height);
				panel1.Location = new Point(panelS1.Location.X + 5, panel1.Location.Y);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS1_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
			{
				if (panelS1.Location.X < panelS2.Location.X - panelS1.Width && panelS1.Location.X > panel2.Location.X)
					panelS1.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS1.Location.Y);
				else
				{
					if (panelS1.Location.X >= panelS2.Location.X - panelS1.Width)
						panelS1.Location = new Point(panelS2.Location.X - panelS1.Width - 5, panelS1.Location.Y);
					if (panelS1.Location.X <= panel2.Location.X + panelS1.Width)
						panelS1.Location = new Point(panel2.Location.X + panelS1.Width + 5, panelS1.Location.Y);
					isMouseDown = false;
				}
			}
		}

		private void panelS2_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS2.Location.X, panelS2.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS2_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel1.Size = new Size(panelS2.Location.X - panel1.Location.X - 1, panel1.Size.Height);
				panel3.Location = new Point(panelS2.Location.X + 5, panel3.Location.Y);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS2_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				if (panelS2.Location.X < panelS3.Location.X - panelS2.Width && panelS2.Location.X > panelS1.Location.X + panelS2.Width)
					panelS2.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS2.Location.Y);
				else
				{
					if (panelS2.Location.X >= panelS3.Location.X - panelS2.Width)
						panelS2.Location = new Point(panelS3.Location.X - panelS2.Width - 5, panelS1.Location.Y);
					if (panelS2.Location.X <= panelS1.Location.X + panelS2.Width)
						panelS2.Location = new Point(panelS1.Location.X + panelS2.Width + 5, panelS1.Location.Y);
					isMouseDown = false;
				}
		}

		private void panelS3_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS3.Location.X, panelS3.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS3_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel3.Size = new Size(panelS3.Location.X - panel3.Location.X - 1, panel3.Size.Height);
				panel4.Location = new Point(panelS3.Location.X + 5, panel4.Location.Y);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS3_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				if (panelS3.Location.X < panelS4.Location.X - panelS3.Width && panelS3.Location.X > panelS2.Location.X + panelS3.Width)
					panelS3.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS3.Location.Y);
				else
				{
					if (panelS3.Location.X >= panelS4.Location.X - panelS3.Width)
						panelS3.Location = new Point(panelS4.Location.X - panelS3.Width - 5, panelS1.Location.Y);
					if (panelS3.Location.X <= panelS2.Location.X + panelS3.Width)
						panelS3.Location = new Point(panelS2.Location.X + panelS3.Width + 5, panelS1.Location.Y);
					isMouseDown = false;
				}
		}

		private void panelS4_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS4.Location.X, panelS4.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS4_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel4.Size = new Size(panelS4.Location.X - panel4.Location.X - 1, panel4.Size.Height);
				panel5.Location = new Point(panelS4.Location.X + 5, panel5.Location.Y);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS4_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				if (panelS4.Location.X < panelS5.Location.X - panelS1.Width && panelS4.Location.X > panelS3.Location.X + panelS1.Width)
					panelS4.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS4.Location.Y);
				else
				{
					if (panelS4.Location.X >= panelS5.Location.X - panelS3.Width)
						panelS4.Location = new Point(panelS5.Location.X - panelS4.Width - 5, panelS1.Location.Y);
					if (panelS4.Location.X <= panelS3.Location.X + panelS3.Width)
						panelS4.Location = new Point(panelS3.Location.X + panelS4.Width + 5, panelS1.Location.Y);
					isMouseDown = false;
				}
		}

		private void panelS5_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS5.Location.X, panelS5.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS5_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel5.Size = new Size(panelS5.Location.X - panel5.Location.X - 1, panel5.Size.Height);
				panel6.Location = new Point(panelS5.Location.X + 5, panel6.Location.Y);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS5_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				if (panelS5.Location.X < panelS6.Location.X - panelS5.Width && panelS5.Location.X > panelS4.Location.X + panelS5.Width)
					panelS5.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS5.Location.Y);
				else
				{
					if (panelS5.Location.X >= panelS6.Location.X - panelS5.Width)
						panelS5.Location = new Point(panelS6.Location.X - panelS5.Width - 5, panelS1.Location.Y);
					if (panelS5.Location.X <= panelS4.Location.X + panelS3.Width)
						panelS5.Location = new Point(panelS4.Location.X + panelS5.Width + 5, panelS1.Location.Y);
					isMouseDown = false;
				}
		}

		private void panelS6_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS6.Location.X, panelS6.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS6_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel6.Size = new Size(panelS6.Location.X - panel6.Location.X - 1, panel6.Size.Height);
				panel7.Location = new Point(panelS6.Location.X + 5, panel7.Location.Y);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS6_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				if (panelS6.Location.X < panelS7.Location.X - panelS5.Width && panelS6.Location.X > panelS5.Location.X + panelS5.Width)
					panelS6.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS6.Location.Y);
				else
				{
					if (panelS6.Location.X >= panelS7.Location.X - panelS5.Width)
						panelS6.Location = new Point(panelS7.Location.X - panelS5.Width - 5, panelS1.Location.Y);
					if (panelS6.Location.X <= panelS5.Location.X + panelS3.Width)
						panelS6.Location = new Point(panelS5.Location.X + panelS5.Width + 5, panelS1.Location.Y);
					isMouseDown = false;
				}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			width_new = this.MinimumSize.Width;

			double ow;

			ow = (double)width_new / width;

			panelS1.Location = new Point(Convert.ToInt32(panelS1.Location.X * ow), panelS1.Location.Y);
			panelS2.Location = new Point(Convert.ToInt32(panelS2.Location.X * ow), panelS2.Location.Y);
			panelS3.Location = new Point(Convert.ToInt32(panelS3.Location.X * ow), panelS3.Location.Y);
			panelS4.Location = new Point(Convert.ToInt32(panelS4.Location.X * ow), panelS4.Location.Y);
			panelS5.Location = new Point(Convert.ToInt32(panelS5.Location.X * ow), panelS5.Location.Y);
			panelS6.Location = new Point(Convert.ToInt32(panelS6.Location.X * ow), panelS6.Location.Y);
			panelS7.Location = new Point(Convert.ToInt32(panelS7.Location.X * ow), panelS7.Location.Y);
			try
			{
				File.WriteAllText(Application.StartupPath + @"\S1.dlb", panelS1.Location.X.ToString());
				File.WriteAllText(Application.StartupPath + @"\S2.dlb", panelS2.Location.X.ToString());
				File.WriteAllText(Application.StartupPath + @"\S3.dlb", panelS3.Location.X.ToString());
				File.WriteAllText(Application.StartupPath + @"\S4.dlb", panelS4.Location.X.ToString());
				File.WriteAllText(Application.StartupPath + @"\S5.dlb", panelS5.Location.X.ToString());
				File.WriteAllText(Application.StartupPath + @"\S6.dlb", panelS6.Location.X.ToString());
				File.WriteAllText(Application.StartupPath + @"\S7.dlb", panelS7.Location.X.ToString());
			}
			catch { }
		}

		private void panelS7_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				mouseOffset = new Point(MousePosition.X, MousePosition.Y);
				locOffset = new Point(panelS7.Location.X, panelS7.Location.Y);
				isMouseDown = true;
			}
		}

		private void panelS7_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				isMouseDown = false;
				panel7.Size = new Size(panelS7.Location.X - panel7.Location.X - 1, panel7.Size.Height);
				DrawExeNes();
				DrawExelNes();
			}
		}

		private void panelS7_MouseMove(object sender, MouseEventArgs e)
		{
			if (isMouseDown)
				if (panelS7.Location.X <= panel8.Location.X + panel8.Width - panelS7.Width - 25 && panelS7.Location.X > panelS6.Location.X + panelS7.Width)
					panelS7.Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), panelS7.Location.Y);
				else
				{
					if (panelS7.Location.X >= panel8.Location.X + panel8.Width - panelS7.Width - 25)
						panelS7.Location = new Point(panel8.Location.X + panel8.Width - panelS7.Width - 30, panelS7.Location.Y);
					if (panelS7.Location.X <= panelS6.Location.X + panelS7.Width)
						panelS7.Location = new Point(panelS6.Location.X + panelS7.Width + 5, panelS7.Location.Y);
					isMouseDown = false;
				}
		}

		private void обновитьГлобальноToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			DrawExeaaal();
		}

		private void личныйКабинетToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SMSForm ss = new SMSForm();
			ss.Owner = this;
			ss.Show();
		}

		private void panel8_MouseEnter(object sender, EventArgs e)
		{
			panel8.Focus();
		}

		private void panel8_MouseEnter(object sender, ScrollEventArgs e)
		{
			panel8.Focus();
		}

		private void panel2_Click(object sender, EventArgs e)
		{
			/* hook = !hook;
             if (!hook)
                 pictureBox1.Image = LMClient.Properties.Resources.ddd;
             else
                 pictureBox1.Image = LMClient.Properties.Resources.ddddown;
             DataSortir();*/
		}

		public void DataSortir()
		{
			/*if (panels.Count > 0)
            {
                if (hook)
                {
                    DataClass DC = new DataClass();
                    List<int> DN = new List<int>();
                    List<string> DLL = new List<string>();
                    List<string> ALL = new List<string>();
                    List<string> TLL = new List<string>();
                    List<string> NLL = new List<string>();
                    List<string> NaLL = new List<string>();
                    List<string> DELL = new List<string>();
                    List<string> SLL = new List<string>();
                    List<Color> CLOR = new List<Color>();

                    CLOR.Add(panels[0].BackColor);
                    DN.Add(DatelNomer[0]);
                    DLL.Add(DataLables[0].Text);
                    ALL.Add(ArtikulLables[0].Text);
                    TLL.Add(TovarLables[0].Text);
                    NLL.Add(NomberLables[0].Text);
                    NaLL.Add(NameLables[0].Text);
                    DELL.Add(DataEndLables[0].Text);
                    SLL.Add(StatusLables[0].Text);
                    for (int i = 1; i < DataLables.Count; i++)
                    {
                        DC = new DataClass(DLL[i - 1][0].ToString() + DLL[i - 1][1].ToString(), DLL[i - 1]);
                        if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 1)
                        {
                            CLOR.Add(panels[i * 7].BackColor);
                            DN.Add(DatelNomer[i]);
                            DLL.Add(DataLables[i].Text);
                            ALL.Add(ArtikulLables[i].Text);
                            TLL.Add(TovarLables[i].Text);
                            NLL.Add(NomberLables[i].Text);
                            NaLL.Add(NameLables[i].Text);
                            DELL.Add(DataEndLables[i].Text);
                            SLL.Add(StatusLables[i].Text);
                        }

                        if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 0)
                        {
                            CLOR.Add(panels[i * 7].BackColor);
                            DN.Add(DatelNomer[i]);
                            DLL.Add(DataLables[i].Text);
                            ALL.Add(ArtikulLables[i].Text);
                            TLL.Add(TovarLables[i].Text);
                            NLL.Add(NomberLables[i].Text);
                            NaLL.Add(NameLables[i].Text);
                            DELL.Add(DataEndLables[i].Text);
                            SLL.Add(StatusLables[i].Text);
                        }

                        if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == -1)
                        {
                            DC = new DataClass(DLL[0][0].ToString() + DLL[0][1].ToString(), DLL[0]);

                            if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == -1)
                            {
                                CLOR.Insert(0, panels[i * 7].BackColor);
                                DN.Insert(0, DatelNomer[i]);
                                DLL.Insert(0, DataLables[i].Text);
                                ALL.Insert(0, ArtikulLables[i].Text);
                                TLL.Insert(0, TovarLables[i].Text);
                                NLL.Insert(0, NomberLables[i].Text);
                                NaLL.Insert(0, NameLables[i].Text);
                                DELL.Insert(0, DataEndLables[i].Text);
                                SLL.Insert(0, StatusLables[i].Text);
                            }
                            else
                            if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 0)
                            {
                                CLOR.Insert(0, panels[i * 7].BackColor);
                                DN.Insert(0, DatelNomer[i]);
                                DLL.Insert(0, DataLables[i].Text);
                                ALL.Insert(0, ArtikulLables[i].Text);
                                TLL.Insert(0, TovarLables[i].Text);
                                NLL.Insert(0, NomberLables[i].Text);
                                NaLL.Insert(0, NameLables[i].Text);
                                DELL.Insert(0, DataEndLables[i].Text);
                                SLL.Insert(0, StatusLables[i].Text);
                            }
                            else
                                for (int l = i; l > 0; l--)
                                {
                                    DC = new DataClass(DLL[l - 1][0].ToString() + DLL[l - 1][1].ToString(), DLL[l - 1]);
                                    if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                        new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 1)
                                    {
                                        CLOR.Insert(l - 1, panels[i * 7].BackColor);
                                        DN.Insert(l - 1, DatelNomer[i]);
                                        DLL.Insert(l - 1, DataLables[i].Text);
                                        ALL.Insert(l - 1, ArtikulLables[i].Text);
                                        TLL.Insert(l - 1, TovarLables[i].Text);
                                        NLL.Insert(l - 1, NomberLables[i].Text);
                                        NaLL.Insert(l - 1, NameLables[i].Text);
                                        DELL.Insert(l - 1, DataEndLables[i].Text);
                                        SLL.Insert(l - 1, StatusLables[i].Text);
                                        break;
                                    }

                                    if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                        new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 0)
                                    {
                                        CLOR.Insert(l - 1, panels[i * 7].BackColor);
                                        DN.Insert(l - 1, DatelNomer[i]);
                                        DLL.Insert(l - 1, DataLables[i].Text);
                                        ALL.Insert(l - 1, ArtikulLables[i].Text);
                                        TLL.Insert(l - 1, TovarLables[i].Text);
                                        NLL.Insert(l - 1, NomberLables[i].Text);
                                        NaLL.Insert(l - 1, NameLables[i].Text);
                                        DELL.Insert(l - 1, DataEndLables[i].Text);
                                        SLL.Insert(l - 1, StatusLables[i].Text);
                                        break;
                                    }
                                }
                        }
                    }

                    for (int i = 0; i < DataLables.Count; i++)
                    {
                        panels[i * 7].BackColor = CLOR[i];
                        panels[i * 7 + 1].BackColor = CLOR[i];
                        panels[i * 7 + 2].BackColor = CLOR[i];
                        panels[i * 7 + 3].BackColor = CLOR[i];
                        panels[i * 7 + 4].BackColor = CLOR[i];
                        panels[i * 7 + 5].BackColor = CLOR[i];
                        panels[i * 7 + 6].BackColor = CLOR[i];
                        DatelNomer[i] = DN[i];
                        DataLables[i].Text = DLL[i];
                        ArtikulLables[i].Text = ALL[i];
                        TovarLables[i].Text = TLL[i];
                        NomberLables[i].Text = NLL[i];
                        NameLables[i].Text = NaLL[i];
                        DataEndLables[i].Text = DELL[i];
                        StatusLables[i].Text = SLL[i];
                    }
                }
                else
                {
                    DataClass DC = new DataClass();
                    List<int> DN = new List<int>();
                    List<string> DLL = new List<string>();
                    List<string> ALL = new List<string>();
                    List<string> TLL = new List<string>();
                    List<string> NLL = new List<string>();
                    List<string> NaLL = new List<string>();
                    List<string> DELL = new List<string>();
                    List<string> SLL = new List<string>();

                    DN.Add(DatelNomer[0]);
                    DLL.Add(DataLables[0].Text);
                    ALL.Add(ArtikulLables[0].Text);
                    TLL.Add(TovarLables[0].Text);
                    NLL.Add(NomberLables[0].Text);
                    NaLL.Add(NameLables[0].Text);
                    DELL.Add(DataEndLables[0].Text);
                    SLL.Add(StatusLables[0].Text);
                    for (int i = 1; i < DataLables.Count; i++)
                    {
                        DC = new DataClass(DLL[i - 1][0].ToString() + DLL[i - 1][1].ToString(), DLL[i - 1]);
                        if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == -1)
                        {
                            DN.Add(DatelNomer[i]);
                            DLL.Add(DataLables[i].Text);
                            ALL.Add(ArtikulLables[i].Text);
                            TLL.Add(TovarLables[i].Text);
                            NLL.Add(NomberLables[i].Text);
                            NaLL.Add(NameLables[i].Text);
                            DELL.Add(DataEndLables[i].Text);
                            SLL.Add(StatusLables[i].Text);
                        }

                        if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 0)
                        {
                            DN.Add(DatelNomer[i]);
                            DLL.Add(DataLables[i].Text);
                            ALL.Add(ArtikulLables[i].Text);
                            TLL.Add(TovarLables[i].Text);
                            NLL.Add(NomberLables[i].Text);
                            NaLL.Add(NameLables[i].Text);
                            DELL.Add(DataEndLables[i].Text);
                            SLL.Add(StatusLables[i].Text);
                        }

                        if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 1)
                        {
                            DC = new DataClass(DLL[0][0].ToString() + DLL[0][1].ToString(), DLL[0]);

                            if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 1)
                            {
                                DN.Insert(0, DatelNomer[i]);
                                DLL.Insert(0, DataLables[i].Text);
                                ALL.Insert(0, ArtikulLables[i].Text);
                                TLL.Insert(0, TovarLables[i].Text);
                                NLL.Insert(0, NomberLables[i].Text);
                                NaLL.Insert(0, NameLables[i].Text);
                                DELL.Insert(0, DataEndLables[i].Text);
                                SLL.Insert(0, StatusLables[i].Text);
                            }
                            else
                            if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                    new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 0)
                            {
                                DN.Insert(0, DatelNomer[i]);
                                DLL.Insert(0, DataLables[i].Text);
                                ALL.Insert(0, ArtikulLables[i].Text);
                                TLL.Insert(0, TovarLables[i].Text);
                                NLL.Insert(0, NomberLables[i].Text);
                                NaLL.Insert(0, NameLables[i].Text);
                                DELL.Insert(0, DataEndLables[i].Text);
                                SLL.Insert(0, StatusLables[i].Text);
                            }
                            else
                                for (int l = i; l > 0; l--)
                                {
                                    DC = new DataClass(DLL[l - 1][0].ToString() + DLL[l - 1][1].ToString(), DLL[l - 1]);
                                    if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                        new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == -1)
                                    {
                                        DN.Insert(l - 1, DatelNomer[i]);
                                        DLL.Insert(l - 1, DataLables[i].Text);
                                        ALL.Insert(l - 1, ArtikulLables[i].Text);
                                        TLL.Insert(l - 1, TovarLables[i].Text);
                                        NLL.Insert(l - 1, NomberLables[i].Text);
                                        NaLL.Insert(l - 1, NameLables[i].Text);
                                        DELL.Insert(l - 1, DataEndLables[i].Text);
                                        SLL.Insert(l - 1, StatusLables[i].Text);
                                        break;
                                    }

                                    if (DC.Rezise(new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).day,
                                        new DataClass(DataLables[i].Text[0].ToString() + DataLables[i].Text[1].ToString(), DataLables[i].Text).month) == 0)
                                    {
                                        DN.Insert(l - 1, DatelNomer[i]);
                                        DLL.Insert(l - 1, DataLables[i].Text);
                                        ALL.Insert(l - 1, ArtikulLables[i].Text);
                                        TLL.Insert(l - 1, TovarLables[i].Text);
                                        NLL.Insert(l - 1, NomberLables[i].Text);
                                        NaLL.Insert(l - 1, NameLables[i].Text);
                                        DELL.Insert(l - 1, DataEndLables[i].Text);
                                        SLL.Insert(l - 1, StatusLables[i].Text);
                                        break;
                                    }
                                }
                        }
                    }

                    for (int i = 0; i < DataLables.Count; i++)
                    {
                        DatelNomer[i] = DN[i];
                        DataLables[i].Text = DLL[i];
                        ArtikulLables[i].Text = ALL[i];
                        TovarLables[i].Text = TLL[i];
                        NomberLables[i].Text = NLL[i];
                        NameLables[i].Text = NaLL[i];
                        DataEndLables[i].Text = DELL[i];
                        StatusLables[i].Text = SLL[i];
                    }
                }
            }*/
		}


		private void label1_Click(object sender, EventArgs e)
		{
			/*hook = !hook;
            if(!hook)
                pictureBox1.Image = LMClient.Properties.Resources.ddd;
            else
                pictureBox1.Image = LMClient.Properties.Resources.ddddown;
            DataSortir();*/
		}

		private void Автозагрузкавклвыкл_Click(object sender, EventArgs e)
		{
			try
			{
				var key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
				string[] k = key.GetValueNames();
				bool be = false;
				for (int i = 0; i < k.Length; i++)
					if (k[i] == "LMClient")
					{
						key.DeleteValue("LMClient");
						be = true;
						Автозагрузкавклвыкл.Text = "Автозагрузка вкл";
						break;
					}

				if (!be)
				{
					key.SetValue("LMClient", Application.ExecutablePath);
					Автозагрузкавклвыкл.Text = "Автозагрузка выкл";
				}
			}
			catch
			{
				MessageBox.Show("Запустите программу от имени администратора", "Внимание");
			}
		}

		private void чатToolStripMenuItem_Click(object sender, EventArgs e)
		{
			VLog LN = new VLog();
			if (File.Exists(Application.StartupPath + @"\ln.dlb"))
				if (File.Exists(Application.StartupPath + @"\ps.dlb"))
					if (File.Exists(Application.StartupPath + @"\nns.dlb"))
						System.Diagnostics.Process.Start(Application.StartupPath + @"\shari.exe");
					else
						LN.Show();
				else
					LN.Show();
			else
				LN.Show();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{/*
            hook = !hook;
            if (!hook)
                pictureBox1.Image = LMClient.Properties.Resources.ddd;
            else
                pictureBox1.Image = LMClient.Properties.Resources.ddddown;
            DataSortir();*/
		}

		private void Potok_Tick(object sender, EventArgs e)
		{
			if (ArtikulLables.Count > 0)
			{
				kola++;
				if (kola >= ArtikulLables.Count - 1)
					kola = 0;

				try
				{
					if (File.Exists(dir + @"\" + DatelNomer[kola] + @"\AVL.dbm"))
					{
						panels[kola * 7].BackColor = Color.FromArgb(255, 191, 191);
						panels[kola * 7 + 1].BackColor = Color.FromArgb(255, 191, 191);
						panels[kola * 7 + 2].BackColor = Color.FromArgb(255, 191, 191);
						panels[kola * 7 + 3].BackColor = Color.FromArgb(255, 191, 191);
						panels[kola * 7 + 4].BackColor = Color.FromArgb(255, 191, 191);
						panels[kola * 7 + 5].BackColor = Color.FromArgb(255, 191, 191);
						panels[kola * 7 + 6].BackColor = Color.FromArgb(255, 191, 191);
					}
					else
					{
						panels[kola * 7].BackColor = Color.FromArgb(191, 255, 191);
						panels[kola * 7 + 1].BackColor = Color.FromArgb(191, 255, 191);
						panels[kola * 7 + 2].BackColor = Color.FromArgb(191, 255, 191);
						panels[kola * 7 + 3].BackColor = Color.FromArgb(191, 255, 191);
						panels[kola * 7 + 4].BackColor = Color.FromArgb(191, 255, 191);
						panels[kola * 7 + 5].BackColor = Color.FromArgb(191, 255, 191);
						panels[kola * 7 + 6].BackColor = Color.FromArgb(191, 255, 191);
					}
				}
				catch { }
			}
		}

		private void добавитьБиблиотекуxslsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<string> list_row = new List<string>();
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				excelapp = new Excel.Application();
				excelappworkbooks = excelapp.Workbooks;
				excelworksheet = (Excel.Worksheet)excelapp.Workbooks.Open(openFileDialog1.FileName,
				 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
				 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
				 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
				 Type.Missing, Type.Missing).Worksheets.get_Item(1);

				A.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[2, 3]).Value2));
				B.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[2, 4]).Value2));
				C.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[2, 5]).Value2));
				D.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[2, 10]).Value2));
				E.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[2, 11]).Value2));

				impTimerMax = excelapp.Cells.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing).Row - 1;

				impTimer = A.Count;
				ImportExcel.Enabled = true;
			}
		}

		private void просмотрБиблиотекExcelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			A.Clear();
			B.Clear();
			C.Clear();
			D.Clear();
			E.Clear();
			File.WriteAllText(Application.StartupPath + @"\A.dlb", "");
			File.WriteAllText(Application.StartupPath + @"\B.dlb", "");
			File.WriteAllText(Application.StartupPath + @"\C.dlb", "");
			File.WriteAllText(Application.StartupPath + @"\D.dlb", "");
			File.WriteAllText(Application.StartupPath + @"\E.dlb", "");
		}

		private void ImportExcel_Tick(object sender, EventArgs e)
		{
			label8.Text = impTimer.ToString() + " из " + impTimerMax;
			loadingPanel.Size = new Size((int)((Size.Width - 5) * ((double)impTimer / impTimerMax)), 2);

			A.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[impTimer + 2, 3]).Value2));
			B.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[impTimer + 2, 4]).Value2));
			C.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[impTimer + 2, 5]).Value2));
			D.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[impTimer + 2, 10]).Value2));
			E.Add(Convert.ToString(((Excel.Range)excelworksheet.Cells[impTimer + 2, 11]).Value2));
			impTimer++;

			if (A[impTimer - 1] == "")
			{
				label8.Text = "";
				loadingPanel.Size = new Size(0, 0);
				ImportExcel.Enabled = false;
				excelapp.Quit();
				string sA = "", sB = "", sC = "", sD = "", sE = "";

				for (int i = 0; i < A.Count; i++)
				{
					sA += Environment.NewLine;
					sA += A[i];
					sB += Environment.NewLine;
					sB += B[i];
					sC += Environment.NewLine;
					sC += C[i];
					sD += Environment.NewLine;
					sD += D[i];
					sE += Environment.NewLine;
					sE += E[i];
				}

				File.WriteAllText(Application.StartupPath + @"\A.dlb", sA);
				File.WriteAllText(Application.StartupPath + @"\B.dlb", sB);
				File.WriteAllText(Application.StartupPath + @"\C.dlb", sC);
				File.WriteAllText(Application.StartupPath + @"\D.dlb", sD);
				File.WriteAllText(Application.StartupPath + @"\E.dlb", sE);
			}
		}

		private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DrawExeNes();
			DrawExelNes();
		}

		private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			for (int k = 0; k < panels.Count; k++)
				if (contextMenuStrip1.SourceControl == panels[k])
				{
					int i = k / 7;

					IC.Hide();
					IC.AS = DatelNomer[i];
					IC.Owner = this;

					StreamReader rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\NaL.dbm");
					IC.label1.Text = rdsr.ReadLine();
					rdsr.Close();

					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\NoL.dbm");
					IC.label2.Text = rdsr.ReadLine();
					rdsr.Close();

					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\TL.dbm");
					IC.label3.Text = rdsr.ReadLine();
					rdsr.Close();

					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\AL.dbm");
					IC.label4.Text = rdsr.ReadLine();
					rdsr.Close();
					//
					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\DEL.dbm");
					IC.label6.Text = rdsr.ReadLine();
					rdsr.Close();

					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\DL.dbm");
					IC.label7.Text = rdsr.ReadLine();
					rdsr.Close();

					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\NSL.dbm");
					IC.label11.Text = rdsr.ReadLine();
					rdsr.Close();

					rdsr = new StreamReader(dir + @"\" + DatelNomer[i] + @"\KL.dbm");
					IC.label12.Text = rdsr.ReadLine() + " шт.";
					rdsr.Close();

					IC.Show();
				}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (f2.create)
			{
				Open();
			}
		}

		public void DeleteRemove(int AS)
		{
			m = new DirectoryInfo(dir).GetDirectories().Length;
			Directory.Delete(dir + @"\" + AS, true);
			for (int j = AS; j < m - 1; j++)
			{
				b = j + 1;
				Directory.Move(dir + @"\" + b, dir + @"\" + j);
			}
			DrawExel();
		}

		public void DrawExeNes()
		{
			if (panelS7.Location.X > panel8.Location.X + panel8.Width - panelS7.Width - 22)
				panelS7.Location = new Point(panel8.Location.X + panel8.Width - panelS7.Width - 22, panelS7.Location.Y);
			panel7.Size = new Size(panelS7.Location.X - panel7.Location.X - 1, panel7.Size.Height);
			panel6.Size = new Size(panelS6.Location.X - panel6.Location.X - 1, panel6.Size.Height);
			panel7.Location = new Point(panelS6.Location.X + 5, panel7.Location.Y);
			panel5.Size = new Size(panelS5.Location.X - panel5.Location.X - 1, panel5.Size.Height);
			panel6.Location = new Point(panelS5.Location.X + 5, panel6.Location.Y);
			panel4.Size = new Size(panelS4.Location.X - panel4.Location.X - 1, panel4.Size.Height);
			panel5.Location = new Point(panelS4.Location.X + 5, panel5.Location.Y);
			panel3.Size = new Size(panelS3.Location.X - panel3.Location.X - 1, panel3.Size.Height);
			panel4.Location = new Point(panelS3.Location.X + 5, panel4.Location.Y);
			panel1.Size = new Size(panelS2.Location.X - panel1.Location.X - 1, panel1.Size.Height);
			panel3.Location = new Point(panelS2.Location.X + 5, panel3.Location.Y);
			panel2.Size = new Size(panelS1.Location.X - panel2.Location.X - 1, panel2.Size.Height);
			panel1.Location = new Point(panelS1.Location.X + 5, panel1.Location.Y);
			label1.MaximumSize = panel2.Size;
			label2.MaximumSize = panel1.Size;
			label3.MaximumSize = panel3.Size;
			label4.MaximumSize = panel4.Size;
			label5.MaximumSize = panel5.Size;
			label6.MaximumSize = panel6.Size;
			label7.MaximumSize = panel7.Size;
		}

		public void DrawExelNes()
		{
			panel7.Size = new Size(panelS7.Location.X - panel7.Location.X - 1, panel7.Size.Height);
			panel6.Size = new Size(panelS6.Location.X - panel6.Location.X - 1, panel6.Size.Height);
			panel7.Location = new Point(panelS6.Location.X + 5, panel7.Location.Y);
			panel5.Size = new Size(panelS5.Location.X - panel5.Location.X - 1, panel5.Size.Height);
			panel6.Location = new Point(panelS5.Location.X + 5, panel6.Location.Y);
			panel4.Size = new Size(panelS4.Location.X - panel4.Location.X - 1, panel4.Size.Height);
			panel5.Location = new Point(panelS4.Location.X + 5, panel5.Location.Y);
			panel3.Size = new Size(panelS3.Location.X - panel3.Location.X - 1, panel3.Size.Height);
			panel4.Location = new Point(panelS3.Location.X + 5, panel4.Location.Y);
			panel1.Size = new Size(panelS2.Location.X - panel1.Location.X - 1, panel1.Size.Height);
			panel3.Location = new Point(panelS2.Location.X + 5, panel3.Location.Y);
			panel2.Size = new Size(panelS1.Location.X - panel2.Location.X - 1, panel2.Size.Height);
			panel1.Location = new Point(panelS1.Location.X + 5, panel1.Location.Y);
			label1.MaximumSize = panel2.Size;
			label2.MaximumSize = panel1.Size;
			label3.MaximumSize = panel3.Size;
			label4.MaximumSize = panel4.Size;
			label5.MaximumSize = panel5.Size;
			label6.MaximumSize = panel6.Size;
			label7.MaximumSize = panel7.Size;
			DrawExel();
		}

		public void DrawExel()
		{
			if (dir != null)
			{
				m = new DirectoryInfo(dir).GetDirectories().Length;
				for (int i = 0; i < m; i++)
				{
					DatelNomer[i] = i;
				}

				for (int j = 0; j < DataLables.Count; j++)
				{
					if (Directory.Exists(dir + @"\" + j))
					{
						StreamReader ardsr = new StreamReader(dir + @"\" + j + @"\DL.dbm");
						if (DataLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\DL.dbm");
							DataLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						ardsr = new StreamReader(dir + @"\" + j + @"\AL.dbm");
						if (ArtikulLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\AL.dbm");
							ArtikulLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						ardsr = new StreamReader(dir + @"\" + j + @"\TL.dbm");
						if (TovarLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\TL.dbm");
							TovarLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						ardsr = new StreamReader(dir + @"\" + j + @"\NoL.dbm");
						if (NomberLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\NoL.dbm");
							NomberLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						ardsr = new StreamReader(dir + @"\" + j + @"\NaL.dbm");
						if (NameLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\NaL.dbm");
							NameLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						ardsr = new StreamReader(dir + @"\" + j + @"\DEL.dbm");
						if (DataEndLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\DEL.dbm");
							DataEndLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						ardsr = new StreamReader(dir + @"\" + j + @"\SL.dbm");
						if (StatusLables[j].Text != ardsr.ReadToEnd())
						{
							StreamReader ardssr = new StreamReader(dir + @"\" + j + @"\SL.dbm");
							StatusLables[j].Text = ardssr.ReadToEnd();
							ardssr.Close();
						}
						ardsr.Close();

						panels[m + j].BackColor = Color.FromArgb(191, 255, 191);
						if (File.Exists(dir + @"\" + j + @"\AVL.dbm"))
							panels[m + j].BackColor = Color.FromArgb(255, 191, 191);
					}
					else
					{
						DataLables[j].Dispose();
						ArtikulLables[j].Dispose();
						TovarLables[j].Dispose();
						NomberLables[j].Dispose();
						NameLables[j].Dispose();
						DataEndLables[j].Dispose();
						StatusLables[j].Dispose();

						panels[m * 7].Dispose();
						panels[m * 7 + 1].Dispose();
						panels[m * 7 + 2].Dispose();
						panels[m * 7 + 3].Dispose();
						panels[m * 7 + 4].Dispose();
						panels[m * 7 + 5].Dispose();
						panels[m * 7 + 6].Dispose();

						DataLables.Remove(DataLables[j]);
						ArtikulLables.Remove(ArtikulLables[j]);
						TovarLables.Remove(TovarLables[j]);
						NomberLables.Remove(NomberLables[j]);
						NameLables.Remove(NameLables[j]);
						DataEndLables.Remove(DataEndLables[j]);
						StatusLables.Remove(StatusLables[j]);

						panels.Remove(panels[m * 7]);
						panels.Remove(panels[m * 7]);
						panels.Remove(panels[m * 7]);
						panels.Remove(panels[m * 7]);
						panels.Remove(panels[m * 7]);
						panels.Remove(panels[m * 7]);
						panels.Remove(panels[m * 7]);
					}
					DrawSize();
				}

				for (int i = 0; i < ArtikulLables.Count; i++)
				{
					panels[i * 7].BackColor = Color.FromArgb(191, 255, 191);
					panels[i * 7 + 1].BackColor = Color.FromArgb(191, 255, 191);
					panels[i * 7 + 2].BackColor = Color.FromArgb(191, 255, 191);
					panels[i * 7 + 3].BackColor = Color.FromArgb(191, 255, 191);
					panels[i * 7 + 4].BackColor = Color.FromArgb(191, 255, 191);
					panels[i * 7 + 5].BackColor = Color.FromArgb(191, 255, 191);
					panels[i * 7 + 6].BackColor = Color.FromArgb(191, 255, 191);
					if (File.Exists(dir + @"\" + i + @"\AVL.dbm"))
					{
						panels[i * 7].BackColor = Color.FromArgb(255, 191, 191);
						panels[i * 7 + 1].BackColor = Color.FromArgb(255, 191, 191);
						panels[i * 7 + 2].BackColor = Color.FromArgb(255, 191, 191);
						panels[i * 7 + 3].BackColor = Color.FromArgb(255, 191, 191);
						panels[i * 7 + 4].BackColor = Color.FromArgb(255, 191, 191);
						panels[i * 7 + 5].BackColor = Color.FromArgb(255, 191, 191);
						panels[i * 7 + 6].BackColor = Color.FromArgb(255, 191, 191);//
					}
				}
				DataSortir();
			}
		}

		public void DrawSize()
		{
			for (int i = 0; i < DataLables.Count; i++)
			{

				panels[i * 7].Location = new Point(3, panels[i * 7].Location.Y);
				panels[i * 7 + 1].Location = new Point(panel1.Location.X - 9, panels[i * 7 + 1].Location.Y);
				panels[i * 7 + 2].Location = new Point(panel3.Location.X - 9, panels[i * 7 + 2].Location.Y);
				panels[i * 7 + 3].Location = new Point(panel4.Location.X - 9, panels[i * 7 + 3].Location.Y);
				panels[i * 7 + 4].Location = new Point(panel5.Location.X - 9, panels[i * 7 + 4].Location.Y);
				panels[i * 7 + 5].Location = new Point(panel6.Location.X - 9, panels[i * 7 + 5].Location.Y);
				panels[i * 7 + 6].Location = new Point(panel7.Location.X - 9, panels[i * 7 + 6].Location.Y);

				panels[i * 7].Size = new Size(panel2.Size.Width, panels[i * 7].Size.Height);
				panels[i * 7 + 1].Size = new Size(panel1.Size.Width, panels[i * 7 + 1].Size.Height);
				panels[i * 7 + 2].Size = new Size(panel3.Size.Width, panels[i * 7 + 2].Size.Height);
				panels[i * 7 + 3].Size = new Size(panel4.Size.Width, panels[i * 7 + 3].Size.Height);
				panels[i * 7 + 4].Size = new Size(panel5.Size.Width, panels[i * 7 + 4].Size.Height);
				panels[i * 7 + 5].Size = new Size(panel6.Size.Width, panels[i * 7 + 5].Size.Height);
				panels[i * 7 + 6].Size = new Size(panel7.Size.Width, panels[i * 7 + 6].Size.Height);

			}
		}
	}

	public class DataClass
	{
		public int day = 1, month = 1;
		public DataClass()
		{
			day = 1;
			month = 1;
		}

		public DataClass(int day, int month)
		{
			this.day = day;
			this.month = month;
		}

		public DataClass(int day, string month)
		{
			this.day = day;
			if (month.Contains("янв"))
				this.month = 1;
			if (month.Contains("февр"))
				this.month = 2;
			if (month.Contains("март"))
				this.month = 3;
			if (month.Contains("апре"))
				this.month = 4;
			if (month.Contains("мая"))
				this.month = 5;
			if (month.Contains("июн"))
				this.month = 6;
			if (month.Contains("июл"))
				this.month = 7;
			if (month.Contains("авг"))
				this.month = 8;
			if (month.Contains("сент"))
				this.month = 9;
			if (month.Contains("окт"))
				this.month = 10;
			if (month.Contains("ноябр"))
				this.month = 11;
			if (month.Contains("декабр"))
				this.month = 12;
		}

		public DataClass(string day, string month)
		{
			this.day = int.Parse(day);
			if (month.Contains("янв"))
				this.month = 1;
			if (month.Contains("февр"))
				this.month = 2;
			if (month.Contains("март"))
				this.month = 3;
			if (month.Contains("апре"))
				this.month = 4;
			if (month.Contains("мая"))
				this.month = 5;
			if (month.Contains("июн"))
				this.month = 6;
			if (month.Contains("июл"))
				this.month = 7;
			if (month.Contains("авг"))
				this.month = 8;
			if (month.Contains("сент"))
				this.month = 9;
			if (month.Contains("окт"))
				this.month = 10;
			if (month.Contains("ноябр"))
				this.month = 11;
			if (month.Contains("декабр"))
				this.month = 12;
		}

		public int Rezise(int day, int month)
		{
			int itog = 2;
			if (this.month > month)
				itog = -1;

			if (this.month < month)
				itog = 1;

			if (this.month == month)
			{
				if (this.day > day)
					itog = -1;

				if (this.day < day)
					itog = 1;

				if (this.day == day)
					itog = 0;
			}
			return itog;
		}
	}
}