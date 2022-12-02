
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Models.Design;
using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// A Specialization of <see cref="IPdfStyle"/> interface.<br/>
    /// Which acts as a base class for creating generic <b>pdf</b> styles.
    /// </summary>
    public partial class PdfBaseStyle : IPdfStyle, ICombinable<PdfBaseStyle>
    {
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

        #region ICombinable<IStyle>

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IStyle>.Combine(IStyle reference) => Combine((PdfBaseStyle)reference);

        #endregion

        #endregion

        #region ICombinable<IPdfStyle>

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IPdfStyle>.Combine(IPdfStyle reference) => Combine((PdfBaseStyle)reference);

        #endregion

        #endregion

        #region ICombinable<DocXBaseStyle>

        #region explicit

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<PdfBaseStyle>.Combine(PdfBaseStyle reference) => Combine(reference);

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
        void IStyle.SetOwner(IOwner reference) => SetOwner((IPdfStyles)reference);

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        /// <value>
        /// The name of the style.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string IStyle.Name { get => Name; set => Name = value; }

        /// <summary>
        /// Gets or sets the name of parent style.
        /// </summary>
        /// <value>
        /// The name of parent style.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        string IStyle.Inherits { get => Inherits; set => Inherits = value; }

        /// <summary>
        /// Gets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        bool IStyle.IsDefault => IsDefault;

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

        #region IPdfStyle

        #region explicit

        /// <summary>
        /// Gets the element that owns this <see cref="IPdfStyle"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IPdfStyles"/> that owns this <see cref="IPdfStyle"/>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IPdfStyles IPdfStyle.Owner => Owner;

        /// <summary>
        /// Sets the element that owns this <see cref="IPdfStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        void IPdfStyle.SetOwner(IPdfStyles reference) => SetOwner(reference);

        #endregion

        #region public readonly properties

        /// <summary>
        /// Gets the element that owns this <see cref="IPdfStyle"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IPdfStyles"/> that owns this <see cref="IPdfStyle"/>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IPdfStyles Owner { get; private set; }

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the name of the style.
        /// </summary>
        /// <value>
        /// The name of the style.
        /// </value>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of parent style.
        /// </summary>
        /// <value>
        /// The name of parent style.
        /// </value>
        [DefaultValue("")]
        [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
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

        #endregion

        #region IOwner

        #region explicit

        /// <summary>
        /// Gets the element that owns this <see cref="IStyle"/>.
        /// </summary>
        /// <value>
        /// The <see cref="IOwner"/> that owns this <see cref="IStyles"/>.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IOwner IStyle.Owner => Owner;

        #endregion

        #endregion

        #endregion

        #region public static properties

        /// <summary>
        /// Returns a new instance containing a default base style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfBaseStyle"/> reference containing the default base style settings.
        /// </value>
        public static PdfBaseStyle Default
        {
            get
            {
                var @default = Empty;
                @default.Name = BaseStyle.NameOfDefaultStyle;

                return @default;
            }
        }

        /// <summary>
        /// Returns a new instance containing an empty style.
        /// </summary>
        /// <value>
        /// A <see cref="PdfBaseStyle"/> reference containing an empty style settings.
        /// </value>
        public static PdfBaseStyle Empty => new();

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
        [Browsable(false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual bool BordersSpecified => 
            !Borders.IsDefault;

        /// <summary>
        /// Gets a value that tells the serializer if the referenced item is to be included.
        /// </summary>
        /// <value>
        /// <b>true</b> if the serializer has to include the element; otherwise, <b>false</b>.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual bool ContentSpecified =>
            !Content.IsDefault;

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the collection of border properties.
        /// </summary>
        /// <value>
        /// Collection of border properties. Each element defines a border.
        /// </value>
        [XmlElement]
        [JsonProperty("borders")]
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
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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
        private PdfBaseStyle InheritStyle => 
            Owner == null 
                ? Empty 
                : (PdfBaseStyle)Owner.GetBy(Inherits);

        #endregion

        #region public static methods

        /// <summary>
        /// Returns a random style name.
        /// </summary>
        /// <returns>
        /// A new style name.
        /// </returns>
        public static string GenerateRandomStyleName() => 
            BaseStyle.GenerateRandomStyleName();

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfBaseStyle Clone()
        {
            var styleCloned = (PdfBaseStyle)MemberwiseClone();
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
        public virtual void Combine(PdfBaseStyle reference) => 
            Combine(reference, true);

        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        /// <param name="forceInherits">Reference style</param>
        public virtual void Combine(PdfBaseStyle reference, bool forceInherits)
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
                    reference.Combine((PdfBaseStyle)inheritStyle);
                }
            }

            Borders.Combine(reference.Borders);
            Content.Combine(reference.Content);
        }

        #endregion

        #region internal methods

        /// <summary>
        /// Sets the element that owns this <see cref="IPdfStyle"/>.
        /// </summary>
        /// <param name="reference">Reference to owner.</param>
        internal void SetOwner(IPdfStyles reference)
        {
            Owner = reference;
        }

        #endregion
    }
}
