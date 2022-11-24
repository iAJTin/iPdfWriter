﻿
using iTin.Core.ComponentModel;

using iTin.Utilities.Abstractions.Writer.Operations.Results;

namespace iTin.Utilities.Abstractions.Writer.Operations.Actions
{
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
        IResult Execute(IOutputResultData data);
    }
}
