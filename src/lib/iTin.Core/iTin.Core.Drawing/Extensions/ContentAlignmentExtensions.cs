
using System.Drawing;

using iTin.Core.Drawing.ComponentModel;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for structures of type <see cref="ContentAlignment"/>.
    /// </summary> 
    public static class ContentAlignmentExtensions
    {
        #region [public] {static} (ImageStyle) ToImageStyle(this ContentAlignment): Equivalence between the types ContentAlignment and ImageStyle
        /// <summary>
        /// Equivalence between the types <see cref="ContentAlignment" /> and <see cref="ImageStyle"/>.
        /// </summary>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <returns>
        /// Equivalent style.
        /// </returns>
        public static ImageStyle ToImageStyle(this ContentAlignment alignment)
        {
            ImageStyle style = ImageStyle.CenterMiddle;

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    style = ImageStyle.TopLeft;
                    break;

                case ContentAlignment.TopCenter:
                    style = ImageStyle.TopMiddle;
                    break;

                case ContentAlignment.TopRight:
                    style = ImageStyle.TopRight;
                    break;

                case ContentAlignment.MiddleLeft:
                    style = ImageStyle.CenterLeft;
                    break;

                case ContentAlignment.MiddleCenter:
                    style = ImageStyle.CenterMiddle;
                    break;

                case ContentAlignment.MiddleRight:
                    style = ImageStyle.CenterRight;
                    break;

                case ContentAlignment.BottomLeft:
                    style = ImageStyle.BottomLeft;
                    break;

                case ContentAlignment.BottomCenter:
                    style = ImageStyle.BottomMiddle;
                    break;

                case ContentAlignment.BottomRight:
                    style = ImageStyle.BottomRight;
                    break;
            }

            return style;
        }
        #endregion

        #region [public] {static} (StringAlignment) ToHorizontalAlignment(this ContentAlignment): Equivalence of the horizontal component of the type ContentAlignment and StringAlignment
        /// <summary>
        /// Equivalence of the horizontal component of the type <see cref="ContentAlignment"/> and <see cref="StringAlignment"/>.
        /// </summary>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <returns>
        /// Equivalent style of the horizontal component.
        /// </returns>
        public static StringAlignment ToHorizontalAlignment(this ContentAlignment alignment)
        {
            StringAlignment horizontalAlignment = StringAlignment.Near;
            switch (alignment)
            {
                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    horizontalAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    horizontalAlignment = StringAlignment.Far;
                    break;
            }

            return horizontalAlignment;
        }
        #endregion

        #region [public] {static} (StringAlignment) ToVerticalAlignment(this ContentAlignment): Equivalence of the vertical component of the type ContentAlignment and StringAlignment
        /// <summary>
        /// Equivalence of the vertical component of the type <see cref="ContentAlignment"/> and <see cref="StringAlignment"/>.
        /// </summary>
        /// <param name="alignment">One of the values in the enumeration <see cref="ContentAlignment"/> that represents the type of alignment.</param>
        /// <returns>
        /// Equivalent style of the vertical component.
        /// </returns>
        public static StringAlignment ToStringVerticalAlignment(this ContentAlignment alignment)
        {
            StringAlignment verticalAlignment = StringAlignment.Near;
            switch (alignment)
            {
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    verticalAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    verticalAlignment = StringAlignment.Far;
                    break;
            }

            return verticalAlignment;
        }
        #endregion
    }
}
