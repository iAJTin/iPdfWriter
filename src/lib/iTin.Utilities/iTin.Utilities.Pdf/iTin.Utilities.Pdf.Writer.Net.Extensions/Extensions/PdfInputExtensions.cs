
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;

using iTin.Utilities.Writer.Abstractions.Operations.Results;

namespace iTin.Utilities.Pdf.Writer
{
    /// <summary>
    /// Static class than contains extension methods for <see cref="PdfInput"/> objects.
    /// </summary>
    public static class PdfInputExtensions
    {
        /// <summary>
        ///  Try download a <see cref="PdfInput"/> reference.
        /// </summary>
        /// <param name="input">The target <see cref="PdfInput"/> reference.</param>
        /// <param name="filename">Destination full path.</param>
        /// <returns>
        /// A <see cref="IResult"/> object that constains the action operation result.
        /// </returns>
        public static IResult Download(this PdfInput input, string filename) => 
            input.ToOutputResult().Download(filename);

        /// <summary>
        ///  Try download a <see cref="PdfInput"/> reference asynchronously.
        /// </summary>
        /// <param name="input">The target <see cref="PdfInput"/> reference.</param>
        /// <param name="filename">Destination full path.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="IResult"/> object that constains the action operation result.
        /// </returns>
        public static async Task<IResult> DownloadAsync(this PdfInput input, string filename, CancellationToken cancellationToken = default) => 
            await input.ToOutputResult().DownloadAsync(filename, cancellationToken);


        private static OutputResult ToOutputResult(this PdfInput input) => 
            new PdfObject { Items = new[] { input.Clone() } }.TryMergeInputs();
    }
}
