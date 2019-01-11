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
    public partial class kriter_secenek_matrisi : Form
    {
        public kriter_secenek_matrisi()
        {
            InitializeComponent();
        }
        public int kritersayisi, seceneksayisi;
        public static ArrayList kriter_secenek_verileri = new ArrayList();

        private void kriter_secenek_matrisi_Load(object sender, EventArgs e)
        {
            this.Height = (seceneksayisi * 60) + 200;
            this.Width = (kritersayisi * 60) + 150;
            int x = 100, y = 50, z = 1;

            for (int i = 0; i < kritersayisi; i++)
            {
                Label lbl = new Label();
                lbl.Width = 60;
                lbl.Height = 50;
                lbl.BackColor = Color.Khaki;
                lbl.ForeColor = Color.Black;
                lbl.Font = new Font("Microsoft Sans Serift", 12);
                lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                lbl.Text = Form1.kriterlist[i].ToString();
                lbl.Location = new Point(x, y);
                x += 60;
                this.Controls.Add(lbl);
            }
            x = 30;
            y = 100;
            for (int i = 0; i < seceneksayisi; i++)
            {
                for (int j = 0; j <= kritersayisi; j++)
                {
                    if (j == 0)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.Black;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Text = Form1.seceneklist[i].ToString();
                        lbl.Location = new Point(x, y);
                        this.Controls.Add(lbl);
                        x += 70;
                    }
                    else
                    {
                        TextBox txt = new TextBox();
                        txt.Width = 60;
                        txt.Height = 50;
                        txt.Name = "TextBox" + z++;
                        txt.ForeColor = Color.Black;
                        txt.Font = new Font("Microsoft Sans Serift", 12);
                        txt.Location = new Point(x, y);
                        x += 60;
                        this.Controls.Add(txt);
                    }
                    if (j == kritersayisi)
                    {
                        y += 50;
                        x = 30;
                    }
                }
            }
            Button btncozum = new Button();

            int genislik = (kritersayisi * 50) / 2;
            int yuseklik = (seceneksayisi * 50 + 100);
            btncozum.Height = 30;
            btncozum.Width = 150;
            btncozum.Text = "HESAPLA";
            btncozum.Location = new Point(genislik + 10, yuseklik);
            x += 50;
            btncozum.Click += new EventHandler(buton1);
            this.Controls.Add(btncozum);
        }
        private void buton1(object sender, EventArgs e)
        {
            kriter_secenek_verileri.Clear();
            
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is TextBox)
                {
                    kriter_secenek_verileri.Add(this.Controls[i].Text);
                }
            }
            sonuclar snc = new sonuclar();
            snc.Show();
        }
    }
}
