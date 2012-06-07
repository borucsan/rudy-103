using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa obsługująca wszystkie wartości kontrolujące np. stan interfejsu
    /// </summary>
    static class Opcje
    {
        public static bool wlacz_informacje = false;
        public static String Nazwa_Przeciwnika = "";
        public static Image Obraz_Przeciwnika = null;
        public static int przeciwnik_wytrzymalosc = 100; //W %
        public static int poziom_wytrzymalosc = 1;
        public static int poziom_sila = 1;
        public static int poziom_szybkosc = 1;
        public static int przeciwnik_punkty = 0;

        public static bool wlacz_cieniowanie = false;
        public static bool wlacz_podloza = false;
        public static bool wlaczonePrzyciskiEkranowe = true;
        public static bool Gora = false;
        public static bool Prawo = false;
        public static bool Dol = false;
        public static bool Lewo = false;
        public static bool Enter = false;

        public static Rectangle przyciskGora;
        public static Rectangle przyciskPrawo;
        public static Rectangle przyciskDol;
        public static Rectangle przyciskLewo;
        public static Rectangle przyciskEnter;

        public static void WylaczInformacje()
        {
            Opcje.wlacz_informacje = false;
            Opcje.Nazwa_Przeciwnika = "";
            Opcje.Obraz_Przeciwnika = null;
            Opcje.przeciwnik_wytrzymalosc = 100;
            Opcje.poziom_sila = 1;
            Opcje.poziom_szybkosc = 1;
            Opcje.poziom_wytrzymalosc = 1;
            Opcje.przeciwnik_punkty = 0;
            
        }

        public static void UstawDomyslneWartosci()
        {
            Opcje.wlacz_cieniowanie = false;
            Opcje.wlaczonePrzyciskiEkranowe = true;
            Opcje.Gora = false;
            Opcje.Prawo = false;
            Opcje.Dol = false;
            Opcje.Lewo = false;
            Opcje.Enter = false;
        }
    }
}
