﻿
namespace iTin.Core.Drawing.Effects
{
    using System.Drawing.Imaging;

    using ComponentModel;
    using Helpers;

    /// <summary>
    /// A Specialization of <see cref="IEffect"/> interface.<br/>
    /// Which represents gray-scale effect.
    /// </summary>
    public class GrayScaleEffect : IEffect
    {
        /// <summary>
        /// Gets the manipulation of the colors in an image to an effect.
        /// </summary>
        /// <returns>
        /// A <see cref="ImageAttributes"/> object that contains the information about how bitmap colors are manipulated.
        /// </returns>
        public ImageAttributes Apply() => ImageHelper.GetImageAttributesFromEffect(EffectType.GrayScale);
    }
}
