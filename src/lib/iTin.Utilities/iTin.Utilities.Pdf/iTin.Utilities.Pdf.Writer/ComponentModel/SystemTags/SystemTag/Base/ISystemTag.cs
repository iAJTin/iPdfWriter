
using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// Defines a generic System Tag.
    /// </summary>
    public interface ISystemTag
    {
        /// <summary>
        /// Returns a value containing the system tag to replace.
        /// </summary>
        /// <value>
        /// One of the <see cref="SystemTags"/> enumeration values.
        /// </value>
        SystemTags Tag { get; }

        /// <summary>
        /// Gets a reference to the replacement object.
        /// </summary>
        /// <value>
        /// A reference to replacement object.
        /// </value>
        ISystemTagTextReplacement BuildReplacementObject();
    }
}
