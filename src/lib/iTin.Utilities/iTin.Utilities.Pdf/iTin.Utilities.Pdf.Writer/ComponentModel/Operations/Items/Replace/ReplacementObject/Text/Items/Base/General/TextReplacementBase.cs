
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text
{
    using System.IO;

    using iTin.Core.Helpers;
    using iTin.Core.Models.Design.Styling;

    using Result.Replace;

    /// <summary>
    /// Specialization of <see cref="IReplacement"/> interface.<br/>
    /// Acts as base class for text insert actions.
    /// </summary>
    public abstract class TextReplacementBase : ITextReplacement
    {
        #region interface

        #region IReplacement

        #region public methods

        #region [public] (ReplaceResult) Apply(string, IInput): Try to execute the replacement action
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

        #region [public] (ReplaceResult) Apply(Stream, IInput): Try to execute the replacement action
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
        public ReplaceResult Apply(Stream input, IInput context) => ReplaceImpl(input, context);
        #endregion

        #endregion

        #endregion

        #region ITextReplacement

        #region public properties

        #region [public] (ReplaceTextOptions) ReplaceOptions: Gets or sets a value that represents replace text options
        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value that represents text replace options.
        /// </summary>
        /// <value>
        /// A <see cref="ReplaceTextOptions"/> instance that contains text options.
        /// </value>
        public ReplaceTextOptions ReplaceOptions { get; set; }
        #endregion

        #region [public] (string) Text: Gets or sets the text to replace
        /// <summary>
        /// Gets or sets the text to replace.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> that contains the text to replace.
        /// </value>
        public string Text { get; set; }
        #endregion

        #endregion

        #endregion

        #endregion

        #region public properties

        #region [public] (BaseStyle) Style: Gets or sets a reference to style to apply
        /// <summary>
        /// Gets or sets a reference to style to apply.
        /// </summary>
        /// <value>
        /// A <see cref="BaseStyle"/> object that contains a reference to style to apply
        /// </value>
        public BaseStyle Style { get; set; }
        #endregion

        #endregion

        #region protected abtract methods

        #region [public] {abstract} (ReplaceResult) ReplaceImpl(Stream, IInput): Implementation to execute when replace action 
        /// <summary>
        /// Implementation to execute when replace action.
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
        protected abstract ReplaceResult ReplaceImpl(Stream input, IInput context);
        #endregion

        #endregion
    }
}
