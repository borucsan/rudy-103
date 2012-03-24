using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa bazowa dla czołgów (Gracza, Przeciników)
    /// </summary>
    abstract class Czolg : Obiekty
    {
        protected int wytrzymalosc;
        protected int szybkosc;
        protected int sila;
        protected Kierunek kierunek;
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys)
        {
            this.wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
        }
        public void Ruch(Kierunek kierunek, Point rozmiar_mapy)
        {
            this.kierunek = kierunek;
            switch (kierunek)
            {
                case Kierunek.GORA:
                    if (Pozycja.Y >= szybkosc) Pozycja.Y -= szybkosc;
                    else Pozycja.Y = 0;
                    break;
                case Kierunek.PRAWO:
                    if (Pozycja.X <= rozmiar_mapy.X - (szybkosc + CollisonDetectRect.Width)) Pozycja.X += szybkosc;
                    else Pozycja.X = rozmiar_mapy.X - CollisonDetectRect.Width;
                    break;
                case Kierunek.DOL:
                    if (Pozycja.Y <= rozmiar_mapy.Y - (szybkosc + CollisonDetectRect.Width)) Pozycja.Y += szybkosc;
                    else Pozycja.Y = rozmiar_mapy.Y - CollisonDetectRect.Width;
                    break;
                case Kierunek.LEWO:
                    if (Pozycja.X >= szybkosc) Pozycja.Y -= szybkosc;
                    else Pozycja.X = 0;
                    break;
            }
        }
        public void Strzelaj()
        {

        }
        public enum Kierunek { GORA, PRAWO, DOL, LEWO }
        public override void Rysuj(PaintEventArgs e){}
          
    }
}
