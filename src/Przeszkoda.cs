using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa przeszkody.
    /// </summary>
    public class Przeszkoda : Obiekty, ICloneable
    {
        /// <summary>
        /// Energia przeszkody.
        /// </summary>
        public int energia { get; private set; }
        /// <summary>
        /// Nazwa przeszkody
        /// </summary>
        public TypPrzeszkody typ { get; private set; }

        /// <summary>
        /// Zwraca prostokąt cieni przeszkody.
        /// </summary>
        public Rectangle Rec_cieni
        {
            get
            {
                if (typ == TypPrzeszkody.BUDYNKI)
                {
                    return new Rectangle(wymiary.X, wymiary.Bottom, this.wymiary.Width, 100);
                }
                else if (typ == TypPrzeszkody.MURY)
                {
                    return new Rectangle(wymiary.X, wymiary.Y, this.wymiary.Width + 5, this.wymiary.Height + 5);
                }
                else return new Rectangle();
            }
        }
        /// <summary>
        /// Konstruktor przeszkód.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="energia">Wytrzymałość maksymalna przeszkody.</param>
        /// <param name="transparent">Czy uczestniczy w kolizjach.</param>
        /// <param name="typ">Typ przeszkody.</param>
        public Przeszkoda(int X, int Y, int Szer, int Wys, int energia, bool transparent, TypPrzeszkody typ)
            : base(X, Y, Szer, Wys)
        {
            this.energia = energia;
            this.transparent = transparent;
            this.typ = typ;
        }
        /// <summary>
        /// Konstruktor przeszkód.
        /// </summary>
        /// <param name="X">Pozycja pozioma obiektu.</param>
        /// <param name="Y">Pozycja pionowa obiektu.</param>
        /// <param name="Szer">Szerokość obiektu.</param>
        /// <param name="Wys">Wysokość obiektu.</param>
        /// <param name="energia">Wytrzymałość maksymalna przeszkody.</param>
        /// <param name="transparent">Czy uczestniczy w kolizjach.</param>
        /// <param name="typ">Typ przeszkody.</param>
        /// <param name="obrazy">Tablica bitmap obiektów.</param>
        public Przeszkoda(int X, int Y, int Szer, int Wys, int energia, bool transparent, TypPrzeszkody typ, params Image[] obrazy)
            : this(X, Y, Szer, Wys, energia, transparent, typ)
        {
            this.obrazy = obrazy;
        }
        /// <summary>
        /// Metoda rysująca przeszkodę.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
        public override void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink) 
        {
            g.DrawImage(obrazy[0], new Rectangle(Wymiary.X - Kamera.Prostokat_Kamery.X, Wymiary.Y - Kamera.Prostokat_Kamery.Y, Wymiary.Width, Wymiary.Height), 0, 0,
                        obrazy[0].Width, obrazy[0].Height, GraphicsUnit.Pixel, transparentPink);
        }
        /// <summary>
        /// Metoda rysująca cienie przeszkód.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="transparentPink"></param>
        public void RysujCienie(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            if (typ == TypPrzeszkody.BUDYNKI)
            {
                Point[] points = new Point[4];
                points[0].X = Wymiary.X - Kamera.Prostokat_Kamery.X;
                points[0].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;
                points[1].X = Wymiary.X + 10 - Kamera.Prostokat_Kamery.X;
                points[1].Y = Wymiary.Y + Wymiary.Height + Wymiary.Height / 3 - Kamera.Prostokat_Kamery.Y;
                points[2].X = Wymiary.X + Wymiary.Width + 10 - Kamera.Prostokat_Kamery.X;
                points[2].Y = Wymiary.Y + Wymiary.Height + Wymiary.Height / 3 - Kamera.Prostokat_Kamery.Y;
                points[3].X = Wymiary.X + Wymiary.Width - Kamera.Prostokat_Kamery.X;
                points[3].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;

                g.FillPolygon(new SolidBrush(Color.Black), points);
            }
            else if (typ == TypPrzeszkody.MURY)
            {
                Point[] points = new Point[4];
                points[0].X = Wymiary.X - Kamera.Prostokat_Kamery.X;
                points[0].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;
                points[1].X = Wymiary.X + 5 - Kamera.Prostokat_Kamery.X;
                points[1].Y = Wymiary.Y + Wymiary.Height + 5 - Kamera.Prostokat_Kamery.Y;
                points[2].X = Wymiary.X + Wymiary.Width + 5 - Kamera.Prostokat_Kamery.X;
                points[2].Y = Wymiary.Y + Wymiary.Height + 5 - Kamera.Prostokat_Kamery.Y;
                points[3].X = Wymiary.X + Wymiary.Width - Kamera.Prostokat_Kamery.X;
                points[3].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;

                g.FillPolygon(new SolidBrush(Color.Black), points);

                Point[] points2 = new Point[4];
                points2[0].X = Wymiary.X + Wymiary.Width - Kamera.Prostokat_Kamery.X;
                points2[0].Y = Wymiary.Y - Kamera.Prostokat_Kamery.Y;
                points2[1].X = Wymiary.X + Wymiary.Width + 5 - Kamera.Prostokat_Kamery.X;
                points2[1].Y = Wymiary.Y + 5 - Kamera.Prostokat_Kamery.Y;
                points2[2].X = Wymiary.X + Wymiary.Width + 5 - Kamera.Prostokat_Kamery.X;
                points2[2].Y = Wymiary.Y + Wymiary.Height + 5 - Kamera.Prostokat_Kamery.Y;
                points2[3].X = Wymiary.X + Wymiary.Width - Kamera.Prostokat_Kamery.X;
                points2[3].Y = Wymiary.Y + Wymiary.Height - Kamera.Prostokat_Kamery.Y;

                g.FillPolygon(new SolidBrush(Color.Black), points2); 
            }
        }
        /// <summary>
        /// Metoda zmiejszająca poziom energi obiektu.
        /// </summary>
        /// <param name="sila">Siła z jaką zadano uszkodzenie.</param>
        /// <returns>true jeśli obiekt został zniszczony</returns>
        public override bool Uszkodz(int sila)
        {
            this.energia = this.energia - sila;
            return this.energia <= 0;
        }
        /// <summary>
        /// Metoda wczytująca dane innej przeszkody.(W celu przebudowania bazy)
        /// </summary>
        /// <param name="przeszkoda">Obiekt przeszkody.</param>
        public void WczytajNoweDaneZWzorca(Przeszkoda przeszkoda)
        {
            this.energia = przeszkoda.energia;
            this.obrazy = przeszkoda.obrazy;
            this.typ = przeszkoda.typ;
        }
        /// <summary>
        /// Typy przeszkód.
        /// </summary>
        public enum TypPrzeszkody : int {
            /// <summary>
            /// Podłoża - drogi itp.
            /// </summary>
            PODLOZA = 0, 
            /// <summary>
            /// Woda - typ podłoża.
            /// </summary>
            WODA, 
            /// <summary>
            /// Wszystkie mury.
            /// </summary>
            MURY, 
            /// <summary>
            /// Typ drzew.
            /// </summary>
            DRZEWA, 
            /// <summary>
            /// Budynki
            /// </summary>
            BUDYNKI, 
            /// <summary>
            /// Baza
            /// </summary>
            BAZA}
        #region ICloneable Members
        /// <summary>
        /// Metoda klonująca przeszkodę.
        /// </summary>
        /// <returns>Nowy obiekt przeszkody.</returns>
        public object Clone()
        {
            Przeszkoda klon = new Przeszkoda(0, 0, this.Wymiary.Width, this.Wymiary.Height, this.energia, this.transparent, this.typ);
            klon.WczytajObrazy(this.obrazy);
            return klon;
        }

        #endregion
    }
}
