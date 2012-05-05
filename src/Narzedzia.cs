using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    static class Narzedzia
    {
        public static Random rand = new Random();

        public static PodzialProstokata DzielProsokat(Rectangle Prosokat, out Rectangle SubProstokat1, out Rectangle SubProstokat2)
        {
            SubProstokat1 = new Rectangle();
            SubProstokat2 = new Rectangle();
            if (Prosokat.Width == Prosokat.Height)
            {
                DzielProsokat(Prosokat, out SubProstokat1, out SubProstokat2, PodzialProstokata.X);
                return PodzialProstokata.X;
            }
            else
            {
                if (Prosokat.Height > Prosokat.Width)
                {
                    DzielProsokat(Prosokat, out SubProstokat1, out SubProstokat2, PodzialProstokata.Y);
                    return PodzialProstokata.Y;
                }
                else
                {
                    DzielProsokat(Prosokat, out SubProstokat1, out SubProstokat2, PodzialProstokata.X);
                    return PodzialProstokata.X;
                }
            }
        }
        public static PodzialProstokata DzielProsokat(Rectangle Prostokat, out Rectangle SubProstokat1, out Rectangle SubProstokat2, PodzialProstokata pp)
        {
            SubProstokat1 = new Rectangle();
            SubProstokat2 = new Rectangle();
            if (pp == PodzialProstokata.Y)
            {
                if ((Prostokat.Height % 2) == 0)
                {
                    SubProstokat1 = new Rectangle(Prostokat.X, Prostokat.Y, Prostokat.Width, Prostokat.Height / 2);
                    SubProstokat2 = new Rectangle(Prostokat.X, Prostokat.Y + Prostokat.Height / 2, Prostokat.Width, Prostokat.Height / 2);
                }
                else
                {
                    SubProstokat1 = new Rectangle(Prostokat.X, Prostokat.Y, Prostokat.Width, Prostokat.Height / 2);
                    SubProstokat2 = new Rectangle(Prostokat.X, Prostokat.Y + Prostokat.Height / 2, Prostokat.Width, Prostokat.Height / 2 + 1);
                }
                return PodzialProstokata.Y;
            }
            else
            {
                if ((Prostokat.Width % 2) == 0)
                {
                    SubProstokat1 = new Rectangle(Prostokat.X, Prostokat.Y, Prostokat.Width / 2, Prostokat.Height);
                    SubProstokat2 = new Rectangle(Prostokat.X + Prostokat.Width / 2, Prostokat.Y, Prostokat.Width / 2, Prostokat.Height);
                }
                else
                {
                    SubProstokat1 = new Rectangle(Prostokat.X, Prostokat.Y, Prostokat.Width / 2, Prostokat.Height);
                    SubProstokat2 = new Rectangle(Prostokat.X + Prostokat.Width / 2, Prostokat.Y, Prostokat.Width / 2 + 1, Prostokat.Height);
                }
                return PodzialProstokata.X;
            }
        }
        public static PodzialProstokata DzielProsokat(Rectangle Prosokat, out Rectangle SubProstokat1, out Rectangle SubProstokat2, PodzialProstokata pp, int pozycja_podzialu)
        {
            SubProstokat1 = new Rectangle();
            SubProstokat2 = new Rectangle();
            if (pp == PodzialProstokata.Y)
            {
                SubProstokat1 = new Rectangle(Prosokat.X, Prosokat.Y, Prosokat.Width, pozycja_podzialu);
                SubProstokat2 = new Rectangle(Prosokat.X, Prosokat.Y + pozycja_podzialu, Prosokat.Width, Prosokat.Height - pozycja_podzialu);
                return PodzialProstokata.Y;
            }
            else
            {
                SubProstokat1 = new Rectangle(Prosokat.X, Prosokat.Y, pozycja_podzialu, Prosokat.Height);
                SubProstokat2 = new Rectangle(Prosokat.X + pozycja_podzialu, Prosokat.Y, Prosokat.Width - pozycja_podzialu, Prosokat.Height);
                return PodzialProstokata.X;
            }
        }
        
        public enum PodzialProstokata : int {X, Y};

        public static System.Drawing.Imaging.ImageAttributes transparentPink;
    }
}
