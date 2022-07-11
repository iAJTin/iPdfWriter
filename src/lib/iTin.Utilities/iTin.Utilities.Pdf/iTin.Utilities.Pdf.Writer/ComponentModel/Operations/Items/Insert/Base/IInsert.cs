
using System.Drawing;
using System.IO;

using iTin.Core.Models.Design.Styling;

using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;

namespace iTin.Utilities.Pdf.Writer.ComponentModel
{
    /// <summary>
    /// Defines allowed actions for insert objects
    /// </summary>
    public interface IInsert
    {
        /// <summary>
        /// Gets or sets a reference a point structure which represents the element offset. The default is <see cref="PointF.Empty"/>.
        /// </summary>
        /// <value>
        /// A <see cref="PointF"/> object that contains element offset to apply.
        /// </value>
        PointF Offset { get; set; }

        /// <summary>
        /// Gets or sets a value to page number on insert this image.
        /// </summary>
        /// <value>
        /// A <see cref="int"/> object that contains page number on insert this image.
        /// </value>
        int Page { get; set; }

        /// <summary>
        /// Gets or sets a reference to 
        /// </summary>
        /// <value>
        /// A <see cref="BaseStyle"/> object that contains a reference to cell style
        /// </value>
        BaseStyle Style { get; set; }



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
        InsertResult Apply(string file, IInput context);

        /// <summary>
        /// Try to execute the inseert action.
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
        InsertResult Apply(Stream input, IInput context);
    }
}
