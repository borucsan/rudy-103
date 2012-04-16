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
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk2_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk2_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk2_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk2_left.png"))
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
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank3_up.png")),
                //new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_up_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank3_right.png")),
                //new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_right_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank3_down.png")),
                //new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_down_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.tank3_left.png"))
                //new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_left_2.png"))
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
            //DodajWzorzecPrzeszkody("zniszczona baza", new Przeszkoda(0, 0, 50, 50, 10, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Baza.baza_zniszczona.png"))));
            
            //Wzorce przeciwników
            Przeciwnik enemy = new Przeciwnik(0, 0, 40, 40, 20, 6, 10, 100);
            enemy.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.enemy_tank_1_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.enemy_tank_1_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.enemy_tank_1_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.enemy_tank_1_left.png"))
                );

            DodajWzorzecPrzeciwnika("przeciwnik_poziom_1", enemy);
 
            //Wzorce efektów
            Efekty e_ogien = new Efekty(0, 0, 25, 25, 4, 6);
            e_ogien.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogień.flame_1.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogień.flame_2.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogień.flame_3.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogień.flame_4.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogień.flame_5.png"))
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
