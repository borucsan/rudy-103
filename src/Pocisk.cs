using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    class Pocisk : Obiekty
    {
        public Czolg.Kierunek kierunek { get; set; }
        public int sila { get; set; }
        public int szybkosc { get; set; }
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, Czolg.Kierunek kierunek)
            : base(X, Y, Szer, Wys)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
        }
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, Czolg.Kierunek kierunek, params Image[] obrazy)
            : base(X, Y, Szer, Wys, obrazy)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
        }
        public override void Rysuj(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            //g.DrawImage(obrazy[(int)kierunek], Wymiary.X - pozycja_kamery.X, Wymiary.Y - pozycja_kamery.Y);
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - pozycja_kamery.X, Wymiary.Y - pozycja_kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
        }
        public void UstawPocisk(int X, int Y, Czolg.Kierunek kierunek)
        {
            Wymiary.X = X;
            Wymiary.Y = Y;
            this.kierunek = kierunek;
            if (kierunek == Czolg.Kierunek.GORA || kierunek == Czolg.Kierunek.DOL)
            {
                Wymiary.X -= 5;
            }
            if (kierunek == Czolg.Kierunek.LEWO || kierunek == Czolg.Kierunek.PRAWO)
            {
                Wymiary.Y -= 5;
            }
        }
        public void ZmienPozycje(int zmiana_X, int zmiana_Y)
        {
            Wymiary.X = Wymiary.X + zmiana_X;
            Wymiary.Y = Wymiary.Y + zmiana_Y;
        }
    }
}
