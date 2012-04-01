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
        public Stack<Przeciwnik> przeciwnicy = new Stack<Przeciwnik>();
        public List<Przeciwnik> przeciwnicy_na_mapie { get; set; }
        public List<Przeszkoda> przeszkody { get; set; }
        public uint poziom { get; set; }
        public int Wysokosc { get; set; }
        public int Szerokosc { get; set; }
        public Plansza(int X, int Y)
        {
            Wysokosc = Y;
            Szerokosc = X;
            poziom = 1;
            przeszkody = new List<Przeszkoda>();
            przeciwnicy = new Stack<Przeciwnik>();
            przeciwnicy_na_mapie = new List<Przeciwnik>();
        }
        public void GenerujDebugMapa(Fabryka fabryka)
        {
            
            Random random = new Random();

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
            
            /*
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(25,25);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(25, 50);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka2"));
            przeszkody.Last().UstawPozycje(150, 25);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka2"));
            przeszkody.Last().UstawPozycje(175, 25);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka3"));
            przeszkody.Last().UstawPozycje(150, 75);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka3"));
            przeszkody.Last().UstawPozycje(175, 75);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka4"));
            przeszkody.Last().UstawPozycje(150, 100);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka4"));
            przeszkody.Last().UstawPozycje(175, 100);

            przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            przeszkody.Last().UstawPozycje(150, 125);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            przeszkody.Last().UstawPozycje(175, 125);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            przeszkody.Last().UstawPozycje(200, 125);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            przeszkody.Last().UstawPozycje(150, 150);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            przeszkody.Last().UstawPozycje(175, 150);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
            przeszkody.Last().UstawPozycje(200, 150);
            */
            


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
            
        }

    }
}