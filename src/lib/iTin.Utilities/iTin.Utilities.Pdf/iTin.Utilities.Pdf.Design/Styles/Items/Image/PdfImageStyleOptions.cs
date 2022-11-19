
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Options;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseOptions"/> class.<br/>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="PdfImageStyle"/> instance.
    /// </summary>
    public class PdfImageStyleOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfImageStyleOptions"/> class.
        /// </summary>
        public PdfImageStyleOptions()
        {
            Content = PdfImageContentOptions.Default;
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
        /// Returns a new instance containing the set of available settings to model an existing <see cref="PdfImageStyle"/> instance.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyleOptions"/> reference containing set of default options.
        /// </value>
        public static PdfImageStyleOptions Default => new();

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
        public bool ContentSpecified => Content != null;

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value that defines an image style in an existing <see cref="PdfImageStyle"/> instance.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model or a <see cref="PdfImageContentOptions"/> instance.
        /// </value>
        [XmlElement]
        [JsonProperty("content")]
        public PdfImageContentOptions Content { get; set; }

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
            Content == null;

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfImageStyleOptions Clone()
        {
            var cloned = (PdfImageStyleOptions) MemberwiseClone();

            if (Content != null)
            {
                cloned.Content = Content.Clone();
            }

            return cloned;
        } 

        #endregion
    }
}
