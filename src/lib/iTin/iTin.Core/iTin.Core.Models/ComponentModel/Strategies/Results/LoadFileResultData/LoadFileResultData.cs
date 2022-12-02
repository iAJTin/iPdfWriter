
using System.IO;

namespace iTin.Core.Models.ComponentModel.Strategies.Result
{
    /// <summary>
    /// Represents the data for an object <see cref="LoadFileResult"/>.
    /// </summary>
    public class LoadFileResultData
    {
        #region public properties

        /// <summary>
        /// Gets or sets a value 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        public FileStream Stream { get; set; }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() => $"FileLenght={Stream.Length}";

        #endregion
    }
}
