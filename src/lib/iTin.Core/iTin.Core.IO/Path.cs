
namespace iTin.Core.IO
{
    using System;
    using System.Reflection;

    using iTin.Registry.Windows;

    using NativePath = System.IO.Path;

    /// <summary>
    /// Helper class for works for path's.
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// Returns a <see cref="char"/> that represents directory separator char. Returns <b>'/'</b> in <b>Unix</b> system or <b>'\'</b> in <b>Windows</b> system.
        /// </summary>
        public static readonly char DirectorySeparatorChar = PlatformInformation.IsUnix ? '/' : '\\';

        /// <summary>
        /// Returns a <see cref="char"/> that represents alternative directory separator char. Always returns <b>'/'</b> char.
        /// </summary>
        public const char AltDirectorySeparatorChar = '/';

        /// <summary>
        /// Returns a <see cref="string"/> that represents parent relative directory. Always returns <b>'..'</b> string.
        /// </summary>
        public const string ParentRelativeDirectory = "..";

        /// <summary>
        /// Returns a <see cref="string"/> that represents this directory. Always returns <b>'.'</b> string.
        /// </summary>
        public const string ThisDirectory = ".";

        /// <summary>
        /// Returns a <see cref="string"/> that represents directory separator char. Returns <b>'/'</b> in <b>Unix</b> system or <b>'\'</b> in <b>Windows</b> system.
        /// </summary>
        public static readonly string DirectorySeparatorStr = new string(DirectorySeparatorChar, 1);

        /// <summary>
        /// Returns a <see cref="char"/> that represents volume separator char. Always returns <b>':'</b> char.
        /// </summary>
        public const char VolumeSeparatorChar = ':';


        internal static bool IsUnixLikePlatform => PlatformInformation.IsUnix;

        /// <summary>
        /// True if the character is the platform directory separator character or the alternate directory separator.
        /// </summary>
        public static bool IsDirectorySeparator(char c) => c == DirectorySeparatorChar || c == AltDirectorySeparatorChar;

        /// <summary>
        /// True if the character is any recognized directory separator character.
        /// </summary>
        public static bool IsAnyDirectorySeparator(char c) => c == '\\' || c == '/';

        /// <summary>
        /// Returns local path.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>
        /// Local path
        /// </returns>
        public static string GetLocalPath(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                return string.Empty;
            }

            var lUri = new Uri(uri);

