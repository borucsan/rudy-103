using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Rudy_103.src
{
    
    /// <summary>
    /// Top10 jest to klasa okna, w którym będą wyświetlone 10 najlepszych wyników.
    /// </summary>
    public partial class Top10 : Form
    {
        /// <summary>
        /// Lista wszystkich wyników, wczytywana z pliku.
        /// </summary>
        public List<Wyniki> lista_wynikow;
        /// <summary>
        /// Konstruktor klasy okna Top10.
        /// </summary>
        public Top10()
        {
            InitializeComponent();
            lista_wynikow = new List<Wyniki>();

            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/Wyniki.xml";
            if (File.Exists(path))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Wyniki>));
                TextReader textReader = new StreamReader(path);

                lista_wynikow = (List<Wyniki>)deserializer.Deserialize(textReader);
                textReader.Close();
                if (lista_wynikow != null)
                {
                    for (int i = 0; i < lista_wynikow.Count; i++)
                    {
                        if (i < 10)
                        {
                            String wynik = "" + (i + 1) + " " + lista_wynikow[i].Nick + " " + lista_wynikow[i].Punkty;
                            listawynikow.Items.Add(wynik);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Metoda ta zamyka bieżące okno i powraca do menu.
        /// </summary>
        private void WrocButton_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}