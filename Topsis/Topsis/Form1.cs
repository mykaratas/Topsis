using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace Topsis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.BorderStyle = BorderStyle.FixedSingle;
        }
        public static ArrayList kriterlist = new ArrayList();
        public static ArrayList seceneklist = new ArrayList();
        

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                label5.Text = "";
                kriterlist.Add(textBox1.Text.ToString());
              
                for (int i = 0; i < kriterlist.Count; i++)
                {
                    label5.Text += kriterlist[i].ToString() + "\n";
                }
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                label6.Text = "";
                seceneklist.Add(textBox2.Text);
                for (int i = 0; i < seceneklist.Count; i++)
                {
                    label6.Text += seceneklist[i].ToString() + "\n";
                }
                textBox2.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kriter_matrisi krtr_mtrs = new kriter_matrisi();
            krtr_mtrs.kritersayisi = kriterlist.Count;
            krtr_mtrs.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
