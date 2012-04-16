using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class Baza : Przeszkoda
    {
        public bool zniszczona{ get; set; } 
        public Baza(int X, int Y, int Szer, int Wys, int energia, bool transparent)
            : base(X, Y, Szer, Wys, energia, transparent) { zniszczona = false; }
        public Baza(int X, int Y, int Szer, int Wys, int energia, bool transparent, params Image[] obrazy)
            : this(X, Y, Szer, Wys, energia, transparent){ this.obrazy = obrazy;}
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            if (zniszczona)
            {
                g.DrawImage(obrazy[1], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                            obrazy[1].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
            }
            else
            {
                g.DrawImage(obrazy[0], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                            obrazy[0].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
            }
            }
        public new void Uszkodz(int sila)
        {
            zniszczona = true;
        }
    }
}
