﻿using System;
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
        /// Ilość potrzevnych punktów doświadczenia do podniesienia poziomu.
        /// </summary>
        public int XP_Potrzebne { get; set; }
        /// <summary>
        /// Ilość aktualnych punktów doświadczenia.
        /// </summary>
        public int XP_Aktualne { get; set; }
        /// <summary>
        /// Ilość punktów które można wymienić na ulepszenia.(TODO)
        /// </summary>
        public int punkty_level { get; set; }
        /// <summary>
        /// Ilość żyć gracza.(Profil)
        /// </summary>
        public int zycia { get; set; }
        /// <summary>
        /// Statystyki gracza.(Profil)
        /// </summary>
        public Statystyki statystkyki { get; set; }
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
            statystkyki = new Statystyki();
            punkty = 0;
            punkty_level = 0;
            XP_Aktualne = 0;
            XP_Potrzebne = 1000;
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
                System.Windows.Forms.MessageBox.Show("Błąd zapisu gry -!" + ex.GetType() + "\n" + ex.Message, "Błąd zapisu gry!\n", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand, System.Windows.Forms.MessageBoxDefaultButton.Button1);
            }
        }
        /// <summary>
        /// Metoda przypisuje wartości z obiektu gracza do obiektu profili.
        /// </summary>
        /// <param name="gracz">Obiekt gracza.</param>
        /// <param name="warsztat">Obiekt warsztatu.</param>
        /// <param name="zapisz_punkty">Parametr określa czy zapisywać punkty rankingowe do profilu</param>
        public void PrzypiszInformacjedoProfilu(Gracz gracz, Warsztat warsztat, bool zapisz_punkty)
        {
            this.punkty_level = gracz.ilosc_punktow_ulepszen;
            if (zapisz_punkty) this.punkty = gracz.punkty;
            this.XP_Aktualne = gracz.XP_Aktualne;
            this.XP_Potrzebne = gracz.XP_Potrzebne;
            this.zycia = gracz.energia;
            this.ulepszenia.poziom_gracza = gracz.poziom;

            this.ulepszenia.poziom_ataku = warsztat.poziom_ataku;
            this.ulepszenia.poziom_magazynku = warsztat.poziom_magazynku;
            this.ulepszenia.poziom_muru = warsztat.poziom_muru;
            this.ulepszenia.poziom_szybkosci = warsztat.poziom_szybkosci;
            this.ulepszenia.poziom_wytrzymalosci = warsztat.poziom_pancerza;
            this.ulepszenia.poziom_zasiegu = warsztat.poziom_zasiegu;

            this.statystkyki.liczba_strzalow = gracz.Strzalow;
            this.statystkyki.strzalow_celnych = gracz.Trafien;
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
            /// Poziom zasięgu(Profil)
            /// </summary>
            [XmlAttribute]
            public int poziom_zasiegu { get; set; }
            /// <summary>
            /// Poziom magazynku(Profil)
            /// </summary>
            [XmlAttribute]
            public int poziom_magazynku { get; set; }
            /// <summary>
            /// Konstruktor domyślny ulepszeń(Profil)
            /// </summary>
            public Ulepszenia()
            {
                poziom_ataku = poziom_wytrzymalosci = poziom_szybkosci = poziom_muru = poziom_zasiegu = poziom_magazynku = 1;
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
            /// Liczba strzałów celnych gracza.
            /// </summary>
            [XmlAttribute]
            public int strzalow_celnych { get; set; }
            /// <summary>
            /// Konstruktor domyślny obiektu statystyk gracza.
            /// </summary>
            public Statystyki()
            {
                liczba_strzalow = strzalow_celnych = 0;
            }
            /// <summary>
            /// Zwraca celność z jaką strzelał gracz.
            /// </summary>
            [XmlIgnore]
            public double Celnosc
            {
                get
                {
                    return (strzalow_celnych / liczba_strzalow) * 100;
                }
            }
        }
    }
}
