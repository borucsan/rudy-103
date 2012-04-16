using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa obsługująca wszystkie wartości kontrolujące np. stan interfejsu
    /// </summary>
    static class Opcje
    {
        public static bool wlacz_cieniowanie = true;
        public static void UstawDomyslneWartosci()
        {
            Opcje.wlacz_cieniowanie = true;
        }
    }
}
