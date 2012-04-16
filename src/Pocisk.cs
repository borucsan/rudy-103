using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    class Pocisk : Obiekty, ICloneable
    {
        public Czolg.Kierunek kierunek { get; set; }
        public int sila { get; set; }
        public int szybkosc { get; set; }
        public Czolg wlasciciel { get; set; }
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, Czolg.Kierunek kierunek)
            : base(X, Y, Szer, Wys)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
        }
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, Czolg.Kierunek kierunek, params Image[] obrazy)
            : base(X, Y, Szer, Wys, obrazy)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
            this.obrazy = obrazy;
        }
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
        }
        public void UstawPocisk(int X, int Y, int sila, Czolg.Kierunek kierunek, Czolg wlasciciel)
        {
            Wymiary.X = X;
            Wymiary.Y = Y;
            this.kierunek = kierunek;
            if (kierunek == Czolg.Kierunek.GORA || kierunek == Czolg.Kierunek.DOL)
            {
                Wymiary.X -= 5;
            }
            if (kierunek == Czolg.Kierunek.LEWO || kierunek == Czolg.Kierunek.PRAWO)
            {
                Wymiary.Y -= 5;
            }
            this.sila = sila;
            this.wlasciciel = wlasciciel;
        }
        public bool Zderzenie(Plansza plansza)
        {
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i].wymiary.IntersectsWith(Wymiary))
                {
                    if (plansza.przeciwnicy_na_mapie[i].Uszkodz(sila))
                    {
                        plansza.zdobyte_punkty += plansza.przeciwnicy_na_mapie[i].punkty;
                        plansza.przeciwnicy_na_mapie.RemoveAt(i);

                    }
                    return true;
                }
                if (plansza.przeciwnicy_na_mapie[i].Pocisk !=null)
                {
                    if (plansza.przeciwnicy_na_mapie[i].Pocisk.wymiary.IntersectsWith(Wymiary))
                    {
                        plansza.przeciwnicy_na_mapie[i].Pocisk = null;
                        return true;
                    }
                }
            }
            if (this.wymiary.IntersectsWith(plansza.baza.wymiary))
            {
                plansza.baza.Uszkodz(this.sila);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
        }
        public bool Zderzenie(Plansza plansza, Gracz gracz)
        {
            if (gracz.wymiary.IntersectsWith(Wymiary))
            {
                gracz.Uszkodz(sila);
                return true;
            }
            if(gracz.Pocisk != null && gracz.Pocisk.wymiary.IntersectsWith(Wymiary))
            {
                gracz.Pocisk = null;
                return true;
            }
            
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i] == this.wlasciciel) continue;
                    if (plansza.przeciwnicy_na_mapie[i].wymiary.IntersectsWith(Wymiary))
                    {
                        return true;
                    }
                if (plansza.przeciwnicy_na_mapie[i].Pocisk != null)
                {
                    if (plansza.przeciwnicy_na_mapie[i].Pocisk.wymiary.IntersectsWith(Wymiary))
                    {
                        plansza.przeciwnicy_na_mapie[i].Pocisk = null;
                        return true;
                    }
                }
            }
            if (this.wymiary.IntersectsWith(plansza.baza.wymiary))
            {
                plansza.baza.Uszkodz(this.sila);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
            
        }
        #region ICloneable Members

        public object Clone()
        {
            Pocisk klon = new Pocisk(0, 0, this.wymiary.Width, this.wymiary.Height, this.sila, this.szybkosc, Czolg.Kierunek.GORA);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }

        #endregion
    }
}
