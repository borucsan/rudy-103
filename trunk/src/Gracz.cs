using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class Gracz : Czolg
    {
        public int punkty { get; set; }
        public int pieniadze { get; set; }
        public int energia { get; set; }
        //public int aktualna_wytrzymalosc { get; set; }
        public bool zginales { get; set; }
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int energia)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie)
        {
            this.punkty = 0;
            this.pieniadze = 0;
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
    }
}