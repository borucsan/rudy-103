using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa do wczytywania map z pliku.
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "https://code.google.com/p/rudy-103/")]
    public class Mapa
    {
        /// <summary>
        /// Szerokość mapy.
        /// </summary>
        [XmlAttribute]
        public int szerokosc { get; set; }
        /// <summary>
        /// Wysokość mapy.
        /// </summary>
        [XmlAttribute]
        public int wysokosc { get; set; }
        /// <summary>
        /// Nazwa mapy.
        /// </summary>
        [XmlAttribute]
        public string nazwa { get; set; }
        /// <summary>
        /// Autor mapy.
        /// </summary>
        [XmlAttribute]
        public string autor { get; set; }
        /// <summary>
        /// Lista przeszkód na mapie.
        /// </summary>
        public List<PrzeszkodaZapisOdczyt> lista_przeszkod { get; set; }
        /// <summary>
        /// Domyślny konstruktor wymagany przez serializacje.
        /// </summary>
        public Mapa()
        {
            this.wysokosc = this.szerokosc = 0;
            this.nazwa = autor = null;
            this.lista_przeszkod = null;
        }
        /// <summary>
        /// Konstruktor mapy.
        /// </summary>
        /// <param name="wysokosc">Wysokość mapy</param>
        /// <param name="szerokosc">Szerokość mapy</param>
        /// <param name="nazwa">Nazwa mapy</param>
        /// <param name="autor">Autor mapy</param>
        public Mapa(int wysokosc, int szerokosc, string nazwa, string autor)
        {
            this.wysokosc = wysokosc;
            this.szerokosc = szerokosc;
            this.nazwa = nazwa;
            this.autor = autor;
            lista_przeszkod = new List<PrzeszkodaZapisOdczyt>();
        }
    }
}
