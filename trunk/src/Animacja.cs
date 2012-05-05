using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class Animacja : Obiekty, ICloneable
    {
        public int aktualny_stan;
        public int ilosc_stanow;
        public int ilosc_wykonan_animacji;
        public int wykonane_animacje;

        public Animacja(int X, int Y, int Szer, int Wys, int _ilosc_stanow, int _ilosc_wykonan_animacji)
            : base(X, Y, Szer, Wys)
        {
            this.ilosc_stanow = _ilosc_stanow;
            this.aktualny_stan = 1;
            this.wykonane_animacje = 0;
            this.ilosc_wykonan_animacji = _ilosc_wykonan_animacji;
        }

        public Animacja(int X, int Y, int Szer, int Wys, int _ilosc_stanow, int _ilosc_wykonan_animacji, params Image[] obrazy)
            : this(X, Y, Szer, Wys, _ilosc_stanow, _ilosc_wykonan_animacji)
        {
            this.wykonane_animacje = 0;
            this.aktualny_stan = 1;
            this.obrazy = obrazy;
        }
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[aktualny_stan - 1], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[aktualny_stan - 1].Width, obrazy[aktualny_stan - 1].Height, GraphicsUnit.Pixel, transparentPink);

        }
        //musi być wywoływane razem z metodą rysuj
        public void ZmienStan()
        {
            this.aktualny_stan += 1;
            if (this.aktualny_stan > this.ilosc_stanow)
            {
                this.aktualny_stan = 1;
                this.wykonane_animacje += 1;
            }
        }
        public object Clone()
        {
            Animacja klon = new Animacja(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.ilosc_stanow, this.ilosc_wykonan_animacji);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        
    }
}
