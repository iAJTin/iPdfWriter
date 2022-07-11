
using System;
using System.Drawing;
using System.Globalization;

namespace iTin.Core.Drawing.Helpers
{
    /// <summary> 
    /// Static class that contains methods for manipulating structures of type <see cref="T:System.Drawing.Color"/>.
    /// </summary>
    public static class ColorHelper
    {
        /// <summary>
        /// Returns a new <see cref="Color"/> structure from its name, its hexdecimal encoding or from color space definition.
        /// </summary>
        /// <param name="value">A <see cref="String"/> that represents the color to convert.</param>
        /// <returns>
        /// Creates a new <see cref="Color"/> structure from the specified string color.
        /// </returns>
        /// <remarks>
        /// The <paramref name="value"/> parameter may contain:
        /// <list type="table">
        ///   <item>
        ///     <description>A color name, such as <see cref="Color.Black"/>, <see cref="Color.Red"/>.</description>
        ///   </item>
        ///   <item>
        ///     <description>A hexadecimal RGB color encoding preceded by the character <b>#</b>, such as <b>#00FF00</b>, <b>#00FFFF</b>.</description>
        ///   </item>
        ///   <item>
        ///     <description>
        ///       A space color definition, for this has to precede the color string with this string <b>sc#</b>.
        ///       <para>for example <b>sc# 0.15 0.15 0.15</b>, where the three color components (RGB) are set to 15% of its maximum value, producing a very light gray. 
        ///       Each component has to be between <b>0.0</b> and <b>1.0</b>.</para>    
        ///     </description>
        ///   </item>
        ///  </list>
        /// </remarks>
        /// <example>
        /// The following code example, we obtain three different ways the same color, the color <see cref="Color.Black"/>.
        /// <code lang="cs">
        ///   using System;   
        ///   using System.Drawing;
        /// 
        ///   using iTin.Core.Helpers;
        /// 
        ///   class ColorTestClass   
        ///   {   
        ///       static int Main()   
        ///       {
        ///            // From color name.
        ///            Color fromColorName = ColorHelper.GetColorFromString("Black");
        ///
        ///            // From hexadecimal.
        ///            Color fromHexString = ColorHelper.GetColorFromString("#000000");
        /// 
        ///            // From space color.
        ///            Color fromSpace = ColorHelper.GetColorFromString("sc: 0.0 0.0 0.0");
        ///       }
        ///   }   
        ///  </code>
        /// </example>
        public static Color GetColorFromString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Color.Empty;
            }

            var isHexColor = value.Trim().StartsWith("#", StringComparison.Ordinal);
            if (isHexColor)
            {
                return ColorTranslator.FromHtml(value);
            }

            var isSpaceColor = value.Trim().StartsWith("sc#", StringComparison.Ordinal);
            if (isSpaceColor)
            {
                return FromSpaceColor(value);
            }

