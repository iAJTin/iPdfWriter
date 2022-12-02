
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace iTin.Utilities.Pdf.Design.Table
{
    /// <summary>
    /// Represents configuration information for an object <see cref="PdfTable"/>.
    /// </summary>
    public sealed class PdfTableConfig : ICloneable
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const TableHeightStrategy DefaultHeightStrategy = TableHeightStrategy.Exact;

        #endregion

        #region public static members

        /// <summary>
        /// Defaults configuration. Transparent background is not used.
        /// </summary>
        public static readonly PdfTableConfig Default = new();

        #endregion

        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTableConfig"/> class.
        /// </summary>
        public PdfTableConfig()
        {
            HeightStrategy = DefaultHeightStrategy;
        }

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

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

        #region public properties

        /// <summary>
        /// Gets or sets a value that indicates the strategy to determine the height of the table to insert. The default value is <see cref="TableHeightStrategy.Exact"/>.
        /// </summary>
        /// <value>
        /// One of the <see cref="TableHeightStrategy"/> enumeration values.
        /// </value>
        [DefaultValue(DefaultHeightStrategy)]
        public TableHeightStrategy HeightStrategy { get; set; }

        #endregion

        #region public methods

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTableConfig Clone() => 
            (PdfTableConfig)MemberwiseClone();

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current instance.
        /// </returns>
        public override string ToString() => 
            $"HeightStrategy = {HeightStrategy}";

        #endregion
    }
}
