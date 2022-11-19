
using iTin.Utilities.Writer.Abstractions.Config;

namespace iTin.Utilities.Pdf.Writer.Config
{
    /// <summary>
    /// Represents configuration information for an object <see cref="PdfObject"/>.
    /// </summary>
    /// <seealso cref="IPdfObjectConfig"/>
    public class PdfOutputResultConfig : IPdfObjectConfig, IOutputResultConfig
    {
        #region public static members

        /// <summary>
        /// Defaults configuration. Defaults no zipped output.
        /// </summary>
        public static readonly PdfOutputResultConfig Default = new() { Zipped = false };

        /// <summary>
        /// Zipped output configuration. This output has been zipped.
        /// </summary>
        public static readonly PdfOutputResultConfig ZippedResult = new() { Zipped = true };

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfOutputResultConfig"/> class.
        /// </summary>
        public PdfOutputResultConfig()
        {
            Zipped = false;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value thats represents filename if is marked as zipped
        /// </summary>
        /// <value>
        /// Represents filename.
        /// </value>
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether compression is allowed.
        /// </summary>
        /// <value>
        /// <b>true</b> if compression is allowed; otherwise, <b>false</b>.
        /// </value>
        public bool Zipped { get; set; }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() =>
            Zipped
                ? $"Zipped={Zipped}, Filename=\"{Filename}\""
                : $"Zipped={Zipped}";

        #endregion
    }
}
