
namespace iTin.Core.Models.Design.Styling
{
    using System;
    using System.Drawing;
    
    using Newtonsoft.Json;

    using Enums;

    /// <summary>
    /// Defines a generic style
    /// </summary>
    public interface IBorder : ICombinable<IBorder>, ICloneable
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
        /// Gets or sets preferred border color.
        /// </summary>
        /// <value>
        /// Preferred border color.
        /// </value>
        [JsonProperty("color")]
        string Color { get; set; }

        /// <summary>
        /// Gets or sets preferred border position.
        /// </summary>
        /// <value>
        /// Preferred border position.
        /// </value>
        [JsonProperty("position")]
        KnownBorderPosition Position { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether to display the border.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the border; otherwise, <see cref="YesNo.No"/>. 
        /// </value>
        [JsonProperty("show")]
        YesNo Show { get; set; }

        /// <summary>
        /// Gets or sets preferred border line style.
        /// </summary>
        /// <value>
        /// Preferred border line style.
        /// </value>
        [JsonProperty("style")]
        KnownLineStyle Style { get; set; }



        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color"/> structure that represents color for this border.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> structure that represents color for this border.
        /// </returns> 
        Color GetColor();

        /// <summary>
        /// Sets the element that owns this <see cref="IBorder"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void SetOwner(IBorders reference);
    }
}
