
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Xml.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;
using iTin.Core.Models.Design.Helpers;
using iTin.Core.Models.Design.Options;

namespace iTin.Core.Models.Design
{
    /// <summary>
    /// Represents a font. Defines a particular format for text, including font face, size, and style attributes.
    /// </summary>
    public partial class FontModel : ICombinable<FontModel>, ICloneable
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string DefaultFontColor = "Black";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string DefaultFontName = "Segoe UI";

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultFontBold = YesNo.No;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultFontItalic = YesNo.No;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const float DefaultFontSize = 10.0f;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultFontUnderline = YesNo.No;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const YesNo DefaultIsScalable = YesNo.Yes;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _name;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _color;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private float _size;
        #endregion

        #region constructor/s

        #region [public] FontModel(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="FontModel"/> class.
        /// </summary>
        public FontModel()
        {
            Color = DefaultFontColor;
            Name = DefaultFontName;
            Size = DefaultFontSize;
            Bold = DefaultFontBold;
            Italic = DefaultFontItalic;
            IsScalable = DefaultIsScalable;
            Underline = DefaultFontUnderline;
        }
        #endregion

        #endregion

        #region interfaces

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

        #region (object) ICombinable<FontModel>.Combine(FontModel): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference pattern</param>
        void ICombinable<FontModel>.Combine(FontModel reference) => Combine(reference);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (FontModel) Default: Default: Gets default font settings
        /// <summary>
        /// Gets default font settings.
        /// </summary>
        /// <value>
        /// A <see cref="FontModel"/> reference containing the default font settings.
        /// </value>
        public static FontModel DefaultFont => new FontModel();
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (FontStyle) FontStyle: Gets a value that represents the different styles defined for this font
        /// <summary>
        /// Gets a value that represents the different styles defined for this font.
        /// </summary>
        /// <value>
        /// A <see cref="FontStyle"/> value that represents the different styles defined for this font.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        public FontStyle FontStyles
        {
            get
            {
                var fontStyles = FontStyle.Regular;
                if (Bold == YesNo.Yes)
                {
                    fontStyles |= FontStyle.Bold;
                }

                if (Italic == YesNo.Yes)
                {
                    fontStyles |= FontStyle.Italic;
                }

                if (Underline == YesNo.Yes)
                {
                    fontStyles |= FontStyle.Underline;
                }

                return fontStyles;
            }
        }
        #endregion

        #endregion

        #region public properties

        #region [public] (string) Name: Gets or sets preferred font name
        /// <summary>
        /// Gets or sets preferred font name. The default is <b>Segoe UI</b>.
        /// </summary>
        /// <value>
        /// Preferred font name. If specified a font name not existent be use the default font. 
        /// </value>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultFontName)]
        public string Name
        {
            get => _name;
            set
            {
                var isValidName = false;
                if (!string.IsNullOrEmpty(value))
                {
                    var isValidFont = IsValidFontName(value);
                    if (isValidFont)
                    {
                        isValidName = true;
                    }
                }

                _name = isValidName
                    ? value
                    : DefaultFontName;
            }
        }
        #endregion

        #region [public] (float) Size: Gets or sets preferred font size
        /// <summary>
        /// Gets or sets preferred font size. The default is <b>10.0</b>.
        /// </summary>
        /// <value>
        /// Preferred font size.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">The value specified is less than of valid value.</exception>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultFontSize)]
        public float Size
        {
            get => _size;
            set
            {
                SentinelHelper.ArgumentLessThan("value", value, 0.0f);

                _size = value;
            }
        }
        #endregion

        #region [public] (string) Color: Gets or sets preferred font color
        /// <summary>
        /// Gets or sets preferred font color. The default is <b>Black</b>.
        /// </summary>
        /// <value>
        /// Preferred font color.
        /// </value>
        /// <exception cref="ArgumentNullException">The value specified is <b>null</b>.</exception>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultFontColor)]
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

        #region [public] (YesNo) IsScalable: Gets or sets a value indicating whether this font is scalable
        /// <summary>
        /// Gets or sets a value indicating whether this font is scalable. The default is <see cref="YesNo.Yes"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if font is scalable; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultIsScalable)]
        [JsonConverter(typeof(StringEnumConverter))]
        public YesNo IsScalable { get; set; }
        #endregion

        #region [public] (YesNo) Bold: Gets or sets a value indicating whether bold style is applied for this font
        /// <summary>
        /// Gets or sets a value indicating whether bold style is applied for this font. The default is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if bold style is applied for this font; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultFontBold)]
        [JsonConverter(typeof(StringEnumConverter))]
        public YesNo Bold { get; set; }
        #endregion

        #region [public] (YesNo) Italic: Gets or sets a value indicating whether italic style is applied for this font
        /// <summary>
        /// Gets or sets a value indicating whether italic style is applied for this font. The default is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if italic style is applied for this font; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultFontItalic)]
        [JsonConverter(typeof(StringEnumConverter))]
        public YesNo Italic { get; set; }
        #endregion

