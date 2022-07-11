
using System;
using System.ComponentModel;
using System.Diagnostics;

using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Drawing.Helpers;

using NativeDrawing = System.Drawing;

namespace iTin.Utilities.Pdf.Design.Image
{
    /// <summary>
    /// Represents configuration information for an object <see cref="PdfImage"/>.
    /// </summary>
    public sealed class PdfImageConfig : ICloneable
    {
        #region public constants
        /// <summary>
        /// Defines the default color name for transparency background.
        /// </summary>
        public const string DefaultColor = "Autodetect";
        #endregion

        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string DefaultTransparentColor = "Transparent";
        #endregion

        #region public static members
        /// <summary>
        /// Defaults configuration. Transparent background is not used.
        /// </summary>
        public static readonly PdfImageConfig Default = new PdfImageConfig();
        #endregion

        #region constructor/s

        #region [public] PdfImageConfig(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImageConfig"/> class.
        /// </summary>
        public PdfImageConfig()
        {
            Effects = null;
            TransparentColor = DefaultColor;
            UseTransparentBackground = false;
        }
        #endregion

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

        #region [private] (object) Clone(): Creates a new object that is a copy of the current instance
        /// <inheritdoc />
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone() => Clone();
        #endregion

        #endregion

        #endregion

        #endregion

        #region public properties

        #region [public] (EffectType[]) Effects: Gets or set a value containing a collection of effects to apply to the image before inserting into the document
        /// <summary>
        /// Gets or set a value containing a collection of effects to apply to the image before inserting into the document.
        /// </summary>
        /// <value>
        /// An image effects collection to apply.
        /// </value>
        public EffectType[] Effects { get; set; }
        #endregion

        #region [public] (string) TransparentColor: Gets or sets preferred of transparent color
        /// <summary>
        /// Gets or sets preferred of transparent color. The default is <b>Autodetect</b>.
        /// </summary>
        /// <value>
        /// Preferred transparent color. 
        /// </value>
        [DefaultValue(DefaultColor)]
        public string TransparentColor { get; set; }
        #endregion

        #region [public] (bool) UseTransparentBackground: Gets or sets a value indicating whether transparent background is used
        /// <summary>
        /// Gets or sets a value indicating whether transparent background is used.
        /// </summary>
        /// <value>
        /// <b>true</b> if use transparent background; otherwise, <b>false</b>.
        /// </value>
        public bool UseTransparentBackground { get; set; }
        #endregion

        #endregion

        #region private properties

        #region [internal] (NativeDrawing.Image) ImageReference: Gets or set a value containing the image which acts as parent
        /// <summary>
        /// Gets or set a value containing the <see cref="Image"/> reference which acts as parent
        /// </summary>
        /// <value>
        /// An <see cref="Image"/> reference.
        /// </value>
        private NativeDrawing.Image ImageReference { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfImageConfig) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfImageConfig Clone()
        {
            var styleCloned = (PdfImageConfig)MemberwiseClone();
            if (Effects != null)
            {
                styleCloned.Effects = (EffectType[]) Effects.Clone();
            }

            return styleCloned;
        }
        #endregion

        #region [public] (NativeDrawing.Color) GetColor(): Gets a reference to the color structure preferred for transparent background
        /// <summary>
        /// Gets a reference to the <see cref="NativeDrawing.Color"/> structure preferred for transparent background.
        /// </summary>
        /// <returns>
        /// <see cref="NativeDrawing.Color"/> structure that represents a .NET color.
        /// </returns> 
        public NativeDrawing.Color GetColor()
        {
            if (ImageReference == null)
            {
                return ColorHelper.GetColorFromString(string.IsNullOrEmpty(TransparentColor) ? DefaultTransparentColor : TransparentColor);
            }

            // Get the color of a background pixel.
            return ((NativeDrawing.Bitmap) ImageReference).GetPixel(1, 1);
        }
        #endregion

        #endregion

        #region internal methods

        #region [internal] (void) SetParentImage(NativeDrawing.Image): Sets the reference to parent image
        /// <summary>
        /// Sets the reference to parent <see cref="NativeDrawing.Image"/>.
        /// </summary>
        /// <param name="parent">An <see cref="NativeDrawing.Image"/> reference</param>
        internal void SetParentImage(NativeDrawing.Image parent)
        {
            ImageReference = parent;
        }
        #endregion

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a string than represents the current instance
        /// <summary>
        /// Returns a string that represents the current instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current instance.
        /// </returns>
        public override string ToString() => $"UseTransparentBackground = {UseTransparentBackground}";
        #endregion

        #endregion
    }
}
