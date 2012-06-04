using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa bazy.
    /// </summary>
    public class Baza : Przeszkoda
    {
        /// <summary>
        /// Zmienna określa czy baza została zniszczona.
        /// </summary>
        public bool zniszczona{ get; set; }
        /// <summary>
        /// Konstruktor przeszkód.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="energia">Wytrzymałość maksymalna przeszkody.</param>
        /// <param name="transparent">Czy uczestniczy w kolizjach.</param>
        public Baza(int X, int Y, int Szer, int Wys, int energia, bool transparent)
            : base(X, Y, Szer, Wys, energia, transparent) { zniszczona = false; }
        /// <summary>
        /// Konstruktor przeszkód.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="energia">Wytrzymałość maksymalna przeszkody.</param>
        /// <param name="transparent">Czy uczestniczy w kolizjach.</param>
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
        public Baza(int X, int Y, int Szer, int Wys, int energia, bool transparent, params Image[] obrazy)
            : this(X, Y, Szer, Wys, energia, transparent){ this.obrazy = obrazy;}
        /// <summary>
        /// Metoda rysująca bazę.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            if (zniszczona)
            {
                g.DrawImage(obrazy[1], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                            obrazy[1].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
            }
            else
            {
                g.DrawImage(obrazy[0], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                            obrazy[0].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
            }
            }
        /// <summary>
        /// Metoda zmiejszająca poziom energi obiektu.
        /// </summary>
        /// <param name="sila">Siła z jaką zadano uszkodzenie.</param>
        /// <returns>true jeśli obiekt został zniszczony</returns>
        public new void Uszkodz(int sila)
        {
            zniszczona = true;
        }
    }
}
