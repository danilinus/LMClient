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
    public partial class FindError : Form
    {
        public int O, m, s;
        string dir;

        public List<string> A = new List<string>();
        public List<string> B = new List<string>();
        public List<string> E = new List<string>();

        Random rnd = new Random();

        bool find = false;

        public FindError()
        {
            InitializeComponent();
        }

        private void FindError_Load(object sender, EventArgs e)
        {
            dir = new StreamReader(new FileStream(Application.StartupPath + @"\BBD.dlb", FileMode.Open, FileAccess.Read)).ReadToEnd();
            m = new DirectoryInfo(dir).GetDirectories().Length;
            //Start.Enabled = true;
        }

        private void Start_Tick(object sender, EventArgs e)
        {
            if (O == 6)
            {
                if (checkBox1.CheckState == CheckState.Checked && find && m > 0)
                {
                    if (!Directory.Exists(dir + @"\" + s))
                        for (int j = s; j < m - 1; j++)
                            Directory.Move(dir + @"\" + new DirectoryInfo(dir).GetDirectories()[j].Name, dir + @"\" + j);

                    s++;
                    if (s >= m)
                    {
                        s = 0;
                        O++;
                    }
                }
                else
                    O++;
            }

            if (O == 0)
            {
                m = new DirectoryInfo(dir).GetDirectories().Length;
                if (checkBox1.CheckState == CheckState.Checked && m > 0)
                {
                    if (!Directory.Exists(dir + @"\" + s))
                    {
                        find = true;
                        s = m;
                    }
                    s++;
                    if (s >= m)
                    {
                        s = 0;
                        O++;
                    }
                }
                else
                    O++;
                
            }

            if (O == 1)
            {
                if (checkBox1.CheckState == CheckState.Checked && find && m > 0)
                {
                    m = new DirectoryInfo(dir).GetDirectories().Length;
                    if (Directory.Exists(dir + @"\" + "Error"))
                    {
                        m = new DirectoryInfo(dir + @"\Error").GetDirectories().Length;
                        O++;
                    }
                    else
                    {
                        Directory.CreateDirectory(dir + @"\Error");
                        O++;
                        O++;
                    }
                }
                else
                    O++;
            }

            if (O == 2)
            {
                if (checkBox1.CheckState == CheckState.Checked && find && m > 0)
                {
                    Directory.Move(dir + @"\Error\" + new DirectoryInfo(dir + @"\Error").GetDirectories()[0].Name, dir + @"\" + rnd.Next(1000, 999999));
                    s++;
                    if (s >= m)
                    {
                        O = 0;
                        s = 0;
                        Directory.Delete(dir + @"\Error");
                        m = new DirectoryInfo(dir).GetDirectories().Length;
                    }
                }
                else
                    O++;
            }

            if (O == 3)
            {
                if (checkBox1.CheckState == CheckState.Checked && find && m > 0)
                {
                    if (dir + @"\" + new DirectoryInfo(dir).GetDirectories()[0].Name != "Error")
                    {
                        try
                        {
                            Directory.Move(dir + @"\" + new DirectoryInfo(dir).GetDirectories()[0].Name, dir + @"\Error\0" + s);
                        }
                        catch
                        {
                            O = 0;
                        }
                    }
                    s++;
                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }
                }
                else
                    O++;
            }


            if (O == 4)
            {
                if (checkBox1.CheckState == CheckState.Checked && find && m > 0)
                {
                    try
                    {
                        Directory.Move(dir + @"\Error\" + new DirectoryInfo(dir + @"\Error").GetDirectories()[0].Name, dir + @"\" + s);
                    }
                    catch
                    {
                        O = 0;
                    }
                    s++;
                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }
                }
                else
                    O++;
            }

            if (O == 5)
            {
                if (checkBox1.CheckState == CheckState.Checked && find && m > 0)
                {
                    try
                    {
                        Directory.Delete(dir + @"\Error");
                    }
                    catch
                    {
                        O = 0;
                    }
                    O++;
                }
                else
                    O++;
            }

            if (O == 7)
            {
                if (checkBox2.CheckState == CheckState.Checked && m > 0)
                {
                    if (!File.Exists(dir + @"\" + s + @"\" + "AL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\AL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "TL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\TL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "NoL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\NoL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "NaL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\NaL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "NSL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\NSL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "DEL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\DEL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "SL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\SL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\" + "DL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\DL.dbm");
                        ardsr.Close();
                    }

                    if (!File.Exists(dir + @"\" + s + @"\KL.dbm"))
                    {
                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\KL.dbm");
                        ardsr.Close();
                    }

                    s++;

                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }
                }
                else
                    O++;
            }

            if (O == 8)
            {
                if (checkBox4.CheckState == CheckState.Checked && m > 0)
                {
                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }

                    if (!File.Exists(dir + @"\" + s + @"\AVL.dbm"))
                    {
                        A = ((Form1)Owner).A;
                        B = ((Form1)Owner).B;
                        E = ((Form1)Owner).E;
                        StreamReader ards = new StreamReader(dir + @"\" + s + @"\AL.dbm");
                        string js = ards.ReadLine();
                        ards.Close();

                        for (int i = 0; i < A.Count; i++)
                            if (E[i] != "")
                                if (A[i] == js || B[i] == js)
                                {
                                    File.WriteAllText(dir + @"\" + s + @"\AVL.dbm", E[i]);
                                    break;
                                }
                    }
                    s++;

                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }
                }
                else
                    O++;
            }

            if (O == 9)
            {
                if (checkBox4.CheckState == CheckState.Checked && m > 0)
                {
                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }

                    if (File.Exists(dir + @"\" + s + @"\AVL.dbm"))
                    {
                        StreamReader ards = new StreamReader(dir + @"\" + s + @"\AVL.dbm");
                        string js = ards.ReadLine();
                        ards.Close();
                        if (js == " " || js == "")
                            File.Delete(dir + @"\" + s + @"\AVL.dbm");
                    }

                    s++;
                }
                else
                {
                    O++;
                    s = 0;
                }
            }


            if (O == 10)
            {
                if (checkBox3.CheckState == CheckState.Checked && m > 0)
                {
                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }

                    StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\Num.dbm");
                    ardsr.WriteLine("№" + rnd.Next(100, 9999));
                    ardsr.Close();

                    s++;
                }
                else
                {
                    s = 0;
                    O++;
                }
            }

            if (O == 11)
            {
                if (checkBox5.CheckState == CheckState.Checked && m > 0)
                {
                    if (File.Exists(dir + @"\" + s + @"\NoL.dbm"))
                    {
                        StreamReader ards = new StreamReader(dir + @"\" + s + @"\NoL.dbm");
                        string sus = ards.ReadLine();
                        ards.Close();

                        if (sus[0] == '+')
                            sus = sus.Remove(0, 1); else
                        if(sus[0] == '8')
                        {
                            sus = sus.Remove(0, 1);
                            sus = sus.Insert(0, "7");
                        }

                        StreamWriter ardsr = new StreamWriter(dir + @"\" + s + @"\NoL.dbm");
                        ardsr.WriteLine(sus);
                        ardsr.Close();
                    }

                    s++;
                    if (s >= m)
                    {
                        O++;
                        s = 0;
                    }
                }
                else
                    O++;
            }

            progressBar1.Value = O;
            progressBar2.Maximum = m;
            progressBar2.Value = s;
            label1.Text = ((int)((s / (double)m) * 100f)).ToString() + "%";
            label2.Text = O + " из " + progressBar1.Maximum;

            if (O == 12)
            {
                s = progressBar2.Maximum;
                progressBar2.Value = progressBar2.Maximum;
                s = 0;
                O = 0;
                m = 0;
                progressBar1.Value = progressBar1.Maximum;
                progressBar2.Value = progressBar2.Maximum;
                label1.Text = "Всё готово";
                label2.Text = "Произведите перезапуск";
                ((Form1)Owner).DrawExeaaal();
                Start.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            Start.Enabled = true;
            Owner.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.CheckState = CheckState.Checked;
            checkBox2.CheckState = CheckState.Checked;
            checkBox4.CheckState = CheckState.Checked;
            checkBox5.CheckState = CheckState.Checked;
            groupBox1.Enabled = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
            Application.Restart();
        }
    }
}
