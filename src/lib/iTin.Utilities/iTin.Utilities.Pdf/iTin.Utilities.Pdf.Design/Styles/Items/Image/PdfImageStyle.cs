
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Models.Design.Enums;

    /// <summary>
    /// A Specialization of <see cref="PdfBaseStyle"/> class.<br/>
    /// Defines a <b>pdf</b> image style.
    /// </summary>
    public partial class PdfImageStyle
    {
        #region private field members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PdfImageContent _content;
        #endregion

        #region public readonly static properties

        #region [public] {static} (PdfImageStyle) Center: Returns a center image style
        /// <summary>
        /// Returns a center image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing center image style settings.
        /// </value>
        public static PdfImageStyle Center => new PdfImageStyle
        {
            Content =
            {
                Alignment =
                {
                    Horizontal = KnownHorizontalAlignment.Center
                }
            }
        };

        #endregion

        #region [public] {static} (PdfImageStyle) Left: Returns a left image style
        /// <summary>
        /// Returns a left image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing left image style settings.
        /// </value>
        public static PdfImageStyle Left => new PdfImageStyle
        {
            Content =
            {
                Alignment =
                {
                    Horizontal = KnownHorizontalAlignment.Left
                }
            }
        };

        #endregion

        #region [public] {static} (PdfImageStyle) Right: Returns a right image style
        /// <summary>
        /// Returns a right image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing right image style settings.
        /// </value>
        public static PdfImageStyle Right => new PdfImageStyle
        {
            Content =
            {
                Alignment =
                {
                    Horizontal = KnownHorizontalAlignment.Right
                }
            }
        };
        #endregion

        #endregion

        #region public new readonly static properties

        #region [public] {new} {static} (PdfImageStyle) Default: Returns a new instance containing a default image style
        /// <summary>
        /// Returns a new instance containing a default image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing the default image style settings.
        /// </value>
        public new static PdfImageStyle Default => new PdfImageStyle();
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

        #region public new properties

        #region [public] {new} (PdfImageContent) Content: Gets or sets content distribution
        /// <summary>
        /// Gets or sets content distribution.
        /// </summary>
        /// <value>
        /// Reference for content distribution.
        /// </value>
        [XmlElement]
        [JsonProperty("content")]
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public new PdfImageContent Content
        {
            get
            {
                if (_content == null)
                {
                    _content = PdfImageContent.Default;
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
        public override bool IsDefault => base.IsDefault && Content.IsDefault;
        #endregion

        #endregion

        #region public new methods

        #region [public] {new} (PdfImageStyle) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfImageStyle Clone()
        {
            var cloned = (PdfImageStyle)base.Clone();
            cloned.Content = Content.Clone();
            cloned.Borders = Borders.Clone();
            cloned.Properties = Properties.Clone();
            
            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(PdfImageStyleOptions): Apply specified options to this style
        /// <summary>
        /// Apply specified options to this style.
        /// </summary>
        public virtual void ApplyOptions(PdfImageStyleOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }
            
            #region Content
            Content.ApplyOptions(options.Content);
            #endregion
        }
        #endregion

        #region [public] {virtual} (void) Combine(PdfImageStyle): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfImageStyle reference)
        {
            if (reference == null)
            {
                return;
            }

            base.Combine(reference);

            Content.Combine(reference.Content);
        }
        #endregion

        #endregion
    }
}
