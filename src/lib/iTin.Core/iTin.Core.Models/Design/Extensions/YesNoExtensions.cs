
using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design
{
    /// <summary>
    /// Extensions methods for <see cref="T:iTin.Core.Models.Design.Enums.YesNo"/> enumerated type. 
    /// </summary>
    public static class YesNoExtensions
    {
        /// <summary>
        /// Converts a value of the enumerated type <see cref="T:iTin.Core.Models.Design.Enums.YesNo"/> to boolean representation.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>
        /// <b>true</b> if value is <b>Yes</b>; otherwise, <b>false</b>.
        /// </returns>
        public static bool AsBoolean(this YesNo value) => value == YesNo.Yes;
    }
}
