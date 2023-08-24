
using System.Runtime.InteropServices;

using iTin.Core.Interop.Shared.Windows;

namespace iTin.Core.Interop.Windows.Development.Graphics.Legacy.Gdi.Font;

/// <summary>
/// Functions that can be used to handle font.
/// </summary>
public static class NativeMethods
{
    /// <summary>
    /// Adds the font resource from the specified file to the system font table. The font can subsequently be used for text output by any application.
    /// For more information, please see https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-addfontresourcew.
    /// </summary>
    [DllImport(ExternDll.Gdi32, EntryPoint="AddFontResourceW", SetLastError = true)]
    public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);
}
