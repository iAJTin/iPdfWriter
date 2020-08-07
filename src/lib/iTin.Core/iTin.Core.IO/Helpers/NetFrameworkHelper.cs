
namespace iTin.Core.IO
{
    using System.Linq;
	using System.Reflection;
    using System.Runtime.Versioning;

	/// <summary>
	/// Static class which contains methods for retrieve <b>.NET Framework</b> information.
	/// </summary>
	internal static class NetFrameworkHelper
    {
        /// <summary>
        /// Returns <see cref="FrameworkVersion"/> that contains full path to current assembly.
        /// </summary>
        /// <returns>
        /// A <see cref="FrameworkVersion"/> that contains full path to current assembly.
        /// </returns>
        public static FrameworkVersion GetAssemblyFrameworkVersion(Assembly assembly)
        {
            var frameworkAttribute = (TargetFrameworkAttribute)assembly.GetCustomAttributes(typeof(TargetFrameworkAttribute), false).SingleOrDefault();

            return new FrameworkVersion(frameworkAttribute);
        }
    }
}
