
namespace iTin.Core.Models.Design
{
    /// <summary>
    /// Defines a generic interface that defines an element can be combined with another element of the same type
    /// </summary>
    public interface ICombinable<T> where T: class
    {
        /// <summary>
        /// Combines this instance with reference parameter.
        /// </summary>
        /// <param name="reference">Target Reference</param>
        void Combine(T reference);
    }
}
