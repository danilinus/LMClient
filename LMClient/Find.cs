using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Client
{
    public partial class Find : Form
    {
        public string dir;
        StreamReader FE;
        List<int> ASin = new List<int>();

        public List<Label> Lab = new List<Label>();
        public List<Panel> panels = new List<Panel>();

        public InfoClient IC = new InfoClient();

        public string[] tagsS;

        int m, i;
        bool b;

        public Find()
        {
            InitializeComponent();
        }

        public void DrawExel()
        {
            if (tags.Text != "")
                if (tags.Text != " ")
                {
                    Pot.Enabled = false;
                    ASin.Clear();
                    tagsS = tags.Text.Split(',');
                    if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                        dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
                    m = new DirectoryInfo(dir).GetDirectories().Length;



                    for (int i = 0; i < Lab.Count; i++)
                    {
                        Lab[i].Dispose();
                        panels[i].Dispose();
                    }
                    panels.Clear();
                    Lab.Clear();

                    if (File.Exists(Application.StartupPath + @"\BBD.dlb"))
                        dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
                    m = new DirectoryInfo(dir).GetDirectories().Length;

                    i = 0;
                    Pot.Enabled = true;
                }
        }

        public void DrawPanel(int i)
        {
            panels.Add(new Panel());
            if(i != 0)
                panels[i].Location = new Point(3, panels[0].Location.Y - 3 + i * 12 + (i * 12) + 2 * i + 3 * i + 3);
            else
                panels[i].Location = new Point(3, i * 12 + (i * 12) + 2 * i + 3 * i + 3);
            panels[i].Size = new Size(panelX.Size.Width - 26, 27);
            panels[i].BackColor = Color.FromArgb(192, 255, 192);
            if (File.Exists(dir + @"\" + i + @"\AVL.dbm"))
                panels[i].BackColor = Color.FromArgb(255, 191, 191);
            panels[i].BorderStyle = BorderStyle.FixedSingle;
            panelX.Controls.Add(panels[i]);

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

        public void PanelsSize()
        {
            for (int i = 0; i < panels.Count; i++)
                panels[i].Size = new Size(panelX.Size.Width - 26, 27);
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

        Size sl;

        private void Find_ResizeEnd(object sender, EventArgs e)
        {
            if (sl != Size)
            {
                panelX.Size = new Size(Size.Width - 36, Size.Height - 90);
                PanelsSize();
            }
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            DrawExel();
        }

        private void Pot_Tick(object sender, EventArgs e)
        {
            if (i >= m)
            {
                Loading.Size = new Size(0, 2);
                Pot.Enabled = false;
            }
            if(tagsS.Length > 1)
            for (int h = 0; h < ASin.Count; h++)
            {
                if (ASin[h] == i)
                { i++; break; }
            }
            if (i < m)
            {
                for (int l = 0; l < tagsS.Length; l++)
                {
                    FE = new StreamReader(dir + @"\" + i + @"\DL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\AL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\DEL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\KL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\NaL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
               
                    FE = new StreamReader(dir + @"\" + i + @"\NoL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\NSL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\SL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                
                    FE = new StreamReader(dir + @"\" + i + @"\TL.dbm");

                    if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                    {
                        FE.Close();
                        for (int k = 0; k < ASin.Count; k++)
                            if (ASin[k] == i)
                                b = true;
                        if (!b)
                        {
                            ASin.Add(i);
                            DrawPanel(ASin.Count - 1);
                            break;
                        }
                        b = false;
                    }
                    FE.Close();
                }

                if (File.Exists(dir + @"\" + i + @"\ZL.dbm"))
                {
                    for (int l = 0; l < tagsS.Length; l++)
                    {
                        FE = new StreamReader(dir + @"\" + i + @"\ZL.dbm");

                        if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                        {
                            FE.Close();
                            for (int k = 0; k < ASin.Count; k++)
                                if (ASin[k] == i)
                                    b = true;
                            if (!b)
                            {
                                ASin.Add(i);
                                DrawPanel(ASin.Count - 1);
                            }
                            b = false;
                        }
                        FE.Close();
                    }
                }

                if (File.Exists(dir + @"\" + i + @"\AVL.dbm"))
                {
                    for (int l = 0; l < tagsS.Length; l++)
                    {
                        FE = new StreamReader(dir + @"\" + i + @"\AVL.dbm");

                        if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                        {
                            FE.Close();
                            for (int k = 0; k < ASin.Count; k++)
                                if (ASin[k] == i)
                                    b = true;
                            if (!b)
                            {
                                ASin.Add(i);
                                DrawPanel(ASin.Count - 1);
                                break;
                            }
                            b = false;
                        }
                        FE.Close();
                    }
                }

                if (File.Exists(dir + @"\" + i + @"\Num.dbm"))
                {
                    for (int l = 0; l < tagsS.Length; l++)
                    {
                        FE = new StreamReader(dir + @"\" + i + @"\Num.dbm");
                        if (FE.ReadToEnd().ToLower().Contains(tagsS[l].ToLower()))
                        {
                            FE.Close();
                            for (int k = 0; k < ASin.Count; k++)
                                if (ASin[k] == i)
                                    b = true;
                            if (!b)
                            {
                                ASin.Add(i);
                                DrawPanel(ASin.Count - 1);
                                break;
                            }
                            b = false;
                        }
                        FE.Close();
                    }

                }
                i++;
                Loading.Size = new Size((int)((Size.Width - 5) * ((double)i / m)), 2);
            }
        }

        private void Find_Load(object sender, EventArgs e)
        {
            Loading.Size = new Size(0, 2);
        }

        private void Find_ResizeBegin(object sender, EventArgs e)
        {
            sl = Size;
        }
    }
}
