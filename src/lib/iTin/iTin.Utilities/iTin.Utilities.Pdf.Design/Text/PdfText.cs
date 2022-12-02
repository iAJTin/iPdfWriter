
namespace iTin.Utilities.Pdf.Design.Text
{
    /// <summary>
    /// Defines a <b>pdf</b> text object.
    /// </summary>
    public sealed class PdfText 
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfText"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="page">The page.</param>
        /// <param name="position">The position.</param>
        public PdfText(string text, int page, long position)
        {
            Text = text;
            Page = page;
            Position = position;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public long Position { get; set; }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString() => 
            $"Text=\"{Text}\", Page={Page}";

        #endregion
    }
}
