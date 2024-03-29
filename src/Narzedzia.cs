﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    static class Narzedzia
    {
      
        public static Random rand = new Random();
        public static System.Drawing.Imaging.ImageAttributes transparentPink;

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
        public static PodzialProstokata DzielProsokat(Rectangle Prosokat, out Rectangle SubProstokat1, out Rectangle SubProstokat2, int pozycja_podzialu)
        {
            SubProstokat1 = new Rectangle();
            SubProstokat2 = new Rectangle();
            if (Prosokat.Width == Prosokat.Height)
            {
                SubProstokat1 = new Rectangle(Prosokat.X, Prosokat.Y, pozycja_podzialu, Prosokat.Height);
                SubProstokat2 = new Rectangle(Prosokat.X + pozycja_podzialu, Prosokat.Y, Prosokat.Width - pozycja_podzialu, Prosokat.Height);
                return PodzialProstokata.X;
            }
            else
            {
                if (Prosokat.Height > Prosokat.Width)
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

        public static List<T> Tasuj<T>(List<T> lista)
        {
            List<int> pozycje = new List<int>(lista.Count);
            List<T> return_list = new List<T>(lista.Count);
            for (int i = 0; i < lista.Count; ++i)
            {
                pozycje.Add(i);
            }
            while (pozycje.Count != 0)
            {
                int los = Narzedzia.rand.Next(0, pozycje.Count);
                return_list.Add(lista[los]);
                pozycje.RemoveAt(los);
            }
            return return_list;
        }
        public static T[] Tasuj<T>(T[] array)
        {
            List<int> pozycje = new List<int>(array.Length);
            List<T> return_list = new List<T>(array.Length);
            for (int i = 0; i < array.Length; ++i)
            {
                pozycje.Add(i);
            }
            while (pozycje.Count != 0)
            {
                int los = Narzedzia.rand.Next(0, pozycje.Count);
                return_list.Add(array[los]);
                pozycje.RemoveAt(los);
            }
            return return_list.ToArray<T>();
        }
        public static string WykryjKarteSD()
        {
            string firstCard = "";

            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo("\\");
            System.IO.FileSystemInfo[] fsi = di.GetFileSystemInfos();

            for (int x = 0; x < fsi.Length; x++)
            {
                if ((fsi[x].Attributes & System.IO.FileAttributes.Temporary) == System.IO.FileAttributes.Temporary)
                {
                    firstCard = fsi[x].FullName;
                }
            }

            return firstCard;
        }
        

        public static int PointToPixelVertical(int width)
        {
            /*
             * Konwertuje szerokość, warość X 
             * na jednostke wyglądającą tak samo 
             * na każdym urządzeniu, teoretycznie
            */
            int mnoznik_szerokosci = Kamera.Prostokat_Kamery.Width / 320;
            return width * mnoznik_szerokosci;
        }
        public static int PointToPixelHorizontal(int height)
        {
            /*
             * Konwertuje wysokość, warość Y 
             * na jednostke wyglądającą tak samo 
             * na każdym urządzeniu, teoretycznie
            */
            int mnoznik_wysokosci = Kamera.Prostokat_Kamery.Height / 240;
            return height * mnoznik_wysokosci;
        }

    }
}
