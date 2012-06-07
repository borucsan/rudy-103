using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa pocisku.
    /// </summary>
    public class Pocisk : Obiekty, ICloneable
    {
        /// <summary>
        /// Kierunek ruchu pocisku.
        /// </summary>
        public Czolg.Kierunek kierunek { get; set; }
        /// <summary>
        /// Siła pocisku.
        /// </summary>
        public int sila { get; set; }
        /// <summary>
        /// Szybkość pocisku.
        /// </summary>
        public int szybkosc { get; set; }
        /// <summary>
        /// Właściciel pocisku.
        /// </summary>
        public Czolg wlasciciel { get; set; }
        /// <summary>
        /// Zasięg pocisku.
        /// </summary>
        public int zasieg { get; set; }
        /// <summary>
        /// Ruch jaki pozostał do wykonania.
        /// </summary>
        public int pozostały_ruch { get; set; }
        /// <summary>
        /// Zmienna określająca trafienia.
        /// </summary>
        public bool trafil { get; set; }
        /// <summary>
        /// Konstruktor pocisków.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="sila">Siła pocisku.</param>
        /// <param name="szybkosc">Szybkość pocisku.</param>
        /// <param name="zasieg">Zasięg pocisku.</param>
        /// <param name="kierunek">Kierunek ruchu pocisku.</param>
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, int zasieg, Czolg.Kierunek kierunek)
            : base(X, Y, Szer, Wys)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
            this.zasieg = zasieg;
            this.trafil = false;
        }
        /// <summary>
        /// Konstruktor pocisków.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="sila">Siła pocisku.</param>
        /// <param name="szybkosc">Szybkość pocisku.</param>
        /// <param name="zasieg">Zasięg pocisku.</param>
        /// <param name="kierunek">Kierunek ruchu pocisku.</param>
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, int zasieg, Czolg.Kierunek kierunek, params Image[] obrazy)
            : base(X, Y, Szer, Wys, obrazy)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
            this.zasieg = zasieg;
            this.obrazy = obrazy;
            this.trafil = false;
        }
        /// <summary>
        /// Metoda rysująca pocisk.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink">Kolor przezroczystości.</param>
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            
        }
        /// <summary>
        /// Metoda ustawiająca parametry czołgu.
        /// </summary>
        /// <param name="X">Pozycja na osi X.</param>
        /// <param name="Y">Pozycja na osi Y.</param>
        /// <param name="sila">Siła pocisku.</param>
        /// <param name="szybkosc">Szybkość pocisku.</param>
        /// <param name="zasieg">Zasięg pocisku.</param>
        /// <param name="kierunek">Kierunek ruch pocisku.</param>
        /// <param name="wlasciciel">Właściciel pocisku</param>
        public void UstawPocisk(int X, int Y, int sila, int szybkosc, int zasieg, Czolg.Kierunek kierunek, Czolg wlasciciel)
        {
            wymiary.X = X;
            wymiary.Y = Y;
            this.kierunek = kierunek;
            if (kierunek == Czolg.Kierunek.GORA || kierunek == Czolg.Kierunek.DOL)
            {
                wymiary.X -= 5;
            }
            if (kierunek == Czolg.Kierunek.LEWO || kierunek == Czolg.Kierunek.PRAWO)
            {
                wymiary.Y -= 5;
            }
            this.sila = sila;
            this.szybkosc = szybkosc;
            this.zasieg = zasieg;
            this.pozostały_ruch = zasieg;
            this.wlasciciel = wlasciciel;
        }
        /// <summary>
        /// Metoda zderzeń dla pocisków.
        /// </summary>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <param name="fabryka">Fabryka obiektów</param>
        /// <returns>true jeśli wykryto trafienie.</returns>
        public bool Zderzenie(Plansza plansza, Fabryka fabryka)
        {
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Wymiary))
                {
                    if (plansza.przeciwnicy_na_mapie[i].Uszkodz(sila))
                    {
                        Opcje.WylaczInformacje();
                        plansza.zdobyte_punkty += plansza.przeciwnicy_na_mapie[i].punkty;
                        ((Gracz)wlasciciel).DodajXP(plansza.przeciwnicy_na_mapie[i].punkty);
                        for (int szer = plansza.przeciwnicy_na_mapie[i].Wymiary.X; szer < plansza.przeciwnicy_na_mapie[i].Wymiary.X + plansza.przeciwnicy_na_mapie[i].Wymiary.Width; szer += 10)
                        {
                            for (int wys = plansza.przeciwnicy_na_mapie[i].Wymiary.Y; wys < plansza.przeciwnicy_na_mapie[i].Wymiary.Y + plansza.przeciwnicy_na_mapie[i].Wymiary.Height; wys += 10)
                            {
                                plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
                                plansza.efekty_na_mapie.Last().UstawPozycje(szer, wys);
                            }
                        }
                        
                        plansza.przeciwnicy_na_mapie.RemoveAt(i);
                    }
                    ++(wlasciciel.Trafien);
                    return true;
                }
                if (plansza.przeciwnicy_na_mapie[i].Pociski.Count > 0)
                {
                    for (int j = 0; j < plansza.przeciwnicy_na_mapie[i].Pociski.Count; ++j)
                    {
                        if (plansza.przeciwnicy_na_mapie[i].Pociski[j].Wymiary.IntersectsWith(Wymiary))
                        {
                            plansza.przeciwnicy_na_mapie[i].Pociski.RemoveAt(j);
                            return true;
                        }
                    }
                }
            }
            if (this.Wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                plansza.baza.Uszkodz(this.sila);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
        }
        /// <summary>
        /// Metoda zderzeń dla pocisków.
        /// </summary>
        /// <param name="plansza">Obiekt planszy.</param>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <returns>true jeśli wykryto trafienie.</returns>
        public bool Zderzenie(Plansza plansza, Gracz gracz)
        {
            if (gracz.Wymiary.IntersectsWith(Wymiary))
            {
                gracz.Uszkodz(sila, plansza);
                return true;
            }
            if(gracz.Pociski.Count > 0)
            {
                for (int i = 0; i < gracz.Pociski.Count; ++i)
                {
                    if(this.wymiary.IntersectsWith(gracz.Pociski[i].Wymiary))
                    {
                        gracz.Pociski.RemoveAt(i);
                        return true;
                    }
                }
            }
            
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i] == this.wlasciciel) continue;
                    if (plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Wymiary))
                    {
                        return true;
                    }
                    if (plansza.przeciwnicy_na_mapie[i].Pociski.Count > 0)
                    {
                        for (int j = 0; j < plansza.przeciwnicy_na_mapie[i].Pociski.Count; ++j)
                        {
                            if (plansza.przeciwnicy_na_mapie[i].Pociski[j].Wymiary.IntersectsWith(Wymiary))
                            {
                                plansza.przeciwnicy_na_mapie[i].Pociski.RemoveAt(j);
                                return true;
                            }
                        }
                    }
            }
            if (this.Wymiary.IntersectsWith(plansza.baza.Wymiary))
            {
                plansza.baza.Uszkodz(this.sila);
                return true;
            }
            return plansza.region.CzyKoliduje(this);
            
        }
        #region ICloneable Members
        /// <summary>
        /// Metoda klonująca pocisk.
        /// </summary>
        /// <returns>Nowy obiekt pocisku.</returns>
        public object Clone()
        {
            Pocisk klon = new Pocisk(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.sila, this.szybkosc, this.zasieg, Czolg.Kierunek.GORA);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }

        #endregion
    }
}
