using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa Kamera określająca prostokąt, który widzi gracz
    /// </summary>
    public static class Kamera
    {
        /// <summary>
        /// Wartość określająca szerokość ekranu
        /// </summary>
        public static int Szerokosc_Ekranu = 240;
        /// <summary>
        /// Wartość określająca wysokość ekranu
        /// </summary>
        public static int Wysokosc_Ekranu = 320;
        /// <summary>
        /// Prostokąt określający wymiary widoku gracza
        /// </summary>
        public static Rectangle Prostokat_Kamery = new Rectangle(0, 0, Szerokosc_Ekranu, Wysokosc_Ekranu);
        /// <summary>
        /// Metoda, która jak sama nazwa wskazuję, odświeża położenie kamery.
        /// </summary>
        public static void Odswiez_Kamere()
        {
            Kamera.Prostokat_Kamery = new Rectangle(Kamera.Prostokat_Kamery.X, Kamera.Prostokat_Kamery.Y, Szerokosc_Ekranu, Wysokosc_Ekranu);
        }
        /// <summary>
        /// Nazwa określająca czy ekran jest obrócony
        /// </summary>
        public static String Orientacja_Ekranu = "Portrait";
    }
}
