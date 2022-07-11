
using System.Drawing;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    /// <summary>
    /// Specialization of <see cref="TextReplacementBase"/> interface.<br/>
    /// Contains the logic to replace a text with an image.
    /// </summary>
    public class WithImageObject : TextReplacementBase
    {
        #region constructor/s

        #region [public] WithImageObject(): Initializes a new instance of the class
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

        #endregion

        #region public static readonly properties

        #region [public] static (WithImageObject) Default: Returns a new instance that contains the default values
        /// <summary>
        /// Returns a new instance of <see cref="WithImageObject"/> class that contains the default values.
        /// </summary>
        /// <value>
        /// A <see cref="WithImageObject"/> that contains the default values.
        /// </value>
        public static WithImageObject Default => new();
        #endregion

        #endregion

        #region public properties

        #region [public] (Design.Image.PdfImage) Image: Gets or sets a reference to pdf image object
        /// <summary>
        /// Gets or sets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="Design.Image.PdfImage"/> object that contains a reference to pdf image object.
        /// </value>
        public Design.Image.PdfImage Image { get; set; }
        #endregion

        #endregion

        #region public new properties

        #region [public] {new} (PdfImageStyle) Style: Gets or sets a reference to new text style format
        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfImageStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageStyle"/> object that contains image offset to apply.
        /// </value>
        public new PdfImageStyle Style { get; set; }
        #endregion

        #endregion
    }
}
