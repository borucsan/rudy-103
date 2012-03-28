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
        private Gracz player;
        private Fabryka fabryka;
        private Plansza plansza;

        private int wytrzymalosc;   //wytrzymalość czołgu
        private int energia;        //ilość żyć, energii

        //Wartości określające minimalne i maksymalne wartości pola wyświetlającego grafike 
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;

        private Image [] i_bateria;

     

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
            maxY = 240;

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

            tlo = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.ziemia_tlo.png"));

            i_bateria[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_100.png"));
            i_bateria[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_85.png"));
            i_bateria[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_70.png"));
            i_bateria[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_55.png"));
            i_bateria[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_40.png"));
            i_bateria[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_25.png"));
            i_bateria[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_10.png"));

            fabryka = new Fabryka(execAssem, true);
            plansza = new Plansza(1000, 1000);
            plansza.GenerujDebugMapa(fabryka);
            label2.Text = "Demo";
            player = Fabryka.ProdukujDomyslnegoGracza(execAssem);

            

            this.timer1.Enabled = true;
            this.czas_rozgrywki.Enabled = true;
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
                            
                            /*if (pozycja_x <= rozmiar_mapy.X - (szybkosc + czolg[0].Width)) pozycja_x += szybkosc;
                            else pozycja_x = rozmiar_mapy.X - czolg[0].Width;*/
                        } break;
                    case Keys.Enter:
                        {
                            player.Strzelaj(fabryka);
                        } break;
                    case Keys.Space:
                        {
                            zmniejsz_energie();
                            energiapicture.Invalidate();
                            
                        } break;
                }
                kamera = new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, maxX, maxY);
                
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            zmienPozycjeGracza();
            player.RuchPocisku(plansza);
            //pictureBox1.Invalidate();
            Invalidate();
            minimapapictureBox.Invalidate();
            
        }

        private void minimapapictureBox_Paint(object sender, PaintEventArgs e)
        {
            /*Rysowanie przeciwnikow zrespionych na minimapie
            for (int i = 0; i < liczba_przeciwnikow_na_mapie; i++)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Blue), (int)przeciwnicy_na_mapie[i].X / 20, 
                    (int)przeciwnicy_na_mapie[i].Y / 20, 2, 2);
            }*/
            //e.Graphics.DrawEllipse(new Pen(Color.Red), (int)pozycja_x/20, (int)pozycja_y/20, 2, 2);
            
            e.Graphics.FillEllipse(new SolidBrush(Color.Red), new Rectangle(((int)player.wymiary.X / 20), ((int)player.wymiary.Y / 20), 2, 2));
            e.Graphics.DrawRectangle(new Pen(Color.Black), (int)pozycja_kamery.X/20, (int)pozycja_kamery.Y/20, 12, 13);
        }

        private void czas_rozgrywki_Tick(object sender, EventArgs e)
        {
            czas_sekundy += 1;
            if (czas_sekundy == 60)
            {
                czas_sekundy = 0;
                czas_minuty += 1;
            }
            
            czaslabel.Text = czas_minuty + ":" + czas_sekundy;
            //czaslabel.Text = "Czas: %0.2d"+czas_sekundy;
        }

        //Metoda do testowania dzialania bateri energii
        //Testowane za pomocą spacji
        private void zmniejsz_energie()
        {
            wytrzymalosc -= 1;
            if (wytrzymalosc <= 0)
            {
                wytrzymalosc = 100;
                energia -= 1;
                if (energia <= 0) energia = 0;
            }
        }

        //Do przerobienia, wersja alpha, nie optymalna
        private void energiapicture_Paint(object sender, PaintEventArgs e)
        {
            int procent_wytrzymalosci = (wytrzymalosc * 100) / 100;
            
            if (energia == 3)
            {
                e.Graphics.DrawImage(i_bateria[0], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                e.Graphics.DrawImage(i_bateria[0], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                {
                    e.Graphics.DrawImage(i_bateria[0], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                {
                    e.Graphics.DrawImage(i_bateria[1], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                {
                    e.Graphics.DrawImage(i_bateria[2], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                {
                    e.Graphics.DrawImage(i_bateria[3], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                {
                    e.Graphics.DrawImage(i_bateria[4], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                {
                    e.Graphics.DrawImage(i_bateria[5], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 10)
                {
                    e.Graphics.DrawImage(i_bateria[6], new Rectangle(37, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
            }
            if (energia == 2)
            {
                e.Graphics.DrawImage(i_bateria[0], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                
                if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                {
                    e.Graphics.DrawImage(i_bateria[0], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                {
                    e.Graphics.DrawImage(i_bateria[1], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                {
                    e.Graphics.DrawImage(i_bateria[2], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                {
                    e.Graphics.DrawImage(i_bateria[3], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                {
                    e.Graphics.DrawImage(i_bateria[4], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                {
                    e.Graphics.DrawImage(i_bateria[5], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 10)
                {
                    e.Graphics.DrawImage(i_bateria[6], new Rectangle(19, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
            }
            if (energia == 1)
            {
                
                if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                {
                    e.Graphics.DrawImage(i_bateria[0], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                {
                    e.Graphics.DrawImage(i_bateria[1], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                {
                    e.Graphics.DrawImage(i_bateria[2], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                {
                    e.Graphics.DrawImage(i_bateria[3], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                {
                    e.Graphics.DrawImage(i_bateria[4], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                {
                    e.Graphics.DrawImage(i_bateria[5], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (procent_wytrzymalosci <= 10)
                {
                    e.Graphics.DrawImage(i_bateria[6], new Rectangle(1, 20, 18, 38), 0, 0, i_bateria[0].Width, i_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                }
            }
        }
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
                plansza.RysujElementy(g, pozycja_kamery, transparentPink);

            }                
            e.Graphics.DrawImage(bitmapBuffor, 0, 0);
        }
    }
}