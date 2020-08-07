
namespace iTin.Core.Drawing.ComponentModel
{
    /// <summary>
    /// Specifies the alignment of the content on the drawing surface.
    /// </summary>
    public enum ImageStyle
    {
        /// <summary>
        /// The image is aligned vertically at the top and horizontally at the left.
        /// </summary>
        TopLeft,

        /// <summary>
        /// The content is aligned vertically at the top and horizontally at the center.
        /// </summary>
        TopMiddle,

        /// <summary>
        /// The image is aligned vertically on the top and horizontally on the right.
        /// </summary>
        TopRight,

        /// <summary>
        /// The image is aligned vertically in the middle and horizontally to the left.
        /// </summary>
        CenterLeft,

        /// <summary>
        /// The image is aligned vertically in the middle and horizontally in the center.
        /// </summary>
        CenterMiddle,

        /// <summary>
        /// The image is aligned vertically in the middle and horizontally to the right.
        /// </summary>
        CenterRight,

        /// <summary>
        /// The image is aligned vertically on the bottom and horizontally on the left.
        /// </summary>
        BottomLeft,

        /// <summary>
        /// The image is aligned vertically at the bottom and horizontally at the center.
        /// </summary>
        BottomMiddle,

        /// <summary>
        /// The image is aligned vertically on the bottom and horizontally on the right.
        /// </summary>
        BottomRight,

        /// <summary>
        /// The image extends across the surface of the control's client rectangle.
        /// </summary>
        Stretch,

        /// <summary>
        /// Tile the image.
        /// </summary>
        Tile,

        /// <summary>
        /// Invert the image horizontally, and then tile it.
        /// </summary>
        TileFlipX,

        /// <summary>
        /// Invert the image horizontally and vertically, and then tile it.
        /// </summary>
        TileFlipXY,

        /// <summary>
        /// Invert the image vertically, and then tile it.
        /// </summary>
        TileFlipY
    }
}
