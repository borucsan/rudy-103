using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    class Przeciwnik : Czolg, ICloneable
    {
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila) { }
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, params Image[] obrazy)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, obrazy) { }
        //public override void Rysuj(PaintEventArgs e) { }

        public object Clone()
        {
            Przeciwnik klon = new Przeciwnik(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.wytrzymalosc, this.szybkosc, this.sila);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
    }
}
