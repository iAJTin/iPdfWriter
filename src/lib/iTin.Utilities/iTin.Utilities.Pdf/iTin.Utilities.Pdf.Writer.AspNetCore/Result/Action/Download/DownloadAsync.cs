
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using iTin.AspNet;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;

using iTin.Utilities.Writer.Abstractions.Operations.Actions;
using iTin.Utilities.Writer.Abstractions.Operations.Results;

using iTinIO = iTin.Core.IO;

namespace iTin.Utilities.Pdf.Writer.Operations.Result.Actions
{
    /// <inheritdoc/>
    /// <summary>
    /// Specialization of <see cref="IOutputAction"/> interface that downloads the file.
    /// </summary>
    /// <seealso cref="IOutputAction"/>
    public class DownloadAsync : IOutputActionAsync
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string PdfExtension = "pdf";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string ZipExtension = "zip";

        #endregion

        #region interfaces

        #region IOutputActionAsync

        #region public async methods   

        /// <summary>
        /// Execute action asynchronously for specified output result.
        /// </summary>
        /// <param name="context">Target output result data.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// <para>
        /// A <see cref="BooleanResult"/> which implements the <see cref="IResult"/> interface reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="bool"/>, which contains the operation result
        /// </para>
        /// </returns>
        public async Task<IResult> ExecuteAsync(IOutputResultData context, CancellationToken cancellationToken = default) =>
            await ExecuteImplAsync(context, cancellationToken);

        #endregion

        #endregion

        #endregion

        #region public properties   

        /// <summary>
        /// Gets or sets the current http context.
        /// </summary>
        /// <value>
        /// The current http context.
        /// </value>
        public HttpContext Context { get; set; }

        /// <summary>
        /// Gets or sets the filename output path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.
        /// </summary>
        /// <value>
        /// The output path.
        /// </value>
        public string Filename { get; set; }

        #endregion

        #region private async methods   

        private async Task<IResult> ExecuteImplAsync(IOutputResultData data, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                return BooleanResult.NullResult;
            }

            try
            {
                var safeFilename = Filename;
                if (string.IsNullOrEmpty(Filename))
                {
                    safeFilename = iTinIO.File
                        .GetUniqueTempRandomFile()
                        .Segments
                        .Last();
                }

                var downloadFilename = data.IsZipped 
                    ? Path.ChangeExtension(safeFilename, ZipExtension) 
                    : Path.ChangeExtension(safeFilename, PdfExtension);

                var streamResult = new FileStreamResult(await data.GetOutputStreamAsync(cancellationToken), MimeMapping.GetMimeMapping(data.IsZipped ? ZipExtension : PdfExtension))
                {
                    EnableRangeProcessing = true, 
                    LastModified = DateTimeOffset.Now,
                    FileDownloadName = downloadFilename
                };

                //streamResult.FileStream.Seek(0, SeekOrigin.Begin);

                //MemoryStream ms = new MemoryStream();
                //StreamWriter sw = new StreamWriter(ms);
                //await sw.WriteLineAsync("fdfdfdf");
                //ms.Seek(0, SeekOrigin.Begin);


                //var a = new FileStreamResult(ms,
                //    MimeMapping.GetMimeMapping(data.IsZipped ? ZipExtension : PdfExtension))
                //{
                //    EnableRangeProcessing = false,
                //    FileDownloadName = downloadFilename
                //};

                ////await a.ExecuteResultAsync(new ActionContext { HttpContext = Context });

                await streamResult.ExecuteResultAsync(new ActionContext { HttpContext = Context });

                return BooleanResult.SuccessResult;
            }
            catch (Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }

        #endregion
    }
}
