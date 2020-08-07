
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Models.Design.Options;

    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfTextStyle"/> instance.
    /// </summary>
    public class PdfTextStyleOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        #region [public] PdfTextStyleOptions(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTextStyleOptions"/> class.
        /// </summary>
        public PdfTextStyleOptions()
        {
            Font = FontOptions.Default;
            Content = PdfTextContentOptions.Default;
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

        #region public static properties

        #region [public] {static} (PdfTextStyleOptions) Default: Returns a new instance containing the set of available settings to model an existing PdfTextStyle instance
        /// <summary>
        /// Returns a new instance containing the set of available settings to model an existing <see cref="PdfTextStyle"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextStyleOptions"/> reference containing set of default options.
        /// </value>
        public static PdfTextStyleOptions Default => new PdfTextStyleOptions();
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (bool) FontSpecified: Gets a value that tells the serializer if the referenced item is to be included
        /// <summary>
        /// Gets a value that tells the serializer if the referenced item is to be included.
        /// </summary>
        /// <value>
        /// <b>true</b> if the serializer has to include the element; otherwise, <b>false</b>.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        [Browsable(false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool FontSpecified => Font != null;
        #endregion

        #region [public] (bool) ContentSpecified: Gets a value that tells the serializer if the referenced item is to be included
        /// <summary>
        /// Gets a value that tells the serializer if the referenced item is to be included.
        /// </summary>
        /// <value>
        /// <b>true</b> if the serializer has to include the element; otherwise, <b>false</b>.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        [Browsable(false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool ContentSpecified => Content != null;
        #endregion

        #endregion

        #region public properties

        #region [public] (FontOptions) Font: Gets or sets a value that defines font style in an existing PdfTextStyle instance.
        /// <summary>
        /// Gets or sets a value that defines style table content in an existing <see cref="PdfTextStyle"/> instance.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model or a <see cref="FontOptions"/> instance.
        /// </value>
        [XmlElement]
        [JsonProperty("font")]
        public FontOptions Font { get; set; }
        #endregion

        #region [public] (PdfTextContentOptions) Content: Gets or sets a value that defines style table content in an existing PdfTextStyle instance.
        /// <summary>
        /// Gets or sets a value that defines style table content in an existing <see cref="PdfTextStyle"/> instance.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model or a <see cref="PdfTextContentOptions"/> instance.
        /// </value>
        [XmlElement]
        [JsonProperty("content")]
        public PdfTextContentOptions Content { get; set; }
        #endregion

        #endregion

        #region public override readonly properties

        #region [public] {override} (bool) IsDefault: Gets a value indicating whether this instance is default
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault => base.IsDefault && Font == null && Content == null;
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfTextStyleOptions) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTextStyleOptions Clone()
        {
            var cloned = (PdfTextStyleOptions) MemberwiseClone();

            if (Font != null)
            {
                cloned.Font = Font.Clone();
            }

            if (Content != null)
            {
                cloned.Content = Content.Clone();
            }

            return cloned;
        } 
        #endregion

        #endregion
    }
}
