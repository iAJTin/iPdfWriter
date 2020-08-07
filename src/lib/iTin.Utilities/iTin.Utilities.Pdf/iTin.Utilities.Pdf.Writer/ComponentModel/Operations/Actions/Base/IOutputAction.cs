
namespace iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action
{
    using iTin.Core.ComponentModel;

    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output;

    /// <summary>
    /// Defines allowed actions for output result.
    /// </summary>
    public interface IOutputAction
    {
        /// <summary>
        /// Execute action for specified output result.
        /// </summary>
        /// <param name="data">Target output result data.</param>
        /// <returns>
        /// A <see cref="IResult"/> reference that contains action result.
        /// </returns>
        IResult Execute(OutputResultData data);
    }
}
