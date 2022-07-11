
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseBasicContent"/> class.<br/>
    /// Defines a <b>pdf</b> text content.
    /// </summary>
    public partial class PdfTextContent
    {
        #region private field members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PdfTextContentAlignment _alignment;
        #endregion

        #region public new readonly static properties

        #region [public] {new} {static} (PdfTextContent) Default: Returns a new instance containing a default text content style settings
        /// <summary>
        /// Returns a new instance containing a default text content style settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextContent"/> reference containing the default text content style settings.
        /// </value>
        public new static PdfTextContent Default => new PdfTextContent();
        #endregion

        #endregion

        #region public new readonly properties

        #region [public] {new} (bool) AlignmentSpecified: Gets a value that tells the serializer if the referenced item is to be included
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
        public new bool AlignmentSpecified => !Alignment.IsDefault;
        #endregion

        #endregion

        #region public new properties

        #region [public] {new} (PdfTextContentAlignment) Alignment: Gets or sets content distribution
        /// <summary>
        /// Gets or sets content distribution.
        /// </summary>
        /// <value>
        /// Reference for content distribution.
        /// </value>
        [XmlElement]
        [JsonProperty("font")]
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public new PdfTextContentAlignment Alignment
        {
            get => _alignment ?? (_alignment = PdfTextContentAlignment.Default);
            set
            {
                if (value != null)
                {
                    _alignment = value;
                }
            }
        }
        #endregion

        #endregion

        #region public override properties

        #region [public] {override} (bool) IsDefault: Gets a value indicating whether this instance is default
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override bool IsDefault => base.IsDefault && Alignment.IsDefault;
        #endregion

        #endregion

        #region public new methods

        #region [public] {new} (PdfTextContent) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfTextContent Clone()
        {
            var cloned = (PdfTextContent)base.Clone();
            cloned.Alignment = Alignment.Clone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(PdfTextContentOptions): Apply specified options to this alignment
        /// <summary>
        /// Apply specified options to this alignment.
        /// </summary>
        public virtual void ApplyOptions(PdfTextContentOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Color
            string colorOption = options.Color;
            bool colorHasValue = !colorOption.IsNullValue();
            if (colorHasValue)
            {
                Color = colorOption;
            }
            #endregion

            #region Show
            YesNo? showOption = options.Show;
            bool showHasValue = showOption.HasValue;
            if (showHasValue)
            {
                Show = showOption.Value;
            }
            #endregion

            #region Alignment
            Alignment.ApplyOptions(options.Alignment);
            #endregion
        }
        #endregion

        #region [public] {virtual} (void) Combine(PdfTextContent): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfTextContent reference)
        {
            if (reference == null)
            {
                return;
            }

            base.Combine(reference);

            Alignment.Combine(reference.Alignment);
        }
        #endregion

        #endregion
    }
}
