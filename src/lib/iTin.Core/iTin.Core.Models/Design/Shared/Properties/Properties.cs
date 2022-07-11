
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

        #region public properties

        #region [public] (int) Count:
        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.</returns>
        public int Count => _list.Count;
        #endregion

        #region [public] (bool) IsReadOnly:
        /// <inheritdoc />
        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.</returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Property this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Property this[string name] => GetByName(name);

        #endregion

        #region public methods

        #region [public] (void) Add(PropertyModel):
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(Property item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
            _list.Add(item);
        }
        #endregion

        #region [public] (void) Clear():
        /// <summary>
        /// 
        /// </summary>
        public void Clear() => _list.Clear();
        #endregion

        #region [public] (Properties) Clone(): Clones this instance.
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
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

        #region [public] (bool) Contains(Property): Returns a value indicating whether the style exist
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Property item) => _list.Contains(item);
        #endregion

        #region [public] (bool) Contains(string): Returns a value indicating whether the style exist
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Contains(string name) => GetByName(name) != null;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(Property item) => _list.IndexOf(item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, Property item) => _list.Insert(index, item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index) => _list.RemoveAt(index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(Property[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(Property item) => _list.Remove(item);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Property> GetEnumerator() => _list.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        #endregion

        #region private methods

        #region [private] (PropertyModel) GetByName(string): Returns a reference to the specified style
        private Property GetByName(string name) => _list.Find(s => s.Name.Equals(name));
        #endregion

        #endregion
    }
}
