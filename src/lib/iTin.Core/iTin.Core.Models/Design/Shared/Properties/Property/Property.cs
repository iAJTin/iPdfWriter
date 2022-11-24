
using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace iTin.Core.Models
{
    /// <summary>
    /// Defines a user custom property.
    /// </summary>
    public partial class Property : ICloneable
    {
        #region interfaces

        #region ICloneable

        #region explicit

        /// <inheritdoc />
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone() => Clone();

        #endregion

        #endregion

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the custom property name.
        /// </summary>
        /// <value>
        /// Property name
        /// </value>
        [XmlAttribute]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the custom property value.
        /// </summary>
        /// <value>
        /// Property value
        /// </value>
        [XmlAttribute]
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public Properties Owner { get; private set; }

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public Property Clone() => (Property)MemberwiseClone();

        internal void SetOwner(Properties reference) => Owner = reference;

        #endregion

        #region public overrides methods

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the current object.
        /// </returns>
        public override string ToString() => $"Name=\"{Name}\"";

        #endregion
    }
}
