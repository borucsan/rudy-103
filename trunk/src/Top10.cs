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
    /// Top10 jest to klasa okna, w którym będą wyświetlone 10 najlepszych wyników.
    /// </summary>
    public partial class Top10 : Form
    {
        /// <summary>
        /// Konstruktor klasy okna Top10.
        /// </summary>
        public Top10()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metoda ta zamyka bieżące okno i powraca do menu, które jest w dalszym ciągu otwarte.
        /// </summary>
        private void WrocButton_Click(object sender, EventArgs e)
        {
            Owner.Show();
            this.Close();
        }
    }
}