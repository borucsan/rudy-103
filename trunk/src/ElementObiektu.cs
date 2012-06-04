using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa wspomagająca tworzenie obiektu, opakowywująca obiekty w drzewie.
    /// </summary>
    /// <typeparam name="T">Typ obiektów trzymanych w drzewie.</typeparam>
    public class ElementObiektu<T> where T : IPodzielny, IComparable
    {
        /// <summary>
        /// Opakowany obiekt.
        /// </summary>
        public T obiekt { get; private set; }
        private Rectangle wymiar;
        /// <summary>
        /// Zmienna określa czy obiekt wymaga narysowania.
        /// </summary>
        public bool rysuj { get; set; }
        /// <summary>
        /// Zmienna określa czy obiekt uczestniczy w kolizjach.
        /// </summary>
        public bool transparent { get; set; }
        /// <summary>
        /// Druga część obiektu, jeśli występuje.
        /// </summary>
        public ElementObiektu<T> element;
        /// <summary>
        /// Konstruktor obiektu opakowywującego obiekty.
        /// </summary>
        /// <param name="obiekt">Obiekt do opakowania.</param>
        public ElementObiektu(T obiekt)
        {
            this.obiekt = obiekt;
            this.wymiar = obiekt.Wymiary;
            this.rysuj = true;
            this.element = null;
            this.transparent = obiekt.Transparent;
        }
        /// <summary>
        /// Konstruktor obiektu opakowywującego obiekty.
        /// </summary>
        /// <param name="obiekt">Obiekt do opakowania.</param>
        /// <param name="polowa">Druga połowa obiektu.</param>
        /// <param name="rec">Wymiar części obiektu.</param>
        /// <param name="rysuj">Zmienna określa czy obiekt wymaga narysowania.</param>
        public ElementObiektu(T obiekt, Rectangle rec, ElementObiektu<T> polowa, bool rysuj)
        {
            this.obiekt = obiekt;
            this.wymiar = rec;
            this.rysuj = rysuj;
            this.element = polowa;
            this.transparent = obiekt.Transparent;
        }
        /// <summary>
        /// Konstruktor obiektu opakowywującego obiekty.
        /// </summary>
        /// <param name="obiekt">Obiekt do opakowania.</param>
        /// <param name="rec">Wymiar części obiektu.</param>
        /// <param name="rysuj">Zmienna określa czy obiekt wymaga narysowania.</param>
        public ElementObiektu(T obiekt, Rectangle rec, bool rysuj)
        {
            this.obiekt = obiekt;
            this.wymiar = rec;
            this.rysuj = rysuj;
            this.element = null;
            this.transparent = obiekt.Transparent;
        }
        /// <summary>
        /// Metoda ustawia drugą cześć obiektu.
        /// </summary>
        /// <param name="element"></param>
        public void UstawElement(ElementObiektu<T> element)
        {
            this.element = element;
        }
        #region IPodzielny Members
        /// <summary>
        /// Wymiary obiektu.
        /// </summary>
        public Rectangle Wymiary
        {
            get { return wymiar; }
        }

        #endregion
    }
}
