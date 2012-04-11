using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Generyczna klasa drzewa BSP.
    /// </summary>
    /// <typeparam name="T">Wstępnie przyjmuje tylko typ "Obiekty"</typeparam>
    class Drzewo<T> where T : Obiekty
    {
        /// <summary>
        /// Korzeń drzewa.
        /// </summary>
        public ElementDrzewa<T> root {get; set;}
        /// <summary>
        /// Maksymalna głebokość drzewa. Określa jak dokładne mają być porównania regionów.
        /// </summary>
        public int glebokosc { get; set; }
        /// <summary>
        /// Konstruktor drzewa BSP
        /// </summary>
        /// <param name="lista_obiektow">Lista wszystkich obiektów podlegających podziałowi</param>
        /// <param name="wymiar">Prostokąt obiektu podlegającego partycjowaniu</param>
        /// <param name="glebokosc">Określa ile ma być węzłów w drzewie.</param>
        /// <param name="kompresja">Parametr umożliwiający tworzenie drzewa zkompresowanego(Zawierającego tylko listy w liściach.</param>
        public Drzewo(List<T> lista_obiektow, Rectangle wymiar, int glebokosc, bool kompresja)
        {
            this.glebokosc = glebokosc;
            if (kompresja)
            {
                root = new ElementDrzewa<T>(null, wymiar, 0);
                TworzDrzewo(root, lista_obiektow, true, 0);
            }
            else
            {
                root = new ElementDrzewa<T>(lista_obiektow, wymiar, 0);
                TworzDrzewo(root, true, 0);
            }
        }
        /// <summary>
        /// Metoda wewnętrzna tworząca "zwykłe" drzewo. 
        /// </summary>
        /// <param name="iterator">Domyślnie korzeń</param>
        /// <param name="dzielXY">Parametr okraślający jak dzielić obszar</param>
        /// <param name="iteracja">Liczba wykonań metody</param>
        private void TworzDrzewo(ElementDrzewa<T> iterator, bool dzielXY, int iteracja)
        {
            if (iteracja < glebokosc)
            {
                iteracja++;
                Rectangle k1, k2;
                List<T> lista1 = new List<T>();
                List<T> lista2 = new List<T>();
                if (dzielXY)
                {
                    k1 = new Rectangle(iterator.Wymiary.X, iterator.Wymiary.Y, iterator.Wymiary.Width / 2, iterator.Wymiary.Height);
                    k2 = new Rectangle(iterator.Wymiary.X + iterator.Wymiary.Width / 2, iterator.Wymiary.Y, iterator.Wymiary.Width / 2, iterator.Wymiary.Height);
                }
                else
                {
                    k1 = new Rectangle(iterator.Wymiary.X, iterator.Wymiary.Y, iterator.Wymiary.Width, iterator.Wymiary.Height / 2);
                    k2 = new Rectangle(iterator.Wymiary.X, iterator.Wymiary.Y + iterator.Wymiary.Height / 2, iterator.Wymiary.Width, iterator.Wymiary.Height / 2);
                }
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    if (k1.Contains(iterator.lista[i].wymiary))
                    {
                        lista1.Add(iterator.lista[i]);
                    }
                    if (k2.Contains(iterator.lista[i].wymiary))
                    {
                        lista2.Add(iterator.lista[i]);
                    }
                }
                if (lista1.Count != 0)
                {
                    iterator.lewo = new ElementDrzewa<T>(lista1, k1, iteracja);
                    TworzDrzewo(iterator.lewo, !dzielXY, iteracja);
                }
                if(lista2.Count != 0)
                {
                    iterator.prawo = new ElementDrzewa<T>(lista2, k2, iteracja);
                    TworzDrzewo(iterator.prawo, !dzielXY, iteracja);
                }
            }
        }
        /// <summary>
        /// Metoda wewnętrzna tworząca "zkompresowane" drzewo. 
        /// </summary>
        /// <param name="iterator">Domyślnie korzeń</param>
        /// <param name="lista_obiektow">Lista obiektów podlegająca podziałowi w bierzącej iteracji</param>
        /// <param name="dzielXY">Parametr okraślający jak dzielić obszar</param>
        /// <param name="iteracja">Liczba wykonań metody</param>
        private void TworzDrzewo(ElementDrzewa<T> iterator, List<T> lista_obiektow, bool dzielXY, int iteracja)
        {
            if (iteracja < glebokosc)
            {
                iteracja++;
                Rectangle k1, k2;
                List<T> lista1 = new List<T>();
                List<T> lista2 = new List<T>();
                if (dzielXY)
                {
                    k1 = new Rectangle(iterator.Wymiary.X, iterator.Wymiary.Y, iterator.Wymiary.Width / 2, iterator.Wymiary.Height);
                    k2 = new Rectangle(iterator.Wymiary.X + iterator.Wymiary.Width / 2, iterator.Wymiary.Y, iterator.Wymiary.Width / 2, iterator.Wymiary.Height);
                }
                else
                {
                    k1 = new Rectangle(iterator.Wymiary.X, iterator.Wymiary.Y, iterator.Wymiary.Width, iterator.Wymiary.Height / 2);
                    k2 = new Rectangle(iterator.Wymiary.X, iterator.Wymiary.Y + iterator.Wymiary.Height / 2, iterator.Wymiary.Width, iterator.Wymiary.Height / 2);
                }
                for (int i = 0; i < lista_obiektow.Count; ++i)
                {
                    if (k1.Contains(lista_obiektow[i].wymiary))
                    {
                        lista1.Add(lista_obiektow[i]);
                    }
                    if (k2.Contains(lista_obiektow[i].wymiary))
                    {
                        lista2.Add(lista_obiektow[i]);
                    }
                }
                if (iteracja == glebokosc)
                {
                    if (lista1.Count != 0)
                    {
                        iterator.lewo = new ElementDrzewa<T>(lista1, k1);
                        TworzDrzewo(iterator.lewo, lista1, !dzielXY, iteracja);
                    }
                    if (lista2.Count != 0)
                    {
                        iterator.prawo = new ElementDrzewa<T>(lista2, k2);
                        TworzDrzewo(iterator.prawo, lista2, !dzielXY, iteracja);
                    }
                }
                else
                {
                    if (lista1.Count != 0)
                    {
                        iterator.lewo = new ElementDrzewa<T>(null, k1);
                        TworzDrzewo(iterator.lewo, lista1, !dzielXY, iteracja);
                    }
                    if (lista2.Count != 0)
                    {
                        iterator.prawo = new ElementDrzewa<T>(null, k2);
                        TworzDrzewo(iterator.prawo, lista2, !dzielXY, iteracja);
                    }
                }
            }
        }
    }
}
