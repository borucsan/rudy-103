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
        protected int wytrzymalosc_bazowa;
        protected int szybkosc;
        protected int sila;
        protected Rectangle rec_ruchu;
        protected int pozostaly_ruch = 0;
        public int Pozostaly_ruch
        {
            get { return pozostaly_ruch; }
            set { pozostaly_ruch = value; }
        }
        public Rectangle Rec_ruchu
        {
            get { return rec_ruchu; }
            set { rec_ruchu = value; }
        }
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
        public int Wytrzymalosc_Bazowa
        {
            get
            {
                return this.wytrzymalosc_bazowa;
            }
            set
            {
                wytrzymalosc_bazowa = value;
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
            this.wytrzymalosc_bazowa = wytrzymalosc;
            this.wytrzymalosc = wytrzymalosc;
            this.szybkosc = szybkosc;
            this.sila = sila;
            this.kierunek = Kierunek.GORA;
            this.rec_ruchu = wymiary;
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
            this.pozostaly_ruch = szybkosc;
        }
        public const int JEDNOSTKA_RUCHU = 1;
        public const int PRZESKOK = 3;
        public const int WARUNEK_PRZESKOKU = 15;
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
        public void Strzelaj(Fabryka fabryka)
        {
            if (pocisk == null)
            {
                pocisk = fabryka.ProdukujPocisk();
                pocisk.UstawPocisk(Wymiary.X + Wymiary.Width/2, Wymiary.Y + Wymiary.Height/2, this.sila, this.szybkosc+5, kierunek, this);
                if (Kamera.Prostokat_Kamery.IntersectsWith(Wymiary))
                {
                    Multimedia.audio_wystrzal.Play();
                }
            }
        }
        protected void Wybuch(Plansza plansza, Fabryka fabryka, int X, int Y)
        {
            
            Random random = new Random();
            
            //plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
            //plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X + random.Next(0, 20), Y + random.Next(0, 20)));

            //plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
            //1plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X - random.Next(0, 20), Y + random.Next(0, 20)));

            //plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Ogień"));
            //plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X - random.Next(0, 20), Y - random.Next(0, 20)));

            if(Kamera.Prostokat_Kamery.IntersectsWith(new Rectangle(X, Y, 1, 1)))
            {
                plansza.efekty_na_mapie.Add(fabryka.ProdukujEfekt("Eksplozja"));
                plansza.efekty_na_mapie.Last().UstawPozycje(new Point(X, Y));
                Multimedia.audio_wybuch.Play();
                
            }
            
        }
        public void RuchPocisku(Plansza plansza, Fabryka fabryka)
        {
            if (pocisk != null)
            {
                
                switch (pocisk.kierunek)
                {
                    case Czolg.Kierunek.GORA:
                        if (pocisk.Wymiary.Y > 0)
                        {
                            pocisk.ZmienPozycje(0, -pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza))
                            {
                                Wybuch(plansza, fabryka, pocisk.Wymiary.X + pocisk.Wymiary.Width/2 - 25, 
                                    pocisk.Wymiary.Y + pocisk.Wymiary.Height/2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.PRAWO:
                        if (pocisk.Wymiary.X < plansza.Szerokosc)
                        {
                            pocisk.ZmienPozycje(pocisk.szybkosc, 0);
                            if (pocisk.Zderzenie(plansza))
                            {
                                Wybuch(plansza, fabryka, pocisk.Wymiary.X + pocisk.Wymiary.Width / 2 - 25,
                                    pocisk.Wymiary.Y + pocisk.Wymiary.Height / 2 - 25);
                                pocisk = null;
                            }
                        }
                        else pocisk = null;
                        break;
                    case Czolg.Kierunek.DOL:
                        if (pocisk.Wymiary.Y < plansza.Wysokosc)
                        {
                            pocisk.ZmienPozycje(0, pocisk.szybkosc);
                            if (pocisk.Zderzenie(plansza))
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
                            if (pocisk.Zderzenie(plansza))
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
        public void WykonajPozostalyRuchiZatwierdz()
        {
            switch (kierunek)
            {
                case Kierunek.GORA:
                        rec_ruchu = new Rectangle(wymiary.X, wymiary.Y - pozostaly_ruch, wymiary.Width, wymiary.Height);
                    break;
                case Kierunek.PRAWO:
                        rec_ruchu = new Rectangle(wymiary.X + pozostaly_ruch, wymiary.Y, wymiary.Width, wymiary.Height);
                    break;
                case Kierunek.DOL:
                        rec_ruchu = new Rectangle(wymiary.X, wymiary.Y + pozostaly_ruch, wymiary.Width, wymiary.Height);
                    break;
                case Kierunek.LEWO:
                        rec_ruchu = new Rectangle(wymiary.X - pozostaly_ruch, wymiary.Y, wymiary.Width, wymiary.Height);
                    break;
            }
            UstawNowyRect(rec_ruchu);
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
            return plansza.region.CzyKoliduje(this);
        }
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
                    ObliczPozycje3(pr);
                    pozostaly_ruch = 0;
                    if(Zderzenie2(plansza)) wymiary.Location = poprzednia_pozycja;
                }
                else
                {
                    wymiary.Location = poprzednia_pozycja;
                }
                return true;
            }
            return false;
        }
        public void ObliczPozycje2(Przeszkoda przeszkoda)
        {
            int X = wymiary.X - przeszkoda.Wymiary.X;
            int Y = wymiary.Y - przeszkoda.Wymiary.Y;
            if (wymiary.Y >= przeszkoda.Wymiary.Bottom && Math.Abs(X) <= wymiary.Width)
            {
                if(wymiary.IntersectsWith(przeszkoda.krawedzie[2]))
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width, przeszkoda.Wymiary.Bottom - 3);
                }
                else if (wymiary.IntersectsWith(przeszkoda.krawedzie[3]))
                {
                    UstawPozycje(przeszkoda.Wymiary.Right, przeszkoda.Wymiary.Bottom - 3);
                }
            }
            if (wymiary.Right <= przeszkoda.Wymiary.X && Math.Abs(Y) <= wymiary.Height)
            {
                if (wymiary.IntersectsWith(przeszkoda.krawedzie[0]))
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width + 3, przeszkoda.Wymiary.Y - wymiary.Height);
                }
                else if (wymiary.IntersectsWith(przeszkoda.krawedzie[2]))
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width + 3, przeszkoda.Wymiary.Bottom);
                }
            }
            if (wymiary.Bottom <= przeszkoda.Wymiary.Y && Math.Abs(X) <= wymiary.Width)
            {
                if (wymiary.IntersectsWith(przeszkoda.krawedzie[0]))
                {
                    UstawPozycje(przeszkoda.Wymiary.X - wymiary.Width, przeszkoda.Wymiary.Y - wymiary.Height + 3);
                }
                else if (wymiary.IntersectsWith(przeszkoda.krawedzie[1]))
                {
                    UstawPozycje(przeszkoda.Wymiary.Right, przeszkoda.Wymiary.Y - wymiary.Height + 3);
                }
            }
            if (wymiary.X >= przeszkoda.Wymiary.Right && Math.Abs(Y) <= wymiary.Height) 
            {
                if (wymiary.IntersectsWith(przeszkoda.krawedzie[1]))
                {
                    UstawPozycje(przeszkoda.Wymiary.Right - 1, przeszkoda.Wymiary.Y - wymiary.Height);
                }
                else if (wymiary.IntersectsWith(przeszkoda.krawedzie[3]))
                {
                    UstawPozycje(przeszkoda.Wymiary.Right - 1, przeszkoda.Wymiary.Bottom);
                }
            }
        }
        public void ObliczPozycje3(Przeszkoda przeszkoda)
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
        public enum Kierunek : int { GORA = 0, PRAWO, DOL, LEWO }
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {

            g.DrawImage(obrazy[(int)kierunek], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[(int)kierunek].Width, obrazy[(int)kierunek].Width, GraphicsUnit.Pixel, transparentPink);
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y + Wymiary.Height + 2 - Kamera.Prostokat_Kamery.Y,
                Wymiary.Width, 5));
            
            int procenty_wytrzymalosci = (100 * this.Wytrzymalosc) / this.Wytrzymalosc_Bazowa;
            g.FillRectangle(new SolidBrush(Color.Red), new Rectangle(Wymiary.X + 1 - Kamera.Prostokat_Kamery.X, Wymiary.Y + Wymiary.Height + 3 - Kamera.Prostokat_Kamery.Y,
                (Wymiary.Width * procenty_wytrzymalosci) / 100, 4));
            
            if (pocisk != null) pocisk.Rysuj(g, transparentPink);
        }
          
    }
}