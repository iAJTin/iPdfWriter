﻿
using System.Drawing;

using iTin.Core.Models.Design.Enums;
using iTin.Utilities.Pdf.Design.Image;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <inheritdoc/>
    /// <summary>
    /// Specialization of <see cref="IInsert"/> interface.<br/>
    /// Acts as base class for insert image actions.
    /// </summary>
    public abstract class InsertImageBase : IInsert
    {
        #region interface

        #region IInsert

        #region public properties

        #region [public] (PointF) Offset: Gets or sets a reference a point structure which represents the element offset
        /// <summary>
        /// Gets or sets a reference a point structure which represents the element offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains image offset to apply.
        /// </value>
        public PointF Offset { get; set; }
        #endregion

        #region [public] (int) Page: Gets or sets a value to page number on insert this image
        /// <summary>
        /// Gets or sets a value to page number on insert this image.
        /// </summary>
        /// <value>
        /// A <see cref="int"/> object that contains page number on insert this image.
        /// </value>
        public int Page { get; set; }
        #endregion

        #region [public] (YesNo) UseTestMode: Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct
        /// <summary>
        /// Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct. The default value is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if works in mode test; otherwise <see cref="YesNo.No"/>.
        /// </value>
        public YesNo UseTestMode { get; set; }
        #endregion

        #endregion

        #endregion

        #endregion

        #region public properties

        #region [public] (PdfImage) Image: Gets or sets a reference to pdf image object
        /// <summary>
        /// Gets or sets a reference to pdf image object.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImage"/> object that contains a reference to pdf image object.
        /// </value>
        public PdfImage Image { get; set; }
        #endregion

        #endregion
    }
}
