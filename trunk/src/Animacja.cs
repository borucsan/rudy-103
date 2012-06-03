using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa Animacja, służy do tworzenia i wyświetlania obrazów w zdefiniowanej kolejności,
    /// co daję efekt animacji.
    /// </summary>
    public class Animacja : Obiekty, ICloneable
    {
        public int aktualny_stan;
        public int ilosc_stanow;
        public int ilosc_wykonan_animacji;
        public int wykonane_animacje;
        /// <summary>
        /// Konstruktor parametryczny klasy Animacja.
        /// </summary>
        /// <param name="X">Określa położenie na osi X.</param>
        /// <param name="Y">Określa położenie na osi Y.</param>
        /// <param name="Szer">Parametr określający szerokość w pixelach wyświetlanej animacji.</param>
        /// <param name="Wys">Parametr określający wysokość w pixelach wyświetlanej animacji.</param>
        /// <param name="_ilosc_stanow">Parametr określający ilość stanów animacji, czyli ile jest obrazów w animacji.</param>
        /// <param name="_ilosc_wykonan_animacji">Parametr określający ile razy animacja ma zostać wykonana.</param>
        public Animacja(int X, int Y, int Szer, int Wys, int _ilosc_stanow, int _ilosc_wykonan_animacji)
            : base(X, Y, Szer, Wys)
        {
            this.ilosc_stanow = _ilosc_stanow;
            this.aktualny_stan = 1;
            this.wykonane_animacje = 0;
            this.ilosc_wykonan_animacji = _ilosc_wykonan_animacji;
        }
        /// <summary>
        /// Konstruktor parametryczny klasy Animacja z uwzględnieniem obrazów.
        /// </summary>
        /// <param name="obrazy">Lista obrazów animacji.</param>
        public Animacja(int X, int Y, int Szer, int Wys, int _ilosc_stanow, int _ilosc_wykonan_animacji, params Image[] obrazy)
            : this(X, Y, Szer, Wys, _ilosc_stanow, _ilosc_wykonan_animacji)
        {
            this.wykonane_animacje = 0;
            this.aktualny_stan = 1;
            this.obrazy = obrazy;
        }
        /// <summary>
        /// Metoda rysująca na ekranie urządzenia daną animację.
        /// </summary>
        /// <param name="g">Parametr powierzchni na której rysujemy.</param>
        /// <param name="transparentPink">Parametr określający atrybuty obrazów, w naszym przypadku jest to tylko kolor przeźroczystości.</param>
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[aktualny_stan - 1], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[aktualny_stan - 1].Width, obrazy[aktualny_stan - 1].Height, GraphicsUnit.Pixel, transparentPink);

        }
        /// <summary>
        /// Metoda, która musi być wywołana, aby zmienić stan animacji, czyli przejść do następnej klatki.
        /// </summary>
        public void ZmienStan()
        {
            this.aktualny_stan += 1;
            if (this.aktualny_stan > this.ilosc_stanow)
            {
                this.aktualny_stan = 1;
                this.wykonane_animacje += 1;
            }
        }
        public object Clone()
        {
            Animacja klon = new Animacja(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.ilosc_stanow, this.ilosc_wykonan_animacji);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }
        
    }
}
