
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

        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfStylesCollection"/> class.
        /// </summary>
        public PdfStylesCollection() : base(null)
        {
        }

        /// <inheritdoc/>
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfStylesCollection"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public PdfStylesCollection(object parent) : base(parent)
        {
        }

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

        #region IStyles

        #region explicit

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

        #region IPdfStyles

        #region explicit

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

        #region protected override methods

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

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfStylesCollection Clone() => CopierHelper.DeepCopy(this);

        #endregion
    }
}
