
using System.Drawing.Drawing2D;

using iTin.Core.Drawing.ComponentModel;

namespace iTin.Core.Drawing
{
    /// <summary>
    /// Static class than contains extension methods for structures of type <see cref="ImageStyle"/>.
    /// </summary> 
    public static class ImageStyleExtensions
    {
        /// <summary>
        /// Equivalence between the types <see cref="ImageStyle"/> and <see cref="WrapMode"/>.
        /// </summary>
        /// <param name="style">One of the values of the enumeration <see cref="ImageStyle"/> that represents the alignment of the images on the surface of the control.</param>
        /// <returns>
        /// Equivalent style
        /// </returns>
        public static WrapMode ToWrapMode(this ImageStyle style)
        {
            switch (style)
            {
                case ImageStyle.Tile:
                    return WrapMode.Tile;

                case ImageStyle.TileFlipX:
                    return WrapMode.TileFlipX;

                case ImageStyle.TileFlipY:
                    return WrapMode.TileFlipY;

                case ImageStyle.TileFlipXY:
                    return WrapMode.TileFlipXY;

                default:
                    return WrapMode.Clamp;
            }
        }
    }
}
