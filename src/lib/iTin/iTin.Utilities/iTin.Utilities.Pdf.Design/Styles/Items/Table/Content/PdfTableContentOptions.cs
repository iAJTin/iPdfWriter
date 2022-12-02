
using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Options;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfTableContent"/> instance.
    /// </summary>
    public class PdfTableContentOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTableContentOptions"/> class.
        /// </summary>
        public PdfTableContentOptions()
        {
            Color = null;
            Show = null;
        }

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

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

        #region public static properties

        /// <summary>
        /// Gets a reference that contains the set of available settings to model an existing <see cref="PdfTableContent"/> instance.
        /// </summary>
        /// <value>
        /// Set of default options.
        /// </value>
        public static PdfTableContentOptions Default => new();

        #endregion

        #region public override readonly properties

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault => 
            base.IsDefault && 
            Color == null && 
            Show == null;

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the preferred content color in an existing <see cref="PdfTableContent"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred border color.
        /// </value>
        [XmlAttribute]
        [JsonProperty("color")]
        public string Color { get; set; }

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

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTableContentOptions Clone() => (PdfTableContentOptions)MemberwiseClone();

        #endregion
    }
}
