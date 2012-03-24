using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Część klasy obiekty implementująca wykrywanie kolizji
    /// </summary>
    abstract partial class Obiekty
    {
        protected Rectangle CollisonDetectRect;
        public bool WykryjKolizje(Obiekty obj)
        {return false; }
    }
}
