
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iTin.Core.Models.Design.Enums
{
    /// <summary>
    /// Specifies known border positions
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownBorderPosition
    {
        /// <summary>
        /// Left border.
        /// </summary>
        Left,

        /// <summary>
        /// Top border.
        /// </summary>
        Top,

        /// <summary>
        /// Right border.
        /// </summary>
        Right,

        /// <summary>
        /// Bottom border.
        /// </summary>
        Bottom        
    }
}
