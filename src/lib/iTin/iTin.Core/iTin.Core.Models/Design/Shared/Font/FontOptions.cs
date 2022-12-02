
using System;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Options
{
    /// <summary>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="FontModel"/> model.
    /// </summary>
    [Serializable]
    public class FontOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="FontOptions"/> class.
        /// </summary>
        public FontOptions()
        {
            Color = null;
            Name = null;
            Size = null;
            Bold = null;
            Italic = null;
            IsScalable = null;
            Underline = null;
        }

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

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

        #region public static properties

        /// <summary>
        /// Gets a reference that contains the set of available settings to model an existing <see cref="FontModel"/>.
        /// </summary>
        /// <value>
        /// A <see cref="FontOptions"/> reference containing the set of available settings.
        /// </value>
        public static FontOptions Default => new();

        #endregion

        #region public override readonly properties

        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault =>
            Name == null &&
            Bold == null &&
            Size == null &&
            Color == null &&
            Italic == null &&
            IsScalable == null &&
            Underline == null;

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets a value indicating whether bold style is applied in an existing <see cref="FontModel"/> instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if bold style is applied to a <see cref="FontModel"/> instance or <b>YesNo.No</b> if bold style is not applied to a <see cref="FontModel"/> instance. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("bold")]
        public YesNo? Bold { get; set; }

        /// <summary>
        /// Gets or sets the preferred font color in an existing <see cref="FontModel"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred font color.
        /// </value>
        [XmlAttribute]
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a existing <see cref="FontModel"/> instance is scalable.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if <see cref="FontModel"/> instance is scalable or <b>YesNo.No</b> if <see cref="FontModel"/> instance is not scalable. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("isScalable")]
        public YesNo? IsScalable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether italic style is applied in an existing <see cref="FontModel"/> instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if italic style is applied to a <see cref="FontModel"/> instance or <b>YesNo.No</b> if italic style is not applied to a <see cref="FontModel"/> instance. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("italic")]
        public YesNo? Italic { get; set; }

        /// <summary>
        /// Gets or sets the preferred font name in an existing <see cref="FontModel"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred font name.
        /// </value>
        [XmlAttribute]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the preferred font size in an existing <see cref="FontModel"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred font size.
        /// </value>
        [XmlAttribute]
        [JsonProperty("size")]
        public float? Size { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether underline style is applied in an existing <see cref="FontModel"/> instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if underline style is applied to a <see cref="FontModel"/> instance or <b>YesNo.No</b> if underline style is not applied to a <see cref="FontModel"/> instance. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("underline")]
        public YesNo? Underline { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public FontOptions Clone() => (FontOptions)MemberwiseClone();

        #endregion
    }
}
