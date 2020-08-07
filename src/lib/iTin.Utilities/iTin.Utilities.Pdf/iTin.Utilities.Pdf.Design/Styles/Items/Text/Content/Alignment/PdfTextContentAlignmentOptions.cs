
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Models.Design.Enums;
    using iTin.Core.Models.Design.Options;

    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfTextContentAlignment"/> instance.
    /// </summary>
    public class PdfTextContentAlignmentOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        #region [public] PdfTextContentAlignmentOptions(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTextContentAlignmentOptions"/> class.
        /// </summary>
        public PdfTextContentAlignmentOptions()
        {
            Vertical = null;
            Horizontal = null;
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

        #region [public] {static} (PdfTextContentAlignmentOptions) Default: Returns a new instance containing the set of available settings to model an existing PdfTextContentAlignment instance
        /// <summary>
        /// Returns a new instance containing the set of available settings to model an existing <see cref="PdfTextContentAlignment"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextContentAlignmentOptions"/> reference containing set of default options.
        /// </value>
        public static PdfTextContentAlignmentOptions Default => new PdfTextContentAlignmentOptions();
        #endregion

        #endregion

        #region public properties

        #region [public] (KnownHorizontalAlignment?) Horizontal: Gets or sets the preferred horizontal alignment in an existing PdfTextContentAlignment instance
        /// <summary>
        /// Gets or sets the preferred horizontal alignment in an existing <see cref="PdfTextContentAlignment"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, One of the enumeration values <see cref="KnownHorizontalAlignment"/>. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("horizontal")]
        public KnownHorizontalAlignment? Horizontal { get; set; }
        #endregion

        #region [public] (KnownVerticalAlignment?) Vertical: Gets or sets the preferred vertical alignment in an existing PdfTableAlignment instance
        /// <summary>
        /// Gets or sets the preferred vertical alignment in an existing <see cref="PdfTextContentAlignment"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, One of the enumeration values <see cref="KnownVerticalAlignment"/>. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("vertical")]
        public KnownVerticalAlignment? Vertical { get; set; }
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
        public override bool IsDefault => base.IsDefault && Vertical == null && Horizontal == null;
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfTextContentAlignmentOptions) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTextContentAlignmentOptions Clone() => (PdfTextContentAlignmentOptions) MemberwiseClone();
        #endregion

        #endregion
    }
}
