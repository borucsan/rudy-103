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
    /// <summary>
    /// Okno do obsługi wyświetlania statystyk
    /// </summary>
    public partial class Statystyki : Form
    {
        //private Warsztat warsztat_gracza;
        /// <summary>
        /// Domyślny konstruktor Okna statystyk 
        /// </summary>
        public Warsztat warsztat;
        public Gracz gracz;
        public Statystyki(Warsztat ref_warsztat, Gracz ref_gracz)
        {
            InitializeComponent();
            this.warsztat = ref_warsztat;
            this.gracz = ref_gracz;
            PunktyLabel.Text = "Punkty: " + gracz.ilosc_punktow_ulepszen;
            PoziomLabel.Text = "Poziom: " + gracz.poziom;
            UstawInformacje();
        }
        
        private void ZamknijStatystykibutton_Click(object sender, EventArgs e)
        {
            Owner.Show();
            ((Gra)Owner).wlaczoneStatystyki = false;
            warsztat.UstawStatystyki(gracz);
            ((Gra)Owner).WznowGre();
            this.Close();

        }

        private void DodajStatyButton_Click(object sender, EventArgs e)
        {
            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (((Button)sender).Name == "DodajAtakButton")
                {
                    warsztat.ZwiekszPoziomAtaku(gracz);
                }
                if (((Button)sender).Name == "DodajObronaButton")
                {
                    warsztat.ZwiekszPoziomPancerza(gracz);
                }
                if (((Button)sender).Name == "DodajSzybkoscButton")
                {
                    warsztat.ZwiekszPoziomSzybkosci(gracz);
                }
                if (((Button)sender).Name == "DodajMagazynekButton")
                {
                    warsztat.ZwiekszPoziomMagazynku(gracz);
                }
                if (((Button)sender).Name == "DodajZasiegButton")
                {
                    warsztat.ZwiekszPoziomZasiegu(gracz);
                }
                if (((Button)sender).Name == "DodajMagazynekButton")
                {
                    warsztat.ZwiekszPoziomMagazynku(gracz);
                }
                if (((Button)sender).Name == "DodajBazaButton")
                {
                    warsztat.ZwiekszPoziomMuru(gracz);
                }
                if (((Button)sender).Name == "DodajZycieButton")
                {
                    warsztat.ZwiekszEnergie(gracz);
                }
                UstawInformacje();
            }
        }
        private void UstawInformacje()
        {
            ObronaTextBox.Text = "" + warsztat.poziom_pancerza;
            PoziomAtakTextBox.Text = "" + warsztat.poziom_ataku;
            PoziomSzybkosciTextBox.Text = "" + warsztat.poziom_szybkosci;
            LufaTextBox.Text = "" + warsztat.poziom_magazynku;
            ZasiegTextBox.Text = "" + warsztat.poziom_zasiegu;
            ZyciaTextBox.Text = "" + gracz.energia;
            BazaTextBox.Text = "" + warsztat.poziom_muru;
            PunktyLabel.Text = "Punkty: " + gracz.ilosc_punktow_ulepszen;
        }

    }
}