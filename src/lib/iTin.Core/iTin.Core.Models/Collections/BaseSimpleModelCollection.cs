
namespace iTin.Core.Models.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    using Design;

    /// <summary>
    /// Represents a strongly typed list of model objects that can be accessed by index, allow defines type of parent.<br/>
    /// Provides methods to search, sort, and manipulate lists.
    /// </summary>
    /// <typeparam name="TItem">The type of elements in the list.</typeparam>
    /// <typeparam name="TParent">The owner type of list.</typeparam>
    [JsonArray]
    [Serializable]
    public abstract class BaseSimpleModelCollection<TItem, TParent> : BaseModel<BaseSimpleModelCollection<TItem, TParent>>, IList<TItem>, IOwner
    {
        #region constructor/s

        #region [protected] BaseSimpleModelCollection(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSimpleModelCollection{TItem, TParent}"/> class.
        /// </summary>
        protected BaseSimpleModelCollection() : this(default)
        {
        }
        #endregion

        #region [protected] BaseSimpleModelCollection(TParent): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSimpleModelCollection{TItem, TParent}"/> class.
        /// </summary>
        /// <param name="parent">Parent type.</param>
        protected BaseSimpleModelCollection(TParent parent)
        {
            List = new List<TItem>();
            Parent = parent;
        }
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (int) Count: Gets the number of elements contained in the collection
        /// <inheritdoc/>
        /// <summary>
        /// Gets the number of elements contained in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <value>
        /// The number of elements contained in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </value>
        [JsonIgnore]
        public int Count => List.Count;
        #endregion

        #region [public] (bool) IsReadOnly: Gets a value indicating whether the collection is read-only
        /// <inheritdoc/>
        /// <summary>
        /// Gets a value indicating whether the <see cref="BaseSimpleModelCollection{TItem, TParent}"/> is read-only.
        /// </summary>
        /// <value>
        /// Always is <b>false</b>
        /// </value>
        [JsonIgnore]
        public bool IsReadOnly => false;
        #endregion

        #region [public] (TParent) Parent: Gets a reference to the owner of the collection
        /// <summary>
        /// Gets a reference to the owner of the collection
        /// </summary>
        /// <value>
        /// Owner collection
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        public TParent Parent { get; }
        #endregion

        #endregion

        #region public override properties

        #region [public] {override} (bool) IsDefault: When overridden in a derived class, gets a value indicating whether this instance contains the default
        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, gets a value indicating whether this instance contains the default.
        /// </summary>
        /// <value>
        /// <b>true</b> if this instance contains the default; otherwise, <b>false</b>.
        /// </value>
        public override bool IsDefault => !this.Any();
        #endregion

        #endregion

        #region protected properties

        #region [protected] (List<TItem>) List: Gets a reference to the inner list
        /// <summary>
        /// Gets a reference to the inner list.
        /// </summary>
        /// <value>
        /// The inner list.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected List<TItem> List { get; set; }
        #endregion

        #endregion

        #region public indexer

        #region [public] (TItem) this[int]: Gets or sets the element at the specified index
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <value>
        /// Item at the specified index.
        /// </value>
        /// <param name="index">Zero-based index of the element to get or set.</param>
        /// <returns>
        /// the value
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index.</exception>
        /// <exception cref="T:System.NotSupportedException">The property is set and is readonly.</exception>
        public TItem this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }
        #endregion

        #endregion

        #region public methods

        #region [public] (void) Add(TItem): Adds an object to the end of the collection
        /// <inheritdoc />
        /// <summary>
        /// Adds an object to the end of the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <param name="item">The object to be added to the end of the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.</param>
        public void Add(TItem item)
        {
            SetOwner(item);

            List.Add(item);
        }
        #endregion

        #region [public] (void) Clear(): Removes all elements from the collection
        /// <inheritdoc />
        /// <summary>
        /// Removes all elements from the collection.
        /// </summary>
        public void Clear() => List.Clear();
        #endregion

        #region [public] (void) Contains(TItem): Determines whether an element is in the collection
        /// <inheritdoc/>
        /// <summary>
        /// Determines whether an element is in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>. The value can be <strong>null</strong> for reference types.</param>
        /// <returns>
        /// <b>true</b> if <paramref name="item"/> is found in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>; otherwise, <strong>false</strong>.
        /// </returns>
        /// <remarks>
        /// This method determines equality by using the default equality comparer, as defined by the object's implementation of the <see cref="IEquatable{TItem}.Equals"/> method for TItem (the type of values in the list).
        /// </remarks>
        public bool Contains(TItem item) => List.Contains(item);
        #endregion

        #region [public] (void) CopyTo(TItem[], int): Copies the entire BaseSimpleModelCollection to a compatible one-dimensional array, starting at the specified index of the target array
        /// <inheritdoc/>
        /// <summary>
        /// Copies the entire <see cref="BaseSimpleModelCollection{TItem, TParent}"/> to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="BaseSimpleModelCollection{TItem, TParent}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is <b>null</b>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source <see cref="BaseSimpleModelCollection{TItem, TParent}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination array.</exception>
        public void CopyTo(TItem[] array, int arrayIndex) => List.CopyTo(array, arrayIndex);
        #endregion

        #region [public] (TItem) Find(Predicate<TItem>): Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire BaseSimpleModelCollection
        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate, and returns the first occurrence within the entire <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <param name="match">The <see cref="Predicate{TItem}" /> delegate that defines the conditions of the element to search for.</param>
        /// <returns>
        /// The first element that matches the conditions defined by the specified predicate, if found; otherwise, the default value for type TItem.
        /// </returns>
        public TItem Find(Predicate<TItem> match) => List.Find(match);
        #endregion

        #region [public] (IEnumerator<TItem>) GetEnumerator(): Returns an enumerator that iterates through the BaseSimpleModelCollection
        /// <inheritdoc/>
        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}.Enumerator"/> for the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </returns>
        public IEnumerator<TItem> GetEnumerator() => List.GetEnumerator();
        #endregion

        #region (IEnumerator) IEnumerable.GetEnumerator(): Returns an enumerator that iterates through a collection
        /// <inheritdoc/>
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => List.GetEnumerator();
        #endregion

        #region [public] (int) IndexOf(TItem): Searches for the specified object and returns the zero-based index of the first occurrence within the entire BaseSimpleModelCollection
        /// <inheritdoc/>
        /// <summary>
        /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>. The value can be <b>null</b> for reference types.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of item within the entire <see cref="BaseSimpleModelCollection{TItem, TParent}"/>, if found; otherwise, –1.
        /// </returns>
        public int IndexOf(TItem item) => List.IndexOf(item);
        #endregion

        #region [public] (void) Insert(int, TItem): Inserts an element into the BaseSimpleModelCollection at the specified index
        /// <inheritdoc/>
        /// <summary>
        /// Inserts an item to the <see cref="BaseSimpleModelCollection{TItem, TParent}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert. The value can be <b>null</b> for reference types.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 - or - index is greater than <see cref="BaseSimpleModelCollection{TItem, TParent}.Count"/>.</exception>
        public void Insert(int index, TItem item) => List.Insert(index, item);
        #endregion

        #region [public] (bool) Remove(TItem): Removes the first occurrence of a specific object from the BaseSimpleModelCollection
        /// <inheritdoc/>
        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>. The value can be <b>null</b> for reference types.</param>
        /// <returns>
        /// <b>true</b> if item is successfully removed; otherwise, <b>false</b>. This method also returns <b>false</b> if item was not found in the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </returns>
        public bool Remove(TItem item) => List.Remove(item);
        #endregion

        #region [public] (void) RemoveAt(int): Removes the element at the specified index of the BaseSimpleModelCollection
        /// <inheritdoc/>
        /// <summary>
        /// Removes the element at the specified index of the <see cref="BaseSimpleModelCollection{TItem, TParent}"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the element to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0 - or - index is greater than <see cref="BaseSimpleModelCollection{TItem, TParent}.Count"/>.</exception>
        public void RemoveAt(int index) => List.RemoveAt(index);
        #endregion

        #endregion

        #region protected abstract methods

        #region [protected] {abstract} (void) SetOwner(TItem): Sets this collection as the owner of the specified item
        /// <summary>
        /// Sets this collection as the owner of the specified item.
        /// </summary>
        /// <param name="item">Target item to set owner.</param>
        protected abstract void SetOwner(TItem item);
        #endregion

        #endregion
    }
}
