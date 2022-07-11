﻿
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

using Newtonsoft.Json;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Styling
{
    /// <summary>
    /// A Specialization of <see cref="IContentAlignment"/> interface.<br/>
    /// Which acts as the base class for different content alignment configurations.
    /// </summary>
    public partial class BaseContentAlignment : IContentAlignment
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const KnownHorizontalAlignment DefaultHorizontalAlignment = KnownHorizontalAlignment.Left;
        #endregion

        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private KnownHorizontalAlignment _horizontal;
        #endregion

        #region constructor/s

        #region [public] BaseContentAlignment(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseContentAlignment"/> class.
        /// </summary>
        public BaseContentAlignment()
        {
            Horizontal = DefaultHorizontalAlignment;
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

        #region (object) ICombinable<IContentAlignment>.Combine(IContentAlignment): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference content alignment</param>
        void ICombinable<IContentAlignment>.Combine(IContentAlignment reference) => Combine((BaseContentAlignment)reference);
        #endregion

        #endregion

        #endregion

        #region IContentAlignment

        #region explicit

        #region (bool) IContentAlignment.IsEmpty: Gets a value indicating whether this style is an empty content alignment
        /// <summary>
        /// Gets a value indicating whether this style is an empty content alignment.
        /// </summary>
        /// <value>
        /// <b>true</b> if is an empty content alignment; otherwise, <b>false</b>.
        /// </value>        
        [XmlIgnore]
        [JsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        bool IContentAlignment.IsEmpty => IsDefault;
        #endregion

        #region (void) IContentAlignment.SetParent(IParent): Sets the element that owns this
        /// <summary>
        /// Sets the parent element of the element.
        /// </summary>
        /// <param name="reference">Reference to parent.</param>
        void IContentAlignment.SetParent(IParent reference) => SetParent(reference);
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

        #region [public] (KnownHorizontalAlignment) Horizontal: Gets or sets preferred horizontal content alignemnt
        /// <summary>
        /// Gets or sets preferred horizontal content alignemnt. The default is <see cref="KnownHorizontalAlignment.Left"/>.
        /// </summary>
        /// <value>
        /// Preferred horizontal content alignemnt.
        /// </value>
        [XmlAttribute]
        [DefaultValue(DefaultHorizontalAlignment)]
        public KnownHorizontalAlignment Horizontal
        {
            get => _horizontal;
            set
            {
                SentinelHelper.IsEnumValid(value);
                _horizontal = value;
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
        public override bool IsDefault => base.IsDefault && Horizontal.Equals(DefaultHorizontalAlignment);
        #endregion

        #endregion

        #endregion

        #region IParent

        #region explicit

        #region (IParent) IContentAlignment.Parent: Gets the parent element of the element
        /// <summary>
        /// Gets the parent element of the element.
        /// </summary>
        /// <value>
        /// The element that represents the container element of the element.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        IParent IContentAlignment.Parent => Parent;
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static properties

        #region [public] {static} (BaseContentAlignment) Default: Gets a default content alignment
        /// <summary>
        /// Gets a default content alignment.
        /// </summary>
        /// <value>
        /// A content alignment.
        /// </value>
        public static BaseContentAlignment Default => Empty;
        #endregion

        #region [public] {static} (BaseContentAlignment) Empty: Gets an empty content alignment
        /// <summary>
        /// Gets an empty content alignment.
        /// </summary>
        /// <value>
        /// An empty content alignment.
        /// </value>
        public static BaseContentAlignment Empty => new BaseContentAlignment();
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

        #region public methods

        #region [public] (BaseContentAlignment) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BaseContentAlignment Clone()
        {
            var cloned = (BaseContentAlignment)MemberwiseClone();
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

        #region [public] {virtual} (void) Combine(BaseContentAlignment): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference content alignment</param>
        public virtual void Combine(BaseContentAlignment reference)
        {
            if (reference == null)
            {
                return;
            }

            if (Horizontal.Equals(DefaultHorizontalAlignment))
            {
                Horizontal = reference.Horizontal;
            }
        }
        #endregion

        #endregion
    }
}
