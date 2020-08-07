
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
    /// A Specialization of <see cref="IBorder"/> interface.<br/>
    /// Which acts as the base class for different border configurations.
    /// </summary>
    public partial class BaseBorder : IBorder
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string DefaultColor = "Black";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultShow = YesNo.No;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const float DefaultWidth = 1.0f;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const KnownLineStyle DefaultLineStyle = KnownLineStyle.Continuous;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _color;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private KnownLineStyle _style;
        #endregion

        #region constructor/s

        #region [public] BaseBorder(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBorder"/> class.
        /// </summary>
        public BaseBorder()
        {
            Show = DefaultShow;
            Color = DefaultColor;
            Width = DefaultWidth;
            Style = DefaultLineStyle;
        }
        #endregion

        #endregion

        #region interfaces

        #region IBorder

        #region explicit

        #region (bool) IBorder.IsEmpty: Gets a value indicating whether this style is an empty border
        /// <summary>
        /// Gets a value indicating whether this style is an empty border.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty border; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
        bool IBorder.IsEmpty => IsDefault;
        #endregion

        #region (void) IBorder.SetOwner(IBorders): Sets the element that owns this
        /// <summary>
        /// Sets the element that owns this <see cref="IBorder"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IBorder.SetOwner(IBorders reference) => SetOwner(reference);
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

        #region [public] (IBorders) Owner: Gets the element that owns this
        /// <summary>
        /// Gets the element that owns this <see cref="IBorder"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IBorders"/> that owns this <see cref="IBorder"/>.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        public IBorders Owner { get; private set; }
        #endregion

        #endregion

        #region public properties

        #region [public] (string) Color: Gets or sets preferred border color
        /// <summary>
        /// Gets or sets preferred border color. The default is <b>Black</b>.
        /// </summary>
        /// <value>
        /// Preferred border color.
        /// </value>
        /// <exception cref="T:System.ArgumentNullException">The value specified is <b>null</b>.</exception>
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

        #region [public] (KnownBorderPosition) Position: Gets or sets preferred border position
        /// <summary>
        /// Gets or sets preferred border position.
        /// </summary>
        /// <value>
        /// Preferred border position.
        /// </value>
        [XmlAttribute]
        public KnownBorderPosition Position { get; set; }
        #endregion

        #region [public] (YesNo) Show: Gets or sets a value that determines whether to display the border
        /// <summary>
        /// Gets or sets a value that determines whether to display the border. The default is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the border; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [XmlAttribute]
        [DefaultValue(DefaultShow)]
        public YesNo Show { get; set; }
        #endregion

        #region [public] (KnownLineStyle) Style: Gets or sets preferred border line style
        /// <summary>
        /// Gets or sets preferred border line style. The default is <see cref="KnownLineStyle.Continuous"/>.
        /// </summary>
        /// <value>
        /// Preferred border line style.
        /// </value>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
        [XmlAttribute]
        [DefaultValue(DefaultLineStyle)]
        public KnownLineStyle Style
        {
            get => _style;
            set
            {
                SentinelHelper.IsEnumValid(value);

                _style = value;
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
        public override bool IsDefault =>
            base.IsDefault &&
            Show.Equals(DefaultShow) &&
            Color.Equals(DefaultColor) &&
            Width.Equals(DefaultWidth) &&
            Style.Equals(DefaultLineStyle);
        #endregion

        #endregion

        #region public methods

        #region [public] (Color) GetColor(): Gets a reference to the Color structure that represents color for this border
        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color"/> structure that represents color for this border.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> structure that represents color for this border.
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

        #region (object) ICombinable<IBorder>.Combine(IBorder): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference border</param>
        void ICombinable<IBorder>.Combine(IBorder reference) => Combine((BaseBorder)reference);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (BaseBorder) Default: Gets a default border
        /// <summary>
        /// Gets a default border.
        /// </summary>
        /// <value>
        /// A default border.
        /// </value>
        public static BaseBorder Default => Empty;
        #endregion

        #region [public] {static} (BaseBorder) Empty: Gets an empty border
        /// <summary>
        /// Gets an empty border.
        /// </summary>
        /// <value>
        /// An empty border.
        /// </value>
        public static BaseBorder Empty => new BaseBorder();
        #endregion

        #endregion

        #region public properties

        #region [public] (float) Width: Gets or sets the width of a border
        /// <summary>
        /// Gets or sets the width of a border. The default is <b>1</b>.
        /// </summary>
        /// <value>
        /// An <see cref="float"/> value that determines the width of a border.
        /// </value>
        [XmlAttribute]
        [JsonProperty("width")]
        [DefaultValue(DefaultWidth)]
        public float Width { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (BaseBorder) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BaseBorder Clone()
        {
            var cloned = (BaseBorder) MemberwiseClone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) Combine(BaseBorder): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference border</param>
        public virtual void Combine(BaseBorder reference)
        {
            if (reference == null)
            {
                return;
            }

            if (Color.Equals(DefaultColor))
            {
                Color = reference.Color;
            }

            if (Style.Equals(DefaultLineStyle))
            {
                Style = reference.Style;
            }

            if (Width.Equals(DefaultWidth))
            {
                Width = reference.Width;
            }
        }
        #endregion

        #endregion

        #region internal methods

        #region [internal] (void) SetOwner(IBorders): Sets the element that owns this
        /// <summary>
        /// Sets the element that owns this <see cref="IBorders"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(IBorders reference)
        {
            Owner = reference;
        }

        #endregion

        #endregion
    }
}
