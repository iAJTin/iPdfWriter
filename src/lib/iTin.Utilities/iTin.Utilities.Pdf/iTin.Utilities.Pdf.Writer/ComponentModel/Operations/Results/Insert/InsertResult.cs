
using System.Collections.Generic;

using iTin.Core.ComponentModel;

using iTin.Utilities.Pdf.Writer.ComponentModel.Input;

namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert
{
    /// <summary>
    /// Specialization of <see cref="ResultBase{InsertResultData}"/> interface.<br/>
    /// Represents result after insert an element in <b>pdf</b> document.
    /// </summary>
    public class InsertResult : ResultBase<InsertResultData>
    {
        #region public static methods 

        #region [public] {static} (InsertResult) CreateErroResult(string, string = ""): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed error.
        /// </returns>
        public static InsertResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });
        #endregion

        #region [public] {static} (InsertResult) CreateErroResult(IResultError[]): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed errors collection.
        /// </returns>
        public static InsertResult CreateErroResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {static} (InsertResult) CreateErroResult(string, InsertResultData, string = null): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="result">Response Result</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed errors collection.
        /// </returns>
        public static InsertResult CreateErroResult(string message, InsertResultData result, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } }, result);
        #endregion

        #region [public] {static} (InsertResult) CreateErroResult(IResultError[], InsertResultData): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed errors collection.
        /// </returns>
        public static InsertResult CreateErroResult(IResultError[] errors, InsertResultData result) =>
            new()
            {
                Result = result,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #endregion

        #region public new static methods 

        #region [public] {new} {static} (InsertResult) CreateSuccessResult(InsertResultData): Returns a new success result
        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="InsertResult"/>.
        /// </returns>
        public new static InsertResult CreateSuccessResult(InsertResultData result) =>
            new()
            {
                Result = result,
                Success = true,
                Errors = new List<IResultError>()
            };
        #endregion

        #region [public] {new} {static} (InsertResult) FromException(Exception): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="InsertResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <returns>
        /// A new <see cref="InsertResult"/> instance for specified exception.
        /// </returns>
        public new static InsertResult FromException(System.Exception exception) => FromException(exception, default);
        #endregion

        #region [public] {new} {static} (InsertResult) FromException(Exception, InsertResultData): Creates a new result instance from known exception
        /// <summary>
        /// Creates a new <see cref="InsertResult"/> instance from known exception.
        /// </summary>
        /// <param name="exception">Target exception.</param>
        /// <param name="result">Response Result</param>
        /// <returns>
        /// A new <see cref="InsertResult"/> instance for specified exception.
        /// </returns>
        public new static InsertResult FromException(System.Exception exception, InsertResultData result) =>
            new()
            {
                Result = result,
                Success = false,
                Errors = new List<IResultError> { new ResultExceptionError { Exception = exception } }
            };
        #endregion

        #endregion

        #region public methods 

        #region [public] (InsertResult) Insert(IInsert, bool = true): Try to insert an element in this input
        /// <summary>
        /// Try to insert an element in this input.
        /// </summary>
        /// <param name="data">Reference to insertable object information</param>
        /// <param name="canInsert">Determines if can insert. Default is <b>true</b>.</param>
        /// <returns>
        /// Operation result.
        /// </returns>
        public InsertResult Insert(IInsert data, bool canInsert = true)
        {
            if (!canInsert)
            {
                return data == null
                    ? CreateErroResult("Missing data")
                    : CreateSuccessResult(new InsertResultData
                    {
                        Context = Result.Context,
                        InputStream = Result.OutputStream,
                        OutputStream = Result.OutputStream
                    });
            }

            InsertResult result = InsertImplStrategy(data, Result.Context);

            if (Result.Context.AutoUpdateChanges)
            {
                Result.Context.Input = result.Result.OutputStream;
            }

            return result;
        }
        #endregion

        #endregion

        #region private methods

        private InsertResult InsertImplStrategy(IInsert data, IInput context)
            => data == null ? InsertResult.CreateErroResult("Missing data") : data.Apply(Result.OutputStream, context);

        #endregion
    }
}
