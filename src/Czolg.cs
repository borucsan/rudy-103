using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

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

        public Kierunek kierunek { get; protected set; }
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
            poprzednia_pozycja = wymiary.Location;
            switch (kierunek)
            {
                case Kierunek.GORA:
                    if (Wymiary.Y >= szybkosc)
                    {
                        Wymiary.Y -= szybkosc;
                        Zderzenie2(plansza);
                    }
                    else Wymiary.Y = 0;
                    break;
                case Kierunek.PRAWO:
                    if (Wymiary.X <= plansza.Szerokosc - (szybkosc + Wymiary.Width))
                    {
                        Wymiary.X += szybkosc;
                        Zderzenie2(plansza);
                    }
                    else Wymiary.X = plansza.Szerokosc - Wymiary.Width;
                    break;
                case Kierunek.DOL:
                    if (Wymiary.Y <= plansza.Wysokosc - (szybkosc + Wymiary.Height))
                    {
                        Wymiary.Y += szybkosc;
                        Zderzenie2(plansza);
                    }
                    else Wymiary.Y = plansza.Wysokosc - Wymiary.Height;
                    break;
                case Kierunek.LEWO:
                    if (Wymiary.X >= szybkosc)
                    {
                        Wymiary.X -= szybkosc;
                        Zderzenie2(plansza);
                    }
                    else Wymiary.X = 0;
                    break;
            }
        }
        public void Strzelaj(Fabryka fabryka)
        {
            if (pocisk == null)
            {
                pocisk = fabryka.ProdukujPocisk();
                pocisk.UstawPocisk(Wymiary.X + Wymiary.Width/2, Wymiary.Y + Wymiary.Height/2, this.sila, kierunek, this);
            }
        }
        protected void DodajPociskOgien(Plansza plansza, Fabryka fabryka, int X, int Y)
        {
            
            Random random = new Random();
            
            //plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
            //plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X + random.Next(0, 20), Y + random.Next(0, 20)));

            //plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
            //1plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X - random.Next(0, 20), Y + random.Next(0, 20)));

            //plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
            //plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X - random.Next(0, 20), Y - random.Next(0, 20)));

            plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Eksplozja"));
            plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X, Y));
        }
        public void RuchPocisku(Plansza plansza, Fabryka fabryka)
        {
            if (pocisk != null)
            {
                switch (pocisk.kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pocisk.wymiary.Y > 0)
                        {
                            pocisk.ZmienPozycje(0, -pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza))
                            {
                                DodajPociskOgien(plansza, fabryka, pocisk.wymiary.X + pocisk.wymiary.Width/2 - 25, 
                                    pocisk.wymiary.Y + pocisk.wymiary.Height/2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.wymiary.X < 1000)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza))
                            {
                                DodajPociskOgien(plansza, fabryka, pocisk.wymiary.X + pocisk.wymiary.Width / 2 - 25,
                                    pocisk.wymiary.Y + pocisk.wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pocisk.wymiary.Y < 1000)
                        {
                            pocisk.ZmienPozycje(0, pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza))
                            {
                                DodajPociskOgien(plansza, fabryka, pocisk.wymiary.X + pocisk.wymiary.Width / 2 - 25,
                                    pocisk.wymiary.Y + pocisk.wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pocisk.wymiary.X > 0)
                        {
                            pocisk.ZmienPozycje(-pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza))
                            {
                                DodajPociskOgien(plansza, fabryka, pocisk.wymiary.X + pocisk.wymiary.Width / 2 - 25,
                                    pocisk.wymiary.Y + pocisk.wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                }
            }
        }
        /// <summary>
        /// Metoda zderzeń v.2
        /// </summary>
        /// <param name="plansza">Obiekt planszy</param>
        public void Zderzenie2(Plansza plansza)
        {
            if (this.wymiary.IntersectsWith(plansza.baza.wymiary))
            {
                ObliczPozycje(plansza.baza.wymiary);
            }
            plansza.region.CzyKoliduje(this);
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i].wymiary.IntersectsWith(wymiary))
                {
                    ObliczPozycje(plansza.przeciwnicy_na_mapie[i].wymiary);
                }
            }
        }
        public void ObliczPozycje(Rectangle rec)
        {
            switch(kierunek)
            {
                case Kierunek.GORA:
                    UstawPozycjeY(rec.Bottom);
                break;
                case Kierunek.PRAWO:
                    UstawPozycjeX(rec.X - wymiary.Width);
                break;
                case Kierunek.DOL:
                    UstawPozycjeY(rec.Y - wymiary.Height);
                break;
                case Kierunek.LEWO:
                UstawPozycjeX(rec.Right);
                break;
            }
        }
        public enum Kierunek : int { GORA = 0, PRAWO, DOL, LEWO }
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {

            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            
            if (pocisk != null) pocisk.Rysuj(g, transparentPink);
        }
          
    }
}