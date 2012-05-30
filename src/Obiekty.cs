using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa bazowa dla wszystkich obiektów na planszy(np. przeszkody, czołgi...)
    /// </summary>
    public abstract class Obiekty : IPodzielny, IComparable
    {
        public Point poprzednia_pozycja { get; protected set; }
        protected Rectangle wymiary;
        protected Image[] obrazy;
        protected bool transparent = false;
      
        /// <summary>
        /// Konstruktor bazowy dla obiektów
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu</param>
        /// <param name="Y">Pozycja pionowa obiektu</param>
        /// <param name="Szer">Prawdopodobnie tymczasowy parametr szerokości obiektu</param>
        /// <param name="Wys">Prawdopodobnie tymczasowy parametr wysokości obiektu</param>
        public Obiekty(int X, int Y, int Szer, int Wys)
        {
            wymiary = new Rectangle(X, Y, Szer, Wys);
        }
        public Obiekty(int X, int Y, int Szer, int Wys, params Image[] obrazy)
        {
            wymiary = new Rectangle(X, Y, Szer, Wys);
            this.obrazy = obrazy;
        }
        /// <summary>
        /// Metoda wczytująca obrazy dla obiektów.
        /// </summary>
        /// <param name="obraz">Tablica wczytywanych obrazów</param>
        public void WczytajObrazy(params Image[] obraz)
        {
            obrazy = obraz;
        }
        /// <summary>
        /// Metoda zwraca Obrazy, wykorzystywane przy wyświetlaniu informacji o przeciwniku
        /// </summary>
        public Image ZwrocObrazy()
        {
            return this.obrazy[0];
        }
        public void ZmienPozycje(int zmiana_X, int zmiana_Y)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = wymiary.X + zmiana_X;
            wymiary.Y = wymiary.Y + zmiana_Y;
        }
        public void UstawPozycje(int X, int Y)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = X;
            wymiary.Y = Y;
        }
        public void UstawPozycje(Point poz)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = poz.X;
            wymiary.Y = poz.Y;
        }
        public void UstawPozycjeX(int X)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = X;
        }
        public void UstawPozycjeY(int Y)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.Y = Y;
        }
        public void UstawNowyRect(Rectangle rec)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary = rec;
        }
        public virtual bool Uszkodz(int sila)
        {
            return false;
        }
        /// <summary>
        /// Abstrakcyjna metoda do rysowania. Parametry do ustalenia.
        /// </summary>
        abstract public void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink);

        #region IPodzielny Members

        public Rectangle Wymiary
        {
            get { return wymiary; }
        }
        public bool Transparent
        {
            get { return transparent; }
        }
        #endregion


        #region IComparable Members

        public int CompareTo(object obj)
        {
            Obiekty objekt = obj as Obiekty;
            if (this.wymiary.Y > objekt.Wymiary.Y) return 1;
            else if (this.wymiary.Y < objekt.Wymiary.Y) return -1;
            else
            {
                if (this.wymiary.X > objekt.Wymiary.X) return 1;
                else if (this.wymiary.X == objekt.Wymiary.X) return 0;
                else return -1;
            }
        }

        #endregion
    }
}
