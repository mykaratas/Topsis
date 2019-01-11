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
    public partial class kriter_matrisi : Form
    {
        public kriter_matrisi()
        {
            InitializeComponent();
        }
        public  int kritersayisi;
        public static ArrayList kriter_matrisi_verileri = new ArrayList();
        public static ArrayList kriter_matrisi_verileri_virgullu = new ArrayList();


        private void kriter_matrisi_Load(object sender, EventArgs e)
        {
            this.Height = (kritersayisi * 60) + 200;
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
            y +=50 ;
            for (int i = 0; i < kritersayisi; i++)
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
                        lbl.Text = Form1.kriterlist[i].ToString();
                        lbl.Location = new Point(x, y);
                        x += 70;
                        this.Controls.Add(lbl);
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
            int yuseklik = (kritersayisi * 50 + 100);
            btncozum.Height = 30;
            btncozum.Width = 150;
            btncozum.Text = "TOPSİS İLE HESAPLA";
            btncozum.Location = new Point(genislik + 10, yuseklik);
            x += 50;
            btncozum.Click += new EventHandler(buton1);
            this.Controls.Add(btncozum);
        }

        private void buton1(object sender, EventArgs e)
        {
            kriter_matrisi_verileri_virgullu.Clear();
            kriter_matrisi_verileri.Clear();
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is TextBox)
                {
                    kriter_matrisi_verileri.Add(this.Controls[i].Text);
                }
            }
            
            string a,b,c,d;
            for (int i = 0; i < kriter_matrisi_verileri.Count; i++)
            {
                if (kriter_matrisi_verileri[i].ToString().Length > 2)
                {
                    a = kriter_matrisi_verileri[i].ToString();
                    b = a[0].ToString();
                    c = a[2].ToString();
                    d = ((double)(Convert.ToDouble(b)) / (double)(Convert.ToDouble(c))).ToString();
                }
                else
                {
                    d = kriter_matrisi_verileri[i].ToString();
                }

                kriter_matrisi_verileri_virgullu.Add(d.ToString());
              
            }

          
            kriter_secenek_matrisi krtr_scnk_mtrs = new kriter_secenek_matrisi();
            krtr_scnk_mtrs.kritersayisi = Form1.kriterlist.Count;
            krtr_scnk_mtrs.seceneksayisi = Form1.seceneklist.Count;
            krtr_scnk_mtrs.Show();
          
        }
    }
}
