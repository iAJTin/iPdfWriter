
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
        #region public new static methods 

        #region [public] {new} {static} (SetResult) CreateErroResult(string, string = ""): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed error.
        /// </returns>
        public new static SetResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });
        #endregion

        #region [public] {new} {static} (SetResult) CreateErroResult(IResultError[]): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed errors collection.
        /// </returns>
        public new static SetResult CreateErroResult(IResultError[] errors) =>
            new()
            {
                Result = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {new} {static} (SetResult) CreateErroResult(string, SetResultData, string = null): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="Result">Response Result</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed errors collection.
        /// </returns>
        public new static SetResult CreateErroResult(string message, SetResultData Result, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } }, Result);
        #endregion

        #region [public] {new} {static} (SetResult) CreateErroResult(IResultError[], SetResultData): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="SetResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new invalid <see cref="SetResult"/> with specified detailed errors collection.
        /// </returns>
        public new static SetResult CreateErroResult(IResultError[] errors, SetResultData Result) =>
            new()
            {
                Result = Result,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {new} {static} (SetResult) CreateSuccessResult(SetResultData): Returns a new success result
        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new valid <see cref="InsertResult"/>.
        /// </returns>
        public new static SetResult CreateSuccessResult(SetResultData Result) =>
            new()
            {
                Result = Result,
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
        /// <param name="Result">Response Result</param>
        /// <returns>
        /// A new <see cref="SetResult"/> instance for specified exception.
        /// </returns>
        public new static SetResult FromException(System.Exception exception, SetResultData Result) =>
            new()
            {
                Result = Result,
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
                    ? InsertResult.CreateErroResult("Missing data")
                    : InsertResult.CreateSuccessResult(new InsertResultData
                    {
                        Context = Result.Context,
                        InputStream = Result.OutputStream,
                        OutputStream = Result.OutputStream
                    });
            }

            InsertResult result = InsertImplStrategy(data, this.Result.Context);

            if (Result.Context.AutoUpdateChanges)
            {
                Result.Context.Input = result.Result.OutputStream;
            }

            return result;
        }
        #endregion

        #region [public] (SetResult) Set(ISet, bool = true): Try to set an element in this input
        /// <summary>
        /// Try to set an element in this input.
        /// </summary>
        /// <param name="data">Reference to replaceable object information</param>
        /// <param name="canSet">Determines if can set. Default is <b>true</b>.</param>
        /// <returns>
        /// Operation result.
        /// </returns>
        public SetResult Set(ISet data, bool canSet = true)
        {
            if (!canSet)
            {
                return data == null
                    ? CreateErroResult("Missing data")
                    : CreateSuccessResult(new SetResultData
                    {
                        Context = Result.Context,
                        InputStream = Result.OutputStream,
                        OutputStream = Result.OutputStream
                    });
            }

            SetResult result = SetImplStrategy(data, Result.Context);

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

        private SetResult SetImplStrategy(ISet data, IInput context)
            => data == null ? SetResult.CreateErroResult("Missing data") : data.Apply(Result.OutputStream, context);

        #endregion
    }
}
