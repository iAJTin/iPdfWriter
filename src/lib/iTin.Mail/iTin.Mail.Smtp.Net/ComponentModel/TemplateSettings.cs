
namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// Defines templates
    /// </summary>
    public class TemplateSettings
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateSettings"/> class.
        /// </summary>
        public TemplateSettings()
        {
            IsBodyHtml = true;
            BodyTemplate = string.Empty;
            SubjectTemplate = string.Empty;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the message body template.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the message body template.
        /// </value>
        public string BodyTemplate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the body content is of type <b>HTML</b>
        /// </summary>
        /// <value>
        /// <b>true</b> if body content is <b>HTML</b>; otherwise <b>false</b>.
        /// </value>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Gets or sets the subject line template for this e-mail message.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the subject template content.
        /// </value>
        public string SubjectTemplate { get; set; }

        #endregion
    }
}
