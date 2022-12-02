
using System;
using System.Diagnostics;
using iTin.Core;
using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;
using iTin.Core.IO;
using iTin.Core.IO.Compression;

using iTextSharp.text.pdf;

using iPdfWriter.Abstractions.Writer.Operations.Actions;
using iPdfWriter.Abstractions.Writer.Operations.Results;
using iPdfWriter.ComponentModel;
using iPdfWriter.Operations.Result.Output;

using NativeIO = System.IO;

namespace iPdfWriter.Operations.Actions
{
    /// <inheritdoc/>
    /// <summary>
    /// Defines allowed actions for output result data
    /// </summary>
    public class SaveToFile : IOutputAction
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

        /// <inheritdoc />
        /// <summary>
        /// Execute action for specified output result data.
        /// </summary>
        /// <param name="data">Target output result data.</param>
        /// <returns>
        /// <para>
        /// An instance which implements the <see cref="iTin.Core.ComponentModel.IResult{T}"/> interface that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// </returns>
        public IResult Execute(IOutputResultData data) => ExecuteImpl(data);

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

        private IResult ExecuteImpl(IOutputResultData data)
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
                var outputStreamToUse = GenerateOutputStream(pdfOutputResultData, Password);
                var actionResult = BooleanResult.SuccessResult;
                var isMergedFile = pdfOutputResultData.Configuration is PdfObjectConfig;
                if (isMergedFile)
                {
                    var streamIsZipped = ((PdfObjectConfig)pdfOutputResultData.Configuration).AllowCompression;
                    actionResult.Success = pdfOutputResultData.Zipped
                        ? streamIsZipped
                            ? outputStreamToUse.TrySaveAsZip(PdfExtension, outPath).Success
                            : outputStreamToUse.SaveToFile(outPath, safeOptions).Success
                        : outputStreamToUse.SaveToFile(outPath, safeOptions).Success;
                }
                else
                {
                    actionResult.Success = outputStreamToUse.SaveToFile(outPath, safeOptions).Success;
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

        private static NativeIO.Stream GenerateOutputStream(PdfOutputResultData data, string password)
        {
            var resultStream = data.Zipped
                ? data.UncompressOutputStream
                : data.OutputStream;

            if (string.IsNullOrEmpty(password))
            {
                return resultStream;
            }

            using var outputStream = new NativeIO.MemoryStream();
            using var reader = new PdfReader(data.GetOutputStream());
            PdfEncryptor.Encrypt(reader, outputStream, true, password, null, PdfWriter.ALLOW_SCREENREADERS);

            return new NativeIO.MemoryStream(outputStream.GetBuffer()).Clone();
        }

        #endregion
    }
}
