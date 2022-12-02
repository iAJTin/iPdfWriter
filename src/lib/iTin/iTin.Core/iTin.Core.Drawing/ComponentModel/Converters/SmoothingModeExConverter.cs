
using iTin.Core.ComponentModel;

namespace iTin.Core.Drawing.ComponentModel
{
    /// <summary>
    /// Provides a type converter to convert objects of type <see cref="T:iTin.Core.Drawing.ComponentModel.SmoothingModeEx" /> to <see cref="T:System.String" /> and from <see cref="T:System.String" /> to <see cref="T:iTin.Core.Drawing.ComponentModel.SmoothingModeEx" />.
    /// </summary>
    /// <remarks>
    /// This converter obtains the value by reflection from the attribute <see cref="T:iTin.Core.EnumDescriptionAttribute" /> associated with the type <see cref="T: iTin.Core.Drawing.ComponentModel.SmoothingModeEx" />.
    /// </remarks>
    public class SmoothingModeExConverter : EnumDescriptionConverter
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="T:iTin.Core.Drawing.ComponentModel.SmoothingModeExConverter"/> para el tipo <see cref="T:iTin.Core.Drawing.ComponentModel.SmoothingModeEx" />.
        /// </summary>
        public SmoothingModeExConverter() : base(typeof(SmoothingModeEx)) { }
    }
}
