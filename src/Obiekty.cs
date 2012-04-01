﻿using System;
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
    abstract class Obiekty
    {
        protected Rectangle Wymiary;
        protected Image[] obrazy;
        public Rectangle wymiary
        {
            get
            {
                return Wymiary;
            }
            set
            {
                Wymiary = value;
            }
        }
      
        /// <summary>
        /// Konstruktor bazowy dla obiektów
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu</param>
        /// <param name="Y">Pozycja pionowa obiektu</param>
        /// <param name="Szer">Prawdopodobnie tymczasowy parametr szerokości obiektu</param>
        /// <param name="Wys">Prawdopodobnie tymczasowy parametr wysokości obiektu</param>
        public Obiekty(int X, int Y, int Szer, int Wys)
        {
            Wymiary = new Rectangle(X, Y, Szer, Wys);
        }
        public Obiekty(int X, int Y, int Szer, int Wys, params Image[] obrazy)
        {
            Wymiary = new Rectangle(X, Y, Szer, Wys);
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
        /// Abstrakcyjna metoda do rysowania. Parametry do ustalenia.
        /// </summary>
        abstract public void Rysuj(Graphics g, Point pozycja_kamery, System.Drawing.Imaging.ImageAttributes transparentPink);
    }
}