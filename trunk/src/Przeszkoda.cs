using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    class Przeszkoda : Obiekty, ICloneable
    {
        private int energia;
        private bool transparent;
        public Przeszkoda(int X, int Y, int Szer, int Wys, bool transparent)
            : base(X, Y, Szer, Wys)
        {
            energia = 100;
            this.transparent = transparent;
        }
        public Przeszkoda(int X, int Y, int Szer, int Wys, bool transparent, params Image[] obrazy)
            : base(X, Y, Szer, Wys, obrazy)
        {
            energia = 100;
            this.transparent = transparent;
        }
        public override void Rysuj(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[0], new Rectangle(Wymiary.X - pozycja_kamery.X, Wymiary.Y - pozycja_kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[0].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
        }
        public object Clone()
        {
            Przeszkoda klon = new Przeszkoda(0, 0, this.Wymiary.Width, this.Wymiary.Height, transparent);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        public void UstawPozycje(int X, int Y)
        {
            Wymiary.X = X; Wymiary.Y = Y;
        }
    }
}
