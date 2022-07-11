
using System.Drawing.Drawing2D;

using iTin.Core.Drawing.ComponentModel;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for type <see cref="SmoothingModeEx"/>.
    /// </summary> 
    public static class SmoothingModeExExtensions
    {
        /// <summary>
        /// Equivalence between the types <see cref="SmoothingModeEx" /> and <see cref="SmoothingMode"/>.
        /// </summary>
        /// <param name="mode">One of the values in the enumeration <see cref="SmoothingModeEx"/> that represents the smoothing quality of the lines.</param>
        /// <returns>
        /// Equivalent representation quality.
        /// </returns>
        public static SmoothingMode ToSmoothingMode(this SmoothingModeEx mode)
        {
            SmoothingMode smmode = SmoothingMode.AntiAlias;
            if (mode == SmoothingModeEx.HighSpeed)
            {
                smmode = SmoothingMode.Default;
            }

            return smmode;
        }
    }
}
