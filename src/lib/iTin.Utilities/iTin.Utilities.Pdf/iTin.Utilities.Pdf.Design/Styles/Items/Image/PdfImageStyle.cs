
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;

namespace iTin.Utilities.Pdf.Design.Styles
{
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

        /// <summary>
        /// Returns a center image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing center image style settings.
        /// </value>
        public static PdfImageStyle Center => new()
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
        /// Returns a left image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing left image style settings.
        /// </value>
        public static PdfImageStyle Left => new()
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
        /// Returns a right image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing right image style settings.
        /// </value>
        public static PdfImageStyle Right => new()
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
        /// Returns a new instance containing a default image style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> reference containing the default image style settings.
        /// </value>
        public new static PdfImageStyle Default => new();

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
        public new PdfImageContent Content
        {
            get
            {
                _content ??= PdfImageContent.Default;
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
        public new PdfImageStyle Clone()
        {
            var cloned = (PdfImageStyle)base.Clone();
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
    }
}
