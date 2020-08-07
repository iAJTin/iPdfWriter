
namespace iTin.Utilities.Pdf.Design.Styles
{
    using iTin.Core.Models.Design.Styling;

    /// <summary>
    /// Defines a generic pdf styles collection
    /// </summary>
    public interface IPdfStyles : IStyles
    {
        /// <summary>
        /// Returns specified style by name
        /// </summary>
        /// <param name="value">Style name to get</param>
        /// <returns>
        /// A <see cref="IStyle"/> reference.
        /// </returns>
        new IPdfStyle GetBy(string value);
    }
}
