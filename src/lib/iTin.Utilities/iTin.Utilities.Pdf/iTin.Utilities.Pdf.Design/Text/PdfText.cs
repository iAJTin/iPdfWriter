
namespace iTin.Utilities.Pdf.Design.Text
{
    using Logging;

    /// <summary>
    /// Defines a <b>pdf</b> text object.
    /// </summary>
    public sealed class PdfText 
    {
        #region constructor/s

        #region [private] PdfText(string, int, long): Initializes a new instance of the class with a native pdf table reference
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfText"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="page">The page.</param>
        /// <param name="position">The position.</param>
        public PdfText(string text, int page, long position)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfText).Assembly.GetName().Name}, v{typeof(PdfText).Assembly.GetName().Version}, Namespace: {typeof(PdfText).Namespace}, Class: {nameof(PdfText)}");
            Logger.Instance.Debug($" Initializes a new instance of the {typeof(PdfText)} class");
            Logger.Instance.Debug($" > Signature: #ctor({typeof(string)}, {typeof(int)}, {typeof(long)})");

            Text = text;
            Page = page;
            Position = position;

            Logger.Instance.Debug($"   -> Text: {Text}");
            Logger.Instance.Debug($"   -> Page: {Page}");
            Logger.Instance.Debug($"   -> Position: {Position}");
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

        #region [public] (long) Position: Gets or sets the position
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public long Position { get; set; }
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
