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
        public Queue<Przeciwnik> przeciwnicy { get; set; }
        public List<Przeciwnik> przeciwnicy_na_mapie { get; set; }
        public List<Efekty> efekty_na_mapie { get; set; }

        public Rectangle[] PunktResp { get; set; }
        public Baza baza { get; set; }
        public DrzewoPrzeszkody region { get; set; }
        public int poziom { get; set; }
        public int Wysokosc { get; set; }
        public int Szerokosc { get; set; }
        public int zdobyte_punkty { get; set; }
        public bool ukonczony_poziom { get; set; }
        protected Image[] Podloze;
        protected int aktualne_podloze;

        public const int MAX_PRZECIWNIKOW_MAPA = 5;

        public Plansza(int X, int Y)
        {
            Wysokosc = Y;
            Szerokosc = X;
            poziom = 0;
            przeciwnicy = new Queue<Przeciwnik>();
            przeciwnicy_na_mapie = new List<Przeciwnik>();
            efekty_na_mapie = new List<Efekty>();

            PunktResp = new Rectangle[3];
            PunktResp[0] = new Rectangle(0, 0, 50, 50);
            PunktResp[1] = new Rectangle(X / 2, 0, 50, 50);
            PunktResp[2] = new Rectangle(X - 50, 0, 50, 50);
            aktualne_podloze = 0;
        }
        public void GenerujDebugMapa(Fabryka fabryka)
        {
            Random random = new Random();
            poziom += 1; 
            zdobyte_punkty = 0;             //ustawiamy punkty zdobyte w tym poziomie na 0
            ukonczony_poziom = false;       //poziom nie jest ukończony
            this.LosujPrzeciwnikow(fabryka);
            this.LosujPrzeszkody(fabryka);
        }
        private void LosujPrzeciwnikow(Fabryka fabryka)
        {
            Random r = new Random();
            int max;
            max = r.Next(poziom * 10, poziom * 10 + 5);
            for (int i = 0; i < max; i++)
            {
                przeciwnicy.Enqueue(fabryka.ProdukujPrzeciwnika("przeciwnik_poziom_1"));
            }
        }
        private void LosujPrzeszkody(Fabryka fabryka)
        {
            int liczba = 0, max, wylosowana;
            
            List<Point> pozycje = new List<Point>();
            Rectangle recbazy = new Rectangle(475, 900, 100, 100);
            for (int szerokosc = 0; szerokosc < this.Szerokosc; szerokosc += 25)
            {
                for (int wysokosc = 0; wysokosc < this.Wysokosc; wysokosc += 25)
                {
                    Point p = new Point(szerokosc, wysokosc);
                    if (recbazy.Contains(p) || Gracz.PunktRespGracza.Contains(p) || PunktResp[0].Contains(p) || PunktResp[1].Contains(p) || PunktResp[2].Contains(p)) continue;
                    pozycje.Add(p);
                }
            }

            baza = fabryka.wzorzec_bazy;
            baza.UstawPozycje(500,925);
            if (poziom < 4)
            {
                max = Narzedzia.rand.Next(250, poziom * 300);
                liczba = Narzedzia.rand.Next(10, 20);
            }
            else
            {
                max = Narzedzia.rand.Next(700, 1200);
                liczba = Narzedzia.rand.Next(10, 108);
            }
            List<Przeszkoda> przeszkody = new List<Przeszkoda>(max + 12);
            
            for (int i = 0; i < liczba; ++i)
            {
                przeszkody.Add(fabryka.ProdukujPrzeszkode("drzewo"));
                wylosowana = Narzedzia.rand.Next(pozycje.Count);
                przeszkody.Last().UstawPozycje(pozycje[wylosowana]);
                pozycje.RemoveAt(wylosowana);
            }
            max = max - liczba;
            if(poziom >= 4)
            {
                liczba = Narzedzia.rand.Next(10, (poziom - 3) * 12);
                for (int i = 0; i < liczba; ++i)
                {
                    przeszkody.Add(fabryka.ProdukujPrzeszkode("mur"));
                    wylosowana = Narzedzia.rand.Next(pozycje.Count);
                    przeszkody.Last().UstawPozycje(pozycje[wylosowana]);
                    pozycje.RemoveAt(wylosowana);
                }
                max = max - liczba;
            }
            if (poziom >= 3)
            {
                liczba = Narzedzia.rand.Next(10, (poziom - 2) * 12);
                for (int i = 0; i < liczba; ++i)
                {
                    przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka4"));
                    wylosowana = Narzedzia.rand.Next(pozycje.Count);
                    przeszkody.Last().UstawPozycje(pozycje[wylosowana]);
                    pozycje.RemoveAt(wylosowana);
                }
                max = max - liczba;
            }
            if (poziom >= 2)
            {
                liczba = Narzedzia.rand.Next(10, (poziom - 1) * 12);
                for (int i = 0; i < liczba; ++i)
                {
                    przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka3"));
                    wylosowana = Narzedzia.rand.Next(pozycje.Count);
                    przeszkody.Last().UstawPozycje(pozycje[wylosowana]);
                    pozycje.RemoveAt(wylosowana);
                }
                max = max - liczba;
            }
            liczba = Narzedzia.rand.Next(10, poziom * 12);
            for (int i = 0; i < liczba; ++i)
            {
                przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka2"));
                wylosowana = Narzedzia.rand.Next(pozycje.Count);
                przeszkody.Last().UstawPozycje(pozycje[wylosowana]);
                pozycje.RemoveAt(wylosowana);
            }
            max = max - liczba;
            liczba = Narzedzia.rand.Next(10, max);
            for (int i = 0; i < liczba; ++i)
            {
                przeszkody.Add(fabryka.ProdukujPrzeszkode("Skrzynka"));
                wylosowana = Narzedzia.rand.Next(pozycje.Count);
                przeszkody.Last().UstawPozycje(pozycje[wylosowana]);
                pozycje.RemoveAt(wylosowana);
            }
            
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


            region = new DrzewoPrzeszkody(przeszkody, new Rectangle(0, 0, Szerokosc, Wysokosc), true);
        }
        public void Respawn(Gracz gracz)
        {
            if (przeciwnicy_na_mapie.Count < MAX_PRZECIWNIKOW_MAPA && przeciwnicy.Count > 0)
            {
                List<Rectangle> temp_resp = new List<Rectangle>(PunktResp);
                int liczba;
                while (true)
                {
                    if (temp_resp.Count == 0) return;
                    bool wolne = true;
                    liczba = Narzedzia.rand.Next(0, temp_resp.Count);
                    if (temp_resp[liczba].IntersectsWith(gracz.Wymiary))
                    {
                        temp_resp.RemoveAt(liczba);
                        continue;
                    }
                    for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
                    {
                        if(temp_resp[liczba].IntersectsWith(przeciwnicy_na_mapie[i].Wymiary))
                        {
                            temp_resp.RemoveAt(liczba);
                            wolne = false;
                            break;
                        }
                    }
                    if (wolne) break;
                }
                Przeciwnik przec = przeciwnicy.Dequeue();
                przec.UstawPozycje(PunktResp[liczba].Location);
                przeciwnicy_na_mapie.Add(przec);
            }
        }
        public void RuszPrzeciwnikow(Fabryka fabryka, Gracz gracz)
        {
            for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
            {
                przeciwnicy_na_mapie[i].Ruch_Przeciwnika(this, fabryka, gracz);
                przeciwnicy_na_mapie[i].RuchPocisku(this, gracz, fabryka);
            }
            /*
            for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
            {
                for (int j = i + 1; j < przeciwnicy_na_mapie.Count; ++j)
                {
                    if (przeciwnicy_na_mapie[i].wymiary.IntersectsWith(przeciwnicy_na_mapie[j].wymiary))
                    {
                        przeciwnicy_na_mapie[i].ObliczPozycje(przeciwnicy_na_mapie[j].wymiary);
                        przeciwnicy_na_mapie[j].ObliczPozycje(przeciwnicy_na_mapie[i].wymiary);
                        break;
                    }
                }
            }*/
        }
        public void RysujElementy(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            
            region.RysujElementy(Kamera.Prostokat_Kamery, g, transparentPink);
            if (baza.Wymiary.IntersectsWith(Kamera.Prostokat_Kamery))
            {
                baza.Rysuj(g, transparentPink);
            }
            //Rysowanie przeciwnikow, którzy znajdują się na mapie.
            if (przeciwnicy_na_mapie != null)
            {
                for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
                {
                    if (przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Kamera.Prostokat_Kamery))
                    {
                        
                        przeciwnicy_na_mapie[i].Rysuj(g, transparentPink);
                    }
                    if (przeciwnicy_na_mapie[i].Pocisk != null)
                    {
                        if (przeciwnicy_na_mapie[i].Pocisk.Wymiary.IntersectsWith(Kamera.Prostokat_Kamery))
                        {
                            przeciwnicy_na_mapie[i].Pocisk.Rysuj(g, transparentPink);
                        }
                    }
                }
            }
        }
        //Metoda rysująca efekty na mapie
        public void RysujEfekty(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            //Rysowanie efektów, które znajdują się na mapie.
            if (efekty_na_mapie != null)
            {
                for (int i = 0; i < efekty_na_mapie.Count; ++i)
                {

                    if (efekty_na_mapie[i].Wymiary.IntersectsWith(Kamera.Prostokat_Kamery))
                    {

                        efekty_na_mapie[i].Rysuj(g, transparentPink);
                         
                    }
                }
            }
        }
        //Metoda sprawdzająca czy efekty wykonały już określoną ilość pętli, jeżeli tak to są usuwane
        public void SprawdzEfekty()
        {
            if (efekty_na_mapie != null)
            {
                for (int i = 0; i < efekty_na_mapie.Count; ++i)
                {
                    if (efekty_na_mapie[i].wykonane_animacje >= efekty_na_mapie[i].ilosc_wykonan_animacji)
                    {
                        efekty_na_mapie.RemoveAt(i);
                    }
                }
            }
        }
        //Metoda zmieniająca stan efektów
        public void ZmienStanEfektow()
        {
            if (efekty_na_mapie != null)
            {
                for (int i = 0; i < efekty_na_mapie.Count; ++i)
                {
                    efekty_na_mapie[i].ZmienStan();
                }
            }
        }

        public void WczytajGrafikePodloza(params Image[] podloza)
        {
            this.Podloze = podloza;
        }
        public void ZmienPodloze()
        {
            aktualne_podloze += 1;
            if (aktualne_podloze >= Podloze.Count()) aktualne_podloze = 0;
        }
        public Image AktualnePodloze() { return this.Podloze[aktualne_podloze]; }
    }
}