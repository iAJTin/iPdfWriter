
namespace iTin.Core.Models.Design.Charting
{
    using System;

    using iTin.Core.Helpers;

    using Collections;

    /// <summary>
    /// Defines a charts collection.
    /// </summary>
    public partial class ChartsCollection : ICharts
    {
        #region constructor/s

        #region [public] ChartsCollection(): Initializes a new instance of the class
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartsCollection"/> class.
        /// </summary>
        public ChartsCollection() : base(null)
        {
        }
        #endregion

        #region [public] ChartsCollection(object): Initializes a new instance of the class
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="ChartsCollection"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ChartsCollection(object parent) : base(parent)
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

        #endregion

        #region protected override methods

        #region [protected] {override} (BaseChart) GetBy(string): Returns the element specified
        /// <inheritdoc />
        /// <summary>
        /// Returns the element specified.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="BaseComplexModelCollection{TItem,TParent,TSearch}"/>.</param>
        /// <returns>
        /// Returns the specified element.
        /// </returns>
        public override BaseChart GetBy(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BaseChart.Default;
            }

            var style = Find(s => s.Name.Equals(value));

            return style ?? BaseChart.Default;
        }
        #endregion

        #region [protected] {override} (void) SetOwner(BaseChart): Sets this collection as the owner of the specified item
        /// <inheritdoc />
        /// <summary>
        /// Sets this collection as the owner of the specified item.
        /// </summary>
        /// <param name="item">Target item to set owner.</param>
        protected override void SetOwner(BaseChart item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }
        #endregion

        #endregion

        #region public methods

        #region [public] (ChartsCollection) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public ChartsCollection Clone() => CopierHelper.DeepCopy(this);
        #endregion

        #endregion
    }
}
