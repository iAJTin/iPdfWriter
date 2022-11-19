﻿
using iTin.Utilities.Pdf.Writer.ComponentModel;

using iTin.Utilities.Writer.Abstractions.Operations.Results;

namespace iTin.Utilities.Pdf.Writer.Operations.Result.Output
{
    /// <summary>
    /// Represents configuration information for an object <see cref="PdfOutputResultData"/>.
    /// </summary>
    public interface IPdfOutputResultData : IOutputResultData
    {
        /// <summary>
        /// Gets a value indicating type of output file.
        /// </summary>
        /// <value>
        /// One of the values of the <see cref="KnownOutputType"/> enumeration.
        /// </value>
        new KnownOutputType OutputType { get; }
    }
}
