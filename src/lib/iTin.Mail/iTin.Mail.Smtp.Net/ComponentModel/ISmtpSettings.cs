
namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// Defines <b>SMTP</b> general settings.
    /// </summary>
    public interface ISmtpSettings
    {
        /// <summary>
        /// Gets domain or computer name that verifies the credential. This value is optional.
        /// </summary>
        /// <value> 
        /// The name of the domain associated with the credential.
        /// </value> 
        string SmtpDomain { get; }

        /// <summary>
        /// Gets name or IP address of the host used for <b>SMTP</b> transactions.
        /// </summary>
        /// <value> 
        /// A <see cref="string"/> that represents name or IP address of the host used for SMTP transactions.
        /// </value> 
        string SmtpHost { get; }

        /// <summary>
        /// Gets port used for <b>SMTP</b> transactions.
        /// </summary>
        /// <value> 
        /// A <see cref="string"/> that represents valid port used for <b>SMTP</b> transactions.
        /// </value> 
        string SmtpPort { get; }

        /// <summary>
        /// Gets <b>SMTP</b> password associated with the credential.
        /// </summary>
        /// <value> 
        /// A <see cref="string"/> that represents password associated with the credential.
        /// </value> 
        string SmtpPwd { get; }

        /// <summary>
        /// Gets <b>SMTP</b> email associated with the credential.
        /// </summary>
        /// <value> 
        /// A <see cref="string"/> that represents email associated with the credential.
        /// </value> 
        string SmtpEmail { get; }

        /// <summary>
        /// Gets a value indicating whether uses Secure Sockets Layer (SSL) to encrypt the connection.
        /// </summary>
        /// <value> 
        /// <b>true</b> if use SSL; Otherwise, <b>false</b> .
        /// </value> 
        string SmtpSsl { get; }

        /// <summary>
        /// Gets <b>SMTP</b> user name associated with the credential.
        /// </summary>
        /// <value> 
        /// A <see cref="string"/> that represents user name associated with the credential.
        /// </value> 
        string SmtpUser { get; }
    }
}
