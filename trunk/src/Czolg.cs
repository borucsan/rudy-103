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
        protected Pocisk pocisk;
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys)
        {
            this.wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
        }
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, params Image[] obrazy)
            : base(X, Y, Szer, Wys, obrazy)
        {
            this.wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
        }
        /// <summary>
        /// Metoda do poruszania czołgami
        /// </summary>
        /// <param name="kierunek">Enumeracja reprezentująca kierunek</param>
        /// <param name="rozmiar_mapy">Struktura rozmiaru mapy</param>
        public void Ruch(Kierunek kierunek, Point rozmiar_mapy)
        {
            this.kierunek = kierunek;
            switch (kierunek)
            {
                case Kierunek.GORA:
                    if (Wymiary.Y >= szybkosc) Wymiary.Y -= szybkosc;
                    else Wymiary.Y = 0;
                    break;
                case Kierunek.PRAWO:
                    if (Wymiary.X <= rozmiar_mapy.X - (szybkosc + Wymiary.Width)) Wymiary.X += szybkosc;
                    else Wymiary.X = rozmiar_mapy.X - Wymiary.Width;
                    break;
                case Kierunek.DOL:
                    if (Wymiary.Y <= rozmiar_mapy.Y - (szybkosc + Wymiary.Height)) Wymiary.Y += szybkosc;
                    else Wymiary.Y = rozmiar_mapy.Y - Wymiary.Height;
                    break;
                case Kierunek.LEWO:
                    if (Wymiary.X >= szybkosc) Wymiary.X -= szybkosc;
                    else Wymiary.X = 0;
                    break;
            }
        }
        public void Strzelaj(Fabryka fabryka)
        {
            if (pocisk == null)
            {
                pocisk = fabryka.ProdukujPocisk();
                pocisk.UstawPocisk(Wymiary.X + Wymiary.Width/2, Wymiary.Y + Wymiary.Height/2, kierunek);
            }
        }
        public void RuchPocisku()
        {
            if (pocisk != null)
            {
                switch (pocisk.kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pocisk.wymiary.Y > 0)
                        {
                            pocisk.ZmienPozycje(0, -pocisk.szybkosc);
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.wymiary.X < 1000)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pocisk.wymiary.Y < 1000)
                        {
                            pocisk.ZmienPozycje(0, pocisk.szybkosc);
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pocisk.wymiary.X > 0)
                        {
                            pocisk.ZmienPozycje(-pocisk.szybkosc, 0);
                        }
                        else pocisk = null;
                        break;
                }
            }
        }
        public enum Kierunek : int { GORA=0, PRAWO, DOL, LEWO }
        public override void Rysuj(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            //Tutaj jest cos nie tak bo nie dokonca Wymiary dzialaja, 
            //50, 50 to jest rozmiar prostokata ktory jest wyswietlany
            //240, 240 jest to rozmiar obrazka, ktory jest przeskalowywany do 50, 50
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - pozycja_kamery.X, Wymiary.Y - pozycja_kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            if (pocisk != null) pocisk.Rysuj(g, pozycja_kamery, transparentPink);
        }
          
    }
}