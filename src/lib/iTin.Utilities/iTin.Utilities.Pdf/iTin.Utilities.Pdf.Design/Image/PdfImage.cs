
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

using iTin.Core;
using iTin.Core.Drawing;
using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Helpers;

using NativeImage = System.Drawing.Image;
using NativePdfImage = iTextSharp.text.Image;
using NativePdfRectangle = iTextSharp.text.Rectangle;

using iTinIO = iTin.Core.IO;

namespace iTin.Utilities.Pdf.Design.Image
{
    /// <summary>
    /// Defines a <b>pdf</b> image object.
    /// </summary>
    public sealed class PdfImage : IEquatable<PdfImage>, IDisposable
    {
        #region public static readonly members

        /// <summary>
        /// Gets a reference indicating that this instance is not valid.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImage"/> reference.
        /// </value>
        public static readonly PdfImage Null = new() { IsValid = false};

        #endregion
        
        #region private field members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _scaleX;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _scaleY;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _hasScaledFit;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _hasScaledPercent;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImage"/> class.
        /// </summary>
        internal PdfImage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImage"/> class with a image path and optional image configuration.
        /// </summary>
        /// <param name="imagePath">A reference to image path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.</param>
        /// <param name="configuration">Image configuration reference.</param>
        internal PdfImage(string imagePath, PdfImageConfig configuration = null)
        {
            var safeConfiguration = configuration;
            if (configuration == null)
            {
                safeConfiguration = PdfImageConfig.Default;
            }

            Configuration = safeConfiguration.Clone();

            var normalizedImagePath = iTinIO.Path.PathResolver(imagePath);
            Path = iTinIO.File.ToUri(normalizedImagePath);

            using var image = NativeImage.FromFile(normalizedImagePath);
            InitializeImage(image);

            IsValid = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImage"/> class from image with optional image configuration.
        /// </summary>
        /// <param name="image">A reference to image object.</param>
        /// <param name="configuration">Image configuration reference.</param>
        internal PdfImage(NativeImage image, PdfImageConfig configuration = null)
        {
            var safeConfiguration = configuration;
            if (configuration == null)
            {
                safeConfiguration = PdfImageConfig.Default;
            }

            Configuration = safeConfiguration.Clone();
            Path = null;

            InitializeImage(image);

            image?.Dispose();

            IsValid = true;
        }

        #endregion

        #region finalizer

        /// <summary>
        /// Finalizer
        /// </summary>
        ~PdfImage()
        {
            Dispose(false);
        }

        #endregion

        #region interfaces

        #region IDisposable

        #region public methods

        /// <summary>
        /// Clean managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        #region IEquatable

        #region public methods

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <b>true</b> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <b>false</b>.
        /// </returns>
        public bool Equals(PdfImage other) => other != null && other.Equals((object)this);

        /// <summary>
        /// Returns a value that indicates whether this class is equal to another
        /// </summary>
        /// <param name="obj">Class with which to compare.</param>
        /// <returns>
        /// Results equality comparison.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is not PdfImage other)
            {
                return false;
            }

            return
                other.Path == Path && 
                other.IsValid == IsValid && 
                other.ToString().Equals(ToString());
        }

        #endregion

        #endregion

        #endregion

        #region public readonly properties

        /// <summary>
        /// Gets a reference to image configuration information.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageConfig"/> that contains the configuration information.
        /// </value>
        public PdfImageConfig Configuration { get; }

