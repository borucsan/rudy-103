using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;


namespace Rudy_103.src
{
    /// <summary>
    /// Klasa profilu gracza.
    /// </summary>
    [Serializable]
    public class ProfilGracza
    {
        /// <summary>
        /// Nazwa profilu.
        /// </summary>
        [XmlAttribute]
        public string nazwa { get; set; }
        /// <summary>
        /// Data aktualizacji profilu.
        /// </summary>
        [XmlAttribute]
        public DateTime data { get; set; }
        /// <summary>
        /// Poziom map.
        /// </summary>
        [XmlAttribute]
        public int poziom { get; set; }
        /// <summary>
        /// Obiekt ulepszeń(Profil).
        /// </summary>
        public Ulepszenia ulepszenia { get; set; }
        /// <summary>
        /// Ilość punktów.
        /// </summary>
        public int punkty { get; set; }
        /// <summary>
        /// Ilość punktów doświadczenia.
        /// </summary>
        public int XP { get; set; }
        /// <summary>
        /// Ilość punktów które można wymienić na ulepszenia.(TODO)
        /// </summary>
        public int punkty_level { get; set; }
        /// <summary>
        /// Ilość żyć gracza.(Profil)
        /// </summary>
        public int zycia { get; set; }

        /// <summary>
        /// Scieżka dostępu do pliku profilu.
        /// </summary>
        [XmlIgnore]
        public string sciezka { get; set; }

        /// <summary>
        /// Konstruktor domyślny profilu.
        /// </summary>
        public ProfilGracza()
        {
            nazwa = "Pusty";
            data = new DateTime();
            poziom = 1;
            ulepszenia = new Ulepszenia();
            punkty = 0;
            punkty_level = 0;
            XP = 0;
            zycia = 3;
        }
        /// <summary>
        /// Metoda zapisu profilu do pliku.
        /// </summary>
        public void ZapiszDane()
        {
            try
            {
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "//Dane"))
                {
                    DirectoryInfo dir = new DirectoryInfo(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "//Dane");
                    dir.CreateSubdirectory("Profile");
                }
                data = DateTime.Now;
                XmlSerializer seserializer = new XmlSerializer(typeof(ProfilGracza));
                FileInfo file = new FileInfo(this.sciezka);
                using (FileStream writer = file.Open(FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    seserializer.Serialize(writer, this);
                    //reader.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Błąd zapisu gry -!" +  ex.GetType() + "\n" + ex.Message, "Błąd zapisu gry!\n", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>
        /// Klasa ulepszeń do zapisu.
        /// </summary>
        [Serializable]
        public class Ulepszenia
        {
            /// <summary>
            /// Poziom gracza (TODO)
            /// </summary>
            [XmlAttribute]
            public int poziom_gracza { get; set; }
            /// <summary>
            /// Poziom ataku(Profil)
            /// </summary>
            [XmlAttribute]
            public int poziom_ataku { get; set; }
            /// <summary>
            /// Poziom pancerza(Profil)
            /// </summary>
            [XmlAttribute]
            public int poziom_wytrzymalosci { get; set; }
            /// <summary>
            /// Poziom szybkości(Profil)
            /// </summary>
            [XmlAttribute]
            public int poziom_szybkosci { get; set; }
            /// <summary>
            /// Poziom muru(Profil)
            /// </summary>
            [XmlAttribute]
            public int poziom_muru { get; set; }
            /// <summary>
            /// Konstruktor domyślny ulepszeń(Profil)
            /// </summary>
            public Ulepszenia()
            {
                poziom_ataku = poziom_wytrzymalosci = poziom_szybkosci = poziom_muru = 1;
                poziom_gracza = 1;
            }
        }

        /// <summary>
        /// Klasa statystyk gracza.
        /// </summary>
        [Serializable]
        public class Statystyki
        {
            /// <summary>
            /// Liczba strzałów jakie oddał gracz.
            /// </summary>
            [XmlAttribute]
            public int liczba_strzalow { get; set; }
            /// <summary>
            /// Celność z jaką strzelał gracz
            /// </summary>
            [XmlAttribute]
            public double celnosc { get; set; }
            /// <summary>
            /// Liczba strzałów celnych gracza.
            /// </summary>
            [XmlAttribute]
            public int strzalow_celnych { get; set; }
            /// <summary>
            /// Konstruktor domyślny obiektu statystyk gracza.
            /// </summary>
            public Statystyki()
            {

            }
        }
    }
}
