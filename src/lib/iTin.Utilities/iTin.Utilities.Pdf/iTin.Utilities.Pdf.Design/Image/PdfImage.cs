
namespace iTin.Utilities.Pdf.Design.Image
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Net;
    using System.Threading;
    
    using iTin.Core;
    using iTin.Core.Drawing;
    using iTin.Core.Drawing.ComponentModel;
    using iTin.Core.Helpers;

    using NativeImage = System.Drawing.Image;
    using NativePdfImage = iTextSharp.text.Image;

    using iTinIO = iTin.Core.IO;

    /// <summary>
    /// Defines a <b>pdf</b> image object.
    /// </summary>
    public class PdfImage : IEquatable<PdfImage>, IDisposable
    {
        #region public static readonly members

        #region [public] {static} (PdfImage) Null: Gets a reference indicating that this instance is not valid
        /// <summary>
        /// Gets a reference indicating that this instance is not valid.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImage"/> reference.
        /// </value>
        public static readonly PdfImage Null = new PdfImage {IsValid = false};
        #endregion

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

        #region [internal] PdfImage(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImage"/> class.
        /// </summary>
        internal PdfImage()
        {
        }
        #endregion

        #region [internal] PdfImage(string, PdfImageConfig = null): Initializes a new instance of the class with a image path and optional image configuration
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImage"/> class with a image path and optional image configuration.
        /// </summary>
        /// <param name="imagePath">A reference to image path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.</param>
        /// <param name="configuration">Image configuration reference.</param>
        internal PdfImage(string imagePath, PdfImageConfig configuration = null) : this(NativeImage.FromFile(iTinIO.Path.PathResolver(imagePath)), configuration)
        {
            Path = iTinIO.File.ToUri(iTinIO.Path.PathResolver(imagePath));
        }
        #endregion

        #region [internal] PdfImage(NativeImage, PdfImageConfig = null): Initializes a new instance of the class from image with optional image configuration
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

            Path = null;
            Configuration = safeConfiguration.Clone(); 
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

            IsValid = true;
        }
        #endregion

        #endregion

        #region finalizer

        #region [~] PdfImage(): Finalizer
        /// <summary>
        /// Finalizer
        /// </summary>
        ~PdfImage()
        {
            Dispose(false);
        }
        #endregion

        #endregion

        #region interfaces

        #region IDisposable

        #region public methods

        #region [public] (void) Dispose(): Clean managed resources
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

        #endregion

        #region IEquatable

        #region public methods

        #region [public] (bool) Equals(PdfImage): Indicates whether the current object is equal to another object of the same type
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <b>true</b> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <b>false</b>.
        /// </returns>
        public bool Equals(PdfImage other) => other.Equals((object)this);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static operators

        #region [public] {static} (bool) operator ==(PdfImage, PdfImage): Implements the equality operator (==). Check if two objects PdfImage are equal
        /// <summary>
        /// Implements the equality operator (==). Check if two objects <see cref="PdfImage"/> are equal.
        /// </summary>
        /// <param name="left">Left part of equality. </param>
        /// <param name="right">Right part of equality. </param>
        /// <returns>
        /// <b>true</b> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <b>false</b>.
        /// </returns>
        public static bool operator ==(PdfImage left, PdfImage right) => left.Equals(right);
        #endregion

        #region [public] {static} (bool) operator !=(PdfImage, PdfImage): Implements the inequality operator (==). Check if two objects PdfImage are not equal
        /// <summary>
        /// Implements the inequality operator (!=). Check if two objects <see cref="PdfImage"/> are not equal.
        /// </summary>
        /// <param name="left">Left part of equality. </param>
        /// <param name="right">Right part of equality. </param>
        /// <returns>
        /// <b>true</b> if <paramref name="left"/> not equal to <paramref name="right"/>; otherwise, <b>false</b>.
        /// </returns>
        public static bool operator !=(PdfImage left, PdfImage right) => !left.Equals(right);
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (PdfImageConfig) Configuration: Gets a reference to image configuration information
        /// <summary>
        /// Gets a reference to image configuration information.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageConfig"/> that contains the configuration information.
        /// </value>
        public PdfImageConfig Configuration { get; }
        #endregion

        #region [public] (NativePdfImage) Image: Gets a reference to pdf image object
        /// <summary>
        /// Gets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="iTextSharp.text.Image"/> object that contains a reference to pdf image object.
        /// </value>
        public NativePdfImage Image { get; private set; }
        #endregion

        #region [public] (bool) IsValid: Gets or sets a value indicating whether the current instance is valid
        /// <summary>
        /// Gets or sets a value indicating whether the current instance is valid.
        /// </summary>
        /// <value>
        /// <b>true</b> if current instance is valid; otherwise <b>false</b>.
        /// </value>
        public bool IsValid { get; private set; }
        #endregion

        #region [public] (NativeImage) OriginalImage: Gets a reference to original image
        /// <summary>
        /// Gets a reference to original image.
        /// </summary>
        /// <value>
        /// A <see cref="System.Drawing.Image"/> object that contains a reference to pdf image object.
        /// </value>
        public NativeImage OriginalImage { get; }
        #endregion

        #region [public] (Uri) Path: Gets a reference to image uri
        /// <summary>
        /// Gets a reference to image uri.
        /// </summary>
        /// <value>
        /// A <see cref="Uri"/> that contains the image uri.
        /// </value>
        public Uri Path { get; }
        #endregion

        #region [public] (NativeImage) ProcessedImage: Gets a reference to the original image after it has been processed
        /// <summary>
        /// Gets a reference to the original image after it has been processed.
        /// </summary>
        /// <value>
        /// A <see cref="System.Drawing.Image"/> object that contains a reference to pdf image object.
        /// </value>
        public NativeImage ProcessedImage { get; private set; }
        #endregion

        #region [public] (float) ScaledHeight: Gets a value containing scaled height of this image
        /// <summary>
        /// Gets a value containing scaled height of this image.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> containing scaled height of this image.
        /// </value>
        public float ScaledHeight { get; private set; }
        #endregion

        #region [public] (float) ScaledWidth: Gets a value containing scaled width of this image
        /// <summary>
        /// Gets a value containing scaled width of this image.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> containing scaled width of this image.
        /// </value>
        public float ScaledWidth { get; private set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfImage) ApplyEffects(EffectType[]): Apply effects image to this instance
        /// <summary>
        /// Apply effects image to this instance.
        /// </summary>
        /// <param name="effects">the dimensions to fit</param>
        /// <returns>
        /// Returns this instance with effects.
        /// </returns>
        public PdfImage ApplyEffects(EffectType[] effects)
        {
            if (this.Equals(PdfImage.Null))
            {
                return this;
            }

            if (Image == null)
            {
                return this;
            }

            NativeImage image = 
                Configuration.Effects == null
                    ? NativeImage.FromStream(OriginalImage.AsStream())
                    : NativeImage.FromStream(OriginalImage.ApplyEffects(Configuration.Effects).AsStream());

            ProcessedImage = (NativeImage)image.ApplyEffects(effects).Clone();
            Image = NativePdfImage.GetInstance(ProcessedImage.AsStream());

            return this;
        }
        #endregion

        #region [public] (PdfImage) ScalePercent(float): Scale the image to a certain percentage
        /// <summary>
        /// Scale the image to a certain percentage.
        /// </summary>
        /// <param name="percent">the scaling percentage</param>
        /// <returns>
        /// Returns this instance scaled to percent value.
        /// </returns>
        public PdfImage ScalePercent(float percent) => ScalePercent(percent, percent);
        #endregion

        #region [public] (PdfImage) ScalePercent(SizeF): Scale the width and height of an image to a certain percentage
        /// <summary>
        /// Scale the width and height of an image to a certain percentage.
        /// </summary>
        /// <param name="size">the scaling percentage of the width and height</param>
        /// <returns>
        /// Returns this instance scaled to percent value.
        /// </returns>
        public PdfImage ScalePercent(SizeF size) => ScalePercent(size.Width, size.Height);
        #endregion

        #region [public] (PdfImage) ScalePercent(float, float): Scale the width and height of an image to a certain percentage
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
            if (this.Equals(PdfImage.Null))
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
        #endregion

        #region [public] (PdfImage) ScaleToFit(float, ScaleStrategy = ScaleStrategy.Auto): Scale this image proportionally until at most indicate the value
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
            if (Equals(PdfImage.Null))
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
        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (PdfImage) FromByteArray(byte[], PdfImageConfig = null): Creates a new PdfImage object from specified byte array, format and optional image effect collection
        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified byte array, format and optional image effect collection.
        /// </summary>
        /// <param name="array">Image as byte array.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromByteArray(byte[] array, PdfImageConfig configuration = null) => FromStream(array.ToMemoryStream(), configuration);
        #endregion

        #region [public] {static} (PdfImage) FromFile(string, PdfImageConfig = null) Creates a new PdfImage object from specified image path and optional image configuration
        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified image path and optional image configuration.
        /// </summary>
        /// <param name="imagePath">Image path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromFile(string imagePath, PdfImageConfig configuration = null) => new PdfImage(imagePath, configuration);
        #endregion

        #region [public] {static} (PdfImage) FromImage(NativeImage, PdfImageConfig = null): Creates a new PdfImage object from specified image, format and optional image effect collection
        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified image, format and optional image effect collection.
        /// </summary>
        /// <param name="image">Image reference.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromImage(NativeImage image, PdfImageConfig configuration = null) => new PdfImage(image, configuration);
        #endregion

        #region [public] {static} (PdfImage) FromStream(Stream, PdfImageConfig = null): Creates a new PdfImage object from specified stream, format and optional image effect collection
        /// <summary>
        /// Creates a new <see cref="PdfImage"/> object from specified stream, format and optional image effect collection.
        /// </summary>
        /// <param name="stream">Image as stream.</param>
        /// <param name="configuration">An image configuration to apply.</param>
        /// <returns>
        /// A new <see cref="PdfImage"/> reference represents image.
        /// </returns>
        public static PdfImage FromStream(Stream stream, PdfImageConfig configuration = null) => FromImage(NativeImage.FromStream(stream), configuration);

        #endregion

        #region [public] {static} (PdfImage) FromUri(Uri imageUri, PdfImageConfig = null, int = 5000): Creates a new PdfImage object from specified uri with the specified format, optionally you can also specify a collection of effects to apply to the image
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

            PdfImage result = PdfImage.Null;

            bool uriIsAccesible = imageUri.IsAccessible();
            if (!uriIsAccesible)
            {
                return result;
            }

            try
            {
                PdfImage pdfimage;
                using (var response = GetResponse(imageUri, timeout))
                {
                    pdfimage = FromStream(response.GetResponseStream(), configuration);
                }

                if (pdfimage == PdfImage.Null)
                {
                    Thread.Sleep(300);
                    using (var response = GetResponse(imageUri, timeout))
                    {
                        pdfimage = FromStream(response.GetResponseStream(), configuration);
                    }

                    if (pdfimage == PdfImage.Null)
                    {
                        Thread.Sleep(500);
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

        #endregion

        #region public override methods

        #region [public] {override} (bool) Equals(object): Returns a value that indicates whether this class is equal to another
        /// <summary>
        /// Returns a value that indicates whether this class is equal to another
        /// </summary>
        /// <param name="obj">Class with which to compare.</param>
        /// <returns>
        /// Results equality comparison.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (!(obj is PdfImage))
            {
                return false;
            }

            var other = (PdfImage)obj;

            return
                other.Path == Path && 
                other.IsValid == IsValid && 
                other.ToString().Equals(ToString());
        }
        #endregion

        #region [public] {override} (int) GetHashCode(): Returns a value that represents the hash code for this class
        /// <summary>
        /// Returns a value that represents the hash code for this class.
        /// </summary>
        /// <returns>
        /// Hash code for this class.
        /// </returns>
        public override int GetHashCode() => ToString().GetHashCode();
        #endregion

        #region [public] {override} (string) ToString(): Returns a string than represents the current object.
        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents this instance.
        /// </returns>
        public override string ToString() => Image == null ? string.Empty : Image.ToString();
        #endregion

        #endregion

        #region protected virtual methods

        #region [protected] {virtual} (void) Dispose(bool): Cleans managed and unmanaged resources
        /// <summary>
        /// Cleans managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// If it is <b>true</b>, the method is invoked directly or indirectly from the user code.
        /// If it is <b>false</b>, the method is called the finalizer and only unmanaged resources are finalized.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // free managed resources
            if (disposing)
            {
                Image = null;
                OriginalImage?.Dispose();
                ProcessedImage?.Dispose();
            }

            // free native resources

            // avoid seconds calls 
            _isDisposed = true;
        }
        #endregion

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
            PdfImage result = PdfImage.Null;

            try
            {
                NativeImage bmp;
                using (var webClient = new WebClient())
                using (var ms = new MemoryStream(webClient.DownloadData(imageUri)))
                {
                    bmp = NativeImage.FromStream(ms);
                }

                return PdfImage.FromImage(bmp);
            }
            catch
            {
                return result;
            }
        }

        #endregion
    }
}

