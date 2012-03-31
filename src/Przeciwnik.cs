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
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila) { }
        public Przeciwnik(int X, int Y, int Szer, int Wys, int wytrzymalosc, int szybkosc, int sila, params Image[] obrazy)
            : base(X, Y, Szer, Wys, wytrzymalosc, szybkosc, sila, obrazy) { }
        //public override void Rysuj(PaintEventArgs e) { }

        public object Clone()
        {
            Przeciwnik klon = new Przeciwnik(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.wytrzymalosc, this.szybkosc, this.sila);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        public void Ruch_Przeciwnika(Plansza mapa, Fabryka fabryczka)
        {
           //Losuj kierunek
            Random random = new Random();
            //kierunek = Kierunek.DOL;
            bool WykrytoPrzeszkode = false;
            bool LosujKierunek = false;
            //this.Ruch(kierunek, mapa);
            int losuj = random.Next(0,51);
            if (losuj == 49) { LosujKierunek = true; }
            
            switch (kierunek)
            {
                case Kierunek.GORA:
                    if (Wymiary.Y >= szybkosc)
                    {
                        Wymiary.Y -= szybkosc;
                        if (Zderzenie(mapa))
                        {
                            Wymiary.Y = Wymiary.Y + szybkosc;
                            WykrytoPrzeszkode = true;
                        }
                    }
                    else WykrytoPrzeszkode = true;
                    break;
                case Kierunek.PRAWO:
                    if (Wymiary.X <= mapa.Szerokosc - (szybkosc + Wymiary.Width))
                    {
                        Wymiary.X += szybkosc;
                        if (Zderzenie(mapa))
                        {
                            Wymiary.X = Wymiary.X - szybkosc;
                            WykrytoPrzeszkode = true;
                        }
                    }
                    else WykrytoPrzeszkode = true;
                    break;
                case Kierunek.DOL:
                    if (Wymiary.Y <= mapa.Wysokosc - (szybkosc + Wymiary.Height))
                    {
                        Wymiary.Y += szybkosc;
                        if (Zderzenie(mapa))
                        {
                            Wymiary.Y = Wymiary.Y - szybkosc;
                            WykrytoPrzeszkode = true;
                        }
                    }
                    else WykrytoPrzeszkode = true;
                    break;
                case Kierunek.LEWO:
                    if (Wymiary.X >= szybkosc) //&& !Zderzenie(mapa))
                    {
                        Wymiary.X -= szybkosc;
                        if (Zderzenie(mapa))
                        {
                            Wymiary.X = Wymiary.X + szybkosc;
                            WykrytoPrzeszkode = true;
                        }
                    }
                    else WykrytoPrzeszkode = true;
                    break;
            }

            if ( (WykrytoPrzeszkode == true) || (LosujKierunek == true) )
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
    }
}
