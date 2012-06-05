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
    /// Klasa formy profili.
    /// </summary>
    public partial class Profil : Form
    {
        private int wybrany_profil;
        //private bool isProfileSelected;
        private ProfilGracza [] profile = new ProfilGracza[3];
        
        /// <summary>
        /// Konstruktor formy profili.
        /// </summary>
        public Profil()
        {
            InitializeComponent();
            timer1.Enabled = true;
            for (int i = 0; i < 3; ++i)
            {
                WczytajProfilXml(i);
            }
            
        }
        private void WczytajProfilXml(int index)
        {
            RadioButton rd = null;
            switch (index)
            {
                default: rd = profil1radioButton; break;
                case 1: rd = profil2radioButton; break;
                case 2: rd = profil3radioButton; break;
            }
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/Dane/Profile/Profil" + index + ".xml";
            if (File.Exists(path))
            {
                try
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(ProfilGracza));
                    using (TextReader reader = new StreamReader(path))
                    {
                        profile[index] = (ProfilGracza)deserializer.Deserialize(reader);
                        //reader.Close();
                    }
                    profile[index].sciezka = path;
                    //rd.Text = profile[index].nazwa;
                    rd.Text = profile[index].data.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd Wczytywania profiu! -" + ex.GetType() + "\n" + ex.Message, "Błąd wczytywania profilu", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    profile[index] = new ProfilGracza();
                    profile[index].sciezka = path;
                    rd.Text = "Pusty";
                }
            }
            else
            {
                profile[index] = new ProfilGracza();
                profile[index].sciezka = path;
                rd.Text = "Pusty";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (profil1radioButton.Checked)
            {
                wybrany_profil = 0;
                
                nowyProfilButton.Enabled = true;
                //isProfileSelected = true;
            }
            if (profil2radioButton.Checked)
            {
                wybrany_profil = 1;
                
                nowyProfilButton.Enabled = true;
                //isProfileSelected = true;
            }
            if (profil3radioButton.Checked)
            {
                wybrany_profil = 2;
                
                nowyProfilButton.Enabled = true;
                //isProfileSelected = true;
            }

        }

        private void wczytajProfilButton_Click(object sender, EventArgs e)
        {
            /*
             * Tutaj powinno znaleźć się wczytywanie z pliku,
             * bądź z bazy danych profilu gracza
            */
            NewGameButton.Enabled = true;
            ContinueButton.Enabled = true;
            LoadCustomButton.Enabled = true;
            panel2.Enabled = false;
            
        }

        private void nowyProfilButton_Click(object sender, EventArgs e)
        {
            ContinueButton.Enabled = true;
            panel2.Enabled = false;
            NewGameButton.Enabled = true;
            LoadCustomButton.Enabled = true;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            string poprzedni = NewGameButton.Text;
            NewGameButton.Text = "Proszę Czekać";
            NewGameButton.Enabled = false;
            string old_path = profile[wybrany_profil].sciezka;
            profile[wybrany_profil] = new ProfilGracza();
            profile[wybrany_profil].sciezka = old_path;

            Gra newgame = new Gra(profile[wybrany_profil]);
            newgame.Owner = this.Owner;
            newgame.Show();
            this.Hide();
            NewGameButton.Text = poprzedni;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            string poprzedni = ContinueButton.Text;
            ContinueButton.Text = "Proszę Czekać";
            ContinueButton.Enabled = false;
            Gra newgame = new Gra(profile[wybrany_profil]);
            newgame.Owner = this.Owner;
            newgame.Show();
            this.Hide();
            NewGameButton.Text = poprzedni;
        }

        private void LoadCustomButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Plik mapy (*.xml)|*.xml";
            dialog.FilterIndex = 0;
            dialog.InitialDirectory = Narzedzia.WykryjKarteSD();
            string poprzedni = LoadCustomButton.Text;
            LoadCustomButton.Text = "Proszę Czekać";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                
                Gra newgame = new Gra(profile[wybrany_profil], dialog.FileName);
                newgame.Owner = this.Owner;
             
                newgame.Show();
                this.Hide();
            }
            dialog.Dispose();
            LoadCustomButton.Text = poprzedni;
        }
        
    }
}