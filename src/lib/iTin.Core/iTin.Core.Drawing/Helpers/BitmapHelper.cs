
using System.Drawing;
using System.Drawing.Imaging;

namespace iTin.Core.Drawing.Helpers
{
    /// <summary>
    /// Static class than contains methods for manipulating objects of type <see cref="Bitmap"/>.
    /// </summary>
    public static class BitmapHelper
    {
        /// <summary>
        /// Creates a new <see cref="Bitmap"/> from width and height specified.
        /// </summary>
        /// <param name="width">Width value</param>
        /// <param name="height">Height value</param>
        /// <returns>
        /// A new <see cref="Bitmap"/>.
        /// </returns>
        public static Bitmap CreateEmptyBitmap(int width, int height) => CreateEmptyBitmap(width, height, Color.White);

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> from width and height specified.
        /// </summary>
        /// <param name="width">Width value</param>
        /// <param name="height">Height value</param>
        /// <returns>
        /// A new <see cref="Bitmap"/>.
        /// </returns>
        public static Bitmap CreateEmptyBitmap(float width, float height) => CreateEmptyBitmap(width, height, Color.White);

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> from width, height and color specified.
        /// </summary>
        /// <param name="width">Width value</param>
        /// <param name="height">Height value</param>
        /// <param name="color">Bitmap color</param>
        /// <returns>
        /// A new <see cref="Bitmap"/>.
        /// </returns>
        public static Bitmap CreateEmptyBitmap(float width, float height, Color color) => CreateEmptyBitmap((int) width, (int) height, color);

        /// <summary>
        /// Creates a new <see cref="Bitmap"/> from width, height and color specified.
        /// </summary>
        /// <param name="width">Width value</param>
        /// <param name="height">Height value</param>
        /// <param name="color">Bitmap color</param>
        /// <returns>
        /// A new <see cref="Bitmap"/>.
        /// </returns>
        public static Bitmap CreateEmptyBitmap(int width, int height, Color color)
        {
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var g = Graphics.FromImage(bmp);
            var brush = new SolidBrush(color);
            g.FillRectangle(brush, 0, 0, width, height);

            return bmp;
        }
    }
}
