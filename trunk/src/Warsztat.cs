using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Klasa obsługująca statystyki gracza, wszelkie upgrade'y.
    /// </summary>
    public class Warsztat
    {
        /// <summary>
        /// Wartość poziomu szybkości gracza od 1-10.
        /// </summary>
        public int poziom_szybkosci;
        /// <summary>
        /// Wartość poziomu pancerza gracza od 1-10.
        /// </summary>
        public int poziom_pancerza;
        /// <summary>
        /// Wartość poziomu ataku gracza od 1-10.
        /// </summary>
        public int poziom_ataku;
        /// <summary>
        /// Wartość poziomu obrony bazy gracza od 1-10.
        /// </summary>
        public int poziom_muru;
        /// <summary>
        /// Wartość poziomu zasięgu wystrzelonych pocisków gracza od 1-10.
        /// </summary>
        public int poziom_zasiegu;
        /// <summary>
        /// Wartość poziomu ilości wystrzelonych pocisków gracza od 1-10.
        /// </summary>
        public int poziom_magazynku;

        //Przyciski panelu Warsztatu(Old)
        /*
        public Rectangle przyciskZamknijUlepszenia;
        public Rectangle przyciskUlepszSzybkosc;
        public Rectangle przyciskUlepszPancerz;
        public Rectangle przyciskUlepszAtak;
        public Rectangle przyciskUlepszMur;
        public Rectangle przyciskUlepszZasieg;
        //public Rectangle przyciskUlepszMagazynek;
        //public Rectangle przyciskDodajZycie;
        */
        //public String ZamknijWarsztatString = "Zamknij";

        /// <summary>
        /// Konstruktor domyślny klasy Warsztat ustawiająca wszystkie statystyki na pierwszy poziom
        /// </summary>
        public Warsztat()
        {
            poziom_szybkosci = 1;
            poziom_pancerza = 1;
            poziom_ataku = 1;
            poziom_muru = 1;
            poziom_zasiegu = 1;
            poziom_magazynku = 1;  
        }

        #region Metody Zwiększające Poziom
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy poziom statystyki jest mniejszy
        /// od 10, jeżeli tak to dodaje poziom i zmniejsza liczbę punktów do rozdania o 1.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszPoziomSzybkosci(Gracz gracz)
        {
            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (poziom_szybkosci >= 10)
                {
                    poziom_szybkosci = 10;
                }
                else
                {
                    poziom_szybkosci += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy poziom statystyki jest mniejszy
        /// od 10, jeżeli tak to dodaje poziom i zmniejsza liczbę punktów do rozdania o 1.
        /// Metoda zwiększa poziom pancerza, wytrzymałości czołga gracza.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszPoziomPancerza(Gracz gracz)
        {

            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (poziom_pancerza >= 10)
                {
                    poziom_pancerza = 10;
                }
                else
                {
                    poziom_pancerza += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy poziom statystyki jest mniejszy
        /// od 10, jeżeli tak to dodaje poziom i zmniejsza liczbę punktów do rozdania o 1.
        /// Metoda zwiększa poziom siły ataku gracza.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszPoziomAtaku(Gracz gracz)
        {

            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (poziom_ataku >= 10)
                {
                    poziom_ataku = 10;
                }
                else
                {
                    poziom_ataku += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy poziom statystyki jest mniejszy
        /// od 10, jeżeli tak to dodaje poziom i zmniejsza liczbę punktów do rozdania o 1.
        /// Metoda zwiększa poziom obrony bazy, każdy poziom jest to inny mur obronny wokół bazy.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszPoziomMuru(Gracz gracz)
        {
            
            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (poziom_muru >= 10)
                {
                    poziom_muru = 10;
                }
                else
                {
                    poziom_muru += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy poziom statystyki jest mniejszy
        /// od 10, jeżeli tak to dodaje poziom i zmniejsza liczbę punktów do rozdania o 1.
        /// Metoda zwiększa poziom zasięgu pocisków gracza.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszPoziomZasiegu(Gracz gracz)
        {
            
            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (poziom_zasiegu >= 10)
                {
                    poziom_zasiegu = 10;
                }
                else
                {
                    poziom_zasiegu += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy poziom statystyki jest mniejszy
        /// od 10, jeżeli tak to dodaje poziom i zmniejsza liczbę punktów do rozdania o 1.
        /// Metoda zwiększa poziom możliwości wystrzelenia większej ilości pocisków.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszPoziomMagazynku(Gracz gracz)
        {

            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (poziom_magazynku >= 10)
                {
                    poziom_magazynku = 10;
                }
                else
                {
                    poziom_magazynku += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        /// <summary>
        /// Metoda sprawdza czy gracz ma punkty do rozdania oraz czy ilość energi(żyć) gracza 
        /// nie przekracza 5, jeżeli tak to dodaje jedną energie(życie) i zmniejsza ilość punktów statystyk o 1.
        /// </summary>
        /// <param name="gracz">Referencja do gracza potrzebna, aby sprawdzać czy ma punkty do rozdania statystyk.</param>
        public void ZwiekszEnergie(Gracz gracz)
        {

            if (gracz.ilosc_punktow_ulepszen > 0)
            {
                if (gracz.energia >= 5)
                {
                    gracz.energia = 5;
                }
                else
                {
                    gracz.energia += 1;
                    gracz.ilosc_punktow_ulepszen -= 1;
                }
            }
        }
        #endregion Metody Zwiększające Poziom
        
        /// <summary>
        /// Metoda ustawiająca wszystkie wartości poziomów na pierwszy poziom.
        /// </summary>
        public void UstawDomyslneWartosci()
        {
            poziom_szybkosci = 1;
            poziom_pancerza = 1;
            poziom_ataku = 1;
            poziom_muru = 1;
            poziom_zasiegu = 1;
            poziom_magazynku = 1;
        }
        /// <summary>
        /// Ustawia poziomy statystyk gracza z profilu.
        /// </summary>
        /// <param name="profil">Referencja na wybrany profil gracza.</param>
        public void UstawWartosciZProfilu(ProfilGracza profil)
        {
            this.poziom_ataku = profil.ulepszenia.poziom_ataku;
            this.poziom_pancerza = profil.ulepszenia.poziom_wytrzymalosci;
            this.poziom_szybkosci = profil.ulepszenia.poziom_szybkosci;
            this.poziom_muru = profil.ulepszenia.poziom_muru;
            this.poziom_zasiegu = profil.ulepszenia.poziom_zasiegu;
            this.poziom_magazynku = profil.ulepszenia.poziom_magazynku;
        }
        /// <summary>
        /// Ustawia statystyki na bazie poziomów, w które gracz zainwestował.
        /// </summary>
        /// <param name="gracz">Referencja do gracza, aby można było ustawić statystyki.</param>
        public void UstawStatystyki(Gracz gracz)
        {
            gracz.Szybkosc = 5 + poziom_szybkosci;
            gracz.Wytrzymalosc_Bazowa = 10 + poziom_pancerza * 10;
            gracz.Wytrzymalosc = gracz.Wytrzymalosc_Bazowa;
            gracz.Sila = 10 + poziom_ataku * 10;
            gracz.Zasieg = 120 + poziom_zasiegu * 30;
            gracz.Max_Pociskow = poziom_magazynku + 1;
           
        }
        //Poprzednia wersja warsztatu, która opierała się na rysowaniu Statystyk na ekranie, oraz ich upgradów
        /*
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
            
            for(int szybkosc = 0; szybkosc < poziom_szybkosci; szybkosc++)
            {
                g.DrawImage(Multimedia.poziom_ulepszenia, new Rectangle(42 + szybkosc * 22, 21, 21, 18), 0, 0, Multimedia.poziom_ulepszenia.Width, Multimedia.poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            g.DrawString("Koszt: " + (500 + 1000 * poziom_szybkosci), new Font("Arial", 10, FontStyle.Bold), new SolidBrush(Color.White),
                new Rectangle(35, 20, 100, 20), drawFormat);
            
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

            #region Rysowanie Ulepszenia Zasięgu
            /*
            g.DrawImage(Multimedia.pasek_ulepszenia, new Rectangle(40, 160, 220, 20), 0, 0, Multimedia.pasek_ulepszenia.Width, Multimedia.pasek_ulepszenia.Height,
               GraphicsUnit.Pixel, transparentPink);

            przyciskUlepszZasieg = new Rectangle(0, 160, 35, 35);
            g.DrawImage(Multimedia.ImageMur, przyciskUlepszZasieg, 0, 0, Multimedia.ImageAtak.Width, Multimedia.ImageAtak.Height,
                GraphicsUnit.Pixel, transparentPink);
            g.DrawString("Koszt: " + (500 + 1000 * poziom_zasiegu), new Font("Arial", 10, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(35, 180, 100, 20), drawFormat);

            for (int zasieg = 0; zasieg < poziom_zasiegu; zasieg++)
            {
                g.DrawImage(Multimedia.poziom_ulepszenia, new Rectangle(42 + zasieg * 22, 161, 21, 18), 0, 0, Multimedia.poziom_ulepszenia.Width, Multimedia.poziom_ulepszenia.Height,
                GraphicsUnit.Pixel, transparentPink);
            }
            */
            /*
            #endregion Rysowanie Ulepszenia Zasięgu

            #region Rysowanie Przycisku Zamkniecia Ulepszeń

            przyciskZamknijUlepszenia = new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, Kamera.Prostokat_Kamery.Height - 30, 200, 30);
            g.DrawImage(Multimedia.przyciskImageZamknij, przyciskZamknijUlepszenia, 0, 0, Multimedia.przyciskImageZamknij.Width,
                Multimedia.przyciskImageZamknij.Height, GraphicsUnit.Pixel, transparentPink);
            g.DrawString(ZamknijWarsztatString, new Font("Arial", 12, FontStyle.Regular), new SolidBrush(Color.Yellow),
                new Rectangle(Kamera.Prostokat_Kamery.Width / 2 - 100, Kamera.Prostokat_Kamery.Height - 25, 200, 25), drawFormat);
            #endregion Rysowanie Przycisku Zamkniecia Ulepszeń
        }
        */
    }
}