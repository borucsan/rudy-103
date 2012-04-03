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
        public int energia { get; set; }
        public int aktualna_wytrzymalosc { get; set; }
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int energia)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila)
        {
            this.punkty = 0;
            this.energia = energia;
            this.aktualna_wytrzymalosc = wytrzymalosc;
        }
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int energia, params Image[] obrazy)
            : this(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, energia)
        {
            this.obrazy = obrazy;
        }
        public new void Uszkodz(int sila)
        {
            this.aktualna_wytrzymalosc = this.aktualna_wytrzymalosc - sila;
        }
        //public override void Rysuj(PaintEventArgs e) { }
    }
}
