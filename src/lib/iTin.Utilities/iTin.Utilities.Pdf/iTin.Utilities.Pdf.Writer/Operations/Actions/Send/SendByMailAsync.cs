
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;

using iTin.Mail.Smtp.Net;

using iTin.Utilities.Writer.Abstractions.Operations.Actions;
using iTin.Utilities.Writer.Abstractions.Operations.Results;

using iTinIO = iTin.Core.IO;

namespace iTin.Utilities.Pdf.Writer.Operations.Result.Actions
{
    /// <inheritdoc/>
    /// <summary>
    /// Specialization of <see cref="IOutputAction"/> interface that send the file by email.
    /// </summary>
    /// <seealso cref="IOutputAction"/>
    public class SendByMailAsync : IOutputActionAsync
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
        /// Gets or sets the email settings
        /// </summary>
        /// <value>
        /// The email settings.
        /// </value>
        public SmtpMailSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the email settings
        /// </summary>
        /// <value>
        /// The email settings.
        /// </value>
        public string FromAddress { get; set; }

        /// <summary>
        /// Gets or sets the display name for <see cref="FromAddress"/> email address.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string FromDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the attached filename.
        /// </summary>
        /// <value>
        /// The attached filename.
        /// </value>
        public string AttachedFilename { get; set; }

        #endregion

        #region private methods

        private async Task<IResult> ExecuteImplAsync(IOutputResultData data, CancellationToken cancellationToken = default)
        {
            if (data == null)
            {
                return BooleanResult.NullResult;
            }

            if (Settings == null)
            {
                return BooleanResult.CreateErrorResult("Missing a valid settings");
            }

            try
            {
                var message = new MailMessage
                {
                    Subject = Settings.Templates.SubjectTemplate,
                    Body = Settings.Templates.BodyTemplate,
                    IsBodyHtml = Settings.Templates.IsBodyHtml,
                    From = new MailAddress(FromAddress, FromDisplayName),
                };

                foreach (var to in Settings.Recipients.ToAddresses)
                {
                    message.To.Add(new MailAddress(to));
                }

                foreach (var cc in Settings.Recipients.CCAddresses)
                {
                    message.CC.Add(new MailAddress(cc));
                }

                var fileExtension = data.IsZipped ? ZipExtension : PdfExtension;
                var filename = Path.ChangeExtension(AttachedFilename, fileExtension);
                message.Attachments.Add(new Attachment(await data.GetOutputStreamAsync(cancellationToken), filename));

                foreach (var attachment in Settings.Attachments)
                {
                    var filenameNormalized = iTinIO.Path.PathResolver(attachment);
                    var fi = new FileInfo(filenameNormalized);
                    if (!fi.Exists)
                    {
                        continue;
                    }

                    message.Attachments.Add(new Attachment(fi.Open(FileMode.Open, FileAccess.Read, FileShare.Read), fi.Name));
                }

                var mail = new SmtpMail(Settings);
                await mail.SendMailAsync(message);

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
