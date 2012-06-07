using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Rudy_103.src
{
    /// <summary>
    /// Okno do obsługi wyświetlania statystyk
    /// oraz dodawania statystyk po zdobytym poziomie.
    /// </summary>
    public partial class Statystyki : Form
    {
        /// <summary>
        /// Obiekt warsztatu, który będzie przechowywał referencję do warsztatu z formatki Gra.
        /// </summary>
        private Warsztat warsztat;
        /// <summary>
        /// Obiekt gracza, który będzie przechowywał referencję do gracza z formatki Gra.
        /// </summary>
        private Gracz gracz;
        /// <summary>
        /// Profil gracza.
        /// </summary>
        private ProfilGracza profil;

        private ManualResetEvent mre;
        /// <summary>
        /// Domyślny konstruktor Okna statystyk.
        /// </summary>
        /// <param name="ref_warsztat">Obiekt warsztatu.</param>
        /// <param name="ref_gracz">Obiekt gracza.</param>
        /// <param name="profil">Obiekt profilu.</param>
        /// <param name="mre">Parametr blokady wątku zapisu.</param>
        public Statystyki(Warsztat ref_warsztat, Gracz ref_gracz, ProfilGracza profil, ManualResetEvent mre)
        {
            InitializeComponent();
            this.warsztat = ref_warsztat;
            this.gracz = ref_gracz;
            this.profil = profil;
            this.mre = mre;
            PunktyLabel.Text = "Punkty: " + gracz.ilosc_punktow_ulepszen;
            PoziomLabel.Text = "Poziom: " + gracz.poziom;
            UstawInformacje();
        }
        
        private void ZamknijStatystykibutton_Click(object sender, EventArgs e)
        {
            
            warsztat.UstawStatystyki(gracz);
            profil.PrzypiszInformacjedoProfilu(gracz, warsztat, true);
            Owner.Show();
            ((Gra)Owner).wlaczoneStatystyki = false;
            ((Gra)Owner).WznowGre();
            this.Close();
            mre.WaitOne();
            mre.Reset();
            ThreadPool.QueueUserWorkItem(ZapiszDane, mre);
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
        private void ZapiszDane(object stateInfo)
        {
             ManualResetEvent mre = (ManualResetEvent)stateInfo;
             try
             {
                 profil.ZapiszDane();
             }
             finally
             {
                 mre.Set();
             }
        }
    }
}