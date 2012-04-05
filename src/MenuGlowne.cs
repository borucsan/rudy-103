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
            NowaGraButton.Visible = true;
            Top10Button.Visible = true;
            WyjdzButton.Visible = true;
            pictureBox1.Visible = true;
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
            
            czas1.Enabled = false;
        }

        private void NowaGraButton_Click(object sender, EventArgs e)
        {
            nowa = null;
            NowaGraButton.Visible = false;
            Top10Button.Visible = false;
            WyjdzButton.Visible = false;
            pictureBox1.Visible = false;
            label1.Visible = false;
            linkLabel1.Visible = false;
            wczytywaniePlikow = true;
            czas1.Enabled = true;
            Invalidate();
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
            System.Diagnostics.Process.Start("http://www.google.pl/", "");
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
                buforBitmapy = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            }

            //Tutaj ladujemy cala grafike
            using (Graphics g = Graphics.FromImage(buforBitmapy))
            {
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 240, 320));
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
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 240, 320));
            g.DrawString("Trwa Wczytywanie", new Font("Arial", 19, FontStyle.Regular), new SolidBrush(Color.White), new RectangleF(10, 100, 230, 50));
            if (stan == 0)
            {
                g.DrawImage(wczytywanieImage[0], new Rectangle(95, 150, 50, 50), new Rectangle(0, 0, wczytywanieImage[0].Width, wczytywanieImage[0].Height), GraphicsUnit.Pixel);
            }
            if (stan == 1)
            {
                g.DrawImage(wczytywanieImage[1], new Rectangle(95, 150, 50, 50), new Rectangle(0, 0, wczytywanieImage[1].Width, wczytywanieImage[1].Height), GraphicsUnit.Pixel);
            }
            if (stan == 2)
            {
                g.DrawImage(wczytywanieImage[2], new Rectangle(95, 150, 50, 50), new Rectangle(0, 0, wczytywanieImage[1].Width, wczytywanieImage[1].Height), GraphicsUnit.Pixel);
            }
            if (stan == 3)
            {
                g.DrawImage(wczytywanieImage[3], new Rectangle(95, 150, 50, 50), new Rectangle(0, 0, wczytywanieImage[1].Width, wczytywanieImage[1].Height), GraphicsUnit.Pixel);
            }
        }
        private void czas1_Tick(object sender, EventArgs e)
        {
            
            czas += 1;
            if (czas == 2) { nowa = new NowaGra(); }
            stan += 1;
            if (stan >= 4) stan = 0;
            Invalidate();
            if (nowa != null)
            {
                
                if (nowa.graWczytana)
                {
                    NowaGraButton.Visible = true;
                    Top10Button.Visible = true;
                    WyjdzButton.Visible = true;
                    pictureBox1.Visible = true;
                    label1.Visible = true;
                    linkLabel1.Visible = true;
                    wczytywaniePlikow = false;
                    czas1.Enabled = false;
                    czas = 0;
                    nowa.Owner = this;
                    
                    nowa.Show();
                    this.Hide();
                }
            }
        }

    }
}