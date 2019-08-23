using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace Client
{
    static class Program
    {
        public static string login, pass;

        [STAThread]
        static void Main()
        {
            if (File.Exists(Application.StartupPath + @"\log.dlb"))
            {
                StreamReader ardsr = new StreamReader(Application.StartupPath + @"\log.dlb");
                login = ardsr.ReadLine();
                ardsr.Close();
            }

            if (File.Exists(Application.StartupPath + @"\pass.dlb"))
            {
                StreamReader ardsr = new StreamReader(Application.StartupPath + @"\pass.dlb");
                pass = ardsr.ReadLine();
                ardsr.Close();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
