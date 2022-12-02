
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iTin.Core.Models.Design.Enums
{
    /// <summary>
    /// Specify a line style.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownLineStyle
    {
        /// <summary>
        /// No line.
        /// </summary>
        None,

        /// <summary>
        /// Continuous line.
        /// </summary>
        Continuous,

        /// <summary>
        /// Dashed line.
        /// </summary>
        Dash,

        /// <summary>
        /// Dash-Dot line.
        /// </summary>
        DashDot,

        /// <summary>
        /// Dash-Dot-Dot line.
        /// </summary>
        DashDotDot,

        /// <summary>
        /// Dot line.
        /// </summary>
        Dot
    }
}
