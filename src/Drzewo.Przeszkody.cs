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
        /// <param name="kompresja">Parametr umożliwiający tworzenie drzewa zkompresowanego(Zawierającego tylko listy w liściach.</param>
        public DrzewoPrzeszkody(List<Przeszkoda> lista_obiektow, Rectangle wymiar, bool kompresja) : 
            base(lista_obiektow, wymiar, kompresja) { }
        /// <summary>
        /// Metoda sprawdzająca kolizję z podanym obiektem.
        /// </summary>
        /// <param name="obj">Objekt z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        public bool CzyKoliduje(Czolg obj)
        {
            if (root.lewo != null)
            {
                if (obj.Wymiary.IntersectsWith(root.lewo.Wymiary)) if (Koliduje(root.lewo, obj)) return true;
            }
            if (root.prawo != null)
            {
                if (obj.Wymiary.IntersectsWith(root.prawo.Wymiary)) if (Koliduje(root.prawo, obj)) return true;
            }
            return false;
        }
        /// <summary>
        /// Metoda sprawdzająca kolizję z podanym pociskiem.
        /// </summary>
        /// <param name="poc">Pocisk z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        public bool CzyKoliduje(Pocisk poc)
        {
            if (root.lewo != null)
            {
                if (poc.Wymiary.IntersectsWith(root.lewo.Wymiary)) if (Koliduje(root.lewo, poc)) return true;
            }
            if (root.prawo != null)
            {
                if (poc.Wymiary.IntersectsWith(root.prawo.Wymiary)) if (Koliduje(root.prawo, poc)) return true;
            }
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
                    if (poc.Wymiary.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        if (Koliduje(iterator.lewo, poc)) return true;
                    }
                }
                if (iterator.prawo != null)
                {
                    if (poc.Wymiary.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        if (Koliduje(iterator.prawo, poc)) return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                    if (pr.transparent) continue;
                    if (poc.Wymiary.IntersectsWith(iterator.lista[i].Wymiary) && pr.energia > 0)
                    {
                        if (pr.Uszkodz(poc.sila))
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
        /// <param name="obj">Prostokąt z którym przetestować kolizję</param>
        /// <returns>Zwraca true jeśli wykryje kolizję z dowolną przeszkodą</returns>
        private bool Koliduje(ElementDrzewa<Przeszkoda> iterator, Czolg obj)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (obj.Wymiary.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        if (Koliduje(iterator.lewo, obj)) return true;
                    }
                }
                if (iterator.prawo != null)
                {
                    if (obj.Wymiary.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        if (Koliduje(iterator.prawo, obj)) return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    Przeszkoda pr = (Przeszkoda)(iterator.lista[i].obiekt);
                    if (pr.transparent) continue;
                    if (obj.Wymiary.IntersectsWith(iterator.lista[i].Wymiary) && pr.energia > 0)
                    {
                        obj.ObliczPozycje(iterator.lista[i].Wymiary);
                        return true;
                    }
                }
            }
            return false;
        }
        public void RysujElementy(Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (root.lewo != null)
            {
                if (rec.IntersectsWith(root.lewo.Wymiary)) RysujEle(root.lewo, rec, g, transparentPink);
            }
            if (root.prawo != null)
            {
                if (rec.IntersectsWith(root.prawo.Wymiary)) RysujEle(root.prawo, rec, g, transparentPink);
            }
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

                if (Opcje.wlacz_cieniowanie == true)
                {
                    for (int i = 0; i < iterator.lista.Count; ++i)
                    {
                        Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                        if (rec.IntersectsWith(iterator.lista[i].Wymiary) && pr.energia > 0)
                        {
                            if (iterator.lista[i].rysuj)
                            {
                                pr.RysujCienie(g, transparentPink);
                            }
                            else
                            {
                                if(iterator.lista[i].element != null)
                                {
                                    if(!(iterator.lista[i].element.Wymiary.IntersectsWith(rec)))
                                    {
                                        pr.RysujCienie(g, transparentPink);
                                    }
                                }
                            }
                        }
                    }
                }
                    //Rysowanie przeszkód
                    for (int i = 0; i < iterator.lista.Count; ++i)
                    {
                        Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                        if (rec.IntersectsWith(iterator.lista[i].Wymiary) && pr.energia > 0)
                        {
                            if (iterator.lista[i].rysuj)
                            {
                                pr.Rysuj(g, transparentPink);
                            }
                            else
                            {
                                if (iterator.lista[i].element != null)
                                {
                                    if (!(iterator.lista[i].element.Wymiary.IntersectsWith(rec)))
                                    {
                                        pr.Rysuj(g, transparentPink);
                                    }
                                }
                            }
                                
                        }
                    }
                    for (int i = 0; i < iterator.lista.Count; ++i)
                    {
                        Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                        if (pr.energia < 0)
                        {
                            iterator.lista.RemoveAt(i);
                        }
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
                        Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                        graph.DrawImage(przeszkoda_mapa, pr.Wymiary.X / 5 + Kamera.Szerokosc_Ekranu / 2 - 100,
                                pr.Wymiary.Y / 5 + 30);
                }
            }
        }
    }
}
