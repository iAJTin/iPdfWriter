
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iTin.Core.Models.Design.Enums
{
    /// <summary>
    /// Specifies how an object or text is horizontally aligned relative to a content
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownHorizontalAlignment
    {
        /// <summary>
        /// Content is aligned horizontally on the left.
        /// </summary>
        Left,

        /// <summary>
        /// Content is aligned horizontally in the center.
        /// </summary>
        Center,

        /// <summary>
        /// Content is aligned horizontally on the right.
        /// </summary>
        Right
    }
}
