using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Rudy_103.src
{
    class Przeciwnik : Czolg, ICloneable
    {
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila) { }
        //public override void Rysuj(PaintEventArgs e) { }

        public object Clone()
        {
            Przeciwnik klon = new Przeciwnik(0, 0, this.CollisonDetectRect.Width, this.CollisonDetectRect.Height, this.wytrzymalosc, this.szybkosc, this.sila);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
    }
}
