
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core;
using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;
using iTin.Core.IO;
using iTin.Core.IO.Compression;

using iTin.Utilities.Abstractions.Writer.Operations.Actions;
using iTin.Utilities.Abstractions.Writer.Operations.Results;

using iTin.Utilities.Pdf.Writer.ComponentModel;
using iTin.Utilities.Pdf.Writer.Operations.Result.Output;

using iTextSharp.text.pdf;

using NativeIO = System.IO;

namespace iTin.Utilities.Pdf.Writer.Operations.Result.Actions
{
    /// <inheritdoc/>
    /// <summary>
    /// Defines allowed actions for output result data
    /// </summary>
    public class SaveToFileAsync : IOutputActionAsync
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string PdfExtension = "pdf";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string ZipExtension = "zip";

        #endregion

        #region interfaces

        #region IOutputAction

        #region public methods

        /// <summary>
        /// Execute action for specified output result data.
        /// </summary>
        /// <param name="context">Target output result data.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// <para>
        /// An instance which implements the <see cref="IResult"/> interface that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// </returns>
        public async Task<IResult> ExecuteAsync(IOutputResultData context, CancellationToken cancellationToken = default) =>
            await ExecuteImplAsync(context, cancellationToken);

        #endregion

        #endregion

        #endregion

        #region public properties   

        /// <summary>
        /// Gets or sets the output path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.
        /// </summary>
        /// <value>
        /// The output path.
        /// </value>
        public string OutputPath { get; set; }

        /// <summary>
        /// Defines a password for this output file.
        /// </summary>
        /// <value>
        /// Password file.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Defines file save options. Allows defining if the directory is created automatically if it does not exist.
        /// </summary>
        /// <value>
        /// Save options reference.
        /// </value>
        public SaveOptions SaveOptions { get; set; }

        #endregion

        #region private methods   

        private async Task<IResult> ExecuteImplAsync(IOutputResultData data, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                return BooleanResult.NullResult;
            }

            var pdfOutputResultData = (PdfOutputResultData)data;
            var safeOptions = SaveOptions ?? SaveOptions.Default;
            var outputExtension = pdfOutputResultData.Zipped ? ZipExtension : PdfExtension;
            var normalizedPath = Path.PathResolver(OutputPath);
            var directoryName = NativeIO.Path.GetDirectoryName(normalizedPath);
            var filename = NativeIO.Path.GetFileName(normalizedPath);
            var filenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(filename);
            var filenameWithExtension = $"{filenameWithoutExtension}.{outputExtension}";
            var outPath = NativeIO.Path.Combine(directoryName, filenameWithExtension);

            try
            {
                var outputStreamToUse = await GenerateOutputStreamAsync(pdfOutputResultData, Password, cancellationToken);
                var actionResult = BooleanResult.SuccessResult;
                var isMergedFile = pdfOutputResultData.Configuration is PdfObjectConfig;
                if (isMergedFile)
                {
                    var streamIsZipped = ((PdfObjectConfig)pdfOutputResultData.Configuration).AllowCompression;
                    actionResult.Success = pdfOutputResultData.Zipped
                        ? streamIsZipped
                            ? (await outputStreamToUse.TrySaveAsZipAsync(PdfExtension, outPath, cancellationToken)).Success
                            : (await outputStreamToUse.SaveToFileAsync(outPath, safeOptions, cancellationToken)).Success
                        : (await outputStreamToUse.SaveToFileAsync(outPath, safeOptions, cancellationToken)).Success;
                }
                else
                {
                    actionResult.Success = (await outputStreamToUse.SaveToFileAsync(outPath, safeOptions, cancellationToken)).Success;
                }
                
                return actionResult;
            }
            catch (Exception e)
            {
                return BooleanResult.FromException(e);
            }
        }

        #endregion

        #region private static methods

        private static async Task<NativeIO.Stream> GenerateOutputStreamAsync(PdfOutputResultData data, string password, CancellationToken cancellationToken = default)
        {
            var resultStream = data.Zipped
                ? data.UncompressOutputStream
                : data.OutputStream;

            if (string.IsNullOrEmpty(password))
            {
                return resultStream;
            }

            using var outputStream = new NativeIO.MemoryStream();
            using var reader = new PdfReader(await data.GetOutputStreamAsync(cancellationToken));
            PdfEncryptor.Encrypt(reader, outputStream, true, password, null, PdfWriter.ALLOW_SCREENREADERS);

            return await new NativeIO.MemoryStream(outputStream.GetBuffer()).CloneAsync(cancellationToken);
        }

        #endregion
    }
}
