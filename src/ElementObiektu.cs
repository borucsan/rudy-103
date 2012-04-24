using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class ElementObiektu<T> where T : IPodzielny 
    {
        public T obiekt { get; private set; }
        private Rectangle wymiar;
        public bool rysuj { get; set; }
        public ElementObiektu<T> element;
        public ElementObiektu(T obiekt)
        {
            this.obiekt = obiekt;
            this.wymiar = obiekt.Wymiary;
            this.rysuj = true;
            this.element = null;
        }
        public ElementObiektu(T obiekt, Rectangle rec, ElementObiektu<T> polowa, bool rysuj)
        {
            this.obiekt = obiekt;
            this.wymiar = rec;
            this.rysuj = rysuj;
            this.element = polowa;
        }
        public ElementObiektu(T obiekt, Rectangle rec, bool rysuj)
        {
            this.obiekt = obiekt;
            this.wymiar = rec;
            this.rysuj = rysuj;
            this.element = null;
        }
        public void UstawElement(ElementObiektu<T> element)
        {
            this.element = element;
        }
        #region IPodzielny Members

        public Rectangle Wymiary
        {
            get { return wymiar; }
        }

        #endregion
    }
}
