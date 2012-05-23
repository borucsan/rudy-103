﻿using System;
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
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[0], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[0].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
        }
        public void RysujCienie(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            Point[] points = new Point[4];
            points[0].X = Wymiary.X - Kamera.Prostokat_Kamery.X;
            points[0].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;
            points[1].X = Wymiary.X + 10 - Kamera.Prostokat_Kamery.X;
            points[1].Y = Wymiary.Y + Wymiary.Height + Wymiary.Height / 3 - Kamera.Prostokat_Kamery.Y;
            points[2].X = Wymiary.X + Wymiary.Width + 10 - Kamera.Prostokat_Kamery.X;
            points[2].Y = Wymiary.Y + Wymiary.Height + Wymiary.Height / 3 - Kamera.Prostokat_Kamery.Y;
            points[3].X = Wymiary.X + Wymiary.Width - Kamera.Prostokat_Kamery.X;
            points[3].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;

            g.FillPolygon(new SolidBrush(Color.Black), points);
        }
        public object Clone()
        {
            Przeszkoda klon = new Przeszkoda(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.energia, transparent);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        public override bool Uszkodz(int sila)
        {
            this.energia = this.energia - sila;
            return this.energia <= 0;
        }
    }
}
