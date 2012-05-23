using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa trzymająca wzorce i "produkująca" obiekty.
    /// </summary>
    class Fabryka
    {
        public Dictionary<String, Przeciwnik> wzorce_przeciwnikow { get; private set; }
        public Dictionary<String, Przeszkoda> wzorce_przeszkod { get; private set; }
        public Dictionary<String, Animacja> wzorce_efektow { get; private set; }
        public Baza wzorzec_bazy { get; private set; }
        public Pocisk wzorzec_pocisku { get; private set; }
        /// <summary>
        /// Konstruktor fabryki wzorców.
        /// </summary>
        public Fabryka(bool TworzDomyslneWzorce)
        {
            wzorce_przeszkod = new Dictionary<string,Przeszkoda>();
            wzorce_przeciwnikow = new Dictionary<string,Przeciwnik>();
            wzorce_efektow = new Dictionary<string, Animacja>();
            wzorzec_pocisku = new Pocisk(0, 0, 10, 10, 5, 10, 30, Czolg.Kierunek.GORA);
            
            wzorzec_pocisku.WczytajObrazy(Multimedia.domyslny_pocisk);
            wzorzec_bazy = new Baza(0, 0, 50, 50, 10, false, Multimedia.baza);
            if (TworzDomyslneWzorce) this.TworzDomyslneWzorce();

        }
        /// <summary>
        /// Metoda dodająca wzorce przeciwników
        /// </summary>
        /// <param name="nazwa">Nazwa Wzorca</param>
        /// <param name="przeciwnik">Obiekt Wzorca</param>
        public void DodajWzorzecPrzeciwnika(String nazwa, Przeciwnik przeciwnik)
        {
            wzorce_przeciwnikow.Add(nazwa, przeciwnik);
        }
        /// <summary>
        /// Metoda dodająca wzorce efektów
        /// </summary>
        /// <param name="nazwa">Nazwa Wzorca</param>
        /// <param name="efekt">Obiekt Wzorca</param>
        public void DodajWzorzecEfektu(String nazwa, Animacja efekt)
        {
            wzorce_efektow.Add(nazwa, efekt);
        }
        /// <summary>
        /// Metoda dodająca wzorce przeszkód
        /// </summary>
        /// <param name="nazwa">Nazwa wzorca</param>
        /// <param name="przeszkoda">Obiekt wzorca</param>
        public void DodajWzorzecPrzeszkody(String nazwa, Przeszkoda przeszkoda)
        {
            wzorce_przeszkod.Add(nazwa, przeszkoda);
        }
        /// <summary>
        /// Metoda tworząca nową instancje przeszkody
        /// </summary>
        /// <param name="nazwa_wzorca">Nazwa wzorca</param>
        /// <returns>Zwraca nową instancje przeszkody</returns>
        public Przeszkoda ProdukujPrzeszkode(String nazwa_wzorca)
        {
            if(!wzorce_przeszkod.ContainsKey(nazwa_wzorca)) return null;
            return (Przeszkoda)wzorce_przeszkod[nazwa_wzorca].Clone();
        }
        /// <summary>
        /// Metoda tworząca nową instancje przeciwnika
        /// </summary>
        /// <param name="nazwa_wzorca">Nazwa wzorca</param>
        /// <returns>Zwraca nową instancje przeciwnika</returns>
        public Przeciwnik ProdukujPrzeciwnika(String nazwa_wzorca)
        {
            if (!wzorce_przeciwnikow.ContainsKey(nazwa_wzorca)) return null;
            return (Przeciwnik)wzorce_przeciwnikow[nazwa_wzorca].Clone();
        }
        /// <summary>
        /// Metoda tworząca nową instancje efektu
        /// </summary>
        /// <param name="nazwa_wzorca">Nazwa wzorca</param>
        /// <returns>Zwraca nową instancje efektu</returns>
        public Animacja ProdukujEfekt(String nazwa_wzorca)
        {
            if (!wzorce_efektow.ContainsKey(nazwa_wzorca)) return null;
            return (Animacja)wzorce_efektow[nazwa_wzorca].Clone();
        }
        public Pocisk ProdukujPocisk()
        {
            return (Pocisk)wzorzec_pocisku.Clone();
        }
        /// <summary>
        /// Metoda statyczna tworząca nową instancje gracza z podstawowymi parametrami.
        /// </summary>
        /// <returns>Zwraca instancję gracza</returns>
        public static Gracz ProdukujDomyslnegoGracza()
        {
            Gracz player = new Gracz(Gracz.PunktRespGracza.X + 5, Gracz.PunktRespGracza.Y + 5, 40, 40, 100, 5, 10, 150, 2, 10, 3);
            
            player.WczytajObrazy(Multimedia.domyslny_gracz);
            return player;
        }
        /// <summary>
        /// Metoda tworząca domyślne wzorce. Jest to metoda pomocnicza
        /// </summary>
        private void TworzDomyslneWzorce()
        {
            //Wzorce przeszkód budynków
            DodajWzorzecPrzeszkody("Budynek A", new Przeszkoda(0, 0, 100, 100, 40, false, Multimedia.budynekA));
            DodajWzorzecPrzeszkody("Budynek B", new Przeszkoda(0, 0, 100, 100, 40, false, Multimedia.budynekB));
            DodajWzorzecPrzeszkody("Budynek C", new Przeszkoda(0, 0, 100, 100, 40, false, Multimedia.budynekC));
            DodajWzorzecPrzeszkody("Garaz", new Przeszkoda(0, 0, 50, 50, 30, false, Multimedia.garaz));
            DodajWzorzecPrzeszkody("Chata", new Przeszkoda(0, 0, 70, 70, 30, false, Multimedia.chata));

            //Wzorce podloza
            DodajWzorzecPrzeszkody("Droga A1", new Przeszkoda(0, 0, 50, 50, 0, true, Multimedia.DrogaA1));
            DodajWzorzecPrzeszkody("Droga A2", new Przeszkoda(0, 0, 50, 50, 0, true, Multimedia.DrogaA2));
            DodajWzorzecPrzeszkody("Trawa", new Przeszkoda(0, 0, 25, 25, 0, true, Multimedia.Trawa));
            DodajWzorzecPrzeszkody("Ziemia", new Przeszkoda(0, 0, 25, 25, 0, true, Multimedia.Ziemia));
            DodajWzorzecPrzeszkody("Piasek", new Przeszkoda(0, 0, 25, 25, 0, true, Multimedia.Piasek));
            DodajWzorzecPrzeszkody("Woda", new Przeszkoda(0, 0, 25, 25, 0, true, Multimedia.Woda));
            

            //Wzorce przeszkód
            DodajWzorzecPrzeszkody("Skrzynka", new Przeszkoda(0, 0, 25, 25, 20, false, Multimedia.skrzynia));
            DodajWzorzecPrzeszkody("cegielka", new Przeszkoda(0, 0, 25, 25, 30, false, Multimedia.murA));
            DodajWzorzecPrzeszkody("cegielka2", new Przeszkoda(0, 0, 25, 25, 40, false, Multimedia.murB));
            DodajWzorzecPrzeszkody("cegielka3", new Przeszkoda(0, 0, 25, 25, 60, false, Multimedia.murC));
            DodajWzorzecPrzeszkody("cegielka4", new Przeszkoda(0, 0, 25, 25, 80, false, Multimedia.murD));
            DodajWzorzecPrzeszkody("mur", new Przeszkoda(0, 0, 25, 25, 20, false, Multimedia.murE));
            DodajWzorzecPrzeszkody("drzewo", new Przeszkoda(0, 0, 25, 25, 0, true, Multimedia.drzewo));
            
            //Wzorce przeciwników
            Przeciwnik [] enemy = new Przeciwnik[10];
            enemy[0] = new Przeciwnik(0, 0, 40, 40, 20, 6, 5, 150, 2, 10, 100);
            enemy[1] = new Przeciwnik(0, 0, 40, 40, 30, 6, 8, 180, 3, 10, 200);
            enemy[2] = new Przeciwnik(0, 0, 40, 40, 30, 7, 15, 210, 4, 10, 300);
            enemy[3] = new Przeciwnik(0, 0, 40, 40, 40, 7, 35, 240, 5, 10, 400);
            enemy[4] = new Przeciwnik(0, 0, 40, 40, 40, 8, 55, 270, 6, 10, 500);
            enemy[5] = new Przeciwnik(0, 0, 40, 40, 50, 8, 70, 300, 7, 10, 600);
            enemy[6] = new Przeciwnik(0, 0, 40, 40, 50, 9, 80, 330, 8, 10, 700);
            enemy[7] = new Przeciwnik(0, 0, 40, 40, 70, 11, 70, 360, 9, 10, 800);
            enemy[8] = new Przeciwnik(0, 0, 40, 40, 90, 13, 90, 390, 10, 10, 900);
            enemy[9] = new Przeciwnik(0, 0, 40, 40, 110, 15, 110, 410, 11, 10, 1000);

            enemy[0].WczytajObrazy(Multimedia.przeciwnik_1);
            enemy[1].WczytajObrazy(Multimedia.przeciwnik_2);
            enemy[2].WczytajObrazy(Multimedia.przeciwnik_3);
            enemy[3].WczytajObrazy(Multimedia.przeciwnik_4);
            enemy[4].WczytajObrazy(Multimedia.przeciwnik_5);
            enemy[5].WczytajObrazy(Multimedia.przeciwnik_6);
            enemy[6].WczytajObrazy(Multimedia.przeciwnik_7);
            enemy[7].WczytajObrazy(Multimedia.przeciwnik_8);
            enemy[8].WczytajObrazy(Multimedia.przeciwnik_9);
            enemy[9].WczytajObrazy(Multimedia.przeciwnik_10);

            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 1", enemy[0]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 2", enemy[1]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 3", enemy[2]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 4", enemy[3]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 5", enemy[4]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 6", enemy[5]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 7", enemy[6]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 8", enemy[7]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 9", enemy[8]);
            DodajWzorzecPrzeciwnika("Przeciwnik: Poziom 10", enemy[9]);

            //Wzorce efektów
            Animacja animacja_ogien = new Animacja(0, 0, 25, 25, 4, 6);
            animacja_ogien.WczytajObrazy(Multimedia.ogien);
            DodajWzorzecEfektu("Ogień", animacja_ogien);

            Animacja animacja_eksplozja = new Animacja(0, 0, 50, 50, 4, 1);
            animacja_eksplozja.WczytajObrazy(Multimedia.eksplozja);
            DodajWzorzecEfektu("Eksplozja", animacja_eksplozja);
        }
    }
}