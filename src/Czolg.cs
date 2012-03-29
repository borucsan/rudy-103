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
        protected int aktualna_wytrzymalosc;
        protected int szybkosc;
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
        public int AktualnaWytrzymalosc
        {
            get
            {
                return aktualna_wytrzymalosc;
            }
            set
            {
                aktualna_wytrzymalosc = value;
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
        protected int sila;
        protected Kierunek kierunek;
        protected Pocisk pocisk;
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys)
        {
            this.wytrzymalosc = wytrzymalosc;
            this.aktualna_wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
        }
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, params Image[] obrazy)
            : base(X, Y, Szer, Wys, obrazy)
        {
            this.wytrzymalosc = wytrzymalosc;
            this.aktualna_wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
        }
        /// <summary>
        /// Metoda do poruszania czołgami
        /// </summary>
        /// <param name="kierunek">Enumeracja reprezentująca kierunek</param>
        /// <param name="plansza">Referencja obiektu mapy</param>
        public void Ruch(Kierunek kierunek, Plansza plansza)
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
                pocisk.UstawPocisk(Wymiary.X + Wymiary.Width/2, Wymiary.Y + Wymiary.Height/2, kierunek);
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
                            Trafienie(plansza);
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.wymiary.X < 1000)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                            Trafienie(plansza);
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pocisk.wymiary.Y < 1000)
                        {
                            pocisk.ZmienPozycje(0, pocisk.szybkosc);
                            Trafienie(plansza);
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pocisk.wymiary.X > 0)
                        {
                            pocisk.ZmienPozycje(-pocisk.szybkosc, 0);
                            Trafienie(plansza);
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
        public bool Zderzenie(Plansza plansza)
        {
            Rectangle pmk = StworzProstokatMozliwychKolizji(plansza);
           /* for (int i = 0; i < plansza.przeciwnicy_na_mapie.Capacity; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(pmk))
                {
                    if(plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Wymiary)) return true;
                }
            }*/
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
        public void Trafienie(Plansza plansza)
        {
            Rectangle pmk = StworzProstokatMozliwychKolizji(plansza);
            /* for (int i = 0; i < plansza.przeciwnicy_na_mapie.Capacity; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(pmk))
                {
                    if(plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Wymiary)) return true;
                }
            }*/
            for (int i = 0; i < plansza.przeszkody.Count(); ++i)
            {
                if ((plansza.przeszkody[i]).wymiary.IntersectsWith(pmk))
                {
                    if (plansza.przeszkody[i].transparent) continue;
                    if (plansza.przeszkody[i].wymiary.IntersectsWith(pocisk.wymiary))
                    {
                        if (plansza.przeszkody[i].Uszkodz(sila) <= 0)
                        {
                            plansza.przeszkody.RemoveAt(i);
                        }
                        pocisk = null;
                        return;
                    }
                }
            }
        }
        public enum Kierunek : int { GORA = 0, PRAWO, DOL, LEWO }
        public override void Rysuj(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            //Tutaj jest cos nie tak bo nie dokonca Wymiary dzialaja, 
            //50, 50 to jest rozmiar prostokata ktory jest wyswietlany
            //240, 240 jest to rozmiar obrazka, ktory jest przeskalowywany do 50, 50
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - pozycja_kamery.X, Wymiary.Y - pozycja_kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            if (pocisk != null) pocisk.Rysuj(g, pozycja_kamery, transparentPink);
        }
          
    }
}