
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Drawing.Helpers;
using iTin.Core.Helpers;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="Image"/>.
    /// </summary> 
    public static class ImageExtensions
    {
        #region [public] {static} (Image) AdjustBrightness(this Image, int): Adjusts the brightness of Image
        /// <summary>
        /// Adjusts the brightness of <see cref="Image"/>.
        /// </summary>
        /// <param name="source">Image source.</param>
        /// <param name="brightness">The brightness.</param>
        /// <returns>
        /// A new <see cref="Image"/>
        /// </returns>        
        public static Image AdjustBrightness(this Image source, int brightness)
        {
            if (source == null)
            {
                return null;
            }

            Bitmap imgAgtEmp1 = new Bitmap(source);
            Bitmap imgAgtEmp2 = new Bitmap(source);

            int width = imgAgtEmp1.Width;
            int height = imgAgtEmp1.Height;
            Color[,] matrix = new Color[height, width];

            for (var j = 0; j < width; j++)
            {
                for (var i = 0; i < height; i++)
                {
                    matrix[i, j] = imgAgtEmp1.GetPixel(j, i);
                    var elColor = matrix[i, j];

                    int r;
                    if (elColor.R + brightness < 0)
                    {
                        r = 0;
                    }
                    else
                    {
                        if (elColor.R + brightness > 255)
                        {
                            r = 255;
                        }
                        else
                        {
                            r = elColor.R + brightness;
                        }
                    }

                    int g;
                    if (elColor.G + brightness < 0)
                    {
                        g = 0;
                    }
                    else
                    {
                        if (elColor.G + brightness > 255)
                        {
                            g = 255;
                        }
                        else
                        {
                            g = elColor.G + brightness;
                        }
                    }

                    int b;
                    if (elColor.B + brightness < 0)
                    {
                        b = 0;
                    }
                    else
                    {
                        if (elColor.B + brightness > 255)
                        {
                            b = 255;
                        }
                        else
                        {
                            b = elColor.B + brightness;
                        }
                    }

                    elColor = Color.FromArgb(r, g, b);
                    imgAgtEmp2.SetPixel(j, i, elColor);
                }
            }

            imgAgtEmp1.Dispose();
            return imgAgtEmp2;
        }
        #endregion


        #region [public] {static} (Image) ApplyEffect(this Image, IEffect): Returns a new image with the specified effect
        /// <summary>
        /// Returns a new <see cref="Image"/> with the specified effect.
        /// </summary>
        /// <param name="image">Image object to which the effect is applied.</param>
        /// <param name="effect">Effect type.</param>
        /// <returns>
        /// Returns a new <see cref="Image"/>, result of applying the effect to specified image .
        /// </returns>
        public static Image ApplyEffect(this Image image, IEffect effect)
        {
            SentinelHelper.ArgumentNull(image, nameof(image));

            Image imageWithEffect;
            using (var bmp = new Bitmap(image.Width, image.Height))
            using (var graphics = Graphics.FromImage(bmp))
            {
                bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                graphics.DrawImage(
                    image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0,
                    0,
                    bmp.Width,
                    bmp.Height,
                    GraphicsUnit.Pixel,
                    effect.Apply());
                imageWithEffect = (Image)bmp.Clone();
            }

            return imageWithEffect;
        }
        #endregion

        #region [public] {static} (Image) ApplyEffect(this Image, EffectType): Returns a new Image with the specified effect
        /// <summary>
        /// Returns a new <see cref="Image"/> with the specified effect.
        /// </summary>
        /// <param name="image">Image object to which the effect is applied.</param>
        /// <param name="effect">Effect type.</param>
        /// <returns>
        /// Returns a new <see cref="Image"/>, result of applying the effect to specified image .
        /// </returns>
        public static Image ApplyEffect(this Image image, EffectType effect)
        {
            SentinelHelper.ArgumentNull(image, nameof(image));

            Image imageWithEffect;
            using (var bmp = new Bitmap(image.Width, image.Height))
            using (var graphics = Graphics.FromImage(bmp))
            {
                bmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                graphics.DrawImage(
                    image,
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
                    0,
                    0,
                    bmp.Width,
                    bmp.Height,
                    GraphicsUnit.Pixel,
                    ImageHelper.GetImageAttributesFromEffect(effect));
                imageWithEffect = (Image)bmp.Clone();
            }

            return imageWithEffect;
        }
        #endregion

        #region [public] {static} (Image) ApplyEffects(this Image, IEffect[]): Returns a new Image with the specified effects
        /// <summary>
        /// Returns a new Image with the specified effects.
        /// </summary>
        /// <param name="image">Image object to which the effect is applied.</param>
        /// <param name="effects">Array of <see cref="IEffect"/> with different effects to apply.</param>
        /// <returns>
        /// Returns a new <see cref="Image"/>, result of applying the effects to specified image .
        /// </returns>
        public static Image ApplyEffects(this Image image, IEffect[] effects)
        {
            SentinelHelper.ArgumentNull(image, nameof(image));
            SentinelHelper.ArgumentNull(effects, nameof(effects));

            Image imageWithEffect = (Image)image.Clone();
            return effects.Aggregate(imageWithEffect, (current, effect) => current.ApplyEffect(effect));
        }
        #endregion

        #region [public] {static} (Image) ApplyEffects(this Image, EffectType[]): Returns a new Image with the specified effects
        /// <summary>
        /// Returns a new Image with the specified effects.
        /// </summary>
        /// <param name="image">Image object to which the effect is applied.</param>
        /// <param name="effects">Array of <see cref="EffectType"/> with different effects to apply.</param>
        /// <returns>
        /// Returns a new <see cref="Image"/>, result of applying the effects to specified image .
        /// </returns>
        public static Image ApplyEffects(this Image image, EffectType[] effects)
        {
            SentinelHelper.ArgumentNull(image, nameof(image));
            SentinelHelper.ArgumentNull(effects, nameof(effects));

            Image imageWithEffect = (Image)image.Clone();
            return effects.Aggregate(imageWithEffect, (current, e) => current.ApplyEffect(e));
        }
        #endregion


        #region [public] {static} (byte[]) AsByteArray(this Image): Converts an image as png into byte array
        /// <summary>
        /// Converts an <see cref="Image"/> as png into byte array.
        /// </summary>
        /// <param name="image">Image to convert.</param>
        /// <returns>
        /// Byte array.
        /// </returns>
        public static byte[] AsByteArray(this Image image) => image.AsByteArray(ImageFormat.Png);
        #endregion

        #region [public] {static} (byte[]) AsByteArray(this Image, ImageFormat): Converts an image with specified format into byte array
        /// <summary>
        /// Converts an <see cref="Image" /> into byte array.
        /// </summary>
        /// <param name="image">Image to convert.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// Byte array.
        /// </returns>
        public static byte[] AsByteArray(this Image image, ImageFormat format) => image.AsStream(format).AsByteArray();
        #endregion


        #region [public] {static} (Stream) AsStream(this Image): Returns a stream which represents the input image as png image format
        /// <summary>
        /// Returns a <see cref="Stream"/> which represents the input <see cref="Image"/> as png image format.
        /// </summary>
        /// <param name="image">Image to convert.</param>
        /// <returns>
        /// Stream.
        /// </returns>
        public static Stream AsStream(this Image image) => image.AsStream(ImageFormat.Png);
        #endregion

        #region [public] {static} (Stream) AsStream(this Image, ImageFormat): Returns a stream which represents the input image with specified image format
        /// <summary>
        /// Returns a <see cref="Stream"/> which represents the input <see cref="Image"/> with specified image format.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="format">The format.</param>
        /// <returns>
        /// Stream.
        /// </returns>
        public static Stream AsStream(this Image image, ImageFormat format)
        {
            MemoryStream stream = new MemoryStream();
            image.Save(stream, format);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        #endregion


        #region [public] {static} (Image) Flip(this Image, KnownFlipStyle): Returns a new image rotated by the specified orientation
        /// <summary>
        /// Returns a new <see cref="Image"/> rotated by the specified orientation.
        /// </summary>
        /// <param name="image"><see cref="Image"/> to be rotated.</param>
        /// <param name="style">New orientation.</param>
        /// <returns>
        /// Returns a new <see cref="Image"/> rotated by the specified orientation..
        /// </returns>
        public static Image Flip(this Image image, FlipStyle style)
        {
            SentinelHelper.IsEnumValid(style);

            if (image == null)
            {
                return null;
            }

            var flipImage = (Image)image.Clone();
            switch (style)
            {
                case FlipStyle.None:
                    flipImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;

                case FlipStyle.X:
                    flipImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;

                case FlipStyle.Y:
                    flipImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    break;

                case FlipStyle.XY:
                    flipImage.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
            }

            return flipImage;
        }
        #endregion


        #region [public] {static} (Image) Rotate(this Image, KnownOrientation): Returns a new image rotated according to the specified orientation
        /// <summary>
        /// Returns a new <see cref="Image"/> rotated according to the specified orientation.
        /// </summary>
        /// <param name="image">A <see cref="Image"/> to be rotated.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the orientation.</param>
        /// <returns>
        /// An object <see cref="Image"/> rotated according to the specified orientation.
        /// </returns>
        public static Image Rotate(this Image image, Orientation orientation)
        {
            if (image == null)
            {
                return null;
            }

            Image rotatedImage = (Image)image.Clone();
            switch (orientation)
            {
                case Orientation.Left:
                    rotatedImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;

                case Orientation.Top:
                    rotatedImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;

                case Orientation.Right:
                    rotatedImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;

                case Orientation.Bottom:
                    rotatedImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
            }

            return rotatedImage;
        }
        #endregion


        #region [public] {static} (Image) ScalePercent(this Image, float): Returns a new scaled image to a certain percentage
        /// <summary>
        /// Returns a new scaled <see cref="Image"/> to a certain percentage
        /// </summary>
        /// <param name="image">Image to scale</param>
        /// <param name="percent">the scaling percentage</param>
        public static Image ScalePercent(this Image image, float percent) => image.ScalePercent(percent, percent);
        #endregion

        #region [public] {static} (Image) ScalePercent(this Image, SizeF): Returns a new image scaled to a certain percentage
        /// <summary>
        /// Returns a new scaled <see cref="Image"/> to a certain percentage
        /// </summary>
        /// <param name="image">Image to scale</param>
        /// <param name="size">the scaling percentage of the width and height</param>
        public static Image ScalePercent(this Image image, SizeF size) => image.ScalePercent(size.Width, size.Height);
        #endregion

        #region [public] {static} (Image) ScalePercent(this Image, float, float): Returns a new image scaled to a certain percentage
        /// <summary>
        /// Returns a new scaled <see cref="Image"/> to a certain percentage
        /// </summary>
        /// <param name="image">Image to scale</param>
        /// <param name="percentX">the scaling percentage of the width</param>
        /// <param name="percentY">the scaling percentage of the height</param>
        public static Image ScalePercent(this Image image, float percentX, float percentY)
        {
            if (image == null)
            {
                return null;
            }

            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            // Consider vertical pics
            if (sourceWidth < sourceHeight)
            {
                float buff = percentX;

                percentX = percentY;
                percentY = buff;
            }

            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;
            float nPercent;

            float nPercentW = percentX / sourceWidth;
            float nPercentH = percentY / sourceHeight;
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((percentX - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((percentY - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);


            Bitmap bmPhoto = new Bitmap((int)percentX, (int)percentY, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Black);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(image,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            image.Dispose();

            return bmPhoto;
        }
        #endregion


        #region [public] {static} (Image) ScaleToFit(this Image, float): Returns a new scaled image so that it fits a certain width and height
        /// <summary>
        /// Returns a new scaled <see cref="Image"/> so that it fits a certain width and height.
        /// </summary>
        /// <param name="image">Image to scale</param>
        /// <param name="percent">the scaling percentage</param>
        public static Image ScaleToFit(this Image image, float percent) => image.ScalePercent(percent, percent);
        #endregion

        #region [public] {static} (Image) ScaleToFit(this Image, SizeF): Returns a new scaled image so that it fits a certain width and height
        /// <summary>
        /// Returns a new scaled <see cref="Image"/> so that it fits a certain width and height.
        /// </summary>
        /// <param name="image">Image to scale</param>
        /// <param name="size">the width to fit</param>
        public static Image ScaleToFit(this Image image, SizeF size) => image.ScalePercent(size.Width, size.Height);
        #endregion

        #region [public] {static} (Image) ScaleToFit(this Image, float, float): Returns a new scaled image so that it fits a certain width and height
        /// <summary>
        /// Returns a new scaled <see cref="Image"/> so that it fits a certain width and height.
        /// </summary>
        /// <param name="image">Image to scale</param>
        /// <param name="width">the width to fit</param>
        /// <param name="height">the width to fit</param>
        public static Image ScaleToFit(this Image image, float width, float height)
        {
            try
            {
                if (width.Equals(image.Width) && height.Equals(image.Height))
                {
                    return image;
                }

                float xRatio = width / image.Width;
                float yRatio = height / image.Height;
                float ratio = Math.Min(xRatio, yRatio);

                SizeF newSize =
                    new SizeF(
                        Math.Min(width, (float)Math.Round(image.Width * ratio, MidpointRounding.AwayFromZero)),
                        Math.Min(height, (float)Math.Round(image.Height * ratio, MidpointRounding.AwayFromZero)));

                // Invialidate internally stored thumbnails.
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                image.RotateFlip(RotateFlipType.Rotate180FlipNone);

                Bitmap newImage = new Bitmap((int)newSize.Width, (int)newSize.Height);
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawImage(image, new RectangleF(new PointF(0.0f, 0.0f), newSize));
                }
                
                return Image.FromStream(newImage.AsStream());
            }
            catch
            {
                return image;
            }
        }
        #endregion


        #region [public] {static} (string) ToBase64(this Image): Converts an image as png into a base64 string       
        /// <summary>
        /// Converts an <see cref="Image"/> as png into a base64 string 
        /// </summary>
        /// <param name="image">Image to convert</param>
        /// <returns>
        /// A <see cref="String"/> which represents an <see cref="Image"/>.
        /// </returns>
        public static string ToBase64(this Image image) => image.ToBase64(ImageFormat.Png);
        #endregion
           
        #region [public] {static} (string) ToBase64(this Image, ImageFormat): Converts an image into a base64 string       
        /// <summary>
        /// Converts an <see cref="Image"/> into a base64 string 
        /// </summary>
        /// <param name="image">Image to convert</param>
        /// <param name="format">The image format.</param>
        /// <returns>
        /// A <see cref="String"/> which represents an <see cref="Image"/>.
        /// </returns>
        public static string ToBase64(this Image image, ImageFormat format)
        {
            using (Stream stream = image.AsStream(format))
            {
                byte[] bytes = stream.AsByteArray();

                return Convert.ToBase64String(bytes);                
            }
        }
        #endregion


        #region [public] {static} (Brush) ToBrush(this Image, RectangleF, ImageStyle): Returns a drawing brush oriented vertically upwards from a reference image without applying an effect specifying the style
        /// <summary>
        /// Returns a drawing <see cref="Brush"/> oriented vertically upwards from a reference image without applying an effect specifying the style.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> base object to create the drawing brush.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="style">One of the values in the enumeration <see cref="ImageStyle"/> that represents image style.</param>
        /// <returns>
        /// Returns a <see cref="Brush"/> object reference that represents the drawing brush.
        /// </returns>
        public static Brush ToBrush(this Image image, RectangleF rect, ImageStyle style) => ToBrush(image, rect, style, Orientation.Top);
        #endregion

        #region [public] {static} (Brush) ToBrush(this Image, RectangleF, ImageStyle, string, EffectType): Returns a drawing brush oriented vertically upwards from a reference image specifying a style and an effect
        /// <summary>
        /// Returns a drawing <see cref="Brush"/> oriented vertically upwards from a reference image specifying a style and an effect.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> base object to create the drawing brush.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="style">One of the values in the enumeration <see cref="ImageStyle"/> that represents image style.</param>
        /// <param name="effect">One of the values in the enumeration <see cref="EffectType"/> that represents the type of effect to be applied.</param>
        /// <returns>
        /// Returns a <see cref="Brush"/> object reference that represents the drawing brush.
        /// </returns>
        public static Brush ToBrush(this Image image, RectangleF rect, ImageStyle style, EffectType effect) => ToBrush(image, rect, style, effect, Orientation.Top);
        #endregion

        #region [public] {static} (Brush) ToBrush(this Image, RectangleF, ImageStyle, Orientation): Returns a brush to draw from a reference image specifying the orientation and style
        /// <summary>
        /// Returns a <see cref="Brush"/> to draw from a reference image specifying the orientation and style.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> base object to create the drawing brush.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="style">One of the values in the enumeration <see cref="ImageStyle"/> that represents image style.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the orientation of the brush.</param>
        /// <returns>
        /// Returns a <see cref="Brush"/> object reference that represents the drawing brush.
        /// </returns>
        public static Brush ToBrush(this Image image, RectangleF rect, ImageStyle style, Orientation orientation) => ToBrush(image, rect, style, EffectType.None, orientation);
        #endregion

        #region [public] {static} (Brush) ToBrush(this Image, RectangleF, ImageStyle, EffectType, Orientation): Returns a brush to draw from a reference image specifying the orientation, style and effect.
        /// <summary>
        /// Returns a <see cref="Brush"/> to draw from a reference image specifying the orientation, style and effect.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> base object to create the drawing brush.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="style">One of the values in the enumeration <see cref="ImageStyle"/> that represents image style.</param>
        /// <param name="effect">One of the values in the enumeration <see cref="EffectType"/> that represents the type of effect to be applied.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the orientation of the brush.</param>
        /// <returns>
        /// Returns a <see cref="Brush"/> object reference that represents the drawing brush.
        /// </returns>
        public static Brush ToBrush(this Image image, RectangleF rect, ImageStyle style, EffectType effect, Orientation orientation) => ToBrush(image, rect, style, effect, orientation, SmoothingModeEx.HighQuality);
        #endregion

        #region [public] {static} (Brush) ToBrush(this Image, RectangleF, ImageStyle, EffectType, Orientation, SmoothingModeEx): Returns a brush to draw from a reference image specifying the orientation, quality, style and effect
        /// <summary>
        /// Returns a <see cref="Brush"/> to draw from a reference image specifying the orientation, quality, style and effect.
        /// </summary>
        /// <param name="image">An <see cref="Image"/> base object to create the drawing brush.</param>
        /// <param name="rect">A <see cref="RectangleF"/> structure that represents the rectangle in which to paint.</param>
        /// <param name="style">One of the values in the enumeration <see cref="ImageStyle"/> that represents image style.</param>
        /// <param name="effect">One of the values in the enumeration <see cref="EffectType"/> that represents the type of effect to be applied.</param>
        /// <param name="orientation">One of the values in the enumeration <see cref="Orientation"/> that represents the orientation of the brush.</param>
        /// <param name="quality">One of the values in the enumeration <see cref="SmoothingModeEx"/> that represents the quality of presentation</param>
        /// <returns>
        /// Returns a <see cref="Brush"/> object reference that represents the drawing brush.
        /// </returns>
        public static Brush ToBrush(this Image image, RectangleF rect, ImageStyle style, EffectType effect, Orientation orientation, SmoothingModeEx quality)
        {
            if (image == null)
            {
                return new SolidBrush(Color.Empty);
            }

            Brush brush;
            TextureBrush tempTextureBrush = null;

            Image texture = image.Rotate(orientation).ApplyEffect(effect);
            using (var bitmap = new Bitmap(texture.Width, texture.Height))
            using (var graphics = Graphics.FromImage(bitmap))
            using (new Smoothing(graphics, quality))
            {
                Rectangle dstRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                Rectangle srcRect = new Rectangle(Point.Empty, texture.Size);
                graphics.DrawImage(texture, dstRect, srcRect, GraphicsUnit.Pixel);

                try
                {
                    tempTextureBrush = new TextureBrush(bitmap) {WrapMode = style.ToWrapMode()};

                    switch (style)
                    {
                        case ImageStyle.TopLeft:
                            tempTextureBrush.TranslateTransform(rect.Left, rect.Top);
                            break;

                        case ImageStyle.TopMiddle:
                            tempTextureBrush.TranslateTransform(rect.Left + (rect.Width - texture.Width) / 2, rect.Top);
                            break;

                        case ImageStyle.TopRight:
                            tempTextureBrush.TranslateTransform(rect.Right - texture.Width, rect.Top);
                            break;

                        case ImageStyle.CenterLeft:
                            tempTextureBrush.TranslateTransform(rect.Left, rect.Top + (rect.Height - texture.Height) / 2);
                            break;

                        case ImageStyle.CenterMiddle:
                            tempTextureBrush.TranslateTransform(rect.Left + (rect.Width - texture.Width) / 2, rect.Top + (rect.Height - texture.Height) / 2);
                            break;

                        case ImageStyle.CenterRight:
                            tempTextureBrush.TranslateTransform(rect.Right - texture.Width, rect.Top + (rect.Height - texture.Height) / 2);
                            break;

                        case ImageStyle.BottomLeft:
                            tempTextureBrush.TranslateTransform(rect.Left, rect.Bottom - texture.Height);
                            break;

                        case ImageStyle.BottomMiddle:
                            tempTextureBrush.TranslateTransform(rect.Left + (rect.Width - texture.Width) / 2, rect.Bottom - texture.Height);
                            break;

                        case ImageStyle.BottomRight:
                            tempTextureBrush.TranslateTransform(rect.Right - texture.Width, rect.Bottom - texture.Height);
                            break;

                        case ImageStyle.Stretch:
                            tempTextureBrush.TranslateTransform(rect.Left, rect.Top);
                            tempTextureBrush.ScaleTransform(rect.Width / texture.Width, rect.Height / texture.Height);
                            break;

                        case ImageStyle.Tile:
                        case ImageStyle.TileFlipX:
                        case ImageStyle.TileFlipY:
                        case ImageStyle.TileFlipXY:
                            tempTextureBrush.TranslateTransform(rect.Left, rect.Top);
                            break;
                    }

                    brush = (Brush)tempTextureBrush.Clone();
                }
                finally
                {
                    tempTextureBrush?.Dispose();
                }
            }

            return brush;
        }
        #endregion
    }
}
