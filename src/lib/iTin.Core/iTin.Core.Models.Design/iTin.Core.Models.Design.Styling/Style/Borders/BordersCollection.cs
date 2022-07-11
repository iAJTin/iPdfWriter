
using System;
using System.Linq;

using iTin.Core.Helpers;
using iTin.Core.Models.Collections;
using iTin.Core.Models.Design.Enums;

namespace iTin.Core.Models.Design.Styling
{
    /// <summary>
    /// Defines a borders collection.
    /// </summary>
    public partial class BordersCollection : IBorders
    {
        #region constructor/s

        #region [public] BordersCollection(): Initializes a new instance of the class
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="BordersCollection"/> class.
        /// </summary>
        public BordersCollection() : base(null)
        {
        }
        #endregion

        #region [public] BordersCollection(object): Initializes a new instance of the class
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="BordersCollection"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public BordersCollection(object parent) : base(parent)
        {
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

        #region (object) ICombinable<IBorders>.Combine(IBorders): Creates a new object that is a copy of the current instance
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Reference style</param>
        void ICombinable<IBorders>.Combine(IBorders reference) => Combine((BordersCollection)reference);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (BordersCollection) FromCustomColor(string): Returns a new border collection for specified color
        /// <summary>
        /// Returns a new border collection for specified color.
        /// </summary>
        /// <param name="borderColor">Border color</param>
        /// <value>
        /// A <see cref="BordersCollection"/> reference.
        /// </value>
        public static BordersCollection FromCustomColor(string borderColor) => CreeateCustomColor(borderColor);
        #endregion

        #region [public] {static} (BordersCollection) FromKnownColor(KnownBorderColor): Returns a new border collection for specified color
        /// <summary>
        /// Returns a new border collection for specified color.
        /// </summary>
        /// <param name="borderColor">Border color</param>
        /// <value>
        /// A <see cref="BordersCollection"/> reference.
        /// </value>
        public static BordersCollection FromKnownColor(KnownBorderColor borderColor) => CreeateKnownBorder(borderColor);
        #endregion

        #endregion

        #region private static methods

        private static BordersCollection CreeateCustomColor(string borderColor)
            => new BordersCollection
            {
                new BaseBorder {Color = borderColor, Position = KnownBorderPosition.Left, Show = YesNo.Yes},
                new BaseBorder {Color = borderColor, Position = KnownBorderPosition.Top, Show = YesNo.Yes},
                new BaseBorder {Color = borderColor, Position = KnownBorderPosition.Right, Show = YesNo.Yes},
                new BaseBorder {Color = borderColor, Position = KnownBorderPosition.Bottom, Show = YesNo.Yes}  
            };

        private static BordersCollection CreeateKnownBorder(KnownBorderColor borderColor)
            => new BordersCollection
            {
                new BaseBorder {Color = borderColor.ToString(), Position = KnownBorderPosition.Left, Show = YesNo.Yes},
                new BaseBorder {Color = borderColor.ToString(), Position = KnownBorderPosition.Top, Show = YesNo.Yes},
                new BaseBorder {Color = borderColor.ToString(), Position = KnownBorderPosition.Right, Show = YesNo.Yes},
                new BaseBorder {Color = borderColor.ToString(), Position = KnownBorderPosition.Bottom, Show = YesNo.Yes}
            };

        #endregion

        #region protected override methods

        #region [protected] {override} (BaseBorder) GetBy(KnownBorderPosition): Returns the element specified
        /// <inheritdoc />
        /// <summary>
        /// Returns the element specified.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="BaseComplexModelCollection{TItem, TParent, TSearch}"/>.</param>
        /// <returns>
        /// Returns the specified element.
        /// </returns>
        public override BaseBorder GetBy(KnownBorderPosition value) => Find(border => border.Position == value);
        #endregion

        #region [protected] {override} (void) SetOwner(BaseBorder): Sets this collection as the owner of the specified item
        /// <inheritdoc />
        /// <summary>
        /// Sets this collection as the owner of the specified item.
        /// </summary>
        /// <param name="item">Target item to set owner.</param>
        protected override void SetOwner(BaseBorder item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }
        #endregion

        #endregion

        #region public methods

        #region [public] (BordersCollection) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public BordersCollection Clone()
        {
            var cloned = new BordersCollection(Parent)
            {
                Properties = Properties.Clone()
            };

            foreach (BaseBorder border in this)
            {
                cloned.Add(border.Clone());
            }

            return cloned;
        }
        #endregion

        #endregion

        #region public virtual methods

        #region [public] {virtual} (void) Combine(BordersCollection): Combines this instance with reference parameter
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">The reference.</param>
        public virtual void Combine(BordersCollection reference)
        {
            if (reference == null)
            {
                return;
            }

            var hasElements = this.Any();
            if (!hasElements)
            {
                foreach (var referenceBorder in reference)
                {
                    var border = referenceBorder.Clone();
                    border.SetOwner(this);
                    Add(border);
                }
            }
            else
            {
                foreach (var border in this)
                {
                    var refborder = reference.GetBy(border.Position);
                    if (refborder != null)
                    {
                        border.Combine(refborder);
                    }
                }

                foreach (var referenceBorder in reference)
                {
                    var border = GetBy(referenceBorder.Position);
                    if (border != null)
                    {
                        continue;
                    }

                    var newBorder = referenceBorder.Clone();
                    newBorder.SetOwner(this);
                    Add(newBorder);
                }
            }
        }
        #endregion

        #endregion
    }
}
