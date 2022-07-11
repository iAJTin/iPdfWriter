
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Class that constains methods for image color detection.
    /// </summary>
    public class Imaging
    {
        /// <summary>
        /// Returns a new <see cref="Image"/> that contains the target color resalted in color white.
        /// </summary>
        /// <param name="image">Image reference</param>
        /// <param name="color">Color reference</param>
        /// <param name="tolerance">Target tolerance</param>
        /// <returns>
        /// A new <see cref="Image"/> instance that contains the target color resalted in color white.
        /// </returns>
        public static Image DetectColor(Bitmap image, Color color, int tolerance) => DetectColor(image, color.R, color.G, color.B, tolerance);

        /// <summary>
        /// Returns a new <see cref="Image"/> that contains the target color resalted in color white.
        /// </summary>
        /// <param name="image">Image reference</param>
        /// <param name="R">Red color component</param>
        /// <param name="G">Green color component</param>
        /// <param name="B">Blue color component</param>
        /// <param name="tolerance">Target tolerance</param>
        /// <returns>
        /// A new <see cref="Image"/> instance that contains the target color resalted in color white.
        /// </returns>
        public static Image DetectColor(Bitmap image, byte R, byte G, int B, int tolerance)
        {
            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            byte[] imageBytes = new byte[Math.Abs(imageData.Stride) * image.Height];
            IntPtr scan0 = imageData.Scan0;

            Marshal.Copy(scan0, imageBytes, 0, imageBytes.Length);

            byte matchingValue = 0xff;
            byte unmatchingValue = 0x00;
            int toleranceSquared = tolerance * tolerance;

            for (int i = 0; i < imageBytes.Length; i += 3)
            {
                byte pixelB = imageBytes[i];
                byte pixelG = imageBytes[i + 1];
                byte pixelR = imageBytes[i + 2];

                int diffR = pixelR - R;
                int diffG = pixelG - G;
                int diffB = pixelB - B;

                int distance = diffR * diffR + diffG * diffG + diffB * diffB;

                imageBytes[i] = imageBytes[i + 1] = imageBytes[i + 2] = distance > toleranceSquared ? unmatchingValue : matchingValue;
            }

            Marshal.Copy(imageBytes, 0, scan0, imageBytes.Length);

            image.UnlockBits(imageData);

            return (Bitmap)image.Clone();
        }
    }
}
