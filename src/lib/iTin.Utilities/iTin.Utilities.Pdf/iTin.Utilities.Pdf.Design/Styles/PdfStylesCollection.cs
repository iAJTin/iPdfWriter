
using System;

using iTin.Core.Helpers;

using iTin.Core.Models.Collections;
using iTin.Core.Models.Design.Styling;

namespace iTin.Utilities.Pdf.Design.Styles
{
    /// <summary>
    /// Defines a styles collection.
    /// </summary>
    public partial class PdfStylesCollection : IPdfStyles
    {
        #region constructor/s

        #region [public] PdfStylesCollection(): Initializes a new instance of the class
        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfStylesCollection"/> class.
        /// </summary>
        public PdfStylesCollection() : base(null)
        {
        }
        #endregion

        #region [public] PdfStylesCollection(object): Initializes a new instance of the class
        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfStylesCollection"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public PdfStylesCollection(object parent) : base(parent)
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

        #region IStyles

        #region explicit

        #region (IStyle) IStyles.GetBy(string): Returns specified style by name
        /// <summary>
        /// Returns specified style by name.
        /// </summary>
        /// <param name="value">Style name to get</param>
        /// <returns>
        /// A <see cref="IStyle"/> reference.
        /// </returns>
        IStyle IStyles.GetBy(string value) => GetBy(value);
        #endregion

        #endregion

        #endregion

        #region IPdfStyles

        #region explicit

        #region (IPdfStyle) IPdfStyles.GetBy(string): Returns specified style by name
        /// <summary>
        /// Returns specified style by name.
        /// </summary>
        /// <param name="value">Style name to get</param>
        /// <returns>
        /// A <see cref="IStyle"/> reference.
        /// </returns>
        IPdfStyle IPdfStyles.GetBy(string value) => GetBy(value);
        #endregion

        #endregion

        #endregion

        #endregion

        #region protected override methods

        #region [protected] {override} (PdfBaseStyle) GetBy(string): Returns the element specified
        /// <inheritdoc />
        /// <summary>
        /// Returns the element specified.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="BaseComplexModelCollection{TItem,TParent,TSearch}"/>.</param>
        /// <returns>
        /// Returns the specified element.
        /// </returns>
        public override PdfBaseStyle GetBy(string value) 
        {
            if (string.IsNullOrEmpty(value))
            {
                return PdfBaseStyle.Default;
            }

            var style = Find(stle => stle.Name.Equals(value));

            return style ?? PdfBaseStyle.Default;
        }
        #endregion

        #region [protected] {override} (void) SetOwner(PdfBaseStyle): Sets this collection as the owner of the specified item
        /// <inheritdoc />
        /// <summary>
        /// Sets this collection as the owner of the specified item.
        /// </summary>
        /// <param name="item">Target item to set owner.</param>
        protected override void SetOwner(PdfBaseStyle item)
        {
            SentinelHelper.ArgumentNull(item, nameof(item));

            item.SetOwner(this);
        }
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfStylesCollection) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfStylesCollection Clone() => CopierHelper.DeepCopy(this);
        #endregion

        #endregion
    }
}
