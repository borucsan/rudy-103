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
        public int energia { get; set; }
        public bool transparent { get; set; }
        public Przeszkoda(int X, int Y, int Szer, int Wys, int energia, bool transparent)
            : base(X, Y, Szer, Wys)
        {
            this.energia = energia;
            this.transparent = transparent;
        }
        public Przeszkoda(int X, int Y, int Szer, int Wys, int energia, bool transparent, params Image[] obrazy)
            : this(X, Y, Szer, Wys, energia, transparent)
        {
            this.obrazy = obrazy;
        }
        public override void Rysuj(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[0], new Rectangle(Wymiary.X - pozycja_kamery.X, Wymiary.Y - pozycja_kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[0].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
        }
        public object Clone()
        {
            Przeszkoda klon = new Przeszkoda(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.energia, transparent);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        public int Uszkodz(int sila)
        {
            return energia = energia - sila;
        }
        public void UstawPozycje(int X, int Y)
        {
            Wymiary.X = X; Wymiary.Y = Y;
        }
    }
}
