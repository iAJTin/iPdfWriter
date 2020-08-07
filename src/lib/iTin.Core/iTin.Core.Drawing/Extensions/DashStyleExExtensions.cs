
namespace iTin.Core.Drawing
{
    using System.Drawing.Drawing2D;

    using ComponentModel;

    /// <summary>
    /// Static class than contains extension methods for type <see cref="DashStyleEx"/>.
    /// </summary> 
    public static class DashStyleExExtensions
    {
        /// <summary>
        /// Equivalence between the types <see cref="DashStyleEx"/> and <see cref="DashStyle"/>.
        /// </summary>
        /// <param name="style">One of the values in the enumeration <see cref="DashStyleEx"/> that represents style of dashed lines.</param>
        /// <returns>
        /// Equivalent value.
        /// </returns>
        public static DashStyle ToDashStyle(this DashStyleEx style)
        {
            DashStyle dashstyle = DashStyle.Solid;

            switch (style)
            {
                case DashStyleEx.Dash:
                    dashstyle = DashStyle.Dash;
                    break;

                case DashStyleEx.DashDot:
                    dashstyle = DashStyle.DashDot;
                    break;

                case DashStyleEx.DashDotDot:
                    dashstyle = DashStyle.DashDotDot;
                    break;

                case DashStyleEx.Dot:
                    dashstyle = DashStyle.Dot;
                    break;
            }

            return dashstyle;
        }
    }
}
