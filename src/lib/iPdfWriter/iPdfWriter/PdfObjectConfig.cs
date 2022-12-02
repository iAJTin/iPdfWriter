
using System.Diagnostics;

namespace iPdfWriter.ComponentModel
{
    using Config;
    using Operations.Replace.Replacement.Text;
    using SystemTag;

    /// <inheritdoc/>
    /// <summary>
    /// Represents configuration information for an object <see cref="PdfObject"/>.
    /// </summary>
    public sealed class PdfObjectConfig : IPdfObjectConfig
    {
        #region public constants

        /// <summary>
        /// Defines a one-megabyte in bytes.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public const long OneMegaByte = 1048576;

        #endregion

        #region public static members

        /// <summary>
        /// Defaults configuration. Use index and delete the physical files after merge.
        /// </summary>
        public static readonly PdfObjectConfig Default = new();

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfObjectConfig"/> class.
        /// </summary>
        public PdfObjectConfig()
        {
            UseIndex = true;
            AllowCompression = false;
            Tags = new SystemTagsCollection();
            CompressionThreshold = int.MinValue;
            DeletePhysicalFilesAfterMerge = true;
            GlobalReplacements = new GlobalReplacementsCollection();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value indicating whether compression is allowed.
        /// </summary>
        /// <value>
        /// <b>true</b> if compression is allowed; otherwise, <b>false</b>.
        /// </value>
        public bool AllowCompression { get; set; }

        /// <summary>
        /// Gets or sets a value that establishes the threshold from which the output stream will be compressed, this value will be set to <b>MB</b>, remember that a <b>MB</b> equals 1.024 Bytes.
        /// </summary>
        /// <value>
        /// A <see cref="float"/> that contains compression threshold in <b>MB</b>.
        /// </value>
        public float CompressionThreshold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether delete physical files after merge.
        /// </summary>
        /// <value>
        /// <b>true</b> if delete physical files after merge; otherwise, <b>false</b>.
        /// </value>
        public bool DeletePhysicalFilesAfterMerge { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the collection of global text replacements when the elements are merged.
        /// </summary>
        /// <value>
        /// A <see cref="GlobalReplacementsCollection"/> that contains the collection of global text replacements when the elements are merged.
        /// </value>
        public GlobalReplacementsCollection GlobalReplacements { get; set; }

        /// <summary>
        /// Gets or sets a value that contains the collection of system tags to replace when the elements are merged.
        /// </summary>
        /// <value>
        /// A <see cref="SystemTagsCollection"/> that contains the collection of system tags to replace when the elements are merged.
        /// </value>
        public SystemTagsCollection Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use index.
        /// </summary>
        /// <value>
        /// <b>true</b> if uses index; otherwise, <b>false</b>.
        /// </value>
        public bool UseIndex { get; set; }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() => 
            $"UseIndex = {UseIndex}, DeletePhysicalFilesAfterMerge = {DeletePhysicalFilesAfterMerge}";

        #endregion
    }
}
