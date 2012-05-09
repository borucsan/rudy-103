using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa przeszkód na wczytywanej/zapisywanej mapie.
    /// </summary>
    [Serializable]
    public class PrzeszkodaZapisOdczyt
    {
        /// <summary>
        /// Nazwa fabryczna przeszkody
        /// </summary>
        [XmlAttribute]
        public String nazwa_fabryczna { get; set; }
        /// <summary>
        /// Identyfikuje typ przeszkody
        /// </summary>
        [XmlAttribute]
        public int warstwa { get; set; }
        /// <summary>
        /// Pozycja przeszkody na mapie
        /// </summary>
        public Pozycja pozycja { get; set; }
        /// <summary>
        /// Konstruktor domyślny wymagany przez serializacje
        /// </summary>
        public PrzeszkodaZapisOdczyt()
        {
            nazwa_fabryczna = null;
            pozycja = null;
            warstwa = 0;
        }
        /// <summary>
        /// Konstruktor przeszkody na wczytanej mapie.
        /// </summary>
        /// <param name="nazwa_fabryczna">Nazwa fabryczna przeszkody</param>
        /// <param name="x">Pozycja na osi x</param>
        /// <param name="y">Pozycja na osi y</param>
        /// <param name="warstwa">Warstwa w której jest przeszkoda</param>
        public PrzeszkodaZapisOdczyt(String nazwa_fabryczna, int x, int y, int warstwa)
        {
            this.nazwa_fabryczna = nazwa_fabryczna;
            this.pozycja = new Pozycja(x, y);
            this.warstwa = warstwa;
        }
        /// <summary>
        /// Klasa wewnętrzna opisująca pozycje
        /// </summary>
        [Serializable]
        public class Pozycja
        {
            /// <summary>
            /// Pozycja na osi y
            /// </summary>
            [XmlAttribute]
            public int y { get; set; }
            /// <summary>
            /// Pozycja na osi x
            /// </summary>
            [XmlAttribute]
            public int x { get; set; }
            /// <summary>
            /// Kosnstruktor domyślny wymagany przez serializację
            /// </summary>
            public Pozycja()
            {
                x = y = 0;
            }
            /// <summary>
            /// Konstruktor
            /// </summary>
            /// <param name="x">Pozycja x</param>
            /// <param name="y">Pozycja y</param>
            public Pozycja(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
    }
}
