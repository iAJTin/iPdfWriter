
namespace iPdfWriter.Operations.Result.Output
{
    using Abstractions.Writer.Operations.Results;
    using ComponentModel;

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
