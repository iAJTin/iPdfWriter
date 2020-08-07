
namespace iTin.Core.Drawing.ComponentModel
{
    /// <summary>
    /// Specifies the style of dashed lines drawn with a <see cref="System.Drawing.Pen"/> object.
    /// </summary>
    public enum DashStyleEx
    {
        /// <summary>
        /// Specify a continuous line.
        /// </summary>
        Solid,

        /// <summary>
        /// Specifies a line consisting of hyphens.
        /// </summary>
        Dash,

        /// <summary>
        /// Specifies a line formed by points.
        /// </summary>
        Dot,

        /// <summary>
        /// Specifies a line formed by a repeating hyphen and point model.
        /// </summary>
        DashDot,

        /// <summary>
        /// Specifies a line formed by a repeating hyphen, dot, and dot model.
        /// </summary>
        DashDotDot
    }
}
