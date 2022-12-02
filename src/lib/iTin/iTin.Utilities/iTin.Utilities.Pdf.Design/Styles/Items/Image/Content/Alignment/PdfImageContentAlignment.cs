
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseContentAlignment"/> class.<br/>
    /// Defines a <b>Pdf</b> image content alignment.
    /// </summary>
    public partial class PdfImageContentAlignment
    {
        #region public new static properties

        /// <summary>
        /// Returns a new instance containing default image content alignment style settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageContentAlignment"/> reference containing the default image content alignment settings.
        /// </value>
        public new static PdfImageContentAlignment Default => new();

        #endregion

        #region public new methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfImageContentAlignment Clone() => (PdfImageContentAlignment)MemberwiseClone();

        #endregion

        #region public virtual methods

        /// <summary>
        /// Apply specified options to this content.
        /// </summary>
        public virtual void ApplyOptions(PdfImageContentAlignmentOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Horizontal
            KnownHorizontalAlignment? horizontalOption = options.Horizontal;
            bool horizontalHasValue = horizontalOption.HasValue;
            if (horizontalHasValue)
            {
                Horizontal = horizontalOption.Value;
            }
            #endregion
        }

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfImageContentAlignment reference)
        {
            if (reference == null)
            {
                return;
            }

            base.Combine(reference);
        }

        #endregion
    }
}
