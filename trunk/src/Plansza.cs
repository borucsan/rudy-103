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
        private Stack<Przeciwnik> przeciwnicy = new Stack<Przeciwnik>();
        public List<Przeciwnik> przeciwnicy_na_mapie { get; set; }
        public List<Przeszkoda> przeszkody { get; set; }
        public uint poziom { get; set; }
        public uint Wysokosc { get; set; }
        public uint Szerokosc { get; set; }
        public Plansza(uint X, uint Y)
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
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(25,25);
            przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
            przeszkody.Last().UstawPozycje(25, 50);
            
        }
        public void RysujElementy(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            for (int i = 0; i < przeszkody.Count; ++i)
            {
                przeszkody[i].Rysuj(g, pozycja_kamery, transparentPink);
            }
        }

    }
}