
namespace iTin.Core.Models.Design.Options
{
    using System;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Enums;

    /// <summary>
    /// Defines a set of options that we can use to quickly adjust an existing <see cref="Shadow"/> instance.
    /// </summary>
    [Serializable]
    public class ShadowOptions : BaseOptions, ICloneable
    {
        #region constructor/s

        #region [public] TitleOptions(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowOptions"/> class.
        /// </summary>
        public ShadowOptions()
        {
            Show = null;
            Color = null;
            Offset = null;
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

        #region [public] {static} (ShadowOptions) Default: Gets a reference that contains the set of available settings to model an existing Shadow instance
        /// <summary>
        /// Gets a reference that contains the set of available settings to model an existing <see cref="Shadow"/> instance.
        /// </summary>
        /// <value>
        /// Set of default options.
        /// </value>
        public static ShadowOptions Default => new ShadowOptions();
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
        public override bool IsDefault => Color == null && Show == null && Offset == null;
        #endregion

        #endregion

        #region public properties

        #region [public] (string) Color: Gets or sets the preferred back color in an existing FontModel instance
        /// <summary>
        /// Gets or sets the preferred color in an existing <see cref="Shadow"/>" instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// Preferred shadow color.
        /// </value>
        [XmlAttribute]
        [JsonProperty("color")]
        public string Color { get; set; }
        #endregion

        #region [public] (int?) Offset: Gets or sets a value that contains the shadow shift, expressed in pixels in an existing ShadowModel instance
        /// <summary>
        /// Gets or sets a value that contains the shadow shift, expressed in pixels in an existing <see cref="Shadow"/> instance. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model or an <see cref="int" /> value that represents the shadow displacement, in pixels.
        /// </value>
        [XmlAttribute]
        [JsonProperty("offset")]
        public int? Offset { get; set; }
        #endregion

        #region [public] (YesNo?) Show: Gets or sets a value that indicates whether an existing TitleModel instance is displayed
        /// <summary>
        /// Gets or sets a value that indicates whether an existing. The default value is <b>(null)</b>, Nothing in Visual Basic.
        /// </summary>
        /// <value>
        /// <b>null</b>, (Nothing in Visual Basic) do not modify the value of the reference model, <b>YesNo.Yes</b> if the instance is displayed or <b>YesNo.No</b> if the instance is not displayed. 
        /// </value>
        [XmlAttribute]
        [JsonProperty("show")]
        public YesNo? Show { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (ShadowOptions) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public ShadowOptions Clone() => (ShadowOptions)MemberwiseClone();
        #endregion

        #endregion
    }
}
