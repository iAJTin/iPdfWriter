
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace iTin.Core.Models.Design.Enums
{
    /// <summary>
    /// Specifies how an object or text is vertically aligned relative to a content
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownVerticalAlignment
    {
        /// <summary>
        /// Content is aligned vertically at the top.
        /// </summary>
        Top,

        /// <summary>
        /// Content is aligned vertically at the center.
        /// </summary>        
        Center,

        /// <summary>
        /// Content is aligned vertically at the bottom.
        /// </summary>
        Bottom
    }
}
