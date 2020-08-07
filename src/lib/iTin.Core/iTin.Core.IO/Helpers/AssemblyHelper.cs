
namespace iTin.Core.IO
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Static class which contains methods for retrieve <see cref="Assembly"/> information.
    /// </summary>
    internal static class AssemblyHelper
    {
        /// <summary>
        /// Returns <see cref="Uri"/> that contains full path to current assembly.
        /// </summary>
        /// <returns>
        /// A <see cref="Uri"/> that contains full path to current assembly.
        /// </returns>
        public static Uri GetFullAssemblyUri() => new Uri(Assembly.GetCallingAssembly().CodeBase);
    }
}