//#region [public] (PdfImage) ScaleToFit(NativePdfRectangle): Scales this image so to the dimensions of the rectangle
///// <summary>
///// Scales the images to the dimensions of the rectangle.
///// </summary>
///// <param name="rectangle">the dimensions to fit</param>
///// <returns>
///// Returns this instance scaled.
///// </returns>
//public PdfImage ScaleToFit(NativePdfRectangle rectangle) => ScaleToFit(rectangle.Width, rectangle.Height);
//#endregion

//#region [public] (PdfImage) ScaleToFit(SizeF): Scales this image so to the dimensions of the size
///// <summary>
///// Scales the images to the dimensions of the size.
///// </summary>
///// <param name="size">the scaling percentage of the size</param>
///// <returns>
///// Returns this instance scaled to percent value.
///// </returns>
//public PdfImage ScaleToFit(SizeF size) => ScaleToFit(size.Width, size.Height);
//#endregion

//#region [public] (PdfImage) ScaleToFit(float, float): Scales this image so that it fits a certain width and height
///// <summary>
///// Scales this image so that it fits a certain width and height.
///// </summary>
///// <param name="width">the width to fit</param>
///// <param name="height">the height to fit</param>
///// <returns>
///// Returns this instance scaled.
///// </returns>
//public PdfImage ScaleToFit(float width, float height)
//{
//    if (this.Equals(PdfImage.Null))
//    {
//        return this;
//    }

//    if (Image == null)
//    {
//        return this;
//    }

//    _hasScaledPercent = false;
//    _hasScaledFit = true;
//    _scaleX = width;
//    _scaleY = height;

//    var original = (NativeImage) OriginalImage.Clone();
//    var originalWithEffects = original;
//    if (Configuration.Effects != null)
//    {
//        originalWithEffects = original.ApplyEffects(Configuration.Effects);
//    }

//    ProcessedImage = (NativeImage)originalWithEffects.ScaleToFit(width, height).Clone();

//    return this;
//}
//#endregion
