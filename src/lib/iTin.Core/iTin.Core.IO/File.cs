
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using iTin.Core.Helpers;
using iTin.Logging;

using NativeIO = System.IO;
using NativeFile = System.IO.File;
using NativePath = System.IO.Path;
using NativeDirectory = System.IO.Directory;

namespace iTin.Core.IO
{
    /// <summary>
    /// 
    /// </summary>
    public static class File
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string TempDirectoryName = "iTin";

        #endregion

        #region public static properties

        /// <summary>
        /// Gets the <b>iTin</b> temporary directory.
        /// </summary>
        /// <value>
        /// The <b>iTin</b> temporary directory.
        /// </value>
        public static string TempDirectoryFullName
        {
            get
            {
                var tempFullPath = NativePath.GetTempPath();
                var tempDirectory = NativePath.Combine(tempFullPath, TempDirectoryName);

                return tempDirectory;
            }
        }

        #endregion

        #region public static methods

        /// <summary>
        /// Clean or create temporary work directory.
        /// </summary>
        public static void CleanOrCreateTemporaryDirectory()
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Clean or create temporary work directory");
            Logger.Instance.Debug(" > Signature: (void) CleanOrCreateTemporaryDirectory()");

            var tempDirectory = TempDirectoryFullName;
            var existTempDirectory = NativeDirectory.Exists(tempDirectory);
            if (existTempDirectory)
            {
                try
                {
                    Array.ForEach(NativeDirectory.GetFiles(tempDirectory), NativeFile.Delete);
                    Logger.Instance.Debug("  > Temporary directory has been cleaned");
                }
                catch (NativeIO.IOException)
                {
                }
            }
            else
            {
                NativeDirectory.CreateDirectory(tempDirectory);
                Logger.Instance.Debug("  > Temporary directory has been created");
            }
        }

        /// <summary>
        /// Copies the files.
        /// </summary>
        /// <param name="sourceDirectory">Source directory.</param>
        /// <param name="targetDirectory">Target directory.</param>
        /// <param name="criterial">File criteria.</param>
        /// <param name="overrides">if is <strong>true</strong> overrides destination file.</param>
        public static void CopyFiles(string sourceDirectory, string targetDirectory, string criterial, bool overrides)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Copies specified files from source to target directory");
            Logger.Instance.Debug($" > Signature: (void) CopyFiles({typeof(string)}, {typeof(string)}, {typeof(string)}, {typeof(bool)})");

            SentinelHelper.IsTrue(string.IsNullOrEmpty(sourceDirectory));
            Logger.Instance.Debug($"   > sourceDirectory: {sourceDirectory}");

            SentinelHelper.IsTrue(string.IsNullOrEmpty(targetDirectory));
            Logger.Instance.Debug($"   > targetDirectory: {targetDirectory}");
            Logger.Instance.Debug($"   > criterial: {criterial}");
            Logger.Instance.Debug($"   > overrides: {overrides}");

            string[] items = NativeDirectory.GetFiles(sourceDirectory, criterial, NativeIO.SearchOption.TopDirectoryOnly);
            foreach (var item in items)
            {
                string filename = NativePath.GetFileName(item);
                string target = NativePath.Combine(targetDirectory, filename);

                NativeFile.Copy(item, target, overrides);
            }

            Logger.Instance.Debug($"  > Output: Has been copied correctly from {sourceDirectory} to {targetDirectory}");
        }

        /// <summary>
        /// Removes the directory and intermediates files.
        /// </summary>
        public static void DeleteTemporaryOutputFiles()
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Removes the directory and intermediates files");
            Logger.Instance.Debug(" > Signature: (void) DeleteTemporaryOutputFiles()");

            var tempDirectory = TempDirectoryFullName;

            var existTempDirectory = NativeDirectory.Exists(tempDirectory);
            if (!existTempDirectory)
            {
                Logger.Instance.Debug("  > Output: Nothing to do, temp directory not exist");
                return;
            }

            try
            {
                Array.ForEach(NativeDirectory.GetFiles(tempDirectory), NativeFile.Delete);
                NativeDirectory.Delete(tempDirectory);
                Logger.Instance.Debug("  > Output: Temp directory has been deleted correctly");
            }
            catch (NativeIO.IOException)
            {
            }
        }

        /// <summary>
        /// Returns a collection that contains all files in a folder with the specified criterial.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="criterial">The criterial.</param>
        /// <param name="timeThreshold">The time threshold.</param>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerable{string}"/> that contains all files in a folder with the specified criterial.
        /// </returns>
        public static IEnumerable<string> GetFiles(string folder, string criterial, int timeThreshold = 0)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Returns a collection that contains all files in a folder with the specified criterial");
            Logger.Instance.Debug($" > Signature: ({typeof(IEnumerable<string>)}) GetFiles({typeof(string)}, {typeof(string)}, {typeof(int)})");
            Logger.Instance.Debug($"   > folder: {folder}");
            Logger.Instance.Debug($"   > criterial: {criterial}");
            Logger.Instance.Debug($"   > timeThreshold: {timeThreshold}");

            var files = new Collection<string>();

            Array.ForEach(
                NativeDirectory.GetFiles(folder, criterial, NativeIO.SearchOption.TopDirectoryOnly),
                file =>
                {
                    var currentTime = DateTime.Now;
                    var lastWriteTime = NativeFile.GetLastWriteTime(file);
                    var diff = currentTime.Subtract(lastWriteTime).TotalSeconds;
                    if (diff >= timeThreshold)
                    {
                        files.Add(NativePath.GetFileName(file));
                    }
                });

            Logger.Instance.Debug($"  > Output: {files.Count} files [{files[0]} ...]");

            return files;
        }

        /// <summary>
        /// Returns a temp <see cref="T:System.Uri"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Uri" /> file.
        /// </returns>
        public static Uri GetUniqueTempRandomFile()
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug($" Returns a temp {typeof(Uri)}");
            Logger.Instance.Debug($" > Signature: ({typeof(Uri)}) GetUniqueTempRandomFile()");

            var tempPath = NativePath.GetTempPath();
            var randomFileName = NativePath.GetRandomFileName();
            var path = NativePath.Combine(tempPath, randomFileName);

            Logger.Instance.Debug($"  > Output: {path}");

            return new Uri(path);
        }

        /// <summary>
        /// Determines whether <paramref name="name"/> is a valid name for a file.
        /// </summary>
        /// <param name="name">Filename to check.</param>
        /// <returns>
        /// <b>true</b> if <paramref name="name"/> is a valid file name; otherwise, <b>false</b>.
        /// </returns>
        public static bool IsValidFileName(string name)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Determines whether specified filename is a valid name for a file");
            Logger.Instance.Debug($" > Signature: ({typeof(bool)}) IsValidFileName({typeof(string)})");
            Logger.Instance.Debug($"   > name: {name}");

            var result = NativePath.GetInvalidFileNameChars().All(c => !name.Contains(c.ToString(CultureInfo.InvariantCulture)));
            Logger.Instance.Debug($"  > Output: {result}");

            return result;
        }

        /// <summary>
        /// Returns a collection that contains all files without extension in a folder with the specified criterial.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="criterial">The criterial.</param>
        /// <param name="timeThreshold">The time threshold.</param>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerable{string}" /> that contains all files in a folder with the specified criterial without extension.
        /// </returns>
        public static IEnumerable<string> GetFilesWithoutExtension(string folder, string criterial, int timeThreshold)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Returns a collection that contains all files without extension in a folder with the specified criterial");
            Logger.Instance.Debug($" > Signature: ({typeof(IEnumerable<string>)}) GetFilesWithoutExtension({typeof(string)}, {typeof(string)}, {typeof(int)})");
            Logger.Instance.Debug($"   > folder: {folder}");
            Logger.Instance.Debug($"   > criterial: {criterial}");
            Logger.Instance.Debug($"   > timeThreshold: {timeThreshold}");

            var files = new Collection<string>();

            Array.ForEach(
                NativeDirectory.GetFiles(folder, criterial, NativeIO.SearchOption.TopDirectoryOnly),
                file =>
                {
                    var currentTime = DateTime.Now;
                    var lastWriteTime = NativeFile.GetLastWriteTime(file);
                    var diff = currentTime.Subtract(lastWriteTime).TotalSeconds;
                    if (diff >= timeThreshold)
                    {
                        files.Add(NativePath.GetFileNameWithoutExtension(file));
                    }
                });

            Logger.Instance.Debug($"  > Output: {files.Count} files [{files[0]} ...]");

            return files;
        }

        /// <summary>
        /// Gets a value indicating whether the specified <b><paramref name="path" /></b> is a web address.
        /// </summary>
        /// <param name="path">Path to check.</param>
        /// <returns>
        /// Returns <b>true</b> if is a web address; otherwise <b>false</b>.
        /// </returns>
        public static bool IsUrl(string path)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug(" Gets a value indicating whether the specified path is a web address.");
            Logger.Instance.Debug($" > Signature: ({typeof(bool)}) IsUrl({typeof(string)})");
            Logger.Instance.Debug($"   > path: {path}");

            if (string.IsNullOrEmpty(path))
            {
                Logger.Instance.Debug($"  > Output: false");

                return false;
            }

            var result = path.IndexOf("://", StringComparison.Ordinal) > 0;
            Logger.Instance.Debug($"  > Output: {result}");

            return result;
        }

        /// <summary>
        /// This method makes a valid URL from a given filename.
        /// </summary>
        /// <param name="filename">A given filename</param>
        /// <returns>
        /// A valid <b>URL</b>.
        /// </returns>
        public static Uri ToUri(string filename)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(File).Assembly.GetName().Name}, v{typeof(File).Assembly.GetName().Version}, Namespace: {typeof(File).Namespace}, Class: {nameof(File)}");
            Logger.Instance.Debug($" This method makes a valid URL from a given filename");
            Logger.Instance.Debug($" > Signature: ({typeof(Uri)}) ToUrl({typeof(string)})");
            Logger.Instance.Debug($"   > filename: {filename}");

            Uri result;
            try
            {
                result = new Uri(filename);
                Logger.Instance.Debug($"  > Output: {result}");

                return result;
            }
            catch
            {
                result = new Uri(NativePath.GetFullPath(filename));
                Logger.Instance.Debug($"  > Output: {result}");

                return result;
            }
        }

        #endregion
    }
}
