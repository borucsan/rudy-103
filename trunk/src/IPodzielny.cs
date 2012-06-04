using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Rudy_103.src
{
    /// <summary>
    /// Interfejs definujący wymiary obiekty.
    /// </summary>
    public interface IPodzielny
    {
        /// <summary>
        /// Prostokąt wymiarów.
        /// </summary>
        Rectangle Wymiary { get; }
        /// <summary>
        /// Zmiena określa czy obiekt powoduje kolizje.
        /// </summary>
        bool Transparent { get; }
    }
}
