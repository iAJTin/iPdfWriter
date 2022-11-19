
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Ionic.Zip;

using iTin.Core.ComponentModel;
using iTin.Core.Helpers;

using NativeIO = System.IO;

namespace iTin.Core.IO.Compression
{
    /// <summary>
    /// Static class than contains common extension methods for objects of the namespace <see cref="T:System.IO.Stream"/>.
    /// </summary>
    public static class StreamExtensions
    {
        #region private constants

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string ZipExtension = "zip";

        #endregion

        #region public static methods

        /// <summary>
        /// Returns a <see cref="T:System.IO.Stream" /> which contains specified stream compressed. If <paramref name="name"/> is <b>null</b> (<b> Nothing </b> in Visual Basic) a random name will be used.
        /// </summary>
        /// <param name="target">Target to compress.</param>
        /// <param name="name">Name of output <see cref="T:System.IO.Stream" />.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream" /> which contains this stream compressed.
        /// </returns>
        public static NativeIO.Stream AsZipStream(this NativeIO.Stream target, string name = null)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));

            string streamName;
            if (name == null)
            {
                var tempFilenameUri = File.GetUniqueTempRandomFile();
                var tempFilename = NativeIO.Path.GetFileName(tempFilenameUri.AbsolutePath);
                streamName = tempFilename;
            }
            else
            {
                streamName = name;
            }

            return new[] { target }.AsZipStream(new[] { streamName });
        }

        /// <summary>
        /// Returns a <see cref="T:System.IO.Stream"/> which contains specified streams list compressed. The <paramref name="itemNames"/> contains the names for every stream entry in streams list.
        /// </summary>
        /// <param name="items">Elements to compress.</param>
        /// <param name="itemNames">The names for every stream entry in streams list.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> than contains the path to created file.
        /// </returns>
        public static NativeIO.Stream AsZipStream(this IEnumerable<NativeIO.Stream> items, IEnumerable<string> itemNames)
        {
            var elementList = items as IList<NativeIO.Stream> ?? items.Clone().ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            var elementNamesList = itemNames as IList<string> ?? itemNames.ToList();
            SentinelHelper.ArgumentNull(elementNamesList, nameof(elementNamesList));

            SentinelHelper.IsTrue(elementList.Count != elementNamesList.Count, "The parameter 'itemsNames' must have the same number of elements as the 'items' list");

            try
            {
                NativeIO.Stream zippedStream = new NativeIO.MemoryStream();

                var tempDirectory = File.TempDirectoryFullName;
                var tempFilenameUri = File.GetUniqueTempRandomFile();
                var tempFilename = NativeIO.Path.GetFileName(tempFilenameUri.AbsolutePath);
                var fullTempPath = NativeIO.Path.Combine(tempDirectory, tempFilename);

                using var zip = new ZipFile(fullTempPath);

                var currentFile = 0;
                foreach (var element in elementList)
                {
                    element.Position = 0;
                    zip.AddEntry(elementNamesList[currentFile], element);
                    currentFile++;
                }

                zip.Save(zippedStream);

                return zippedStream;
            }
            catch
            {
                return NativeIO.Stream.Null;
            }
        }

        /// <summary>
        /// Try saves a <see cref="T:System.IO.Stream" /> into a zip file. If <paramref name="filename"/> is <b>null</b> (<b>Nothing</b> in Visual Basic) a random name will be used.
        /// </summary>
        /// <param name="target">Target to saves as zipped.</param>
        /// <param name="zipfileOutputPath">The zipfile output path.</param>
        /// <param name="filename">The name of stream into zipfile.</param>
        /// <returns>
        /// <b>true</b> if zip compressed file has been created; <b>false</b> otherwise.
        /// </returns>
        public static IResult SaveAsZip(this NativeIO.Stream target, string zipfileOutputPath, string filename = null)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));
            SentinelHelper.ArgumentNull(zipfileOutputPath, nameof(zipfileOutputPath));

            return target
                .AsZipStream(filename)
                .SaveToFile(NativeIO.Path.ChangeExtension(zipfileOutputPath, ZipExtension));
        }

        /// <summary>
        /// Try saves a specified streams list compressed into a zip file.
        /// </summary>
        /// <param name="items">Target to saves as zipped.</param>
        /// <param name="zipfileOutputPath">The zipfile output path.</param>
        /// <param name="itemNames">The name of stream into zipfile.</param>
        /// <returns>
        /// <b>true</b> if zip compressed file has been created; <b>false</b> otherwise.
        /// </returns>
        public static IResult SaveAsZip(this IEnumerable<NativeIO.Stream> items, string zipfileOutputPath, IEnumerable<string> itemNames)
        {
            var elementList = items as IList<NativeIO.Stream> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            SentinelHelper.ArgumentNull(zipfileOutputPath, nameof(zipfileOutputPath));

            var namesList = itemNames as IList<string> ?? itemNames.ToList();
            SentinelHelper.ArgumentNull(namesList, nameof(namesList));

            return elementList
                .AsZipStream(namesList)
                .SaveToFile(NativeIO.Path.ChangeExtension(zipfileOutputPath, ZipExtension));
        }

        /// <summary>
        /// Returns a value indicating whether zip compressed file has been created.
        /// </summary>
        /// <param name="target">Target to compress.</param>
        /// <param name="streamExtension">The stream extension.</param>
        /// <param name="outputPath">The output path.</param>
        /// <returns>
        /// Operation result.
        /// </returns>
        public static IResult TrySaveAsZip(this NativeIO.Stream target, string streamExtension, string outputPath) => 
            target.AsByteArray().TrySaveAsZip(streamExtension, outputPath);

        #endregion

        #region public static async methods

        /// <summary>
        /// Returns asynchronously a <see cref="T:System.IO.Stream" /> which contains specified stream compressed. If <paramref name="name"/> is <b>null</b> (<b> Nothing </b> in Visual Basic) a random name will be used.
        /// </summary>
        /// <param name="target">Target to compress.</param>
        /// <param name="name">Name of output <see cref="T:System.IO.Stream" />.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream" /> which contains this stream compressed.
        /// </returns>
        public static async Task<NativeIO.Stream> AsZipStreamAsync(this NativeIO.Stream target, string name = null, CancellationToken cancellationToken = default)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));

            string streamName;
            if (name == null)
            {
                var tempFilenameUri = File.GetUniqueTempRandomFile();
                var tempFilename = NativeIO.Path.GetFileName(tempFilenameUri.AbsolutePath);
                streamName = tempFilename;
            }
            else
            {
                streamName = name;
            }

            return await new[] { target }.AsZipStreamAsync(new[] { streamName }, cancellationToken);
        }

        /// <summary>
        /// Returns a <see cref="T:System.IO.Stream"/> which contains specified streams list compressed. The <paramref name="itemNames"/> contains the names for every stream entry in streams list.
        /// </summary>
        /// <param name="items">Elements to compress.</param>
        /// <param name="itemNames">The names for every stream entry in streams list.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> than contains the path to created file.
        /// </returns>
        public static async Task<NativeIO.Stream> AsZipStreamAsync(this IEnumerable<NativeIO.Stream> items, IEnumerable<string> itemNames, CancellationToken cancellationToken = default)
        {
            var elementList = items as IList<NativeIO.Stream> ?? (await  items.CloneAsync(cancellationToken)).ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            var elementNamesList = itemNames as IList<string> ?? itemNames.ToList();
            SentinelHelper.ArgumentNull(elementNamesList, nameof(elementNamesList));

            SentinelHelper.IsTrue(elementList.Count != elementNamesList.Count, "The parameter 'itemsNames' must have the same number of elements as the 'items' list");

            try
            {
                NativeIO.Stream zippedStream = new NativeIO.MemoryStream();

                var tempDirectory = File.TempDirectoryFullName;
                var tempFilenameUri = File.GetUniqueTempRandomFile();
                var tempFilename = NativeIO.Path.GetFileName(tempFilenameUri.AbsolutePath);
                var fullTempPath = NativeIO.Path.Combine(tempDirectory, tempFilename);

                using var zip = new ZipFile(fullTempPath);

                var currentFile = 0;
                foreach (var element in elementList)
                {
                    element.Position = 0;
                    zip.AddEntry(elementNamesList[currentFile], element);
                    currentFile++;
                }

                zip.Save(zippedStream);

                return zippedStream;
            }
            catch
            {
                return NativeIO.Stream.Null;
            }
        }

        /// <summary>
        /// Try saves a <see cref="T:System.IO.Stream" /> into a zip file. If <paramref name="filename"/> is <b>null</b> (<b>Nothing</b> in Visual Basic) a random name will be used.
        /// </summary>
        /// <param name="target">Target to saves as zipped.</param>
        /// <param name="zipfileOutputPath">The zipfile output path.</param>
        /// <param name="filename">The name of stream into zipfile.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// <b>true</b> if zip compressed file has been created; <b>false</b> otherwise.
        /// </returns>
        public static async Task<IResult> SaveAsZipAsync(this NativeIO.Stream target, string zipfileOutputPath, string filename = null, CancellationToken cancellationToken = default)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));
            SentinelHelper.ArgumentNull(zipfileOutputPath, nameof(zipfileOutputPath));

            return await (await target.AsZipStreamAsync(filename, cancellationToken)).SaveToFileAsync(NativeIO.Path.ChangeExtension(zipfileOutputPath, ZipExtension), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Try saves a specified streams list compressed into a zip file.
        /// </summary>
        /// <param name="items">Target to saves as zipped.</param>
        /// <param name="zipfileOutputPath">The zipfile output path.</param>
        /// <param name="itemNames">The name of stream into zipfile.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// <b>true</b> if zip compressed file has been created; <b>false</b> otherwise.
        /// </returns>
        public static async Task<IResult> SaveAsZipAsync(this IEnumerable<NativeIO.Stream> items, string zipfileOutputPath, IEnumerable<string> itemNames, CancellationToken cancellationToken = default)
        {
            var elementList = items as IList<NativeIO.Stream> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            SentinelHelper.ArgumentNull(zipfileOutputPath, nameof(zipfileOutputPath));

            var namesList = itemNames as IList<string> ?? itemNames.ToList();
            SentinelHelper.ArgumentNull(namesList, nameof(namesList));

            return await (await elementList.AsZipStreamAsync(namesList, cancellationToken)).SaveToFileAsync(NativeIO.Path.ChangeExtension(zipfileOutputPath, ZipExtension), cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Returns a value indicating whether zip compressed file has been created.
        /// </summary>
        /// <param name="target">Target to compress.</param>
        /// <param name="streamExtension">The stream extension.</param>
        /// <param name="outputPath">The output path.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// Operation result.
        /// </returns>
        public static async Task<IResult> TrySaveAsZipAsync(this NativeIO.Stream target, string streamExtension, string outputPath, CancellationToken cancellationToken = default) =>
            await (await target.AsByteArrayAsync(cancellationToken)).TrySaveAsZipAsync(streamExtension, outputPath, cancellationToken);

        #endregion
    }
}
