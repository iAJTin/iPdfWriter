
using System.Drawing;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;

namespace iPdfWriter.Operations.Replace.Replacement.Text
{
    using SystemTag;

    /// <summary>
    /// Specialization of <see cref="SystemTagTextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with a <see cref="SystemTags"/>.
    /// </summary>
    internal class WithSystemTagObject : SystemTagTextReplacementBase
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="WithSystemTagObject"/> class.
        /// </summary>
        public WithSystemTagObject()
        {
            Tag = SystemTags.None;
            UseTestMode = YesNo.No;
            Offset = PointF.Empty;
            Style = PdfTextStyle.Default;
            ReplaceOptions = ReplaceTextOptions.Default;
        }

        #endregion

        #region public static readonly properties

        /// <summary>
        /// Returns a new instance of <see cref="WithSystemTagObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithSystemTagObject"/> that contains the default values.
        /// </value>
        public static WithSystemTagObject Default => new();

        #endregion

        #region public new properties

        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfTextStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfTextStyle Style { get; set; }

        #endregion
    }
}
