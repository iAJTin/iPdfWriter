﻿
namespace iTin.Core.Models.Design.Enums
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Specifies content data type.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownBasicColor
    {
        /// <summary>
        /// Gets a system-defined color that has an RGB value of #000000.
        /// </summary>
        Black,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #0000FF.
        /// </summary>
        Blue,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #00FF00.
        /// </summary>
        Green,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #00FFFF.
        /// </summary>
        Cyan,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #FF0000.
        /// </summary>
        Red,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #FF00FF.
        /// </summary>
        Magenta,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #FFFF00.
        /// </summary>
        Yellow,

        /// <summary>
        /// Gets a system-defined color that has an RGB value of #FFFFFF.
        /// </summary>
        White
    }
}
