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
    public abstract class Czolg : Obiekty
    {
        /// <summary>
        /// Obecny poziom wytrzymałości czołgu.(Poziom życia)
        /// </summary>
        protected int wytrzymalosc;
        /// <summary>
        /// Wytrzymałość bazowa czołgu
        /// </summary>
        protected int wytrzymalosc_bazowa;
        /// <summary>
        /// Szybkość czołgu
        /// </summary>
        protected int szybkosc;
        /// <summary>
        /// Siła strzałów czołgu.
        /// </summary>
        protected int sila;
        /// <summary>
        /// Zasięg strzałów czołgu.
        /// </summary>
        protected int zasieg;
        /// <summary>
        /// Ruch jaki pozostał do wykonania.
        /// </summary>
        protected int pozostaly_ruch = 0;
        /// <summary>
        /// Czas ostatniego strzału.
        /// </summary>
        protected int ostatni_strzal = 0;
        /// <summary>
        /// Szybkość strzelania.
        /// </summary>
        protected int przeladowanie;
        /// <summary>
        /// Zmienna statystyk określająca liczbę oddanych strzałów.
        /// </summary>
        protected int ilosc_strzalow = 0;
        /// <summary>
        /// Zmienna statystyk określająca liczbę celnych strzałów.
        /// </summary>
        protected int ilosc_trafien = 0;
        
        /// <summary>
        /// Maksymalna ilość pocisków jaką "jednocześnie" może wystrzelić czołg.
        /// </summary>
        protected int max_pociskow;
        /// <summary>
        /// Lista wystrzelonych pocisków.
        /// </summary>
        protected List<Pocisk> pociski;

        #region Wlasciwosci
        /// <summary>
        /// Szybkość strzelania.
        /// </summary>
        public int Przeladowanie
        {
            get { return przeladowanie; }
            set { przeladowanie = value; }
        }
        /// <summary>
        /// Czas ostatniego strzału.
        /// </summary>
        public int Czas_strzalu
        {
            get { return ostatni_strzal = 0; }
        }
        /// <summary>
        /// Ruch jaki pozostał do wykonania.
        /// </summary>
        public int Pozostaly_ruch
        {
            get { return pozostaly_ruch; }
            set { pozostaly_ruch = value; }
        }
        /// <summary>
        /// Kierunek w który porusza się czołg.
        /// </summary>
        public Kierunek kierunek { get; protected set; }
        /// <summary>
        /// Obecny poziom wytrzymałości czołgu.(Poziom życia)
        /// </summary>
        public int Wytrzymalosc { get { return wytrzymalosc; } set { wytrzymalosc = value; }}
        /// <summary>
        /// Wytrzymałość bazowa czołgu
        /// </summary>
        public int Wytrzymalosc_Bazowa { get { return this.wytrzymalosc_bazowa; } set { wytrzymalosc_bazowa = value; }}
        /// <summary>
        /// Szybkość czołgu
        /// </summary>
        public int Szybkosc { get { return szybkosc; } set { szybkosc = value;}}
        /// <summary>
        /// Siła strzałów czołgu.
        /// </summary>
        public int Sila { get { return sila; } set { sila = value; }}
        /// <summary>
        /// Zasięg strzałów czołgu.
        /// </summary>
        public int Zasieg
        {
            get { return this.zasieg; }
            set { this.zasieg = value; }
        }
        /// <summary>
        /// Maksymalna ilość pocisków jaką "jednocześnie" może wystrzelić czołg.
        /// </summary>
        public int Max_Pociskow
        {
            get { return max_pociskow; }
            set { max_pociskow = value; }
        }
        /// <summary>
        /// Lista wystrzelonych pocisków.
        /// </summary>
        public List<Pocisk> Pociski
        {
            get
            {
                return pociski;
            }
        }
        /// <summary>
        /// Zmienna statystyk określająca liczbę oddanych strzałów.
        /// </summary>
        public int Strzalow { get { return ilosc_strzalow; } set { ilosc_strzalow = value; } }
        /// <summary>
        /// Zmienna statystyk określająca liczbę celnych strzałów.
        /// </summary>
        public int Trafien { get { return ilosc_trafien; } set { ilosc_trafien = value; } }
        #endregion
        /// <summary>
        /// Konstruktor czołgów.(Tylko do przeciążenia)
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
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie)
            : base(X, Y, Szer, Wys)
        {
            this.wytrzymalosc_bazowa = wytrzymalosc;
            this.wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.zasieg = zasieg;
            this.kierunek = Kierunek.GORA;
            this.max_pociskow = max_pociskow;
            this.przeladowanie = przeladowanie;
            pociski = new List<Pocisk>(max_pociskow);
        }
        /// <summary>
        /// Konstruktor czołgów.(Tylko do przeciążenia)
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
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
        public Czolg(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, int zasieg, int max_pociskow, int przeladowanie, params Image[] obrazy)
            : this(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, zasieg, max_pociskow, przeladowanie)
        {
            this.obrazy = obrazy;
        }
        /// <summary>
        /// Metoda do poruszania czołgami.
        /// </summary>
        /// <param name="kierunek">Enumeracja reprezentująca kierunek.</param>
        /// <param name="plansza">Referencja obiektu mapy.</param>
        public virtual void Ruch(Kierunek kierunek, Plansza plansza)
        {
            this.kierunek = kierunek;
            this.pozostaly_ruch = szybkosc;
        }
        /// <summary>
        /// Stała określa jak często będą sprawdzane kolizje podczas ruchu. 
        /// </summary>
        public const int JEDNOSTKA_RUCHU = 1;
        /// <summary>
        /// Stała określa ilość jednostek o ile czołg "przeskoczy" dotykając krawędzi przeszkody.
        /// </summary>
        public const int PRZESKOK = 3;
        /// <summary>
        /// Stała określa maksymalną odległość krawędzi czołgu od krawędzi przeszkody aby uzyskać wspomaganie przy ruchu.
        /// </summary>
        public const int WARUNEK_PRZESKOKU = 15;
        /// <summary>
        /// Metoda wykonuje przesuniecie o JEDNOSTKA_RUCHU i sprawdza kolizję.
        /// </summary>
        /// <param name="plansza"></param>
        public void WykonajRuch(Plansza plansza)
        {
            switch (kierunek)
            {
                case Kierunek.GORA:

                        ZmienPozycje(0, -JEDNOSTKA_RUCHU);
                        Zderzenie3(plansza);
                        //rec_ruchu = new Rectangle(wymiary.X, wymiary.Y - szybkosc, wymiary.Width, wymiary.Height);
                    break;
                case Kierunek.PRAWO:
                        //rec_ruchu = new Rectangle(wymiary.X + szybkosc, wymiary.Y, wymiary.Width, wymiary.Height);
                        ZmienPozycje(JEDNOSTKA_RUCHU, 0);
                        Zderzenie3(plansza);
                    break;
                case Kierunek.DOL:
                        //rec_ruchu = new Rectangle(wymiary.X, wymiary.Y + szybkosc, wymiary.Width, wymiary.Height);
                        ZmienPozycje(0, JEDNOSTKA_RUCHU);
                        Zderzenie3(plansza);
                    break;
                case Kierunek.LEWO:
                        ZmienPozycje(-JEDNOSTKA_RUCHU, 0);
                        Zderzenie3(plansza);
                    break;
            }
            pozostaly_ruch = pozostaly_ruch - JEDNOSTKA_RUCHU;
        }
        /// <summary>
        /// Metoda wykonująca strzał czołgu.
        /// </summary>
        /// <param name="fabryka">Fabrka obiektów.</param>
        /// <param name="czas_strzalu">Częstotliwość z jaką oddawane są strzały.</param>
        public void Strzelaj(Fabryka fabryka, int czas_strzalu)
        {
            if (pociski.Count < max_pociskow && Math.Abs(czas_strzalu - przeladowanie) >= ostatni_strzal)
            {
                pociski.Add(fabryka.ProdukujPocisk());
                pociski.Last().UstawPocisk(Wymiary.X + Wymiary.Width/2, Wymiary.Y + Wymiary.Height/2, this.sila, this.szybkosc+5,this.zasieg, kierunek, this);
                if (Kamera.Prostokat_Kamery.IntersectsWith(Wymiary))
                {
                    Multimedia.audio_wystrzal.Play();
                }
                ++ilosc_strzalow;
                ostatni_strzal = czas_strzalu;
            }
        }
        /// <summary>
        /// Metoda tworząca efekt wybuchu na planszy.
        /// </summary>
        /// <param name="plansza">Aktualna plansza</param>
        /// <param name="fabryka">Fabryka obiektów.</param>
        /// <param name="X">Pozycja wybuchu na osi X</param>
        /// <param name="Y">Pozycja wybuchu na osi Y</param>
        protected void Wybuch(Plansza plansza, Fabryka fabryka, int X, int Y)
        {
            if(Kamera.Prostokat_Kamery.IntersectsWith(new Rectangle(X, Y, 10, 10)))
            {
                plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Eksplozja"));
                plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X, Y));
                Multimedia.audio_wybuch.Play();
                
            }
        }
        /// <summary>
        /// Metoda wykonująca ruch pocisku.
        /// </summary>
        /// <param name="plansza">Aktualna plansza.</param>
        /// <param name="fabryka">Fabryka obiektów.</param>
        public void RuchPocisku(Plansza plansza, Fabryka fabryka)
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
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, fabryka);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pociski[i].Wymiary.X < plansza.Szerokosc)
                        {
                            pociski[i].ZmienPozycje(pociski[i].szybkosc, 0);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, fabryka);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pociski[i].Wymiary.Y < plansza.Wysokosc)
                        {
                            pociski[i].ZmienPozycje(0, pociski[i].szybkosc);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, fabryka);
                            pociski[i].pozostały_ruch -= pociski[i].szybkosc;
                        }
                        else pociski[i].trafil = true;
                        break;
                    case Czolg.Kierunek.LEWO:
                        if (pociski[i].Wymiary.X > 0)
                        {
                            pociski[i].ZmienPozycje(-pociski[i].szybkosc, 0);
                            pociski[i].trafil = pociski[i].Zderzenie(plansza, fabryka);
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
        public virtual bool Zderzenie2(Plansza plansza)
        {
            if (wymiary.Y < 0 || wymiary.Bottom > plansza.Wysokosc || wymiary.X < 0 || wymiary.Right > plansza.Szerokosc)
            {
                wymiary.Location = poprzednia_pozycja;
                return true;
            }
            if (this.wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                wymiary.Location = poprzednia_pozycja;
                return true;
            }
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (this.wymiary.IntersectsWith(plansza.przeciwnicy_na_mapie[i].wymiary))
                {
                    wymiary.Location = poprzednia_pozycja;
                    return true;
                }
            }
            if (plansza.region.CzyKoliduje(this)) return true;
            return plansza.podloza.CzyKoliduje(this);
        }
        /// <summary>
        /// Metoda zderzeń v.3.
        /// </summary>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <returns>true jeśli wykryje zderzenie.</returns>
        public bool Zderzenie3(Plansza plansza)
        {
            if (wymiary.Y < 0 || wymiary.Bottom > plansza.Wysokosc || wymiary.X < 0 || wymiary.Right > plansza.Szerokosc)
            {
                wymiary.Location = poprzednia_pozycja;
                pozostaly_ruch = 0;
                return true;
            }
            if (this.wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                wymiary.Location = poprzednia_pozycja;
                pozostaly_ruch = 0;
                return true;
            }
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (this.wymiary.IntersectsWith(plansza.przeciwnicy_na_mapie[i].wymiary))
                {
                    wymiary.Location = poprzednia_pozycja;
                    pozostaly_ruch = 0;
                    return true;
                }
            }
            Przeszkoda pr = plansza.region.CzyKoliduje2(this) as Przeszkoda;
            if (pr != null)
            {
                wymiary.Location = poprzednia_pozycja;
                if (pozostaly_ruch == szybkosc)
                {
                    ObliczPozycje(pr);
                    pozostaly_ruch = 0;
                    if(Zderzenie2(plansza)) wymiary.Location = poprzednia_pozycja;
                }
                else
                {
                    wymiary.Location = poprzednia_pozycja;
                }
                return true;
            }
            Przeszkoda pod = plansza.podloza.CzyKoliduje2(this) as Przeszkoda;
            if (pod != null)
            {
                wymiary.Location = poprzednia_pozycja;
                if (pozostaly_ruch == szybkosc)
                {
                    ObliczPozycje(pod);
                    pozostaly_ruch = 0;
                    if (Zderzenie2(plansza)) wymiary.Location = poprzednia_pozycja;
                }
                else
                {
                    wymiary.Location = poprzednia_pozycja;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Metoda ustwia czołg przy wspomaganiu ominiecia przeszkód.
        /// </summary>
        /// <param name="przeszkoda"></param>
        public void ObliczPozycje(Przeszkoda przeszkoda)
        {
            int X = wymiary.X - przeszkoda.Wymiary.X;
            int Y = wymiary.Y - przeszkoda.Wymiary.Y;
            if (kierunek == Kierunek.GORA)
            {
                if (wymiary.Right >= przeszkoda.Wymiary.X && wymiary.Right <= przeszkoda.Wymiary.X + WARUNEK_PRZESKOKU)
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width, przeszkoda.Wymiary.Bottom - PRZESKOK);
                }
                else if (wymiary.X >= przeszkoda.Wymiary.Right - WARUNEK_PRZESKOKU && wymiary.X <= przeszkoda.Wymiary.Right)
                {
                    UstawPozycje(przeszkoda.Wymiary.Right, przeszkoda.Wymiary.Bottom - PRZESKOK);
                }
            }
            if (kierunek == Kierunek.PRAWO)
            {
                if (wymiary.Bottom >= przeszkoda.Wymiary.Y && wymiary.Bottom <= przeszkoda.Wymiary.Y + WARUNEK_PRZESKOKU)
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width + PRZESKOK, przeszkoda.Wymiary.Y - wymiary.Height);
                }
                else if (wymiary.Y >= przeszkoda.Wymiary.Bottom - WARUNEK_PRZESKOKU && wymiary.Y <= przeszkoda.Wymiary.Bottom)
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width + PRZESKOK, przeszkoda.Wymiary.Bottom);
                }
            }
            if (kierunek == Kierunek.DOL)
            {
                if (wymiary.Right >= przeszkoda.Wymiary.X && wymiary.Right <= przeszkoda.Wymiary.X + WARUNEK_PRZESKOKU)
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width, przeszkoda.Wymiary.Y - wymiary.Height + PRZESKOK);
                }
                else if (wymiary.X >= przeszkoda.Wymiary.Right - WARUNEK_PRZESKOKU && wymiary.X <= przeszkoda.Wymiary.Right)
                {
                    UstawPozycje(przeszkoda.Wymiary.Right, przeszkoda.Wymiary.Y - wymiary.Height + PRZESKOK);
                }
            }
            if (kierunek == Kierunek.LEWO)
            {
                if (wymiary.Bottom >= przeszkoda.Wymiary.Y && wymiary.Bottom <= przeszkoda.Wymiary.Y + WARUNEK_PRZESKOKU)
                {
                    UstawPozycje(przeszkoda.Wymiary.Right - PRZESKOK, przeszkoda.Wymiary.Y - wymiary.Height);
                }
                else if (wymiary.Y >= przeszkoda.Wymiary.Bottom - WARUNEK_PRZESKOKU && wymiary.Y <= przeszkoda.Wymiary.Bottom)
                {
                    UstawPozycje(przeszkoda.Wymiary.Right - PRZESKOK, przeszkoda.Wymiary.Bottom);
                }
            }
        }
        /// <summary>
        /// Enumeracja określająca kierunek ruchu.
        /// </summary>
        public enum Kierunek : int 
        { 
            /// <summary>
            /// Ruch w górę planszy(-Y).
            /// </summary>
            GORA = 0,
            /// <summary>
            /// Ruch w prawo planszy(+X).
            /// </summary>
            PRAWO,
            /// <summary>
            /// Ruch w dół planszy(+Y).
            /// </summary>
            DOL,
            /// <summary>
            /// Ruch w lewo planszy(-X).
            /// </summary>
            LEWO
        }
        #region Rysowanie
        /// <summary>
        /// Metoda rysująca czołg.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink">Kolor przezroczystości</param>
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {

            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
        }
        /// <summary>
        /// Metoda rysująca pociski czołgu.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink">Kolor przezroczystości</param>
        public void RysujPociski(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            for (int i = 0; i < pociski.Count; ++i) pociski[i].Rysuj(g, transparentPink);
        }
        /// <summary>
        /// Metoda rysująca pasek życia czołgu.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink">Kolor przezroczystości</param>
        public void RysujPasekZycia(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            int procenty_wytrzymalosci = (100 * this.Wytrzymalosc) / this.Wytrzymalosc_Bazowa;
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y + Wymiary.Height + 2 - Kamera.Prostokat_Kamery.Y,
                Wymiary.Width, 5));
            g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(Wymiary.X + 1 - Kamera.Prostokat_Kamery.X, Wymiary.Y + Wymiary.Height + 3 - Kamera.Prostokat_Kamery.Y,
                (Wymiary.Width * procenty_wytrzymalosci) / 100, 4));
        }

        #endregion
    }
}