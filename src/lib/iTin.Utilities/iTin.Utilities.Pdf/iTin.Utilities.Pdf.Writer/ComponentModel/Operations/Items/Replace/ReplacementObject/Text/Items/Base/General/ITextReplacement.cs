
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    /// <summary>
    /// Defines allowed actions for text replacement object
    /// </summary>
    public interface ITextReplacement : IReplacement
    {
        /// <summary>
        /// Gets or sets a value that represents text replace options.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains text options.
        /// </value>
        ReplaceTextOptions ReplaceOptions { get; set; }

        /// <summary>
        /// Gets or sets the text to replace.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the text to replace.
        /// </value>
        string Text { get; set; }
    }
}
