
namespace iTin.Utilities.Pdf.Design.Text
{
    /// <summary>
    /// Defines a <b>pdf</b> text line object.
    /// </summary>
    public sealed class PdfTextLine
    {
        #region constructor/s

        #region [private] PdfText(string, int): Initializes a new instance of the class with a native pdf table reference
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfText"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="page">The page.</param>
        public PdfTextLine(string text, int page)
        {
            Text = text;
            Page = page;
        }
        #endregion

        #endregion

        #region public properties

        #region [public] (string) Text: Gets or sets the text
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }
        #endregion

        #region [public] (int) Page: Gets or sets the page
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; }
        #endregion

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a string than represents the current object.
        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString() => $"Text=\"{Text}\", Page={Page}";
        #endregion

        #endregion
    }
}
