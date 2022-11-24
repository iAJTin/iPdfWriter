
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;

using iTin.Utilities.Abstractions.Writer.Operations.Results;

namespace iTin.Utilities.Abstractions.Writer.Operations.Actions
{
    /// <summary>
    /// Defines allowed actions for output result.
    /// </summary>
    public interface IOutputActionAsync
    {
        /// <summary>
        /// Execute action asynchronously for specified output result.
        /// </summary>
        /// <param name="data">Target output result data.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="IResult"/> reference that contains action result.
        /// </returns>
        Task<IResult> ExecuteAsync(IOutputResultData data, CancellationToken cancellationToken = default);
    }
}
