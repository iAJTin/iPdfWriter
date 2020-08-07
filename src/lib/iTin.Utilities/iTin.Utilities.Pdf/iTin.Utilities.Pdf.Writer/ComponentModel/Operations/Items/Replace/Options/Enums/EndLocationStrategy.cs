
namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// Defines the strategy of the end point of the element to be inserted.
    /// </summary>
    public enum EndLocationStrategy
    {
        /// <summary>
        /// Use the position defined in the source document. Takes no action.
        /// </summary>
        Default,

        /// <summary>
        /// Sets the end position to the right margin of the document.
        /// </summary>
        RightMargin,

        /// <summary>
        /// Sets the end position just at the beginning of the next element, if it does not exist, this value is ignored.
        /// </summary>
        NextElement
    }
}
