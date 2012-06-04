using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa węzłów drzewa BSP.
    /// </summary>
    /// <typeparam name="T">Lista elementów które ma przechowywać drzewo.</typeparam>
    public class ElementDrzewa<T> where T : IPodzielny, IComparable
    {
        /// <summary>
        /// Lista elementów które przechowywuje element drzewa.
        /// </summary>
        public List<ElementObiektu<T>> lista { get; set; }
        /// <summary>
        /// Wymiary danego elementu.(Część planszy)
        /// </summary>
        public Rectangle Wymiary { get; set; }
        /// <summary>
        /// Lewa cześć elementu.
        /// </summary>
        public ElementDrzewa<T> lewo { get; set; }
        /// <summary>
        /// Prawa cześć elementu.
        /// </summary>
        public ElementDrzewa<T> prawo { get; set; }
        /// <summary>
        /// Konstruktor węzła drzewa.
        /// </summary>
        /// <param name="sublist">Lista w danym węźle.</param>
        /// <param name="wym"></param>
        public ElementDrzewa(List<ElementObiektu<T>> sublist, Rectangle wym)
        {
            lista = sublist;
            Wymiary = wym;
            prawo = lewo = null;
        }
    }
}
