using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    
    static class Multimedia
    {
        #region Grafika
        public static Image [] wczytywanieImage;
        public static Image [] interfejs_bateria;
        public static Image interfejs_informacje;
        public static Image interfejs_pole_radaru;
        public static Image tank_bateria;
        //public static Image[] interfejs_radar; //Nowy radar
        public static Image pusta_mapa;
        public static Image przeszkoda_mapa;
        public static Image gracz_mapa;
        public static Image tlo_mapa;

        public static Image przyciskImageZamknij;

        public static Image[] interfejs_buttonUp;
        public static Image[] interfejs_buttonRight;
        public static Image[] interfejs_buttonDown;
        public static Image[] interfejs_buttonLeft;
        public static Image[] interfejs_buttonEnter;

        public static Image ImageAtak;
        public static Image ImageSzybkosc;
        public static Image ImagePancerz;
        public static Image ImageMur;

        public static Image pasek_ulepszenia;
        public static Image poziom_ulepszenia;

        public static Image[] tlo;

        public static Image[] domyslny_pocisk;

        public static Image[] baza;

        public static Image[] domyslny_gracz;
        public static Image[] polska_gracz;

        public static Image[] przeciwnik_1;
        public static Image[] przeciwnik_2;
        public static Image[] przeciwnik_3;
        public static Image[] przeciwnik_4;
        public static Image[] przeciwnik_5;
        public static Image[] przeciwnik_6;
        public static Image[] przeciwnik_7;
        public static Image[] przeciwnik_8;
        public static Image[] przeciwnik_9;
        public static Image[] przeciwnik_10;

        public static Image[] ogien;
        public static Image[] eksplozja;

        public static Image[] intro_images;

        public static Image budynekA;
        public static Image budynekB;
        public static Image budynekC;
        public static Image chata;
        public static Image garaz;

        public static Image DrogaA1;
        public static Image DrogaA2;
        public static Image Trawa;
        public static Image Ziemia;
        public static Image Piasek;
        public static Image Woda;

        public static Image skrzynia;
        public static Image murA;
        public static Image murB;
        public static Image murC;
        public static Image murD;
        public static Image murE;
        public static Image drzewo;
        #endregion Grafika

        #region Dźwięki
        public static Dzwiek audio_wybuch;
        public static Dzwiek audio_wystrzal;
        public static Dzwiek audio_zginales;
        public static Dzwiek audio_game_over;
        #endregion Dźwięki
        public static void WczytajMultimedia(System.Reflection.Assembly execAssem)
        {
            #region Wczytywanie Grafiki
            wczytywanieImage = new Image[4];
            wczytywanieImage[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_1.png"));
            wczytywanieImage[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_2.png"));
            wczytywanieImage[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_3.png"));
            wczytywanieImage[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Wczytywanie.load_4.png"));

            #region Interfejs
            interfejs_bateria = new Image[7];
            interfejs_bateria[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_100.png"));
            interfejs_bateria[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_85.png"));
            interfejs_bateria[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_70.png"));
            interfejs_bateria[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_55.png"));
            interfejs_bateria[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_40.png"));
            interfejs_bateria[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_25.png"));
            interfejs_bateria[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.bateria_10.png"));

            interfejs_informacje = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.menu.png"));

            interfejs_pole_radaru = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.pole_radaru.png"));

            tank_bateria = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.tank_bateria.png"));

            pusta_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.pusta_mapa.png"));
            przeszkoda_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.przeszkoda.png"));
            gracz_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.gracz.png"));
            tlo_mapa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Mapa.tlo_mapa.png"));

            przyciskImageZamknij = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Interfejs.przycisk.png"));

            interfejs_buttonUp = new Image[2];
            interfejs_buttonRight = new Image[2];
            interfejs_buttonDown = new Image[2];
            interfejs_buttonLeft = new Image[2];
            interfejs_buttonEnter = new Image[2];
            interfejs_buttonUp[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_up.png"));
            interfejs_buttonUp[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_up_on.png"));
            interfejs_buttonRight[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_right.png"));
            interfejs_buttonRight[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_right_on.png"));
            interfejs_buttonDown[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_down.png"));
            interfejs_buttonDown[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_down_on.png"));
            interfejs_buttonLeft[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_left.png"));
            interfejs_buttonLeft[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_left_on.png"));
            interfejs_buttonEnter[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_enter.png"));
            interfejs_buttonEnter[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przyciski.button_enter_on.png"));
            #endregion Interfejs

            #region Warsztat
            ImageSzybkosc = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_szybkosc.png"));
            ImagePancerz = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_pancerz.png"));
            ImageAtak = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_atak.png"));
            ImageMur = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.ulepsz_mur.png"));

            pasek_ulepszenia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.pasek_ulepszenia.png"));
            poziom_ulepszenia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Warsztat.poziom_ulepszenia.png"));
            #endregion Warsztat

            #region Tło Planszy
            tlo = new Image[10];
            tlo[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_1.png"));
            tlo[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_2.png"));
            tlo[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_3.png"));
            tlo[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_4.png"));
            tlo[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_5.png"));
            tlo[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_6.png"));
            tlo[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_7.png"));
            tlo[7] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_8.png"));
            tlo[8] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_9.png"));
            tlo[9] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Ziemia.tlo_10.png"));
            #endregion Tło Planszy

            domyslny_pocisk = new Image[4];
            domyslny_pocisk[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_up.png"));
            domyslny_pocisk[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_right.png"));
            domyslny_pocisk[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_down.png"));
            domyslny_pocisk[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Pociski.Domyslny.pocisk2_left.png"));

            baza = new Image[2];
            baza[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Baza.baza_nowa.png"));
            baza[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Baza.baza_zniszczona.png"));
           
            #region Czolgi
            domyslny_gracz = new Image[4];
            domyslny_gracz[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_up.png"));
            domyslny_gracz[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_right.png"));
            domyslny_gracz[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_down.png"));
            domyslny_gracz[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Domyslny.tank_default_left.png"));

            polska_gracz = new Image[4];
            polska_gracz[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Polska.tank_poland_up.png"));
            polska_gracz[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Polska.tank_poland_right.png"));
            polska_gracz[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Polska.tank_poland_down.png"));
            polska_gracz[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Gracz.Polska.tank_poland_left.png"));

            przeciwnik_1 = new Image[4];
            przeciwnik_1[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_up.png"));
            przeciwnik_1[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_right.png"));
            przeciwnik_1[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_down.png"));
            przeciwnik_1[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_1.enemy_tank_1_left.png"));

            przeciwnik_2 = new Image[4];
            przeciwnik_2[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_up.png"));
            przeciwnik_2[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_right.png"));
            przeciwnik_2[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_down.png"));
            przeciwnik_2[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_2.enemy_tank_2_left.png"));

            przeciwnik_3 = new Image[4];
            przeciwnik_3[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_up.png"));
            przeciwnik_3[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_right.png"));
            przeciwnik_3[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_down.png"));
            przeciwnik_3[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_3.enemy_tank_3_left.png"));

            przeciwnik_4 = new Image[4];
            przeciwnik_4[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_up.png"));
            przeciwnik_4[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_right.png"));
            przeciwnik_4[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_down.png"));
            przeciwnik_4[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_4.enemy_tank_4_left.png"));

            przeciwnik_5 = new Image[4];
            przeciwnik_5[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_up.png"));
            przeciwnik_5[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_right.png"));
            przeciwnik_5[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_down.png"));
            przeciwnik_5[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_5.enemy_tank_5_left.png"));

            przeciwnik_6 = new Image[4];
            przeciwnik_6[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_up.png"));
            przeciwnik_6[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_right.png"));
            przeciwnik_6[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_down.png"));
            przeciwnik_6[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_6.enemy_tank_6_left.png"));

            przeciwnik_7 = new Image[4];
            przeciwnik_7[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_up.png"));
            przeciwnik_7[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_right.png"));
            przeciwnik_7[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_down.png"));
            przeciwnik_7[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_7.enemy_tank_7_left.png"));

            przeciwnik_8 = new Image[4];
            przeciwnik_8[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_up.png"));
            przeciwnik_8[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_right.png"));
            przeciwnik_8[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_down.png"));
            przeciwnik_8[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_8.enemy_tank_8_left.png"));

            przeciwnik_9 = new Image[4];
            przeciwnik_9[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_up.png"));
            przeciwnik_9[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_right.png"));
            przeciwnik_9[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_down.png"));
            przeciwnik_9[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_9.enemy_tank_9_left.png"));

            przeciwnik_10 = new Image[4];
            przeciwnik_10[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_up.png"));
            przeciwnik_10[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_right.png"));
            przeciwnik_10[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_down.png"));
            przeciwnik_10[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeciwnicy.Poziom_10.enemy_tank_10_left.png"));
#endregion Czolgi

            #region Efekty
            ogien = new Image[5];
            ogien[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_1.png"));
            ogien[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_2.png"));
            ogien[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_3.png"));
            ogien[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_4.png"));
            ogien[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Ogien.flame_5.png"));

            eksplozja = new Image[5];
            eksplozja[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_1.png"));
            eksplozja[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_2.png"));
            eksplozja[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_3.png"));
            eksplozja[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_4.png"));
            eksplozja[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Efekty.Eksplozja.wybuch_5.png"));

            intro_images = new Image[12];
            intro_images[0] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_1.png"));
            intro_images[1] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_2.png"));
            intro_images[2] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_3.png"));
            intro_images[3] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_4.png"));
            intro_images[4] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_5.png"));
            intro_images[5] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_6.png"));
            intro_images[6] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_7.png"));
            intro_images[7] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_8.png"));
            intro_images[8] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_9.png"));
            intro_images[9] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_10.png"));
            intro_images[10] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_11.png"));
            intro_images[11] = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Intro.intro_12.png"));

            #endregion Efekty

            #region Budynki
            budynekA = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.budynekA1.png"));
            budynekB = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.budynekB1.png"));
            budynekC = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.budynekC1.png"));
            chata = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.chata.png"));
            garaz = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Budynki.garage.png"));

            #endregion Budynki

            #region Podłoża
            DrogaA1 = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Podloze.Droga_NS.png"));
            DrogaA2 = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Podloze.Droga_EW.png"));
            Trawa = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Podloze.Grass.png"));
            Ziemia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Podloze.Ground.png"));
            Piasek = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Podloze.Sand.png"));
            Woda = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Podloze.Water.png"));
            #endregion Podłoża

            #region Przeszkody
            skrzynia = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.skrzynka.png"));
            murA = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka.png"));
            murB = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka2.png"));
            murC = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka3.png"));
            murD = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.cegielka4.png"));
            murE = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.mur.png"));
            drzewo = new System.Drawing.Bitmap(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Przeszkody.drzewo.png"));
            #endregion Przeszkody

            #endregion Wczytywanie Grafiki

            #region Wczytywanie Dźwięków
            audio_wybuch = new Dzwiek(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Dzwieki.wybuch.wav"));
            audio_wystrzal = new Dzwiek(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Dzwieki.wystrzal.wav"));
            audio_zginales = new Dzwiek(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Dzwieki.killed.wav"));
            audio_game_over = new Dzwiek(execAssem.GetManifestResourceStream(@"Rudy_103.Resources.Dzwieki.end.wav"));
            #endregion Wczytywanie Dźwięków

        }
    }

    
}
