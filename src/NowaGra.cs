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
    /// NowaGra - jest to klasa okna nowej gry.
    /// </summary>
    public partial class NowaGra : Form
    {
        private int punkty; //tymczasowe zanim klasa gracz nie bedzie miała własnych punktów
        
        private Image [] czolg;
        private int pozycja_x, pozycja_y;
        private int szybkosc;
        private String kierunek;

        private Image [] pocisk;
        private int pozycja_pocisku_x, pozycja_pocisku_y;
        private int szybkosc_pocisku;
        private String kierunek_pocisku;
       
        bool pocisk_na_mapie;
        /// <summary>
        /// Konstruktor klasy nowej gry.
        /// </summary>
        public NowaGra()
        {
            InitializeComponent();
            punkty = 0;
            szybkosc = 5;
            pozycja_x = 100;
            pozycja_y = 100;
            kierunek = "up";

            kierunek_pocisku = null;
            szybkosc_pocisku = 10;
            pozycja_pocisku_x = 0;
            pozycja_pocisku_y = 0;

            pocisk_na_mapie = false;
            //Dostajemy się do resource wkompilowaniego w aplikacje
            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();
            czolg = new Image[8];
            pocisk = new Image[4];

            czolg[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_up_1.png"));
            czolg[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_up_2.png"));

            czolg[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_down_1.png"));
            czolg[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_down_2.png"));

            czolg[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_left_1.png"));
            czolg[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_left_2.png"));

            czolg[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_right_1.png"));
            czolg[7] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.czolg_right_2.png"));

            pocisk[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_up.png"));
            pocisk[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_down.png"));
            pocisk[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_left.png"));
            pocisk[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.pocisk_right.png"));

            this.timer1.Enabled = true;
        }

        private System.Windows.Forms.KeyEventArgs wcisniety_klawisz = null;
        private void NowaGra_KeyDown(object sender, KeyEventArgs e)
        {
            wcisniety_klawisz = e;
        }
        private void NowaGra_KeyUp(object sender, KeyEventArgs e)
        {
            wcisniety_klawisz = null;
        }

        private void zmienPozycjeGracza()
        {
            if (wcisniety_klawisz != null)
            {
                switch (wcisniety_klawisz.KeyCode)
                {
                    case Keys.Up:
                        {
                            kierunek = "up";
                            if (pozycja_y >= szybkosc) pozycja_y -= szybkosc;
                            else pozycja_y = 0;
                        } break;
                    case Keys.Down:
                        {
                            kierunek = "down";
                            if (pozycja_y <= pictureBox1.Height - (szybkosc + czolg[0].Width)) pozycja_y += szybkosc;
                            else pozycja_y = pictureBox1.Height - czolg[0].Width;
                        } break;
                    case Keys.Left:
                        {
                            kierunek = "left";
                            if (pozycja_x >= szybkosc) pozycja_x -= szybkosc;
                            else pozycja_x = 0;
                        }break;
                    case Keys.Right:
                        {
                            kierunek = "right";
                            if (pozycja_x <= pictureBox1.Width - (szybkosc + czolg[0].Width)) pozycja_x += szybkosc;
                            else pozycja_x = pictureBox1.Width - czolg[0].Width;
                        } break;
                    case Keys.Enter:
                        {
                            //Metoda strzelania
                            pociskStrzelaj();
                        } break;
                }
            }
        }
        private void pociskStrzelaj()
        {
            kierunek_pocisku = kierunek;
            if (kierunek_pocisku == "up")
            {
                pozycja_pocisku_x = pozycja_x + 8;
                pozycja_pocisku_y = pozycja_y;
            }
            if (kierunek_pocisku == "down")
            {
                pozycja_pocisku_x = pozycja_x + 8;
                pozycja_pocisku_y = pozycja_y + 14;
            }
            if (kierunek_pocisku == "left")
            {
                pozycja_pocisku_x = pozycja_x;
                pozycja_pocisku_y = pozycja_y +8;
            }
            if (kierunek_pocisku == "right")
            {
                pozycja_pocisku_x = pozycja_x + 14;
                pozycja_pocisku_y = pozycja_y + 8;
            }
            pocisk_na_mapie = true;
        }
        private void zmienPozycjePocisku()
        {
            if (pocisk_na_mapie == true)
            {
                if (kierunek_pocisku == "up")
                {
                    if (pozycja_pocisku_y > 0)
                    {
                        pozycja_pocisku_y -= szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
                if (kierunek_pocisku == "down")
                {
                    if (pozycja_pocisku_y < pictureBox1.Height)
                    {
                        pozycja_pocisku_y += szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
                if (kierunek_pocisku == "left")
                {
                    if (pozycja_pocisku_x > 0)
                    {
                        pozycja_pocisku_x -= szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
                if (kierunek_pocisku == "right")
                {
                    if (pozycja_pocisku_x < pictureBox1.Width)
                    {
                        pozycja_pocisku_x += szybkosc_pocisku;
                    }
                    else
                    {
                        pocisk_na_mapie = false;
                        kierunek_pocisku = null;
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            if (kierunek == "up")
            { 
                e.Graphics.DrawImage(czolg[0], pozycja_x, pozycja_y);
                e.Graphics.DrawImage(czolg[1], pozycja_x, pozycja_y);                  
            }
            if (kierunek == "down")
            {
                e.Graphics.DrawImage(czolg[2], pozycja_x, pozycja_y);
                e.Graphics.DrawImage(czolg[3], pozycja_x, pozycja_y);
            }
            if (kierunek == "left")
            {
                e.Graphics.DrawImage(czolg[4], pozycja_x, pozycja_y);
                e.Graphics.DrawImage(czolg[5], pozycja_x, pozycja_y);
            }
            if (kierunek == "right")
            {
                e.Graphics.DrawImage(czolg[6], pozycja_x, pozycja_y);
                e.Graphics.DrawImage(czolg[7], pozycja_x, pozycja_y);
            }
            if (pocisk_na_mapie == true)
            {
                if (kierunek_pocisku == "up")
                {
                    e.Graphics.DrawImage(pocisk[0], pozycja_pocisku_x, pozycja_pocisku_y);
                }
                if (kierunek_pocisku == "down")
                {
                    e.Graphics.DrawImage(pocisk[1], pozycja_pocisku_x, pozycja_pocisku_y);
                }
                if (kierunek_pocisku == "left")
                {
                    e.Graphics.DrawImage(pocisk[2], pozycja_pocisku_x, pozycja_pocisku_y);
                }
                if (kierunek_pocisku == "right")
                {
                    e.Graphics.DrawImage(pocisk[3], pozycja_pocisku_x, pozycja_pocisku_y);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zmienPozycjeGracza();
            zmienPozycjePocisku();
            pictureBox1.Invalidate();
        }
    }
}