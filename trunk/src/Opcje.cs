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
    /// Klasa Opcje zostaje wywołana w trakcie gry. Jest to swego rodzaju Pauza w grze.
    /// </summary>
    public partial class Opcje : Form
    {
        /// <summary>
        /// Konstruktor klasy Opcje
        /// </summary>
        public Opcje()
        {
            InitializeComponent();
        }

        private void WznowButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WyjdzButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}