        /// <summary>
        /// Gets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="iTextSharp.text.Image"/> object that contains a reference to pdf image object.
        /// </value>
        public NativePdfImage Image { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current instance is valid.
        /// </summary>
        /// <value>
        /// <b>true</b> if current instance is valid; otherwise <b>false</b>.
        /// </value>
        public bool IsValid { get; private set; }

        /// <summary>
        /// Gets a reference to original image.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.Drawing.Image"/> object that contains a reference to pdf image object.
        /// </value>
        public NativeImage OriginalImage { get; private set; }

        /// <summary>
        /// Gets a reference to image uri.
        /// </summary>
        /// <value>
        /// A <see cref="Uri"/> that contains the image uri.
        /// </value>
        public Uri Path { get; private set; }

        /// <summary>
        /// Gets a reference to the original image after it has been processed.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.Drawing.Image"/> object that contains a reference to pdf image object.
        /// </value>
        public NativeImage ProcessedImage { get; private set; }

        /// <summary>
        /// Gets a value containing scaled height of this image.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> containing scaled height of this image.
        /// </value>
        public float ScaledHeight { get; private set; }

        /// <summary>
        /// Gets a value containing scaled width of this image.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> containing scaled width of this image.
        /// </value>
        public float ScaledWidth { get; private set; }

        #endregion

        #region public methods

        /// <summary>
        /// Apply effects image to this instance.
        /// </summary>
        /// <param name="effects">the dimensions to fit</param>
        /// <returns>
        /// Returns this instance with effects.
        /// </returns>
        public PdfImage ApplyEffects(EffectType[] effects)
        {
            if (Equals(Null))
            {
                return this;
            }

            if (Image == null)
            {
                return this;
            }

            var image = 
                Configuration.Effects == null
                    ? NativeImage.FromStream(OriginalImage.AsStream())
                    : NativeImage.FromStream(OriginalImage.ApplyEffects(Configuration.Effects).AsStream());

            ProcessedImage = (NativeImage)image.ApplyEffects(effects).Clone();
            Image = NativePdfImage.GetInstance(ProcessedImage.AsStream());

            return this;
        }

        /// <summary>
        /// Scale the image to a certain percentage.
        /// </summary>
        /// <param name="percent">the scaling percentage</param>
        /// <returns>
        /// Returns this instance scaled to percent value.
        /// </returns>
        public PdfImage ScalePercent(float percent) => 
            ScalePercent(percent, percent);

        /// <summary>
        /// Scale the width and height of an image to a certain percentage.
        /// </summary>
        /// <param name="size">the scaling percentage of the width and height</param>
        /// <returns>
        /// Returns this instance scaled to percent value.
        /// </returns>
        public PdfImage ScalePercent(SizeF size) => 
            ScalePercent(size.Width, size.Height);

        /// <summary>
        /// Scale the width and height of an image to a certain percentage.
        /// </summary>
        /// <param name="percentX">the scaling percentage of the width</param>
        /// <param name="percentY">the scaling percentage of the height</param>
        /// <returns>
        /// Returns this instance scaled to percent value.
        /// </returns>
        public PdfImage ScalePercent(float percentX, float percentY)
        {
            if (Equals(Null))
            {
                return this;
            }

            if (Image == null)
            {
                return this;
            }

            ScaledHeight = (float)((double)Image.Height * (double)percentY / 100.0);
            ScaledWidth = (float)((double)Image.Width * (double)percentX / 100.0);

            return this;
        }

        /// <summary>
        /// Scale this image proportionally until at most indicate the value of the parameter <paramref name="maxSize"/>, if the value is less than or equal to zero it returns the raw image.
        /// </summary>
        /// <param name="maxSize">The max size for image</param>
        /// <param name="strategy">Strategy scale. The deafult value is <see cref="ScaleStrategy.Auto"/></param>
        /// <returns>
        /// Returns this instance scaled
        /// </returns>
        public PdfImage ScaleTo(float maxSize, ScaleStrategy strategy = ScaleStrategy.Auto)
        {
            if (Equals(Null))
            {
                return this;
            }

            if (Image == null)
            {
                return this;
            }

            if (maxSize <= 0)
            {
                return this;
            }

            float width;
            float height;

            switch (strategy)
            {
                case ScaleStrategy.Horizontal:
                    var hratio = maxSize / Image.Width;
                    height = Image.Height * hratio;
                    width = Image.Width * hratio;
                    break;

                case ScaleStrategy.Vertical:
                    var vratio = maxSize / Image.Height;
                    height = Image.Height * vratio;
                    width = Image.Width * vratio;
                    break;

                default:
                case ScaleStrategy.Auto:
                    var ratio = Math.Min(Image.Width, Image.Height) / Math.Max(Image.Width, Image.Height);
                    width = Image.Width >= Image.Height ? maxSize : maxSize * ratio;
                    height = Image.Height >= Image.Width ? maxSize : maxSize * ratio;
                    break;
            }

            ScaledHeight = height;
            ScaledWidth = width;

            return this;
        }

        /// <summary>
        /// Scales the images to the dimensions of the rectangle.
        /// </summary>
        /// <param name="rectangle">the dimensions to fit</param>
        /// <returns>
        /// Returns this instance scaled.
        /// </returns>
        public PdfImage ScaleToFit(NativePdfRectangle rectangle) => 
            ScaleToFit(rectangle.Width, rectangle.Height);

        /// <summary>
        /// Scales the images to the dimensions of the size.
        /// </summary>
        /// <param name="size">the scaling percentage of the size</param>
        /// <returns>
        /// Returns this instance scaled to percent value.
        /// </returns>
        public PdfImage ScaleToFit(SizeF size) => 
            ScaleToFit(size.Width, size.Height);

        /// <summary>
        /// Scales this image so that it fits a certain width and height.
        /// </summary>
        /// <param name="width">the width to fit</param>
        /// <param name="height">the height to fit</param>
        /// <returns>
        /// Returns this instance scaled.
        /// </returns>
        public PdfImage ScaleToFit(float width, float height)
        {
            if (Equals(Null))
            {
                return this;
            }

            if (Image == null)
            {
                return this;
            }

            _hasScaledPercent = false;
            _hasScaledFit = true;
            _scaleX = width;
            _scaleY = height;

            var original = (NativeImage)OriginalImage.Clone();
            var originalWithEffects = original;
            if (Configuration.Effects != null)
            {
                originalWithEffects = original.ApplyEffects(Configuration.Effects);
            }

            ProcessedImage = (NativeImage)originalWithEffects.ScaleToFit(width, height).Clone();

            return this;
        }

        #endregion

        #region public static methods

        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified byte array, format and optional image effect collection.
        /// </summary>
        /// <param name="array">Image as byte array.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromByteArray(byte[] array, PdfImageConfig configuration = null) => 
            FromStream(array.ToMemoryStream(), configuration);

        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified image path and optional image configuration.
        /// </summary>
        /// <param name="imagePath">Image path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromFile(string imagePath, PdfImageConfig configuration = null) => 
            new(imagePath, configuration);

        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified image, format and optional image effect collection.
        /// </summary>
        /// <param name="image">Image reference.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromImage(NativeImage image, PdfImageConfig configuration = null) => 
            new(image, configuration);

        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified stream, format and optional image effect collection.
        /// </summary>
        /// <param name="stream">Image as stream.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromStream(Stream stream, PdfImageConfig configuration = null) => 
            FromImage(NativeImage.FromStream(stream), configuration);

        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from the specified uri with the specified format, optionally you can also specify a collection of effects to apply to the image.
        /// If the process of getting the image fails or the uri is wrong, <b>null</b> is returned.
        /// </summary>
        /// <param name="imageUri">Uri to image resource.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <param name="timeout">An image effects collection to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromUri(Uri imageUri, PdfImageConfig configuration = null, int timeout = 15000)
        {
            SentinelHelper.ArgumentNull(imageUri, nameof(imageUri));

            PdfImage result = Null;

            var uriIsAccesibleResult = imageUri.IsAccessible();
            if (!uriIsAccesibleResult.Success)
            {
                return result;
            }

            try
            {
                using var response = GetResponse(imageUri, timeout);
                var pdfimage = FromStream(response.GetResponseStream(), configuration);        
                if (pdfimage.Equals(Null))
                {
                    Task.Delay(300);
                    
                    using var responseAlternative = GetResponse(imageUri, timeout);
                    pdfimage = FromStream(response.GetResponseStream(), configuration);
                    if (pdfimage.Equals(Null))
                    {
                        Task.Delay(500);
                        pdfimage = GetPdfImageByWebClient(imageUri);
                    }
                }
                
                result = pdfimage;
            }
            catch
            {
                return result;
            }

            return result;
        }

        #endregion

        #region public static async methods

        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from the specified uri with the specified format, optionally you can also specify a collection of effects to apply to the image.
        /// If the process of getting the image fails or the uri is wrong, <b>null</b> is returned.
        /// </summary>
        /// <param name="imageUri">Uri to image resource.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <param name="timeout">An image effects collection to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static async Task<PdfImage> FromUriAsync(Uri imageUri, PdfImageConfig configuration = null, int timeout = 15000)
        {
            SentinelHelper.ArgumentNull(imageUri, nameof(imageUri));

            PdfImage result = Null;

            var uriIsAccesibleResult = await imageUri.IsAccessibleAsync();
            if (!uriIsAccesibleResult.Success)
            {
                return result;
            }

            try
            {
                using var response = await GetResponseAsync(imageUri, timeout);

                var pdfimage = FromStream(response.GetResponseStream(), configuration);                
                if (pdfimage.Equals(Null))
                {
                    await Task.Delay(300);

                    using var responseAlternative = await GetResponseAsync(imageUri, timeout);
                    pdfimage = FromStream(responseAlternative.GetResponseStream(), configuration);                    
                    if (pdfimage.Equals(Null))
                    {
                        await Task.Delay(500);
                        pdfimage = await GetPdfImageByWebClientAsync(imageUri);
                    }
                }

                result = pdfimage;
            }
            catch
            {
                return result;
            }

            return result;
        }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a value that represents the hash code for this class.
        /// </summary>
        /// <returns>
        /// Hash code for this class.
        /// </returns>
        public override int GetHashCode() => 
            ToString().GetHashCode();

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString() => 
            Image == null 
                ? string.Empty
                : Image.ToString();

        #endregion

        #region private methods

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // free managed resources
            if (disposing)
            {
                Path = null;
                Image = null;
                OriginalImage?.Dispose();
                ProcessedImage?.Dispose();
            }

            // free native resources

            // avoid seconds calls 
            _isDisposed = true;
        }

