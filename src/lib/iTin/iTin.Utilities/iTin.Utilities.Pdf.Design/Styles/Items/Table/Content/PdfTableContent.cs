
using System.Diagnostics;

using iTin.Core;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="BaseContent"/> class.<br/>
    /// Defines a <b>pdf</b> image content.
    /// </summary>
    public partial class PdfTableContent
    {
        #region public new readonly static properties

        /// <summary>
        /// Returns a new instance containing a default table content style settings.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableContent"/> reference containing the default table content style settings.
        /// </value>
        public new static PdfTableContent Default => new();

        #endregion

        #region public override properties

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override bool IsDefault => base.IsDefault;

        #endregion

        #region public new methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public new PdfTableContent Clone()
        {
            var cloned = (PdfTableContent)base.Clone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }

        #endregion

        #region public virtual methods

        /// <summary>
        /// Apply specified options to this content.
        /// </summary>
        public virtual void ApplyOptions(PdfTableContentOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Color
            string colorOption = options.Color;
            bool colorHasValue = !colorOption.IsNullValue();
            if (colorHasValue)
            {
                Color = colorOption;
            }
            #endregion

            #region Show
            YesNo? showOption = options.Show;
            bool showHasValue = showOption.HasValue;
            if (showHasValue)
            {
                Show = showOption.Value;
            }
            #endregion
        }

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        public virtual void Combine(PdfTableContent reference)
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
