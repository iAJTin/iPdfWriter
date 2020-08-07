
namespace iTin.Core.Drawing.ComponentModel
{
    using System.Drawing;

    /// <summary>
    /// Sets the smoothing quality of lines and curves for the graphic context in <see cref="SmoothingModeEx.HighQuality"/>.
    /// </summary>
    public class AntiAlias : Smoothing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AntiAlias"/> class by setting the smoothing quality to <see cref="SmoothingModeEx.HighQuality"/>.
        /// </summary>
        /// <param name="graphics">Surface <see cref="Graphics"/> on which to draw.</param>
        public AntiAlias(Graphics graphics) : base(graphics, SmoothingModeEx.HighQuality)
        {
        }
    }
}
