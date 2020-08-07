
namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    using System.IO;

    using iTin.Core.Helpers;

    using Replacement.Text;
    using Result.Replace;

    /// <summary>
    /// Specialization of <see cref="IReplace"/> interface.<br/>
    /// Contains the information for replace a text object.
    /// </summary>
    public class ReplaceSystemTag : IReplace
    {
        #region constructor/s

        #region [public] ReplaceSystemTag(ISystemTagTextReplacement): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplaceSystemTag"/> class.
        /// </summary>
        /// <param name="replacementObject">A object implementation of <see cref="ISystemTagTextReplacement"/>.</param>
        public ReplaceSystemTag(ISystemTagTextReplacement replacementObject)
        {
            ReplacementObject = replacementObject;
        }
        #endregion

        #endregion

        #region interfaces

        #region IReplace

        #region public methods

        #region [public] (bool) Apply(string, IInput context): Try to execute the replacement action
        /// <summary>
        /// Try to execute the replacement action.
        /// </summary>
        /// <param name="file">file input</param>
        /// <param name="context">Input context</param>
        /// <returns>
        /// <para>
        /// A <see cref="ReplaceResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="ReplaceResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public ReplaceResult Apply(string file, IInput context) => Apply(StreamHelper.TextFileToStream(file), context);
        #endregion

        #region [public] (bool) Apply(Stream, IInput context): Try to execute the replacement action
        /// <summary>
        /// Try to execute the replacement action.
        /// </summary>
        /// <param name="input">stream input</param>
        /// <param name="context">Input context</param>
        /// <returns>
        /// <para>
        /// A <see cref="ReplaceResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="ReplaceResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public ReplaceResult Apply(Stream input, IInput context) => ReplacementObject.Apply(input, context);
        #endregion

        #endregion

        #endregion

        #endregion

        #region public properties

        #region [public] (ISystemTagTextReplacement) ReplacementObject: Gets a reference to the replacement object
        /// <summary>
        /// Gets a reference to the replacement object.
        /// </summary>
        /// <value>
        /// A reference to replacement object.
        /// </value>
        public ISystemTagTextReplacement ReplacementObject { get; }
        #endregion

        #endregion
    }
}
