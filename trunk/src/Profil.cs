using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Rudy_103.src
{
    
    public partial class Profil : Form
    {
        private int wybrany_profil;
        private bool isProfileSelected;
        public Profil()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (profil1radioButton.Checked)
            {
                wybrany_profil = 1;
                wczytajProfilButton.Enabled = true;
                nowyProfilButton.Enabled = true;
                isProfileSelected = true;
            }
            if (profil2radioButton.Checked)
            {
                wybrany_profil = 2;
                wczytajProfilButton.Enabled = true;
                nowyProfilButton.Enabled = true;
                isProfileSelected = true;
            }
            if (profil3radioButton.Checked)
            {
                wybrany_profil = 3;
                wczytajProfilButton.Enabled = true;
                nowyProfilButton.Enabled = true;
                isProfileSelected = true;
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
            if (wybrany_profil == 1)
            {
                profil1radioButton.Text = DateTime.Now.Date.ToString(); 
                /*
                 * Tutaj powinno być nadpisanie starego zapisanego stanu
                */
                
            }
            if (wybrany_profil == 2)
            {
                profil2radioButton.Text = DateTime.Now.Date.ToString();
                /*
                 * Tutaj powinno być nadpisanie starego zapisanego stanu
                */

            }
            if (wybrany_profil == 3)
            {
                profil3radioButton.Text = DateTime.Now.Date.ToString();
                /*
                 * Tutaj powinno być nadpisanie starego zapisanego stanu
                */

            }
            panel2.Enabled = false;
            NewGameButton.Enabled = true;
            LoadCustomButton.Enabled = true;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            NewGameButton.Text = "Proszę Czekać";
            NewGameButton.Enabled = false;
            Gra newgame = new Gra();
            newgame.Owner = this.Owner;
            newgame.Show();
            this.Hide();
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {

        }

        private void LoadCustomButton_Click(object sender, EventArgs e)
        {

        }
    }
}