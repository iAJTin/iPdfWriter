
using iTin.Core.ComponentModel;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// Defines the system replaceable tags.
    /// </summary>
    public enum SystemTags
    {
        /// <summary>
        /// Replace page number tag
        /// </summary>
        [EnumDescription("none")]
        None,

        /// <summary>
        /// Replace page number tag
        /// </summary>
        [EnumDescription("#$pn$#")]
        PageNumber,

        /// <summary>
        /// Replace total pages number tag
        /// </summary>
        [EnumDescription("#$tp$#")]
        TotalPages
    }
}
