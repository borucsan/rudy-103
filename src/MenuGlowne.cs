using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Rudy_103.src;

namespace Rudy_103.src
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Konstruktor klasy głównego okna.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NowaGraButton_Click(object sender, EventArgs e)
        {
            NowaGra C_NowaGra = new NowaGra();
            C_NowaGra.Show();
        }

        private void Top10Button_Click(object sender, EventArgs e)
        {
            Top10 C_Top10 = new Top10();
            C_Top10.Show();
        }

        private void WyjdzButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Metoda która obsłguje wyłączanie gry.
        /// Tworzy MessageBox'a, w którym pyta czy użytkownik jest pewien.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            DialogResult wybor = MessageBox.Show("Czy na pewno chcesz wyjść z gry?", "Rudy 103 - Wyjście z Gry",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            e.Cancel = (wybor == DialogResult.No);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.google.pl/", "");
        }

    }
}