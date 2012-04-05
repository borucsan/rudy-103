using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa bazowa dla czołgów (Gracza, Przeciników)
    /// </summary>
    abstract class Czolg : Obiekty
    {
        protected int wytrzymalosc;
        protected int szybkosc;
        protected int sila;
        public int Wytrzymalosc
        {
            get
            {
                return wytrzymalosc;
            }
            set
            {
                wytrzymalosc = value;
            }
        }
        public int Szybkosc
        {
            get
            {
                return szybkosc;
            }
            set
            {
                szybkosc = value;
            }
        }
        public int Sila
        {
            get
            {
                return sila;
            }
            set
            {
                sila = value;
            }
        }

        protected Kierunek kierunek;
        protected Pocisk pocisk;
        public Pocisk Pocisk
        {
            get
            {
                return pocisk;
            }
            set
            {
                pocisk = value;
            }
        }
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys)
        {
            this.wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
        }
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, params Image[] obrazy)
            : this(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila)
        {
            this.obrazy = obrazy;
        }
        /// <summary>
        /// Metoda do poruszania czołgami
        /// </summary>
        /// <param name="kierunek">Enumeracja reprezentująca kierunek</param>
        /// <param name="plansza">Referencja obiektu mapy</param>
        public virtual void Ruch(Kierunek kierunek, Plansza plansza)
        {
            this.kierunek = kierunek;
            switch (kierunek)
            {
                case Kierunek.GORA:
                    if (Wymiary.Y >= szybkosc)
                    {
                        Wymiary.Y -= szybkosc;
                        if (Zderzenie(plansza))
                        {
                            Wymiary.Y = Wymiary.Y + szybkosc;
                        }
                    }
                    //else Wymiary.Y = 0;
                    break;
                case Kierunek.PRAWO:
                    if (Wymiary.X <= plansza.Szerokosc - (szybkosc + Wymiary.Width))
                    {
                        Wymiary.X += szybkosc;
                        if(Zderzenie(plansza))
                        {
                            Wymiary.X = Wymiary.X - szybkosc;
                        }
                    }
                    //else Wymiary.X = plansza.Szerokosc - Wymiary.Width;
                    break;
                case Kierunek.DOL:
                    if (Wymiary.Y <= plansza.Wysokosc - (szybkosc + Wymiary.Height))
                    {
                        Wymiary.Y += szybkosc;
                        if (Zderzenie(plansza))
                        {
                            Wymiary.Y = Wymiary.Y - szybkosc;
                        }
                    }
                    //else Wymiary.Y = plansza.Wysokosc - Wymiary.Height;
                    break;
                case Kierunek.LEWO:
                    if (Wymiary.X >= szybkosc && !Zderzenie(plansza))
                    {
                        Wymiary.X -= szybkosc;
                        if (Zderzenie(plansza))
                        {
                            Wymiary.X = Wymiary.X + szybkosc;
                        }
                    }
                    //else Wymiary.X = 0;
                    break;
            }
        }
        public void Strzelaj(Fabryka fabryka)
        {
            if (pocisk == null)
            {
                pocisk = fabryka.ProdukujPocisk();
                pocisk.UstawPocisk(Wymiary.X + Wymiary.Width/2, Wymiary.Y + Wymiary.Height/2, this.sila, kierunek);
            }
        }
        public void RuchPocisku(Plansza plansza)
        {
            if (pocisk != null)
            {
                switch (pocisk.kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pocisk.wymiary.Y > 0)
                        {
                            pocisk.ZmienPozycje(0, -pocisk.szybkosc);
                            if(pocisk.Zderzenie(plansza)) pocisk = null;
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.wymiary.X < 1000)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza)) pocisk = null;
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pocisk.wymiary.Y < 1000)
                        {
                            pocisk.ZmienPozycje(0, pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza)) pocisk = null;
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pocisk.wymiary.X > 0)
                        {
                            pocisk.ZmienPozycje(-pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza)) pocisk = null;
                        }
                        else pocisk = null;
                        break;
                }
            }
        }
        public Rectangle StworzProstokatMozliwychKolizji(Plansza plansza)
        {
            Rectangle ProstokatMozliwychKolizji = new Rectangle() ;
            switch(kierunek)
            {
                case Kierunek.GORA:
                    ProstokatMozliwychKolizji = new Rectangle(Wymiary.X, 0, Wymiary.Width, Wymiary.Y);
                break;
                case Kierunek.PRAWO:
                    ProstokatMozliwychKolizji = new Rectangle(Wymiary.X + Wymiary.Width, Wymiary.Y,plansza.Szerokosc - (Wymiary.X + Wymiary.Width), Wymiary.Height);
                break;
                case Kierunek.DOL:
                    ProstokatMozliwychKolizji = new Rectangle(Wymiary.X, Wymiary.Y + Wymiary.Height, Wymiary.Width, plansza.Wysokosc - (Wymiary.Y + Wymiary.Height));
                break;
                case Kierunek.LEWO:
                    ProstokatMozliwychKolizji = new Rectangle(0, Wymiary.Y, Wymiary.X, Wymiary.Height);
                break;
            }
            return ProstokatMozliwychKolizji;
        }
        public virtual bool Zderzenie(Plansza plansza)
        {
            Rectangle pmk = StworzProstokatMozliwychKolizji(plansza);
           for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if ((plansza.przeciwnicy_na_mapie[i]).wymiary.IntersectsWith(pmk))
                {
                    if(plansza.przeciwnicy_na_mapie[i].wymiary.IntersectsWith(Wymiary)) return true;
                }
            }
            for (int i = 0; i < plansza.przeszkody.Count(); ++i)
            {
                if ((plansza.przeszkody[i]).wymiary.IntersectsWith(pmk))
                {
                    if (plansza.przeszkody[i].transparent) continue;
                    if (plansza.przeszkody[i].wymiary.IntersectsWith(Wymiary)) return true;
                }
            }
            return false;
        }
        /*
        public bool Zderzenie2(Plansza plansza)
        {
            
            Rectangle pmk = StworzProstokatMozliwychKolizji(plansza);
            List<Przeszkoda> dosprawdzenia = (from i in plansza.przeszkody where i.wymiary.IntersectsWith(pmk) select i).ToList<Przeszkoda>;
            return false;
        }*/
        public enum Kierunek : int { GORA = 0, PRAWO, DOL, LEWO }
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            if (pocisk != null) pocisk.Rysuj(g, transparentPink);
        }
          
    }
}