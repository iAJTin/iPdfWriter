
using iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text;

namespace iTin.Utilities.Pdf.Writer.Operations.Replace
{
    /// <summary>
    /// Specialization of <see cref="IReplace"/> interface.<br/>
    /// Contains the information for replace a text object.
    /// </summary>
    public class ReplaceSystemTag : IReplace
    {
        #region constructor/s

        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceSystemTag"/> class.
        /// </summary>
        /// <param name="replacementObject">A object implementation of <see cref="ISystemTagTextReplacement"/>.</param>
        public ReplaceSystemTag(ISystemTagTextReplacement replacementObject)
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
        public ISystemTagTextReplacement ReplacementObject { get; }

        #endregion
    }
}
