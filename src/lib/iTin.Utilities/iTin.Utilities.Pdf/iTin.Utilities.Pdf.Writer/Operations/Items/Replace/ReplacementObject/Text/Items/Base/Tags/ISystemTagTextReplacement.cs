
using iTin.Utilities.Pdf.Writer.SystemTag;

namespace iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text
{
    /// <summary>
    /// Defines allowed actions for text replacement object
    /// </summary>
    public interface ISystemTagTextReplacement : IReplacement
    {
        /// <summary>
        /// Gets or sets a value that contains the system tag to replace.
        /// </summary>
        /// <value>
        /// One of the <see cref="SystemTags"/> enumeration values.
        /// </value>
        SystemTags Tag { get; set; }
    }
}
