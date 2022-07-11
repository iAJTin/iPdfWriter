
using System.Drawing;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    /// <summary>
    /// Defines allowed actions for text replacement object
    /// </summary>
    public interface ITextReplacement : IReplacement
    {
        /// <summary>
        /// Gets or sets a reference a point structure which represents the text/image offset. The default is <see cref="PointF.Empty"/>.
        /// Positive values on the y axis move the text/image down and positive values on the x axis move the text/image right.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains text/image offset to apply.
        /// </value>
        public PointF Offset { get; set; }

        /// <summary>
        /// Gets or sets the text to replace.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the text to replace.
        /// </value>
        string Text { get; set; }
    }
}
