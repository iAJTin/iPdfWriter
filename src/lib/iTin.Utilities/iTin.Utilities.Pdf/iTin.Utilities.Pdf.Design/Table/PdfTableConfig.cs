
namespace iTin.Utilities.Pdf.Design.Table
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;

    /// <summary>
    /// Represents configuration information for an object <see cref="PdfTable"/>.
    /// </summary>
    public sealed class PdfTableConfig : ICloneable
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const TableHeightStrategy DefaultHeightStrategy = TableHeightStrategy.Auto;
        #endregion

        #region public static members
        /// <summary>
        /// Defaults configuration. Transparent background is not used.
        /// </summary>
        public static readonly PdfTableConfig Default = new PdfTableConfig();
        #endregion

        #region constructor/s

        #region [public] PdfTableConfig(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTableConfig"/> class.
        /// </summary>
        public PdfTableConfig()
        {
            HeightStrategy = DefaultHeightStrategy;
        }
        #endregion

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

        #region [private] (object) Clone(): Creates a new object that is a copy of the current instance
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

        #region [public] (TableHeightStrategy) HeightStrategy: Gets or sets a value that indicates the strategy to determine the height of the table to insert
        /// <summary>
        /// Gets or sets a value that indicates the strategy to determine the height of the table to insert. The default value is <see cref="TableHeightStrategy.Auto"/>.
        /// </summary>
        /// <value>
        /// One of the <see cref="TableHeightStrategy"/> enumeration values.
        /// </value>
        [DefaultValue(DefaultHeightStrategy)]
        public TableHeightStrategy HeightStrategy { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfTableConfig) Clone(): Clones this instance
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public PdfTableConfig Clone() => (PdfTableConfig)MemberwiseClone();

        #endregion

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a string than represents the current instance
        /// <summary>
        /// Returns a string that represents the current instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current instance.
        /// </returns>
        public override string ToString() => $"HeightStrategy = {HeightStrategy}";
        #endregion

        #endregion
    }
}
