
namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// Defines the strategy of the start point of the element to be inserted.
    /// </summary>
    public enum StartLocationStrategy
    {
        /// <summary>
        /// Use the position defined in the source document. Takes no action.
        /// </summary>
        Default,

        /// <summary>
        /// Sets the origin position to the left margin of the document
        /// </summary>
        LeftMargin,

        /// <summary>
        /// Sets the origin position just at the ending of the previous element, if it does not exist, this value is ignored
        /// </summary>
        PreviousElement
    }
}
