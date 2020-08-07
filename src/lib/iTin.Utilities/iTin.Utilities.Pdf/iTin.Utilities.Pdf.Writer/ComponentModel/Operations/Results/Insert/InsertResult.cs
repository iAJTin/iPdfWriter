
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert
{
    using System.Collections.Generic;

    using iTin.Core.ComponentModel;

    using Replace;
    using Set;

    /// <summary>
    /// Specialization of <see cref="ResultBase{InsertResultData}"/> interface.<br/>
    /// Represents result after insert an element in <b>pdf</b> document.
    /// </summary>
    public class InsertResult : ResultBase<InsertResultData>
    {
        #region public new static methods 

        #region [public] {new} {static} (InsertResult) CreateErroResult(string, string = ""): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed error.
        /// </returns>
        public new static InsertResult CreateErroResult(string message, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } });
        #endregion

        #region [public] {new} {static} (InsertResult) CreateErroResult(IResultError[]): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed errors collection.
        /// </returns>
        public new static InsertResult CreateErroResult(IResultError[] errors) =>
            new InsertResult
            {
                Value = default,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {new} {static} (InsertResult) CreateErroResult(string, InsertResultData, string = null): Returns a new result with specified detailed error
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed error.
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="value">Response value</param>
        /// <param name="code">Error code</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed errors collection.
        /// </returns>
        public new static InsertResult CreateErroResult(string message, InsertResultData value, string code = "") => CreateErroResult(new IResultError[] { new ResultError { Code = code, Message = message } }, value);
        #endregion

        #region [public] {new} {static} (InsertResult) CreateErroResult(IResultError[], InsertResultData): Returns a new result with specified detailed errors collection
        /// <summary>
        /// Returns a new <see cref="InsertResult"/> with specified detailed errors collection.
        /// </summary>
        /// <param name="errors">A errors collection</param>
        /// <param name="value">Response value</param>
        /// <returns>
        /// A new invalid <see cref="InsertResult"/> with specified detailed errors collection.
        /// </returns>
        public new static InsertResult CreateErroResult(IResultError[] errors, InsertResultData value) =>
            new InsertResult
            {
                Value = value,
                Success = false,
                Errors = (IResultError[])errors.Clone()
            };
        #endregion

        #region [public] {new} {static} (InsertResult) CreateSuccessResult(InsertResultData): Returns a new success result
        /// <summary>
        /// Returns a new success result.
        /// </summary>
        /// <param name="value">Response value</param>
        /// <returns>
        /// A new valid <see cref="InsertResult"/>.
        /// </returns>
        public new static InsertResult CreateSuccessResult(InsertResultData value) =>
            new InsertResult
            {
                Value = value,
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
        /// <param name="value">Response value</param>
        /// <returns>
        /// A new <see cref="InsertResult"/> instance for specified exception.
        /// </returns>
        public new static InsertResult FromException(System.Exception exception, InsertResultData value) =>
            new InsertResult
            {
                Value = value,
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
                        Context = Value.Context,
                        InputStream = Value.OutputStream,
                        OutputStream = Value.OutputStream
                    });
            }

            InsertResult result = InsertImplStrategy(data, Value.Context);

            if (Value.Context.AutoUpdateChanges)
            {
                Value.Context.Input = result.Value.OutputStream;
            }

            return result;
        }
        #endregion

        #region [public] (ReplaceResult) Replace(IReplace, bool = true): Try to replace an element in this input
        /// <summary>
        /// Try to replace an element in this input.
        /// </summary>
        /// <param name="data">Reference to replaceable object information</param>
        /// <param name="canReplace">Determines if can replace. Default is <b>true</b>.</param>
        /// <returns>
        /// Operation result.
        /// </returns>
        public ReplaceResult Replace(IReplace data, bool canReplace = true)
        {
            if (!canReplace)
            {
                return data == null
                    ? ReplaceResult.CreateErroResult("Missing data")
                    : ReplaceResult.CreateSuccessResult(new ReplaceResultData
                    {
                        Context = Value.Context,
                        InputStream = Value.OutputStream,
                        OutputStream = Value.OutputStream
                    });
            }

            ReplaceResult result = ReplaceImplStrategy(data, Value.Context);

            if (Value.Context.AutoUpdateChanges)
            {
                Value.Context.Input = result.Value.OutputStream;
            }

            return result;
        }
        #endregion

        #region [public] (SetResult) Set(ISet, bool = true): Try to set an element in this input
        /// <summary>
        /// Try to set an element in this input.
        /// </summary>
        /// <param name="data">Reference to insertable object information</param>
        /// <param name="canSet">Determines if can set. Default is <b>true</b>.</param>
        /// <returns>
        /// Operation result.
        /// </returns>
        public SetResult Set(ISet data, bool canSet = true)
        {
            if (!canSet)
            {
                return data == null
                    ? SetResult.CreateErroResult("Missing data")
                    : SetResult.CreateSuccessResult(new SetResultData
                    {
                        Context = Value.Context,
                        InputStream = Value.OutputStream,
                        OutputStream = Value.OutputStream
                    });
            }

            SetResult result = SetImplStrategy(data, Value.Context);

            if (Value.Context.AutoUpdateChanges)
            {
                Value.Context.Input = result.Value.OutputStream;
            }

            return result;
        }
        #endregion

        #endregion

        #region private methods

        private InsertResult InsertImplStrategy(IInsert data, IInput context)
            => data == null ? InsertResult.CreateErroResult("Missing data") : data.Apply(Value.OutputStream, context);

        private ReplaceResult ReplaceImplStrategy(IReplace data, IInput context)
            => data == null ? ReplaceResult.CreateErroResult("Missing data") : data.Apply(Value.OutputStream, context);

        private SetResult SetImplStrategy(ISet data, IInput context)
            => data == null ? SetResult.CreateErroResult("Missing data") : data.Apply(Value.OutputStream, context);

        #endregion
    }
}
