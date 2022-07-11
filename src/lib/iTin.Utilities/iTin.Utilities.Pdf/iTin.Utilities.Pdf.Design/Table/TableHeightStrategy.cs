
using System;

namespace iTin.Utilities.Pdf.Design.Table
{
    /// <summary>
    /// Specifies the known table height strategy.
    /// </summary>
    [Serializable]
    public enum TableHeightStrategy
    {
        /// <summary>
        /// Automatically determine table height strategy
        /// </summary>
        Auto,

        /// <summary>
        /// Exact table height
        /// </summary>
        Exact
    }
}