            return string.Concat(lUri.LocalPath, lUri.Fragment);
        }

        /// <summary>
        /// Ensures a trailing directory separator character
        /// </summary>
        public static string EnsureTrailingSeparator(string s)
        {
            if (s.Length == 0 || IsAnyDirectorySeparator(s[s.Length - 1]))
            {
                return s;
            }

            // Use the existing slashes in the path, if they're consistent
            bool hasSlash = s.IndexOf('/') >= 0;
            bool hasBackslash = s.IndexOf('\\') >= 0;
            if (hasSlash && !hasBackslash)
            {
                return $"{s}/";
            }

            if (!hasSlash && hasBackslash)
            {
                return $"{s}\\";
            }

            // If there are no slashes or they are inconsistent, use the current platform's slash.
            return s + DirectorySeparatorChar;
        }


        /// <summary>
        /// Removes trailing directory separator characters
        /// </summary>
        /// <remarks>
        /// This will trim the root directory separator:
        /// "C:\" maps to "C:", and "/" maps to ""
        /// </remarks>
        public static string TrimTrailingSeparators(string s)
        {
            int lastSeparator = s.Length;
            while (lastSeparator > 0 && IsDirectorySeparator(s[lastSeparator - 1]))
            {
                lastSeparator -= 1;
            }

            if (lastSeparator != s.Length)
            {
                s = s.Substring(0, lastSeparator);
            }

            return s;
        }

        /// <summary>
        /// True if the path is an absolute path (rooted to drive or network share)
        /// </summary>
        public static bool IsAbsolute(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (IsUnixLikePlatform)
            {
                return path[0] == DirectorySeparatorChar;
            }

            // "C:\"
            if (IsDriveRootedAbsolutePath(path))
            {
                // Including invalid paths (e.g. "*:\")
                return true;
            }

            // "\\machine\share"
            // Including invalid/incomplete UNC paths (e.g. "\\goo")
            return path.Length >= 2 &&
                   IsDirectorySeparator(path[0]) &&
                   IsDirectorySeparator(path[1]);
        }

        /// <summary>
        /// True if the child path is a child of the parent path.
        /// </summary>
        public static bool IsChildPath(string parentPath, string childPath) =>
            parentPath.Length > 0 && 
            childPath.Length > parentPath.Length && 
            PathsEqual(childPath, parentPath, parentPath.Length) && 
            (IsDirectorySeparator(parentPath[parentPath.Length - 1]) || IsDirectorySeparator(childPath[parentPath.Length]));

        /// <summary>
        /// True if the two paths are the same.
        /// </summary>
        public static bool PathsEqual(string path1, string path2) => PathsEqual(path1, path2, Math.Max(path1.Length, path2.Length));


        /// <summary>
        /// Returns true if given path is absolute and starts with a drive specification ("C:\").
        /// </summary>
        private static bool IsDriveRootedAbsolutePath(string path) => path.Length >= 3 && path[1] == VolumeSeparatorChar && IsDirectorySeparator(path[2]);

        /// <summary>
        /// True if the two paths are the same.  (but only up to the specified length)
        /// </summary>
        private static bool PathsEqual(string path1, string path2, int length)
        {
            if (path1.Length < length || path2.Length < length)
            {
                return false;
            }

            for (int i = 0; i < length; i++)
            {
                if (!PathCharEqual(path1[i], path2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private static bool PathCharEqual(char x, char y)
        {
            if (IsDirectorySeparator(x) && IsDirectorySeparator(y))
            {
                return true;
            }

            return IsUnixLikePlatform
                ? x == y
                : char.ToUpperInvariant(x) == char.ToUpperInvariant(y);
        }

        /// <summary>
        /// Gets a valid full path from a relative path, includes a paths on network drives
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>
        /// Valid full path.
        /// </returns>
        public static string PathResolver(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            return
                PathResolverImpl(
                    path.Contains(":")
                        ? UncPathResolver(path)
                        : path);
        }

        /// <summary>
        /// Resolves a mapped network drive into valid <b>UNC</b> path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>
        /// <b>UNC</b> path.
        /// </returns>
        public static string UncPathResolver(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return path;
            }

            if (IsNetworkDrive(path))
            {
                return $"{RegistryOperations.GetCurrentUserKeyValue<string>($@"Network\\{path.ToUpperInvariant()[0]}", "RemotePath")}{path.Remove(0, 2)}";
            }

            return path;
        }

        /// <summary>
        /// Checks if the given path is a network drive.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>
        /// <b>true</b> if is a network drive; otherwise <b>false</b>.
        /// </returns>
        public static bool IsNetworkDrive(string path) => !string.IsNullOrEmpty(path) && RegistryOperations.CheckCurrentUserKey($@"Network\\{path.ToUpperInvariant()[0]}");



        /// <summary>
        /// Gets a valid full path from a relative path.
        /// </summary>
        /// <param name="relativePath">Element to recover.</param>
        /// <returns>
        /// Valid full path.
        /// </returns>
        /// <exception cref="ArgumentNullException">The value specified is outside the range of valid values.</exception>
        private static string PathResolverImpl(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentNullException(nameof(relativePath));
            }

            var relativePathNormalized = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            var isRelativePath = relativePathNormalized.Trim().StartsWith("~", StringComparison.Ordinal);
            if (!isRelativePath)
            {
                return relativePath;
            }

            if (relativePathNormalized.Length.Equals(1))
            {
                relativePathNormalized = relativePathNormalized.Insert(1, Path.DirectorySeparatorChar.ToString());
            }
            else if (!relativePathNormalized[1].Equals(Path.DirectorySeparatorChar))
            {
                relativePathNormalized = relativePathNormalized.Insert(1, Path.DirectorySeparatorChar.ToString());
            }

            var callingUri = AssemblyHelper.GetFullAssemblyUri();
            var candidateUri = new UriBuilder(callingUri);
            var unscapedCandidateUri = Uri.UnescapeDataString(candidateUri.Path);
            var candidateRootPath = NativePath.GetDirectoryName(unscapedCandidateUri);

            var rootPattern = $"~{DirectorySeparatorChar}";
            
#if NETSTANDARD2_1 || NET5_0_OR_GREATER
            var outputPartialPath = ReadOnlySpan<char>.Empty;
            if (!relativePathNormalized.Equals(rootPattern))
            {
                outputPartialPath = relativePathNormalized
                    .AsSpan()[(relativePathNormalized.IndexOf('~') + 1)..]
                    .TrimStart(DirectorySeparatorChar);
            }
#else
            var outputPartialPath = string.Empty;
            if (!relativePathNormalized.Equals(rootPattern))
            {
                outputPartialPath = relativePathNormalized.Split(new[] { rootPattern }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
#endif

            var targetAssembly = Assembly.GetEntryAssembly();
            if (targetAssembly == null)
            {
                targetAssembly = Assembly.GetCallingAssembly();
            }

            var netFrameworkVersion = NetFrameworkHelper.GetAssemblyFrameworkVersion(targetAssembly);

            var rootPath = candidateRootPath.ToUpperInvariant()
                .Replace("BIN", string.Empty)
                .Replace("RELEASE", string.Empty)
                .Replace($"{DirectorySeparatorChar}DEBUG", string.Empty)
                .Replace($"{DirectorySeparatorChar}NET{netFrameworkVersion.VersionNumber}", string.Empty);

            var runtimeRootPath = rootPath;
            var hasRuntimeOutputFolder = !string.IsNullOrEmpty(netFrameworkVersion.RuntimeOutputFolder());
            if (hasRuntimeOutputFolder)
            {
                runtimeRootPath = rootPath.Replace($"{DirectorySeparatorChar}{netFrameworkVersion.RuntimeOutputFolder().ToUpperInvariant()}", string.Empty);
            }

#if NETSTANDARD2_1 || NET5_0_OR_GREATER
            return NativePath.Combine(runtimeRootPath, outputPartialPath.ToString());
#else
            return NativePath.Combine(runtimeRootPath, outputPartialPath);
#endif
        }
    }
}
