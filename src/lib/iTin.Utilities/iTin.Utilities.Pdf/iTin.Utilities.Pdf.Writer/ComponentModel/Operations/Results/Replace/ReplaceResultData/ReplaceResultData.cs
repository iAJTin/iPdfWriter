
using System.IO;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace
{
    /// <summary>
    /// Represents insert data for an object <see cref="ReplaceResult"/>.
    /// </summary>
    public class ReplaceResultData
    {
        #region internal properties

        #region [public] (IInput) Context: Gets or sets a
        /// <summary>
        /// Gets or sets a value indicating whether output file has been zipped.
        /// </summary>
        /// <value>
        /// <b>true</b> if output file has been zipped; otherwise, <b>false</b>.
        /// </value>
        internal IInput Context { get; set; }
        #endregion

        #endregion

        #region public properties

        #region [public] (Stream) InputStream: Gets or sets a
        /// <summary>
        /// Gets or sets a value indicating whether output file has been zipped.
        /// </summary>
        /// <value>
        /// <b>true</b> if output file has been zipped; otherwise, <b>false</b>.
        /// </value>
        public Stream InputStream { get; set; }

        #endregion

        #region [public] (Stream) OutputStream: Gets or sets a
        /// <summary>
        /// Gets or sets a value indicating whether output file has been zipped.
        /// </summary>
        /// <value>
        /// <b>true</b> if output file has been zipped; otherwise, <b>false</b>.
        /// </value>
        public Stream OutputStream { get; set; }
    
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
        public override string ToString() => $"{(OutputStream.Length > InputStream.Length ? "Modified" : "Default")}";
        #endregion

        #endregion
    }
}
