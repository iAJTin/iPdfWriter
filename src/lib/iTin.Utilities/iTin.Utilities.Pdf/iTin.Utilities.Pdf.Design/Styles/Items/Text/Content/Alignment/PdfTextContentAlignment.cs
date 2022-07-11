
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseContentAlignment"/> class.<br/>
    /// Defines a <b>pdf</b> text content alignment.
    /// </summary>
    public partial class PdfTextContentAlignment
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const KnownVerticalAlignment DefaultVerticalAlignment = KnownVerticalAlignment.Center;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private KnownVerticalAlignment _vertical;
        #endregion

        #region constructor/s

        #region [public] PdfTextContentAlignment(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTextContentAlignment"/> class.
        /// </summary>
        public PdfTextContentAlignment()
        {
            Vertical = DefaultVerticalAlignment;
        }
        #endregion

        #endregion

        #region public new static properties

        #region [public] {new} {static} (PdfTextContentAlignment) Returns a new instance containing default text content alignment style settings
        /// <summary>
        /// Returns a new instance containing default text content alignment style settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextContentAlignment"/> reference containing the default text content alignment settings.
        /// </value>
        public new static PdfTextContentAlignment Default => new PdfTextContentAlignment();

        #endregion

        #endregion

        #region public properties

        #region [public] (KnownVerticalAlignment) Vertical: Gets or sets vertical alignment.
        /// <summary>
        /// Gets or sets vertical alignment. The default is <see cref="KnownVerticalAlignment.Center"/>.
        /// </summary>
        /// <value>
        /// One of the <see cref="KnownVerticalAlignment"/> values.
        /// </value>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultVerticalAlignment)]
        public KnownVerticalAlignment Vertical
        {
            get => _vertical;
            set
            {
                SentinelHelper.IsEnumValid(value);
                _vertical = value;
            }
        }
        #endregion

        #endregion

        #region public override properties

        #region [public] {override} (bool) IsDefault: Gets a value indicating whether this instance is default
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault => base.IsDefault && Vertical.Equals(DefaultVerticalAlignment);
        #endregion

        #endregion

        #region public new methods

        #region [public] {new} (XlsxCellContentAlignment) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfTextContentAlignment Clone() => (PdfTextContentAlignment)MemberwiseClone();
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(PdfTextContentAlignmentOptions): Apply specified options to this content
        /// <summary>
        /// Apply specified options to this content.
        /// </summary>
        public virtual void ApplyOptions(PdfTextContentAlignmentOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Horizontal
            KnownHorizontalAlignment? horizontalOption = options.Horizontal;
            bool horizontalHasValue = horizontalOption.HasValue;
            if (horizontalHasValue)
            {
                Horizontal = horizontalOption.Value;
            }
            #endregion

            #region Vertical
            KnownVerticalAlignment? verticalOption = options.Vertical;
            bool verticalHasValue = verticalOption.HasValue;
            if (verticalHasValue)
            {
                Vertical = verticalOption.Value;
            }
            #endregion
        }
        #endregion

        #region [public] {virtual} (void) Combine(PdfTextContentAlignment): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfTextContentAlignment reference)
        {
            if (reference == null)
            {
                return;
            }

            base.Combine(reference);

            if (Vertical.Equals(DefaultVerticalAlignment))
            {
                Vertical = reference.Vertical;
            }
        }
        #endregion

        #endregion
    }
}
