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
    /// <typeparam name="T">Przyjmuje obiekty zwracające Rectangle</typeparam>
    class Drzewo<T> where T : IPodzielny
    {
        /// <summary>
        /// Korzeń drzewa.
        /// </summary>
        public ElementDrzewa<T> root {get; private set;}
        /// <summary>
        /// Konstruktor drzewa BSP
        /// </summary>
        /// <param name="lista_obiektow">Lista wszystkich obiektów podlegających podziałowi</param>
        /// <param name="wymiar">Prostokąt obiektu podlegającego partycjowaniu</param>
        /// <param name="kompresja">Parametr umożliwiający tworzenie drzewa zkompresowanego(Zawierającego tylko listy w liściach.</param>
        public Drzewo(List<T> lista_obiektow, Rectangle wymiar, bool kompresja)
        {
            List<ElementObiektu<T>> elem = new List<ElementObiektu<T>>();
            for (int i = 0; i < lista_obiektow.Count; ++i)
            {
                elem.Add(new ElementObiektu<T>(lista_obiektow[i]));
            }
            if (kompresja)
            {
                root = new ElementDrzewa<T>(null, wymiar, 0);
                TworzDrzewo(root, elem);
            }
            else
            {
                root = new ElementDrzewa<T>(elem, wymiar, 0);
                TworzDrzewo(root);
            }
        }
        /// <summary>
        /// Metoda wewnętrzna tworząca "zwykłe" drzewo. 
        /// </summary>
        /// <param name="iterator">Domyślnie korzeń</param>
        private void TworzDrzewo(ElementDrzewa<T> iterator)
        {
                Rectangle k1, k2;
                List<ElementObiektu<T>> lista1 = new List<ElementObiektu<T>>();
                List<ElementObiektu<T>> lista2 = new List<ElementObiektu<T>>();
                Narzedzia.PodzialProstokata pp = Narzedzia.DzielProsokat(iterator.Wymiary, out k1, out k2);
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    if (k1.Contains(iterator.lista[i].Wymiary))
                    {
                        lista1.Add(iterator.lista[i]);
                    }
                    if (k2.Contains(iterator.lista[i].Wymiary))
                    {
                        lista2.Add((iterator.lista[i]));
                    }
                    if (k1.IntersectsWith(iterator.lista[i].Wymiary) && k2.IntersectsWith(iterator.lista[i].Wymiary))
                    {
                        Rectangle cz1, cz2;
                        if (pp == Narzedzia.PodzialProstokata.X)
                        {
                            int podzial = iterator.lista[i].Wymiary.Right - k1.Right;
                            Narzedzia.DzielProsokat(iterator.lista[i].Wymiary, out cz1, out cz2, Narzedzia.PodzialProstokata.X, podzial);
                        }
                        else
                        {
                            int podzial = iterator.lista[i].Wymiary.Bottom - k1.Bottom;
                            Narzedzia.DzielProsokat(iterator.lista[i].Wymiary, out cz1, out cz2, Narzedzia.PodzialProstokata.Y, podzial);
                        }
                        ElementObiektu<T> el1, el2;
                        
                        el1 = new ElementObiektu<T>(iterator.lista[i].obiekt, cz1, true);
                        el2 = new ElementObiektu<T>(iterator.lista[i].obiekt, cz2, false);
                        lista1.Add(el1);
                        lista2.Add(el2);
                    }
                }
                if ((k1.Width < 50 && k1.Height < 50))
                {
                    if (lista1.Count != 0)
                    {
                        iterator.lewo = new ElementDrzewa<T>(lista1, k1);
                    }
                    if (lista2.Count != 0)
                    {
                        iterator.prawo = new ElementDrzewa<T>(lista2, k2);
                    }
                }
                else
                {
                    if (lista1.Count != 0)
                    {
                        iterator.lewo = new ElementDrzewa<T>(lista1, k1);
                        TworzDrzewo(iterator.lewo);
                    }
                    if (lista2.Count != 0)
                    {
                        iterator.prawo = new ElementDrzewa<T>(lista2, k2);
                        TworzDrzewo(iterator.prawo);
                    }
                }
            }
        
        /// <summary>
        /// Metoda wewnętrzna tworząca "zkompresowane" drzewo. 
        /// </summary>
        /// <param name="iterator">Domyślnie korzeń</param>
        /// <param name="lista_obiektow">Lista obiektów podlegająca podziałowi w bierzącej iteracji</param>
        private void TworzDrzewo(ElementDrzewa<T> iterator, List<ElementObiektu<T>> lista_obiektow)
        {
                Rectangle k1, k2;
                List<ElementObiektu<T>> lista1 = new List<ElementObiektu<T>>();
                List<ElementObiektu<T>> lista2 = new List<ElementObiektu<T>>();
                Narzedzia.PodzialProstokata pp = Narzedzia.DzielProsokat(iterator.Wymiary, out k1, out k2);
                for (int i = 0; i < lista_obiektow.Count; ++i)
                {
                    if (k1.Contains(lista_obiektow[i].Wymiary))
                    {
                        lista1.Add(lista_obiektow[i]);
                    }
                    if (k2.Contains(lista_obiektow[i].Wymiary))
                    {
                        lista2.Add(lista_obiektow[i]);
                    }
                    if (k1.IntersectsWith(lista_obiektow[i].Wymiary) && k2.IntersectsWith(lista_obiektow[i].Wymiary))
                    {
                        Rectangle cz1, cz2;
                        if (pp == Narzedzia.PodzialProstokata.X)
                        {
                            int podzial = lista_obiektow[i].Wymiary.Width - (lista_obiektow[i].Wymiary.Right - k1.Right);
                            Narzedzia.DzielProsokat(lista_obiektow[i].Wymiary, out cz1, out cz2, Narzedzia.PodzialProstokata.X, podzial);
                        }
                        else
                        {
                            int podzial = lista_obiektow[i].Wymiary.Height - (lista_obiektow[i].Wymiary.Bottom - k1.Bottom);
                            Narzedzia.DzielProsokat(lista_obiektow[i].Wymiary, out cz1, out cz2, Narzedzia.PodzialProstokata.Y, podzial);
                        }
                        ElementObiektu<T> el1, el2;
                        if (lista_obiektow[i].rysuj)
                        {
                            el1 = new ElementObiektu<T>(lista_obiektow[i].obiekt, cz1, true);
                            el2 = new ElementObiektu<T>(lista_obiektow[i].obiekt, cz2, el1, false);
                        }
                        else
                        {
                            el1 = new ElementObiektu<T>(lista_obiektow[i].obiekt, cz1, lista_obiektow[i].element, false);
                            el2 = new ElementObiektu<T>(lista_obiektow[i].obiekt, cz2, lista_obiektow[i].element, false);
                        }
                        lista1.Add(el1);
                        lista2.Add(el2);
                    }
                }
                if ((k1.Width < 50 && k1.Height < 50))
                {
                    if (lista1.Count != 0)
                    {
                        iterator.lewo = new ElementDrzewa<T>(lista1, k1);
                    }
                    if (lista2.Count != 0)
                    {
                        iterator.prawo = new ElementDrzewa<T>(lista2, k2);
                    }
                }
                else
                {
                    if (lista1.Count != 0)
                    {
                        iterator.lewo = new ElementDrzewa<T>(null, k1);
                        TworzDrzewo(iterator.lewo, lista1);
                    }
                    if (lista2.Count != 0)
                    {
                        iterator.prawo = new ElementDrzewa<T>(null, k2);
                        TworzDrzewo(iterator.prawo, lista2);
                    }
                }
            }
        }
}
