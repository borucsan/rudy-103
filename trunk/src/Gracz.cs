using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class Gracz : Czolg
    {
        private int punkty;
        private int energia;
        private int obecna_wytrzymalosc;
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int energia)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila)
        {
            this.punkty = 0;
            this.energia = energia;
        }
        public Gracz(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int energia, params Image[] obrazy)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, obrazy)
        {
            this.punkty = 0;
            this.energia = energia;
        }
        //public override void Rysuj(PaintEventArgs e) { }
    }
}