        #region [public] (YesNo) Underline: Gets or sets a value indicating whether the underline style is applied for this font
        /// <summary>
        /// Gets or sets a value indicating whether the underline style is applied for this font. The default is <see cref="YesNo.No"/>.
        /// </summary>
        /// <value>
        /// <see cref="YesNo.Yes"/> if the underline style is applied for this font; otherwise, <see cref="YesNo.No"/>.
        /// </value>
        [JsonProperty]
        [XmlAttribute]
        [DefaultValue(DefaultFontUnderline)]
        [JsonConverter(typeof(StringEnumConverter))]
        public YesNo Underline { get; set; }
        #endregion

        #endregion

        #region public override properties

        #region [public] {override} (bool) IsDefault: Gets a value indicating whether this instance is default
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault =>
            Name.Equals(DefaultFontName) &&
            Bold.Equals(DefaultFontBold) &&
            Size.Equals(DefaultFontSize) &&
            Color.Equals(DefaultFontColor) &&
            Italic.Equals(DefaultFontItalic) &&
            IsScalable.Equals(DefaultIsScalable) &&
            Underline.Equals(DefaultFontUnderline);
        #endregion

        #endregion

        #region public methods

        #region [public] (FontModel) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public FontModel Clone()
        {
            var cloned = (FontModel)MemberwiseClone();
            cloned.Properties = Properties.Clone();

            return cloned;
        }
        #endregion

        #region [public] (Color) GetColor(): Gets a reference to the color structure preferred for this font
        /// <summary>
        /// Gets a reference to the <see cref="T:System.Drawing.Color" /> structure preferred for this font.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Drawing.Color"/> structure that represents a .NET color.
        /// </returns>
        public Color GetColor() => ColorHelper.GetColorFromString(Color);
        #endregion

        #region [public] (Font) ToFont(): Gets a reference to native .NET font representing the font model
        /// <summary>
        /// Gets a reference to native .NET font representing the font model
        /// </summary>
        /// <returns>
        /// Native .NET font representing the font model
        /// </returns>
        public Font ToFont() => new Font(Name, Size, FontStyles, GraphicsUnit.Pixel);
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) ApplyOptions(FontOptions): Apply specified options to this font
        /// <summary>
        /// Apply specified options to this font.
        /// </summary>
        public virtual void ApplyOptions(FontOptions options)
        {
            if (options == null)
            {
                return;
            }

            if (options.IsDefault)
            {
                return;
            }

            #region Bold
            YesNo? boldOption = options.Bold;
            bool boldHasValue = boldOption.HasValue;
            if (boldHasValue)
            {
                Bold = boldOption.Value;
            }
            #endregion

            #region Color
            string colorOption = options.Color;
            bool colorHasValue = !colorOption.IsNullValue();
            if (colorHasValue)
            {
                Color = colorOption;
            }
            #endregion

            #region IsScalable
            YesNo? isScalableOption = options.IsScalable;
            bool isScalableHasValue = isScalableOption.HasValue;
            if (isScalableHasValue)
            {
                IsScalable = isScalableOption.Value;
            }
            #endregion

            #region Italic
            YesNo? italicOption = options.Italic;
            bool italicHasValue = italicOption.HasValue;
            if (italicHasValue)
            {
                Italic = italicOption.Value;
            }
            #endregion

            #region Name
            string nameOption = options.Name;
            bool nameHasValue = !nameOption.IsNullValue();
            if (nameHasValue)
            {
                Name = nameOption;
            }
            #endregion

            #region Size
            float? sizeOption = options.Size;
            bool sizeHasValue = sizeOption.HasValue;
            if (sizeHasValue)
            {
                Size = sizeOption.Value;
            }
            #endregion

            #region Underline
            YesNo? underlineOption = options.Underline;
            bool underlineHasValue = underlineOption.HasValue;
            if (underlineHasValue)
            {
                Underline = underlineOption.Value;
            }
            #endregion
        }
        #endregion

        #region [public] {virtual} (void) Combine(FontModel): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        public virtual void Combine(FontModel reference)
        {
            if (reference == null)
            {
                return;
            }

            if (Bold.Equals(DefaultFontBold))
            {
                Bold = reference.Bold;
            }

            if (Color.Equals(DefaultFontColor))
            {
                Color = reference.Color;
            }

            if (Italic.Equals(DefaultFontItalic))
            {
                Italic = reference.Italic;
            }

            if (Name.Equals(DefaultFontName))
            {
                Name = reference.Name;
            }

            if (Size.Equals(DefaultFontSize))
            {
                Size = reference.Size;
            }

            if (Underline.Equals(DefaultFontUnderline))
            {
                Underline = reference.Underline;
            }
        }
        #endregion

        #endregion

        #region private static methods

        #region [private] {static} (bool) IsValidFontName: Gets a value indicating whether the font is installed on this system
        /// <summary>
        /// Gets a value indicating whether the font is installed on this system.
        /// </summary>
        /// <param name="fontName">Font to check.</param>
        /// <returns>
        /// <strong>true</strong> if the font is installed on the system; otherwise, <strong>false</strong>.
        /// </returns>
        private static bool IsValidFontName(string fontName)
        {
            bool hasFamilies;

            using (var ifc = new InstalledFontCollection())
            {
                hasFamilies = ifc.Families.Any(font => font.Name.Equals(fontName, StringComparison.Ordinal));
            }

            return hasFamilies;
        }
        #endregion

        #endregion
    }
}
