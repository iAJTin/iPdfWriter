
using System.IO;

namespace iTin.Utilities.Abstractions.Writer.Operations.Results
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResultData
    {
        /// <summary>
        /// Gets or sets a value 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        Stream InputStream { get; set; }

        /// <summary>
        /// Gets or sets a value 
        /// </summary>
        /// <value>
        /// 
        /// </value>
        Stream OutputStream { get; set; }
    }
}
