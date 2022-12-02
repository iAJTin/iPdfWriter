
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core;
using iTin.Core.ComponentModel;
using iTin.Core.IO.Compression;

namespace iPdfWriter.Operations.Result.Output
{
    using Abstractions.Writer.Operations.Actions;
    using Abstractions.Writer.Operations.Results;

    using ComponentModel;
    using Config;

    /// <summary>
    /// Represents configuration information for an object <see cref="PdfOutputResultData"/>.
    /// </summary>
    public class PdfOutputResultData : IPdfOutputResultData
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfOutputResultData"/> class.
        /// </summary>
        public PdfOutputResultData()
        {
            Zipped = false;
        }

        #endregion

        #region interfaces

        #region IPdfOutputResultData

        /// <summary>
        /// Gets a value indicating type of output file.
        /// </summary>
        /// <value>
        /// One of the values of the <see cref="KnownOutputType"/> enumeration.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        Enum IOutputResultData.OutputType => OutputType;

        #endregion

        #region public readonly properties

        /// <summary>
        /// Gets or sets a value indicating whether output file has been zipped.
        /// </summary>
        /// <value>
        /// <b>true</b> if output file has been zipped; otherwise, <b>false</b>.
        /// </value>
        public bool IsZipped => Zipped;

        /// <summary>
        /// Gets a value indicating type of output file.
        /// </summary>
        /// <value>
        /// One of the values of the <see cref="KnownOutputType"/> enumeration.
        /// </value>
        public KnownOutputType OutputType => 
            Zipped 
                ? KnownOutputType.Zip 
                : KnownOutputType.Pdf;

        #endregion

        #region public methods

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
        public IResult Action(IOutputAction output) => ActionImplStrategy(output, this);

        /// <summary>
        /// Returns a reference to the output stream.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains a reference to the output stream.
        /// </returns>
        public Stream GetOutputStream() => new MemoryStream(OutputStream.AsByteArray());

        /// <summary>
        /// Returns a reference to the uncompressed output stream. If <see cref="OutputType"/> is <b>Pdf</b> this value is equals to <see cref="OutputStream"/> value property.
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains the original outupt uncrompressed.
        /// </returns>
        public Stream GetUnCompressedOutputStream() => new MemoryStream(UncompressOutputStream.AsByteArray());

        #endregion

        #region public async methods

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
        public async Task<IResult> Action(IOutputActionAsync output, CancellationToken cancellationToken = default) => 
            await ActionImplStrategyAsync(output, this, cancellationToken);

        /// <summary>
        /// Returns a reference to the output stream asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="Stream"/> that contains a reference to the output stream.
        /// </returns>
        public async Task<Stream> GetOutputStreamAsync(CancellationToken cancellationToken = default) => 
            new MemoryStream(await OutputStream.AsByteArrayAsync(cancellationToken));

        /// <summary>
        /// Returns a reference to the uncompressed output stream. If <see cref="OutputType"/> is <b>Pdf</b> this value is equals to <see cref="OutputStream"/> value property asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="Stream"/> that contains the original outupt uncrompressed.
        /// </returns>
        public async Task<Stream> GetUnCompressedOutputStreamAsync(CancellationToken cancellationToken = default) => 
            new MemoryStream(await UncompressOutputStream.AsByteArrayAsync(cancellationToken));

        #endregion

        #endregion

        #region internal properties

        /// <summary>
        /// Gets or sets the pdf configuration settings to apply.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IPdfObjectConfig Configuration { get; set; }

        /// <summary>
        /// Gets a value that contains a reference to the output stream.
        /// </summary>
        /// <value>
        /// A <see cref="Stream"/> that contains a reference to the output stream.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal Stream OutputStream
        {
            get
            {
                if (Configuration == null)
                {
                    return UncompressOutputStream;
                }

                return Zipped
                    ? UncompressOutputStream.AsZipStream()
                    : UncompressOutputStream;
            }
        }


        /// <summary>
        /// Gets a value that contains a reference to the uncompressed output stream. If <see cref="OutputType"/> is <b>Pdf</b> this value is equals to <see cref="OutputStream"/> value property.
        /// </summary>
        /// <value>
        /// A <see cref="Stream"/> that contains a reference to the uncompressed output stream.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal Stream UncompressOutputStream { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether output file has been zipped.
        /// </summary>
        /// <value>
        /// <b>true</b> if output file has been zipped; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal bool Zipped { get; set; }

        #endregion

        #region public override methods

        /// <inheritdoc/>
        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() => 
            $"IsZipped={IsZipped}, OutputType={OutputType}";

        #endregion

        #region private static methods

        private static IResult ActionImplStrategy(IOutputAction output, IOutputResultData result) =>
            output == null
                ? OutputResult.NullResult
                : output.Execute(result);

        #endregion

        #region private async static methods

        private static async Task<IResult> ActionImplStrategyAsync(IOutputActionAsync output, IOutputResultData result, CancellationToken cancellationToken) =>
            output == null
                ? await Task.FromResult(OutputResult.NullResult)
                : await output.ExecuteAsync(result, cancellationToken);

        #endregion
    }
}
