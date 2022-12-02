
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
        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed error.
        /// </returns>
        public new static LoadFileResult CreateErrorResult(string message, string code = "") =>
            CreateErrorResult(new IResultError[] { new ResultError { Code = code, Message = message } });

        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </returns>
        public new static LoadFileResult CreateErrorResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };

        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="result">Response Result</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </returns>
        public new static LoadFileResult CreateErrorResult(string message, LoadFileResultData result, string code = "") =>
            CreateErrorResult(new IResultError[] { new ResultError { Code = code, Message = message } }, result);

        /// <summary>
        /// Returns a new <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new invalid <see cref="LoadFileResult"/> with specified detailed errors collection.
        /// </returns>
        public new static LoadFileResult CreateErrorResult(IResultError[] errors, LoadFileResultData result) =>
            new()
            {
                Result = result,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };

        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="LoadFileResult"/>.
        /// </returns>
        public new static LoadFileResult CreateSuccessResult(LoadFileResultData result) =>
            new()
            {
                Result = result,
                Success = true,
                Errors = new List<IResultError>()
            };

        /// <summary>
        /// Creates a new <see cref="LoadFileResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <returns>
        /// A new <see cref="LoadFileResult"/> instance for specified exception.
        /// </returns>
        public new static LoadFileResult FromException(System.Exception exception) => 
            FromException(exception, default);

        /// <summary>
        /// Creates a new <see cref="LoadFileResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new <see cref="LoadFileResult"/> instance for specified exception.
        /// </returns>
        public new static LoadFileResult FromException(System.Exception exception, LoadFileResultData result) =>
            new()
            {
                Result = result,
                Success = false,
                Errors = new List<IResultError> { new ResultExceptionError { Exception = exception } }
            };
    }
}
