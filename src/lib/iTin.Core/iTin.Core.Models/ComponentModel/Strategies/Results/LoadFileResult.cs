
using System.Collections.Generic;

using iTin.Core.ComponentModel;

namespace iTin.Core.Models.ComponentModel.Strategies.Result
{
    /// <summary>
    /// Specialization of <see cref="ResultBase{LoadFileResultData}"/> interface.<br/>
    /// Represents result when load a style file.
    /// </summary>
    public class LoadFileResult : ResultBase<LoadFileResultData>
    {
        #region [public] {new} {static} (LoadFileResult) CreateErroResult(string, string = ""): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed error.
        /// </returns>
        public new static LoadFileResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });
        #endregion

        #region [public] {new} {static} (LoadFileResult) CreateErroResult(IResultError[]): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </returns>
        public new static LoadFileResult CreateErroResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {new} {static} (LoadFileResult) CreateErroResult(string, LoadFileResultData, string = null): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="Result">Response Result</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </returns>
        public new static LoadFileResult CreateErroResult(string message, LoadFileResultData Result, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } }, Result);
        #endregion

        #region [public] {new} {static} (LoadFileResult) CreateErroResult(IResultError[], LoadFileResultData): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </returns>
        public new static LoadFileResult CreateErroResult(IResultError[] errors, LoadFileResultData Result) =>
            new()
            {
                Result = Result,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {new} {static} (LoadFileResult) CreateSuccessResult(LoadFileResultData): Returns a new success result
        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="LoadFileResult"/>.
        /// </returns>
        public new static LoadFileResult CreateSuccessResult(LoadFileResultData Result) =>
            new()
            {
                Result = Result,
                Success = true,
                Errors = new List<IResultError>()
            };
        #endregion

        #region [public] {new} {static} (LoadFileResult) FromException(Exception): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="LoadFileResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <returns>
        /// A new <see cref="LoadFileResult"/> instance for specified exception.
        /// </returns>
        public new static LoadFileResult FromException(System.Exception exception) => FromException(exception, default);
        #endregion

        #region [public] {new} {static} (LoadFileResult) FromException(Exception, LoadFileResultData): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="LoadFileResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new <see cref="LoadFileResult"/> instance for specified exception.
        /// </returns>
        public new static LoadFileResult FromException(System.Exception exception, LoadFileResultData Result) =>
            new()
            {
                Result = Result,
                Success = false,
                Errors = new List<IResultError> { new ResultExceptionError { Exception = exception } }
            };
        #endregion
    }
}
