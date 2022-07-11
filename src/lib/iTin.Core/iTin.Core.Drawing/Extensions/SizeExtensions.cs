
using System.Drawing;

using iTin.Core.Drawing.ComponentModel;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="Size"/>.
    /// </summary> 
    public static class SizeExtensions
    {
        /// <summary>
        /// Align the <see cref="Size"/> structure inside the target rectangle.
        /// </summary>
        /// <param name="size">Structure <see cref="Size"/> to be aligned.</param>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <param name="destRect">Structure <see cref="Rectangle"/> where it will be aligned.</param>
        /// <returns>
        /// Structure <see cref="Rectangle"/> that represents the destination rectangle that contains the aligned element.
        /// </returns>
        public static Rectangle AlignInside(this Size size, ContentAlignment alignment, Rectangle destRect)
        {
            Rectangle rect = new Rectangle(destRect.Location, size);
            return rect.AlignInside(alignment, destRect);
        }

        /// <summary>
        /// Align the <see cref="Size"/> structure outside the target rectangle.
        /// </summary>
        /// <param name="size">Estructura <see cref="Size"/> que se va alinear.</param>
        /// <param name="alignment">One of the values in the enumeration <see cref="OutsideAlignment"/> that represents the type of alignment.</param>
        /// <param name="destRect">Structure <see cref="Rectangle"/> where it will be aligned.</param>
        /// <returns>
        /// Structure <see cref="Rectangle"/> that represents the destination rectangle that contains the aligned element.
        /// </returns>
        public static Rectangle AlignOutside(this Size size, OutsideAlignment alignment, Rectangle destRect)
        {
            Rectangle rect = new Rectangle(destRect.Location, size);
            return rect.AlignOutside(alignment, destRect);
        }
    }
}
