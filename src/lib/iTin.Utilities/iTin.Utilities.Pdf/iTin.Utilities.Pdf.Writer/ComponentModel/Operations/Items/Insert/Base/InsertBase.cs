
using System.Drawing;
using System.IO;

using iTin.Core.Helpers;
using iTin.Core.Models.Design.Styling;

using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <inheritdoc/>
    /// <summary>
    /// Specialization of <see cref="IInsert"/> interface.<br/>
    /// Acts as base class for insert actions.
    /// </summary>
    public abstract class InsertBase : IInsert
    {
        #region interface

        #region IInsert

        #region public properties

        #region [public] (PointF) Offset: Gets or sets a reference a point structure which represents the element offset
        /// <summary>
        /// Gets or sets a reference a point structure which represents the element offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains image offset to apply.
        /// </value>
        public PointF Offset { get; set; }
        #endregion

        #region [public] (int) Page: Gets or sets a value to page number on insert this image
        /// <summary>
        /// Gets or sets a value to page number on insert this image.
        /// </summary>
        /// <value>
        /// A <see cref="int"/> object that contains page number on insert this image.
        /// </value>
        public int Page { get; set; }
        #endregion

        #region [public] (BaseStyle) Style: Gets or sets a reference to cell style
        /// <summary>
        /// Gets or sets a reference to 
        /// </summary>
        /// <value>
        /// A <see cref="BaseStyle"/> object that contains a reference to cell style
        /// </value>
        public BaseStyle Style { get; set; }
        #endregion

        #endregion

        #region public methods

        #region [public] (InsertResult) Apply(string, IInput): Try to execute the insert action 
        /// <summary>
        /// Try to execute the insert action.
        /// </summary>
        /// <param name="file">file input</param>
        /// <param name="context">Input context</param>
        /// <returns>
        /// <para>
        /// A <see cref="InsertResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="InsertResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public InsertResult Apply(string file, IInput context) => Apply(StreamHelper.TextFileToStream(file), context);
        #endregion

        #region [public] (InsertResult) Apply(Stream, IInput): Try to execute the insert action 
        /// <summary>
        /// Try to execute the insert action.
        /// </summary>
        /// <param name="input">stream input</param>
        /// <param name="context">Input context</param>
        /// <returns>
        /// <para>
        /// A <see cref="InsertResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="InsertResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public InsertResult Apply(Stream input, IInput context) => InsertImpl(input, context);
        #endregion

        #endregion

        #endregion

        #endregion

        #region protected abtract methods

        #region [public] {abstract} (InsertResult) InsertImpl(Stream, IInput): Implementation to execute when insert action 
        /// <summary>
        /// Implementation to execute when insert action.
        /// </summary>
        /// <param name="input">stream input</param>
        /// <param name="context">Input context</param>
        /// <returns>
        /// <para>
        /// A <see cref="InsertResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="InsertResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        protected abstract InsertResult InsertImpl(Stream input, IInput context);
        #endregion

        #endregion
    }
}
