
using iTin.Core.Hardware.Abstractions.Devices.Graphics;
using iTin.Core.Interop.Windows.Development.Graphics.Legacy.Gdi.Font;

namespace iTin.Core.Hardware.Windows.Devices.Graphics.Font
{
    /// <summary>
    /// Specialization of the <see cref="IFontOperations"/> interface that contains the <strong>Font</strong> operations for <b>Windows</b> system.
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
        public int AddFontResource(string fullPath) => NativeMethods.AddFontResource(fullPath);
    }
}
