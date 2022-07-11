
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="PdfBaseStyle"/> class.<br/>
    /// Defines a <b>pdf</b> table style.
    /// </summary>
    public partial class PdfTableStyle
    {
        #region private field members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PdfTableContent _content;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PdfTableAlignment _alignment;
        #endregion

        #region public new readonly static properties

        #region [public] {new} {static} (PdfTableStyle) Default: Returns a new instance containing a default table style
        /// <summary>
        /// Returns a new instance containing a default table style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableStyle"/> reference containing the default table style settings.
        /// </value>
        public new static PdfTableStyle Default => new PdfTableStyle();
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
        public bool AlignmentSpecified => !Alignment.IsDefault;
        #endregion
        
        #endregion

        #region public new readonly properties

        #region [public] {new} (bool) ContentSpecified: Gets a value that tells the serializer if the referenced item is to be included
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
        public new bool ContentSpecified => !Content.IsDefault;
        #endregion

        #endregion

        #region public properties

        #region [public] (PdfTableAlignment) Alignment: Gets or sets table alignment
        /// <summary>
        /// Gets or sets table alignment.
        /// </summary>
        /// <value>
        /// Reference for table alignment.
        /// </value>
        [XmlElement]
        [JsonProperty("alignment")]
        public PdfTableAlignment Alignment
        {
            get => _alignment ?? (_alignment = PdfTableAlignment.Default);
            set => _alignment = value;
        }
        #endregion

        #endregion

        #region public new properties

        #region [public] {new} (PdfTableContent) Content: Gets or sets content distribution
        /// <summary>
        /// Gets or sets content distribution.
        /// </summary>
        /// <value>
        /// Reference for content distribution.
        /// </value>
        [XmlElement]
        [JsonProperty("content")]
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public new PdfTableContent Content
        {
            get
            {
                if (_content == null)
                {
                    _content = PdfTableContent.Default;
                }

                _content.SetParent(this);

                return _content;
            }
            set
            {
                if (value != null)
                {
                    _content = value;
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
        public override bool IsDefault => base.IsDefault && Alignment.IsDefault && Content.IsDefault;
        #endregion

        #endregion

        #region public new methods

        #region [public] {new} (PdfTableStyle) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfTableStyle Clone()
        {
            var cloned = (PdfTableStyle)base.Clone();
            cloned.Content = Content.Clone();
            cloned.Borders = Borders.Clone();
            cloned.Alignment = Alignment.Clone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(PdfTableStyleOptions): Apply specified options to this style
        /// <summary>
        /// Apply specified options to this style.
        /// </summary>
        public virtual void ApplyOptions(PdfTableStyleOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Alignment
            Alignment.ApplyOptions(options.Alignment);
            #endregion

            #region Content
            Content.ApplyOptions(options.Content);
            #endregion
        }
        #endregion

        #region [public] {virtual} (void) Combine(PdfTableStyle): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfTableStyle reference)
        {
            if (reference == null)
            {
                return;
            }

            base.Combine(reference);

            Content.Combine(reference.Content);
            Alignment.Combine(reference.Alignment);
        }
        #endregion

        #endregion
    }
}
