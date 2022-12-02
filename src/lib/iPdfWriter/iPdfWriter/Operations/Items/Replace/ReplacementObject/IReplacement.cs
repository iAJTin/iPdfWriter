
using iTin.Core.Models.Design.Enums;

namespace iPdfWriter.Operations.Replace.Replacement
{
    /// <summary>
    /// Defines allowed actions for bookmark replacement object
    /// </summary>
    public interface IReplacement
    {
        /// <summary>
        /// Gets or sets a value that represents text replace options.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains text options.
        /// </value>
        ReplaceTextOptions ReplaceOptions { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the elements to be inserted are shown with a red border that identifies their position and size in order to validate that they are correct. The default value is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if works in mode test; otherwise <see cref="YesNo.No"/>.
        /// </value>
        public YesNo UseTestMode { get; set; }
    }
}
