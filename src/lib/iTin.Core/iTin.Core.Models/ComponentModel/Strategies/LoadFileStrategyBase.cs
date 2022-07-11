
using System;

using iTin.Core.Models.ComponentModel.Strategies.Result;

namespace iTin.Core.Models.ComponentModel
{
    /// <summary>
    /// Base class for load file strategies.<br/>
    /// Implements functionality to load a style file.
    /// </summary>
    internal abstract class LoadFileStrategyBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual LoadFileResult TryLoadFile(string filename, KnownFileFormat format)
        {
            throw new NotImplementedException();
        }
    }
}
