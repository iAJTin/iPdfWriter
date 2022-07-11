
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

        #region [public] {new} {static} (PdfImageContentAlignment) Default: Returns a new instance containing default image content alignment style settings
        /// <summary>
        /// Returns a new instance containing default image content alignment style settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfImageContentAlignment"/> reference containing the default image content alignment settings.
        /// </value>
        public new static PdfImageContentAlignment Default => new PdfImageContentAlignment();

        #endregion

        #endregion

        #region public new methods

        #region [public] {new} (PdfImageContentAlignment) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfImageContentAlignment Clone() => (PdfImageContentAlignment)MemberwiseClone();
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(PdfImageContentAlignmentOptions): Apply specified options to this content
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
        #endregion

        #region [public] {virtual} (void) Combine(PdfImageContentAlignment): Combines this instance with reference parameter
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

        #endregion
    }
}
