using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    class Przeciwnik : Czolg, ICloneable
    {
        public int punkty { get; set; }
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int dodawane_punkty)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila) { this.punkty = dodawane_punkty; }
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int dodawane_punkty, params Image[] obrazy)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, obrazy) { this.punkty = dodawane_punkty; }
        //public override void Rysuj(PaintEventArgs e) { }

        public object Clone()
        {
            Przeciwnik klon = new Przeciwnik(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.wytrzymalosc, this.szybkosc, this.sila, this.punkty);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        public void Ruch_Przeciwnika(Plansza mapa, Fabryka fabryczka, Gracz gracz)
        {
           //Losuj kierunek
            Random random = new Random();
            //kierunek = Kierunek.DOL;
            bool WykrytoPrzeszkode = false;
            
            bool LosujKierunek = false;
            //this.Ruch(kierunek, mapa);

            
            int losuj = random.Next(0, 51);
            if (losuj == 49) { LosujKierunek = true; }

            switch (kierunek)
            {
                case Kierunek.GORA:
                    if (Wymiary.Y >= szybkosc)
                    {
                        Wymiary.Y -= szybkosc;
                        WykrytoPrzeszkode = Zderzenie2(mapa, gracz);
                    }
                    else WykrytoPrzeszkode = true;
                    break;
                case Kierunek.PRAWO:
                    if (Wymiary.X <= mapa.Szerokosc - (szybkosc + Wymiary.Width))
                    {
                        Wymiary.X += szybkosc;
                        WykrytoPrzeszkode = Zderzenie2(mapa, gracz);
                    }
                    else WykrytoPrzeszkode = true;
                    break;
                case Kierunek.DOL:
                    if (Wymiary.Y <= mapa.Wysokosc - (szybkosc + Wymiary.Height))
                    {
                        Wymiary.Y += szybkosc;
                        WykrytoPrzeszkode = Zderzenie2(mapa, gracz);
                    }
                    else WykrytoPrzeszkode = true;
                    break;
                case Kierunek.LEWO:
                    if (Wymiary.X >= szybkosc)
                    {
                        Wymiary.X -= szybkosc;
                        WykrytoPrzeszkode = Zderzenie2(mapa, gracz);
                    }
                    else WykrytoPrzeszkode = true;
                    break;
            }

            if ((WykrytoPrzeszkode == true) || (LosujKierunek == true))
            {
                Strzelaj(fabryczka);

                random = new Random();
                int losuj_kierunek = (int)random.Next(0, 5);
                if (losuj_kierunek == 1) kierunek = Kierunek.GORA;
                if (losuj_kierunek == 2) kierunek = Kierunek.PRAWO;
                if (losuj_kierunek == 3) kierunek = Kierunek.DOL;
                if (losuj_kierunek == 4) kierunek = Kierunek.LEWO;

                WykrytoPrzeszkode = false;
                LosujKierunek = false;
            }
            int CzyStrzelac;
            random = new Random();
            CzyStrzelac = random.Next(0, 3);
            if (CzyStrzelac == 2) Strzelaj(fabryczka);
        
            
        }
        public void RuchPocisku(Plansza plansza, Gracz gracz, Fabryka fabryka)
        {
            if (pocisk != null)
            {
                switch (pocisk.kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pocisk.wymiary.Y > 0)
                        {
                            pocisk.ZmienPozycje(0, -pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza, gracz))
                            {
                                DodajPociskOgien(plansza, fabryka, pocisk.wymiary.X + pocisk.wymiary.Width / 2 - 25,
                                    pocisk.wymiary.Y + pocisk.wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.wymiary.X < 1000)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza, gracz))
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
                            if (pocisk.Zderzenie(plansza, gracz))
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
                            if (pocisk.Zderzenie(plansza, gracz))
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
        public bool Zderzenie2(Plansza plansza, Gracz gracz)
        {
            if (gracz.wymiary.IntersectsWith(Wymiary))
            {
                ObliczPozycje(gracz.wymiary);
                return true;
            }
            if (this.wymiary.IntersectsWith(plansza.baza.wymiary))
            {
                ObliczPozycje(plansza.baza.wymiary);
                return true;
            }
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i] == this) continue;
                if (plansza.przeciwnicy_na_mapie[i].wymiary.IntersectsWith(wymiary))
                {
                    ObliczPozycje(plansza.przeciwnicy_na_mapie[i].wymiary);
                    return true;
                }
            }
            return plansza.region.CzyKoliduje(this);
        }
        public override bool Uszkodz(int sila)
        {
            this.wytrzymalosc = this.wytrzymalosc - sila;
            return this.wytrzymalosc <= 0;
        }
    }
}
