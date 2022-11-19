
namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// Defines credential data
    /// </summary>
    public class SmtpCredential
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpCredential"/> class.
        /// </summary>
        public SmtpCredential()
        {
            Domain = string.Empty;
            Host = "smtp.gmail.com";
            Password = string.Empty;
            Port = 587;
            UseSsl = true;
            UserName = string.Empty;
            Email = string.Empty;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets domain or computer name that verifies the credential.
        /// </summary>
        /// <value>
        /// The name of the domain associated with the credential. The default is an empty string ("").
        /// </value>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets name or IP address of the host used for SMTP transactions.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the name or IP address of the computer to use for SMTP transactions. The default is <b>smtp.gmail.com</b>.
        /// </value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets password for the user name associated with the credential.
        /// </summary>
        /// <value>
        /// The password associated with the credential.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets port used for SMTP transactions.
        /// </summary>
        /// <value>
        /// An <see cref="T:System.Int32"/> that contains the port number on the SMTP host. The default value is <b>587</b>.
        /// </value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether uses Secure Sockets Layer (SSL) to encrypt the connection.
        /// </summary>
        /// <value>
        /// <b>true</b> if use <b>SSL</b>; Otherwise, <b>false</b>.
        /// </value>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Gets or sets user name associated with the credential.
        /// </summary>
        /// <value>
        /// The preferred previous delimiter for field.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets email associated with the credential.
        /// </summary>
        /// <value>
        /// The preferred previous delimiter for field.
        /// </value>
        public string Email { get; set; }

        #endregion
    }
}
