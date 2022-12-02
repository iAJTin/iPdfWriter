
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Helpers;

namespace iTin.Core.Models.Design.Styling
{
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

        #region interfaces

        #region IBorder

        #region explicit

        /// <summary>
        /// Gets a value indicating whether this style is an empty border.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty border; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
        bool IBorder.IsEmpty => IsDefault;

        /// <summary>
        /// Sets the element that owns this <see cref="IBorder"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IBorder.SetOwner(IBorders reference) => SetOwner(reference);

        #endregion

        #region public readonly properties

        /// <summary>
        /// Gets a value indicating whether this style is an empty style.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty style; otherwise, <b>false</b>.
        /// </value>        
        public bool IsEmpty => IsDefault;

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

        #region public properties

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

        /// <summary>
        /// Gets or sets preferred border position.
        /// </summary>
        /// <value>
        /// Preferred border position.
        /// </value>
        [XmlAttribute]
        public KnownBorderPosition Position { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether to display the border. The default is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display the border; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [XmlAttribute]
        [DefaultValue(DefaultShow)]
        public YesNo Show { get; set; }

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

        #region public override properties

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

        #region public methods

        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color"/> structure that represents color for this border.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> structure that represents color for this border.
        /// </returns> 
        public Color GetColor() => ColorHelper.GetColorFromString(Color);

        #endregion

        #endregion

        #region ICloneable

        #region explicit

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

        #region ICombinable

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference border</param>
        void ICombinable<IBorder>.Combine(IBorder reference) => Combine((BaseBorder)reference);

        #endregion

        #endregion

        #endregion

        #region public static properties

        /// <summary>
        /// Gets a default border.
        /// </summary>
        /// <value>
        /// A default border.
        /// </value>
        public static BaseBorder Default => Empty;

        /// <summary>
        /// Gets an empty border.
        /// </summary>
        /// <value>
        /// An empty border.
        /// </value>
        public static BaseBorder Empty => new();

        #endregion

        #region public properties

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

        #region public methods

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

        #region public virtual methods

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

        #region internal methods

        /// <summary>
        /// Sets the element that owns this <see cref="IBorders"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(IBorders reference)
        {
            Owner = reference;
        }

        #endregion
    }
}
