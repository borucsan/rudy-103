using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa przeciwników.
    /// </summary>
    public class Przeciwnik : Czolg, ICloneable
    {
        /// <summary>
        /// Wartość punktowa przeciwnika.
        /// </summary>
        public int punkty { get; private set; }
        /// <summary>
        /// Zmienna określa czy przeciwnik jest w czasie wjazdu w obszar planszy.
        /// </summary>
        public bool respwan { get; set; }
        /// <summary>
        /// Konstruktor przeciwników.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="wytrzymalosc">Wytrzymałość maksymalna czołgu.</param>
        /// <param name="szybkosc">Szybkość maksymalna czołgu.</param>
        /// <param name="sila">Siła maksymalna strzałów czołgu.</param>
        /// <param name="zasieg">Zasię maksymalny strzałów czołgu.</param>
        /// <param name="max_pociskow">Maksymalna ilość pocisków.</param>
        /// <param name="przeladowanie">Częstotliwość strzałów</param>
        /// <param name="dodawane_punkty">Wartość punktowa przeciwnika.</param>
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, int dodawane_punkty)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie) { this.punkty = dodawane_punkty; }
        /// <summary>
        /// Konstruktor przeciwników.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="wytrzymalosc">Wytrzymałość maksymalna czołgu.</param>
        /// <param name="szybkosc">Szybkość maksymalna czołgu.</param>
        /// <param name="sila">Siła maksymalna strzałów czołgu.</param>
        /// <param name="zasieg">Zasię maksymalny strzałów czołgu.</param>
        /// <param name="max_pociskow">Maksymalna ilość pocisków.</param>
        /// <param name="przeladowanie">Częstotliwość strzałów</param>
        /// <param name="dodawane_punkty">Wartość punktowa przeciwnika.</param>
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int dodawane_punkty, int przeladowanie, params Image[] obrazy)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie, obrazy) { this.punkty = dodawane_punkty; }
        /// <summary>
        /// Metoda do poruszania czołgami.
        /// </summary>
        public void Ruch()
        {
            pozostaly_ruch = szybkosc;
        }
        /// <summary>
        /// Metoda wykonuje przesuniecie o JEDNOSTKA_RUCHU, sprawdza kolizję i analizuje następny ruch.
        /// </summary>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <param name="fabryczka">Fabeyka obiektów</param>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <param name="czas_strzalu">Częstotliwość z jaką oddawane są strzały.</param>
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
        /// <summary>
        /// Metoda analizuje następny ruch przeciwnika. 
        /// </summary>
        /// <param name="wykryto_przeszkode">Zmienna określa czy wykryto przeszkode.</param>
        /// <param name="fabryka">Fabryka obiektów.</param>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <param name="czas_strzalu">Częstotliwość z jaką oddawane są strzały.</param>
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
        /// <summary>
        /// Metoda wykonująca ruch pocisku.
        /// </summary>
        /// <param name="plansza">Aktualna plansza.</param>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <param name="fabryka">Fabryka obiektów.</param>
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
        /// <summary>
        /// Metoda zderzeń v.2
        /// </summary>
        /// <param name="plansza">Obiekt planszy</param>
        public override bool Zderzenie2(Plansza plansza)
        {
            if (this.Wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                ObliczPozycje(plansza.baza);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
        }
        /// <summary>
        /// Metoda zderzeń v.3.
        /// </summary>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <returns>true jeśli wykryje zderzenie.</returns>
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
        /// <summary>
        /// Metoda zmiejszająca poziom energi przeciwnika.
        /// </summary>
        /// <param name="sila">Siła z jaką zadano uszkodzenie.</param>
        /// <returns>true jeśli obiekt został zniszczony</returns>
        public override bool Uszkodz(int sila)
        {
            this.wytrzymalosc = this.wytrzymalosc - sila;
            return this.wytrzymalosc <= 0;
        }


        #region ICloneable Members
        /// <summary>
        /// Metoda klonuje przeciwnika.
        /// </summary>
        /// <returns>Nowy obiekt przeciwnika.</returns>
        public object Clone()
        {
            Przeciwnik klon = new Przeciwnik(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.wytrzymalosc, this.szybkosc, this.sila, this.zasieg, this.max_pociskow, this.przeladowanie, this.punkty);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }

        #endregion
    }
}
