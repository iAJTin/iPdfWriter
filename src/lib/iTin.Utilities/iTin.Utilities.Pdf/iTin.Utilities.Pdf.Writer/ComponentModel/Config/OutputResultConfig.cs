
namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <inheritdoc/>
    /// <summary>
    /// Represents configuration information for an object <see cref="PdfObject"/>.
    /// </summary>
    /// <seealso cref="IPdfObjectConfig"/>
    public class OutputResultConfig : IPdfObjectConfig
    {
        #region public static members
        /// <summary>
        /// Defaults configuration. Defaults no zipped output.
        /// </summary>
        public static readonly OutputResultConfig Default = new OutputResultConfig { Zipped = false };

        /// <summary>
        /// Zipped output configuration. This output has been zipped.
        /// </summary>
        public static readonly OutputResultConfig ZippedResult = new OutputResultConfig { Zipped = true };
        #endregion

        #region constructor/s

        #region [public] OutputResultConfig(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputResultConfig"/> class.
        /// </summary>
        public OutputResultConfig()
        {
            Zipped = false;
        }
        #endregion

        #endregion

        #region public properties

        #region [public] (bool) Filename: Gets or sets a value thats represents filename if is marked as zipped
        /// <summary>
        /// Gets or sets a value thats represents filename if is marked as zipped
        /// </summary>
        /// <value>
        /// <b>true</b> if compression is allowed; otherwise, <b>false</b>.
        /// </value>
        public string Filename { get; set; }
        #endregion 

        #region [public] (bool) Zipped: Gets or sets a value indicating whether compression is allowed
        /// <summary>
        /// Gets or sets a value indicating whether compression is allowed.
        /// </summary>
        /// <value>
        /// <b>true</b> if compression is allowed; otherwise, <b>false</b>.
        /// </value>
        public bool Zipped { get; set; }
        #endregion 

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a string than represents the current object.
        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() 
            => Zipped
                ? $"Zipped={Zipped}, Filename=\"{Filename}\""
                : $"Zipped={Zipped}";
        #endregion

        #endregion
    }
}
