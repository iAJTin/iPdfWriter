
using System;
using System.Threading;
using System.Threading.Tasks;

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
                using var memoryStream = stream as NativeIO.MemoryStream ?? stream.ToMemoryStream();
                Logger.Instance.Debug(" > Output: Success: True");

                return memoryStream.SaveToFileImpl(fileName, options);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error while save stream to file", ex);
                Logger.Instance.Info("  > Output: Success: False");

                return BooleanResult.FromException(ex);
            }
        }

        /// <summary>
        /// Saves this stream into a file with name specified by parameter <paramref name="fileName"/> asynchronously.
        /// You can indicate whether to automatically create the destination path if it does not exist. By default it will try to create the destination path.
        /// The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.
        /// </summary>
        /// <param name="stream">Stream to save</param>
        /// <param name="fileName">Destination file path. Absolute or relative (~) paths are allowed</param>
        /// <param name="options">Output save options</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="IResult"/> object that contains the operation result
        /// </returns>
        public static async Task<IResult> SaveToFileAsync(this NativeIO.Stream stream, string fileName, SaveOptions options = null, CancellationToken cancellationToken = default)
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
                using var memoryStream = stream as NativeIO.MemoryStream ?? await stream.ToMemoryStreamAsync(cancellationToken);
                Logger.Instance.Debug(" > Output: Success: True");

                return await memoryStream.SaveToFileImplAsync(fileName, options);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Error while save stream to file", ex);
                Logger.Instance.Info("  > Output: Success: False");

                return BooleanResult.FromException(ex);
            }
        }


        private static IResult SaveToFileImpl(this NativeIO.MemoryStream stream, string fileName, SaveOptions options = null)
        {
            try
            {
                var safeOptions = options;
                if (options == null)
                {
                    safeOptions = SaveOptions.Default;
                }

                var parsedFullFilenamePath = Path.PathResolver(fileName);
                var directoryName = NativeIO.Path.GetDirectoryName(parsedFullFilenamePath);
                var di = new NativeIO.DirectoryInfo(directoryName!);
                var existDirectory = di.Exists;
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


        private static async Task<IResult> SaveToFileImplAsync(this NativeIO.MemoryStream stream, string fileName, SaveOptions options = null)
        {
            try
            {
                var safeOptions = options;
                if (options == null)
                {
                    safeOptions = SaveOptions.Default;
                }

                var parsedFullFilenamePath = Path.PathResolver(fileName);
                var directoryName = NativeIO.Path.GetDirectoryName(parsedFullFilenamePath);
                var di = new NativeIO.DirectoryInfo(directoryName!);
                var existDirectory = di.Exists;
                if (!existDirectory)
                {
                    if (safeOptions.CreateFolderIfNotExist)
                    {
                        NativeIO.Directory.CreateDirectory(directoryName);
                    }
                }

#if NETSTANDARD2_1 || NET5_0_OR_GREATER

                await using (var fs = new NativeIO.FileStream(parsedFullFilenamePath, NativeIO.FileMode.Create, NativeIO.FileAccess.ReadWrite, NativeIO.FileShare.ReadWrite))
                {
                    stream.WriteTo(fs);
                }
#else
                using (var fs = new NativeIO.FileStream(parsedFullFilenamePath, NativeIO.FileMode.Create, NativeIO.FileAccess.ReadWrite, NativeIO.FileShare.ReadWrite))
                {
                    stream.WriteTo(fs);
                }
#endif

                return await Task.FromResult(BooleanResult.SuccessResult);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BooleanResult.FromException(ex));
            }
        }
    }
}
