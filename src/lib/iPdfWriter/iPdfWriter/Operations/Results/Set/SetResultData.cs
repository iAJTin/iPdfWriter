
using System.IO;

namespace iPdfWriter.Operations.Result.Set
{
    using Abstractions.Writer.Operations.Results;
    using Input;

    /// <summary>
    /// Represents set data for an object <see cref="SetResult"/>.
    /// </summary>
    public class SetResultData : IResultData
    {
        #region interfaces

        #region IResultData

        #region public properties

        /// <summary>
        /// Gets or sets a value 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public Stream InputStream { get; set; }

        /// <summary>
        /// Gets or sets a value 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public Stream OutputStream { get; set; }

        #endregion

        #endregion

        #endregion

        #region internal properties

        /// <summary>
        /// Gets or sets a value 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        internal IPdfInput Context { get; set; }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() => 
            $"{(OutputStream.Length > InputStream.Length ? "Modified" : "Default")}";

        #endregion
    }
}