            return Color.FromName(value);
        }

        /// <summary>
        /// Combine two colors with relative percentages.
        /// </summary>
        /// <param name="color1">First color.</param>
        /// <param name="percent1">Percentage of the first color.</param>
        /// <param name="color2">Second color</param>
        /// <param name="percent2">Percentage of the second color.</param>
        /// <returns>
        /// Returns a new <see cref="Color"/> structure as result of the combination.
        /// </returns>
        public static Color MergeColors(Color color1, float percent1, Color color2, float percent2) => MergeColors(color1, percent1, color2, percent2, Color.Empty, 0f);

        /// <summary>
        /// Combinar tres colores con porcentajes relativos.
        /// </summary>
        /// <param name="color1">First color.</param>
        /// <param name="percent1">Percentage of the first color.</param>
        /// <param name="color2">Second color</param>
        /// <param name="percent2">Percentage of the second color.</param>
        /// <param name="color3">Third color.</param>
        /// <param name="percent3">Percentage of the third color.</param>
        /// <returns>
        /// Returns a new <see cref="Color"/> structure as result of the combination.
        /// </returns>
        public static Color MergeColors(Color color1, float percent1, Color color2, float percent2, Color color3, float percent3)
        {
            var red = (int)((color1.R * percent1) + (color2.R * percent2) + (color3.R * percent3));
            var green = (int)((color1.G * percent1) + (color2.G * percent2) + (color3.G * percent3));
            var blue = (int)((color1.B * percent1) + (color2.B * percent2) + (color3.B * percent3));

            if (red < 0)
            {
                red = 0;
            }

            if (red > 255)
            {
                red = 255;
            }

            if (green < 0)
            {
                green = 0;
            }

            if (green > 255)
            {
                green = 255;
            }

            if (blue < 0)
            {
                blue = 0;
            }

            if (blue > 255)
            {
                blue = 255;
            }

            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Converts specified color to html color string.
        /// </summary>
        /// <param name="color"><see cref="Color"/> structure to convert.</param>
        /// <returns>
        /// Returns a new <see cref="String"/> thats constains converted color.
        /// </returns>
        public static string ToHtmlColor(Color color) => $"#{color.R:X2}{color.G:X2}{color.B:X2}";

        /// <summary>
        /// Converts specified color to grayscale.
        /// </summary>
        /// <param name="color"><see cref="Color"/> structure to convert.</param>
        /// <returns>
        /// Returns a new <see cref="Color"/> structure converted to grayscale.
        /// </returns>
        public static Color ToGray(Color color)
        {
            int gray = (color.R + color.G + color.B) / 3;
            color = Color.FromArgb(gray, gray, gray);
            return color;
        }

        /// <summary>
        /// Returns the hexadecimal encoding of a color.
        /// </summary>
        /// <param name="value"><see cref="Color"/> structure to convert.</param>
        /// <returns>
        /// A hexadecimal <see cref="String" /> representing color.
        /// </returns>
        /// <example>
        /// The following code example, we obtain hexadecimal code for white color without chararacter <b>#</b>. The result is <b>FFFFFF</b>.
        /// <code lang="cs">
        ///   using System;   
        ///   using System.Drawing;
        /// 
        ///   using iTin.Core.Drawing.Helpers;
        /// 
        ///   class ColorTestClass   
        ///   {   
        ///       static int Main()   
        ///       {
        ///            // From color name.
        ///            string hexColorString = ColorHelper.GetColorFromString(Color.White);
        ///            
        ///            // Print result.
        ///            Console.WriteLine("The hexadecimal representation of the color white is {0}", hexColorString); 
        ///       }
        ///   }   
        ///  </code>
        /// </example>
        public static string ToHex(Color value) => value.ToHex();

        /// <summary>
        /// Sets transparency level of specified color.
        /// </summary>
        /// <param name="color"><see cref="Color"/> structure to convert.</param>
        /// <param name="opacity">The level of transparency applied. The value must be between 0 and 255.</param>
        /// <returns>
        /// Returns a new <see cref="Color"/> structure with specified transparency level.
        /// </returns>
        /// <remarks>
        /// If the value is outside the limits, not makes no changes.
        /// </remarks>
        public static Color ToTransparencyLevel(Color color, int opacity)
        {
            if (opacity < 0 || opacity > 255)
            {
                return color;
            }

            color = Color.FromArgb(opacity, color);
            return color;
        }


        /// <summary>
        /// A color obtained from the color percentage of each of its components.
        /// </summary>
        /// <param name="value">A <see cref="String"/> that represents the color to convert.</param>
        /// <returns>
        /// Creates a <see cref="Color"/> structure from the specified string color.
        /// </returns>
        private static Color FromSpaceColor(string value)
        {
            string[] valueArray = null;

            if (value.StartsWith("sc#", StringComparison.Ordinal))
            {
                value = value.Substring(4);
                valueArray = value.Split(' ');
            }

            if (valueArray == null)
            {
                throw new InvalidOperationException("Invalid color"); //ComponentModel.Exception.GeneralExceptionResourceManager.GetString("INVALID_COLOR"));
            }

            var r = 0xff * (1 - float.Parse(valueArray[0], CultureInfo.InvariantCulture));
            var g = 0xff * (1 - float.Parse(valueArray[1], CultureInfo.InvariantCulture));
            var b = 0xff * (1 - float.Parse(valueArray[2], CultureInfo.InvariantCulture));

            return Color.FromArgb((int)r, (int)g, (int)b);
        }
    }
}
