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
        public static bool wlacz_cieniowanie = true;
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
        
        public static void UstawDomyslneWartosci()
        {
            Opcje.wlacz_cieniowanie = true;
            Opcje.wlaczonePrzyciskiEkranowe = true;
            Opcje.Gora = false;
            Opcje.Prawo = false;
            Opcje.Dol = false;
            Opcje.Lewo = false;
            Opcje.Enter = false;
        }
    }
}
