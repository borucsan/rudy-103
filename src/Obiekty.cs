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
    abstract class Obiekty : IPodzielny
    {
        public Point poprzednia_pozycja { get; protected set; }
        protected Rectangle wymiary;
        protected Image[] obrazy;
      
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
            wymiary.X = wymiary.X + zmiana_X;
            wymiary.Y = wymiary.Y + zmiana_Y;
        }
        public void UstawPozycje(int X, int Y)
        {
            wymiary.X = X;
            wymiary.Y = Y;
        }
        public void UstawPozycje(Point poz)
        {
            wymiary.X = poz.X;
            wymiary.Y = poz.Y;
        }
        public void UstawPozycjeX(int X)
        {
            wymiary.X = X;
        }
        public void UstawPozycjeY(int Y)
        {
            wymiary.Y = Y;
        }
        public void UstawNowyRect(Rectangle rec)
        {
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

        #endregion
    }
}
