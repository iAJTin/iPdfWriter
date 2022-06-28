
using System;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;
using iTin.Core.Helpers;
using iTin.Logging;

using NativeIO = System.IO;

namespace iTin.Core.IO
{
    /// <summary>
    /// Static class than contains extension methods for objects of type <see cref="T:System.IO.Stream" />.
    /// </summary> 
    public static class StreamExtensions
    {
        /// <summary>
        /// Saves this stream into a file with name specified by parameter <paramref name="fileName"/>.
        /// You can indicate whether to automatically create the destination path if it does not exist. By default it will try to create the destination path.
        /// The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.
        /// </summary>
        /// <param name="stream">Stream to save</param>
        /// <param name="fileName">Destination file path. Absolute or relative (~) paths are allowed</param>
        /// <param name="options">Output save options</param>
        /// <returns>
        /// A <see cref="IResult"/> object that contains the operation result
        /// </returns>
        public static IResult SaveToFile(this NativeIO.Stream stream, string fileName, SaveOptions options = null)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(StreamExtensions).Assembly.GetName().Name}, v{typeof(StreamExtensions).Assembly.GetName().Version}, Namespace: {typeof(StreamExtensions).Namespace}, Class: {nameof(StreamExtensions)}");
            Logger.Instance.Debug(" Saves this stream into a file with name specified by filename");
            Logger.Instance.Debug($" > Signature: ({typeof(IResult)}) SaveToFile(this {typeof(NativeIO.Stream)}, {typeof(string)})");

            SentinelHelper.ArgumentNull(stream, nameof(stream));
            Logger.Instance.Debug($"   > stream: {stream.Length} bytes");

            SentinelHelper.ArgumentNull(fileName, nameof(fileName));
            Logger.Instance.Debug($"   > fileName: {fileName}");

            try
            {
                IResult saveResult;
                using (var memoryStream = stream as NativeIO.MemoryStream ?? stream.ToMemoryStream())
                {
                    Logger.Instance.Debug(" > Output: Success: True");

                    saveResult = memoryStream.SaveToFile(fileName, options);
                }

                return saveResult;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error while save stream to file", ex);
                Logger.Instance.Info("  > Output: Success: False");

                return BooleanResult.FromException(ex);
            }
        }


        private static IResult SaveToFile(this NativeIO.MemoryStream stream, string fileName, SaveOptions options = null)
        {
            try
            {
                var safeOptions = options;
                if (options == null)
                {
                    safeOptions = SaveOptions.Default;
                }

                string parsedFullFilenamePath = Path.PathResolver(fileName);
                string directoryName = NativeIO.Path.GetDirectoryName(parsedFullFilenamePath);
                NativeIO.DirectoryInfo di = new NativeIO.DirectoryInfo(directoryName);
                bool existDirectory = di.Exists;
                if (!existDirectory)
                {
                    if (safeOptions.CreateFolderIfNotExist)
                    {
                        NativeIO.Directory.CreateDirectory(directoryName);
                    }
                }

                using (var fs = new NativeIO.FileStream(parsedFullFilenamePath, NativeIO.FileMode.Create, NativeIO.FileAccess.ReadWrite, NativeIO.FileShare.ReadWrite))
                {
                    stream.WriteTo(fs);
                }

                return BooleanResult.SuccessResult;
            }
            catch(Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }
    }
}
