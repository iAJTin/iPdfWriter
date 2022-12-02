
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace iTin.Core.Models.Design.Styling
{
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
        /// <param name="reference">Reference style</param>
        void ICombinable<IStyle>.Combine(IStyle reference) => Combine((BaseStyle)reference);

        #endregion

        #endregion

        #region IStyle

        #region explicit

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

        /// <summary>
        /// Gets a value indicating whether this style is an empty style.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty style; otherwise, <b>false</b>.
        /// </value>        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        bool IStyle.IsEmpty => IsDefault;

        /// <summary>
        /// Sets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IStyle.SetOwner(IOwner reference) => SetOwner(reference);

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
        /// Gets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IOwner"/> that owns this <see cref="IStyles"/>.
        /// </value>
        public IOwner Owner { get; private set; }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        /// <value>
        /// The name of the style.
        /// </value>
        [XmlElement]
        public string Name { get; set; }

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

        #region public override properties

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

        #region public methods

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

        #region public static properties

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

        /// <summary>
        /// Gets an empty style.
        /// </summary>
        /// <value>
        /// An empty style.
        /// </value>
        public static BaseStyle Empty => new();

        #endregion

        #region public virtual readonly properties

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

        #region public properties

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
            get => _borders ??= new BordersCollection(this);
            set => _borders = value;
        }

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
            get => _content ??= new BaseContent();
            set => _content = value;
        }

        #endregion

        #region private properties

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

        #region public static methods

        /// <summary>
        /// Returns a random style name.
        /// </summary>
        /// <returns>
        /// A new style name.
        /// </returns>
        public static string GenerateRandomStyleName() => 
            Path.ChangeExtension(IO.File.GetUniqueTempRandomFile().Segments.Last(), string.Empty)
                .Replace(".", string.Empty);

        #endregion

        #region public methods

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        public void Combine(BaseStyle reference) => Combine(reference, true);

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

        #region public virtual methods

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

        #region internal methods

        /// <summary>
        /// Sets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(IOwner reference)
        {
            Owner = reference;
        }

        #endregion
    }
}
