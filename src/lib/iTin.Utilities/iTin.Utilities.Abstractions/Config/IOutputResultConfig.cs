
namespace iTin.Utilities.Writer.Abstractions.Config
{
    /// <summary>
    /// Represents configuration information for an object.
    /// </summary>
    public interface IOutputResultConfig
    {
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
    }
}
