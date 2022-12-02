
using System.Drawing.Imaging;

using iTin.Core.Drawing.ComponentModel;
using iTin.Core.Drawing.Helpers;

namespace iTin.Core.Drawing.Effects
{
    /// <summary>
    /// A Specialization of <see cref="IEffect"/> interface.<br/>
    /// Which represents gray-scale green effect.
    /// </summary>
    public class GrayScaleGreenEffect : IEffect
    {
        /// <summary>
        /// Gets the manipulation of the colors in an image to an effect.
        /// </summary>
        /// <returns>
        /// A <see cref="ImageAttributes"/> object that contains the information about how bitmap colors are manipulated.
        /// </returns>
        public ImageAttributes Apply() => ImageHelper.GetImageAttributesFromEffect(EffectType.GrayScaleGreen);
    }
}
