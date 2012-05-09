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
        public void Ruch()
        {
            pozostaly_ruch = szybkosc;
        }
        public void WykonajRuchPrzeciwnika(Plansza plansza, Fabryka fabryczka, Gracz gracz)
        {
            bool WykrytoPrzeszkode = false;
            switch (kierunek)
            {
                case Kierunek.GORA:
                        ZmienPozycje(0, -JEDNOSTKA_RUCHU);
                        if (Zderzenie3(plansza, gracz))
                        { ZmienPozycje(0, JEDNOSTKA_RUCHU); WykrytoPrzeszkode = true; }
                        //rec_ruchu = new Rectangle(wymiary.X, wymiary.Y - szybkosc, wymiary.Width, wymiary.Height);
                    break;
                case Kierunek.PRAWO:
                        //rec_ruchu = new Rectangle(wymiary.X + szybkosc, wymiary.Y, wymiary.Width, wymiary.Height);
                        ZmienPozycje(JEDNOSTKA_RUCHU, 0);
                        if (Zderzenie3(plansza, gracz)){ ZmienPozycje(-JEDNOSTKA_RUCHU, 0); WykrytoPrzeszkode = true; }
                    break;
                case Kierunek.DOL:
                        //rec_ruchu = new Rectangle(wymiary.X, wymiary.Y + szybkosc, wymiary.Width, wymiary.Height);
                        ZmienPozycje(0, JEDNOSTKA_RUCHU);
                        if (Zderzenie3(plansza, gracz)) { ZmienPozycje(0, -JEDNOSTKA_RUCHU); WykrytoPrzeszkode = true; }
                    break;
                case Kierunek.LEWO:
                        ZmienPozycje(-JEDNOSTKA_RUCHU, 0);
                        if (Zderzenie3(plansza, gracz)){ ZmienPozycje(JEDNOSTKA_RUCHU, 0); WykrytoPrzeszkode = true; }
                    break;
            }
            AnalizaRuchu(WykrytoPrzeszkode, fabryczka);
            pozostaly_ruch = pozostaly_ruch - JEDNOSTKA_RUCHU;
        }
        public void AnalizaRuchu(bool wykryto_przeszkode, Fabryka fabryka)
        {
            bool LosujKierunek = false;
            int losuj = Narzedzia.rand.Next(0, 51);
            if (losuj == 49) LosujKierunek = true;
            if ((wykryto_przeszkode == true) || (LosujKierunek == true))
            {
                Strzelaj(fabryka);
                int losuj_kierunek = Narzedzia.rand.Next(0, 5);
                if (losuj_kierunek == 1) kierunek = Kierunek.GORA;
                if (losuj_kierunek == 2) kierunek = Kierunek.PRAWO;
                if (losuj_kierunek == 3) kierunek = Kierunek.DOL;
                if (losuj_kierunek == 4) kierunek = Kierunek.LEWO;
            }
            int CzyStrzelac;
            CzyStrzelac = Narzedzia.rand.Next(0, 3);
            if (CzyStrzelac == 2) Strzelaj(fabryka);
        }
        public void SprawdzPozostaleKolizje(Plansza plansza, Gracz gracz)
        {
            if (this.Rec_ruchu.IntersectsWith(gracz.Rec_ruchu))
            {
                Pozostaly_ruch = 0;
                return;
            }
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (this.Rec_ruchu.IntersectsWith(plansza.przeciwnicy_na_mapie[i].Rec_ruchu))
                {
                    Pozostaly_ruch = 0;
                    break;
                }
            }
            if (Pozostaly_ruch > 0) WykonajPozostalyRuchiZatwierdz();
            else UstawNowyRect(Rec_ruchu);
        }
        public void RuchPocisku(Plansza plansza, Gracz gracz, Fabryka fabryka)
        {
            if (pocisk != null)
            {
                switch (pocisk.kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pocisk.Wymiary.Y > 0)
                        {
                            pocisk.ZmienPozycje(0, -pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza, gracz))
                            {
                                Wybuch(plansza, fabryka, pocisk.Wymiary.X + pocisk.Wymiary.Width / 2 - 25,
                                    pocisk.Wymiary.Y + pocisk.Wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.Wymiary.X < 1000)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza, gracz))
                            {
                                Wybuch(plansza, fabryka, pocisk.Wymiary.X + pocisk.Wymiary.Width / 2 - 25,
                                    pocisk.Wymiary.Y + pocisk.Wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pocisk.Wymiary.Y < 1000)
                        {
                            pocisk.ZmienPozycje(0, pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza, gracz))
                            {
                                Wybuch(plansza, fabryka, pocisk.Wymiary.X + pocisk.Wymiary.Width / 2 - 25,
                                    pocisk.Wymiary.Y + pocisk.Wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pocisk.Wymiary.X > 0)
                        {
                            pocisk.ZmienPozycje(-pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza, gracz))
                            {
                                Wybuch(plansza, fabryka, pocisk.Wymiary.X + pocisk.Wymiary.Width / 2 - 25,
                                    pocisk.Wymiary.Y + pocisk.Wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                }
            }
        }
        public override bool Zderzenie2(Plansza plansza)
        {
            if (this.Wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                ObliczPozycje2(plansza.baza);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
        }
        public bool Zderzenie3(Plansza plansza, Gracz gracz)
        {
            if (wymiary.Y < 0 || wymiary.Bottom > plansza.Wysokosc || wymiary.X < 0 || wymiary.Right > plansza.Szerokosc) return true;
            if (this.wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                return true;
            }
            if (this.wymiary.IntersectsWith(gracz.Wymiary))
            {
                return true;
            }
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (this == plansza.przeciwnicy_na_mapie[i]) continue;
                if (this.wymiary.IntersectsWith(plansza.przeciwnicy_na_mapie[i].wymiary))
                {
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
