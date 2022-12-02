
using System;
using System.Drawing;

using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Helpers;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="Graphics"/>.
    /// </summary> 
    public static class GraphicsExtensions
    {
        /// <summary>
        /// Returns a brush to draw from a reference image specifying the orientation, quality, style and effect.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        /// <param name="rect">Structure <see cref="RectangleF"/> that represents the rectangle to paint on.</param>
        /// <param name="orientation">One of the enumeration values <see cref="Orientation"/> that represents the orientation of the brush. </param>
        /// <returns>
        /// Returns a <see cref="Brush"/> object that represents the drawing brush.
        /// </returns>
        /// <exception cref="ArgumentNullException">The value of image is <b>null</b>.</exception>
        public static Graphics ToOrientation(this Graphics graphics, Rectangle rect, Orientation orientation)
        {
            SentinelHelper.ArgumentNull(graphics, nameof(graphics));

            bool isValidRect = rect.IsValid();
            if (!isValidRect)
            {
                return graphics;
            }

            int translateX = 0;
            int translateY = 0;
            float rotation = 0f;

            switch (orientation)
            {
                case Orientation.Left:
                    rect = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);

                    // Translate back from a quater left turn to the original place 
                    translateX = rect.X - rect.Y - 1;
                    translateY = rect.X + rect.Y + rect.Width;
                    rotation = 270;
                    break;

                case Orientation.Right:
                    // Invert the dimensions of the rectangle for drawing upwards
                    rect = new Rectangle(rect.X, rect.Y, rect.Height, rect.Width);

                    // Translate back from a quater right turn to the original place 
                    translateX = rect.X + rect.Y + rect.Height + 1;
                    translateY = -(rect.X - rect.Y);
                    rotation = 90f;
                    break;

                case Orientation.Bottom:
                    // Translate to opposite side of origin, so the rotate can then bring it back to original position but mirror image
                    translateX = rect.X * 2 + rect.Width;
                    translateY = rect.Y * 2 + rect.Height;
                    rotation = 180f;
                    break;
            }

            // Apply the transforms if we have any to apply
            if ((translateX != 0) || (translateY != 0))
            {
                graphics.TranslateTransform(translateX, translateY);
            }

            if (rotation != 0f)
            {
                graphics.RotateTransform(rotation);
            }

            return graphics;
        }
    }
}
