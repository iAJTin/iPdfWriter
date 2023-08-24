
using System.ComponentModel;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Styling;
using IStyle = iTin.Core.Models.Design.IStyle;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="IStyle"/> interface.<br/>
    /// Defines a generic <b>pdf</b> style.
    /// </summary>
    public interface IPdfStyle : IStyle, ICombinable<IPdfStyle>
    {
        /// <summary>
        /// Gets the element that owns this <see cref="IPdfStyle"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IPdfStyles"/> that owns this <see cref="IPdfStyle"/>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        new IPdfStyles Owner { get; }


        /// <summary>
        /// Sets the element that owns this <see cref="IPdfStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void SetOwner(IPdfStyles reference);
    }
}
