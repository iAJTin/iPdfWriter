
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;

using iTin.Core.Helpers;

namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// class which encapsules the behavior of send mail.
    /// </summary>
    public class SmtpMail
    {
        #region private readonly fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly SmtpMailSettings _settings;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="Mail"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public SmtpMail(SmtpMailSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Sends mail with specified credential synchronously.
        /// </summary>
        /// <param name="message">Message to send.</param>
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
        public IResult SendMail(MailMessage message)
        {
            SentinelHelper.ArgumentNull(message, nameof(message));         

            using (var client = new SmtpClient(_settings.Credential.Host, _settings.Credential.Port))
            {
                client.EnableSsl = _settings.Credential.UseSsl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                bool userNameIsEmpty = string.IsNullOrEmpty(_settings.Credential.UserName);
                if (!userNameIsEmpty)
                {
                    client.Credentials = new NetworkCredential(_settings.Credential.UserName, _settings.Credential.Password, _settings.Credential.Domain);
                }

                try
                {
                    client.Send(message);
                }
                catch(Exception ex)
                {
                    BooleanResult.FromException(ex);
                }

                message.Dispose();
            }

            return BooleanResult.SuccessResult;
        }

        #endregion

        #region public async methods

        /// <summary>
        /// Sends mail with specified credential synchronously.
        /// </summary>
        /// <param name="message">Message to send.</param>
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
        public async Task<IResult> SendMailAsync(MailMessage message)
        {
            SentinelHelper.ArgumentNull(message, nameof(message));

            using var client = new SmtpClient(_settings.Credential.Host, _settings.Credential.Port)
            {
                EnableSsl = _settings.Credential.UseSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            var userNameIsEmpty = string.IsNullOrEmpty(_settings.Credential.UserName);
            if (!userNameIsEmpty)
            {
                client.Credentials = new NetworkCredential(_settings.Credential.UserName, _settings.Credential.Password, _settings.Credential.Domain);
            }

            ServicePointManager.ServerCertificateValidationCallback = AcceptAllCertifications;
            client.SendCompleted += SendCompletedCallback;

            try
            {
                client.SendAsync(message, message);

                return await Task.FromResult(BooleanResult.SuccessResult);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BooleanResult.FromException(ex));
            }
        }
        
        #endregion

        #region private static methods

        private static bool AcceptAllCertifications(object sender, X509Certificate certification, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            var smtp = (SmtpClient)sender;
            var message = (MailMessage)e.UserState;

            if (e.Cancelled)
            {
            }

            if (e.Error != null)
            {
                // logger_.Error(e.Error.ToString());
            }
            else
            {
                smtp?.Dispose();
                message?.Dispose();
            }
        }

        #endregion
    }
}
