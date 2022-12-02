
namespace iPdfWriter.Operations.Replace
{
    using Replacement.Text;

    /// <inheritdoc/>
    /// <summary>
    /// Specialization of <see cref="IReplace"/> interface that contains the information for replace a text object.
    /// </summary>
    public class ReplaceText : IReplace
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceText"/> class.
        /// </summary>
        /// <param name="replacementObject">A object implementation of <see cref="ITextReplacement"/>.</param>
        public ReplaceText(ITextReplacement replacementObject)
        {
            ReplacementObject = replacementObject;
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets a reference to the replacement object.
        /// </summary>
        /// <value>
        /// A reference to replacement object.
        /// </value>
        public ITextReplacement ReplacementObject { get; }

        #endregion
    }
}
