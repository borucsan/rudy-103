using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa gracza.
    /// </summary>
    public class Gracz : Czolg
    {
        /// <summary>
        /// Ilość punktów jakie posiada gracz.(Ranking)
        /// </summary>
        public int punkty { get; set; }
        /// <summary>
        /// Ilość posiadanych punktów doświadczenia.
        /// </summary>
        public int XP_Aktualne { get; private set; }
        /// <summary>
        /// Ilość wymaganych punktów doświadczenia na następny poziom.
        /// </summary>
        public int XP_Potrzebne { get; private set; }
        /// <summary>
        /// Ilość żyć gracza.
        /// </summary>
        public int energia { get; set; }
        /// <summary>
        /// Zmienna określa czy "gracz stracił życie".
        /// </summary>
        public bool zginales { get; set; }
        /// <summary>
        /// Zmienna określa czy gracz zdobył nowy poziom.
        /// </summary>
        public bool levelUp { get; set; }
        /// <summary>
        /// Ilość punktów które można wymienić na ulepszenia.
        /// </summary>
        public int ilosc_punktow_ulepszen { get; set; }
        /// <summary>
        /// Poziom gracza.
        /// </summary>
        public int poziom { get; set; }
        /// <summary>
        /// Konstruktor obiektu gracza.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="wytrzymalosc">Wytrzymałość maksymalna czołgu.</param>
        /// <param name="szybkosc">Szybkość maksymalna czołgu.</param>
        /// <param name="sila">Siła maksymalna strzałów czołgu.</param>
        /// <param name="zasieg">Zasię maksymalny strzałów czołgu.</param>
        /// <param name="max_pociskow">Maksymalna ilość pocisków.</param>
        /// <param name="przeladowanie">Częstotliwość strzałów.</param>
        /// <param name="energia">Ilość żyć gracza.</param>
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int energia)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie)
        {
            this.punkty = 0;
            this.XP_Aktualne = 0;
            this.XP_Potrzebne = 1000;
            this.energia = energia;
        }
        /// <summary>
        /// Konstruktor obiektu gracza.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="wytrzymalosc">Wytrzymałość maksymalna czołgu.</param>
        /// <param name="szybkosc">Szybkość maksymalna czołgu.</param>
        /// <param name="sila">Siła maksymalna strzałów czołgu.</param>
        /// <param name="zasieg">Zasię maksymalny strzałów czołgu.</param>
        /// <param name="max_pociskow">Maksymalna ilość pocisków.</param>
        /// <param name="przeladowanie">Częstotliwość strzałów.</param>
        /// <param name="energia">Ilość żyć gracza.</param>
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int energia, params Image[] obrazy)
            : this(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie, energia)
        {
            this.obrazy = obrazy;
        }
        /// <summary>
        /// Metoda zmiejszająca poziom energi gracza.
        /// </summary>
        /// <param name="sila">Siła z jaką zadano uszkodzenie.</param>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <returns>true jeśli obiekt został zniszczony</returns>
        public void Uszkodz(int sila, Plansza plansza)
        {
            this.Wytrzymalosc = this.Wytrzymalosc - sila;
            if (this.Wytrzymalosc <= 0)
            {
                
                Multimedia.audio_zginales.Play();
                this.zginales = true;
                this.Wytrzymalosc = this.Wytrzymalosc_Bazowa;
                this.energia -= 1;
                this.UstawPozycje(PunktRespGracza.X + 5, PunktRespGracza.Y + 5);
                Kamera.Prostokat_Kamery.X = plansza.Szerokosc / 2 - Kamera.Szerokosc_Ekranu / 2;
                Kamera.Prostokat_Kamery.Y = plansza.Wysokosc - Kamera.Wysokosc_Ekranu;

            }
        }
        /// <summary>
        /// Metoda dodaję punkty doświadczenia.
        /// </summary>
        /// <param name="ilosc">Ilość punktów do dodania.</param>
        public void DodajXP(int ilosc)
        {
            XP_Aktualne += ilosc;
            if (XP_Aktualne >= XP_Potrzebne)
            {
                XP_Aktualne -= XP_Potrzebne;
                ++ilosc_punktow_ulepszen;
                ++poziom;
                XP_Potrzebne = poziom * 1000;
                this.levelUp = true;
            }
        }
        /// <summary>
        /// Metoda wczytująca dane z profilu.
        /// </summary>
        /// <param name="profil">Obiekt profilu gracza.</param>
        public void WczytajDaneGracza(ProfilGracza profil)
        {
            this.poziom = profil.ulepszenia.poziom_gracza;
            this.ilosc_punktow_ulepszen = profil.punkty_level;
            this.XP_Aktualne = profil.XP_Aktualne;
            this.XP_Potrzebne = profil.XP_Potrzebne;
            this.ilosc_strzalow = profil.statystkyki.liczba_strzalow;
            this.ilosc_trafien = profil.statystkyki.strzalow_celnych;
        }
        /// <summary>
        /// Określa pozycję i wymiary punktu odroczenia czołgu gracza.
        /// </summary>
       public static Rectangle PunktRespGracza = new Rectangle(425, 925, 50, 50);
        /// <summary>
        /// Metoda rysująca pasek poziomu doświadczenia.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
       public void RysujPasekXP(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
       {
           int procenty_xp = (100 * this.XP_Aktualne) / this.XP_Potrzebne;
           g.DrawRectangle(new Pen(Color.Black), new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y + Wymiary.Height + 8 - Kamera.Prostokat_Kamery.Y,
                Wymiary.Width, 5));
           g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(Wymiary.X + 1 - Kamera.Prostokat_Kamery.X, Wymiary.Y + Wymiary.Height + 9 - Kamera.Prostokat_Kamery.Y,
               (Wymiary.Width * procenty_xp) / 100, 4));
       }
    }
}