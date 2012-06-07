using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa planszy.
    /// </summary>
    public class Plansza
    {
        /// <summary>
        /// Stos puli przeciwników poziomu.
        /// </summary>
        public Stack<Przeciwnik> przeciwnicy { get; private set; }
        /// <summary>
        /// Lista przeciwników na mapie.
        /// </summary>
        public List<Przeciwnik> przeciwnicy_na_mapie { get; private set; }
        /// <summary>
        /// Lista efektów na mapie.
        /// </summary>
        public List<Animacja> efekty_na_mapie { get; private set; }
        /// <summary>
        /// Tablica punktów odrodzeń przeciwników.
        /// </summary>
        public Rectangle[] PunktResp { get; private set; }
        /// <summary>
        /// Obiekt bazy na planszy.
        /// </summary>
        public Baza baza { get; private set; }
        /// <summary>
        /// Drzewo przeszkód.
        /// </summary>
        public DrzewoPrzeszkody region { get; private set; }
        /// <summary>
        /// Drzewo podłoża.
        /// </summary>
        public DrzewoPrzeszkody podloza { get; private set; }
        private List<Przeszkoda> mur_bazy = new List<Przeszkoda>(8);
        /// <summary>
        /// Poziom planszy.
        /// </summary>
        public int poziom { get; private set; }
        /// <summary>
        /// Wysokość planszy.
        /// </summary>
        public int Wysokosc { get; private set; }
        /// <summary>
        /// Szerokość planszy.
        /// </summary>
        public int Szerokosc { get; private set; }
        /// <summary>
        /// Zdobyte punkty na poziomie.
        /// </summary>
        public int zdobyte_punkty { get; set; }
        /// <summary>
        /// Zmienna określa czy ukończono poziom.
        /// </summary>
        public bool ukonczony_poziom { get; set; }
        /// <summary>
        /// Tablica teł.
        /// </summary>
        protected Image[] Podloze;
        /// <summary>
        /// Aktualnie wybrane tło.
        /// </summary>
        protected int aktualne_podloze;

        /// <summary>
        /// Stała określająca maksymalną ilość przeciwników na mapie.
        /// </summary>
        public const int MAX_PRZECIWNIKOW_MAPA = 5;

        /// <summary>
        /// Konstruktor planszy.
        /// </summary>
        /// <param name="X">Szerokość mapy.</param>
        /// <param name="Y">Wysokość mapy.</param>
        public Plansza(int X, int Y)
        {
            Wysokosc = Y;
            Szerokosc = X;
            poziom = 0;
            przeciwnicy = new Stack<Przeciwnik>();
            przeciwnicy_na_mapie = new List<Przeciwnik>();
            efekty_na_mapie = new List<Animacja>();

            PunktResp = new Rectangle[3];
            PunktResp[0] = new Rectangle(0, -50, 50, 50);
            PunktResp[1] = new Rectangle(X / 2, -50, 50, 50);
            PunktResp[2] = new Rectangle(X - 50, -50, 50, 50);
            aktualne_podloze = 0;
        }

        //public static Plansza WczytajMape() { return null; }
        /// <summary>
        /// Metoda fabrykująca mapę.
        /// </summary>
        /// <param name="plik">Pełna ścieżka do pliku.</param>
        /// <param name="poziom_gracza">Poziom gracza.</param>
        /// <param name="fabryka">Fabryka obiektów.</param>
        /// <param name="poziom_muru">Poziom muru.</param>
        /// <returns>Obiekt mapy.</returns>
        public static Plansza WczytajMape(String plik, int poziom_gracza, Fabryka fabryka, int poziom_muru)
        {
            Mapa mapa;
            List<Przeszkoda> przeszkody = new List<Przeszkoda>();
            List<Przeszkoda> podloza = new List<Przeszkoda>();
            
            XmlSerializer deserializer = new XmlSerializer(typeof(Mapa));
            using (TextReader reader = new StreamReader(plik))
            {
                mapa = (Mapa)deserializer.Deserialize(reader);
                //reader.Close();
            }
            Plansza plansza = new Plansza(mapa.szerokosc, mapa.wysokosc);
            plansza.poziom = poziom_gracza;
            plansza.baza = fabryka.wzorzec_bazy;
            plansza.baza.UstawPozycje((plansza.Szerokosc / 2) - 25, plansza.Wysokosc - 50);
            Rectangle Rplanszy = new Rectangle(0, 0, plansza.Szerokosc, plansza.Wysokosc);
            Rectangle recbazy = new Rectangle(plansza.baza.Wymiary.X - 25, plansza.baza.Wymiary.Y - 25, 100, 100);
            Gracz.PunktRespGracza = new Rectangle(plansza.baza.Wymiary.X - 75, plansza.baza.Wymiary.Y, 50, 50);
            for (int i = 0; i < mapa.lista_przeszkod.Count; ++i)
            {
                Przeszkoda prze = fabryka.ProdukujPrzeszkode(mapa.lista_przeszkod[i].nazwa_fabryczna);
                prze.UstawPozycje(mapa.lista_przeszkod[i].pozycja.x, mapa.lista_przeszkod[i].pozycja.y);
                if (mapa.lista_przeszkod[i].warstwa == 1)
                {
                    podloza.Add(prze);
                }
                else if(mapa.lista_przeszkod[i].warstwa == 2)
                {
                    if (Rplanszy.Contains(prze.Wymiary) && !plansza.baza.Wymiary.IntersectsWith(prze.Wymiary) && !Gracz.PunktRespGracza.IntersectsWith(prze.Wymiary) && !plansza.PunktResp[0].IntersectsWith(prze.Wymiary) && !plansza.PunktResp[1].IntersectsWith(prze.Wymiary) && !plansza.PunktResp[2].IntersectsWith(prze.Wymiary))
                    {
                        przeszkody.Add(prze);
                    }
                }
            }
            #region BudowaObronyBazy
            string typ_muru = Plansza.WybierzTypMuru(poziom_muru);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X - 25, plansza.baza.Wymiary.Y + 25);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X - 25, plansza.baza.Wymiary.Y);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X - 25, plansza.baza.Wymiary.Y - 25);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X, plansza.baza.Wymiary.Y - 25);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X + 25, plansza.baza.Wymiary.Y - 25);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X + 50, plansza.baza.Wymiary.Y - 25);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X + 50, plansza.baza.Wymiary.Y);
            plansza.mur_bazy.Add(fabryka.ProdukujPrzeszkode(typ_muru));
            plansza.mur_bazy.Last().UstawPozycje(plansza.baza.Wymiary.X + 50, plansza.baza.Wymiary.Y + 25);
            przeszkody.AddRange(plansza.mur_bazy);
            
            #endregion

            plansza.podloza = new DrzewoPrzeszkody(podloza, new Rectangle(0, 0, plansza.Szerokosc, plansza.Wysokosc), true);
            plansza.region = new DrzewoPrzeszkody(przeszkody, new Rectangle(0, 0, plansza.Szerokosc, plansza.Wysokosc), true);
            plansza.LosujPrzeciwnikow(fabryka);
            return plansza;
        }
        /// <summary>
        /// Metoda zmieniająca typ muru bazy po upgrade'ie.
        /// </summary>
        /// <param name="fabryka">Fabryka obiektów</param>
        /// <param name="poziom_muru">Poziom muru z warsztatu.</param>
        public void PrzebudujBaze(Fabryka fabryka, int poziom_muru)
        {
            Przeszkoda pre = fabryka.ProdukujPrzeszkode(Plansza.WybierzTypMuru(poziom_muru));
            for (int i = 0; i < mur_bazy.Count; ++i)
            {
                mur_bazy[i].WczytajNoweDaneZWzorca(pre);
            }
        }
        /// <summary>
        /// Metoda wybiera typ mapy.
        /// </summary>
        /// <param name="poziom">Poziom muru</param>
        /// <returns>Nazwę muru</returns>
        private static string WybierzTypMuru(int poziom)
        {
            switch (poziom)
            {
                case 1:
                    return "Skrzynka";
                case 2:
                    return "cegielka";
                case 3:
                    return "cegielka2";
                case 5:
                    return "cegielka3";
                case 6:
                    return "cegielka4";
                default:
                    return "mur";
            }
        }
        /// <summary>
        /// Metoda losująca przeciwników zaleźnie od poziomu.
        /// </summary>
        /// <param name="fabryka">Fabryka obiektów.</param>
        private void LosujPrzeciwnikow(Fabryka fabryka)
        {
            int max;
            if (poziom == 1) max = 15;
            else max = 20 + (10 * poziom / 35); //Narzedzia.rand.Next(18, 20);
            if (poziom >= 9) max -= LosujTypPrzeciwnikow(10, this.poziom, fabryka);
            if (poziom >= 8 && max > 0) max -= LosujTypPrzeciwnikow(9, this.poziom, fabryka);
            if (poziom >= 7 && max > 0) max -= LosujTypPrzeciwnikow(8, this.poziom, fabryka);
            if (poziom >= 6 && max > 0) max -= LosujTypPrzeciwnikow(7, this.poziom, fabryka);
            if (poziom >= 5 && max > 0) max -= LosujTypPrzeciwnikow(6, this.poziom, fabryka);
            if (poziom >= 4 && max > 0) max -= LosujTypPrzeciwnikow(5, this.poziom, fabryka);
            if (poziom >= 3 && max > 0) max -= LosujTypPrzeciwnikow(4, this.poziom, fabryka);
            if (poziom >= 2 && max > 0) max -= LosujTypPrzeciwnikow(3, this.poziom, fabryka);


            max -= LosujTypPrzeciwnikow(2,this.poziom, fabryka);
            if (max <= 0) return;
            for (int i = 0; i < max; ++i)
            {
                przeciwnicy.Push(fabryka.ProdukujPrzeciwnika("Przeciwnik: Poziom 1"));
            }
        }
        private int LosujTypPrzeciwnikow(int poziom_przeciwnika, int poziom, Fabryka fabryka)
        {
            string typ = "Przeciwnik: Poziom " + poziom_przeciwnika.ToString();
            int liczba = poziom - (poziom_przeciwnika - 2);
            for (int i = 0; i < liczba; ++i)
            {
                przeciwnicy.Push(fabryka.ProdukujPrzeciwnika(typ));
            }
            return liczba;
        }
        /// <summary>
        /// Metoda tworząca nowych przeciwników na mapie.
        /// </summary>
        /// <param name="gracz">Obiekt gracza.</param>
        public void Respawn(Gracz gracz)
        {
            if (przeciwnicy_na_mapie.Count < MAX_PRZECIWNIKOW_MAPA && przeciwnicy.Count > 0)
            {
                List<Rectangle> temp_resp = new List<Rectangle>(PunktResp);
                int liczba;
                bool wolne = false;
                while (true)
                {
                    if (temp_resp.Count == 0) return;
                    wolne = true;
                    liczba = Narzedzia.rand.Next(0, temp_resp.Count);
                    if (temp_resp[liczba].IntersectsWith(gracz.Wymiary))
                    {
                        temp_resp.RemoveAt(liczba);
                        wolne = false;
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
                if (wolne)
                {
                    Przeciwnik przec = przeciwnicy.Pop();
                    przec.UstawPozycje(PunktResp[liczba].Location);
                    przeciwnicy_na_mapie.Add(przec);
                }
            }
        }
        #region Ruch
        /// <summary>
        /// Metoda wykonuje Ruch przeciwników.
        /// </summary>
        /// <param name="fabryka">Fabryka obiektów.</param>
        /// <param name="gracz">Obiekt gracza.</param>
        public void RuszPrzeciwnikow(Fabryka fabryka, Gracz gracz)
        {
            for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
            {
                przeciwnicy_na_mapie[i].Ruch();
                przeciwnicy_na_mapie[i].RuchPocisku(this, gracz, fabryka);
            }
        }
        /// <summary>
        /// Rusza wszystkich przeciwników i sprawdza kolizje.
        /// </summary>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <param name="fabryka">Fabryka obiektów.</param>
        /// <param name="czas_strzalu">Czas strzałów.</param>
        public void RusziSprawdz(Gracz gracz, Fabryka fabryka, int czas_strzalu)
        {
            while (gracz.Pozostaly_ruch > 0)
            {
                gracz.WykonajRuch(this);
            }

            for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
            {
                while (przeciwnicy_na_mapie[i].Pozostaly_ruch > 0)
                {
                    przeciwnicy_na_mapie[i].WykonajRuchPrzeciwnika(this, fabryka, gracz, czas_strzalu);
                }
            }
        }
        #endregion

        
      
        #region Rysowanie elementow i efektow
        /// <summary>
        /// Rysuje elementów mapy.
        /// </summary>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
        public void RysujElementy(Gracz gracz, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            podloza.RysujPodloze(Kamera.Prostokat_Kamery, g, transparentPink);
            if (Opcje.wlacz_cieniowanie) region.RysujCienie(Kamera.Prostokat_Kamery, g, transparentPink);
            if (baza.Wymiary.IntersectsWith(Kamera.Prostokat_Kamery))
            {
                baza.Rysuj(g, transparentPink);
            }
            //Rysowanie przeciwnikow, którzy znajdują się na mapie.
            gracz.RysujPociski(g, transparentPink);
            if (przeciwnicy_na_mapie != null)
            {
                for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
                {
                    for (int j = 0; j < przeciwnicy_na_mapie[i].Pociski.Count; ++j)
                    {
                        przeciwnicy_na_mapie[i].Pociski[j].Rysuj(g, transparentPink);
                    }
                    if (przeciwnicy_na_mapie[i].Wymiary.IntersectsWith(Kamera.Prostokat_Kamery))
                    {
                        przeciwnicy_na_mapie[i].Rysuj(g, transparentPink);
                    }
                }
            }
            gracz.Rysuj(g, Narzedzia.transparentPink);
            region.RysujElementy(Kamera.Prostokat_Kamery, g, transparentPink);
            for (int i = 0; i < przeciwnicy_na_mapie.Count; ++i)
            {
                przeciwnicy_na_mapie[i].RysujPasekZycia(g, transparentPink);
            }
            gracz.RysujPasekZycia(g, transparentPink);
            gracz.RysujPasekXP(g, transparentPink);
        }
        /// <summary>
        /// Metoda rysująca efekty na mapie.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
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
        #endregion
        /// <summary>
        /// Sprawdza zakończenie efektów na planszy.
        /// </summary>
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
        /// <summary>
        /// Metoda zmieniająca stan efktów na mapie.
        /// </summary>
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
        /// <summary>
        /// Wczytuje grafiki teł.
        /// </summary>
        /// <param name="podloza"></param>
        public void WczytajGrafikePodloza(params Image[] podloza)
        {
            this.Podloze = podloza;
        }
        /// <summary>
        /// Zmienia aktualne tło.
        /// </summary>
        public void ZmienPodloze()
        {
            aktualne_podloze += 1;
            if (aktualne_podloze >= Podloze.Count()) aktualne_podloze = 0;
        }
        /// <summary>
        /// Zwraca aktualne tło.
        /// </summary>
        /// <returns></returns>
        public Image AktualnePodloze() { return this.Podloze[aktualne_podloze]; }

        #region Losowe mapy
        private void LosujPrzeszkody(Fabryka fabryka)
        {
            int liczba = 0, max, wylosowana;
            Rectangle recbazy = new Rectangle(this.baza.Wymiary.X - 25, this.baza.Wymiary.Y - 25, 100, 100);
            List<Point> pozycje = new List<Point>();
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
            baza.UstawPozycje(500, 925);
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
            if (poziom >= 4)
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
                przeszkody.Add(fabryka.ProdukujPrzeszkode("cegielka"));
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
        /// <summary>
        /// Generowanie losowe mapy.
        /// </summary>
        /// <param name="fabryka">Fabryka obiektów,</param>
        public void GenerujDebugMapa(Fabryka fabryka)
        {
            Random random = new Random();
            poziom += 1;
            zdobyte_punkty = 0;             //ustawiamy punkty zdobyte w tym poziomie na 0
            ukonczony_poziom = false;       //poziom nie jest ukończony
            this.LosujPrzeciwnikow(fabryka);
            this.LosujPrzeszkody(fabryka);
        }
        #endregion
    }
}