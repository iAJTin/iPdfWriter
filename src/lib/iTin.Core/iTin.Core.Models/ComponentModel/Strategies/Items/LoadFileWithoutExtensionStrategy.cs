
using System.IO;

using iTin.Core.Models.ComponentModel.Strategies.Result;

using iTinPath = iTin.Core.IO.Path;

namespace iTin.Core.Models.ComponentModel.Strategies
{
    /// <summary>
    /// A Specialization of <see cref="LoadFileStrategyBase"/> class.<br/>
    /// Which try to load a style file without extension defined.
    /// </summary>
    internal class LoadFileWithoutExtensionStrategy : LoadFileStrategyBase
    {
        /// <summary>
        /// Try to load a style file without extension defined.
        /// </summary>
        /// <param name="fileName">Target path</param>
        /// <param name="format">Format file</param>
        /// <returns>
        /// <para>
        /// A <see cref="LoadFileResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="LoadFileResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public override LoadFileResult TryLoadFile(string fileName, KnownFileFormat format)
        {
            var parsedFilenamePath = iTinPath.PathResolver(fileName);
            var fileInfo = new FileInfo(parsedFilenamePath);
            
            return fileInfo.Exists
                ? LoadFileResult.CreateSuccessResult(
                    new LoadFileResultData
                    {
                        Stream = new FileStream(parsedFilenamePath, FileMode.Open, FileAccess.Read)
                    })
                : LoadFileResult.CreateErrorResult("File not found");
        }
    }
}
