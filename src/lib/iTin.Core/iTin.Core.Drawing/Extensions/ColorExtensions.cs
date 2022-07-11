
using System;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for structures of type <see cref="Color"/>.
    /// </summary> 
    internal static class ColorExtensions
    {
        /// <summary>
        /// Gets the hexadecimal encoding of a color.
        /// </summary>
        /// <param name="value">color to convert.</param>
        /// <returns>
        /// An hexadecimal <see cref="String"/> than represents the color.
        /// </returns>
        public static string ToHex(this Color value)
        {
            var result = new StringBuilder();
            result.Append("#");
            result.Append(value.R.ToString("x2", CultureInfo.InvariantCulture));
            result.Append(value.G.ToString("x2", CultureInfo.InvariantCulture));
            result.Append(value.B.ToString("x2", CultureInfo.InvariantCulture));

            return result.ToString();
        }
    }
}
