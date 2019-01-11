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
    public partial class sonuclar : Form
    {
        public ArrayList kriter_matrisi_sutun_toplam = new ArrayList();
        public ArrayList kriter_matrisinin_sutun_toplama_bolumu = new ArrayList();
        public ArrayList kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması = new ArrayList();
        public ArrayList karar_matrisinin_kareleri = new ArrayList();
        public ArrayList karar_matrisinin_karelerinin_sutun_toplamlari = new ArrayList();
        public ArrayList karar_matrislerinin_sutun_toplamina_bolumu = new ArrayList();
        public ArrayList agirliklandirilmis_normalize_matris = new ArrayList();
        public ArrayList ideal_çozum_matrisi = new ArrayList();
        public ArrayList negatif_ideal_çozum_matrisi = new ArrayList();
        public ArrayList ideal_uzakliklar = new ArrayList();
        public ArrayList ideal_uzakliklar_toplam = new ArrayList();
        public ArrayList ideal_uzakliklar_S = new ArrayList();
        public ArrayList negatif_ideal_uzakliklar = new ArrayList();
        public ArrayList negatif_ideal_uzakliklar_toplam = new ArrayList();
        public ArrayList negatif_ideal_uzakliklar_s_eksi = new ArrayList();
        public ArrayList c_degeri = new ArrayList();
        public ArrayList kriter_satir_ortalamasi_carpi_c_degeri = new ArrayList();
        double en_iyi = 0;



        public sonuclar()
        {
            InitializeComponent();
            kriter_matrisi_sutun_toplam.Clear();
            kriter_matrisinin_sutun_toplama_bolumu.Clear();
            kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması.Clear();
            karar_matrisinin_kareleri.Clear();
            karar_matrisinin_karelerinin_sutun_toplamlari.Clear();
            karar_matrislerinin_sutun_toplamina_bolumu.Clear();
            agirliklandirilmis_normalize_matris.Clear();
            ideal_çozum_matrisi.Clear();
            negatif_ideal_çozum_matrisi.Clear();
            ideal_uzakliklar.Clear();
            ideal_uzakliklar_toplam.Clear();
            ideal_uzakliklar_S.Clear();
            negatif_ideal_uzakliklar.Clear();
            negatif_ideal_uzakliklar_toplam.Clear();
            negatif_ideal_uzakliklar_s_eksi.Clear();
            c_degeri.Clear();
            kriter_satir_ortalamasi_carpi_c_degeri.Clear();
            en_iyi = 0;



            //KRİTER MATRİSİ İŞLEMLERİ
            double b = 0;
            for (int i = 0; i < Form1.kriterlist.Count; i++)
            {
                for (int j = i; j < kriter_matrisi.kriter_matrisi_verileri_virgullu.Count; j += Form1.kriterlist.Count)
                {
                    b += Convert.ToDouble(kriter_matrisi.kriter_matrisi_verileri_virgullu[j]);
                    // kriter matrisinde girilen kriterlerin sütun toplamlarını aldık
                }
                kriter_matrisi_sutun_toplam.Add(b.ToString());
                b = 0;
            }

            int dongu = 0;
            for (int i = 0; i < kriter_matrisi.kriter_matrisi_verileri_virgullu.Count; i++)
            {
                kriter_matrisinin_sutun_toplama_bolumu.Add((Convert.ToDouble(kriter_matrisi.kriter_matrisi_verileri_virgullu[i]) / Convert.ToDouble(kriter_matrisi_sutun_toplam[dongu])).ToString());
                //her kriter hücresini sutun toplamına böldük.
                dongu++;
                if (dongu == kriter_matrisi_sutun_toplam.Count)
                {
                    dongu = 0;
                }
            }
            //kriter matrisinin sütun toplamına bölünmüş halinin satır toplamlarını bulduk
            double sonuc = 0;
            int sayac = 0;
            for (int i = 0; i < kriter_matrisinin_sutun_toplama_bolumu.Count; i++)
            {
                sonuc += Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu[i]);
                sayac++;
                if (sayac == Form1.kriterlist.Count)
                {
                    kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması.Add(((double)sonuc / (double)Form1.kriterlist.Count).ToString());
                    sayac = 0;
                    sonuc = 0;
                }
            }

            //KARAR MATRİSİ İŞLEMLERİ

            for (int i = 0; i < kriter_secenek_matrisi.kriter_secenek_verileri.Count; i++)
            {
                double a = 0;   //karelerini al
                a = Convert.ToDouble(kriter_secenek_matrisi.kriter_secenek_verileri[i]) * Convert.ToDouble(kriter_secenek_matrisi.kriter_secenek_verileri[i]);
                karar_matrisinin_kareleri.Add(a.ToString());
            }

            double bb = 0;
            sonuc = 0;
            for (int i = 0; i < Form1.kriterlist.Count; i++)
            {
                for (int j = i; j < karar_matrisinin_kareleri.Count; j += Form1.kriterlist.Count)
                {
                    bb += Convert.ToDouble(karar_matrisinin_kareleri[j]);   // kareleri alınan sütunların toplamını al ve kökünü al
                }
                sonuc = Math.Sqrt(bb);
                karar_matrisinin_karelerinin_sutun_toplamlari.Add(sonuc.ToString());
                sonuc = 0;
                bb = 0;
            }

            int dongusayisi = 0;
            for (int i = 0; i < kriter_secenek_matrisi.kriter_secenek_verileri.Count; i++)
            {
                double c = 0;
                c = Convert.ToDouble(kriter_secenek_matrisi.kriter_secenek_verileri[i]) / Convert.ToDouble(karar_matrisinin_karelerinin_sutun_toplamlari[dongusayisi]);
                //ilk verilen değerleri kökü alınan sutun toplamlarına  böl.
                karar_matrislerinin_sutun_toplamina_bolumu.Add(c.ToString());
                dongusayisi++;
                if (dongusayisi == karar_matrisinin_karelerinin_sutun_toplamlari.Count)
                {
                    dongusayisi = 0;
                }
            }
            dongusayisi = 0;

            for (int i = 0; i < karar_matrislerinin_sutun_toplamina_bolumu.Count; i++)
            {
                double c = 0;
                double x = Convert.ToDouble(karar_matrislerinin_sutun_toplamina_bolumu[i].ToString());
                double y = Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması[dongusayisi].ToString());
                c = (double)x * (double)y;
                //ağırlıklandırılmış normalize matris
                agirliklandirilmis_normalize_matris.Add(c.ToString());
                dongusayisi++;
                if (dongusayisi == kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması.Count)
                {
                    dongusayisi = 0;
                }
            }


            //İDEAL VE NEGATİF İDEAL ÇÖZÜM MATRİSLERİNİ BULDUK
            for (int i = 0; i < Form1.kriterlist.Count; i++)
            {
                double enkucuk = Convert.ToDouble(agirliklandirilmis_normalize_matris[i]);
                double enbuyuk = Convert.ToDouble(agirliklandirilmis_normalize_matris[i]);

                for (int j = Form1.kriterlist.Count + i; j < agirliklandirilmis_normalize_matris.Count; j += Form1.kriterlist.Count)
                {
                    if (Convert.ToDouble(agirliklandirilmis_normalize_matris[j]) < enkucuk)
                    {
                        enkucuk = Convert.ToDouble(agirliklandirilmis_normalize_matris[j]);
                    }
                    if (Convert.ToDouble(agirliklandirilmis_normalize_matris[j]) > enbuyuk)
                    {
                        enbuyuk = Convert.ToDouble(agirliklandirilmis_normalize_matris[j]);
                    }
                }
                negatif_ideal_çozum_matrisi.Add(enkucuk.ToString());
                ideal_çozum_matrisi.Add(enbuyuk.ToString());
                enbuyuk = 0;
                enkucuk = 0;
            }

            //İdeal ve ideal olmayan noktalara olan Uzaklıkları bulduk
            dongu = 0;
            for (int i = 0; i < agirliklandirilmis_normalize_matris.Count; i++)
            {
                double a = ((double)Convert.ToDouble(agirliklandirilmis_normalize_matris[i]) - (double)Convert.ToDouble(ideal_çozum_matrisi[dongu])) * ((double)Convert.ToDouble(agirliklandirilmis_normalize_matris[i]) - (double)Convert.ToDouble(ideal_çozum_matrisi[dongu]));
                ideal_uzakliklar.Add(a.ToString());
                dongu++;
                if (dongu == ideal_çozum_matrisi.Count)
                {
                    dongu = 0;
                }
            }

            //ideal uzaklık tablosunda satır toplamlarını bulduk
            //ideal uzaklık tablosunda satır toplamlarının karekökünü aldık

            sonuc = 0;
            sayac = 0;
            for (int i = 0; i < ideal_uzakliklar.Count; i++)
            {
                sonuc += (double)Convert.ToDouble(ideal_uzakliklar[i]);
                sayac++;
                if (sayac == Form1.kriterlist.Count)
                {
                    ideal_uzakliklar_toplam.Add(sonuc.ToString());
                    ideal_uzakliklar_S.Add((Math.Sqrt(sonuc)).ToString());
                    sayac = 0;
                    sonuc = 0;
                }
            }

            //Negatif İdeal Uzaklıkları bulduk
            dongu = 0;
            for (int i = 0; i < agirliklandirilmis_normalize_matris.Count; i++)
            {
                double a = ((double)Convert.ToDouble(agirliklandirilmis_normalize_matris[i]) - (double)Convert.ToDouble(negatif_ideal_çozum_matrisi[dongu])) * ((double)Convert.ToDouble(agirliklandirilmis_normalize_matris[i]) - (double)Convert.ToDouble(negatif_ideal_çozum_matrisi[dongu]));
                negatif_ideal_uzakliklar.Add(a.ToString());
                dongu++;
                if (dongu == negatif_ideal_çozum_matrisi.Count)
                {
                    dongu = 0;
                }
            }

            //negatif ideal uzaklık tablosunda satır toplamlarını bulduk
            //negatif ideal uzaklık tablosunda satır toplamlarını karekökünü aldık

            sonuc = 0;
            sayac = 0;
            for (int i = 0; i < negatif_ideal_uzakliklar.Count; i++)
            {
                sonuc += (double)Convert.ToDouble(negatif_ideal_uzakliklar[i]);
                sayac++;
                if (sayac == Form1.kriterlist.Count)
                {
                    negatif_ideal_uzakliklar_toplam.Add(sonuc.ToString());
                    negatif_ideal_uzakliklar_s_eksi.Add((Math.Sqrt(sonuc)).ToString());
                    sayac = 0;
                    sonuc = 0;
                }
            }
            //c değerini hesapladık.
            sonuc = 0;
            for (int i = 0; i < negatif_ideal_uzakliklar_s_eksi.Count; i++)
            {
                double e = Convert.ToDouble(negatif_ideal_uzakliklar_s_eksi[i]);
                double a = Convert.ToDouble(ideal_uzakliklar_S[i]);
                sonuc = (double)e / ((double)e + (double)a);
                c_degeri.Add(sonuc.ToString());
                e = 0;
                a = 0;
                sonuc = 0;
            }
            sonuc = 0;
            double toplam = 0;
            for (int i = 0; i < c_degeri.Count; i++)
            {
                for (int j = 0; j < Form1.kriterlist.Count; j++)
                {
                    toplam += (double)Convert.ToDouble(c_degeri[i]) * (double)Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması[j]);
                }
                sonuc = (double)toplam / (double)Form1.kriterlist.Count;
                kriter_satir_ortalamasi_carpi_c_degeri.Add(sonuc.ToString());
                toplam = 0;
                sonuc = 0;
            }

            double enbyk = Convert.ToDouble(kriter_satir_ortalamasi_carpi_c_degeri[0].ToString());
            int kisi = 0;
            for (int i = 1; i < kriter_satir_ortalamasi_carpi_c_degeri.Count; i++)
            {
                if (Convert.ToDouble(kriter_satir_ortalamasi_carpi_c_degeri[i].ToString()) > enbyk)
                {
                    enbyk = Convert.ToDouble(kriter_satir_ortalamasi_carpi_c_degeri[i].ToString());
                    kisi = i;
                }
            }
            label3.Text = Form1.seceneklist[kisi].ToString();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = 150;
            int y = 100;
            int z = 0;
            if (comboBox1.Text == "Kriter Matrisi")
            {
                groupBox1.Height = (Form1.kriterlist.Count * 50) + 300;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 300;
                this.Height = (Form1.kriterlist.Count * 50) + 380;
                this.Width = (Form1.kriterlist.Count * 60) + 300;
                groupBox1.Controls.Clear();

                for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                    groupBox1.Controls.Add(lbl);

                }
                x = 80;
                y += 50;
                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Height = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Text = Form1.kriterlist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 70;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisi.kriter_matrisi_verileri_virgullu[z]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                        if (j == Form1.kriterlist.Count)
                        {
                            y += 50;
                            x = 80;
                        }
                    }
                }
                z = 0;
                x = 60;
                for (int i = 0; i <= kriter_matrisi_sutun_toplam.Count; i++)
                {
                    if (i == 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.Width = 80;
                        lbl1.Height = 50;
                        lbl1.Text = "Sütun Toplam";
                        lbl1.BackColor = Color.Khaki;
                        lbl1.ForeColor = Color.DarkRed;
                        lbl1.Font = new Font("Microsoft Sans Serift", 12);
                        lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                        lbl1.Location = new Point(x, y);
                        x += 90;
                        groupBox1.Controls.Add(lbl1);
                    }
                    else
                    {
                        Label lbl2 = new Label();
                        lbl2.Width = 60;
                        lbl2.Height = 50;
                        lbl2.BackColor = Color.Khaki;
                        lbl2.ForeColor = Color.DarkRed;
                        lbl2.Font = new Font("Microsoft Sans Serift", 12);
                        lbl2.BorderStyle = BorderStyle.FixedSingle;
                        lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisi_sutun_toplam[z]), 3).ToString();
                        lbl2.Location = new Point(x, y);
                        x += 60;
                        z++;
                        groupBox1.Controls.Add(lbl2);
                    }
                }
            }
            else if (comboBox1.Text == "Normalize Kriter Matrisi")
            {
                groupBox1.Height = (Form1.kriterlist.Count * 50) + 250;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 360;
                this.Height = (Form1.kriterlist.Count * 50) + 330;
                this.Width = (Form1.kriterlist.Count * 60) + 360;
                z = 0;
                x = 150;
                y = 100;
                groupBox1.Controls.Clear();
                for (int i = 0; i <= Form1.kriterlist.Count; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 100;
                        lbl.Height = 50;
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Text = "Satır Ortalaması";
                        lbl.Location = new Point(x, y);
                        x += 80;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                x = 90;
                y += 50;
                int c = 0;

                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count + 1; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Height = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.kriterlist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            if (j == Form1.kriterlist.Count + 1)
                            {
                                Label lbl2 = new Label();
                                lbl2.Width = 60;
                                lbl2.Height = 50;
                                lbl2.BackColor = Color.Khaki;
                                lbl2.ForeColor = Color.DarkRed;
                                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                                lbl2.BorderStyle = BorderStyle.FixedSingle;
                                lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması[c]), 3).ToString();
                                lbl2.Location = new Point(x, y);
                                x += 60;
                                groupBox1.Controls.Add(lbl2);
                                c++;
                            }
                            else
                            {
                                Label lbl2 = new Label();
                                lbl2.Width = 60;
                                lbl2.Height = 50;
                                lbl2.BackColor = Color.Khaki;
                                lbl2.ForeColor = Color.Black;
                                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                                lbl2.BorderStyle = BorderStyle.FixedSingle;
                                lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu[z]), 3).ToString();
                                lbl2.Location = new Point(x, y);
                                x += 60;
                                groupBox1.Controls.Add(lbl2);
                                z++;
                            }
                        }
                        if (j == Form1.kriterlist.Count + 1)
                        {
                            y += 50;
                            x = 90;
                        }
                    }
                }
            }
            else if (comboBox1.Text == "Karar Matrisi")
            {
                groupBox1.Height = (Form1.seceneklist.Count * 50) + 200;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 300;
                this.Height = (Form1.seceneklist.Count * 50) + 280;
                this.Width = (Form1.kriterlist.Count * 60) + 300;
                x = 150;
                y = 100;
                z = 0;
                groupBox1.Controls.Clear();
                for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                    groupBox1.Controls.Add(lbl);
                }
                x = 90;
                y += 50;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = kriter_secenek_matrisi.kriter_secenek_verileri[z].ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                        if (j == Form1.kriterlist.Count)
                        {
                            y += 50;
                            x = 90;
                        }
                    }
                }
            }
            else if (comboBox1.Text == "Normalize Karar Matrisi")
            {
                this.Width = (Form1.kriterlist.Count * 60) + (Form1.kriterlist.Count * 60) + 480;
                this.Height = (Form1.seceneklist.Count * 50) + 430;
                groupBox1.Height = (Form1.seceneklist.Count * 50) + 350;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + (Form1.kriterlist.Count * 60) + 480;
                x = 150;
                y = 100;
                z = 0;
                groupBox1.Controls.Clear();
                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    Label lbl = new Label();
                    lbl.Width = 60;
                    lbl.Width = 50;
                    lbl.BackColor = Color.Khaki;
                    lbl.ForeColor = Color.Black;
                    lbl.Font = new Font("Microsoft Sans Serift", 12);
                    lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                    lbl.Text = Form1.kriterlist[i].ToString();
                    lbl.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl);
                }
                x = 90;
                y += 50;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Convert.ToDouble(karar_matrisinin_kareleri[z]).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                        if (j == Form1.kriterlist.Count)
                        {
                            y += 50;
                            x = 90;
                        }
                    }
                }

                z = 0;
                x = 80;

                for (int i = 0; i <= karar_matrisinin_karelerinin_sutun_toplamlari.Count; i++)
                {
                    if (i == 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.Width = 70;
                        lbl1.Height = 50;
                        lbl1.BackColor = Color.Khaki;
                        lbl1.ForeColor = Color.DarkRed;
                        lbl1.Font = new Font("Microsoft Sans Serift", 12);
                        lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                        lbl1.Text = "Sütun Toplam";
                        lbl1.Location = new Point(x, y);
                        x += 70;
                        groupBox1.Controls.Add(lbl1);
                    }
                    else
                    {
                        Label lbl2 = new Label();
                        lbl2.Width = 60;
                        lbl2.Height = 50;
                        lbl2.BackColor = Color.Khaki;
                        lbl2.ForeColor = Color.DarkRed;
                        lbl2.Font = new Font("Microsoft Sans Serift", 12);
                        lbl2.BorderStyle = BorderStyle.FixedSingle;
                        lbl2.Text = Math.Round(Convert.ToDouble(karar_matrisinin_karelerinin_sutun_toplamlari[z]), 6).ToString();
                        lbl2.Location = new Point(x, y);
                        x += 60;
                        z++;
                        groupBox1.Controls.Add(lbl2);
                    }
                }

                x += 120;
                y = 100;
                z = 0;

                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    Label lbl = new Label();
                    lbl.Width = 60;
                    lbl.Width = 50;
                    lbl.BackColor = Color.Khaki;
                    lbl.ForeColor = Color.Black;
                    lbl.Font = new Font("Microsoft Sans Serift", 12);
                    lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                    lbl.Text = Form1.kriterlist[i].ToString();
                    lbl.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl);
                }

                x -= (Form1.kriterlist.Count * 60) + 60;
                int a = x;
                y += 50;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(karar_matrislerinin_sutun_toplamina_bolumu[z]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                        if (j == Form1.kriterlist.Count)
                        {
                            y += 50;
                            x = a;
                        }
                    }
                }
            }
            else if (comboBox1.Text == "Ağırlıklandırılmış Normalize Matris")
            {
                this.Width = (Form1.kriterlist.Count * 60) + 360;
                this.Height = (Form1.seceneklist.Count * 50) + 380;
                groupBox1.Height = (Form1.seceneklist.Count * 50) + 300;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 360;
                groupBox1.Controls.Clear();
                x = 80;
                y = 100;
                z = 0;
                Label lbl5 = new Label();
                lbl5.Width = 60;
                lbl5.Height = 50;
                lbl5.BackColor = Color.Khaki;
                lbl5.ForeColor = Color.DarkRed;
                lbl5.Font = new Font("Microsoft Sans Serift", 12);
                lbl5.Font = new Font(lbl5.Font, lbl5.Font.Style ^ FontStyle.Underline);
                lbl5.Text = "Ağırlıklar";
                lbl5.Location = new Point(x, y);
                groupBox1.Controls.Add(lbl5);
                x += 70;
                //AĞIRLIKLAR YAZILDI

                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    Label lbl1 = new Label();
                    lbl1.Width = 60;
                    lbl1.Height = 50;
                    lbl1.BackColor = Color.Khaki;
                    lbl1.ForeColor = Color.DarkRed;
                    lbl1.Font = new Font("Microsoft Sans Serift", 12);
                    lbl1.BorderStyle = BorderStyle.FixedSingle;
                    lbl1.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması[i]), 3).ToString();
                    lbl1.Location = new Point(x, y);
                    groupBox1.Controls.Add(lbl1);
                    x += 60;
                }
                x = 150;
                y += 50;
                //KRİTERLER YAZILDI
                for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                    groupBox1.Controls.Add(lbl);
                }
                x = 90;
                y += 50;
                z = 0;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(agirliklandirilmis_normalize_matris[z]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                        if (j == Form1.kriterlist.Count)
                        {
                            y += 50;
                            x = 90;
                        }
                    }
                }
            }
            else if (comboBox1.Text == "İdeal Ve Negatif İdeal Çözüm Matrisi")
            {
                this.Width = (Form1.kriterlist.Count * 60) + 300;
                this.Height = 630;
                groupBox1.Height = 550;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 300;
                groupBox1.Controls.Clear();
                x = 150;
                y = 100;
                z = 0;
                for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                    groupBox1.Controls.Add(lbl);
                }
                y += 50;
                x = 40;
                Label lbl1 = new Label();
                lbl1.Width = 110;
                lbl1.Height = 50;
                lbl1.BackColor = Color.Khaki;
                lbl1.ForeColor = Color.Black;
                lbl1.Font = new Font("Microsoft Sans Serift", 12);
                lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                lbl1.Text = "İdeal Çözüm Değeri";
                lbl1.Location = new Point(x, y);
                groupBox1.Controls.Add(lbl1);

                x = 150;
                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    Label lbl = new Label();
                    lbl.Width = 60;
                    lbl.Height = 50;
                    lbl.BackColor = Color.Khaki;
                    lbl.ForeColor = Color.Black;
                    lbl.Font = new Font("Microsoft Sans Serift", 12);
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Text = Math.Round(Convert.ToDouble(ideal_çozum_matrisi[i]), 3).ToString();
                    lbl.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl);
                }

                x = 150;
                y += 150;

                for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                    groupBox1.Controls.Add(lbl);
                }

                y += 50;
                x = 40;
                Label lbl2 = new Label();
                lbl2.Width = 110;
                lbl2.Height = 50;
                lbl2.BackColor = Color.Khaki;
                lbl2.ForeColor = Color.Black;
                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                lbl2.Font = new Font(lbl2.Font, lbl2.Font.Style ^ FontStyle.Underline);
                lbl2.Text = "Negatif İdeal Çözüm Değeri";
                lbl2.Location = new Point(x, y);
                groupBox1.Controls.Add(lbl2);

                x = 150;
                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    Label lbl = new Label();
                    lbl.Width = 60;
                    lbl.Height = 50;
                    lbl.BackColor = Color.Khaki;
                    lbl.ForeColor = Color.Black;
                    lbl.Font = new Font("Microsoft Sans Serift", 12);
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Text = Math.Round(Convert.ToDouble(negatif_ideal_çozum_matrisi[i]), 3).ToString();
                    lbl.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl);
                }
            }
            else if (comboBox1.Text == "İdeal Ve İdeal Olmayan Noktalara Olan Uzaklık")
            {
                this.Height = ((Form1.seceneklist.Count * 50)) + 330;
                groupBox1.Height = ((Form1.seceneklist.Count * 50)) + 250;
                this.Width = (((Form1.kriterlist.Count + 2) * 60) * 2) + 480;
                groupBox1.Width = (((Form1.kriterlist.Count + 2) * 60) * 2) + 480;
                groupBox1.Controls.Clear();
                x = 150;
                y = 100;
                z = 0;
                for (int i = 0; i < Form1.kriterlist.Count + 2; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Text = "Toplam";
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (i == Form1.kriterlist.Count + 1)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.Text = "S+";
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                int a = 0, b = 0;
                z = 0;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    y += 50;
                    x = 90;
                    for (int j = 0; j <= Form1.kriterlist.Count + 2; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else if (j == Form1.kriterlist.Count + 1)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(ideal_uzakliklar_toplam[a]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            a++;
                        }
                        else if (j == Form1.kriterlist.Count + 2)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(ideal_uzakliklar_S[b]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            b++;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(ideal_uzakliklar[z]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }

                    }
                }


                //-----------------------------------------------------------------------------------

                x = ((Form1.kriterlist.Count + 2) * 60) + 330;
                int c = x;
                y = 100;
                z = 0;
                for (int i = 0; i < Form1.kriterlist.Count + 2; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.Text = "Toplam";
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (i == Form1.kriterlist.Count + 1)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.Text = "S-";
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                a = 0; b = 0;
                z = 0;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    y += 50;
                    x = c - 60;
                    for (int j = 0; j <= Form1.kriterlist.Count + 2; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else if (j == Form1.kriterlist.Count + 1)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.Text = Math.Round(Convert.ToDouble(negatif_ideal_uzakliklar_toplam[a]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            a++;
                        }
                        else if (j == Form1.kriterlist.Count + 2)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(negatif_ideal_uzakliklar_s_eksi[b]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            b++;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(negatif_ideal_uzakliklar[z]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                    }
                }
            }
            else
            {

                this.Height = ((Form1.seceneklist.Count * 50)) + 330;
                groupBox1.Height = ((Form1.seceneklist.Count * 50)) + 250;
                this.Width = (3 * 60) + 300;
                groupBox1.Width = (3 * 60) + 300;

                groupBox1.Controls.Clear();
                x = 150;
                y = 100;
                Label lbl2 = new Label();
                lbl2.Width = 60;
                lbl2.Height = 50;
                lbl2.BackColor = Color.Khaki;
                lbl2.ForeColor = Color.Black;
                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                lbl2.Font = new Font(lbl2.Font, lbl2.Font.Style ^ FontStyle.Underline); lbl2.Text = "S+";
                lbl2.Location = new Point(x, y);
                x += 60;
                groupBox1.Controls.Add(lbl2);
                Label lbl1 = new Label();
                lbl1.Width = 60;
                lbl1.Height = 50;
                lbl1.BackColor = Color.Khaki;
                lbl1.ForeColor = Color.Black;
                lbl1.Font = new Font("Microsoft Sans Serift", 12);
                lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                lbl1.Text = "S-";
                lbl1.Location = new Point(x, y);
                x += 60;
                groupBox1.Controls.Add(lbl1);
                Label lbl3 = new Label();
                lbl3.Width = 60;
                lbl3.Height = 50;
                lbl3.BackColor = Color.Khaki;
                lbl3.ForeColor = Color.DarkRed;
                lbl3.Font = new Font("Microsoft Sans Serift", 12);
                lbl3.Font = new Font(lbl3.Font, lbl3.Font.Style ^ FontStyle.Underline);
                lbl3.Text = "C*";
                lbl3.Location = new Point(x, y);
                x += 60;
                groupBox1.Controls.Add(lbl3);
                x = 100;
                y += 50;
                z = 0;
                int k = 0, d = 0;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    Label lbl4 = new Label();
                    lbl4.Width = 60;
                    lbl4.Height = 50;
                    lbl4.BackColor = Color.Khaki;
                    lbl4.ForeColor = Color.Black;
                    lbl4.Font = new Font("Microsoft Sans Serift", 12);
                    lbl4.Font = new Font(lbl4.Font, lbl4.Font.Style ^ FontStyle.Underline);
                    lbl4.Text = Form1.seceneklist[i].ToString();
                    lbl4.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl4);

                    Label lbl5 = new Label();
                    lbl5.Width = 60;
                    lbl5.Height = 50;
                    lbl5.BackColor = Color.Khaki;
                    lbl5.ForeColor = Color.Black;
                    lbl5.Font = new Font("Microsoft Sans Serift", 12);
                    lbl5.BorderStyle = BorderStyle.FixedSingle;
                    lbl5.Text = ideal_uzakliklar_S[z].ToString();
                    lbl5.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl5);
                    z++;

                    Label lbl6 = new Label();
                    lbl6.Width = 60;
                    lbl6.Height = 50;
                    lbl6.BackColor = Color.Khaki;
                    lbl6.ForeColor = Color.Black;
                    lbl6.Font = new Font("Microsoft Sans Serift", 12);
                    lbl6.BorderStyle = BorderStyle.FixedSingle;
                    lbl6.Text = negatif_ideal_uzakliklar_s_eksi[k].ToString();
                    lbl6.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl6);
                    k++;


                    Label lbl7 = new Label();
                    lbl7.Width = 60;
                    lbl7.Height = 50;
                    lbl7.BackColor = Color.Khaki;
                    lbl7.ForeColor = Color.DarkRed;
                    lbl7.Font = new Font("Microsoft Sans Serift", 12);
                    lbl7.BorderStyle = BorderStyle.FixedSingle;
                    lbl7.Text = c_degeri[d].ToString();
                    lbl7.Location = new Point(x, y);
                    x += 60;
                    groupBox1.Controls.Add(lbl7);
                    d++;
                    y += 50;
                    x = 100;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {//aaaa
            int x = 150;
            int y = 100;
            int z = 0;
            groupBox1.Height = (Form1.kriterlist.Count * 50) + 300;
            groupBox1.Width = (Form1.kriterlist.Count * 60) + 300;
            this.Height = (Form1.kriterlist.Count * 50) + 380;
            this.Width = (Form1.kriterlist.Count * 60) + 300;
            groupBox1.Controls.Clear();

            for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                groupBox1.Controls.Add(lbl);

            }
            x = 80;
            y += 50;
            for (int i = 0; i < Form1.kriterlist.Count; i++)
            {
                for (int j = 0; j <= Form1.kriterlist.Count; j++)
                {
                    if (j == 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.Width = 60;
                        lbl1.Height = 50;
                        lbl1.BackColor = Color.Khaki;
                        lbl1.ForeColor = Color.Black;
                        lbl1.Font = new Font("Microsoft Sans Serift", 12);
                        lbl1.Text = Form1.kriterlist[i].ToString();
                        lbl1.Location = new Point(x, y);
                        groupBox1.Controls.Add(lbl1);
                        x += 70;
                    }
                    else
                    {
                        Label lbl2 = new Label();
                        lbl2.Width = 60;
                        lbl2.Height = 50;
                        lbl2.BackColor = Color.Khaki;
                        lbl2.ForeColor = Color.Black;
                        lbl2.Font = new Font("Microsoft Sans Serift", 12);
                        lbl2.BorderStyle = BorderStyle.FixedSingle;
                        lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisi.kriter_matrisi_verileri_virgullu[z]), 3).ToString();
                        lbl2.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl2);
                        z++;
                    }
                    if (j == Form1.kriterlist.Count)
                    {
                        y += 50;
                        x = 80;
                    }
                }
            }
            z = 0;
            x = 60;
            for (int i = 0; i <= kriter_matrisi_sutun_toplam.Count; i++)
            {
                if (i == 0)
                {
                    Label lbl1 = new Label();
                    lbl1.Width = 80;
                    lbl1.Height = 50;
                    lbl1.Text = "Sütun Toplam";
                    lbl1.BackColor = Color.Khaki;
                    lbl1.ForeColor = Color.DarkRed;
                    lbl1.Font = new Font("Microsoft Sans Serift", 12);
                    lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                    lbl1.Location = new Point(x, y);
                    x += 90;
                    groupBox1.Controls.Add(lbl1);
                }
                else
                {
                    Label lbl2 = new Label();
                    lbl2.Width = 60;
                    lbl2.Height = 50;
                    lbl2.BackColor = Color.Khaki;
                    lbl2.ForeColor = Color.DarkRed;
                    lbl2.Font = new Font("Microsoft Sans Serift", 12);
                    lbl2.BorderStyle = BorderStyle.FixedSingle;
                    lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisi_sutun_toplam[z]), 3).ToString();
                    lbl2.Location = new Point(x, y);
                    x += 60;
                    z++;
                    groupBox1.Controls.Add(lbl2);
                }
            }
        } //kriter matrisi

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x = 150;
            int y = 100;
            int z = 0;
            if (true)
            {
                groupBox1.Height = (Form1.kriterlist.Count * 50) + 300;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 300;
                this.Height = (Form1.kriterlist.Count * 50) + 380;
                this.Width = (Form1.kriterlist.Count * 60) + 300;
                groupBox1.Controls.Clear();

                for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                    groupBox1.Controls.Add(lbl);

                }
                x = 80;
                y += 50;
                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Height = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Text = Form1.kriterlist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 70;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisi.kriter_matrisi_verileri_virgullu[z]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                        if (j == Form1.kriterlist.Count)
                        {
                            y += 50;
                            x = 80;
                        }
                    }
                }
                z = 0;
                x = 60;
                for (int i = 0; i <= kriter_matrisi_sutun_toplam.Count; i++)
                {
                    if (i == 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.Width = 80;
                        lbl1.Height = 50;
                        lbl1.Text = "Sütun Toplam";
                        lbl1.BackColor = Color.Khaki;
                        lbl1.ForeColor = Color.DarkRed;
                        lbl1.Font = new Font("Microsoft Sans Serift", 12);
                        lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                        lbl1.Location = new Point(x, y);
                        x += 90;
                        groupBox1.Controls.Add(lbl1);
                    }
                    else
                    {
                        Label lbl2 = new Label();
                        lbl2.Width = 60;
                        lbl2.Height = 50;
                        lbl2.BackColor = Color.Khaki;
                        lbl2.ForeColor = Color.DarkRed;
                        lbl2.Font = new Font("Microsoft Sans Serift", 12);
                        lbl2.BorderStyle = BorderStyle.FixedSingle;
                        lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisi_sutun_toplam[z]), 3).ToString();
                        lbl2.Location = new Point(x, y);
                        x += 60;
                        z++;
                        groupBox1.Controls.Add(lbl2);
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int x = 150;
            int y = 100;
            int z = 0;
            {
                groupBox1.Height = (Form1.kriterlist.Count * 50) + 250;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 360;
                this.Height = (Form1.kriterlist.Count * 50) + 330;
                this.Width = (Form1.kriterlist.Count * 60) + 360;
                z = 0;
                x = 150;
                y = 100;
                groupBox1.Controls.Clear();
                for (int i = 0; i <= Form1.kriterlist.Count; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 100;
                        lbl.Height = 50;
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Text = "Satır Ortalaması";
                        lbl.Location = new Point(x, y);
                        x += 80;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                x = 90;
                y += 50;
                int c = 0;

                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count + 1; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Height = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.kriterlist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            if (j == Form1.kriterlist.Count + 1)
                            {
                                Label lbl2 = new Label();
                                lbl2.Width = 60;
                                lbl2.Height = 50;
                                lbl2.BackColor = Color.Khaki;
                                lbl2.ForeColor = Color.DarkRed;
                                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                                lbl2.BorderStyle = BorderStyle.FixedSingle;
                                lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması[c]), 3).ToString();
                                lbl2.Location = new Point(x, y);
                                x += 60;
                                groupBox1.Controls.Add(lbl2);
                                c++;
                            }
                            else
                            {
                                Label lbl2 = new Label();
                                lbl2.Width = 60;
                                lbl2.Height = 50;
                                lbl2.BackColor = Color.Khaki;
                                lbl2.ForeColor = Color.Black;
                                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                                lbl2.BorderStyle = BorderStyle.FixedSingle;
                                lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu[z]), 3).ToString();
                                lbl2.Location = new Point(x, y);
                                x += 60;
                                groupBox1.Controls.Add(lbl2);
                                z++;
                            }
                        }
                        if (j == Form1.kriterlist.Count + 1)
                        {
                            y += 50;
                            x = 90;
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int x = 150;
            int y = 100;
            int z = 0;
            groupBox1.Height = (Form1.seceneklist.Count * 50) + 200;
            groupBox1.Width = (Form1.kriterlist.Count * 60) + 300;
            this.Height = (Form1.seceneklist.Count * 50) + 280;
            this.Width = (Form1.kriterlist.Count * 60) + 300;
            x = 150;
            y = 100;
            z = 0;
            groupBox1.Controls.Clear();
            for (int i = 0; i < Form1.kriterlist.Count; i++)
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
                groupBox1.Controls.Add(lbl);
            }
            x = 90;
            y += 50;
            for (int i = 0; i < Form1.seceneklist.Count; i++)
            {
                for (int j = 0; j <= Form1.kriterlist.Count; j++)
                {
                    if (j == 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.Width = 60;
                        lbl1.Width = 50;
                        lbl1.BackColor = Color.Khaki;
                        lbl1.ForeColor = Color.Black;
                        lbl1.Font = new Font("Microsoft Sans Serift", 12);
                        lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                        lbl1.Text = Form1.seceneklist[i].ToString();
                        lbl1.Location = new Point(x, y);
                        groupBox1.Controls.Add(lbl1);
                        x += 60;
                    }
                    else
                    {
                        Label lbl2 = new Label();
                        lbl2.Width = 60;
                        lbl2.Height = 50;
                        lbl2.BackColor = Color.Khaki;
                        lbl2.ForeColor = Color.Black;
                        lbl2.Font = new Font("Microsoft Sans Serift", 12);
                        lbl2.BorderStyle = BorderStyle.FixedSingle;
                        lbl2.Text = kriter_secenek_matrisi.kriter_secenek_verileri[z].ToString();
                        lbl2.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl2);
                        z++;
                    }
                    if (j == Form1.kriterlist.Count)
                    {
                        y += 50;
                        x = 90;
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            int x = 150;
            int y = 100;
            int z = 0;
            groupBox1.Height = (Form1.kriterlist.Count * 50) + 250;
                groupBox1.Width = (Form1.kriterlist.Count * 60) + 360;
                this.Height = (Form1.kriterlist.Count * 50) + 330;
                this.Width = (Form1.kriterlist.Count * 60) + 360;
                z = 0;
                x = 150;
                y = 100;
                groupBox1.Controls.Clear();
                for (int i = 0; i <= Form1.kriterlist.Count; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 100;
                        lbl.Height = 50;
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Text = "Satır Ortalaması";
                        lbl.Location = new Point(x, y);
                        x += 80;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                x = 90;
                y += 50;
                int c = 0;

                for (int i = 0; i < Form1.kriterlist.Count; i++)
                {
                    for (int j = 0; j <= Form1.kriterlist.Count + 1; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Height = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.kriterlist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else
                        {
                            if (j == Form1.kriterlist.Count + 1)
                            {
                                Label lbl2 = new Label();
                                lbl2.Width = 60;
                                lbl2.Height = 50;
                                lbl2.BackColor = Color.Khaki;
                                lbl2.ForeColor = Color.DarkRed;
                                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                                lbl2.BorderStyle = BorderStyle.FixedSingle;
                                lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu_sonucunun_satir_ortalaması[c]), 3).ToString();
                                lbl2.Location = new Point(x, y);
                                x += 60;
                                groupBox1.Controls.Add(lbl2);
                                c++;
                            }
                            else
                            {
                                Label lbl2 = new Label();
                                lbl2.Width = 60;
                                lbl2.Height = 50;
                                lbl2.BackColor = Color.Khaki;
                                lbl2.ForeColor = Color.Black;
                                lbl2.Font = new Font("Microsoft Sans Serift", 12);
                                lbl2.BorderStyle = BorderStyle.FixedSingle;
                                lbl2.Text = Math.Round(Convert.ToDouble(kriter_matrisinin_sutun_toplama_bolumu[z]), 3).ToString();
                                lbl2.Location = new Point(x, y);
                                x += 60;
                                groupBox1.Controls.Add(lbl2);
                                z++;
                            }
                        }
                        if (j == Form1.kriterlist.Count + 1)
                        {
                            y += 50;
                            x = 90;
                        }
                    }
                }
            }

        private void button5_Click(object sender, EventArgs e)
        {
            int x, y, z;  
            {
                this.Height = ((Form1.seceneklist.Count * 50)) + 330;
                groupBox1.Height = ((Form1.seceneklist.Count * 50)) + 250;
                this.Width = (((Form1.kriterlist.Count + 2) * 60) * 2) + 480;
                groupBox1.Width = (((Form1.kriterlist.Count + 2) * 60) * 2) + 480;
                groupBox1.Controls.Clear();
                x = 150;
                y = 100;
                z = 0;
                for (int i = 0; i < Form1.kriterlist.Count + 2; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Text = "Toplam";
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (i == Form1.kriterlist.Count + 1)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.Text = "S+";
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                int a = 0, b = 0;
                z = 0;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    y += 50;
                    x = 90;
                    for (int j = 0; j <= Form1.kriterlist.Count + 2; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else if (j == Form1.kriterlist.Count + 1)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(ideal_uzakliklar_toplam[a]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            a++;
                        }
                        else if (j == Form1.kriterlist.Count + 2)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(ideal_uzakliklar_S[b]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            b++;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(ideal_uzakliklar[z]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }

                    }
                }


                //-----------------------------------------------------------------------------------

                x = ((Form1.kriterlist.Count + 2) * 60) + 330;
                int c = x;
                y = 100;
                z = 0;
                for (int i = 0; i < Form1.kriterlist.Count + 2; i++)
                {
                    if (i == Form1.kriterlist.Count)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.Text = "Toplam";
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Font = new Font("Microsoft Sans Serift", 12);
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else if (i == Form1.kriterlist.Count + 1)
                    {
                        Label lbl = new Label();
                        lbl.Width = 60;
                        lbl.Height = 50;
                        lbl.Text = "S-";
                        lbl.BackColor = Color.Khaki;
                        lbl.ForeColor = Color.DarkRed;
                        lbl.Font = new Font(lbl.Font, lbl.Font.Style ^ FontStyle.Underline);
                        lbl.Location = new Point(x, y);
                        x += 60;
                        groupBox1.Controls.Add(lbl);
                    }
                    else
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
                        groupBox1.Controls.Add(lbl);
                    }
                }
                a = 0; b = 0;
                z = 0;
                for (int i = 0; i < Form1.seceneklist.Count; i++)
                {
                    y += 50;
                    x = c - 60;
                    for (int j = 0; j <= Form1.kriterlist.Count + 2; j++)
                    {
                        if (j == 0)
                        {
                            Label lbl1 = new Label();
                            lbl1.Width = 60;
                            lbl1.Width = 50;
                            lbl1.BackColor = Color.Khaki;
                            lbl1.ForeColor = Color.Black;
                            lbl1.Font = new Font("Microsoft Sans Serift", 12);
                            lbl1.Font = new Font(lbl1.Font, lbl1.Font.Style ^ FontStyle.Underline);
                            lbl1.Text = Form1.seceneklist[i].ToString();
                            lbl1.Location = new Point(x, y);
                            groupBox1.Controls.Add(lbl1);
                            x += 60;
                        }
                        else if (j == Form1.kriterlist.Count + 1)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.Text = Math.Round(Convert.ToDouble(negatif_ideal_uzakliklar_toplam[a]), 3).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            a++;
                        }
                        else if (j == Form1.kriterlist.Count + 2)
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.DarkRed;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(negatif_ideal_uzakliklar_s_eksi[b]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            b++;
                        }
                        else
                        {
                            Label lbl2 = new Label();
                            lbl2.Width = 60;
                            lbl2.Height = 50;
                            lbl2.BackColor = Color.Khaki;
                            lbl2.ForeColor = Color.Black;
                            lbl2.Font = new Font("Microsoft Sans Serift", 12);
                            lbl2.BorderStyle = BorderStyle.FixedSingle;
                            lbl2.Text = Math.Round(Convert.ToDouble(negatif_ideal_uzakliklar[z]), 5).ToString();
                            lbl2.Location = new Point(x, y);
                            x += 60;
                            groupBox1.Controls.Add(lbl2);
                            z++;
                        }
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void sonuclar_Load(object sender, EventArgs e)
        {
            button6.Text = label3.Text;
        }
    }
    }
    


