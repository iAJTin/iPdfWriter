
using System.Drawing;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Image;
using iTin.Utilities.Pdf.Design.Styles;

namespace iPdfWriter.Operations.Replace.Replacement.Text
{
    /// <summary>
    /// Specialization of <see cref="TextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with an image.
    /// </summary>
    public class WithImageObject : TextReplacementBase
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="WithImageObject"/> class.
        /// </summary>
        public WithImageObject()
        {
            UseTestMode = YesNo.No;
            Offset = PointF.Empty;
            Style = PdfImageStyle.Default;
            ReplaceOptions = ReplaceTextOptions.Default;
        }

        #endregion

        #region public static readonly properties

        /// <summary>
        /// Returns a new instance of <see cref="WithImageObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithImageObject"/> that contains the default values.
        /// </value>
        public static WithImageObject Default => new();

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImage"/> object that contains a reference to pdf image object.
        /// </value>
        public PdfImage Image { get; set; }

        #endregion

        #region public new properties

        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfImageStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfImageStyle Style { get; set; }

        #endregion
    }
}
