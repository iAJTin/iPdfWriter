
namespace iTin.Core.Drawing.ComponentModel
{
    using System.ComponentModel;

    using Core.ComponentModel;

    /// <summary>
    /// Specifies whether the lines and curves and edges of the filled areas are smoothed.
    /// </summary>
    [TypeConverter(typeof(SmoothingModeExConverter))]
    public enum SmoothingModeEx
    {
        /// <summary>
        /// Specifies that the lines are not smoothed.
        /// </summary>
        [EnumDescription("High Speed")]
        HighSpeed,

        /// <summary>
        /// Specify a representation with smoothed lines.
        /// </summary>
        [EnumDescription("High Quality")]
        HighQuality
    }
}
