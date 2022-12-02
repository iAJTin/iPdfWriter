
using System.Drawing;

using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Styling;

namespace iPdfWriter.Operations.Replace.Replacement.Text
{
    using SystemTag;

    /// <summary>
    /// Specialization of <see cref="IReplacement"/> interface.<br/>
    /// Acts as base class for system tags replacement actions.
    /// </summary>
    public abstract class SystemTagTextReplacementBase : ISystemTagTextReplacement
    {
        #region interfaces

        #region IReplacement

        #region public properties

        /// <summary>
        /// Gets or sets a reference a point structure which represents the text/image/table offset. The default is <see cref="PointF.Empty"/>.
        /// Positive values on the y axis move the text/image/table down and positive values on the x axis move the text/image right.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains text/image/table offset to apply.
        /// </value>
        public PointF Offset { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value that represents text replace options.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains text options.
        /// </value>
        public ReplaceTextOptions ReplaceOptions { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct. The default value is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if works in mode test; otherwise <see cref="YesNo.No"/>.
        /// </value>
        public YesNo UseTestMode { get; set; }

        #endregion

        #endregion

        #region ISystemTagTextReplacement

        #region public properties

        /// <summary>
        /// Gets or sets a value that contains the system tag to replace.
        /// </summary>
        /// <value>
        /// One of the <see cref="SystemTags"/> enumeration values.
        /// </value>
        public SystemTags Tag { get; set; }

        #endregion

        #endregion

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a reference to style to apply.
        /// </summary>
        /// <value>
        /// A <see cref="BaseStyle"/> object that contains a reference to style to apply
        /// </value>
        public BaseStyle Style { get; set; }

        #endregion
    }
}
