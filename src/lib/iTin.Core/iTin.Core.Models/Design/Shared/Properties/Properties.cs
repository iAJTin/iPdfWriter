
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using iTin.Core.Helpers;

namespace iTin.Core.Models
{
    /// <summary>
    /// Defines a user-custom property.
    /// </summary>
    public partial class Properties : IList<Property>, ICloneable
    {
        #region private members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly List<Property> _list;
        #endregion

        #region constructor/s

        #region [public] Properties(): Initializes a new instance of this class
        /// <summary>
        /// Initializes a new instance of the <see cref="Properties"/> class.
        /// </summary>
        public Properties()
        {
            _list = new List<Property>();
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

        #region public readonly properties

        #region [public] (int) Count: Gets the number of property items contained in this properties
        /// <summary>
        /// Gets the number of property items contained in this <see cref="Properties"/>.
        /// </summary>
        /// <returns>
        /// The number of property items contained in this  <see cref="Properties"/>.
        /// </returns>
        public int Count => _list.Count;
        #endregion

        #region [public] (bool) IsReadOnly: Gets a value indicating whether this properties is read-only.
        /// <summary>
        /// Gets a value indicating whether this <see cref="Properties"/> is read-only.
        /// </summary>
        /// <returns>
        /// Always returns <see langword="false"/>.
        /// </returns>
        public bool IsReadOnly => false;
        #endregion

        #endregion

        #region public overrides properties

        #region [public] {overide} (bool) IsDefault: Gets a value indicating whether this instance contains the default
        /// <inheritdoc />
        public override bool IsDefault => !this.Any();
        #endregion

        #endregion

        #region public indexers

        #region [public] (Property) this[int]: Gets or sets the property at the specified index
        /// <summary>
        /// Gets or sets the property at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the property to get or set.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///        <paramref name="index" /> is less than 0.
        /// 
        /// -or-
        /// 
        /// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.List`1.Count" />.</exception>
        /// <returns>
        /// The property at the specified index.
        /// </returns>
        public Property this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }
        #endregion

        #region [public] (Property) this[string]: Gets or sets the property with the specified name
        /// <summary>
        /// Gets or sets the property with the specified name.
        /// </summary>
        /// <param name="name">The property name to get or set.</param>
        /// <returns>
        /// The property with the specified name.
        /// </returns>
        public Property this[string name] => GetByName(name);
        #endregion

        #endregion

        #region public methods

        #region [public] (void) Add(Property): Adds a property item to the end of this properties
        /// <summary>
        /// Adds a property item to the end of this <see cref="Properties"/>.
        /// </summary>
        /// <param name="item">The property item to be added to the end of this <see cref="Properties"/>. The value can not be <see langword="null"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        public void Add(Property item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item!.SetOwner(this);
            _list.Add(item);
        }
        #endregion

        #region [public] (void) Clear(): Removes all property items from this properties
        /// <summary>
        /// Removes all property items from this <see cref="Properties"/>.
        /// </summary>
        public void Clear() => _list.Clear();
        #endregion

        #region [public] (Properties) Clone(): Clones this instance.
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public Properties Clone()
        {
            var propertiesCloned = new Properties();

            foreach (var property in this)
            {
                property.SetOwner(propertiesCloned);
                propertiesCloned.Add(property.Clone());
            }

            return propertiesCloned;
        }
        #endregion

        #region [public] (bool) Contains(Property): Determines whether a property item is in this properties
        /// <summary>
        /// Determines whether a property item is in this <see cref="Properties"/>.
        /// </summary>
        /// <param name="item">The target property to locate. The value can not be <see langword="null"/>.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> is found in this <see cref="Properties"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Contains(Property item) => _list.Contains(item);
        #endregion

        #region [public] (bool) Contains(string): Determines whether a property name item is in this properties
        /// <summary>
        /// Determines whether a property name item is in this <see cref="Properties"/>.
        /// </summary>
        /// <param name="name">The target property name to locate. The value can not be <see langword="null"/>.</param>
        /// <returns>
        /// <see langword="true"/> if property with <paramref name="name"/> is found in this <see cref="Properties"/>; otherwise, <see langword="false" />.
        /// </returns>
        public bool Contains(string name) => GetByName(name) != null;
        #endregion

        #region [public] (void) CopyTo(Property[], int): Inserts an element into this properties at the specified index
        /// <summary>
        /// Copies the entire <see cref="Properties"/> to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="Properties"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// The number of elements in the source <see cref="Properties"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(Property[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        #endregion

        #region [public] (int) IndexOf(Property): Determines whether a property name item is in this properties
        /// <summary>
        /// Searches for the specified property and returns the zero-based index of the first occurrence within the entire this <see cref="Properties"/>.
        /// </summary>
        /// <param name="item">The target property to locate. The value can not be <see langword="null"/>.</param>
        /// <returns>
        /// The zero-based index of the first occurrence of <paramref name="item" /> within the entire this <see cref="Properties"/>, if found; otherwise, -1.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        public int IndexOf(Property item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            return _list.IndexOf(item);
        }
        #endregion

        #region [public] (IEnumerator<Property>) GetEnumerator(): Returns an enumerator that iterates through a collection
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<Property> GetEnumerator() => _list.GetEnumerator();
        #endregion

        #region [public] (IEnumerator) IEnumerable.GetEnumerator()(): Returns an enumerator that iterates through a collection
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();
        #endregion

        #region [public] (void) Insert(int, Property): Inserts an element into this properties at the specified index
        /// <summary>
        /// Inserts an element into this <see cref="Properties"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item" /> should be inserted.</param>
        /// <param name="item">The propery to insert. The value can not be <see langword="null" />.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="index" /> is less than 0.
        /// 
        /// -or-
        /// 
        /// <paramref name="index" /> is greater than <see cref="Count" />.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        public void Insert(int index, Property item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            _list.Insert(index, item);
        }
        #endregion

        #region [public] (bool) Remove(Property): Removes the first occurrence of a specific object from this properties
        /// <summary>
        /// Removes the first occurrence of a specific object from this <see cref="Properties"/>.
        /// </summary>
        /// <param name="item">The object to remove from this <see cref="Properties"/>. The value can not be <see langword="null"/>.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item" /> is successfully removed; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if <paramref name="item" /> was not found in this <see cref="Properties"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="item"/> is <see langword="null"/></exception>
        public bool Remove(Property item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            return _list.Remove(item);
        }
        #endregion

        #region [public] (void) RemoveAt(int): Inserts an element into this properties at the specified index
        /// <summary>
        /// Removes the property at the specified index of this <see cref="Properties"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the property to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///        <paramref name="index" /> is less than 0.
        /// 
        /// -or-
        /// 
        /// <paramref name="index" /> is equal to or greater than <see cref="Count" />.</exception>
        public void RemoveAt(int index) => _list.RemoveAt(index);
        #endregion

        #endregion

        #region private methods

        #region [private] (PropertyModel) GetByName(string): Returns a reference to the specified style
        private Property GetByName(string name) => _list.Find(s => s.Name.Equals(name));
        #endregion

        #endregion
    }
}
