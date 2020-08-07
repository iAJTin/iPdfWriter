
namespace iTin.Core.Models.Design.Enums
{
    using System;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Represents a boolean value.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum YesNo
    {
        /// <summary>
        /// Represents the boolean value <b>true</b>.
        /// </summary>
        Yes,

        /// <summary>
        /// Represents the boolean value <b>false</b>.
        /// </summary>
        No
    }
}
