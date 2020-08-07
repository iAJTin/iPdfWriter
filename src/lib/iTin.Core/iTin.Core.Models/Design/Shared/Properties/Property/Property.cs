
namespace iTin.Core.Models
{
    using System;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines a user custom property.
    /// </summary>
    public partial class Property : ICloneable
    {
        #region interfaces

        #region ICloneable

        #region explicit

        #region (object) ICloneable.Clone(): Creates a new object that is a copy of the current instance
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

        #endregion

        #region public properties

        #region [public] (string) Name: Gets or sets the custom property name
        /// <summary>
        /// Gets or sets the custom property name.
        /// </summary>
        /// <value>
        /// Property name
        /// </value>
        [XmlAttribute]
        [JsonProperty("name")]
        public string Name { get; set; }
        #endregion

        #region [public] (string) Value: Gets or sets the custom property value
        /// <summary>
        /// Gets or sets the custom property value.
        /// </summary>
        /// <value>
        /// Property value
        /// </value>
        [XmlAttribute]
        [JsonProperty("value")]
        public string Value { get; set; }
        #endregion

        #region [public] (Properties) Owner: Gets the element that owns this
        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public Properties Owner { get; private set; }

        #endregion

        #endregion

        #region public methods

        #region [public] (Property) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public Property Clone() => (Property)MemberwiseClone();
        #endregion

        #region [public] (void) SetOwner(Properties): Sets the element that owns this
        internal void SetOwner(Properties reference) => Owner = reference;
        #endregion

        #endregion

        #region public overrides methods

        #region [public] {override} (string) ToString(): Returns a string that represents the current object
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the current object.
        /// </returns>
        public override string ToString() => $"Name=\"{Name}\"";
        #endregion

        #endregion
    }
}
