
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Enums;

namespace iTin.Utilities.Pdf.Design.Styles
{
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

        /// <summary>
        /// Gets a text style.
        /// </summary>
        /// <value>
        /// Center text style.
        /// </value>
        public static PdfTextStyle Center => new()
        {
            Content =
            {
                Alignment =
                {
                    Horizontal = KnownHorizontalAlignment.Center
                }
            }
        };

        /// <summary>
        /// Gets a text style.
        /// </summary>
        /// <value>
        /// Left text style.
        /// </value>
        public static PdfTextStyle Left => new()
        {
            Content =
            {
                Alignment =
                {
                    Horizontal = KnownHorizontalAlignment.Left
                }
            }
        };

        /// <summary>
        /// Gets a text style.
        /// </summary>
        /// <value>
        /// Right text style.
        /// </value>
        public static PdfTextStyle Right => new()
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

        #region public new readonly static properties

        /// <summary>
        /// Returns a new instance containing a default text style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextStyle"/> reference containing the default text style settings.
        /// </value>
        public new static PdfTextStyle Default => new();

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
        public bool FontSpecified => !Font.IsDefault;

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
        public new bool ContentSpecified => !Content.IsDefault;

        #endregion

        #region public properties

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
            get => _font ??= FontModel.DefaultFont;
            set => _font = value;
        }

        #endregion

        #region public new properties

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
                _content ??= PdfTextContent.Default;
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
            Content.IsDefault;

        #endregion

        #region public new methods

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

        #region public virtual methods

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
    }
}
