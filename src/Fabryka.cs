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
        private Dictionary<String, Przeciwnik> wzorce_przeciwnikow;
        private Dictionary<String, Przeszkoda> wzorce_przeszkod;
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
            wzorzec_pocisku = new Pocisk(0, 0, 8, 10, 5, 10, Czolg.Kierunek.GORA);
            wzorzec_pocisku.WczytajObrazy(
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_up.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_right.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_down.png")),
                new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_left.png"))
                );
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
        public Pocisk ProdukujPocisk()
        {
            return wzorzec_pocisku;
        }
        /// <summary>
        /// Metoda statyczna tworząca nową instancje gracza z podstawowymi parametrami.
        /// </summary>
        /// <returns>Zwraca instancję gracza</returns>
        public static Gracz ProdukujDomyslnegoGracza(System.Reflection.Assembly execAssem)
        {
            Gracz player = new Gracz(100, 100, 50, 50, 100, 5, 10, 3);
            
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
        public void TworzDomyslneWzorce(System.Reflection.Assembly execAssem)
        {
            DodajWzorzecPrzeszkody("ziemia", new Przeszkoda(0, 0, 25, 25, true, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.ziemia.png"))));
            DodajWzorzecPrzeszkody("cegielka", new Przeszkoda(0, 0, 25, 25, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.cegielka.png"))));
            DodajWzorzecPrzeszkody("cegielka2", new Przeszkoda(0, 0, 25, 25, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.cegielka2.png"))));
            DodajWzorzecPrzeszkody("cegielka3", new Przeszkoda(0, 0, 25, 25, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.cegielka3.png"))));
            DodajWzorzecPrzeszkody("cegielka4", new Przeszkoda(0, 0, 25, 25, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.cegielka4.png"))));
            DodajWzorzecPrzeszkody("drzewo", new Przeszkoda(0, 0, 25, 25, false, new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.drzewo.png"))));
        }
    }
}
