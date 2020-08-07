
namespace iTin.Core.Models.Design.Enums
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Enumeración que contiene las políticas a aplicar para el eje vertical.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownVerticalAxisPolicy
    {
        /// <summary>
        /// Cada serie tiene su popria escala.
        /// </summary>
        Auto,

        /// <summary>
        /// Establece los valores de ambos ejes verticales de las series al que sea mayor de los dos.
        /// </summary>
        AutoChoice,

        /// <summary>
        /// Establece los valores del eje vertical primario en el eje vertical secundario de la serie.
        /// </summary>
        ReferenceToPrimaryVerticalAxis,

        /// <summary>
        /// Establece los valores del eje vertical secundario en el eje vertical primario de la serie.
        /// </summary>
        ReferenceToSecondaryVerticalAxis,
    }
}
