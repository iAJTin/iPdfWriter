
namespace iTin.Core.Drawing.ComponentModel
{
    using Core.ComponentModel;

    /// <summary>
    /// Provides a type converter to convert objects of type <see cref="T:iTin.Core.Drawing.ComponentModel.EffectType" /> to <see cref="T:System.String" />
    /// and from <see cref="T:System.String" /> 'to' <see cref="T:iTin.Core.Drawing.KnownEffectType" />.
    /// </summary>
    /// <remarks>
    /// This converter obtains the value by reflection from the attribute <see cref="T:iTin.Core.ComponentModel.EnumDescriptionAttribute" /> associated with
    /// the type <see cref="T:iTin.Core.Drawing.ComponentModel.EffectType" />.
    /// </remarks>
    public class EffectTypeConverter : EnumDescriptionConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:iTin.Core.Drawing.ComponentModel.EffectTypeConverter"/> class for type <see cref="T:iTin.Core.Drawing.ComponentModel.EffectType" />.
        /// </summary>
        public EffectTypeConverter() : base(typeof(EffectType))
        {
        }
    }
}
