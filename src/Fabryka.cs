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
        public Dictionary<String, Przeciwnik> wzorce_przeciwnikow;
        private Dictionary<String, Przeszkoda> wzorce_przeszkod;
        public Dictionary<String, Efekty> wzorce_efektow;
        public Baza wzorzec_bazy { get; set; }
        private Pocisk wzorzec_pocisku;
        public Pocisk WzorzecPocisku
        {
            get
            {
                return wzorzec_pocisku;
            }
            set
            {
                wzorzec_pocisku = value;
            }
        }
        /// <summary>
        /// Konstruktor fabryki wzorców.
        /// </summary>
        public Fabryka(System.Reflection.Assembly execAssem, bool TworzDomyslneWzorce)
        {
            wzorce_przeszkod = new Dictionary<string,Przeszkoda>();
            wzorce_przeciwnikow = new Dictionary<string,Przeciwnik>();
            wzorce_efektow = new Dictionary<string, Efekty>();
            wzorzec_pocisku = new Pocisk(0, 0, 10, 10, 5, 10, Czolg.Kierunek.GORA);
            
            wzorzec_pocisku.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_left.png"))
                
                );
            wzorzec_bazy = new Baza(0, 0, 50, 50, 10, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Baza.baza_nowa.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Baza.baza_zniszczona.png")));
            if (TworzDomyslneWzorce) this.TworzDomyslneWzorce(execAssem);

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
        public void DodajWzorzecEfektu(String nazwa, Efekty efekt)
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
        public Efekty ProdukujEfekt(String nazwa_wzorca)
        {
            if (!wzorce_efektow.ContainsKey(nazwa_wzorca)) return null;
            return (Efekty)wzorce_efektow[nazwa_wzorca].Clone();
        }
        public Pocisk ProdukujPocisk()
        {
            return (Pocisk)wzorzec_pocisku.Clone();
        }
        /// <summary>
        /// Metoda statyczna tworząca nową instancje gracza z podstawowymi parametrami.
        /// </summary>
        /// <returns>Zwraca instancję gracza</returns>
        public static Gracz ProdukujDomyslnegoGracza(System.Reflection.Assembly execAssem)
        {
            Gracz player = new Gracz(425, 930, 40, 40, 100, 5, 10, 3);
            
            player.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_left.png"))
                );
            return player;
        }
        /// <summary>
        /// Metoda tworząca domyślne wzorce. Jest to metoda pomocnicza
        /// </summary>
        /// <param name="execAssem"></param>
        public void TworzDomyslneWzorce(System.Reflection.Assembly execAssem)
        {
            //Wzorce przeszkód
            
            DodajWzorzecPrzeszkody("Skrzynka", new Przeszkoda(0, 0, 25, 25, 20, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.skrzynka.png"))));
            DodajWzorzecPrzeszkody("cegielka", new Przeszkoda(0, 0, 25, 25, 30, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka.png"))));
            DodajWzorzecPrzeszkody("cegielka2", new Przeszkoda(0, 0, 25, 25, 40, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka2.png"))));
            DodajWzorzecPrzeszkody("cegielka3", new Przeszkoda(0, 0, 25, 25, 60, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka3.png"))));
            DodajWzorzecPrzeszkody("cegielka4", new Przeszkoda(0, 0, 25, 25, 80, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka4.png"))));
            DodajWzorzecPrzeszkody("mur", new Przeszkoda(0, 0, 25, 25, 20, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.mur.png"))));
            DodajWzorzecPrzeszkody("drzewo", new Przeszkoda(0, 0, 25, 25, 0, true, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.drzewo.png"))));
            DodajWzorzecPrzeszkody("Budynek A", new Przeszkoda(0, 0, 50, 50, 40, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.budynekA1.png"))));
            DodajWzorzecPrzeszkody("Budynek B", new Przeszkoda(0, 0, 50, 50, 40, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.budynekB1.png"))));
            DodajWzorzecPrzeszkody("Budynek C", new Przeszkoda(0, 0, 50, 50, 40, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.budynekC1.png"))));
            //DodajWzorzecPrzeszkody("zniszczona baza", new Przeszkoda(0, 0, 50, 50, 10, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Baza.baza_zniszczona.png"))));
            
            //Wzorce przeciwników
            Przeciwnik [] enemy = new Przeciwnik[10];
            enemy[0] = new Przeciwnik(0, 0, 40, 40, 20, 6, 5, 100);
            enemy[1] = new Przeciwnik(0, 0, 40, 40, 30, 6, 8, 200);
            enemy[2] = new Przeciwnik(0, 0, 40, 40, 30, 7, 15, 300);
            enemy[3] = new Przeciwnik(0, 0, 40, 40, 40, 7, 35, 400);
            enemy[4] = new Przeciwnik(0, 0, 40, 40, 40, 8, 55, 500);
            enemy[5] = new Przeciwnik(0, 0, 40, 40, 50, 8, 70, 600);
            enemy[6] = new Przeciwnik(0, 0, 40, 40, 50, 9, 80, 700);
            enemy[7] = new Przeciwnik(0, 0, 40, 40, 70, 11, 70, 800);
            enemy[8] = new Przeciwnik(0, 0, 40, 40, 90, 13, 90, 900);
            enemy[9] = new Przeciwnik(0, 0, 40, 40, 110, 15, 110, 1000);

            enemy[0].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_left.png"))
                );
            enemy[1].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_left.png"))
                );
            enemy[2].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_left.png"))
                );
            enemy[3].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_left.png"))
                );
            enemy[4].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_left.png"))
                );
            enemy[5].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_left.png"))
                );
            enemy[6].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_left.png"))
                );
            enemy[7].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_left.png"))
                );
            enemy[8].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_left.png"))
                );
            enemy[9].WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_left.png"))
                );

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
            Efekty e_ogien = new Efekty(0, 0, 25, 25, 4, 6);
            e_ogien.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_1.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_3.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_4.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_5.png"))
                );
            DodajWzorzecEfektu("Ogień", e_ogien);

            Efekty e_eksplozja = new Efekty(0, 0, 50, 50, 4, 1);
            e_eksplozja.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_1.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_3.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_4.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_5.png"))
                );
            DodajWzorzecEfektu("Eksplozja", e_eksplozja);

            
        }
    }
}
