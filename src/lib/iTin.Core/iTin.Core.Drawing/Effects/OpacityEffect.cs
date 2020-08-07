
namespace iTin.Core.Drawing.Effects
{
    using System.Diagnostics;
    using System.Drawing.Imaging;

    using Core.Helpers;

    using ComponentModel;
    using Helpers;

    /// <summary>
    /// A Specialization of <see cref="IEffect"/> interface.<br/>
    /// Which represents opacity effect.
    /// </summary>
    public class OpacityEffect : IEffect
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const float DefaultPercent = 100.0f;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _percent;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpacityEffect"/> class.
        /// </summary>
        public OpacityEffect()
        {
            PercentValue = DefaultPercent;
        }

        /// <summary>
        /// Gets or sets opacity value expressed as value between 0 and 100 
        /// </summary>
        /// <value>
        /// A <see cref="float"/> that represents the opacity value.
        /// </value>
        public float PercentValue
        {
            get => _percent;
            set
            {
                SentinelHelper.ArgumentOutOfRange("value", value, 0.0f, 100.0f, "El valor debe estar comprendido entre 0 y 100");

                _percent = value;
            }
        }

        /// <summary>
        /// Applies this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="ImageAttributes"/> that represents this effect.
        /// </returns>
        public ImageAttributes Apply() => ImageHelper.GetImageAttributesFromOpacityValueEffect(_percent);
    }
}
