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
    public class DrzewoPrzeszkody : Drzewo<Przeszkoda>
    {
        private List<Przeszkoda> narysowane = new List<Przeszkoda>();
        /// <summary>
        /// Konstruktor drzewa BSP dla przeszkód.
        /// </summary>
        /// <param name="lista_obiektow">Lista wszystkich obiektów podlegających podziałowi</param>
        /// <param name="wymiar">Prostokąt obiektu podlegającego partycjowaniu</param>
        /// <param name="kompresja">Parametr umożliwiający tworzenie drzewa zkompresowanego(Zawierającego tylko listy w liściach.</param>
        public DrzewoPrzeszkody(List<Przeszkoda> lista_obiektow, Rectangle wymiar, bool kompresja) : 
            base(lista_obiektow, wymiar, kompresja) { }
        #region Sprawdzanie kolizji
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
                    if (iterator.lista[i].transparent) continue;
                    Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                    if (poc.Wymiary.IntersectsWith(iterator.lista[i].Wymiary) && pr.energia > 0)
                    {
                        pr.Uszkodz(poc.sila);
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
                    if (iterator.lista[i].transparent) continue;
                    Przeszkoda pr = (Przeszkoda)(iterator.lista[i].obiekt);
                    if (obj.Wymiary.IntersectsWith(pr.Wymiary) && pr.energia > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Metoda sprawdza czy czołg koliduje z przeszkodą.
        /// </summary>
        /// <param name="obj">Obiekt czołgu</param>
        /// <returns>Obiekt z którym nastąpiła kolizja.</returns>
        public Obiekty CzyKoliduje2(Czolg obj)
        {
            if (root.lewo != null)
            {
                if (obj.Wymiary.IntersectsWith(root.lewo.Wymiary))
                {
                    Obiekty ret = Koliduje2(root.lewo, obj);
                    if (ret != null) return ret;
                }
            }
            if (root.prawo != null)
            {
                if (obj.Wymiary.IntersectsWith(root.prawo.Wymiary))
                {
                    Obiekty ret = Koliduje2(root.prawo, obj);
                    if (ret != null) return ret;
                }
            }
            return null;
        }
        private Obiekty Koliduje2(ElementDrzewa<Przeszkoda> iterator, Czolg obj)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (obj.Wymiary.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        Obiekty ret = Koliduje2(iterator.lewo, obj);
                        if (ret != null) return ret;
                    }
                }
                if (iterator.prawo != null)
                {
                    if (obj.Wymiary.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        Obiekty ret = Koliduje2(iterator.prawo, obj);
                        if (ret != null) return ret;
                    }
                }
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    if (iterator.lista[i].transparent) continue;
                    Przeszkoda pr = (Przeszkoda)(iterator.lista[i].obiekt);
                    if (obj.Wymiary.IntersectsWith(pr.Wymiary) && pr.energia > 0) return pr;
                }
            }
            return null;
        }
        #endregion

        #region Rysowanie podloza
        /// <summary>
        /// Metoda rysująca podłoże.
        /// </summary>
        /// <param name="rec">Prostokąt widoku.</param>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
        public void RysujPodloze(Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (root.lewo != null)
            {
                if (rec.IntersectsWith(root.lewo.Wymiary)) RysujPodloze(root.lewo, rec, g, transparentPink);
            }
            if (root.prawo != null)
            {
                if (rec.IntersectsWith(root.prawo.Wymiary)) RysujPodloze(root.prawo, rec, g, transparentPink);
            }
            narysowane.Clear();
        }
        private void RysujPodloze(ElementDrzewa<Przeszkoda> iterator, Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (rec.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        RysujPodloze(iterator.lewo, rec, g, transparentPink);
                    }
                }
                if (iterator.prawo != null)
                {
                    if (rec.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        RysujPodloze(iterator.prawo, rec, g, transparentPink);
                    }
                }
            }
            else
            {
                //Rysowanie przeszkód
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    
                    Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                    if (rec.IntersectsWith(iterator.lista[i].Wymiary))
                    {
                        bool juznarysowane = false;
                        for (int j = 0; j < narysowane.Count; ++j)
                        {
                            if (pr == narysowane[j])
                            {
                                juznarysowane = true;
                                break;
                            }
                        }
                        if (!juznarysowane)
                        {
                            if (Opcje.wlacz_podloza)
                            {
                                pr.Rysuj(g, transparentPink);
                                this.narysowane.Add(pr);
                            }
                            else
                            {
                                if (pr.typ == Przeszkoda.TypPrzeszkody.WODA)
                                {
                                    pr.Rysuj(g, transparentPink);
                                    this.narysowane.Add(pr);
                                }
                            }
                        }
                        
                    }
                }
            }
        }
        #endregion

        #region Rysowanie cieni
        /// <summary>
        /// Metoda rysująca cienie.
        /// </summary>
        /// <param name="rec">Prostokąt widoku.</param>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
        public void RysujCienie(Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (root.lewo != null)
            {
                if (rec.IntersectsWith(root.lewo.Wymiary)) RysujCienie(root.lewo, rec, g, transparentPink);
            }
            if (root.prawo != null)
            {
                if (rec.IntersectsWith(root.prawo.Wymiary)) RysujCienie(root.prawo, rec, g, transparentPink);
            }
        }
        private void RysujCienie(ElementDrzewa<Przeszkoda> iterator, Rectangle rec, Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (iterator.lewo != null || iterator.prawo != null)
            {
                if (iterator.lewo != null)
                {
                    if (rec.IntersectsWith(iterator.lewo.Wymiary))
                    {
                        RysujCienie(iterator.lewo, rec, g, transparentPink);
                    }
                }
                if (iterator.prawo != null)
                {
                    if (rec.IntersectsWith(iterator.prawo.Wymiary))
                    {
                        RysujCienie(iterator.prawo, rec, g, transparentPink);
                    }
                }
                this.narysowane.Clear();
            }
            else
            {
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                    if (rec.IntersectsWith(pr.Rec_cieni) && pr.energia > 0)
                    {
                        bool juznarysowane = false;
                        for (int j = 0; j < narysowane.Count; ++j)
                        {
                            if (pr == narysowane[j])
                            {
                                juznarysowane = true;
                                break;
                            }
                        }
                        if (!juznarysowane)
                        {
                            pr.RysujCienie(g, transparentPink);
                            narysowane.Add(pr);
                        }
                    }
                }
            }
        }
