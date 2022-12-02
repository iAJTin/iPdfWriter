
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Helpers;
using iTin.Core.Models.Design.Options;

namespace iTin.Core.Models.Design
{
    /// <summary>
    /// Represents the visual setting of border's shadow. Includes the shadow color and visibility.
    /// </summary>
    public partial class Shadow : ICombinable<Shadow>, ICloneable
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const int DefaultOffset = 2;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultShow = YesNo.No;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string DefaultColor = "LightGray";

        #endregion

        #region private members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _color;

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="Shadow"/> class.
        /// </summary>
        public Shadow()
        {
            Show = DefaultShow;
            Color = DefaultColor;
            Offset = DefaultOffset;
        }

        #endregion

        #region interfaces

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
        /// <param name="reference">Reference pattern</param>
        void ICombinable<Shadow>.Combine(Shadow reference) => Combine(reference);

        #endregion

        #endregion

        #endregion

        #region public readonly static properties

        /// <summary>
        /// Returns a new instance containing default shadow settings.
        /// </summary>
        /// <value>
        /// A <see cref="Shadow"/> reference containing the default shadow settings.
        /// </value>
        public static Shadow Default => new();

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets preferred shadow color. The default is <b>LightGray</b>.
        /// </summary>
        /// <value>
        /// Preferred shadow color.
        /// </value>
        /// <exception cref="ArgumentNullException">The value specified is <b>null</b>.</exception>
        [XmlAttribute]
        [JsonProperty("color")]
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
        /// Gets or sets the shadow shift, in pixels. The default is <b>2</b>.
        /// </summary>
        /// <value>
        /// An <see cref="int"/> value that represents the shadow displacement, in pixels.
        /// </value>
        [XmlAttribute]
        [JsonProperty("offset")]
        [DefaultValue(DefaultOffset)]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets a value that determines whether displays shadow. The default is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if display shadow; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        /// <exception cref="InvalidEnumArgumentException">The value specified is outside the range of valid values.</exception>
        [XmlAttribute]
        [JsonProperty("show")]
        [DefaultValue(DefaultShow)]
        public YesNo Show { get; set; }

        #endregion

        #region public override properties

        /// <inheritdoc/>
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault => 
            base.IsDefault && 
            Show.Equals(DefaultShow) && 
            Offset.Equals(DefaultOffset) && 
            Color.Equals(DefaultColor);

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Shadow Clone() => (Shadow) MemberwiseClone();

        /// <summary>
        /// Gets a reference to the <see cref="System.Drawing.Color"/> structure preferred for shadow color.
        /// </summary>
        /// <returns>
        /// <see cref="System.Drawing.Color"/> structure that represents a .NET color.
        /// </returns>
        public Color GetColor() => ColorHelper.GetColorFromString(Color);

        #endregion

        #region public virtual methods

        /// <summary>
        /// Apply specified options to this instance.
        /// </summary>
        public virtual void ApplyOptions(ShadowOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Color
            string colorOption = options.Color;
            bool colorHasValue = !colorOption.IsNullValue();
            if (colorHasValue)
            {
                Color = colorOption;
            }
            #endregion

            #region Offset
            int? offsetOption = options.Offset;
            bool offsetHasValue = offsetOption.HasValue;
            if (offsetHasValue)
            {
                Offset = offsetOption.Value;
            }
            #endregion

            #region Show
            YesNo? showOption = options.Show;
            bool showHasValue = showOption.HasValue;
            if (showHasValue)
            {
                Show = showOption.Value;
            }
            #endregion
        }

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        public virtual void Combine(Shadow reference)
        {
            if (reference == null)
            {
                return;
            }

            if (Color.Equals(DefaultColor))
            {
                Color = reference.Color;
            }

            if (Offset.Equals(DefaultOffset))
            {
                Offset = reference.Offset;
            }

            if (Show.Equals(DefaultShow))
            {
                Show = reference.Show;
            }
        }

        #endregion
    }
}
