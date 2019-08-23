using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeroyMerlinClient
{
    public partial class LoginIn : Window
    {
        public LoginIn()
        {
            InitializeComponent();
            Owner = Win.mainWindow;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox1.Text != " "
                && textBox2.Text != " ")
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("Mes.mf", FileMode.Create))
                {
                    MessageSer a = new MessageSer();
                    a.nns = textBox1.Text + " (Магазин: " + textBox2.Text + ")";
                    formatter.Serialize(fs, a);
                }
                System.Diagnostics.Process.Start("shari.exe");
                Close();
            }
        }
    }
}
