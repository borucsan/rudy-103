﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    class Warsztat
    {
        public int poziom_szybkosci;
        public int poziom_pancerza;
        public int poziom_ataku;
        public int poziom_muru;

        //Przyciski panelu Warsztatu
        public Rectangle przyciskZamknijUlepszenia;
        public Rectangle przyciskUlepszSzybkosc;
        public Rectangle przyciskUlepszPancerz;
        public Rectangle przyciskUlepszAtak;
        public Rectangle przyciskUlepszMur;

        public Warsztat()
        {
            poziom_szybkosci = 1;
            poziom_pancerza = 1;
            poziom_ataku = 1;
            poziom_muru = 1;        
        }

        #region Metody Zwiększające Poziom
        public void ZwiekszPoziomSzybkosci(Gracz gracz)
        {
            int cena = (500 + 1000 * poziom_szybkosci);
            if (gracz.pieniadze >= cena)
            {
                if (poziom_szybkosci >= 10)
                {
                    poziom_szybkosci = 10;
                }
                else
                {
                    poziom_szybkosci += 1;
                    gracz.pieniadze -= cena;
                }
            }
        }
        
        public void ZwiekszPoziomPancerza(Gracz gracz)
        {
            int cena = (500 + 1000 * poziom_pancerza);
            if (gracz.pieniadze >= cena)
            {
                if (poziom_pancerza >= 10)
                {
                    poziom_pancerza = 10;
                }
                else
                {
                    poziom_pancerza += 1;
                    gracz.pieniadze -= cena;
                }
            }
        }
        public void ZwiekszPoziomAtaku(Gracz gracz)
        {
            int cena = (500 + 1000 * poziom_ataku);
            if (gracz.pieniadze >= cena)
            {
                if (poziom_ataku >= 10)
                {
                    poziom_ataku = 10;
                }
                else
                {
                    poziom_ataku += 1;
                    gracz.pieniadze -= cena;
                }
            }
        }
        public void ZwiekszPoziomMuru(Gracz gracz)
        {
            int cena = (500 + 1000 * poziom_muru);
            if (gracz.pieniadze >= cena)
            {
                if (poziom_muru >= 10)
                {
                    poziom_muru = 10;
                }
                else
                {
                    poziom_muru += 1;
                    gracz.pieniadze -= cena;
                }
            }
        }
        #endregion Metody Zwiększające Poziom
        
        public void UstawDomyslneWartosci()
        {
            poziom_szybkosci = 1;
            poziom_pancerza = 1;
            poziom_ataku = 1;
            poziom_muru = 1;
        }
        
        public void UstawStatystyki(Gracz gracz)
        {
            gracz.Szybkosc = 5 + poziom_szybkosci;
            gracz.Wytrzymalosc_Bazowa = 10 + poziom_pancerza * 10;
            gracz.Wytrzymalosc = gracz.Wytrzymalosc_Bazowa;
            gracz.Sila = 10 + poziom_ataku * 10;
            gracz.Zasieg = 120 + poziom_ataku * 30;
            gracz.Max_Pociskow = poziom_ataku + 1;
           
        }

        public void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            #region Rysowanie Ulepszenia Szybkości
            
            g.DrawImage(Multimedia.pasek_ulepszenia, new Rectangle(40, 20, 220, 20), 0, 0, Multimedia.pasek_ulepszenia.Width, Multimedia.pasek_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszSzybkosc = new Rectangle(0, 20, 35, 35);
            g.DrawImage(Multimedia.ImageSzybkosc, przyciskUlepszSzybkosc, 0, 0, Multimedia.ImageSzybkosc.Width, Multimedia.ImageSzybkosc.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: "+(500 + 1000 * poziom_szybkosci), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(35, 40, 100, 20), drawFormat);

            for(int szybkosc = 0; szybkosc < poziom_szybkosci; szybkosc++)
            {
                g.DrawImage(Multimedia.poziom_ulepszenia, new Rectangle(42 + szybkosc * 22, 21, 21, 18), 0, 0, Multimedia.poziom_ulepszenia.Width, Multimedia.poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            
            
            #endregion Rysowanie Ulepszenia Szybkości

            #region Rysowanie Ulepszenia Pancerza

            g.DrawImage(Multimedia.pasek_ulepszenia, new Rectangle(40, 55, 220, 20), 0, 0, Multimedia.pasek_ulepszenia.Width, Multimedia.pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszPancerz = new Rectangle(0, 55, 35, 35);
            g.DrawImage(Multimedia.ImagePancerz, przyciskUlepszPancerz, 0, 0, Multimedia.ImagePancerz.Width, Multimedia.ImagePancerz.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: "+(500 + 1000 * poziom_pancerza), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(35, 75, 100, 20), drawFormat);

            for(int pancerz = 0; pancerz < poziom_pancerza; pancerz++)
            {
                g.DrawImage(Multimedia.poziom_ulepszenia, new Rectangle(42 + pancerz * 22, 56, 21, 18), 0, 0, Multimedia.poziom_ulepszenia.Width, Multimedia.poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            
            #endregion Rysowanie Ulepszenia Pancerza

            #region Rysowanie Ulepszenia Ataku
            g.DrawImage(Multimedia.pasek_ulepszenia, new Rectangle(40, 90, 220, 20), 0, 0, Multimedia.pasek_ulepszenia.Width, Multimedia.pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszAtak = new Rectangle(0, 90, 35, 35);
            g.DrawImage(Multimedia.ImageAtak, przyciskUlepszAtak, 0, 0, Multimedia.ImageAtak.Width, Multimedia.ImageAtak.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: "+(500 + 1000 * poziom_ataku), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(35, 110, 100, 20), drawFormat);

            for(int atak = 0; atak < poziom_ataku; atak++)
            {
                g.DrawImage(Multimedia.poziom_ulepszenia, new Rectangle(42 + atak * 22, 91, 21, 18), 0, 0, Multimedia.poziom_ulepszenia.Width, Multimedia.poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            
            #endregion Rysowanie Ulepszenia Ataku

            #region Rysowanie Ulepszenia Bazy
            
            g.DrawImage(Multimedia.pasek_ulepszenia, new Rectangle(40, 125, 220, 20), 0, 0, Multimedia.pasek_ulepszenia.Width, Multimedia.pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszMur = new Rectangle(0, 125, 35, 35);
            g.DrawImage(Multimedia.ImageMur, przyciskUlepszMur, 0, 0, Multimedia.ImageAtak.Width, Multimedia.ImageAtak.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: " + (500 + 1000 * poziom_muru), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(35, 145, 100, 20), drawFormat);

            for(int mur = 0; mur < poziom_muru; mur++)
            {
                g.DrawImage(Multimedia.poziom_ulepszenia, new Rectangle(42 + mur * 22, 126, 21, 18), 0, 0, Multimedia.poziom_ulepszenia.Width, Multimedia.poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            
            
            #endregion Rysowanie Ulepszenia Bazy

            #region Rysowanie Przycisku Zamkniecia Ulepszeń

            przyciskZamknijUlepszenia = new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, Kamera.Prostokat_Kamery.Height - 30, 200, 30);
            g.DrawImage(Multimedia.przyciskImageZamknij, przyciskZamknijUlepszenia, 0, 0, Multimedia.przyciskImageZamknij.Width,
                Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Zamknij", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, Kamera.Prostokat_Kamery.Height - 25, 200, 25), drawFormat);
            #endregion Rysowanie Przycisku Zamkniecia Ulepszeń
        }
    }
}