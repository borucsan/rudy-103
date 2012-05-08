using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa do zapisywania wyników graczy lub gracza do pliku
    /// </summary>
    [Serializable]
    public class Wyniki
    {
        /// <summary>
        /// Określa nazwę gracza, jaką wpisał użykownik.
        /// </summary>
        [XmlElement("NazwaGracza")]
        public String Nick
        { get; set; }

        /// <summary>
        /// Ilość zdobytych punktów przez gracza podczas gry.
        /// </summary>
        [XmlElement("ZdobytePunkty")]
        public int Punkty
        { get; set; }

        /// <summary>
        /// Konstruktor domyślny.
        /// </summary>
        public Wyniki()
        {
            this.Nick = "";
            this.Punkty = 0;
        }
        /// <summary>
        /// Konstruktor parametryczny ustawiający wartości obiektu tej klasy.
        /// </summary>
        /// <param name="_Nick"></param>
        /// <param name="_punkty"></param>
        public Wyniki(String _Nick, int _punkty)
        {
            this.Nick = _Nick;
            this.Punkty = _punkty;
        }

    }
}
