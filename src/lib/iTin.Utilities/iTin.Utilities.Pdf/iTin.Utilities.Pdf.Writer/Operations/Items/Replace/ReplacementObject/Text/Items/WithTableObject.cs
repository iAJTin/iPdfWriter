
using System.Diagnostics;
using System.Drawing;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;
using iTin.Utilities.Pdf.Design.Table;

namespace iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text
{
    /// <summary>
    /// Specialization of <see cref="TextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with a pdf table.
    /// </summary>
    public class WithTableObject : TextReplacementBase
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const float DefaultFixedWidth = -float.MinValue;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="WithTableObject"/> class.
        /// </summary>
        public WithTableObject()
        {
            UseTestMode = YesNo.No;
            Offset = PointF.Empty;
            Style = PdfTableStyle.Default;
            FixedWidth = DefaultFixedWidth;
            ReplaceOptions = ReplaceTextOptions.Default;
        }

        #endregion

        #region public static readonly properties

        /// <summary>
        /// Returns a new instance of <see cref="WithTableObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithTableObject"/> that contains the default values.
        /// </value>
        public static WithTableObject Default => new();

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value that contains the table fixed width to use. The default value is -<see cref="float.MinValue"/> (No use fixed width).
        /// </summary>
        /// <value>
        /// A <see cref="float"/> object that contains the table fixed width to use.
        /// </value>
        public float FixedWidth { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the table to replace the text.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTable"/> object that contains the table to replace the text.
        /// </value>
        public PdfTable Table { get; set; }

        #endregion

        #region public new properties

        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfTableStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfTableStyle Style { get; set; }

        #endregion
    }
}
