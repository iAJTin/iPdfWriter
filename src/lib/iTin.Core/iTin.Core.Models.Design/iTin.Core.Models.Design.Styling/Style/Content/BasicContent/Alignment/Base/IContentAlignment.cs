
namespace iTin.Core.Models.Design.Styling
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    using Newtonsoft.Json;

    using Enums;

    /// <summary>
    /// Defines a generic content alignment.
    /// </summary>
    public interface IContentAlignment : ICombinable<IContentAlignment>, ICloneable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        bool IsDefault { get; }

        /// <summary>
        /// Gets a value indicating whether this style is an empty style.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty style; otherwise, <b>false</b>.
        /// </value>       
        bool IsEmpty { get; }

        /// <summary>
        /// Gets the parent element of the element.
        /// </summary>
        /// <value>
        /// The element that represents the container element of the element.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        [Browsable(false)]
        IParent Parent { get; }



        /// <summary>
        /// Gets or sets preferred horizontal alignment.
        /// </summary>
        /// <value>
        /// Preferred horizontal alignment.
        /// </value>
        [JsonProperty("horizontal")]
        KnownHorizontalAlignment Horizontal { get; set; }



        /// <summary>
        /// Sets the parent element of the element.
        /// </summary>
        /// <param name="reference">Reference to parent.</param>
        void SetParent(IParent reference);
    }
}
