using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa pochodna po drzewie BSP do podziału przeszkód na obszary.
    /// </summary>
    class DrzewoPrzeszkody : Drzewo<Przeszkoda>
    {
        /// <summary>
        /// Konstruktor drzewa BSP dla przeszkód.
        /// </summary>
        /// <param name="lista_obiektow">Lista wszystkich obiektów podlegających podziałowi</param>
        /// <param name="wymiar">Prostokąt obiektu podlegającego partycjowaniu</param>
        /// <param name="glebokosc">Określa ile ma być węzłów w drzewie.</param>
        /// <param name="kompresja">Parametr umożliwiający tworzenie drzewa zkompresowanego(Zawierającego tylko listy w liściach.</param>
        public DrzewoPrzeszkody(List<Przeszkoda> lista_obiektow, Rectangle wymiar, int glebokosc, bool kompresja) : 
            base(lista_obiektow, wymiar, glebokosc, kompresja) { }
        /// <summary>
        /// Metoda sprawdzająca kolizję z podanym obiektem.
        /// </summary>
        /// <param name="obj">Objekt z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        public bool CzyKoliduje(Obiekty obj)
        {
            if (obj.wymiary.IntersectsWith(root.lewo.Wymiary)) if (Koliduje(root.lewo, obj.wymiary)) return true;
            if (obj.wymiary.IntersectsWith(root.prawo.Wymiary)) if (Koliduje(root.prawo, obj.wymiary)) return true;
            return false;
        }
        /// <summary>
        /// Metoda sprawdzająca kolizję z podanym prostokątem.
        /// </summary>
        /// <param name="rec">Prostokąt z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        public bool CzyKoliduje(Rectangle rec)
        {
            if (rec.IntersectsWith(root.lewo.Wymiary)) if(Koliduje(root.lewo, rec)) return true;
            if (rec.IntersectsWith(root.prawo.Wymiary)) if(Koliduje(root.prawo, rec)) return true;
            return false;
        }
        /// <summary>
        /// Metoda sprawdzająca kolizję z podanym pociskiem.
        /// </summary>
        /// <param name="poc">Pocisk z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        public bool CzyKoliduje(Pocisk poc)
        {
            if (poc.wymiary.IntersectsWith(root.lewo.Wymiary)) if(Koliduje(root.lewo, poc)) return true;
            if (poc.wymiary.IntersectsWith(root.prawo.Wymiary)) if(Koliduje(root.prawo, poc)) return true;
            return false;
        }
        /// <summary>
        /// Metoda wewnętrzna wspomagająca sprawdzanie kolizji
        /// </summary>
        /// <param name="iterator">Kolejny węzeł drzewa</param>
        /// <param name="poc">Pocisk z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        private bool Koliduje(ElementDrzewa<Przeszkoda> iterator, Pocisk poc)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (poc.wymiary.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        if (Koliduje(iterator.lewo, poc)) return true;
                    }
                }
                if (iterator.prawo != null)
                {
                    if (poc.wymiary.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        if (Koliduje(iterator.prawo, poc)) return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    if (iterator.lista[i].transparent) continue;
                    if (poc.wymiary.IntersectsWith(iterator.lista[i].wymiary))
                    {
                        if (iterator.lista[i].Uszkodz(poc.sila))
                        {
                            iterator.lista.RemoveAt(i);
                        }
                        return true;
                    }

                }
            }
            return false;
        }
        /// <summary>
        /// Metoda wewnętrzna wspomagająca sprawdzanie kolizji
        /// </summary>
        /// <param name="iterator">Kolejny węzeł drzewa</param>
        /// <param name="rec">Prostokąt z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        private bool Koliduje(ElementDrzewa<Przeszkoda> iterator, Rectangle rec)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (rec.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        if (Koliduje(iterator.lewo, rec)) return true;
                    }
                }
                if (iterator.prawo != null)
                {
                    if (rec.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        if (Koliduje(iterator.prawo, rec)) return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    if (iterator.lista[i].transparent) continue;
                    if (rec.IntersectsWith(iterator.lista[i].wymiary)) return true;
                }
            }
            return false;
        }
        public void RysujElementy(Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (rec.IntersectsWith(root.lewo.Wymiary)) RysujEle(root.lewo, rec, g, transparentPink);
            if (rec.IntersectsWith(root.prawo.Wymiary)) RysujEle(root.prawo, rec, g, transparentPink);
        }
        private void RysujEle(ElementDrzewa<Przeszkoda> iterator, Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (rec.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        RysujEle(iterator.lewo, rec, g, transparentPink);
                    }
                }
                if (iterator.prawo != null)
                {
                    if (rec.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        RysujEle(iterator.prawo, rec, g, transparentPink);
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    if (rec.IntersectsWith(iterator.lista[i].wymiary)) iterator.lista[i].Rysuj(g, transparentPink);

                }
            }
        }
        public void RysujMape(Graphics graph, Image przeszkoda_mapa)
        {
            RysujEleMapa(root.lewo, graph, przeszkoda_mapa);
            RysujEleMapa(root.prawo, graph, przeszkoda_mapa);
        }
        private void RysujEleMapa(ElementDrzewa<Przeszkoda> iterator, Graphics graph, Image przeszkoda_mapa)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    RysujEleMapa(iterator.lewo, graph, przeszkoda_mapa);
                }
                if (iterator.prawo != null)
                {
                    RysujEleMapa(iterator.prawo, graph, przeszkoda_mapa);
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    graph.DrawImage(przeszkoda_mapa, iterator.lista[i].wymiary.X / 5 + 20,
                                iterator.lista[i].wymiary.Y / 5 + 40);
                }
            }
        }
    }
}
