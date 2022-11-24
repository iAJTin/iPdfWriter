
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using iTin.Core.Drawing.ComponentModel;

namespace iTin.Core.Drawing.Helpers
{
    /// <summary> 
    /// Static class than contains methods for manipulating images.
    /// </summary>
    public static class ImageHelper
    {
        private static readonly ColorMatrix DisabledColorMatrix =
            new(
                new[]
                {
                    new[] { 0.30f, 0.30f, 0.30f, 0.00f, 0.00f },
                    new[] { 0.59f, 0.59f, 0.59f, 0.00f, 0.00f },
                    new[] { 0.11f, 0.11f, 0.11f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, -0.60f, 1.00f }
                });

        private static readonly ColorMatrix GrayScaleColorMatrix =
            new(
                new[]
                {
                    new[] { 0.30f, 0.30f, 0.30f, 0.00f, 0.00f },
                    new[] { 0.59f, 0.59f, 0.59f, 0.00f, 0.00f },
                    new[] { 0.11f, 0.11f, 0.11f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix GrayScaleRedColorMatrix =
            new(
                new[]
                {
                    new[] { 1.00f, 0.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.59f, 0.59f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.11f, 0.11f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix GrayScaleGreenColorMatrix =
            new(
                new[]
                {
                    new[] { 0.30f, 0.00f, 0.30f, 0.00f, 0.00f },
                    new[] { 0.00f, 1.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.11f, 0.00f, 0.11f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix GrayScaleBlueColorMatrix =
            new(
                new[]
                {
                    new[] { 0.30f, 0.30f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.59f, 0.59f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 1.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix BrightnessColorMatrix =
            new(
                new[]
                {
                    new[] { 1.00f, 0.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 1.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 1.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.10f, 0.10f, 0.10f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix MoreBrightnessColorMatrix =
            new(
                new[]
                {
                    new[] { 1.00f, 0.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 1.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 1.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { 0.20f, 0.20f, 0.20f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix DarkColorMatrix =
            new(
                new[]
                {
                    new[] { 1.00f, 0.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 1.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 1.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { -0.10f, -0.10f, -0.10f, 0.00f, 1.00f }
                });

        private static readonly ColorMatrix MoreDarkColorMatrix =
            new(
                new[]
                {
                    new[] { 1.00f, 0.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 1.00f, 0.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 1.00f, 0.00f, 0.00f },
                    new[] { 0.00f, 0.00f, 0.00f, 1.00f, 0.00f },
                    new[] { -0.25f, -0.25f, -0.25f, 0.00f, 1.00f }
                });

        /// <summary>
        /// Converts a image in base64 codificaction into a <see cref="Image"/>.
        /// </summary>
        /// <param name="base64String">image as base64.</param>
        /// <returns>
        /// A <see cref="Image"/> object that contains the image.
        /// </returns>
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            var imageBytes = Convert.FromBase64String(base64String);

            using var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);

            return Image.FromStream(ms, true);
        }

        /// <summary>
        /// Gets the manipulation of the colors in an image to an effect.
        /// </summary>
        /// <param name="effect">Effect type.</param>
        /// <returns>
        /// A <see cref="ImageAttributes"/> object that contains the information about how bitmap colors are manipulated. 
        /// </returns>
        public static ImageAttributes GetImageAttributesFromEffect(EffectType effect)
        {
            var newColorMatrix = new ColorMatrix();
            switch (effect)
            {
                case EffectType.None:
                    break;

                case EffectType.Disabled:
                    newColorMatrix = DisabledColorMatrix;
                    break;

                case EffectType.GrayScale:
                    newColorMatrix = GrayScaleColorMatrix;
                    break;

                case EffectType.GrayScaleRed:
                    newColorMatrix = GrayScaleRedColorMatrix;
                    break;

                case EffectType.GrayScaleGreen:
                    newColorMatrix = GrayScaleGreenColorMatrix;
                    break;

                case EffectType.Brightness:
                    newColorMatrix = BrightnessColorMatrix;
                    break;

                case EffectType.MoreBrightness:
                    newColorMatrix = MoreBrightnessColorMatrix;
                    break;

                case EffectType.Dark:
                    newColorMatrix = DarkColorMatrix;
                    break;

                case EffectType.MoreDark:
                    newColorMatrix = MoreDarkColorMatrix;
                    break;

                case EffectType.GrayScaleBlue:
                    newColorMatrix = GrayScaleBlueColorMatrix;
                    break;

                case EffectType.OpacityPercent05:
                    newColorMatrix.Matrix33 = 0.05f;
                    break;

                case EffectType.OpacityPercent10:
                    newColorMatrix.Matrix33 = 0.10f;
                    break;

                case EffectType.OpacityPercent20:
                    newColorMatrix.Matrix33 = 0.20f;
                    break;

                case EffectType.OpacityPercent30:
                    newColorMatrix.Matrix33 = 0.30f;
                    break;

                case EffectType.OpacityPercent40:
                    newColorMatrix.Matrix33 = 0.40f;
                    break;

                case EffectType.OpacityPercent50:
                    newColorMatrix.Matrix33 = 0.50f;
                    break;

                case EffectType.OpacityPercent60:
                    newColorMatrix.Matrix33 = 0.60f;
                    break;

                case EffectType.OpacityPercent70:
                    newColorMatrix.Matrix33 = 0.70f;
                    break;

                case EffectType.OpacityPercent80:
                    newColorMatrix.Matrix33 = 0.80f;
                    break;

                case EffectType.OpacityPercent90:
                    newColorMatrix.Matrix33 = 0.90f;
                    break;
            }

            ImageAttributes imageAttributes;
            ImageAttributes tempImageAttributes = null;
            try
            {
                tempImageAttributes = new ImageAttributes();
                tempImageAttributes.SetColorMatrix(newColorMatrix);
                imageAttributes = (ImageAttributes)tempImageAttributes.Clone();
            }
            finally
            {
                tempImageAttributes?.Dispose();
            }

            return imageAttributes;
        }

        /// <summary>
        /// Gets the manipulation of the colors in an image to an effect.
        /// </summary>
        /// <param name="threshold">Effect type.</param>
        /// <returns>
        /// A <see cref="ImageAttributes"/> object that contains the information about how bitmap colors are manipulated. 
        /// </returns>
        public static ImageAttributes GetImageAttributesFromOpacityValueEffect(float threshold)
        {
            ImageAttributes imageAttributes;
            ImageAttributes tempImageAttributes = null;
            try
            {
                tempImageAttributes = new ImageAttributes();
                tempImageAttributes.SetColorMatrix(new ColorMatrix { Matrix33 = threshold / 100.0f });
                imageAttributes = (ImageAttributes)tempImageAttributes.Clone();
            }
            finally
            {
                tempImageAttributes?.Dispose();
            }

            return imageAttributes;
        }

        /// <summary>
        /// Gets the type <c>MIME</c> of the image
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <returns>
        /// A <see cref="String"/> which contains <b>MIME</b> type.
        /// </returns>
        public static string GetImageMimeType(byte[] imageData)
        {
            var mimeType = "image/unknown";

            try
            {
                Guid id;

                using (var ms = imageData.ToMemoryStream())
                {
                    using var img = Image.FromStream(ms);
                    id = img.RawFormat.Guid;
                }

                if (id == ImageFormat.Png.Guid)
                {
                    mimeType = "image/png";
                }
                else if (id == ImageFormat.Bmp.Guid)
                {
                    mimeType = "image/bmp";
                }
                else if (id == ImageFormat.Emf.Guid)
                {
                    mimeType = "image/x-emf";
                }
                else if (id == ImageFormat.Exif.Guid)
                {
                    mimeType = "image/jpeg";
                }
                else if (id == ImageFormat.Gif.Guid)
                {
                    mimeType = "image/gif";
                }
                else if (id == ImageFormat.Icon.Guid)
                {
                    mimeType = "image/ico";
                }
                else if (id == ImageFormat.Jpeg.Guid)
                {
                    mimeType = "image/jpeg";
                }
                else if (id == ImageFormat.MemoryBmp.Guid)
                {
                    mimeType = "image/bmp";
                }
                else if (id == ImageFormat.Tiff.Guid)
                {
                    mimeType = "image/tiff";
                }
                else if (id == ImageFormat.Wmf.Guid)
                {
                    mimeType = "image/wmf";
                }
            }
            catch
            {
                // Nothing to do
            }

            return mimeType;
        }
    }
}
