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
        public Statystyki()
        {
            InitializeComponent();
        }
        
        private void ZamknijStatystykibutton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

    }
}