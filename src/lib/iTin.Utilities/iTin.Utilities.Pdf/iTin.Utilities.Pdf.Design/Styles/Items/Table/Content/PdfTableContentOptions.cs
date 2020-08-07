
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Models.Design.Enums;
    using iTin.Core.Models.Design.Options;

    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfTableContent"/> instance.
    /// </summary>
    public class PdfTableContentOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        #region [public] PdfTableContentOptions(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTableContentOptions"/> class.
        /// </summary>
        public PdfTableContentOptions()
        {
            Color = null;
            Show = null;
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

        #region [public] {static} (PdfTableContentOptions) Default: Gets a reference that contains the set of available settings to model an existing PdfTableContent instance
        /// <summary>
        /// Gets a reference that contains the set of available settings to model an existing <see cref="PdfTableContent"/> instance.
        /// </summary>
        /// <value>
        /// Set of default options.
        /// </value>
        public static PdfTableContentOptions Default => new PdfTableContentOptions();
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
        public override bool IsDefault => base.IsDefault && Color == null && Show == null;
        #endregion

        #endregion

        #region public properties

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

        #region public methods

        #region [public] (PdfTableContentOptions) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTableContentOptions Clone() => (PdfTableContentOptions)MemberwiseClone();
        #endregion

        #endregion
    }
}
