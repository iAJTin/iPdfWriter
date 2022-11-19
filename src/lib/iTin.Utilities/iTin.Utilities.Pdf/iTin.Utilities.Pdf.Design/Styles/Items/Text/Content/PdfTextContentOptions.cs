
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Options;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfTextContent"/> instance.
    /// </summary>
    public class PdfTextContentOptions : BaseOptions, ICloneable
    {
        #region constructor/s

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
        /// Returns a new instance containing the set of available settings to model an existing <see cref="PdfTextContent"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextContentOptions"/> reference containing set of default options.
        /// </value>
        public static PdfTextContentOptions Default => new();

        #endregion

        #region public readonly properties

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

        #region public properties

        /// <summary>
        /// Gets or sets a value that defines style table alignment in an existing <see cref="PdfTextContent"/> instance.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model or a <see cref="PdfTextContentAlignmentOptions"/> instance.
        /// </value>
        [XmlElement]
        [JsonProperty("alignment")]
        public PdfTextContentAlignmentOptions Alignment { get; set; }

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
            Show == null && 
            Alignment == null;

        #endregion

        #region public methods

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
    }
}
