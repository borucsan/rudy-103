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
        //Wartości określające graficzny interfejs użytkownika
        private bool panelEnergii;
        private bool panelRadaru;
        private bool panelInformacji;
        private bool przyciskMapy;      //przycisk, który będzie otwierał większą mapę
        private bool panelMapy;         //większa mapa
        private bool narysowanaMapa;    //zmienna sprawdzajaca stan narysowanej mapy
        private bool przyciskOpcji;
        private bool panelOpcji;
        private bool panelUlepszen;     //zmienna służąca do włączania/wyłączania panelu ulepszeń

        //Prostokaty okreslajace przyciski
        Rectangle przyciskMapyProst;
        Rectangle przyciskOpcjiProst;

        Rectangle przyciskZamknijMapeProst;

        Rectangle przyciskZamknijOpcjeProst;
        Rectangle przyciskWylaczRadar;
        Rectangle przyciskWylaczEnergie;
        Rectangle przyciskWylaczInformacje;
        Rectangle przyciskWylaczCieniowanie;
        Rectangle przyciskWyjscia;

        Rectangle przyciskZamknijUkonczonyPoziom;


        private int czas_sekundy;
        private int czas_minuty;

        /// <summary>
        /// Wartość określająca czy gra wczytała już wszystkie pliki
        /// </summary>
        public bool graWczytana = false;

        private String s_czas;  //String do wyświetlania czasu
        private String s_poziom; //String do wyświetlania poziomu
        private String s_przeciwnicy; //String do wyświetlania liczby przeciwników
        private String s_punkty;    //String do wyświetlania liczby punktów
        private int licznik_informacji; //Licznik potrzebny do timera informacji (czas_informacji)


        private Gracz player;
        private Fabryka fabryka;
        private Plansza plansza;
        private Warsztat warsztat;

        //Wartości określające minimalne i maksymalne wartości pola wyświetlającego grafike 
        private int minX;
        private int maxX;
        private int minY;
        private int maxY;

        private Image[] i_bateria;
        private Image i_menu;
        private Image i_rect;
        private Image radar;

        //Obrazy do obsługi Mapy
        private Image pusta_mapa;
        private Image przeszkoda_mapa;
        private Image gracz_mapa;
        private Image tlo_mapa;

        private Image przyciskImageZamknij;

        private int numer_efektu;

        //Zmienne dotyczące rysowania
        private System.Drawing.Imaging.ImageAttributes transparentPink;

        //Rectangle kamera;
        //Point pozycja_kamery; //Lewy górny róg  kamery

        Image tlo;
        /// <summary>
        /// Konstruktor klasy nowej gry.
        /// </summary>
        public NowaGra()
        {
            InitializeComponent();

            minX = 0;
            minY = 0;
            //Kamera.Szerokosc_Ekranu = this.Width;
            //Kamera.Wysokosc_Ekranu = this.Height;
            maxX = Kamera.Szerokosc_Ekranu;
            maxY = Kamera.Wysokosc_Ekranu;

            czas_minuty = 0;
            czas_sekundy = 0;

            //Dostajemy się do resource wkompilowaniego w aplikacje
            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();
            //Ustawianie koloru przeźroczystości
            transparentPink = new System.Drawing.Imaging.ImageAttributes();
            transparentPink.SetColorKey(Color.Pink, Color.Pink);
            

            Kamera.Prostokat_Kamery.X = 400;
            Kamera.Prostokat_Kamery.Y = 680;

            i_bateria = new Image[7];

            tlo = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tlo.png"));

            i_bateria[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_100.png"));
            i_bateria[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_85.png"));
            i_bateria[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_70.png"));
            i_bateria[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_55.png"));
            i_bateria[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_40.png"));
            i_bateria[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_25.png"));
            i_bateria[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.bateria_10.png"));

            i_rect = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.transp_rect2.png"));
            radar = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Radar.png"));
            i_menu = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.menu.png"));

            pusta_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.pusta_mapa.png"));
            przeszkoda_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.przeszkoda.png"));
            gracz_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.gracz.png"));
            tlo_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.tlo_mapa.png"));

            przyciskImageZamknij = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.przycisk.png"));


            numer_efektu = 0;
            licznik_informacji = 1;

            fabryka = new Fabryka(execAssem, true);
            plansza = new Plansza(1000, 1000);
            plansza.WczytajGrafikePodloza(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_1.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_3.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_4.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_5.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_6.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_7.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_8.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_9.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_10.png"))
                );
            plansza.GenerujDebugMapa(fabryka);

            player = Fabryka.ProdukujDomyslnegoGracza(execAssem);
            warsztat = new Warsztat(execAssem);
            warsztat.UstawDomyslneWartosci();
            warsztat.UstawStatystyki(player);

            //Ustawiamy początkowy GUI 
            panelEnergii = true;
            panelRadaru = true;
            panelInformacji = true;

            przyciskMapy = true;
            panelMapy = false;

            przyciskOpcji = true;
            panelOpcji = false;

            this.timer1.Enabled = true;
            this.czas_rozgrywki.Enabled = true;
            this.czas_efektow.Enabled = true;
            this.czas_informacji.Enabled = true;
            this.czas_odswiezania.Enabled = true;

            graWczytana = true;
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
            #region Reakcja Na Wciśnięty Klawisz
            if (wcisniety_klawisz != null)
            {
                switch (wcisniety_klawisz.KeyCode)
                {
                    
                    case System.Windows.Forms.Keys.Up:
                        {
                            if ((player.wymiary.Y + player.wymiary.Height / 2) > (Kamera.Prostokat_Kamery.Y + maxY / 2))
                            {
                                player.Ruch(Czolg.Kierunek.GORA, plansza);
                                
                            }
                            else
                            {
                                Kamera.Prostokat_Kamery.Y -= player.Szybkosc;
                                if (Kamera.Prostokat_Kamery.Y <= minY) Kamera.Prostokat_Kamery.Y = minY;//zmien pozycje kamery
                                player.Ruch(Czolg.Kierunek.GORA, plansza);
                                
                            }
                        } break;
                    case System.Windows.Forms.Keys.Down:
                        {
                            if ((player.wymiary.Y + player.wymiary.Height / 2) < (Kamera.Prostokat_Kamery.Y + maxY / 2))
                            {
                                player.Ruch(Czolg.Kierunek.DOL, plansza);
                            }
                            else
                            {
                                Kamera.Prostokat_Kamery.Y += player.Szybkosc;
                                if (Kamera.Prostokat_Kamery.Y + maxY >= plansza.Wysokosc)
                                    Kamera.Prostokat_Kamery.Y = plansza.Wysokosc - maxY;
                                player.Ruch(Czolg.Kierunek.DOL, plansza);
                            }
                        } break;
                    case System.Windows.Forms.Keys.Left:
                        {
                            if ((player.wymiary.X + player.wymiary.Width / 2) > (Kamera.Prostokat_Kamery.X + maxX / 2))
                            {
                                player.Ruch(Czolg.Kierunek.LEWO, plansza);
                            }
                            else
                            {
                                Kamera.Prostokat_Kamery.X -= player.Szybkosc;
                                if (Kamera.Prostokat_Kamery.X <= minX) Kamera.Prostokat_Kamery.X = minX;

                                player.Ruch(Czolg.Kierunek.LEWO, plansza);
                            }
                        } break;
                    case System.Windows.Forms.Keys.Right:
                        {
                            if ((player.wymiary.X + player.wymiary.Height / 2) < (Kamera.Prostokat_Kamery.X + maxX / 2))
                            {
                                player.Ruch(Czolg.Kierunek.PRAWO, plansza);
                            }
                            else
                            {
                                Kamera.Prostokat_Kamery.X += player.Szybkosc;
                                if (Kamera.Prostokat_Kamery.X + maxX >= plansza.Szerokosc)
                                    Kamera.Prostokat_Kamery.X = plansza.Szerokosc - maxX;

                                player.Ruch(Czolg.Kierunek.PRAWO, plansza);
                            }
                        } break;
                    case System.Windows.Forms.Keys.Enter:
                        {
                            player.Strzelaj(fabryka);
                        } break;
                    case Keys.Space:
                        {
                            plansza.ZmienPodloze();
                        } break;
                    case Keys.C:
                        {
                            player.punkty += 2000;
                            player.pieniadze += 2000;
                        } break;
                    case Keys.X:
                        {
                            player.Uszkodz(20);
                        } break;
                }
                //kamera = new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, maxX, maxY);

            }
            #endregion Reakcja Na Wciśnięty Klawisz
        }

        //Metoda do testowania dzialania bateri energii
        //Testowane za pomocą spacji
        private void zmniejsz_energie()
        {
            player.aktualna_wytrzymalosc -= 1;
            if (player.aktualna_wytrzymalosc <= 0)
            {
                player.aktualna_wytrzymalosc = player.Wytrzymalosc;
                player.energia -= 1;
                if (player.energia <= 0) player.energia = 0;
            }
        }
        /// <summary>
        /// Nadpisujemy OnPaintBackground() żeby nie przerysowywane było tło, zapobiega to miganiu obrazu
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
        private Bitmap bitmapBuffor = null;
        private void NowaGra_Paint(object sender, PaintEventArgs e)
        {
            //Tworzymy nowy buffor jezeli potrzebny.
            if (bitmapBuffor == null)
            {
                bitmapBuffor = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            //Tutaj rysujemy cala grafike
            using (Graphics g = Graphics.FromImage(bitmapBuffor))
            {
                //g.Clear(Color.White);
                g.DrawImage(plansza.AktualnePodloze(), 0, 0);
                player.Rysuj(g, transparentPink);

                plansza.RysujElementy(g, transparentPink);
                plansza.RysujEfekty(g, transparentPink);
                RysujInterfejs(g, transparentPink);
                g.Dispose();
            }
            e.Graphics.DrawImage(bitmapBuffor, 0, 0);

        }

        private Bitmap bufforMapa = null;
        private void RysujInterfejs(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            if (panelRadaru)
            {
                #region RysowanieMiniMapy
                Rectangle prostokat = new Rectangle(170, 20, 50, 50);
                //Rectangle radar_rect = new Rectangle(145, 0, 100, 100);
                Rectangle prostokat2 = new Rectangle(169, 19, 51, 51);

                g.DrawRectangle(new Pen(Color.Blue), prostokat2);
                g.DrawImage(i_rect, prostokat, 0, 0, i_rect.Width, i_rect.Height, GraphicsUnit.Pixel, transparentPink);
                //g.DrawImage(radar, radar_rect, 0, 0, radar.Width, radar.Height, GraphicsUnit.Pixel, transparentPink);
                g.FillEllipse(new SolidBrush(Color.White), new Rectangle(prostokat.X + ((int)player.wymiary.X / 20), prostokat.Y + ((int)player.wymiary.Y / 20), 2, 2));
                if (plansza.przeciwnicy_na_mapie != null)
                {
                    for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; i++)
                    {
                        g.FillEllipse(new SolidBrush(Color.Yellow), new Rectangle(prostokat.X + ((int)plansza.przeciwnicy_na_mapie[i].wymiary.X / 20),
                            prostokat.Y + ((int)plansza.przeciwnicy_na_mapie[i].wymiary.Y / 20), 2, 2));
                    }
                }

                g.DrawRectangle(new Pen(Color.Red), prostokat.X + (int)Kamera.Prostokat_Kamery.X / 20, prostokat.Y + (int)Kamera.Prostokat_Kamery.Y / 20, 12, 13);
                #endregion RysowanieMiniMapy
            }
            if (panelInformacji)
            {
                #region RysowanieInformacji
                g.DrawImage(i_menu, new Rectangle(minX, maxY - 30, 240, 30), 0, 0, i_menu.Width, i_menu.Height, GraphicsUnit.Pixel, transparentPink);

                if (licznik_informacji == 1) g.DrawString(s_poziom, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
                if (licznik_informacji == 2) g.DrawString(s_czas, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
                if (licznik_informacji == 3) g.DrawString(s_przeciwnicy, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);
                if (licznik_informacji == 4) g.DrawString(s_punkty, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(minX, maxY - 25, maxX, 25), drawFormat);

                #endregion RysowanieInformacji
            }
            if (panelEnergii)
            {
                #region RysowanieBaterii
                int procent_wytrzymalosci = (player.aktualna_wytrzymalosc * 100) / player.Wytrzymalosc;
                if (player.energia == 3)
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
                if (player.energia == 2)
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
                if (player.energia == 1)
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
            if (przyciskMapy)
            {
                #region RysowaniePrzyciskuMapa

                przyciskMapyProst = new Rectangle(180, maxY - 30, 60, 30);
                Rectangle prostokatPrzycisk2 = new Rectangle(180, maxY - 25, 60, 15);

                g.DrawImage(przyciskImageZamknij, przyciskMapyProst, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);

                g.DrawString("MAPA", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow), prostokatPrzycisk2, drawFormat);

                #endregion RysowaniePrzyciskuMapa
            }
            if (panelMapy)
            {
                #region RysowanieMapy
                if (narysowanaMapa == false)
                {
                    bufforMapa = new Bitmap(ClientSize.Width, ClientSize.Height);
                    using (Graphics graph = Graphics.FromImage(bufforMapa))
                    {
                        graph.DrawImage(tlo_mapa, 0, 0);

                        //graph.DrawRectangle(new Pen(Color.Blue), new Rectangle(19, 20, 201, 30));
                        //graph.FillRectangle(new SolidBrush(Color.Black), new Rectangle(20, 21, 200, 29));
                        graph.DrawString("Mapa", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Yellow),
                            new Rectangle(19, 5, 201, 25), drawFormat);
                        graph.DrawRectangle(new Pen(Color.Blue), new Rectangle(19, 39, 201, 201));

                        graph.DrawImage(pusta_mapa, new Rectangle(20, 40, 200, 200), 0, 0, pusta_mapa.Width, pusta_mapa.Height,
                            GraphicsUnit.Pixel, transparentPink);
                        //graph.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, 200, 200));
                        /*for (int i = 0; i < plansza.przeszkody.Count; i++)
                        {
                            graph.DrawImage(przeszkoda_mapa, plansza.przeszkody[i].wymiary.X / 5 + 20,
                                plansza.przeszkody[i].wymiary.Y / 5 + 40);
                            /*graph.DrawRectangle(new Pen(Color.Red, 1), new Rectangle(plansza.przeszkody[i].wymiary.X / 5,
                                plansza.przeszkody[i].wymiary.Y / 5, plansza.przeszkody[i].wymiary.Width / 5,
                                plansza.przeszkody[i].wymiary.Height / 5));
                            
                        }*/
                        plansza.region.RysujMape(graph, przeszkoda_mapa);
                        graph.DrawImage(gracz_mapa, player.wymiary.X / 5 + 20, player.wymiary.Y / 5 + 40);
                        graph.Dispose();
                    }

                    g.DrawImage(bufforMapa, 0, 0);
                    narysowanaMapa = true;

                }
                else
                {
                    g.DrawImage(bufforMapa, 0, 0);
                }
                #endregion RysowanieMapy

                #region RysowaniePrzyciskuZamknieciaMapy
                przyciskZamknijMapeProst = new Rectangle(20, 285, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskZamknijMapeProst, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Zamknij Mapę", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow), new Rectangle(20, 290, 200, 25), drawFormat);
                #endregion RysowaniePrzyciskuZamknieciaMapy
            }

            if (przyciskOpcji)
            {
                #region RysowaniePrzyciskuOpcji

                przyciskOpcjiProst = new Rectangle(0, maxY - 30, 60, 30);
                Rectangle prostokatPrzycisk2 = new Rectangle(0, maxY - 25, 60, 15);

                g.DrawImage(przyciskImageZamknij, przyciskOpcjiProst, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);

                g.DrawString("OPCJE", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow), prostokatPrzycisk2, drawFormat);

                #endregion RysowaniePrzyciskuOpcji
            }
            if (panelOpcji)
            {
                #region RysowaniePaneluOpcji
                g.DrawImage(tlo_mapa, 0, 0);

                //g.DrawRectangle(new Pen(Color.Blue), new Rectangle(19, 20, 201, 30));
                //g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(20, 21, 200, 29));
                g.DrawString("Opcje", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 5, 201, 25), drawFormat);
                #endregion RysowaniePaneluOpcji

                #region Rysowanie Przycisku Radaru
                przyciskWylaczRadar = new Rectangle(20, 70, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskWylaczRadar, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                if (panelRadaru) g.DrawString("Wyłącz Radar", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Green),
                     new Rectangle(20, 75, 200, 25), drawFormat);
                else g.DrawString("Włącz Radar", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Red),
                    new Rectangle(20, 75, 200, 25), drawFormat);
                #endregion Rysowanie Przycisku Radaru

                #region Rysowanie Przycisku Energii
                przyciskWylaczEnergie = new Rectangle(20, 110, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskWylaczEnergie, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                if (panelEnergii) g.DrawString("Wyłącz Energie", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Green),
                     new Rectangle(20, 115, 200, 25), drawFormat);
                else g.DrawString("Włącz Energie", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Red),
                    new Rectangle(20, 115, 200, 25), drawFormat);
                #endregion Rysowanie Przycisku Energii

                #region Rysowanie Przycisku Informacji
                przyciskWylaczInformacje = new Rectangle(20, 150, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskWylaczInformacje, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                if (panelInformacji) g.DrawString("Wyłącz Informacje", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Green),
                     new Rectangle(20, 155, 200, 25), drawFormat);
                else g.DrawString("Włącz Informacje", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Red),
                    new Rectangle(20, 155, 200, 25), drawFormat);
                #endregion Rysowanie Przycisku Informacji

                #region Rysowanie Przycisku Cieniowania
                przyciskWylaczCieniowanie = new Rectangle(20, 190, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskWylaczCieniowanie, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                if (Opcje.wlacz_cieniowanie) g.DrawString("Wyłącz Cieniowanie", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Green),
                     new Rectangle(20, 195, 200, 25), drawFormat);
                else g.DrawString("Włącz Cieniowanie", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Red),
                    new Rectangle(20, 195, 200, 25), drawFormat);
                #endregion Rysowanie Przycisku Cieniowania

                #region Rysowanie Przycisku Wyjscia
                przyciskWyjscia = new Rectangle(20, 230, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskWyjscia, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Zakończ Grę", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(20, 235, 200, 25), drawFormat);
                #endregion Rysowanie Przycisku Wyjscia

                #region Rysowanie Przycisku Zamkniecia Opcji
                przyciskZamknijOpcjeProst = new Rectangle(20, 285, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskZamknijOpcjeProst, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Zamknij Opcje", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(20, 290, 200, 25), drawFormat);
                #endregion Rysowanie Przycisku Zamkniecia Opcji
            }
            if (panelUlepszen)
            {
                #region Rysowanie Panelu Ulepszeń
                g.DrawImage(tlo_mapa, 0, 0);

                g.DrawString("Warsztat", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 5, 201, 25), drawFormat);
                g.DrawString("Ilość pieniędzy: " + player.pieniadze, new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Green),
                    new Rectangle(19, 30, 201, 20), drawFormat);

                warsztat.Rysuj(g, transparentPink);

                #endregion Rysowanie Panelu Ulepszeń


            }
            if (player.zginales)
            {
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(5, 120, 230, 30));
                g.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 120, 230, 30));
                g.DrawString("Zostałeś Zniszczony!", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Black),
                    new Rectangle(5, 122, 230, 26), drawFormat);
            }

            if (plansza.ukonczony_poziom)
            {
                g.DrawImage(tlo_mapa, 0, 0);

                g.DrawString(s_poziom + " Ukończony", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 5, 201, 25), drawFormat);

                g.DrawString(s_czas, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 35, 201, 25), drawFormat);

                g.DrawString(s_punkty, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 65, 201, 25), drawFormat);

                przyciskZamknijUkonczonyPoziom = new Rectangle(20, 285, 200, 30);
                g.DrawImage(przyciskImageZamknij, przyciskZamknijUkonczonyPoziom, 0, 0, przyciskImageZamknij.Width,
                    przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Przejdź Dalej", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(20, 290, 200, 25), drawFormat);
            }

        }

        #region Timery
        private void czas_efektow_Tick(object sender, EventArgs e)
        {
            //Metody zmieniają stan(state) efektów i sprawdzają czy została wykonana podana ilość animacji, jeżeli tak, to usuwają ten efekt.
            plansza.ZmienStanEfektow();
            plansza.SprawdzEfekty();
            numer_efektu += 1;
            if (numer_efektu > 4) numer_efektu = 0;
        }
        private void czas_informacji_Tick(object sender, EventArgs e)
        {
            licznik_informacji += 1;
            if (licznik_informacji > 4) licznik_informacji = 1;

            //Jeżeli gracz zginął to po 3 sekundach wyłączy informację, że zginął
            if (player.zginales == true)
            {
                player.zginales = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            zmienPozycjeGracza();
            player.RuchPocisku(plansza, fabryka);
            plansza.RuszPrzeciwnikow(fabryka, player);
            if (plansza.przeciwnicy_na_mapie.Count == 0 && plansza.przeciwnicy.Count > 0)
            {
                plansza.Respawn(player);
            }
            if (czas_sekundy % 20 == 0)
            {
                plansza.Respawn(player);
            }

            if (player.energia <= 0)
            {
                //Wykonanie kończenia gry i zliczenia punktów
            }
            if ((plansza.przeciwnicy.Count + plansza.przeciwnicy_na_mapie.Count) == 0)
            {
                plansza.ukonczony_poziom = true;
                ZliczPunkty();
                WstrzymajGre();

                //Wykonaj kończenie poziomu: zliczenie punktów; ulepszenia; nowy poziom;
            }
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
        private void czas_odswiezania_Tick(object sender, EventArgs e)
        {
            s_poziom = "Poziom: " + plansza.poziom;
            s_przeciwnicy = "Przeciwnicy: " + (plansza.przeciwnicy.Count + plansza.przeciwnicy_na_mapie.Count);
            s_punkty = "Punkty: " + (player.punkty + plansza.zdobyte_punkty);

            //Kamera.Szerokosc_Ekranu = this.ClientSize.Width;
            //Kamera.Wysokosc_Ekranu = this.ClientSize.Height;
            //Kamera.Prostokat_Kamery.Width = 320;
            //Kamera.Prostokat_Kamery.Height = 320;
            //Invalidate(new Rectangle(0, 0, Kamera.Szerokosc_Ekranu, Kamera.Wysokosc_Ekranu));
            Invalidate();
        }

        #endregion Timery

        private void NowaGra_Click(object sender, EventArgs e)
        {
            //Kontrola graficznego interfejsu użytkownika

            Rectangle mysz = new Rectangle(MousePosition.X, MousePosition.Y, 1, 1);

            #region Mapa
            if (przyciskMapy)
            {
                if (mysz.IntersectsWith(przyciskMapyProst))
                {
                    panelMapy = true;           //włączamy rysowanie mapy
                    narysowanaMapa = false;     //ustawiamy, że mapa nie jest jeszcze narysowana
                    przyciskMapy = false;       //wyłączamy przycisk
                    przyciskOpcji = false;
                    //przyciskZamknijMape = true;  //włączmy przycisk zamknięcia mapy
                    przyciskMapyProst = new Rectangle();   //zerujemy wymiary po nacisnieciu

                    WstrzymajGre();
                }
            }
            if (panelMapy)
            {
                if (mysz.IntersectsWith(przyciskZamknijMapeProst))
                {
                    panelMapy = false;
                    przyciskMapy = true;
                    przyciskOpcji = true;
                    //przyciskZamknijMape = false;
                    przyciskZamknijMapeProst = new Rectangle(); //zerujemy wymiary po nacisnieciu

                    WznowGre();

                }
            }

            #endregion Mapa

            #region Opcje
            if (przyciskOpcji)
            {
                if (mysz.IntersectsWith(przyciskOpcjiProst))
                {
                    panelOpcji = true;           //włączamy rysowanie mapy
                    przyciskMapy = false;       //wyłączamy przycisk
                    przyciskOpcji = false;
                    //przyciskZamknijOpcje = true;  //włączmy przycisk zamknięcia mapy
                    przyciskOpcjiProst = new Rectangle();   //zerujemy wymiary po nacisnieciu

                    WstrzymajGre();
                }
            }
            if (panelOpcji)
            {
                if (mysz.IntersectsWith(przyciskWylaczRadar))
                {
                    panelRadaru = !panelRadaru;
                }
                if (mysz.IntersectsWith(przyciskWylaczEnergie))
                {
                    panelEnergii = !panelEnergii;
                }
                if (mysz.IntersectsWith(przyciskWylaczInformacje))
                {
                    panelInformacji = !panelInformacji;
                }
                if (mysz.IntersectsWith(przyciskWylaczCieniowanie))
                {
                    Opcje.wlacz_cieniowanie = !Opcje.wlacz_cieniowanie;
                }
                if (mysz.IntersectsWith(przyciskWyjscia))
                {
                    //Tutaj powinny być obsłużone jeszcze metody wyjścia z gry
                    //np. Zliczenie punktów
                    Owner.Show();   //Włącza Menu Główne
                    this.Close();   //Zamyka okno Nowa Gra
                }
                if (mysz.IntersectsWith(przyciskZamknijOpcjeProst))
                {
                    panelOpcji = false;
                    przyciskMapy = true;
                    przyciskOpcji = true;

                    przyciskZamknijOpcjeProst = new Rectangle();   //zerujemy wymiary po nacisnieciu
                    przyciskWylaczRadar = new Rectangle();
                    przyciskWylaczEnergie = new Rectangle();
                    przyciskWylaczInformacje = new Rectangle();
                    przyciskWyjscia = new Rectangle();
                    przyciskWylaczCieniowanie = new Rectangle();

                    WznowGre();
                }
            }
            #endregion Opcje

            #region Ulepszenia
            if (panelUlepszen)
            {
                if (mysz.IntersectsWith(warsztat.przyciskZamknijUlepszenia))
                {
                    panelUlepszen = false;
                    warsztat.UstawStatystyki(player);
                    warsztat.przyciskZamknijUlepszenia = new Rectangle();
                    czas_minuty = 0;
                    czas_sekundy = 0;
                    plansza.GenerujDebugMapa(fabryka);
                    player.UstawPozycje(420, 925);
                    Kamera.Prostokat_Kamery.X = 400;
                    Kamera.Prostokat_Kamery.Y = 680;
                    WznowGre();
                }
                if (mysz.IntersectsWith(warsztat.przyciskUlepszSzybkosc))
                {
                    warsztat.ZwiekszPoziomSzybkosci(player);
                    warsztat.przyciskUlepszSzybkosc = new Rectangle();
                }
                if (mysz.IntersectsWith(warsztat.przyciskUlepszPancerz))
                {
                    warsztat.ZwiekszPoziomPancerza(player);
                    warsztat.przyciskUlepszPancerz = new Rectangle();
                }
                if (mysz.IntersectsWith(warsztat.przyciskUlepszAtak))
                {
                    warsztat.ZwiekszPoziomAtaku(player);
                    warsztat.przyciskUlepszAtak = new Rectangle();
                }
                if (mysz.IntersectsWith(warsztat.przyciskUlepszMur))
                {
                    warsztat.ZwiekszPoziomMuru(player);
                    warsztat.przyciskUlepszMur = new Rectangle();
                }
            }
            #endregion Ulepszenia

            #region Ukończony Poziom
            if (plansza.ukonczony_poziom)
            {
                if (mysz.IntersectsWith(przyciskZamknijUkonczonyPoziom))
                {
                    plansza.ukonczony_poziom = false;
                    panelUlepszen = true;
                    przyciskZamknijUkonczonyPoziom = new Rectangle();
                }
            }
            #endregion Ukończony Poziom
        }

        private void WstrzymajGre()
        {
            this.timer1.Enabled = false;
            this.czas_rozgrywki.Enabled = false;
        }
        private void WznowGre()
        {
            this.timer1.Enabled = true;
            this.czas_rozgrywki.Enabled = true;
        }
        private void ZliczPunkty()
        {
            int mnoznik_punktowy = 1;
            player.punkty += plansza.zdobyte_punkty * mnoznik_punktowy;
            player.pieniadze += plansza.zdobyte_punkty * mnoznik_punktowy;
        }

    }
}