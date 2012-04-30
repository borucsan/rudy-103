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
//using Rudy_103.src;

namespace Rudy_103.src
{
    public partial class MainWindow : Form
    {
        
        private bool wczytywaniePlikow;
        private int stan;
        private NowaGra nowa;
        private int czas;
        private Image [] wczytywanieImage;
        /// <summary>
        /// Konstruktor klasy głównego okna.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            wczytywaniePlikow = false;
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;

            //KontynuujButton.Visible = true;
            //NowaGraButton.Visible = true;
            //Top10Button.Visible = true;
            //WyjdzButton.Visible = true;
            
            label1.Visible = true;
            linkLabel1.Visible = true;

            czas = 0;
            stan = 0;

            wczytywanieImage = new Image[4];

            System.Reflection.Assembly execAssem = System.Reflection.Assembly.GetExecutingAssembly();
            wczytywanieImage[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_1.png"));
            wczytywanieImage[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_2.png"));
            wczytywanieImage[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_3.png"));
            wczytywanieImage[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_4.png"));
            
            Kamera.Szerokosc_Ekranu = Screen.PrimaryScreen.Bounds.Width;
            Kamera.Wysokosc_Ekranu = Screen.PrimaryScreen.Bounds.Height;

            czas1.Enabled = false;

            OdswiezEkran();
        }

        private void NowaGraButton_Click(object sender, EventArgs e)
        {
            nowa = null;
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;

            //KontynuujButton.Visible = false;
            //NowaGraButton.Visible = false;
            //Top10Button.Visible = false;
            //WyjdzButton.Visible = false;
            
            //label1.Visible = false;
            //linkLabel1.Visible = false;
            wczytywaniePlikow = true;
            czas1.Enabled = true;
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
                buforBitmapy = new Bitmap(Kamera.Szerokosc_Ekranu, Kamera.Wysokosc_Ekranu);
            }

            //Tutaj ladujemy cala grafike
            using (Graphics g = Graphics.FromImage(buforBitmapy))
            {
                g.Clear(Color.Black);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, Kamera.Szerokosc_Ekranu, Kamera.Wysokosc_Ekranu));
                if (wczytywaniePlikow)
                {
                    wczytywanieNowejGry(g);
                }
            }
            e.Graphics.DrawImage(buforBitmapy, 0, 0);
            
        }
        private void wczytywanieNowejGry(Graphics g)
        {

            //g.Graphics.DrawRectangle(new Pen(Color.Blue), new Rectangle(0, 0, 240, 320));
            g.Clear(Color.Black);
            //g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 240, 320));
            g.DrawString("Trwa Wczytywanie", new Font("Arial", 15, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(Kamera.Szerokosc_Ekranu/2 - 80, Kamera.Wysokosc_Ekranu/2 - 80, 200, 30));
            if (stan == 0)
            {
                g.DrawImage(wczytywanieImage[0], new Rectangle(Kamera.Szerokosc_Ekranu/2 - 25, Kamera.Wysokosc_Ekranu/2 - 25, 50, 50), new Rectangle(0, 0, wczytywanieImage[0].Width, wczytywanieImage[0].Height), GraphicsUnit.Pixel);
            }
            if (stan == 1)
            {
                g.DrawImage(wczytywanieImage[1], new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 25, Kamera.Wysokosc_Ekranu / 2 - 25, 50, 50), new Rectangle(0, 0, wczytywanieImage[1].Width, wczytywanieImage[1].Height), GraphicsUnit.Pixel);
            }
            if (stan == 2)
            {
                g.DrawImage(wczytywanieImage[2], new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 25, Kamera.Wysokosc_Ekranu / 2 - 25, 50, 50), new Rectangle(0, 0, wczytywanieImage[1].Width, wczytywanieImage[1].Height), GraphicsUnit.Pixel);
            }
            if (stan == 3)
            {
                g.DrawImage(wczytywanieImage[3], new Rectangle(Kamera.Szerokosc_Ekranu / 2 - 25, Kamera.Wysokosc_Ekranu / 2 - 25, 50, 50), new Rectangle(0, 0, wczytywanieImage[1].Width, wczytywanieImage[1].Height), GraphicsUnit.Pixel);
            }
        }
        private void czas1_Tick(object sender, EventArgs e)
        {
            
            czas += 1;
            if (czas == 2) { nowa = new NowaGra(); }
            stan += 1;
            if (stan >= 4) stan = 0;
            OdswiezEkran();
            if (nowa != null)
            {
                
                if (nowa.graWczytana)
                {
                    panel1.Visible = true;
                    panel2.Visible = true;
                    panel3.Visible = true;

                    //KontynuujButton.Visible = true;
                    //NowaGraButton.Visible = true;
                    //Top10Button.Visible = true;
                    //WyjdzButton.Visible = true;
                    
                    //label1.Visible = true;
                    //linkLabel1.Visible = true;
                    wczytywaniePlikow = false;
                    czas1.Enabled = false;
                    czas = 0;
                    nowa.Owner = this;
                    
                    nowa.Show();
                    this.Hide();
                }
            }
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
        

    }
}