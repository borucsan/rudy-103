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
        public int zasieg { get; set; }
        public int pozostały_ruch { get; set; }
        public bool trafil { get; set; }
        public Pocisk(int X, int Y, int Szer, int Wys, int sila, int szybkosc, int zasieg, Czolg.Kierunek kierunek)
            : base(X, Y, Szer, Wys)
        {
            this.kierunek = kierunek;
            this.sila = sila;
            this.szybkosc = szybkosc;
            this.zasieg = zasieg;
            this.trafil = false;
        }
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
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            
        }
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
        
        public bool Zderzenie(Plansza plansza)
        {
            for (int i = 0; i < plansza.przeciwnicy_na_mapie.Count; ++i)
            {
                if (plansza.przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Wymiary))
                {
                    
                    if (plansza.przeciwnicy_na_mapie[i].Uszkodz(sila))
                    {
                        Opcje.WylaczInformacje();
                        plansza.zdobyte_punkty += plansza.przeciwnicy_na_mapie[i].punkty;
                        plansza.przeciwnicy_na_mapie.RemoveAt(i);

                    }
                    
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
        public bool Zderzenie(Plansza plansza, Gracz gracz)
        {
            if (gracz.Wymiary.IntersectsWith(Wymiary))
            {
                gracz.Uszkodz(sila);
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

        public object Clone()
        {
            Pocisk klon = new Pocisk(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.sila, this.szybkosc, this.zasieg, Czolg.Kierunek.GORA);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }

        #endregion
    }
}
