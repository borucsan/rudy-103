using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;

namespace Rudy_103.src
{
    class Plansza
    {
        public Stack<Przeciwnik> przeciwnicy { get; set; }
        public List<Przeciwnik> przeciwnicy_na_mapie { get; set; }
        public List<Przeszkoda> przeszkody { get; set; }
        public Rectangle[] PunktResp { get; set; }
        public uint poziom { get; set; }
        public int Wysokosc { get; set; }
        public int Szerokosc { get; set; }

        public const int MAX_PRZECIWNIKOW_MAPA = 5;

        public Plansza(int X, int Y)
        {
            Wysokosc = Y;
            Szerokosc = X;
            poziom = 1;
            przeszkody = new List<Przeszkoda>();
            przeciwnicy = new Stack<Przeciwnik>();
            przeciwnicy_na_mapie = new List<Przeciwnik>();
            PunktResp = new Rectangle[3];
            PunktResp[0] = new Rectangle(0, 0, 40, 40);
            PunktResp[1] = new Rectangle(X / 2, 0, 40, 40);
            PunktResp[2] = new Rectangle(X - 40, Y - 40, 40, 40);
        }
        public void GenerujDebugMapa(Fabryka fabryka)
        {
            
            Random random = new Random();
            int ilosc_przeciwnikow = 5;
            Przeciwnik przeciwnik;
            
            
            for (int i = 0; i < ilosc_przeciwnikow; i++)
            {
                przeciwnik = fabryka.ProdukujPrzeciwnika("przeciwnik_poziom_1");
                przeciwnik.wymiary = new Rectangle(900, 100, 40, 40);
                przeciwnicy.Push(przeciwnik);

                przeciwnik = fabryka.ProdukujPrzeciwnika("przeciwnik_poziom_1");
                przeciwnik.wymiary = new Rectangle(100, 100, 40, 40);
                przeciwnicy.Push(przeciwnik);
            }
            
            for (int szerokosc = 0; szerokosc < this.Szerokosc; szerokosc += 25)
            {
                for (int wysokosc = 0; wysokosc < this.Wysokosc; wysokosc += 25)
                {
                    switch (random.Next(0, 4))
                    {
                        case 1:
                            {
                                przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka2"));
                                przeszkody.Last().UstawPozycje(szerokosc, wysokosc);
                            } break;
                        case 2:
                            {
                                
                            } break;
                        case 3:
                            {
                                
                            } break;
                    }
                   
                 }
            }
            //Dodawanie bazy na mape
            przeszkody.Add(fabryka.ProdukujPrzeszkode("nowa baza"));
            przeszkody.Last().UstawPozycje(500, 925);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(475, 900);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(500, 900);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(525, 900);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(550, 900);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(550, 925);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(550, 950);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(550, 975);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(525, 975);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(500, 975);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(475, 975);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(475, 950);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(475, 925);
            
        }
        private void LosujPrzeszkody(Fabryka fabryka)
        {
            /*
            int liczba = 0, max = 1200;
            Random r = new Random();
            liczba = r.Next(10, 108);
            for (int i = 0; i < liczba; ++i)
            {
                przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            }
            max = max - liczba;
            if(poziom >= 4)
            {
                liczba = r.Next(10, poziom - 3);
            }*/

        }
        public void Respawn()
        {
            if (przeciwnicy_na_mapie.Count < MAX_PRZECIWNIKOW_MAPA && przeciwnicy.Count > 0)
            {
                Random random = new Random();
                Przeciwnik przec = przeciwnicy.Pop();
                przec.UstawPozycje(PunktResp[random.Next(0, 2)].Location);
                przeciwnicy_na_mapie.Add(przec);
            }
        }
        public void RuszPrzeciwnikow(Fabryka fabryka, Gracz gracz)
        {
            for (int i = 0; i < przeciwnicy_na_mapie.Count; i++)
            {
                przeciwnicy_na_mapie[i].Ruch_Przeciwnika(this, fabryka, gracz);
                przeciwnicy_na_mapie[i].RuchPocisku(this, gracz);
            }
        }
        public void RysujElementy(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (przeszkody != null)
            {
                for (int i = 0; i < przeszkody.Count; ++i)
                {
                    if (przeszkody[i].wymiary.IntersectsWith(new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, 240, 320)))
                    {
                        przeszkody[i].Rysuj(g, pozycja_kamery, transparentPink);
                    }
                }
            }
            //Rysowanie przeciwnikow, którzy znajdują się na mapie.
            if (przeciwnicy_na_mapie != null)
            {
                for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
                {
                    if(przeciwnicy_na_mapie[i].wymiary.IntersectsWith( new Rectangle(pozycja_kamery.X, pozycja_kamery.Y, 240, 320) ))
                    {
                        przeciwnicy_na_mapie[i].Rysuj(g,pozycja_kamery, transparentPink);
                    }
                }
            }
            
        }

    }
}