#endregion

        #region Rysowanie elementow
        /// <summary>
        /// Metoda rysująca elementy.
        /// </summary>
        /// <param name="rec">Prostokąt widoku.</param>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
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
            narysowane.Clear();
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
                //Rysowanie przeszkód
                for (int i = 0; i < iterator.lista.Count; ++i)
                {
                    Przeszkoda pr = (iterator.lista[i].obiekt) as Przeszkoda;
                    if (rec.IntersectsWith(iterator.lista[i].Wymiary) && pr.energia > 0)
                    {
                        bool juznarysowane = false;
                        for(int j = 0 ; j < narysowane.Count; ++j)
                        {
                            if (pr == narysowane[j])
                            {
                                juznarysowane = true;
                                break;
                            }
                        }
                        if (!juznarysowane)
                        {
                            pr.Rysuj(g, transparentPink);
                            narysowane.Add(pr);
                        }
                    }
                }
            }
        }
        #endregion

        #region Rysowanie minimapy
        /// <summary>
        /// Metoda rysująca minimapę.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="przeszkoda_mapa"></param>
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
                        graph.DrawImage(przeszkoda_mapa, pr.Wymiary.X / Narzedzia.PointToPixelVertical(5) + Kamera.Szerokosc_Ekranu / 2 - Narzedzia.PointToPixelVertical(100),
                                pr.Wymiary.Y / Narzedzia.PointToPixelHorizontal(5) + Narzedzia.PointToPixelHorizontal(30));
                }
            }
        }
        #endregion
    }
}