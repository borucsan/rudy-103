using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rudy_103.src
{
    /// <summary>
    /// NowaGra - jest to klasa okna nowej gry.
    /// </summary>
    public partial class NowaGra : Form
    {
        private int czas_sekundy;
        private int czas_minuty;

        private String s_czas;  //String do wyświetlania czasu
        private String s_poziom; //String do wyświetlania poziomu
        private String s_przeciwnicy; //String do wyświetlania liczby przeciwników
        private String s_punkty;    //String do wyświetlania liczby punktów
        private int licznik_informacji; //Licznik potrzebny do timera informacji (czas_informacji)
        

        private Gracz player;
        private Fabryka fabryka;
        private Plansza plansza;
        private Przeciwnik [] enemy;

        private int wytrzymalosc;   //wytrzymalość czołgu
        private int energia;        //ilość żyć, energii

        //Wartości określające minimalne i maksymalne wartości pola wyświetlającego grafike 
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;

        private Image [] i_bateria;
        private Image [] i_ogien;
        private Image i_menu;
        private Image i_rect;
        private int numer_efektu;

        //Zmienne dotyczące rysowania
        private System.Drawing.Imaging.ImageAttributes transparentPink;
        
        Rectangle kamera;
        Point pozycja_kamery; //Lewy górny róg  kamery
        
        Image tlo;
        /// <summary>
        /// Konstruktor klasy nowej gry.
        /// </summary>
        public NowaGra()
        {
            InitializeComponent();
            wytrzymalosc = 100;
            energia = 3;

            minX = 0;
            minY = 0;
            maxX = 240;
            maxY = 320;

            czas_minuty = 0;
            czas_sekundy = 0;

            //Dostajemy się do resource wkompilowaniego w aplikacje
            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();
            //Ustawianie koloru przeźroczystości
            transparentPink = new System.Drawing.Imaging.ImageAttributes();
            transparentPink.SetColorKey(Color.Pink, Color.Pink);

            pozycja_kamery.X = minX;
            pozycja_kamery.Y = minY;
            
            kamera = new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, maxX, maxY);

            i_bateria = new Image[7];
            i_ogien = new Image[5];

            tlo = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tlo.png"));

            i_bateria[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_100.png"));
            i_bateria[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_85.png"));
            i_bateria[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_70.png"));
            i_bateria[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_55.png"));
            i_bateria[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_40.png"));
            i_bateria[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_25.png"));
            i_bateria[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_10.png"));

            i_rect = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.transp_rect2.png"));
            i_menu = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.menu.png"));

            i_ogien[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.flame_1.png"));
            i_ogien[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.flame_2.png"));
            i_ogien[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.flame_3.png"));
            i_ogien[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.flame_4.png"));
            i_ogien[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.flame_5.png"));

            numer_efektu = 0;
            licznik_informacji = 1;

            fabryka = new Fabryka(execAssem, true);
            plansza = new Plansza(1000, 1000);
            plansza.GenerujDebugMapa(fabryka);
            
            player = Fabryka.ProdukujDomyslnegoGracza(execAssem);

            Random pozycja = new Random();
            enemy = new Przeciwnik[10];
            for (int i = 0; i < 10; i++)
            {
                enemy[i] = fabryka.ProdukujPrzeciwnika("przeciwnik_poziom_1");
                enemy[i].wymiary = new Rectangle(pozycja.Next(0, 1000), pozycja.Next(0, 1000), 40, 40);
            }
            //enemy[0] = fabryka.ProdukujPrzeciwnika("przeciwnik_poziom_1");
            //enemy[0].wymiary = new Rectangle(900, 20, 40, 40);

            this.timer1.Enabled = true;
            this.czas_rozgrywki.Enabled = true;
            this.czas_efektow.Enabled = true;
            this.czas_informacji.Enabled = true;
        }

        private System.Windows.Forms.KeyEventArgs wcisniety_klawisz = null;
        private void NowaGra_KeyDown(object sender, KeyEventArgs e)
        {
            wcisniety_klawisz = e;
        }
        private void NowaGra_KeyUp(object sender, KeyEventArgs e)
        {
            wcisniety_klawisz = null;
        }

        private void zmienPozycjeGracza()
        {
            if (wcisniety_klawisz != null)
            {
                switch (wcisniety_klawisz.KeyCode)
                {
                    case Keys.Up:
                        {
                            if ((player.wymiary.Y + player.wymiary.Height / 2) > (pozycja_kamery.Y + maxY / 2))
                            {
                                player.Ruch(Czolg.Kierunek.GORA, plansza);
                            }
                            else
                            {
                                pozycja_kamery.Y -= player.Szybkosc;
                                if (pozycja_kamery.Y <= minY) pozycja_kamery.Y = minY;//zmien pozycje kamery
                                player.Ruch(Czolg.Kierunek.GORA, plansza);
                            }
                            
                            /*if (pozycja_y >= szybkosc) pozycja_y -= szybkosc;
                            else pozycja_y = 0;*/
                           
                        } break;
                    case Keys.Down:
                        {
                            if ((player.wymiary.Y + player.wymiary.Height / 2) < (pozycja_kamery.Y + maxY / 2))
                            {
                                player.Ruch(Czolg.Kierunek.DOL, plansza);
                            }
                            else
                            {
                                pozycja_kamery.Y += player.Szybkosc;
                                if (pozycja_kamery.Y + maxY >= plansza.Wysokosc)
                                    pozycja_kamery.Y = plansza.Wysokosc - maxY;
                                player.Ruch(Czolg.Kierunek.DOL, plansza);
                            }
                            
                            //kierunek = "down";
                            
                            /*
                            if (pozycja_y <= rozmiar_mapy.Y - (szybkosc + czolg[0].Width)) pozycja_y += szybkosc;
                            else pozycja_y = rozmiar_mapy.Y - czolg[0].Width;*/
                        } break;
                    case Keys.Left:
                        {
                            if ((player.wymiary.X + player.wymiary.Width / 2) > (pozycja_kamery.X + maxX / 2))
                            {
                                player.Ruch(Czolg.Kierunek.LEWO, plansza);
                            }
                            else
                            {
                                pozycja_kamery.X -= player.Szybkosc;
                                if (pozycja_kamery.X <= minX) pozycja_kamery.X = minX;

                                player.Ruch(Czolg.Kierunek.LEWO, plansza);
                            }
                            
                            /*if (pozycja_x >= szybkosc) pozycja_x -= szybkosc;
                            else pozycja_x = 0;*/
                        }break;
                    case Keys.Right:
                        {
                            if ((player.wymiary.X + player.wymiary.Height / 2) < (pozycja_kamery.X + maxX / 2))
                            {
                                player.Ruch(Czolg.Kierunek.PRAWO, plansza);
                            }
                            else
                            {
                                pozycja_kamery.X += player.Szybkosc;
                                if (pozycja_kamery.X + maxX >= plansza.Szerokosc)
                                    pozycja_kamery.X = plansza.Szerokosc - maxX;

                                player.Ruch(Czolg.Kierunek.PRAWO, plansza);
                            }
                            
                        } break;
                    case Keys.Enter:
                        {
                            player.Strzelaj(fabryka);
                        } break;
                    case Keys.Space:
                        {
                            zmniejsz_energie();
                        } break;
                }
                kamera = new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, maxX, maxY);
                
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            zmienPozycjeGracza();
            player.RuchPocisku(plansza);
            for (int i = 0; i < 10; i++)
            {
                enemy[i].Ruch_Przeciwnika(plansza, fabryka);
            }
            
            s_poziom = "Poziom: "+plansza.poziom;
            s_przeciwnicy = "Przeciwnicy: " + plansza.przeciwnicy.Count;
            s_punkty = "Punkty: " + player.Punkty;
            
            Invalidate();
        }

        private void czas_rozgrywki_Tick(object sender, EventArgs e)
        {
            czas_sekundy += 1;
            if (czas_sekundy == 60)
            {
                czas_sekundy = 0;
                czas_minuty += 1;
            }
            s_czas = string.Format("{0:00}:{1:00}", czas_minuty, czas_sekundy); //czas_minuty.ToString() + ":" + czas_sekundy.ToString();
            
        }

        //Metoda do testowania dzialania bateri energii
        //Testowane za pomocą spacji
        private void zmniejsz_energie()
        {
            player.AktualnaWytrzymalosc -= 1;
            if (player.AktualnaWytrzymalosc <= 0)
            {
                player.AktualnaWytrzymalosc = player.Wytrzymalosc;
                player.Energia -= 1;
                if (player.Energia <= 0) player.Energia = 0;
            }
        }

        //Do przerobienia, wersja alpha, nie optymalna
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
        private Bitmap bitmapBuffor = null;
        private void NowaGra_Paint(object sender, PaintEventArgs e)
        {
            /*cos w tym stylu trzeba bedzie zaimplementować kiedy będą dane wrzucane do jakiegoś kontenera danych
            for(int x = 0; x<rozmiar_mapy.X; x++)
            {
                for(int y = 0; y<rozmiar_mapy.Y; y++)
                {
                    if (lista[x][y].pozycja_obiektu.x <= pozycja_kamery.X + pictureBox1.Width + 50
                        && lista[x][y].pozycja_obiektu.y <= pozycja_kamery.Y + pictureBox1.Height + 50)
                    {
                        lista[x][y].Rysuj(PaintEventArgs e);
                    }
                }
            }
            */
            //Tworzymy nowy buffor jezeli potrzebny.
            if (bitmapBuffor == null)
            {
                bitmapBuffor = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            //Tutaj ladujemy cala grafike
            using (Graphics g = Graphics.FromImage(bitmapBuffor))
            {
                //g.Clear(Color.White);
                g.DrawImage(tlo, 0, 0);
                player.Rysuj(g, pozycja_kamery, transparentPink);
                
                for (int i = 0; i < 10; i++)
                {
                    enemy[i].Rysuj(g, pozycja_kamery, transparentPink);
                }
                
                plansza.RysujElementy(g, pozycja_kamery, transparentPink);
                DodajOgien(g, new Point(200, 200), transparentPink, numer_efektu);
                RysujInterfejs(g, transparentPink);

            }                
            e.Graphics.DrawImage(bitmapBuffor, 0, 0);
        }

        private void DodajOgien(Graphics g, Point pozycja_uderzenia, System.Drawing.Imaging.ImageAttributes transparentPink, int numer)
        {
            
            g.DrawImage(i_ogien[numer], new Rectangle(pozycja_uderzenia.X-50 - pozycja_kamery.X, pozycja_uderzenia.Y - pozycja_kamery.Y, 25, 25), 0, 0,
                        i_ogien[numer].Width, i_ogien[numer].Height, GraphicsUnit.Pixel, transparentPink);
            g.DrawImage(i_ogien[numer], new Rectangle(pozycja_uderzenia.X - pozycja_kamery.X, pozycja_uderzenia.Y-50 - pozycja_kamery.Y, 25, 25), 0, 0,
                        i_ogien[numer].Width, i_ogien[numer].Height, GraphicsUnit.Pixel, transparentPink);
            g.DrawImage(i_ogien[numer], new Rectangle(pozycja_uderzenia.X - 25 - pozycja_kamery.X, pozycja_uderzenia.Y+25 - pozycja_kamery.Y, 25, 25), 0, 0,
                        i_ogien[numer].Width, i_ogien[numer].Height, GraphicsUnit.Pixel, transparentPink);
        }

        private void czas_efektow_Tick(object sender, EventArgs e)
        {
            numer_efektu += 1;
            if (numer_efektu > 4) numer_efektu = 0;
        }
        private void RysujInterfejs(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            //Color kolor = Color.FromArgb(10, 155, 10);
            //kolor.A = 100;
            //System.Drawing.Color kolor = System.Drawing.Color.FromArgb(0xAA00DD00);
            Rectangle prostokat = new Rectangle(170, 20, 50, 50);
            Rectangle prostokat2 = new Rectangle(169, 19, 51, 51);
            Brush pedzel = new SolidBrush(Color.FromArgb(0x7800FF00));
            g.DrawRectangle(new Pen(Color.Blue), prostokat2);
            g.DrawImage(i_rect, prostokat, 0, 0, i_rect.Width, i_rect.Height, GraphicsUnit.Pixel, transparentPink);
            //g.FillRectangle(pedzel, prostokat);
            g.FillEllipse(new SolidBrush(Color.White), new Rectangle( prostokat.X + ((int)player.wymiary.X / 20), prostokat.Y + ((int)player.wymiary.Y / 20), 2, 2));
            for (int i = 0; i < 10; i++)
            {
                g.FillEllipse(new SolidBrush(Color.Yellow), new Rectangle( prostokat.X + ((int)enemy[i].wymiary.X / 20), prostokat.Y + ((int)enemy[i].wymiary.Y / 20), 2, 2));
            }

            g.DrawRectangle(new Pen(Color.Red), prostokat.X + (int)pozycja_kamery.X / 20, prostokat.Y + (int)pozycja_kamery.Y / 20, 12, 13);
            g.DrawImage(i_menu, new Rectangle(minX, maxY-30, 240, 30), 0, 0, i_menu.Width, i_menu.Height, GraphicsUnit.Pixel, transparentPink);
            
            #region RysowanieInformacji
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            if (licznik_informacji == 1) g.DrawString(s_poziom, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
            if (licznik_informacji == 2) g.DrawString(s_czas, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
            if (licznik_informacji == 3) g.DrawString(s_przeciwnicy, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
            if (licznik_informacji == 4) g.DrawString(s_punkty, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
            #endregion RysowanieInformacji
            #region RysowanieBaterii
            int procent_wytrzymalosci = (player.AktualnaWytrzymalosc * 100) / player.Wytrzymalosc;
            if (player.Energia == 3)
            {
                Rectangle prostokat3 = new Rectangle(minX + 20, minY + 34, 18, 36);
                g.DrawImage(i_bateria[0], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                prostokat3.X += 20;
                g.DrawImage(i_bateria[0], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                prostokat3.X += 20;
                if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                {
                    g.DrawImage(i_bateria[0], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                {
                    g.DrawImage(i_bateria[1], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                {
                    g.DrawImage(i_bateria[2], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                {
                    g.DrawImage(i_bateria[3], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                {
                    g.DrawImage(i_bateria[4], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                {
                    g.DrawImage(i_bateria[5], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 10)
                {
                    g.DrawImage(i_bateria[6], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
            }
            if (player.Energia == 2)
            {
                Rectangle prostokat3 = new Rectangle(minX + 20, minY + 20, 18, 36);
                g.DrawImage(i_bateria[0], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                
                prostokat3.X += 20;
                if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                {
                    g.DrawImage(i_bateria[0], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                {
                    g.DrawImage(i_bateria[1], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                {
                    g.DrawImage(i_bateria[2], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                {
                    g.DrawImage(i_bateria[3], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                {
                    g.DrawImage(i_bateria[4], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                {
                    g.DrawImage(i_bateria[5], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 10)
                {
                    g.DrawImage(i_bateria[6], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
            }
            if (player.Energia == 1)
            {
                Rectangle prostokat3 = new Rectangle(minX + 20, minY + 20, 18, 36);
                if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                {
                    g.DrawImage(i_bateria[0], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                {
                    g.DrawImage(i_bateria[1], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                {
                    g.DrawImage(i_bateria[2], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                {
                    g.DrawImage(i_bateria[3], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                {
                    g.DrawImage(i_bateria[4], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                {
                    g.DrawImage(i_bateria[5], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 10)
                {
                    g.DrawImage(i_bateria[6], prostokat3, 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
            }
            #endregion RysowanieBaterii
        }

        private void czas_informacji_Tick(object sender, EventArgs e)
        {
            
            licznik_informacji += 1;
            if (licznik_informacji > 4) licznik_informacji = 1;

            
        }
    }
}