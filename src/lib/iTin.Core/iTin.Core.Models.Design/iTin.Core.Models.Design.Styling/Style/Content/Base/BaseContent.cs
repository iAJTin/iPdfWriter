
namespace iTin.Core.Models.Design.Styling
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using iTin.Core.Helpers;

    using Enums;
    using Helpers;

    /// <summary>
    /// A Specialization of <see cref="IContent"/> interface.<br/>
    /// Which acts as the base class for different contents.
    /// </summary>
    public partial class BaseContent : IContent
    {
        #region public constants
        /// <summary>
        /// Defines default content color.
        /// </summary>
        public const string DefaultColor = "Transparent";
        #endregion

        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultShow = YesNo.Yes;
        #endregion

        #region private field members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private YesNo _show;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _color;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _alternateColor;
        #endregion

        #region constructor/s

        #region [public] BaseContent(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContent"/> class.
        /// </summary>
        public BaseContent()
        {
            Show = DefaultShow;
            Color = DefaultColor;
        }
        #endregion

        #endregion

        #region interfaces

        #region IContent

        #region explicit

        #region (bool) IContent.IsEmpty: Gets a value indicating whether this style is an empty border
        /// <summary>
        /// Gets a value indicating whether this style is an empty border.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty border; otherwise, <b>false</b>.
        /// </value>        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        bool IContent.IsEmpty => IsDefault;
        #endregion

        #region (void) IContent.SetParent(IParent): Sets the element that owns this
        /// <summary>
        /// Sets the parent element of the element.
        /// </summary>
        /// <param name="reference">Reference to parent.</param>
        void IContent.SetParent(IParent reference) => SetParent((BaseStyle)reference);
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (bool) IsEmpty: Gets a value indicating whether this style is an empty style
        /// <summary>
        /// Gets a value indicating whether this style is an empty style.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty style; otherwise, <b>false</b>.
        /// </value>        
        public bool IsEmpty => IsDefault;
        #endregion

        #endregion

        #region public properties

        #region [public] (string) AlternateColor: Gets or sets preferred alternate content color
        /// <summary>
        /// Gets or sets preferred alternate content color. The default is <b>Transparent</b>.
        /// </summary>
        /// <value>
        /// Preferred alternate content color.
        /// </value>
        /// <exception cref="ArgumentNullException">The value specified is <b>null</b>.</exception>
        [XmlAttribute]
        public string AlternateColor
        {
            get => string.IsNullOrEmpty(_alternateColor) ? _color : _alternateColor;
            set
            {
                SentinelHelper.ArgumentNull(value, nameof(AlternateColor));

                _alternateColor = value;
            }
        }
        #endregion

        #region [public] (string) Color: Gets or sets preferred content color
        /// <summary>
        /// Gets or sets preferred content color. The default is <b>Transparent</b>.
        /// </summary>
        /// <value>
        /// Preferred content color.
        /// </value>
        /// <exception cref="ArgumentNullException">The value specified is <b>null</b>.</exception>
        [XmlAttribute]
        [DefaultValue(DefaultColor)]
        public string Color
        {
            get => _color;
            set
            {
                SentinelHelper.ArgumentNull(value, nameof(value));

                _color = value;
            }
        }
        #endregion

        #region [public] (YesNo) Show: Gets or sets a value that determines whether to display this content picture
        /// <summary>
        /// Gets or sets a value that determines whether to display this content picture. The default is <see cref="YesNo.Yes"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display this content picture; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        /// <exception cref="InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
        [XmlAttribute]
        [DefaultValue(DefaultShow)]
        public YesNo Show
        {
            get => _show;
            set
            {
                SentinelHelper.IsEnumValid(value);
                _show = value;
            }
        }

        #endregion

        #endregion

        #region public override properties

        #region [public] {overide} (bool) IsDefault: Gets a value indicating whether this instance is default
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault => base.IsDefault && Show.Equals(DefaultShow) && Color.Equals(DefaultColor);
        #endregion

        #endregion

        #region public methods

        #region [public] (Color) GetAlternateColor(): Gets a reference to the Color structure that represents alternate color for this content
        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color"/> structure that represents alternate color for this content.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> structure that represents alternate color for this content.
        /// </returns> 
        public Color GetAlternateColor() => ColorHelper.GetColorFromString(AlternateColor);
        #endregion

        #region [public] (Color) GetColor(): Gets a reference to the Color structure that represents color for this border
        /// <summary>
        /// Gets a reference to the <see cref="System.Drawing.Color"/> structure that represents color for this border.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Drawing.Color"/> structure that represents color for this border.
        /// </returns> 
        public Color GetColor() => ColorHelper.GetColorFromString(Color);
        #endregion

        #endregion

        #endregion

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

        #region ICombinable

        #region explicit

        #region (object) ICombinable<IContent>.Combine(IContent): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference content</param>
        void ICombinable<IContent>.Combine(IContent reference) => Combine((BaseContent)reference);
        #endregion

        #endregion

        #endregion

        #region IParent

        #region explicit

        #region (IParent) IContent.Parent: Gets the parent element of the element
        /// <summary>
        /// Gets the parent element of the element.
        /// </summary>
        /// <value>
        /// The element that represents the container element of the element.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IParent IContent.Parent => Parent;
        #endregion

        #endregion

        #endregion

        #endregion

        #region public readonly properties

        #region [public] (IParent) Parent: Gets the parent element of the element
        /// <summary>
        /// Gets the parent element of the element.
        /// </summary>
        /// <value>
        /// The element that represents the container element of the element.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        [Browsable(false)]
        public IParent Parent { get; private set; }
        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (BaseContent) Default: Gets a default content
        /// <summary>
        /// Gets a default content.
        /// </summary>
        /// <value>
        /// A default content.
        /// </value>
        public static BaseContent Default => Empty;
        #endregion

        #region [public] {static} (BaseColorContent) Empty: Gets an empty content
        /// <summary>
        /// Gets an empty content.
        /// </summary>
        /// <value>
        /// An empty content.
        /// </value>
        public static BaseContent Empty => new BaseContent();
        #endregion

        #endregion

        #region public methods

        #region [public] (BaseContent) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BaseContent Clone()
        {
            var cloned = (BaseContent)MemberwiseClone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }
        #endregion

        #region [public] (void) SetParent(IParent): Sets the parent element of the element
        /// <summary>
        /// Sets the parent element of the element.
        /// </summary>
        /// <param name="reference">Reference to parent.</param>
        public void SetParent(IParent reference)
        {
            Parent = reference;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) Combine(BaseContent): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference content</param>
        public virtual void Combine(BaseContent reference)
        {
            if (reference == null)
            {
                return;
            }

            if (AlternateColor.Equals(DefaultColor))
            {
                AlternateColor = reference.AlternateColor;
            }

            if (Color.Equals(DefaultColor))
            {
                Color = reference.Color;
            }

            if (Show.Equals(DefaultShow))
            {
                Show = reference.Show;
            }
        }
        #endregion

        #endregion
    }
}
