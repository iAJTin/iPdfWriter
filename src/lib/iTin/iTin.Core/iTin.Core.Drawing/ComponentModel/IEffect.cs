
using System.Drawing.Imaging;

namespace iTin.Core.Drawing.ComponentModel
{
    /// <summary>
    /// Interface IEffect
    /// </summary>
    public interface IEffect
    {
        /// <summary>
        /// Gets the manipulation of the colors in an image to an effect.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Drawing.Imaging.ImageAttributes"/> object that contains the information about how bitmap colors are manipulated. 
        /// </returns>
        ImageAttributes Apply();
    }
}
