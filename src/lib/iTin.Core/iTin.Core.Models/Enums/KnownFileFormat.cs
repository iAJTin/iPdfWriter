
namespace iTin.Core.Models
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Defines input file format for model imports
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownFileFormat
    {
        /// <summary>
        /// File format is <b>XML</b>.
        /// </summary>
        Xml,

        /// <summary>
        /// File format is <b>XML</b>.
        /// </summary>
        Json
    }
}
