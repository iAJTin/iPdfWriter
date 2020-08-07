
namespace iTin.Core.Models.Design.Styling
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.IO;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// A Specialization of <see cref="IStyle"/> interface.<br/>
    /// Defines a generic style.
    /// </summary>
    public partial class BaseStyle : IStyle
    {
        #region public constants
        /// <summary>
        /// The name of default style. Always is '_Default_'.
        /// </summary>
        public const string NameOfDefaultStyle = "_Default_";
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BaseContent _content;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BordersCollection _borders;
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

        #region (object) ICombinable<IStyle>.Combine(IStyle): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IStyle>.Combine(IStyle reference) => Combine((BaseStyle)reference);
        #endregion

        #endregion

        #endregion

        #region IStyle

        #region explicit

        #region (IBorders) IStyle.Borders: Gets or sets the collection of border properties
        /// <summary>
        /// Gets or sets the collection of border properties.
        /// </summary>
        /// <value>
        /// Collection of border properties. Each element defines a border.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IBorders IStyle.Borders
        {
            get => _borders;
            set => _borders = (BordersCollection)value;
        }
        #endregion

        #region (IContent) IStyle.Content: Gets or sets the collection of border properties
        /// <summary>
        /// Gets or sets the content of style
        /// </summary>
        /// <value>
        /// Content
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IContent IStyle.Content
        {
            get => _content;
            set => _content = (BaseContent)value;
        }
        #endregion

        #region (bool) IStyle.IsEmpty: Gets a value indicating whether this style is an empty style
        /// <summary>
        /// Gets a value indicating whether this style is an empty style.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty style; otherwise, <b>false</b>.
        /// </value>        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        bool IStyle.IsEmpty => IsDefault;
        #endregion

        #region (void) IStyle.SetOwner(IOwner): Sets the element that owns this
        /// <summary>
        /// Sets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IStyle.SetOwner(IOwner reference) => SetOwner(reference);
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

        #region [public] (IOwner) Owner: Gets the element that owns this
        /// <summary>
        /// Gets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IOwner"/> that owns this <see cref="IStyles"/>.
        /// </value>
        public IOwner Owner { get; private set; }
        #endregion

        #endregion

        #region public properties

        #region [public] (string) Name: Gets or sets the name of the style
        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        /// <value>
        /// The name of the style.
        /// </value>
        [XmlElement]
        public string Name { get; set; }

        #endregion

        #region [public] (string) Inherits: Gets or sets the name of parent style
        /// <summary>
        /// Gets or sets the name of parent style.
        /// </summary>
        /// <value>
        /// The name of parent style.
        /// </value>
        [XmlElement]
        [DefaultValue("")]
        public string Inherits { get; set; }

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
            Content.IsDefault &&
            Borders.IsDefault &&
            string.IsNullOrEmpty(Inherits);
        #endregion

        #endregion

        #region public methods

        #region [public] (IStyle) TryGetInheritStyle(): Try gets a reference to inherit model
        /// <summary>
        /// Try gets a reference to inherit model.
        /// </summary>
        /// <returns>
        /// An inherit style.
        /// </returns>
        public IStyle TryGetInheritStyle() => InheritStyle;
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (BaseStyle) Default: Gets a default style
        /// <summary>
        /// Gets a default style.
        /// </summary>
        /// <value>
        /// A default style.
        /// </value>
        public static BaseStyle Default
        {
            get
            {
                var @default = Empty;
                @default.Name = NameOfDefaultStyle;

                return @default;
            }
        }
        #endregion

        #region [public] {static} (BaseStyle) Empty: Gets an empty style
        /// <summary>
        /// Gets an empty style.
        /// </summary>
        /// <value>
        /// An empty style.
        /// </value>
        public static BaseStyle Empty => new BaseStyle();
        #endregion

        #endregion

        #region public virtual readonly properties

        #region [public] {virtual} (bool) BordersSpecified: Gets a value that tells the serializer if the referenced item is to be included
        /// <summary>
        /// Gets a value that tells the serializer if the referenced item is to be included.
        /// </summary>
        /// <value>
        /// <b>true</b> if the serializer has to include the element; otherwise, <b>false</b>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual bool BordersSpecified => !Borders.IsDefault;
        #endregion

        #region [public] {virtual} (bool) ContentSpecified: Gets a value that tells the serializer if the referenced item is to be included
        /// <summary>
        /// Gets a value that tells the serializer if the referenced item is to be included.
        /// </summary>
        /// <value>
        /// <b>true</b> if the serializer has to include the element; otherwise, <b>false</b>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual bool ContentSpecified => !Content.IsDefault;
        #endregion

        #endregion

        #region public properties

        #region [public] (BordersCollection) Borders: Gets or sets the collection of border properties
        /// <summary>
        /// Gets or sets the collection of border properties.
        /// </summary>
        /// <value>
        /// Collection of border properties. Each element defines a border.
        /// </value>
        [JsonProperty("borders")]
        [XmlArrayItem("Border", typeof(BaseBorder), IsNullable = false)]
        public BordersCollection Borders
        {
            get => _borders ?? (_borders = new BordersCollection(this));
            set => _borders = value;
        }
        #endregion

        #region [public] (BaseContent) Content: Gets or sets a reference to the content model
        /// <summary>
        /// Gets or sets a reference to the content model.
        /// </summary>
        /// <value>
        /// Reference that contains the definition of as shown the content.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        public BaseContent Content
        {
            get => _content ?? (_content = new BaseContent());
            set => _content = value;
        }
        #endregion

        #endregion

        #region private properties

        #region [private] (BaseStyle) InheritStyle: Gets a reference to inherit model
        /// <summary>
        /// Gets a reference to inherit model.
        /// </summary>
        /// <value>
        /// A inherit style.
        /// </value>
        [XmlIgnore]
        [JsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BaseStyle InheritStyle => Owner == null
            ? Empty
            : (BaseStyle)((IStyles)Owner).GetBy(Inherits);
        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (string) GenerateRandomStyleName(): Returns a random style name
        /// <summary>
        /// Returns a random style name.
        /// </summary>
        /// <returns>
        /// A new style name.
        /// </returns>
        public static string GenerateRandomStyleName() 
            => Path.ChangeExtension(IO.File.GetUniqueTempRandomFile().Segments.Last(), string.Empty).Replace(".", string.Empty);
        #endregion

        #endregion

        #region public methods

        #region [public] (void) Combine(BaseStyle): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        public void Combine(BaseStyle reference) => Combine(reference, true);
        #endregion

        #region [public] (BaseStyle) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BaseStyle Clone()
        {
            var styleCloned = (BaseStyle)MemberwiseClone();
            styleCloned.Borders = Borders.Clone();
            styleCloned.Content = Content.Clone();
            styleCloned.Properties = Properties.Clone();

            return styleCloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) Combine(BaseStyle, bool): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        /// <param name="forceInherits">Reference style</param>
        public virtual void Combine(BaseStyle reference, bool forceInherits)
        {
            if (reference == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(Name))
            {
                throw new NullReferenceException("Primero asignar un nombre de estilo antes de combinar");
            }

            if (forceInherits)
            {
                var hasInheritStyle = !string.IsNullOrEmpty(reference.Inherits);
                if (hasInheritStyle)
                {
                    var inheritStyle = reference.TryGetInheritStyle();
                    reference.Combine((BaseStyle)inheritStyle);
                }
            }

            Borders.Combine(reference.Borders);
            Content.Combine(reference.Content);
        }
        #endregion

        #endregion

        #region internal methods

        #region [internal] (void) SetOwner(IOwner): Sets the element that owns this
        /// <summary>
        /// Sets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(IOwner reference)
        {
            Owner = reference;
        }
        #endregion

        #endregion
    }
}
