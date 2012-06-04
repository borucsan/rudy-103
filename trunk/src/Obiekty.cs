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
        /// <summary>
        /// Poprzednia pozycja na której znajdował się obiekt.
        /// </summary>
        public Point poprzednia_pozycja { get; protected set; }
        /// <summary>
        /// Wymiary obiektu.
        /// </summary>
        protected Rectangle wymiary;
        /// <summary>
        /// Tablica bitmap obiektów.
        /// </summary>
        protected Image[] obrazy;
        /// <summary>
        /// Zmienna określa czy obiekt powoduje kolizje.
        /// </summary>
        protected bool transparent = false;
      
        /// <summary>
        /// Konstruktor bazowy dla obiektów.(Tylko do przeciążenia)
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        public Obiekty(int X, int Y, int Szer, int Wys)
        {
            wymiary = new Rectangle(X, Y, Szer, Wys);
        }
        /// <summary>
        /// Konstruktor bazowy dla obiektów.(Tylko do przeciążenia)
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
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
        /// <summary>
        /// Metoda pozwala zmieniać pozycję obiektu.
        /// </summary>
        /// <param name="zmiana_X">Zmiana położenia na osi X.</param>
        /// <param name="zmiana_Y">Zmiana położenia na osi Y.</param>
        public void ZmienPozycje(int zmiana_X, int zmiana_Y)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = wymiary.X + zmiana_X;
            wymiary.Y = wymiary.Y + zmiana_Y;
        }
        /// <summary>
        /// Metoda ustawia pozycję obiektu.
        /// </summary>
        /// <param name="X">Nowe położenie na osi X.</param>
        /// <param name="Y">Nowe położenie na osi Y.</param>
        public void UstawPozycje(int X, int Y)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = X;
            wymiary.Y = Y;
        }
        /// <summary>
        /// Metoda ustawia pozycję obiektu.
        /// </summary>
        /// <param name="poz">Nowa pozycja obiektu.</param>
        public void UstawPozycje(Point poz)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = poz.X;
            wymiary.Y = poz.Y;
        }
        /// <summary>
        /// Metoda ustawia pozycję na osi X.
        /// </summary>
        /// <param name="X">Nowe położenie na osi X.</param>
        public void UstawPozycjeX(int X)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.X = X;
        }
        /// <summary>
        /// Metoda ustawia pozycję na osi Y.
        /// </summary>
        /// <param name="Y">Nowe położenie na osi Y.</param>
        public void UstawPozycjeY(int Y)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary.Y = Y;
        }
        /// <summary>
        /// Metoda ustawia cały prostokąt wymiaru.
        /// </summary>
        /// <param name="rec">Nowy prostokąt wymiaru</param>
        public void UstawNowyRect(Rectangle rec)
        {
            poprzednia_pozycja = wymiary.Location;
            wymiary = rec;
        }
        /// <summary>
        /// Metoda zmiejszająca poziom energi obiektu.
        /// </summary>
        /// <param name="sila">Siła z jaką zadano uszkodzenie.</param>
        /// <returns>true jeśli obiekt został zniszczony</returns>
        public virtual bool Uszkodz(int sila)
        {
            return false;
        }
        /// <summary>
        /// Abstrakcyjna metoda do rysowania. Parametry do ustalenia.
        /// </summary>
        abstract public void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink);

        #region IPodzielny Members
        /// <summary>
        /// Prostokąt wymiarów.
        /// </summary>
        public Rectangle Wymiary
        {
            get { return wymiary; }
        }
        /// <summary>
        /// Zmiena określa czy obiekt powoduje kolizje.
        /// </summary>
        public bool Transparent
        {
            get { return transparent; }
        }
        #endregion


        #region IComparable Members
        /// <summary>
        /// Metoda porównująca obiekty
        /// </summary>
        /// <param name="obj">Obiekt z którym chcemy porównać</param>
        /// <returns></returns>
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
