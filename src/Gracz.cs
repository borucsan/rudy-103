using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    public class Gracz : Czolg
    {
        public int punkty { get; set; }
        public int XP_Aktualne { get; set; }
        public int XP_Potrzebne { get; set; }
        public int energia { get; set; }
        //public int aktualna_wytrzymalosc { get; set; }
        public bool zginales { get; set; }
        public int ilosc_punktow_ulepszen { get; set; }
        public int poziom { get; set; }
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int energia)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie)
        {
            this.punkty = 0;
            this.XP_Aktualne = 0;
            this.XP_Potrzebne = 100;
            this.energia = energia;
            //this.aktualna_wytrzymalosc = wytrzymalosc;
        }
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int energia, params Image[] obrazy)
            : this(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie, energia)
        {
            this.obrazy = obrazy;
        }
        public void Uszkodz(int sila, Plansza plansza)
        {
            //this.aktualna_wytrzymalosc = this.aktualna_wytrzymalosc - sila;
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
       public static Rectangle PunktRespGracza = new Rectangle(425, 925, 50, 50);

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