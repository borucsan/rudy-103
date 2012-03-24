using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Rudy_103.src
{
    class Pocisk : Obiekty
    {
        public Pocisk(int X, int Y, int Szer, int Wys, int sila)
            : base(X, Y, Szer, Wys)
        {
            
        }
        public override void Rysuj(PaintEventArgs e) { }
    }
}
