
namespace iTin.Core.Models.Design.Options
{
    using System;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Enums;

    /// <summary>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="FontModel"/> model.
    /// </summary>
    [Serializable]
    public class FontOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        #region [public] FontOptions(): Initializes a new instance of this class
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

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

        #region [private] (object) Clone(): Creates a new object that is a copy of the current instance
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

        #region public static properties

        #region [public] {static} (FontOptions) Default: Gets a reference that contains the set of available settings to model an existing FontModel
        /// <summary>
        /// Gets a reference that contains the set of available settings to model an existing <see cref="FontModel"/>.
        /// </summary>
        /// <value>
        /// A <see cref="FontOptions"/> reference containing the set of available settings.
        /// </value>
        public static FontOptions Default => new FontOptions();
        #endregion

        #endregion

        #region public override readonly properties

        #region [public] {override} (bool) IsDefault: Gets a value indicating whether this instance is default
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

        #endregion

        #region public properties

        #region [public] (YesNo?) Bold: Gets or sets a value indicating whether bold style is applied in an existing FontModel instance
        /// <summary>
        /// Gets or sets a value indicating whether bold style is applied in an existing <see cref="FontModel"/> instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if bold style is applied to a <see cref="FontModel"/> instance or <b>YesNo.No</b> if bold style is not applied to a <see cref="FontModel"/> instance. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("bold")]
        public YesNo? Bold { get; set; }
        #endregion

        #region [public] (string) Color: Gets or sets the preferred font color in an existing FontModel instance
        /// <summary>
        /// Gets or sets the preferred font color in an existing <see cref="FontModel"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred font color.
        /// </value>
        [XmlAttribute]
        [JsonProperty("color")]
        public string Color { get; set; }
        #endregion

        #region [public] (YesNo?) IsScalable: Gets or sets a value indicating whether a existing FontModel instance is scalable
        /// <summary>
        /// Gets or sets a value indicating whether a existing <see cref="FontModel"/> instance is scalable.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if <see cref="FontModel"/> instance is scalable or <b>YesNo.No</b> if <see cref="FontModel"/> instance is not scalable. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("isScalable")]
        public YesNo? IsScalable { get; set; }
        #endregion

        #region [public] (YesNo?) Italic: Gets or sets a value indicating whether italic style is applied in an existing FontModel instance
        /// <summary>
        /// Gets or sets a value indicating whether italic style is applied in an existing <see cref="FontModel"/> instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if italic style is applied to a <see cref="FontModel"/> instance or <b>YesNo.No</b> if italic style is not applied to a <see cref="FontModel"/> instance. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("italic")]
        public YesNo? Italic { get; set; }
        #endregion

        #region [public] (string) Name: Gets or sets the preferred font name in an existing FontModel instance
        /// <summary>
        /// Gets or sets the preferred font name in an existing <see cref="FontModel"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred font name.
        /// </value>
        [XmlAttribute]
        [JsonProperty("name")]
        public string Name { get; set; }
        #endregion

        #region [public] (float?) Size: Gets or sets preferred font size in an existing FontModel instance
        /// <summary>
        /// Gets or sets the preferred font size in an existing <see cref="FontModel"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred font size.
        /// </value>
        [XmlAttribute]
        [JsonProperty("size")]
        public float? Size { get; set; }
        #endregion

        #region [public] (YesNo?) Underline: Gets or sets a value indicating whether underline style is applied in an existing FontModel instance
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

        #endregion

        #region public methods

        #region [public] (FontOptions) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public FontOptions Clone() => (FontOptions)MemberwiseClone();
        #endregion

        #endregion
    }
}
