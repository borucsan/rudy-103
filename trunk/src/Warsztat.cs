using System;
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

        //Obrazy panelu Warsztatu
        private Image pasek_ulepszenia;
        private Image poziom_ulepszenia;
        private Image przyciskImageSzybkosc;
        private Image przyciskImagePancerz;
        private Image przyciskImageAtak;
        private Image przyciskImageMur;
        private Image przyciskImageZamknij;

        //Przyciski panelu Warsztatu
        public Rectangle przyciskZamknijUlepszenia;
        public Rectangle przyciskUlepszSzybkosc;
        public Rectangle przyciskUlepszPancerz;
        public Rectangle przyciskUlepszAtak;
        public Rectangle przyciskUlepszMur;

        public Warsztat(System.Reflection.Assembly execAssem)
        {
            poziom_szybkosci = 1;
            poziom_pancerza = 1;
            poziom_ataku = 1;
            poziom_muru = 1;
        
            przyciskImageZamknij = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.przycisk.png"));
            pasek_ulepszenia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.pasek_ulepszenia.png"));
            poziom_ulepszenia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.poziom_ulepszenia.png"));
            przyciskImageSzybkosc = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_szybkosc.png"));
            przyciskImagePancerz = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_pancerz.png"));
            przyciskImageAtak = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_atak.png"));
            przyciskImageMur = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_mur.png"));
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
            gracz.Wytrzymalosc = 10 + poziom_pancerza * 10;
            gracz.aktualna_wytrzymalosc = gracz.Wytrzymalosc;
            gracz.Sila = 10 + poziom_ataku * 10;
        }

        public void Rysuj(Graphics g, System.Drawing.Imaging.ImageAttributes transparentPink)
        {
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;

            #region Rysowanie Ulepszenia Szybkości
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 50, 230, 50));
            g.DrawImage(pasek_ulepszenia, new Rectangle(10, 79, 220, 20), 0, 0, pasek_ulepszenia.Width, pasek_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszSzybkosc = new Rectangle(10, 43, 35, 35);
            g.DrawImage(przyciskImageSzybkosc, przyciskUlepszSzybkosc, 0, 0, przyciskImageSzybkosc.Width, przyciskImageSzybkosc.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: "+(500 + 1000 * poziom_szybkosci), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(110, 54, 110, 20), drawFormat);

            for(int szybkosc = 0; szybkosc < poziom_szybkosci; szybkosc++)
            {
                g.DrawImage(poziom_ulepszenia, new Rectangle(12+szybkosc*22, 80, 21, 18), 0, 0, poziom_ulepszenia.Width, poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            
            
            #endregion Rysowanie Ulepszenia Szybkości

            #region Rysowanie Ulepszenia Pancerza
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 110, 230, 50));

            g.DrawImage(pasek_ulepszenia, new Rectangle(10, 139, 220, 20), 0, 0, pasek_ulepszenia.Width, pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszPancerz = new Rectangle(10, 103, 35, 35);
            g.DrawImage(przyciskImagePancerz, przyciskUlepszPancerz, 0, 0, przyciskImagePancerz.Width, przyciskImagePancerz.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: "+(500 + 1000 * poziom_pancerza), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(110, 114, 110, 20), drawFormat);

            for(int pancerz = 0; pancerz < poziom_pancerza; pancerz++)
            {
                g.DrawImage(poziom_ulepszenia, new Rectangle(12 + pancerz*22, 140, 21, 18), 0, 0, poziom_ulepszenia.Width, poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            
            #endregion Rysowanie Ulepszenia Pancerza

            #region Rysowanie Ulepszenia Ataku
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 170, 230, 50));

            g.DrawImage(pasek_ulepszenia, new Rectangle(10, 170 + 29, 220, 20), 0, 0, pasek_ulepszenia.Width, pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszAtak = new Rectangle(10, 170 - 7, 35, 35);
            g.DrawImage(przyciskImageAtak, przyciskUlepszAtak, 0, 0, przyciskImageAtak.Width, przyciskImageAtak.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: "+(500 + 1000 * poziom_ataku), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(110, 170 + 4, 110, 20), drawFormat);

            for(int atak = 0; atak < poziom_ataku; atak++)
            {
                g.DrawImage(poziom_ulepszenia, new Rectangle(12 + atak*22, 170 + 30, 21, 18), 0, 0, poziom_ulepszenia.Width, poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            

            #endregion Rysowanie Ulepszenia Ataku

            #region Rysowanie Ulepszenia Bazy
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(5, 230, 230, 50));

            g.DrawImage(pasek_ulepszenia, new Rectangle(10, 230 + 29, 220, 20), 0, 0, pasek_ulepszenia.Width, pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszMur = new Rectangle(10, 230 - 7, 35, 35);
            g.DrawImage(przyciskImageMur, przyciskUlepszMur, 0, 0, przyciskImageAtak.Width, przyciskImageAtak.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: " + (500 + 1000 * poziom_muru), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(110, 230 + 4, 110, 20), drawFormat);

            for(int mur = 0; mur < poziom_muru; mur++)
            {
                g.DrawImage(poziom_ulepszenia, new Rectangle(12 + mur*22, 230 + 30, 21, 18), 0, 0, poziom_ulepszenia.Width, poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            

            #endregion Rysowanie Ulepszenia Bazy

            #region Rysowanie Przycisku Zamkniecia Ulepszeń

            przyciskZamknijUlepszenia = new Rectangle(20, 285, 200, 30);
            g.DrawImage(przyciskImageZamknij, przyciskZamknijUlepszenia, 0, 0, przyciskImageZamknij.Width,
                przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Zamknij", new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(20, 290, 200, 25), drawFormat);
            #endregion Rysowanie Przycisku Zamkniecia Ulepszeń
        }
    }
}
