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
    /// Forma wywoływana kiedy zostanie zakończona rozgrywka.
    /// </summary>
    public partial class KoniecGry : Form
    {
        /// <summary>
        /// Nazwa gracza wykorzystywana do tworzenia obiektu klasy Wyniki
        /// </summary>
        public String nick_gracza;
        /// <summary>
        /// Zmienna określająca zdobyte punkty, wykorzystywana do tworzenia obiektu klasy Wyniki
        /// </summary>
        public int punkty;
        /// <summary>
        /// Lista klasy Wyniki, która jest uzupełniana z pliku i zapisywana do pliku. 
        /// </summary>
        public List<Wyniki> lista_wynikow;
        bool dodany_wynik;
        /// <summary>
        /// Konstruktor formy Końca Gry z parametrem punktów, który przekazuje gracz.
        /// </summary>
        /// <param name="_punkty"></param>
        public KoniecGry(int _punkty)
        {
            InitializeComponent();
            this.punkty = _punkty;
            punktylabel.Text = "Punkty: " + this.punkty;
            lista_wynikow = new List<Wyniki>();
        }

        private void zatwierdzambutton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Length == 0 || textBox1.Text.ToString().Length > 10)
            {
                MessageBox.Show("Nick nie prawidłowy. Podaj prawidłowy nick, do 10 znaków", "Nieprawidłowy Nick", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);

            }
            else
            {
                zatwierdzambutton.Enabled = false;
                zatwierdzambutton.Visible = false;
                nick_gracza = textBox1.Text.ToString();
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
                            if (lista_wynikow[i].Punkty < punkty)
                            {
                                lista_wynikow.Insert(i, new Wyniki(nick_gracza, punkty));
                                dodany_wynik = true;
                                break;
                            }
                        }
                        if (dodany_wynik == false)
                        {
                            lista_wynikow.Add(new Wyniki(nick_gracza, punkty));
                        }
                    }
                    else
                    {
                        lista_wynikow.Add(new Wyniki(nick_gracza, punkty));
                    }
                }
                else
                {
                    lista_wynikow.Add(new Wyniki(nick_gracza, punkty));
                }

                if (lista_wynikow != null)
                {
                    for (int i = 0; i < lista_wynikow.Count; i++)
                    {
                        String wynik = "" + (i + 1) + " " + lista_wynikow[i].Nick + " " + lista_wynikow[i].Punkty;
                        listBox1.Items.Add(wynik);
                    }
                }
                wyjdzbutton.Enabled = true;
                wyjdzbutton.Visible = true;
            }
        }

        private void wyjdzbutton_Click(object sender, EventArgs e)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Wyniki>));
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/Wyniki.xml";
            TextWriter textWriter = new StreamWriter(path);
            serializer.Serialize(textWriter, lista_wynikow);
            textWriter.Close();
            Owner.Show();
            this.Close();
        }
    }
}