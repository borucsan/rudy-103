using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsMobile;
using Microsoft.WindowsMobile.Status;

namespace Rudy_103.src
{
    /// <summary>
    /// NowaGra - jest to klasa okna nowej gry.
    /// </summary>
    public partial class Gra : Form
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

        private int czas_respawnow;
        private int czas_strzalow;

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

        private int numer_efektu;

        private int ilosc_opcji;
        
        /// <summary>
        /// Konstruktor klasy nowej gry.
        /// </summary>
        public Gra()
        {
            InitializeComponent();

            czas_minuty = 0;
            czas_sekundy = 0;
            czas_respawnow = 0;
            czas_strzalow = 0;
            //Dostajemy się do resource wkompilowaniego w aplikacje
            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();

            

            numer_efektu = 0;
            licznik_informacji = 1;

            fabryka = new Fabryka(execAssem, true);
            warsztat = new Warsztat(execAssem);
            warsztat.UstawDomyslneWartosci();
            try
            {
                string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/Mapy/City.xml";
                plansza = Plansza.WczytajMape(path, 1, fabryka, warsztat.poziom_muru);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd wczytywania mapy!\n" + ex.Message, "Błąd wczytywania mapy", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }
            //plansza = new Plansza(1000, 1000);
            plansza.WczytajGrafikePodloza(Multimedia.tlo);
            //plansza.GenerujDebugMapa(fabryka);
            player = Fabryka.ProdukujDomyslnegoGracza(execAssem);
            warsztat.UstawStatystyki(player);
            Kamera.Szerokosc_Ekranu = this.Width;
            Kamera.Wysokosc_Ekranu = this.Height;
            Kamera.Prostokat_Kamery.X = plansza.Szerokosc/2 - Kamera.Szerokosc_Ekranu/2;
            Kamera.Prostokat_Kamery.Y = plansza.Wysokosc - Kamera.Wysokosc_Ekranu;
            Kamera.Odswiez_Kamere();
            //Ustawiamy początkowy GUI 
            panelEnergii = true;
            panelRadaru = true;
            panelInformacji = true;

            przyciskMapy = true;
            panelMapy = false;

            przyciskOpcji = true;
            panelOpcji = false;

            ilosc_opcji = 6;

            this.timer1.Enabled = true;
            this.czas_rozgrywki.Enabled = true;
            this.czas_efektow.Enabled = true;
            this.czas_informacji.Enabled = true;
            this.czas_odswiezania.Enabled = true;
            //this.timer2.Enabled = true;

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
            Opcje.Gora = false;
            Opcje.Prawo = false;
            Opcje.Dol = false;
            Opcje.Lewo = false;
            Opcje.Enter = false;
        }

        private void ReakcjaWirtualnychKlawiszy()
        {

            if (Opcje.Gora)
            {
                if ((player.Wymiary.Y + player.Wymiary.Height / 2) > (Kamera.Prostokat_Kamery.Y + Kamera.Wysokosc_Ekranu / 2))
                {
                    player.Ruch(Czolg.Kierunek.GORA, plansza);

                }
                else
                {
                    Kamera.Prostokat_Kamery.Y -= player.Szybkosc;
                    if (Kamera.Prostokat_Kamery.Y <= 0) Kamera.Prostokat_Kamery.Y = 0;//zmien pozycje kamery
                    player.Ruch(Czolg.Kierunek.GORA, plansza);

                }
            }
            if (Opcje.Prawo)
            {
                if ((player.Wymiary.X + player.Wymiary.Height / 2) < (Kamera.Prostokat_Kamery.X + Kamera.Szerokosc_Ekranu / 2))
                {
                    player.Ruch(Czolg.Kierunek.PRAWO, plansza);
                }
                else
                {
                    Kamera.Prostokat_Kamery.X += player.Szybkosc;
                    if (Kamera.Prostokat_Kamery.X + Kamera.Szerokosc_Ekranu >= plansza.Szerokosc)
                        Kamera.Prostokat_Kamery.X = plansza.Szerokosc - Kamera.Szerokosc_Ekranu;

                    player.Ruch(Czolg.Kierunek.PRAWO, plansza);
                }
            }
            if (Opcje.Dol)
            {
                if ((player.Wymiary.Y + player.Wymiary.Height / 2) < (Kamera.Prostokat_Kamery.Y + Kamera.Wysokosc_Ekranu / 2))
                {
                    player.Ruch(Czolg.Kierunek.DOL, plansza);
                }
                else
                {
                    Kamera.Prostokat_Kamery.Y += player.Szybkosc;
                    if (Kamera.Prostokat_Kamery.Y + Kamera.Wysokosc_Ekranu >= plansza.Wysokosc)
                        Kamera.Prostokat_Kamery.Y = plansza.Wysokosc - Kamera.Wysokosc_Ekranu;
                    player.Ruch(Czolg.Kierunek.DOL, plansza);
                }
            }
            if (Opcje.Lewo)
            {
                if ((player.Wymiary.X + player.Wymiary.Width / 2) > (Kamera.Prostokat_Kamery.X + Kamera.Szerokosc_Ekranu / 2))
                {
                    player.Ruch(Czolg.Kierunek.LEWO, plansza);
                }
                else
                {
                    Kamera.Prostokat_Kamery.X -= player.Szybkosc;
                    if (Kamera.Prostokat_Kamery.X <= 0) Kamera.Prostokat_Kamery.X = 0;

                    player.Ruch(Czolg.Kierunek.LEWO, plansza);
                }
            }
            if (Opcje.Enter)
            {
                player.Strzelaj(fabryka, czas_strzalow);
            }
            
            
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
                            Opcje.Gora = true;
                        } break;
                    case System.Windows.Forms.Keys.Down:
                        {
                            Opcje.Dol = true;
                        } break;
                    case System.Windows.Forms.Keys.Left:
                        {
                            Opcje.Lewo = true;
                        } break;
                    case System.Windows.Forms.Keys.Right:
                        {
                            Opcje.Prawo = true;
                        } break;
                    case System.Windows.Forms.Keys.Enter:
                        {
                            Opcje.Enter = true;
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
                    case Keys.Z:
                        {
                            panelUlepszen = !panelUlepszen;
                        } break;
                    case Keys.V:
                        {
                            plansza.przeciwnicy.Clear();
                            plansza.przeciwnicy_na_mapie.Clear();
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
            player.Wytrzymalosc -= 1;
            if (player.Wytrzymalosc <= 0)
            {
                player.Wytrzymalosc = player.Wytrzymalosc_Bazowa;
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
                bitmapBuffor = new Bitmap(Kamera.Szerokosc_Ekranu, Kamera.Wysokosc_Ekranu);
            }

            //Tutaj rysujemy cala grafike
            using (Graphics g = Graphics.FromImage(bitmapBuffor))
            {
                g.Clear(Color.Gray);
                if (Kamera.Szerokosc_Ekranu > plansza.AktualnePodloze().Width)
                {
                    g.DrawImage(plansza.AktualnePodloze(), (Kamera.Szerokosc_Ekranu-plansza.AktualnePodloze().Width)/2, 0);
                }
                else
                {
                    g.DrawImage(plansza.AktualnePodloze(), 0, 0);
                }

                plansza.RysujElementy(player,g, Narzedzia.transparentPink);
                plansza.RysujEfekty(g, Narzedzia.transparentPink);
                RysujInterfejs(g, Narzedzia.transparentPink);
                g.Dispose();
            }
            e.Graphics.DrawImage(bitmapBuffor, 0, 0);

        }

        private Bitmap bufforMapa = null;
        private void RysujInterfejs(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            if (Opcje.wlaczonePrzyciskiEkranowe)
            {
                
                Opcje.przyciskGora = new Rectangle(Kamera.Szerokosc_Ekranu - 45 - 12, Kamera.Wysokosc_Ekranu - 120, 25, 25);
                Opcje.przyciskEnter = new Rectangle(Kamera.Szerokosc_Ekranu - 45 - 12, Kamera.Wysokosc_Ekranu - 120 + 30, 25, 25);
                Opcje.przyciskDol = new Rectangle(Kamera.Szerokosc_Ekranu - 45 - 12, Kamera.Wysokosc_Ekranu - 120 + 30 + 30, 25, 25);
                Opcje.przyciskLewo = new Rectangle(Kamera.Szerokosc_Ekranu - 75 - 12, Kamera.Wysokosc_Ekranu - 120 + 30, 25, 25);
                Opcje.przyciskPrawo = new Rectangle(Kamera.Szerokosc_Ekranu - 45 - 12 + 30, Kamera.Wysokosc_Ekranu - 120 + 30, 25, 25);
                if (Opcje.Gora) g.DrawImage(Multimedia.interfejs_buttonUp[1], Opcje.przyciskGora, 0, 0, Multimedia.interfejs_buttonUp[1].Width, Multimedia.interfejs_buttonUp[1].Height, GraphicsUnit.Pixel,
                    transparentPink);
                else g.DrawImage(Multimedia.interfejs_buttonUp[0], Opcje.przyciskGora, 0, 0, Multimedia.interfejs_buttonUp[0].Width, Multimedia.interfejs_buttonUp[0].Height, GraphicsUnit.Pixel,
                    transparentPink);
                if (Opcje.Prawo) g.DrawImage(Multimedia.interfejs_buttonRight[1], Opcje.przyciskPrawo, 0, 0, Multimedia.interfejs_buttonRight[1].Width, Multimedia.interfejs_buttonRight[1].Height, GraphicsUnit.Pixel,
                    transparentPink);
                else g.DrawImage(Multimedia.interfejs_buttonRight[0], Opcje.przyciskPrawo, 0, 0, Multimedia.interfejs_buttonRight[0].Width, Multimedia.interfejs_buttonRight[0].Height, GraphicsUnit.Pixel,
                    transparentPink);
                if (Opcje.Dol) g.DrawImage(Multimedia.interfejs_buttonDown[1], Opcje.przyciskDol, 0, 0, Multimedia.interfejs_buttonDown[1].Width, Multimedia.interfejs_buttonDown[1].Height, GraphicsUnit.Pixel,
                    transparentPink);
                else g.DrawImage(Multimedia.interfejs_buttonDown[0], Opcje.przyciskDol, 0, 0, Multimedia.interfejs_buttonDown[0].Width, Multimedia.interfejs_buttonDown[0].Height, GraphicsUnit.Pixel,
                    transparentPink);
                if (Opcje.Lewo) g.DrawImage(Multimedia.interfejs_buttonLeft[1], Opcje.przyciskLewo, 0, 0, Multimedia.interfejs_buttonLeft[1].Width, Multimedia.interfejs_buttonLeft[1].Height, GraphicsUnit.Pixel,
                    transparentPink);
                else g.DrawImage(Multimedia.interfejs_buttonLeft[0], Opcje.przyciskLewo, 0, 0, Multimedia.interfejs_buttonLeft[0].Width, Multimedia.interfejs_buttonLeft[0].Height, GraphicsUnit.Pixel,
                    transparentPink);
                if (Opcje.Enter) g.DrawImage(Multimedia.interfejs_buttonEnter[1], Opcje.przyciskEnter, 0, 0, Multimedia.interfejs_buttonEnter[1].Width, Multimedia.interfejs_buttonEnter[1].Height, GraphicsUnit.Pixel,
                    transparentPink);
                else g.DrawImage(Multimedia.interfejs_buttonEnter[0], Opcje.przyciskEnter, 0, 0, Multimedia.interfejs_buttonEnter[0].Width, Multimedia.interfejs_buttonEnter[0].Height, GraphicsUnit.Pixel,
                    transparentPink);


            }
            if (Opcje.wlacz_informacje)
            {
                g.DrawRectangle(new Pen(Color.Black), new Rectangle(0, 40, 50, 110));
                if (Opcje.Obraz_Przeciwnika != null)
                {
                    g.DrawImage(Opcje.Obraz_Przeciwnika, new Rectangle(5, 41, 40, 40), 0, 0, Opcje.Obraz_Przeciwnika.Width, Opcje.Obraz_Przeciwnika.Height,
                        GraphicsUnit.Pixel, transparentPink);
                    
                    g.DrawRectangle(new Pen(Color.Black), new Rectangle(4, 81, 42, 5));
                    g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(5, 82, (40*Opcje.przeciwnik_wytrzymalosc)/100, 4));

                    g.DrawImage(Multimedia.ImageAtak, new Rectangle(1, 90, 15, 15), 0, 0, Multimedia.ImageAtak.Width, Multimedia.ImageAtak.Height,
                        GraphicsUnit.Pixel, transparentPink);
                    g.DrawString("" + Opcje.poziom_sila + "lvl", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Red),
                        new RectangleF(10, 90, 40, 15), drawFormat);
                    g.DrawImage(Multimedia.ImagePancerz, new Rectangle(1, 105, 15, 15), 0, 0, Multimedia.ImageAtak.Width, Multimedia.ImageAtak.Height,
                        GraphicsUnit.Pixel, transparentPink);
                    g.DrawString("" + Opcje.poziom_wytrzymalosc + "lvl", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Red),
                        new RectangleF(10, 105, 40, 15), drawFormat);
                    g.DrawImage(Multimedia.ImageSzybkosc, new Rectangle(1, 120, 15, 15), 0, 0, Multimedia.ImageAtak.Width, Multimedia.ImageAtak.Height,
                        GraphicsUnit.Pixel, transparentPink);
                    g.DrawString("" + Opcje.poziom_szybkosc + "lvl", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Red),
                        new RectangleF(10, 120, 40, 15), drawFormat);
                    g.DrawString("" + Opcje.przeciwnik_punkty + "pkt", new Font("Arial", 8, FontStyle.Regular), new SolidBrush(Color.Red),
                        new RectangleF(1, 135, 50, 15), drawFormat);
                }
            }

            if (panelRadaru)
            {
                #region RysowanieMiniMapy
                int szerokosc_radaru = plansza.Szerokosc / 10;
                int wysokosc_radaru = plansza.Wysokosc / 10;
                Rectangle prostokatRadaru = new Rectangle(Kamera.Szerokosc_Ekranu - (szerokosc_radaru+1), 1, szerokosc_radaru, wysokosc_radaru);
                //Rectangle radar_rect = new Rectangle(Kamera.Szerokosc_Ekranu - 51, 1, 50, 50);
                Rectangle prostokatObwoduRadaru = new Rectangle(Kamera.Szerokosc_Ekranu - (szerokosc_radaru + 2), 0, (szerokosc_radaru + 1), (wysokosc_radaru + 1));

                g.DrawRectangle(new Pen(Color.Blue), prostokatObwoduRadaru);
                g.DrawImage(Multimedia.interfejs_pole_radaru, prostokatRadaru, 0, 0, Multimedia.interfejs_pole_radaru.Width, Multimedia.interfejs_pole_radaru.Height, GraphicsUnit.Pixel, transparentPink);
                //g.DrawImage(radar, radar_rect, 0, 0, radar.Width, radar.Height, GraphicsUnit.Pixel, transparentPink);
                g.FillEllipse(new SolidBrush(Color.White), new Rectangle(prostokatRadaru.X + ((int)player.Wymiary.X / 10), prostokatRadaru.Y + ((int)player.Wymiary.Y / 10), 4, 4));
                if (plansza.przeciwnicy_na_mapie != null)
                {
                    for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; i++)
                    {
                        g.FillEllipse(new SolidBrush(Color.Yellow), new Rectangle(prostokatRadaru.X + ((int)plansza.przeciwnicy_na_mapie[i].Wymiary.X / 10),
                            prostokatRadaru.Y + ((int)plansza.przeciwnicy_na_mapie[i].Wymiary.Y / 10), 4, 4));
                    }
                }

                g.DrawRectangle(new Pen(Color.Red), prostokatRadaru.X + (int)Kamera.Prostokat_Kamery.X / 10, prostokatRadaru.Y + (int)Kamera.Prostokat_Kamery.Y / 10, (int)Kamera.Prostokat_Kamery.Width / 10, (int)Kamera.Prostokat_Kamery.Height / 10);
                #endregion RysowanieMiniMapy
            }
            if (panelInformacji)
            {
                #region RysowanieInformacji
                g.DrawImage(Multimedia.interfejs_informacje, new Rectangle(Kamera.Szerokosc_Ekranu / 2 - Multimedia.interfejs_informacje.Width / 2, Kamera.Wysokosc_Ekranu - 30, Multimedia.interfejs_informacje.Width, 30), 0, 0, Multimedia.interfejs_informacje.Width, Multimedia.interfejs_informacje.Height, GraphicsUnit.Pixel, transparentPink);

                if (licznik_informacji == 1) g.DrawString(s_poziom, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(0, Kamera.Wysokosc_Ekranu - 25, Kamera.Szerokosc_Ekranu, 25), drawFormat);
                if (licznik_informacji == 2) g.DrawString(s_czas, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(0, Kamera.Wysokosc_Ekranu - 25, Kamera.Szerokosc_Ekranu, 25), drawFormat);
                if (licznik_informacji == 3) g.DrawString(s_przeciwnicy, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(0, Kamera.Wysokosc_Ekranu - 25, Kamera.Szerokosc_Ekranu, 25), drawFormat);
                if (licznik_informacji == 4) g.DrawString(s_punkty, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(0, Kamera.Wysokosc_Ekranu - 25, Kamera.Szerokosc_Ekranu, 25), drawFormat);

                #endregion RysowanieInformacji
            }
            if (panelEnergii)
            {
                #region Lifes
                Rectangle prostokat3 = new Rectangle(5, 0, 30, 30);
                for (int i = 0; i < player.energia; i++)
                {
                    g.DrawImage(Multimedia.polska_gracz[0], prostokat3, 0, 0, Multimedia.polska_gracz[0].Width, Multimedia.polska_gracz[0].Height, GraphicsUnit.Pixel, transparentPink);
                    prostokat3.X += 35;
                }
                #endregion Lifes
                
                #region RysowanieBaterii
                /*
                int procent_wytrzymalosci = (player.Wytrzymalosc * 100) / player.Wytrzymalosc_Bazowa;
                if (player.energia == 3)
                {
                    Rectangle prostokat3 = new Rectangle(0, 0, 18, 36);
                    g.DrawImage(Multimedia.interfejs_bateria[0], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    prostokat3.X += 20;
                    g.DrawImage(Multimedia.interfejs_bateria[0], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    prostokat3.X += 20;
                    if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[0], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[1], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[2], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[3], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[4], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[5], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 10)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[6], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                }
                if (player.energia == 2)
                {
                    Rectangle prostokat3 = new Rectangle(0, 0, 18, 36);
                    g.DrawImage(Multimedia.interfejs_bateria[0], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);

                    prostokat3.X += 20;
                    if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[0], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[1], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[2], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[3], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[4], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[5], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 10)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[6], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                }
                if (player.energia == 1)
                {
                    Rectangle prostokat3 = new Rectangle(0, 0, 18, 36);
                    if (procent_wytrzymalosci <= 100 && procent_wytrzymalosci > 85)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[0], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 85 && procent_wytrzymalosci > 70)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[1], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 70 && procent_wytrzymalosci > 55)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[2], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 55 && procent_wytrzymalosci > 40)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[3], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 40 && procent_wytrzymalosci > 25)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[4], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 25 && procent_wytrzymalosci > 10)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[5], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                    if (procent_wytrzymalosci <= 10)
                    {
                        g.DrawImage(Multimedia.interfejs_bateria[6], prostokat3, 0, 0, Multimedia.interfejs_bateria[0].Width, Multimedia.interfejs_bateria[0].Height, GraphicsUnit.Pixel, transparentPink);
                    }
                }
                */
                #endregion RysowanieBaterii
            }
            if (przyciskMapy)
            {
                #region RysowaniePrzyciskuMapa

                przyciskMapyProst = new Rectangle(Kamera.Szerokosc_Ekranu / 2 + Multimedia.interfejs_informacje.Width / 2, Kamera.Wysokosc_Ekranu - 30, 60, 30);
                Rectangle prostokatPrzycisk2 = new Rectangle(Kamera.Szerokosc_Ekranu / 2 + Multimedia.interfejs_informacje.Width / 2, Kamera.Wysokosc_Ekranu - 25, 60, 15);

                g.DrawImage(Multimedia.przyciskImageZamknij, przyciskMapyProst, 0, 0, Multimedia.przyciskImageZamknij.Width,
                    Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);

                g.DrawString("MAPA", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow), prostokatPrzycisk2, drawFormat);

                #endregion RysowaniePrzyciskuMapa
            }
            if (panelMapy)
            {
                #region RysowanieMapy
                if (narysowanaMapa == false)
                {
                    bufforMapa = new Bitmap(Kamera.Szerokosc_Ekranu, Kamera.Wysokosc_Ekranu);
                    using (Graphics graph = Graphics.FromImage(bufforMapa))
                    {
                        if (Kamera.Szerokosc_Ekranu > Multimedia.tlo_mapa.Width)
                        {
                            graph.DrawImage(Multimedia.tlo_mapa, (Kamera.Szerokosc_Ekranu - Multimedia.tlo_mapa.Width) / 2, 0);
                        }
                        else
                        {
                            graph.DrawImage(Multimedia.tlo_mapa, 0, 0);
                        }
                       
                        //graph.DrawRectangle(new Pen(Color.Blue), new Rectangle(19, 20, 201, 30));
                        //graph.FillRectangle(new SolidBrush(Color.Black), new Rectangle(20, 21, 200, 29));
                        graph.DrawString("Mapa", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Yellow),
                            new Rectangle(Kamera.Szerokosc_Ekranu/2-101, 1, 200, 25), drawFormat);
                        graph.DrawRectangle(new Pen(Color.Blue), new Rectangle(Kamera.Szerokosc_Ekranu/2-101, 29, 201, 201));

                        graph.DrawImage(Multimedia.pusta_mapa, new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 100, 30, 200, 200), 0, 0, Multimedia.pusta_mapa.Width, Multimedia.pusta_mapa.Height,
                            GraphicsUnit.Pixel, transparentPink);

                        plansza.region.RysujMape(graph, Multimedia.przeszkoda_mapa);
                        graph.DrawImage(Multimedia.gracz_mapa, player.Wymiary.X / 5 + Kamera.Szerokosc_Ekranu / 2 - 100, player.Wymiary.Y / 5 + 30);
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
                przyciskZamknijMapeProst = new Rectangle(Kamera.Szerokosc_Ekranu/2 - 70, Kamera.Wysokosc_Ekranu - 30, 140, 30);
                g.DrawImage(Multimedia.przyciskImageZamknij, przyciskZamknijMapeProst, 0, 0, Multimedia.przyciskImageZamknij.Width,
                    Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Zamknij Mapę", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow), przyciskZamknijMapeProst, drawFormat);
                #endregion RysowaniePrzyciskuZamknieciaMapy
            }

            if (przyciskOpcji)
            {
                #region RysowaniePrzyciskuOpcji

                przyciskOpcjiProst = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - Multimedia.interfejs_informacje.Width / 2 - 60, Kamera.Wysokosc_Ekranu - 30, 60, 30);
                Rectangle prostokatPrzycisk2 = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - Multimedia.interfejs_informacje.Width / 2 - 60, Kamera.Wysokosc_Ekranu - 25, 60, 15);

                g.DrawImage(Multimedia.przyciskImageZamknij, przyciskOpcjiProst, 0, 0, Multimedia.przyciskImageZamknij.Width,
                    Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);

                g.DrawString("OPCJE", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow), prostokatPrzycisk2, drawFormat);

                #endregion RysowaniePrzyciskuOpcji
            }
            if (panelOpcji)
            {
                #region RysowaniePaneluOpcji
                g.Clear(Color.Gray);
                if (Kamera.Szerokosc_Ekranu > Multimedia.tlo_mapa.Width)
                {
                    g.DrawImage(Multimedia.tlo_mapa, (Kamera.Szerokosc_Ekranu - Multimedia.tlo_mapa.Width) / 2, 0);
                }
                else
                {
                    g.DrawImage(Multimedia.tlo_mapa, 0, 0);
                }
                
                g.DrawString("Opcje", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(Kamera.Szerokosc_Ekranu/2 - 50, 0, 100, 20), drawFormat);
                #endregion RysowaniePaneluOpcji

                int wysokosc_elementu = (Kamera.Wysokosc_Ekranu - 20 - (ilosc_opcji*5) ) / ilosc_opcji;
                
                int aktualna_wysokosc = 25;
                    
                    #region Rysowanie Przycisku Radaru
                    przyciskWylaczRadar = new Rectangle(Kamera.Szerokosc_Ekranu/2 - 100, aktualna_wysokosc, 200, 30);
                    g.DrawImage(Multimedia.przyciskImageZamknij, przyciskWylaczRadar, 0, 0, Multimedia.przyciskImageZamknij.Width,
                        Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                    if (panelRadaru) g.DrawString("Wyłącz Radar", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Green),
                         przyciskWylaczRadar, drawFormat);
                    else g.DrawString("Włącz Radar", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Red),
                        przyciskWylaczRadar, drawFormat);
                    aktualna_wysokosc += 35;
                    #endregion Rysowanie Przycisku Radaru

                    #region Rysowanie Przycisku Energii
                    przyciskWylaczEnergie = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 100, aktualna_wysokosc, 200, 30);
                    g.DrawImage(Multimedia.przyciskImageZamknij, przyciskWylaczEnergie, 0, 0, Multimedia.przyciskImageZamknij.Width,
                        Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                    if (panelEnergii) g.DrawString("Wyłącz Energie", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Green),
                         przyciskWylaczEnergie, drawFormat);
                    else g.DrawString("Włącz Energie", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Red),
                        przyciskWylaczEnergie, drawFormat);
                    aktualna_wysokosc += 35;
                    #endregion Rysowanie Przycisku Energii

                    #region Rysowanie Przycisku Informacji
                    przyciskWylaczInformacje = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 100, aktualna_wysokosc, 200, 30);
                    g.DrawImage(Multimedia.przyciskImageZamknij, przyciskWylaczInformacje, 0, 0, Multimedia.przyciskImageZamknij.Width,
                        Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                    if (panelInformacji) g.DrawString("Wyłącz Informacje", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Green),
                         przyciskWylaczInformacje, drawFormat);
                    else g.DrawString("Włącz Informacje", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Red),
                        przyciskWylaczInformacje, drawFormat);
                    aktualna_wysokosc += 35;
                    #endregion Rysowanie Przycisku Informacji

                    #region Rysowanie Przycisku Cieniowania
                    przyciskWylaczCieniowanie = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 100, aktualna_wysokosc, 200, 30);
                    g.DrawImage(Multimedia.przyciskImageZamknij, przyciskWylaczCieniowanie, 0, 0, Multimedia.przyciskImageZamknij.Width,
                        Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                    if (Opcje.wlacz_cieniowanie) g.DrawString("Wyłącz Cieniowanie", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Green),
                         przyciskWylaczCieniowanie, drawFormat);
                    else g.DrawString("Włącz Cieniowanie", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Red),
                        przyciskWylaczCieniowanie, drawFormat);
                    aktualna_wysokosc += 35;
                    #endregion Rysowanie Przycisku Cieniowania

                    #region Rysowanie Przycisku Wyjscia
                    przyciskWyjscia = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 100, aktualna_wysokosc, 200, 30);
                    g.DrawImage(Multimedia.przyciskImageZamknij, przyciskWyjscia, 0, 0, Multimedia.przyciskImageZamknij.Width,
                        Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                    g.DrawString("Zakończ Grę", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                        przyciskWyjscia, drawFormat);
                    aktualna_wysokosc += 35;
                    #endregion Rysowanie Przycisku Wyjscia

                    #region Rysowanie Przycisku Zamkniecia Opcji
                    przyciskZamknijOpcjeProst = new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 100, aktualna_wysokosc, 200, 30);
                    g.DrawImage(Multimedia.przyciskImageZamknij, przyciskZamknijOpcjeProst, 0, 0, Multimedia.przyciskImageZamknij.Width,
                        Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                    g.DrawString("Zamknij Opcje", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                        przyciskZamknijOpcjeProst, drawFormat);
                    aktualna_wysokosc += 35;
                    #endregion Rysowanie Przycisku Zamkniecia Opcji
               
            }
            if (panelUlepszen)
            {
                #region Rysowanie Panelu Ulepszeń
                g.Clear(Color.Black);
                g.DrawString("Ilość pieniędzy: " + player.pieniadze, new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Green),
                    new Rectangle(Kamera.Prostokat_Kamery.Width/2 - 100, 0, 200, 20), drawFormat);

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
                g.Clear(Color.Black);

                g.DrawString(s_poziom + " Ukończony", new Font("Arial", 14, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(Kamera.Prostokat_Kamery.Width/2-100, 5, 200, 25), drawFormat);

                g.DrawString(s_czas, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, 35, 200, 25), drawFormat);

                g.DrawString("Punkty: "+player.punkty, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, 65, 200, 25), drawFormat);

                przyciskZamknijUkonczonyPoziom = new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, 
                    Kamera.Prostokat_Kamery.Height - 40, 200, 30);
                g.DrawImage(Multimedia.przyciskImageZamknij, przyciskZamknijUkonczonyPoziom, 0, 0, Multimedia.przyciskImageZamknij.Width,
                    Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Przejdź Dalej", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, Kamera.Prostokat_Kamery.Height - 30, 200, 25), drawFormat);
            }
            /*
            if (player.energia <= 0)
            {
                g.DrawImage(Grafika.tlo_mapa, 0, 0);

                g.DrawString("Game Over", new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 5, 201, 25), drawFormat);

                g.DrawString(s_czas, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 35, 201, 25), drawFormat);

                g.DrawString("Punkty: " + player.punkty, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(19, 65, 201, 25), drawFormat);

                przyciskZamknijKoniecGry = new Rectangle(20, 285, 200, 30);
                g.DrawImage(Grafika.przyciskImageZamknij, przyciskZamknijKoniecGry, 0, 0, Grafika.przyciskImageZamknij.Width,
                    Grafika.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
                g.DrawString("Wyjdź", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                    new Rectangle(20, 290, 200, 25), drawFormat);
            }
            */
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
            ReakcjaWirtualnychKlawiszy();
            plansza.RuszPrzeciwnikow(fabryka, player);
            plansza.RusziSprawdz(player, fabryka, czas_strzalow);
            player.RuchPocisku(plansza, fabryka);
            if (czas_respawnow % 150 == 0 || (plansza.przeciwnicy_na_mapie.Count == 0 && plansza.przeciwnicy.Count > 0))
            {
                plansza.Respawn(player);
                czas_respawnow = 1;
            }
            if (player.energia <= 0)
            {
                GameOver();
                //Wykonanie kończenia gry i zliczenia punktów
            }
            if ((plansza.przeciwnicy.Count + plansza.przeciwnicy_na_mapie.Count) == 0)
            {
                plansza.ukonczony_poziom = true;
                player.Pociski.Clear();
                ZliczPunkty();
                WstrzymajGre();
                //Wykonaj kończenie poziomu: zliczenie punktów; ulepszenia; nowy poziom;
            }
            czas_respawnow++;
            czas_strzalow++;
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

            OdswiezEkran();
        }

        #endregion Timery

        private void NowaGra_Click(object sender, EventArgs e)
        {
            //Kontrola graficznego interfejsu użytkownika

            Rectangle mysz = new Rectangle(MousePosition.X, MousePosition.Y, 1, 1);
            #region Informacje o Przeciwniku
            if (przyciskMapy && przyciskOpcji) //Sprawdza czy jesteśmy w głównym interfejsie
            {
                if (mysz.IntersectsWith(new Rectangle(player.Wymiary.X - Kamera.Prostokat_Kamery.X, player.Wymiary.Y - Kamera.Prostokat_Kamery.Y, 
                    player.Wymiary.Width, player.Wymiary.Height)))
                {
                    Opcje.WylaczInformacje();
                    
                }
                for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; i++)
                {
                    if (mysz.IntersectsWith(new Rectangle(plansza.przeciwnicy_na_mapie[i].Wymiary.X - Kamera.Prostokat_Kamery.X,
                        plansza.przeciwnicy_na_mapie[i].Wymiary.Y - Kamera.Prostokat_Kamery.Y, plansza.przeciwnicy_na_mapie[i].Wymiary.Width,
                        plansza.przeciwnicy_na_mapie[i].Wymiary.Height)))
                    {
                        Opcje.Nazwa_Przeciwnika = "Przeciwnik";
                        Opcje.Obraz_Przeciwnika = plansza.przeciwnicy_na_mapie[i].ZwrocObrazy();
                        Opcje.wlacz_informacje = true;
                        Opcje.poziom_wytrzymalosc = (plansza.przeciwnicy_na_mapie[i].Wytrzymalosc_Bazowa - 10) / 10;
                        Opcje.poziom_sila = (plansza.przeciwnicy_na_mapie[i].Sila - 10) / 10;
                        Opcje.poziom_szybkosc = (plansza.przeciwnicy_na_mapie[i].Szybkosc - 5);
                        Opcje.przeciwnik_wytrzymalosc = (plansza.przeciwnicy_na_mapie[i].Wytrzymalosc * 100) / plansza.przeciwnicy_na_mapie[i].Wytrzymalosc_Bazowa;
                        Opcje.przeciwnik_punkty = plansza.przeciwnicy_na_mapie[i].punkty;
                    }
                }
            }
            #endregion Informacje o Przeciwniku

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
                    GameOver();
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
                    czas_respawnow = 0;
                    int poziom = plansza.poziom + 1;
                    try
                    {
                        string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/Mapy/City.xml";
                        plansza = Plansza.WczytajMape(path, poziom, fabryka, warsztat.poziom_muru);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd wczytywania mapy!\n" + ex.Message, "Błąd wczytywania mapy", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        this.Close();
                        return;
                    }
                    plansza.WczytajGrafikePodloza(Multimedia.tlo);
                    player.UstawPozycje(Gracz.PunktRespGracza.X + 5, Gracz.PunktRespGracza.Y + 5);
                    Kamera.Prostokat_Kamery.X = Gracz.PunktRespGracza.X - 25;
                    Kamera.Prostokat_Kamery.Y = Gracz.PunktRespGracza.Y - 275;
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

        private void OdswiezEkran()
        {
            //Landscape
            if (SystemState.DisplayRotation == 0)
            {
                if (Kamera.Orientacja_Ekranu.Equals("Landscape") == false)
                {
                    Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
                    Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;
                    this.bitmapBuffor = null;
                    narysowanaMapa = false;
                    this.bufforMapa = null;
                    Kamera.Orientacja_Ekranu = "Landscape";
                }
                //Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
                //Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;
            }
            //Portrait
            if (SystemState.DisplayRotation == 90 || SystemState.DisplayRotation == -90)
            {
                if (Kamera.Orientacja_Ekranu.Equals("Portrait") == false)
                {
                    Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
                    Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;
                    this.bitmapBuffor = null;
                    narysowanaMapa = false;
                    this.bufforMapa = null;
                    Kamera.Orientacja_Ekranu = "Portrait";
                }
            }
            Kamera.Odswiez_Kamere();
            Invalidate();
        }

        private void NowaGra_MouseDown(object sender, MouseEventArgs e)
        {
            #region Graficzne Sterowanie
            Rectangle mysz = new Rectangle(MousePosition.X, MousePosition.Y, 1, 1);
            if (Opcje.wlaczonePrzyciskiEkranowe)
            {
                if (mysz.IntersectsWith(Opcje.przyciskGora))
                {
                    Opcje.Gora = true;
                }
                if (mysz.IntersectsWith(Opcje.przyciskPrawo))
                {
                    Opcje.Prawo = true;
                }
                if (mysz.IntersectsWith(Opcje.przyciskDol))
                {
                    Opcje.Dol = true;
                }
                if (mysz.IntersectsWith(Opcje.przyciskLewo))
                {
                    Opcje.Lewo = true;
                }
                if (mysz.IntersectsWith(Opcje.przyciskEnter))
                {
                    Opcje.Enter = true;
                }

            }
            #endregion Graficzne Sterowanie
        }

        private void NowaGra_MouseUp(object sender, MouseEventArgs e)
        {
            Opcje.Gora = false;
            Opcje.Prawo = false;
            Opcje.Dol = false;
            Opcje.Lewo = false;
            Opcje.Enter = false;
        }
        private void GameOver()
        {
            ZliczPunkty();
            WstrzymajGre();
            Multimedia.audio_game_over.Play();
            KoniecGry koniec = new KoniecGry(player.punkty);
            koniec.Owner = this.Owner;
            koniec.Show();
            this.Close();
        }
    }
}