        private void InitializeImage(ICloneable image)
        {
            OriginalImage = (NativeImage)image.Clone();

            var concreteOriginalImage = (Bitmap)image.Clone();
            if (Configuration.UseTransparentBackground)
            {
                if (Configuration.TransparentColor.Equals(PdfImageConfig.DefaultColor, StringComparison.OrdinalIgnoreCase))
                {
                    Configuration.SetParentImage(concreteOriginalImage);
                }

                concreteOriginalImage.MakeTransparent(Configuration.GetColor());
            }

            NativeImage processedImage = (NativeImage)concreteOriginalImage.Clone();
            if (Configuration.Effects != null)
            {
                processedImage = (NativeImage)concreteOriginalImage.ApplyEffects(Configuration.Effects).Clone();
            }

            ProcessedImage = (NativeImage)processedImage.Clone();
            ScaledHeight = processedImage.Height;
            ScaledWidth = processedImage.Width;

            Image = NativePdfImage.GetInstance(ProcessedImage, ImageFormat.Png);
        }

        #endregion

        #region private static methods

        private static HttpWebResponse GetResponse(Uri imageUri, int timeout = 15000)
        {
            var request = (HttpWebRequest)WebRequest.Create(imageUri);
            request.Timeout = timeout;
            request.ReadWriteTimeout = timeout;
            
            return (HttpWebResponse)request.GetResponse();
        }

        private static PdfImage GetPdfImageByWebClient(Uri imageUri)
        {
            PdfImage result = Null;

            try
            {                
                using var webClient = new WebClient();
                using var ms = new MemoryStream(webClient.DownloadData(imageUri));
                var bmp = NativeImage.FromStream(ms);
                
                return FromImage(bmp);
            }
            catch
            {
                return result;
            }
        }

        #endregion

        #region private static async methods

        private static async Task<HttpWebResponse> GetResponseAsync(Uri imageUri, int timeout = 15000)
        {
            var request = (HttpWebRequest) WebRequest.Create(imageUri);
            request.Timeout = timeout;
            request.ReadWriteTimeout = timeout;

            return (HttpWebResponse) await request.GetResponseAsync();
        }

        private static async Task<PdfImage> GetPdfImageByWebClientAsync(Uri imageUri)
        {
            PdfImage result = Null;

            try
            {
                using var webClient = new WebClient();
                using var ms = new MemoryStream(await webClient.DownloadDataTaskAsync(imageUri));
                var bmp = NativeImage.FromStream(ms);
                
                return FromImage(bmp);
            }
            catch
            {
                return result;
            }
        }

        #endregion
    }
}
