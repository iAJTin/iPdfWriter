
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    /// <summary>
    /// Defines allowed actions for text replacement object
    /// </summary>
    public interface ISystemTagTextReplacement : IReplacement
    {
        /// <summary>
        /// Gets or sets a value that represents text replace options.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains text options.
        /// </value>
        ReplaceTextOptions ReplaceOptions { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the system tag to replace.
        /// </summary>
        /// <value>
        /// One of the <see cref="SystemTags"/> enumeration values.
        /// </value>
        SystemTags Tag { get; set; }
    }
}
