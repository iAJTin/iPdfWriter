
using System.Collections.ObjectModel;
using System.Linq;

namespace iTin.Mail.Smtp.Net
{
    /// <summary>
    /// Defines templates
    /// </summary>
    public class RecipientsSettings
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="RecipientsSettings"/> class.
        /// </summary>
        public RecipientsSettings()
        {
            CCAddresses = new Collection<string>().ToArray();
            ToAddresses = new Collection<string>().ToArray();
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the <b>CC</b> email addresses.
        /// </summary>
        /// <value>
        /// The <b>CC</b> email addresses.
        /// </value>
        public string[] CCAddresses { get; set; }

        /// <summary>
        /// Gets or sets the <b>To</b> email addresses.
        /// </summary>
        /// <value>
        /// The <b>To</b> email addresses.
        /// </value>
        public string[] ToAddresses { get; set; }

        #endregion
    }
}
