
using System.Drawing;

using iTin.Core.Models.Design.Enums;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// Defines allowed actions for insert objects
    /// </summary>
    public interface IInsert
    {
        /// <summary>
        /// Gets or sets a reference a point structure which represents the element offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains element offset to apply.
        /// </value>
        PointF Offset { get; set; }

        /// <summary>
        /// Gets or sets a value to page number on insert this image.
        /// </summary>
        /// <value>
        /// A <see cref="int"/> object that contains page number on insert this image.
        /// </value>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct. The default value is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if works in mode test; otherwise <see cref="YesNo.No"/>.
        /// </value>
        public YesNo UseTestMode { get; set; }
    }
}
