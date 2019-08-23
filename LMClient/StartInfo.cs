using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class StartInfo : Form
    {
        public int m;
        string dir;
        private Point mouseOffset, locOffset;
        private bool isMouseDown = false;

        public List<int> ASin = new List<int>();

        public List<Label> Lab = new List<Label>();

        public InfoClient IC = new InfoClient();

        public List<Panel> panels = new List<Panel>();

        StreamReader FS, FE;

        public List<string> A = new List<string>();
        public List<string> B = new List<string>();
        public List<string> E = new List<string>();

        public StartInfo()
        {
            InitializeComponent();
        }

        private void StartInfo_Load(object sender, EventArgs e)
        {
            A = ((Form1)Owner).A;
            B = ((Form1)Owner).B;
            E = ((Form1)Owner).E;
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                for (int i = 0; i < ASin.Count; i++)
                {
                    panels.Add(new Panel());
                    panels[i].Location = new Point(3, i * 12 + (i * 12) + 2 * i + 3 * i + 3);
                    panels[i].Size = new Size(panel1.Size.Width - 26, 27);
                    panels[i].BackColor = Color.FromArgb(191, 255, 191);
                    
                    
                    for (int j = 0; j < A.Count; j++)
                    {
                        StreamReader rdsr = new StreamReader(dir + @"\" + ASin[i] + @"\AL.dbm");
                        StreamReader rdsrss = new StreamReader(dir + @"\" + ASin[i] + @"\AL.dbm");
                        if (rdsrss.ReadToEnd().Contains(A[j]) || rdsr.ReadToEnd().Contains(B[j]))
                            if (E[j] != "")
                                panels[i].BackColor = Color.FromArgb(255, 191, 191);
                        rdsrss.Close();
                        rdsr.Close();
                    }

                    panels[i].BorderStyle = BorderStyle.FixedSingle;
                    panels[i].ContextMenuStrip = contextMenuStrip1;
                    panel1.Controls.Add(panels[i]);
                    panels[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    panels[i].ContextMenuStrip = contextMenuStrip1;
                    panel1.Controls.Add(panels[i]);

                    Lab.Add(new Label());
                    StreamReader rdsrs = new StreamReader(dir + @"\" + ASin[i] + @"\NaL.dbm");
                    StreamReader rdsars = new StreamReader(dir + @"\" + ASin[i] + @"\NoL.dbm");
                    Lab[i].Text = (rdsrs.ReadLine() + " | " + rdsars.ReadLine());
                    rdsars.Close();
                    rdsrs.Close();
                    Lab[i].Location = new Point(3, 3);
                    Lab[i].Size = new Size(panels[i].Size.Width, Lab[i].Size.Height);
                    Lab[i].Click += new EventHandler(ClickO);
                    Lab[i].MouseEnter += new EventHandler(label_MouseEnter);
                    Lab[i].MouseLeave += new EventHandler(label_MouseLeave);
                    panels[i].Controls.Add(Lab[i]);
                }
        }

        private void StartInfo_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseDown = false;
        }

        private void StartInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
                Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), MousePosition.Y - (mouseOffset.Y - locOffset.Y));

        }

        private void StartInfo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(MousePosition.X, MousePosition.Y);
                locOffset = new Point(this.Location.X, this.Location.Y);
                isMouseDown = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DrawExel();
            if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            for (int k = 0; k < Lab.Count; k++)
                if (sender == Lab[k])
                    Lab[k].Font = new Font(label2.Font, FontStyle.Underline);

        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            for (int k = 0; k < Lab.Count; k++)
                if (sender == Lab[k])
                    Lab[k].Font = new Font(label2.Font, FontStyle.Regular);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                isMouseDown = false;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
                Location = new Point(MousePosition.X - (mouseOffset.X - locOffset.X), MousePosition.Y - (mouseOffset.Y - locOffset.Y));

        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOffset = new Point(MousePosition.X, MousePosition.Y);
                locOffset = new Point(this.Location.X, this.Location.Y);
                isMouseDown = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void ClickO(object sender, EventArgs e)
        {
            for (int k = 0; k < Lab.Count; k++)
                if (sender == Lab[k])
                {
                    IC.Hide();
                    IC.AS = ASin[k];
                    IC.Owner = this;
                    StreamReader rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\NaL.dbm");
                    IC.label1.Text = rdsr.ReadLine();
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\NoL.dbm");
                    IC.label2.Text = rdsr.ReadLine();
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\TL.dbm");
                    IC.label3.Text = rdsr.ReadLine();
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\AL.dbm");
                    IC.label4.Text = rdsr.ReadLine();
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\DEL.dbm");
                    IC.label6.Text = rdsr.ReadLine();
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\NSL.dbm");
                    IC.label11.Text = rdsr.ReadLine();
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\KL.dbm");
                    IC.label12.Text = rdsr.ReadLine() + " шт.";
                    rdsr.Close();
                    rdsr = new StreamReader(dir + @"\" + ASin[k] + @"\DL.dbm");
                    IC.label7.Text = rdsr.ReadLine();
                    rdsr.Close();
                    IC.Show();
                }
        }

        public void DrawExel()
        {
            label2.Text = "";

            for (int i = 0; i < Lab.Count; i++)
            {
                Lab[i].Dispose();
                panels[i].Dispose();
            }
            panels.Clear();                
            Lab.Clear();

            ASin.Clear();

            m = new DirectoryInfo(dir).GetDirectories().Length;
            for (int i = 0; i < m; i++)
            {
                FS = new StreamReader(dir + @"\" + i + @"\DEL.dbm");
                FE = new StreamReader(dir + @"\" + i + @"\SL.dbm");
                if (FS.ReadLine() == DateTime.Today.ToString("dd MMMMMMMMMMM.") && FE.ReadLine() != "Клиент оповещен")
                {
                    FE.Close();
                    File.WriteAllText(dir + @"\" + i + @"\SL.dbm", "Не оповещен");
                    ASin.Add(i);
                }
                FE.Close();
                FS.Close();
            }

            for (int i = 0; i < ASin.Count; i++)
            {
                panels.Add(new Panel());
                panels[i].Location = new Point(3, i * 12 + (i * 12) + 2 * i + 3 * i + 3);
                panels[i].Size = new Size(panel1.Size.Width - 26, 27);
                panels[i].BackColor = Color.FromArgb(191, 255, 191);
                for (int j = 0; j < A.Count; j++)
                {
                    StreamReader rdsr = new StreamReader(dir + @"\" + ASin[i] + @"\AL.dbm");
                    StreamReader rdsrss = new StreamReader(dir + @"\" + ASin[i] + @"\AL.dbm");
                    if (rdsrss.ReadToEnd().Contains(A[j]) || rdsr.ReadToEnd().Contains(B[j]))
                        if (E[j] != "")
                            panels[i].BackColor = Color.FromArgb(255, 191, 191);
                    rdsrss.Close();
                    rdsr.Close();
                }

                panels[i].BorderStyle = BorderStyle.FixedSingle;
                panels[i].ContextMenuStrip = contextMenuStrip1;
                panel1.Controls.Add(panels[i]);

                Lab.Add(new Label());
                StreamReader rdsrs = new StreamReader(dir + @"\" + ASin[i] + @"\NaL.dbm");
                StreamReader rdsars = new StreamReader(dir + @"\" + ASin[i] + @"\NoL.dbm");
                Lab[i].Text = (rdsrs.ReadLine() + " | " + rdsars.ReadLine());
                rdsars.Close();
                rdsrs.Close();
                Lab[i].Location = new Point(3, 3);
                Lab[i].Size = new Size(panels[i].Size.Width, Lab[i].Size.Height);
                Lab[i].Click += new EventHandler(ClickO);
                Lab[i].MouseEnter += new EventHandler(label_MouseEnter);
                Lab[i].MouseLeave += new EventHandler(label_MouseLeave);
                panels[i].Controls.Add(Lab[i]);
            }
        }
    }
}
