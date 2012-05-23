using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class ElementDrzewa<T> where T : IPodzielny, IComparable
    {
        public List<ElementObiektu<T>> lista { get; set; }
        public Rectangle Wymiary { get; set; }
        public ElementDrzewa<T> lewo { get; set; }
        public ElementDrzewa<T> prawo { get; set; }
        public int iteracja;
        public ElementDrzewa(List<ElementObiektu<T>> sublist, Rectangle wym)
        {
            lista = sublist;
            Wymiary = wym;
            prawo = lewo = null;
        }
        public ElementDrzewa(List<ElementObiektu<T>> sublist, Rectangle wym, int iteracja)
        {
            lista = sublist;
            Wymiary = wym;
            prawo = lewo = null;
            this.iteracja = iteracja;
        }
    }
}
