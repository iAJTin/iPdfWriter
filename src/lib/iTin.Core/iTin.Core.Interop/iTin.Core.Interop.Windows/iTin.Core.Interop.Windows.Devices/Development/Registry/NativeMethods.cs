
namespace iTin.Core.Interop.Shared.Windows.Development.Registry
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// Functions to develop Device and Driver Installation Reference.
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// Retrieves the data associated with the default or unnamed value of a specified registry key. The data must be a null-terminated
        /// <remarks>
        /// For more information, please see https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryvaluea.
        /// </remarks>
        /// </summary>
        [DllImport(ExternDll.Advapi32, SetLastError = true)]
        public static extern uint RegQueryValue(IntPtr keyClass, IntPtr subKey, StringBuilder classDescription, ref uint sizeB);

        /// <summary>
        /// Retrieves the type and data for the specified value name associated with an open registry key. To ensure that any string values (REG_SZ, REG_MULTI_SZ, and REG_EXPAND_SZ) returned are null-terminated, use the RegGetValue function.
        /// <remarks>
        /// For more information, please see https://docs.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regqueryvalueexa
        /// </remarks>
        /// </summary>
        [DllImport(ExternDll.Advapi32, CharSet = CharSet.Unicode)]
        public static extern uint RegQueryValueEx(IntPtr hKey, string lpValueName, IntPtr lpReserved, IntPtr lpType, byte[] lpData, ref uint lpcbData);
    }
}
