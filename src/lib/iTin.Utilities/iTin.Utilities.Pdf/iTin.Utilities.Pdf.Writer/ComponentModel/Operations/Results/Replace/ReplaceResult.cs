
using System.Collections.Generic;

using iTin.Core.ComponentModel;

using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace
{
    /// <summary>
    /// Specialization of <see cref="ResultBase{ReplaceResultData}"/> interface.<br/>
    /// Represents result after insert an element in <b>pdf</b> document.
    /// </summary>
    public class ReplaceResult : ResultBase<ReplaceResultData>
    {
        #region public static methods 

        #region [public] {static} (ReplaceResult) CreateErroResult(string, string = ""): Returns a new InsertResult with specified detailed error
        /// <summary>
        /// Returns a new <see cref="ReplaceResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="ReplaceResult"/> with specified detailed error.
        /// </returns>
        public static ReplaceResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });
        #endregion

        #region [public] {static} (ReplaceResult) CreateErroResult(IResultError[]): Returns a new InsertResult with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="ReplaceResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="ReplaceResult"/> with specified detailed errors collection.
        /// </returns>
        public static ReplaceResult CreateErroResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #endregion

        #region public static new methods 

        #region [public] {new} {static} (ReplaceResult) CreateSuccessResult(ReplaceResultData): Returns a new success result
        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="ReplaceResult"/>.
        /// </returns>
        public new static ReplaceResult CreateSuccessResult(ReplaceResultData result) =>
            new()
            {
                Result = result,
                Success = true,
                Errors = new List<IResultError>()
            };
        #endregion

        #region [public] {new} {static} (ReplaceResult) FromException(Exception): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="ReplaceResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <returns>
        /// A new <see cref="ReplaceResult"/> instance for specified exception.
        /// </returns>
        public new static ReplaceResult FromException(System.Exception exception) => FromException(exception, default);
        #endregion

        #region [public] {new} {static} (ReplaceResult) FromException(Exception, ReplaceResultData): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="InsertResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new <see cref="ReplaceResult"/> instance for specified exception.
        /// </returns>
        public new static ReplaceResult FromException(System.Exception exception, ReplaceResultData result) =>
            new()
            {
                Result = result,
                Success = false,
                Errors = new List<IResultError> { new ResultExceptionError { Exception = exception } }
            };
        #endregion

        #endregion
    }
}
