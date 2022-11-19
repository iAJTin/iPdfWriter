
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

using iTin.Core.Hardware.Abstractions.Devices.Graphics;

using Linux = iTin.Core.Hardware.Linux.Devices.Graphics.Font;
using MacOS = iTin.Core.Hardware.MacOS.Devices.Graphics.Font;
using Windows = iTin.Core.Hardware.Windows.Devices.Graphics.Font;

namespace iTin.Hardware.Abstractions.Devices.Graphics.Font
{
    /// <summary>
    /// Define 
    /// </summary>
    public class FontOperations
    {
        #region private readonly members
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IFontOperations _operations;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Dictionary<OSPlatform, IFontOperations> _operationsTable =
            new Dictionary<OSPlatform, IFontOperations>
            {
                { OSPlatform.Windows, new Windows.FontOperations() },
                { OSPlatform.Linux, new Linux.FontOperations() },
                { OSPlatform.OSX, new MacOS.FontOperations() }
            };
        #endregion

        #region constructor/s

        #region [private] FontOperations(): Prevents a default instance of this class
        /// <summary>
        /// Prevents a default instance of the <see cref="FontOperations"/> class from being created.
        /// </summary>
        private FontOperations()
        {
            _operations = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? _operationsTable[OSPlatform.Windows]
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? _operationsTable[OSPlatform.OSX]
                    : _operationsTable[OSPlatform.Linux];
        }
        #endregion

        #endregion

        #region public static readonly properties

        #region [public] {static} (FontOperations) Instance: Gets an unique instance of this class
        /// <summary>
        /// Gets an unique instance of this class.
        /// </summary>
        /// <value>
        /// A <see cref="FontOperations"/> reference that contains <b>SMBIOS</b> operations.
        /// </value>
        public static FontOperations Instance { get; } = new FontOperations();
        #endregion

        #endregion

        #region public methods

        #region [public] (int) AddFontResource(ISmbiosConnectOptions = null): Try to register a font from file
        /// <summary>
        /// Try to register a <strong>Font</strong> from file.
        /// </summary>
        /// <param name="fullPath">Full path to font file</param>
        /// <returns>
        /// A 
        /// </returns>
        public int AddFontResource(string fullPath) => _operations.AddFontResource(fullPath);
        #endregion

        #endregion
    }
}
