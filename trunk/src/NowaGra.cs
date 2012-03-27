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

        //private Image [] czolg;
        private int pozycja_x, pozycja_y;
        private int szybkosc;
        private String kierunek;
        private int wytrzymalosc;   //wytrzymalość czołgu
        private int energia;        //ilość żyć, energii

        private Image [] i_bateria;

        private Image [] pocisk;
        private int pozycja_pocisku_x, pozycja_pocisku_y;
        private int szybkosc_pocisku;
        private String kierunek_pocisku;

        private Image cegielka;

        //Zmienne dotyczące rysowania
        private System.Drawing.Imaging.ImageAttributes transparentPink;
        bool pocisk_na_mapie;
        Point rozmiar_mapy;
        
        Rectangle kamera;
        Point pozycja_kamery; //Lewy górny róg  kamery
        
        Image ziemia;
        Image tlo;
        /// <summary>
        /// Konstruktor klasy nowej gry.
        /// </summary>
        public NowaGra()
        {
            InitializeComponent();
            szybkosc = 5;
            pozycja_x = 100;
            pozycja_y = 100;
            kierunek = "up";
            wytrzymalosc = 100;
            energia = 3;
            kierunek_pocisku = null;
            szybkosc_pocisku = 10;
            pozycja_pocisku_x = 0;
            pozycja_pocisku_y = 0;

            pocisk_na_mapie = false;
            
            
            rozmiar_mapy.X = 1000;
            rozmiar_mapy.Y = 1000;
            
            czas_minuty = 0;
            czas_sekundy = 0;

            //Dostajemy się do resource wkompilowaniego w aplikacje
            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();
            //Ustawianie koloru przeźroczystości
            transparentPink = new System.Drawing.Imaging.ImageAttributes();
            transparentPink.SetColorKey(Color.Pink, Color.Pink);

            pozycja_kamery.X = 0;
            pozycja_kamery.Y = 0;
            
            kamera = new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, pictureBox1.Width, pictureBox1.Height);
            
            //czolg = new Image[8];
            pocisk = new Image[4];
            i_bateria = new Image[7];

            ziemia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.ziemia.png"));
            cegielka = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.cegielka.png"));
            tlo = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.ziemia_tlo.png"));

            i_bateria[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_100.png"));
            i_bateria[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_85.png"));
            i_bateria[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_70.png"));
            i_bateria[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_55.png"));
            i_bateria[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_40.png"));
            i_bateria[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_25.png"));
            i_bateria[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_10.png"));
            /*
            //czolg[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank_up.png"));
            //czolg[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_up_1.png"));
            czolg[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank2_1_up.png"));
            czolg[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_up_2.png"));

            //czolg[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank_down.png"));
            //czolg[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_down_1.png"));
            czolg[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank2_1_down.png"));
            czolg[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_down_2.png"));

            //czolg[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_left_1.png"));
            czolg[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank2_1_left.png"));
            czolg[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_left_2.png"));

            //czolg[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_right_1.png"));
            czolg[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank2_1_right.png"));
            czolg[7] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_right_2.png"));
            
            pocisk[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_up.png"));
            pocisk[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_down.png"));
            pocisk[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_left.png"));
            pocisk[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_right.png"));
            */
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
                            pozycja_kamery.Y -= szybkosc;
                            if (pozycja_kamery.Y <= 0) pozycja_kamery.Y = 0;//zmien pozycje kamery
                            kierunek = "up";
                            player.Ruch(Czolg.Kierunek.GORA, rozmiar_mapy);
                            /*if (pozycja_y >= szybkosc) pozycja_y -= szybkosc;
                            else pozycja_y = 0;*/
                           
                        } break;
                    case Keys.Down:
                        {
                            pozycja_kamery.Y += szybkosc;
                            if (pozycja_kamery.Y + pictureBox1.Height >= rozmiar_mapy.Y) 
                                pozycja_kamery.Y = rozmiar_mapy.Y - pictureBox1.Height;
                            kierunek = "down";
                            player.Ruch(Czolg.Kierunek.DOL, rozmiar_mapy);
                            /*
                            if (pozycja_y <= rozmiar_mapy.Y - (szybkosc + czolg[0].Width)) pozycja_y += szybkosc;
                            else pozycja_y = rozmiar_mapy.Y - czolg[0].Width;*/
                        } break;
                    case Keys.Left:
                        {
                            pozycja_kamery.X -= szybkosc;
                            if (pozycja_kamery.X <= 0) pozycja_kamery.X = 0;
                            
                            kierunek = "left";
                            player.Ruch(Czolg.Kierunek.LEWO, rozmiar_mapy);
                            
                            /*if (pozycja_x >= szybkosc) pozycja_x -= szybkosc;
                            else pozycja_x = 0;*/
                        }break;
                    case Keys.Right:
                        {
                            pozycja_kamery.X += szybkosc;
                            if (pozycja_kamery.X + pictureBox1.Width >= rozmiar_mapy.X)
                                pozycja_kamery.X = rozmiar_mapy.X - pictureBox1.Width;
                            
                            kierunek = "right";
                            player.Ruch(Czolg.Kierunek.PRAWO, rozmiar_mapy);
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
                kamera = new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, pictureBox1.Width, pictureBox1.Height);
                
            }
        }
        private void pociskStrzelaj()
        {
            kierunek_pocisku = kierunek;
            if (kierunek_pocisku == "up")
            {
                pozycja_pocisku_x = pozycja_x + 8;
                pozycja_pocisku_y = pozycja_y;
            }
            if (kierunek_pocisku == "down")
            {
                pozycja_pocisku_x = pozycja_x + 8;
                pozycja_pocisku_y = pozycja_y + 14;
            }
            if (kierunek_pocisku == "left")
            {
                pozycja_pocisku_x = pozycja_x;
                pozycja_pocisku_y = pozycja_y +8;
            }
            if (kierunek_pocisku == "right")
            {
                pozycja_pocisku_x = pozycja_x + 14;
                pozycja_pocisku_y = pozycja_y + 8;
            }
            pocisk_na_mapie = true;
        }
        private void zmienPozycjePocisku()
        {
            if (pocisk_na_mapie == true)
            {
                if (kierunek_pocisku == "up")
                {
                    if (pozycja_pocisku_y > 0)
                    {
                        pozycja_pocisku_y -= szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
                if (kierunek_pocisku == "down")
                {
                    if (pozycja_pocisku_y < pictureBox1.Height)
                    {
                        pozycja_pocisku_y += szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
                if (kierunek_pocisku == "left")
                {
                    if (pozycja_pocisku_x > 0)
                    {
                        pozycja_pocisku_x -= szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
                if (kierunek_pocisku == "right")
                {
                    if (pozycja_pocisku_x < pictureBox1.Width)
                    {
                        pozycja_pocisku_x += szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
            }
        }
        private Bitmap bitmapBuffor = null;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
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
                plansza.RysujElementy(g, pozycja_kamery, transparentPink);
                player.Rysuj(g, pozycja_kamery, transparentPink);
                
                /*g.DrawImage(czolg[0], new Rectangle(500 - pozycja_kamery.X, 500 - pozycja_kamery.Y, czolg[0].Width, czolg[0].Height), 0, 0,
                    czolg[0].Width, czolg[0].Height, GraphicsUnit.Pixel, transparentPink);*/
                /*
                g.DrawImage(cegielka, new Rectangle(300 - pozycja_kamery.X, 200 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(300 - pozycja_kamery.X, 225 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(300 - pozycja_kamery.X, 250 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(300 - pozycja_kamery.X, 275 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(300 - pozycja_kamery.X, 300 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);

                g.DrawImage(cegielka, new Rectangle(325 - pozycja_kamery.X, 200 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(325 - pozycja_kamery.X, 225 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(325 - pozycja_kamery.X, 250 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(325 - pozycja_kamery.X, 275 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawImage(cegielka, new Rectangle(325 - pozycja_kamery.X, 300 - pozycja_kamery.Y, cegielka.Width, cegielka.Height), 0, 0,
                        cegielka.Width, cegielka.Height, GraphicsUnit.Pixel, transparentPink);
                
                if (kierunek == "up")
                {
                    //e.Graphics.DrawImage(czolg[0], pozycja_x, pozycja_y);
                    g.DrawImage(czolg[0], new Rectangle(pozycja_x - pozycja_kamery.X, pozycja_y - pozycja_kamery.Y, czolg[0].Width, czolg[0].Height), 0, 0,
                        czolg[0].Width, czolg[0].Height, GraphicsUnit.Pixel, transparentPink);

                }
                if (kierunek == "down")
                {
                    //e.Graphics.DrawImage(czolg[2], pozycja_x, pozycja_y);
                    g.DrawImage(czolg[2], new Rectangle(pozycja_x - pozycja_kamery.X, pozycja_y - pozycja_kamery.Y, czolg[2].Width, czolg[2].Height), 0, 0,
                        czolg[2].Width, czolg[2].Height, GraphicsUnit.Pixel, transparentPink);

                }
                if (kierunek == "left")
                {
                    //e.Graphics.DrawImage(czolg[4], pozycja_x, pozycja_y);
                    g.DrawImage(czolg[4], new Rectangle(pozycja_x - pozycja_kamery.X, pozycja_y - pozycja_kamery.Y, czolg[4].Width, czolg[4].Height), 0, 0,
                        czolg[4].Width, czolg[4].Height, GraphicsUnit.Pixel, transparentPink);
                }
                if (kierunek == "right")
                {
                    //e.Graphics.DrawImage(czolg[6], pozycja_x, pozycja_y);
                    g.DrawImage(czolg[6], new Rectangle(pozycja_x - pozycja_kamery.X, pozycja_y - pozycja_kamery.Y, czolg[6].Width, czolg[6].Height), 0, 0,
                        czolg[6].Width, czolg[6].Height, GraphicsUnit.Pixel, transparentPink);
                }*/
                
                /*if (pocisk_na_mapie == true)
                {
                    if (kierunek_pocisku == "up")
                    {
                        g.DrawImage(pocisk[0], pozycja_pocisku_x - pozycja_kamery.X, pozycja_pocisku_y - pozycja_kamery.Y);
                    }
                    if (kierunek_pocisku == "down")
                    {
                        g.DrawImage(pocisk[1], pozycja_pocisku_x - pozycja_kamery.X, pozycja_pocisku_y - pozycja_kamery.Y);
                    }
                    if (kierunek_pocisku == "left")
                    {
                        g.DrawImage(pocisk[2], pozycja_pocisku_x - pozycja_kamery.X, pozycja_pocisku_y - pozycja_kamery.Y);
                    }
                    if (kierunek_pocisku == "right")
                    {
                        g.DrawImage(pocisk[3], pozycja_pocisku_x - pozycja_kamery.X, pozycja_pocisku_y - pozycja_kamery.Y);
                    }
                }*/
            }
            //Teraz wczytujemy to co w bufforze na ekran
            e.Graphics.DrawImage(bitmapBuffor, 0, 0);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zmienPozycjeGracza();
            player.RuchPocisku();
            pictureBox1.Invalidate();
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
    }
}