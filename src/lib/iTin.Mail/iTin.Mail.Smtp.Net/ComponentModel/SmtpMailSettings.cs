
using System.Collections.ObjectModel;
using System.Linq;

using iTin.Core;

namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// Class which encapsules the behavior of send mail.
    /// </summary>
    public class SmtpMailSettings
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="SmtpMailSettings"/> class.
        /// </summary>
        public SmtpMailSettings()
        {
            Attachments = new Collection<string>().ToArray();
        }

        #endregion

        #region public static properties

        /// <summary>
        /// Creates a settings from confoguration information.
        /// </summary>
        /// <param name="configuration">General settings</param>
        /// <returns>
        /// A new <see cref="SmtpMailSettings"/>.
        /// </returns>
        public static SmtpMailSettings CreateFromConfiguration(IMailSmtpSettings configuration)
        {
            return new SmtpMailSettings
            {
                Credential =
                    new SmtpCredential
                    {
                        Port = int.Parse(configuration.SmtpPort),
                        UseSsl = configuration.SmtpSsl.AsBoolean(),
                        Host = configuration.SmtpHost,
                        Password = configuration.SmtpPwd,
                        UserName = configuration.SmtpUser,
                        Email = configuration.SmtpEmail
                    },
                Templates =
                    new TemplateSettings
                    {
                        BodyTemplate = configuration.EmailBodyTemplate,
                        SubjectTemplate = configuration.EmailSubjectTemplate
                    }
            };
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a reference to server mail credential.
        /// </summary>
        /// <value>
        /// <see cref="SmtpCredential" /> which contains mail server credential.
        /// </value>
        public SmtpCredential Credential { get; set; }

        /// <summary>
        /// Gets or sets a reference to email message.
        /// </summary>
        /// <value>
        /// <see cref="TemplateSettings" /> which contains email message.
        /// </value>
        public TemplateSettings Templates { get; set; }

        /// <summary>
        /// Gets or sets a reference to email recipients 
        /// </summary>
        /// <value>
        /// <see cref="RecipientsSettings"/> which contains a reference to email recipients.
        /// </value>
        public RecipientsSettings Recipients { get; set; }

        /// <summary>
        /// Gets or sets a reference attached files collection.
        /// </summary>
        /// <value>
        /// <see cref="string"/>[] which contains a reference to attached files collection.
        /// </value>
        public string[] Attachments { get; set; }

        #endregion
    }
}
