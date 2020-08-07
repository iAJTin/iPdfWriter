
namespace iTin.Core.Models.Design.Styling
{
    using System;

    /// <summary>
    /// Defines a generic style
    /// </summary>
    public interface IStyles : ICloneable, IOwner
    {
        /// <summary>
        /// Returns specified style by name
        /// </summary>
        /// <param name="value">Style name to get</param>
        /// <returns>
        /// A <see cref="IStyle"/> reference.
        /// </returns>
        IStyle GetBy(string value);
    }
}
