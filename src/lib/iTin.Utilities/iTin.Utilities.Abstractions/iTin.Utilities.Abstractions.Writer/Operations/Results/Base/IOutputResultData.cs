
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;

using iTin.Utilities.Abstractions.Writer.Operations.Actions;

namespace iTin.Utilities.Abstractions.Writer.Operations.Results
{
    /// <summary>
    /// Defines allowed actions for output result.
    /// </summary>
    public interface IOutputResultData
    {
        /// <summary>
        /// Gets or sets a value indicating whether output file has been zipped.
        /// </summary>
        /// <value>
        /// <b>true</b> if output file has been zipped; otherwise, <b>false</b>.
        /// </value>
        bool IsZipped { get; }

        /// <summary>
        /// Gets a value indicating type of output file.
        /// </summary>
        /// <value>
        /// enumeration.
        /// </value>
        Enum OutputType { get; }


        /// <summary>
        /// Executes specified action for this output result instance.
        /// </summary>
        /// <param name="output">Target output result.</param>
        /// <returns>
        /// <para>
        /// An instance which implements the <see cref="IResult"/> interface that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// </returns>
        IResult Action(IOutputAction output);
        
        /// <summary>
        /// Returns a reference to the output stream.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains a reference to the output stream.
        /// </returns>
        Stream GetOutputStream();

        /// <summary>
        /// Returns a reference to the uncompressed output stream. If <see cref="OutputType"/> is <b>Pdf</b> this value is equals to OutputStream value property.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains the original outupt uncrompressed.
        /// </returns>
        public Stream GetUnCompressedOutputStream();


        /// <summary>
        /// Executes specified action for this output result instance asynchronously.
        /// </summary>
        /// <param name="output">Target output result.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// <para>
        /// An instance which implements the <see cref="IResult"/> interface that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// </returns>
        Task<IResult> Action(IOutputActionAsync output, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a reference to the output stream.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="Stream"/> that contains a reference to the output stream.
        /// </returns>
        Task<Stream> GetOutputStreamAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a reference to the uncompressed output stream. If <see cref="OutputType"/> is <b>Pdf</b> this value is equals to OutputStream value property.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the original outupt uncrompressed.
        /// </returns>
        Task<Stream> GetUnCompressedOutputStreamAsync(CancellationToken cancellationToken = default);
    }
}
