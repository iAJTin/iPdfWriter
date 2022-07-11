
using System.Drawing;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design.Charting
{
    /// <summary>
    /// Defines a generic content.
    /// </summary>
    public interface IGenericChart : IChart, ICombinable<IGenericChart>
    {
        /// <summary>
        /// Gets or sets preferred back color for a chart
        /// </summary>
        /// <value>
        /// Preferred back color.
        /// </value>
        [JsonProperty("backcolor")]
        string BackColor { get; set; }


        /// <summary>
        /// Gets a reference to the <see cref="Color"/> structure than represents back color for a chart.
        /// </summary>
        /// <returns>
        /// A <see cref="Color"/> structure that represents back color.
        /// </returns> 
        Color GetBackColor();
    }
}
