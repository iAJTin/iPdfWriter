﻿
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output
{
    using System.Collections.Generic;

    using iTin.Core.ComponentModel;

    /// <summary>
    /// Specialization of <see cref="ResultBase{OutputResultData}"/> interface.<br/>
    /// Represents configuration information for an object <see cref="OutputResult"/>.
    /// </summary>
    public class OutputResult : ResultBase<OutputResultData>
    {
        /// <summary>
        /// Returns a new <see cref="OutputResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="OutputResult"/> with specified detailed error.
        /// </returns>
        public new static OutputResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });

        /// <summary>
        /// Returns a new <see cref="OutputResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="OutputResult"/> with specified detailed errors collection.
        /// </returns>
        public new static OutputResult CreateErroResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };

        /// <summary>
        /// Returns a new <see cref="OutputResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="Result">Response Result</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="OutputResult"/> with specified detailed errors collection.
        /// </returns>
        public new static OutputResult CreateErroResult(string message, OutputResultData Result, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } }, Result);

        /// <summary>
        /// Returns a new <see cref="OutputResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new invalid <see cref="OutputResult"/> with specified detailed errors collection.
        /// </returns>
        public new static OutputResult CreateErroResult(IResultError[] errors, OutputResultData Result) =>
            new()
            {
                Result = Result,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };

        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="OutputResult"/>.
        /// </returns>
        public new static OutputResult CreateSuccessResult(OutputResultData Result) =>
            new()
            {
                Result = Result,
                Success = true,
                Errors = new List<IResultError>()
            };

        /// <summary>
        /// Creates a new <see cref="OutputResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <returns>
        /// A new <see cref="OutputResult"/> instance for specified exception.
        /// </returns>
        public new static OutputResult FromException(System.Exception exception) => FromException(exception, default);

        /// <summary>
        /// Creates a new <see cref="OutputResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new <see cref="OutputResult"/> instance for specified exception.
        /// </returns>
        public new static OutputResult FromException(System.Exception exception, OutputResultData Result) =>
            new()
            {
                Result = Result,
                Success = false,
                Errors = new List<IResultError> { new ResultExceptionError { Exception = exception } }
            };
    }
}
