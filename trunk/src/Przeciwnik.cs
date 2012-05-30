using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    public class Przeciwnik : Czolg, ICloneable
    {
        public int punkty { get; private set; }
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int dodawane_punkty)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie) { this.punkty = dodawane_punkty; }
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int dodawane_punkty, int przeladowanie, params Image[] obrazy)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie, obrazy) { this.punkty = dodawane_punkty; }
        public bool respwan { get; set; }
        public object Clone()
        {
            Przeciwnik klon = new Przeciwnik(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.wytrzymalosc, this.szybkosc, this.sila, this.zasieg, this.max_pociskow, this.przeladowanie, this.punkty);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        public void Ruch()
        {
            pozostaly_ruch = szybkosc;
        }
        public void WykonajRuchPrzeciwnika(Plansza plansza, Fabryka fabryczka, Gracz gracz, int czas_strzalu)
        {
            bool WykrytoPrzeszkode = false;
            switch (kierunek)
            {
                case Kierunek.GORA:
                        ZmienPozycje(0, -JEDNOSTKA_RUCHU);
                        if (Zderzenie3(plansza, gracz))
                        { 
                            ZmienPozycje(0, JEDNOSTKA_RUCHU); 
                            WykrytoPrzeszkode = true; 
                        }
                    break;
                case Kierunek.PRAWO:
                        ZmienPozycje(JEDNOSTKA_RUCHU, 0);
                        if (Zderzenie3(plansza, gracz))
                        { 
                            ZmienPozycje(-JEDNOSTKA_RUCHU, 0); 
                            WykrytoPrzeszkode = true; 
                        }
                    break;
                case Kierunek.DOL:
                        ZmienPozycje(0, JEDNOSTKA_RUCHU);
                        if (Zderzenie3(plansza, gracz)) 
                        { 
                            ZmienPozycje(0, -JEDNOSTKA_RUCHU); 
                            WykrytoPrzeszkode = true; 
                        }
                    break;
                case Kierunek.LEWO:
                        ZmienPozycje(-JEDNOSTKA_RUCHU, 0);
                        if (Zderzenie3(plansza, gracz)){ ZmienPozycje(JEDNOSTKA_RUCHU, 0); WykrytoPrzeszkode = true; }
                    break;
            }
            AnalizaRuchu(WykrytoPrzeszkode, fabryczka, plansza, czas_strzalu);
            pozostaly_ruch = pozostaly_ruch - JEDNOSTKA_RUCHU;
        }
        public void AnalizaRuchu(bool wykryto_przeszkode, Fabryka fabryka, Plansza plansza, int czas_strzalu)
        {
            bool LosujKierunek = false;
            if (new Rectangle(0, 0, plansza.Szerokosc, plansza.Wysokosc).Contains(wymiary))
            {
                respwan = false;
                int losuj = Narzedzia.rand.Next(0, 51);
                if (losuj == 49) LosujKierunek = true;
                if ((wykryto_przeszkode == true) || (LosujKierunek == true))
                {
                //Strzelaj(fabryka, czas_strzalu);
                    int losuj_kierunek = Narzedzia.rand.Next(0, 5);
                    if (losuj_kierunek == 1) kierunek = Kierunek.GORA;
                    if (losuj_kierunek == 2) kierunek = Kierunek.PRAWO;
                    if (losuj_kierunek == 3) kierunek = Kierunek.DOL;
                    if (losuj_kierunek == 4) kierunek = Kierunek.LEWO;
                }
            }
            else
            {
                respwan = true;
                kierunek = Kierunek.DOL;
            }
            
            
            int CzyStrzelac;
            CzyStrzelac = Narzedzia.rand.Next(0, 3);
            if (CzyStrzelac == 2 || wykryto_przeszkode || LosujKierunek) Strzelaj(fabryka, czas_strzalu);
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
            for (int i = 0; i < pociski.Count; ++i)
            {
                if (pociski[i].pozostały_ruch < pociski[i].szybkosc) pociski[i].pozostały_ruch = pociski[i].szybkosc - (pociski[i].szybkosc - pociski[i].pozostały_ruch);
                switch (pociski[i].kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pociski[i].Wymiary.Y > 0)
                        {
                            pociski[i].ZmienPozycje(0, -pociski[i].szybkosc);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, gracz);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pociski[i].Wymiary.X < plansza.Szerokosc)
                        {
                            pociski[i].ZmienPozycje(pociski[i].szybkosc, 0);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, gracz);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pociski[i].Wymiary.Y < plansza.Wysokosc)
                        {
                            pociski[i].ZmienPozycje(0, pociski[i].szybkosc);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, gracz);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pociski[i].Wymiary.X > 0)
                        {
                            pociski[i].ZmienPozycje(-pociski[i].szybkosc, 0);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, gracz);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                }
                if (pociski[i].pozostały_ruch <= 0) pociski[i].trafil = true;

                if (pociski[i].trafil)
                {
                    Wybuch(plansza, fabryka, pociski[i].Wymiary.X + pociski[i].Wymiary.Width / 2 - 25,
                                    pociski[i].Wymiary.Y + pociski[i].Wymiary.Height / 2 - 25);
                    pociski.RemoveAt(i);
                }
            }
        }
        public override bool Zderzenie2(Plansza plansza)
        {
            if (this.Wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                ObliczPozycje(plansza.baza);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
        }
        public bool Zderzenie3(Plansza plansza, Gracz gracz)
        {
            if ((wymiary.Y < 0 || wymiary.Bottom > plansza.Wysokosc || wymiary.X < 0 || wymiary.Right > plansza.Szerokosc) && respwan == false) return true;
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
