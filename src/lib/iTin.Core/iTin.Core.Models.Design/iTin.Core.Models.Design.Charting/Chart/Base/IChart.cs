
using System;
using System.ComponentModel;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Charting
{
    /// <summary>
    /// Defines a generic chart
    /// </summary>
    public interface IChart : ICombinable<IChart>, ICloneable
    {
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        bool IsDefault { get; }

        /// <summary>
        /// Gets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <value>
        /// The <see cref="ICharts"/> that owns this <see cref="IChart"/>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        ICharts Owner { get; }



        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        /// <value>
        /// The name of the style.
        /// </value>
        [JsonProperty("name")]
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether to display the chart. The default is <see cref="YesNo.Yes"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the chart; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [JsonProperty("show")]
        YesNo Show { get; set; }

        ///// <summary>
        ///// Gets or sets the collection of border properties.
        ///// </summary>
        ///// <value>
        ///// Collection of border properties. Each element defines a border.
        ///// </value>
        //[JsonProperty("borders")]
        //IBorders Borders { get; set; }

        ///// <summary>
        ///// Gets or sets a reference to the content model.
        ///// </summary>
        ///// <value>
        ///// Reference that contains the definition of as shown the content.
        ///// </value>
        //[JsonProperty("content")]
        //IContent Content { get; set; }



        /// <summary>
        /// Sets the element that owns this <see cref="IChart"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void SetOwner(ICharts reference);
    }
}
