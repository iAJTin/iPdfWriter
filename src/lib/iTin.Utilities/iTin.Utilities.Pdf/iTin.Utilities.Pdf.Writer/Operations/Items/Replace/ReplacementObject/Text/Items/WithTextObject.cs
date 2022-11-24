
using System.Drawing;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;

namespace iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text
{
    /// <summary>
    /// Specialization of <see cref="TextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with another text.
    /// </summary>
    public class WithTextObject : TextReplacementBase
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="WithTextObject"/> class.
        /// </summary>
        public WithTextObject()
        {
            NewText = Text;
            UseTestMode = YesNo.No;
            Offset = PointF.Empty;
            Style = PdfTextStyle.Default;
            ReplaceOptions = ReplaceTextOptions.Default;
        }

        #endregion

        #region public static readonly properties

        /// <summary>
        /// Returns a new instance of <see cref="WithTextObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithTextObject"/> that contains the default values.
        /// </value>
        public static WithTextObject Default => new();

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value that contains the new text. The default is same text value.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the new text.
        /// </value>
        public string NewText { get; set; }

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
