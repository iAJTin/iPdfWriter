
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

        /// <summary>
        /// Returns a new instance containing a default text content style settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextContent"/> reference containing the default text content style settings.
        /// </value>
        public new static PdfTextContent Default => new();

        #endregion

        #region public new readonly properties

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

        #region public new properties

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
            get => _alignment ??= PdfTextContentAlignment.Default;
            set
            {
                if (value != null)
                {
                    _alignment = value;
                }
            }
        }

        #endregion

        #region public override properties

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override bool IsDefault => 
            base.IsDefault && 
            Alignment.IsDefault;

        #endregion

        #region public new methods

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

        #region public virtual methods

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
    }
}
