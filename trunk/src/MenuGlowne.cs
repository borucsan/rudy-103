using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsMobile;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsCE.Forms;
//using Rudy_103.src;

namespace Rudy_103.src
{
    public partial class MainWindow : Form
    {
        
        private int czas;
        private bool isIntro;
        private Animacja Intro_Animacja;
        
        /// <summary>
        /// Konstruktor klasy głównego okna.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            isIntro = true;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;

            czas = 0;

            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();
            Narzedzia.transparentPink = new System.Drawing.Imaging.ImageAttributes();
            Narzedzia.transparentPink.SetColorKey(Color.Pink, Color.Pink);

            //Wczytywanie Grafiki
            Multimedia.WczytajMultimedia(execAssem);

            SystemSettings.ScreenOrientation = ScreenOrientation.Angle270;
            Kamera.Prostokat_Kamery.X = 0;
            Kamera.Prostokat_Kamery.Y = 0;
            Kamera.Szerokosc_Ekranu = this.Width;
            Kamera.Wysokosc_Ekranu = this.Height;
            Kamera.Odswiez_Kamere();

            OdswiezEkran();

            Intro_Animacja = new Animacja(Kamera.Prostokat_Kamery.Width / 2 - 100, Kamera.Prostokat_Kamery.Height / 2 - 100, 200, 200, 6, 1);
            Intro_Animacja.WczytajObrazy(Multimedia.intro_images);

            czas_odswiezania.Enabled = true;
            czas1.Enabled = true;

        }

        private void NowaGraButton_Click(object sender, EventArgs e)
        {
            Profil wybor_profilu = new Profil();
            wybor_profilu.Owner = this;
            wybor_profilu.Show();
            this.Hide();
        }

        private void Top10Button_Click(object sender, EventArgs e)
        {
            Top10 C_Top10 = new Top10();
            C_Top10.Owner = this;
            
            C_Top10.Show();
            this.Hide();
        }

        private void WyjdzButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Metoda która obsłguje wyłączanie gry.
        /// Tworzy MessageBox'a, w którym pyta czy użytkownik chce zakończyć program.
        /// </summary>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            DialogResult wybor = MessageBox.Show("Czy na pewno chcesz wyjść z gry?", "Rudy 103 - Wyjście z Gry",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            e.Cancel = (wybor == DialogResult.No);
            SystemSettings.ScreenOrientation = ScreenOrientation.Angle0;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://code.google.com/p/rudy-103/", "");
        }

        /// <summary>
        /// Nadpisujemy OnPaintBackground() żeby nie przerysowywane było tło, zapobiega to miganiu obrazu
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }

        private Bitmap buforBitmapy = null;
        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            //Tworzymy nowy buffor jezeli potrzebny.
            if (buforBitmapy == null)
            {
                buforBitmapy = new Bitmap(800, 800);
            }

            //Tutaj ladujemy cala grafike
            using (Graphics g = Graphics.FromImage(buforBitmapy))
            {
                g.Clear(Color.Black);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 800, 800));
                if (isIntro)
                {
                    Intro(g);
                }
            }
            e.Graphics.DrawImage(buforBitmapy, 0, 0);
            
        }
        private void Intro(Graphics g)
        {
            g.Clear(Color.Black);
            Intro_Animacja.UstawPozycje(Kamera.Prostokat_Kamery.Width/2-100, 0);
            Intro_Animacja.Rysuj(g, Narzedzia.transparentPink);
        }
       
        private void czas1_Tick(object sender, EventArgs e)
        {
            czas += 1;
            if (czas == 6) 
            {
                isIntro = false;
                panel1.Visible = true;
                panel2.Visible = true;
                panel3.Visible = true;
                czas1.Enabled = false;
            }
            Intro_Animacja.ZmienStan();
            
        }
        private void czas_odswiezania_Tick(object sender, EventArgs e)
        {
            OdswiezEkran();
        }
        private void OdswiezEkran()
        {
            //Landscape
            if (SystemState.DisplayRotation == 0)
            {
                if (Kamera.Orientacja_Ekranu.Equals("Landscape") == false)
                {
                    Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
                    Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;
                    this.buforBitmapy = null;
                    Kamera.Orientacja_Ekranu = "Landscape";
                }
                //Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
                //Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;
            }
            //Portrait
            if (SystemState.DisplayRotation == 90 || SystemState.DisplayRotation == -90)
            {
                if (Kamera.Orientacja_Ekranu.Equals("Portrait") == false)
                {
                    Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
                    Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;
                    this.buforBitmapy = null;
                    Kamera.Orientacja_Ekranu = "Portrait";
                }
            }

            Invalidate();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.Up))
            {
                // Up
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Down))
            {
                // Down
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Left))
            {
                // Left
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Right))
            {
                // Right
            }
            if ((e.KeyCode == System.Windows.Forms.Keys.Enter))
            {
                // Enter
            }

        }

        
        

    }
}