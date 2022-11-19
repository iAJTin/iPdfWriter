
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

using iTin.AspNet.Web;

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
    public class Download : IOutputAction
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
        /// <param name="context">Target output result data.</param>
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
        public IResult Execute(IOutputResultData context) => ExecuteImpl(context);

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
        /// Gets or sets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename { get; set; }

        #endregion

        #region private methods   

        private IResult ExecuteImpl(IOutputResultData data)
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

                data.GetOutputStream().Download(downloadFilename, Context.Response);

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
