
namespace iTin.Utilities.Pdf.Design.Styles
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Models.Design;
    using iTin.Core.Models.Design.Enums;

    /// <summary>
    /// A Specialization of <see cref="PdfBaseStyle"/> class.<br/>
    /// Defines a <b>pdf</b> text style.
    /// </summary>
    public partial class PdfTextStyle
    {
        #region private field members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private FontModel _font;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private PdfTextContent _content;
        #endregion

        #region public readonly static properties

        #region [public] {static} (PdfTextStyle) Center: Gets a text style
        /// <summary>
        /// Gets a text style.
        /// </summary>
        /// <value>
        /// Center text style.
        /// </value>
        public static PdfTextStyle Center => new PdfTextStyle
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

        #region [public] {static} (PdfTextStyle) Left: Gets a text style
        /// <summary>
        /// Gets a text style.
        /// </summary>
        /// <value>
        /// Left text style.
        /// </value>
        public static PdfTextStyle Left => new PdfTextStyle
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

        #region [public] {static} (PdfTextStyle) Right: Gets a text style
        /// <summary>
        /// Gets a text style.
        /// </summary>
        /// <value>
        /// Right text style.
        /// </value>
        public static PdfTextStyle Right => new PdfTextStyle
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

        #region [public] {new} {static} (PdfTextStyle) Default: Returns a new instance containing a default text style
        /// <summary>
        /// Returns a new instance containing a default text style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextStyle"/> reference containing the default text style settings.
        /// </value>
        public new static PdfTextStyle Default => new PdfTextStyle();
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (bool) FontSpecified: Gets a value that tells the serializer if the referenced item is to be included
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
        public bool FontSpecified => !Font.IsDefault;
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

        #region [public] (FontModel) Font: Gets or sets the font model
        /// <summary>
        /// Gets or sets the font model.
        /// </summary>
        /// <value>
        /// Reference that contains the definition of a font.            
        /// </value>
        [XmlElement]
        [JsonProperty("font")]
        public FontModel Font
        {
            get => _font ?? (_font = FontModel.DefaultFont);
            set => _font = value;
        }
        #endregion

        #endregion

        #region public new properties

        #region [public] {new} (PdfTextContent) Content: Gets or sets content distribution
        /// <summary>
        /// Gets or sets content distribution.
        /// </summary>
        /// <value>
        /// Reference for content distribution.
        /// </value>
        [XmlElement]
        [JsonProperty("content")]
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
        public new PdfTextContent Content
        {
            get
            {
                if (_content == null)
                {
                    _content = PdfTextContent.Default;
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

        #region [public] {new} (PdfTextStyle) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfTextStyle Clone()
        {
            var cloned = (PdfTextStyle)base.Clone();
            cloned.Font = Font.Clone();
            cloned.Content = Content.Clone();
            cloned.Borders = Borders.Clone();
            cloned.Properties = Properties.Clone();
            
            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(PdfTextStyleOptions): Apply specified options to this style
        /// <summary>
        /// Apply specified options to this style.
        /// </summary>
        public virtual void ApplyOptions(PdfTextStyleOptions options)
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

            #region Font
            Font.ApplyOptions(options.Font);
            #endregion
        }
        #endregion

        #region [public] {virtual} (void) Combine(PdfTextStyle): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfTextStyle reference)
        {
            if (reference == null)
            {
                return;
            }

            base.Combine(reference);

            Font.Combine(reference.Font);
            Content.Combine(reference.Content);
        }
        #endregion

        #endregion
    }
}
