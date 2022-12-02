
using System;

using Newtonsoft.Json;

namespace iTin.Core.Models.Collections
{
    /// <inheritdoc/>
    /// <summary>
    /// A Specialization of <see cref="BaseSimpleModelCollection{TItem, TParent}"/> class.<br/>.
    /// Which acts as the base class for nodes of model which are of collection type
    /// </summary>
    /// <typeparam name="TItem">The type of elements in the list.</typeparam>
    /// <typeparam name="TParent">The owner type of list.</typeparam>
    /// <typeparam name="TSearch">The type of search element.</typeparam>
    [JsonArray]
    [Serializable]
    public abstract class BaseComplexModelCollection<TItem, TParent, TSearch> : BaseSimpleModelCollection<TItem, TParent>
    {
        #region constructor/s

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseComplexModelCollection{TItem, TParent, TSearch}"/> class.
        /// </summary>
        /// <param name="parent">Parent type.</param>
        protected BaseComplexModelCollection(TParent parent) : base(parent)
        {
        }

        #endregion

        #region public indexer

        /// <summary>
        /// Gets or sets the element specified by <paramref name="value"/>.
        /// </summary>
        /// <value>
        /// Item
        /// </value>
        /// <param name="value">Zero-based index of the element to get or set.</param>
        /// <returns>
        /// The value.
        /// </returns>
        public TItem this[TSearch value] => GetBy(value);

        #endregion

        #region public methods

        /// <summary>
        /// Determines whether an element is in the <see cref="BaseComplexModelCollection{TItem, TParent, TSearch}"/>.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="BaseComplexModelCollection{TItem, TParent, TSearch}"/>.</param>
        /// <returns>
        /// <b>true</b> if <paramref name="value"/> is found in the <see cref="BaseComplexModelCollection{TItem, TParent, TSearch}"/>; otherwise, <b>false</b>.
        /// </returns>
        public bool Contains(TSearch value) => GetBy(value) != null;

        #endregion

        #region public abstract methods

        /// <summary>
        /// Returns the element specified.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.</param>
        /// <returns>
        /// Returns the specified element.
        /// </returns>
        public abstract TItem GetBy(TSearch value);

        #endregion
    }
}
