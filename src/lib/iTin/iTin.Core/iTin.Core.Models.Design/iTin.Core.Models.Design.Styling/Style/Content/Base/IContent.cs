
using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Styling
{
    /// <summary>
    /// Defines a generic content.
    /// </summary>
    public interface IContent : ICombinable<IContent>, ICloneable, IParent
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
        [Browsable(false)]
        [XmlIgnore]
        [JsonIgnore]
        IParent Parent { get; }



        /// <summary>
        /// Gets or sets preferred content color.
        /// </summary>
        /// <value>
        /// Preferred border color.
        /// </value>
        [JsonProperty("color")]
        string Color { get; set; }

        /// <summary>
        /// Gets or sets preferred alternate content color.
        /// </summary>
        /// <value>
        /// Preferred border color.
        /// </value>
        [JsonProperty("alternate-color")]
        string AlternateColor { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether to display the border.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the border; otherwise, <see cref="YesNo.No"/>. 
        /// </value>
        [JsonProperty("show")]
        YesNo Show { get; set; }



        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color"/> structure that represents alternate color for this content.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> structure that represents alternate color for this content.
        /// </returns> 
        Color GetAlternateColor();

        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color"/> structure that represents color for this content.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> structure that represents color for this content.
        /// </returns> 
        Color GetColor();


        /// <summary>
        /// Sets the parent element of the element.
        /// </summary>
        /// <param name="reference">Reference to parent.</param>
        void SetParent(IParent reference);
    }
}
