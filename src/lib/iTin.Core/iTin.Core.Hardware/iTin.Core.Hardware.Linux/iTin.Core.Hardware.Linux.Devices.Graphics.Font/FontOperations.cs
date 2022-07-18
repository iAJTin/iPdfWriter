
using iTin.Core.Hardware.Common.Devices.Graphics;

namespace iTin.Core.Hardware.Linux.Devices.Graphics.Font
{
    /// <summary>
    /// Specialization of the <see cref="IFontOperations"/> interface that contains the <strong>Font</strong> operations for <b>Linux</b> system.
    /// </summary>
    public class FontOperations : IFontOperations
    {
        /// <summary>
        /// Try to register a <strong>Font</strong> from file.
        /// </summary>
        /// <param name="fullPath">Full path to font file</param>
        /// <returns>
        /// A 
        /// </returns>
        public int AddFontResource(string fullPath)
        {
            return -1;
        }
    }
}
