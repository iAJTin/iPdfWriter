
namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// Defines general template settings of a email.
    /// </summary>
    public interface IMailTemplateSettings
    {
        /// <summary>
        /// Gets the email body template.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.String" /> that contains the email body template.
        /// </value>
        string EmailBodyTemplate { get; }

        /// <summary>
        /// Gets the email subject template.
        /// </summary>
        /// <value>
        /// A <see cref="T:System.String" /> that contains the email subject template.
        /// </value>
        string EmailSubjectTemplate { get; }
    }
}
