
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Models.Design.Enums;
    using iTin.Core.Models.Design.Options;

    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfTextContent"/> instance.
    /// </summary>
    public class PdfTextContentOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        #region [public] PdfTextContentOptions(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTextContentOptions"/> class.
        /// </summary>
        public PdfTextContentOptions()
        {
            Color = null;
            Show = null;
            Alignment = null;
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

        #region [public] {static} (PdfTextContentOptions) Default: Returns a new instance containing the set of available settings to model an existing PdfTextContent instance
        /// <summary>
        /// Returns a new instance containing the set of available settings to model an existing <see cref="PdfTextContent"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextContentOptions"/> reference containing set of default options.
        /// </value>
        public static PdfTextContentOptions Default => new PdfTextContentOptions();
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (bool) AlignmentSpecified: Gets a value that tells the serializer if the referenced item is to be included
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
        public bool AlignmentSpecified => Alignment != null;
        #endregion

        #endregion

        #region public properties

        #region [public] (PdfTextContentAlignmentOptions) Alignment: Gets or sets a value that defines style table alignment in an existing PdfTextContent instance.
        /// <summary>
        /// Gets or sets a value that defines style table alignment in an existing <see cref="PdfTextContent"/> instance.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model or a <see cref="PdfTextContentAlignmentOptions"/> instance.
        /// </value>
        [XmlElement]
        [JsonProperty("alignment")]
        public PdfTextContentAlignmentOptions Alignment { get; set; }
        #endregion

        #region [public] (string) Color: Gets or sets the preferred content color in an existing PdfTableContent instance
        /// <summary>
        /// Gets or sets the preferred content color in an existing <see cref="PdfTableContent"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred border color.
        /// </value>
        [XmlAttribute]
        [JsonProperty("color")]
        public string Color { get; set; }
        #endregion

        #region [public] (YesNo?) Show: Gets or sets a value that indicates whether an existing PdfTableContent instance is displayed
        /// <summary>
        /// Gets or sets a value that indicates whether an existing <see cref="PdfTableContent"/> instance is displayed. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if the instance is displayed or <b>YesNo.No</b> if the instance is not displayed. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("show")]
        public YesNo? Show { get; set; }
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
        public override bool IsDefault => base.IsDefault && Color == null && Show == null && Alignment == null;
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfTextContentOptions) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTextContentOptions Clone()
        {
            var cloned = (PdfTextContentOptions) MemberwiseClone();
           
            if (Alignment != null)
            {
                cloned.Alignment = Alignment.Clone();
            }

            return cloned;
        } 
        #endregion

        #endregion
    }
}
