
using System.Collections.Generic;

using iTin.Core.ComponentModel;

using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Set
{
    /// <summary>
    /// Specialization of <see cref="ResultBase{SetResultData}"/> interface.<br/>
    /// Represents result after set an element in <b>pdf</b> document.
    /// </summary>
    public class SetResult : ResultBase<SetResultData>
    {
        #region public static methods 

        #region [public] {static} (SetResult) CreateErroResult(string, string = ""): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed error.
        /// </returns>
        public static SetResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });
        #endregion

        #region [public] {static} (SetResult) CreateErroResult(IResultError[]): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed errors collection.
        /// </returns>
        public static SetResult CreateErroResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {static} (SetResult) CreateErroResult(string, SetResultData, string = null): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="result">Response Result</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed errors collection.
        /// </returns>
        public static SetResult CreateErroResult(string message, SetResultData result, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } }, result);
        #endregion

        #region [public] {static} (SetResult) CreateErroResult(IResultError[], SetResultData): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed errors collection.
        /// </returns>
        public static SetResult CreateErroResult(IResultError[] errors, SetResultData result) =>
            new()
            {
                Result = result,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #endregion

        #region public new static methods 

        #region [public] {new} {static} (SetResult) CreateSuccessResult(SetResultData): Returns a new success result
        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="InsertResult"/>.
        /// </returns>
        public new static SetResult CreateSuccessResult(SetResultData result) =>
            new()
            {
                Result = result,
                Success = true,
                Errors = new List<IResultError>()
            };
        #endregion

        #region [public] {new} {static} (SetResult) FromException(Exception): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="SetResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <returns>
        /// A new <see cref="SetResult"/> instance for specified exception.
        /// </returns>
        public new static SetResult FromException(System.Exception exception) => FromException(exception, default);
        #endregion

        #region [public] {new} {static} (SetResult) FromException(Exception, SetResultData): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="SetResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new <see cref="SetResult"/> instance for specified exception.
        /// </returns>
        public new static SetResult FromException(System.Exception exception, SetResultData result) =>
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
