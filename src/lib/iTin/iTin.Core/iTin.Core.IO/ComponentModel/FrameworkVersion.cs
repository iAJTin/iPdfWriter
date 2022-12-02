
using System;
using System.Runtime.Versioning;

namespace iTin.Core.IO
{
    /// <summary>
    /// This class allows to obtain the .net framework folder for a specific version.
    /// </summary>
    internal class FrameworkVersion
    {
        #region constructor

        /// <summary>
        /// Initialize a new instance of the <see cref="T:iTin.Core.Drawing.Clipping" /> class.
        /// </summary>
        /// <param name="frameworkAttribute">Framework compiled information</param>
        internal FrameworkVersion(TargetFrameworkAttribute frameworkAttribute)
        {
            var items = frameworkAttribute.FrameworkName.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            VersionName = items[0];

            var frameworkVersionItems = items[1].Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
            VersionNumber = frameworkVersionItems[1].Replace("v", string.Empty);
        }

        #endregion

        #region public properties

        /// <summary>
        /// Gets a framework version name.
        /// </summary>
        /// <value>
        /// Version name.
        /// </value>
        public string VersionName { get; }

        /// <summary>
        /// Gets a framework version number.
        /// </summary>
        /// <value>
        /// Version number.
        /// </value>
        public string VersionNumber { get; }

        #endregion

        #region public methods

        /// <summary>
        /// Returns runtime output folder for this version name and number.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that contains runtime output folder.
        /// </returns>
        public string RuntimeOutputFolder()
        {
            var isNetFramework = VersionName.Contains("NETFramework");
            if (isNetFramework)
            {
                return $"net{VersionNumber.Replace(".", string.Empty)}";
            }

            var isNetStandard = VersionName.Contains("NETStandard");
            if (isNetStandard)
            {
                return $"netstandard{VersionNumber}";
            }

            var isNetCore = VersionName.Contains("NETCore");
            if (isNetCore)
            {
                return float.Parse(VersionNumber) >= 60f
                    ? $"net{VersionNumber}" 
                    : $"netcoreapp{VersionNumber}";
            }

            var isUap = VersionName.Contains("UniversalWindowsPlatform");
            if (isUap)
            {
                return $"uap{VersionNumber}";
            }

            var isWindowsPhone = VersionName.Contains("WindowsPhone");
            if (isWindowsPhone)
            {
                return $"wp{VersionNumber.Replace(".", string.Empty)}";
            }

            var isWindowsStore = VersionName.Contains("WindowsStore");
            if (isWindowsStore)
            {
                return $"netcore{VersionNumber.Replace(".", string.Empty)}";
            }

            var isMicroFramework = VersionName.Contains("NETMicroFramework");
            if (isMicroFramework)
            {
                return $"netmf{VersionNumber.Replace(".", string.Empty)}";
            }

            var isSilverlight = VersionName.Contains("Silverlight");
            if (isSilverlight)
            {
                return $"sl{VersionNumber.Replace(".", string.Empty)}";
            }

            return string.Empty;
        }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> that represents the current object.
        /// </returns>
        public override string ToString() => 
            $"VersionName = \"{VersionName}\", VersionNumber = {VersionNumber}";

        #endregion
    }
}
