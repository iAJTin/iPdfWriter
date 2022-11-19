﻿
using System.Drawing;

using iTin.Core.Models.Design.Enums;

using iTin.Utilities.Pdf.Design.Styles;

using iTin.Utilities.Pdf.Writer.Operations.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text;

namespace iTin.Utilities.Pdf.Writer.SystemTag
{
    /// <summary>
    /// Specialization of the <see cref="ISystemTag"/> interface that represents the system tag that allows to replace the total page number in a document when merging documents.
    /// </summary>
    public sealed class TotalPagesSystemTag : ISystemTag
    {
        #region interfaces

        #region ISystemTag

        #region public readonly properties

        /// <summary>
        /// Returns a value containing the system tag to replace.
        /// </summary>
        /// <value>
        /// Always return <see cref="SystemTags.TotalPages"/>.
        /// </value>
        public SystemTags Tag => SystemTags.TotalPages;

        #endregion

        #region public methods

        /// <summary>
        /// Returns a reference to an object that replaces text based on the implementation of the <see cref="ISystemTagTextReplacement"/> interface.
        /// </summary>
        /// <returns>
        /// A reference that implements the <see cref="ISystemTagTextReplacement"/> interface.
        /// </returns>
        public ISystemTagTextReplacement BuildReplacementObject() 
            => new WithSystemTagObject
            {
                Tag = SystemTags.PageNumber,
                Style = Style ?? PdfTextStyle.Default,
                Offset = Offset,
                UseTestMode = UseTestMode,
                ReplaceOptions = ReplaceOptions ?? ReplaceTextOptions.Default
            };

        #endregion

        #endregion

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value that represents text replace options.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions" /> instance that contains text options.
        /// </value>
        public ReplaceTextOptions ReplaceOptions { get; set; }

        /// <summary>
        /// Gets or sets a reference to new text style format. The default is <see cref="PdfTextStyle.Default"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTextStyle"/> object that contains image offset to apply.
        /// </value>
        public PdfTextStyle Style { get; set; }

        /// <summary>
        /// Gets or sets a reference a point structure which represents the new text offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains new text offset to apply.
        /// </value>
        public PointF Offset { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct. The default value is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if works in mode test; otherwise <see cref="YesNo.No"/>.
        /// </value>
        public YesNo UseTestMode { get; set; }

        #endregion
    }
}
