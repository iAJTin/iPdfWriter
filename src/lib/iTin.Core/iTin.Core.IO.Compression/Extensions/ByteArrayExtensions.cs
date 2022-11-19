
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Ionic.Zip;

using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;
using iTin.Core.Helpers;

using NativeIO = System.IO;

namespace iTin.Core.IO.Compression
{
    /// <summary>
    /// Static class than contains extension methods for objects <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.
    /// </summary> 
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Try to Create a zip file with specified element.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <param name="itemExtension">File elements extension.</param>
        /// <param name="outputPath">Zip output path.</param>
        /// <returns>
        /// A <see cref="IResult"/> than contains zipped file.
        /// </returns>
        public static IResult TrySaveAsZip(this byte[] item, string itemExtension, string outputPath) => 
            new[] {item}.TrySaveAsZip(itemExtension, outputPath);

        /// <summary>
        /// Try to Create a zip file with specified elements.
        /// </summary>
        /// <param name="items">Element to compress.</param>
        /// <param name="itemsExtension">File elements extension.</param>
        /// <param name="outputPath">Zip output path.</param>
        /// <returns>
        /// A <see cref="string"/> than contains the path to created file.
        /// </returns>
        public static IResult TrySaveAsZip(this IEnumerable<byte[]> items, string itemsExtension, string outputPath)
        {
            var elementList = items as IList<byte[]> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(items));

            try
            {
                var zipFilenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(outputPath);

                var existPath = NativeIO.File.Exists(outputPath);
                if (existPath)
                {
                    NativeIO.File.Delete(outputPath);
                }

                var outputDirectoryName = NativeIO.Path.GetDirectoryName(outputPath);
                var di = NativeIO.Directory.CreateDirectory(outputDirectoryName);

                using (var zip = new ZipFile(outputPath))
                {
                    var currentFile = 0;
                    var multipleElements = elementList.Count > 1;
                    var filenameBuilder = new StringBuilder();
                    foreach (var element in elementList)
                    {
                        filenameBuilder.Clear();
                        filenameBuilder.Append(zipFilenameWithoutExtension);

                        if (multipleElements)
                        {
                            filenameBuilder.Append(currentFile);
                        }

                        filenameBuilder.Append(".");
                        filenameBuilder.Append(itemsExtension);
                        zip.AddEntry(filenameBuilder.ToString(), element);
                        currentFile++;
                    }

                    zip.Save();
                }

                return BooleanResult.SuccessResult;
            }
            catch(Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }

        /// <summary>
        /// Returns a stream with with specified element compressed.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> than contains zipped file.
        /// </returns>
        public static NativeIO.Stream ToZipStream(this byte[] item) =>
            new[] {item}.ToZipStream();

        /// <summary>
        /// Returns a stream with with specified element compressed
        /// </summary>
        /// <param name="elements">Zip elements.</param>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> than contains zipped file.
        /// </returns>
        public static NativeIO.Stream ToZipStream(this IEnumerable<byte[]> elements)
        {
            var elementList = elements as IList<byte[]> ?? elements.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elements));

            File.CleanOrCreateTemporaryDirectory();

            var zipNameTempUri = File.GetUniqueTempRandomFile();
            var zipNameLocalPath = NativeIO.Path.GetFileName(zipNameTempUri.LocalPath);
            var tempDirectory = File.TempDirectoryFullName;
            var zipFullPath = NativeIO.Path.Combine(tempDirectory, zipNameLocalPath);
            var zipFilenameWithExtension = NativeIO.Path.GetFileName(zipFullPath);
            var zipFilenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(zipFullPath);

            var existPath = NativeIO.File.Exists(zipFullPath);
            if (existPath)
            {
                NativeIO.File.Delete(zipFullPath);
            }

            var outputStreamName = $"_{zipFilenameWithExtension}";
            var outputStreamFullPath = NativeIO.Path.Combine(tempDirectory, outputStreamName);
            var fs = new NativeIO.FileStream(outputStreamFullPath, NativeIO.FileMode.OpenOrCreate, NativeIO.FileAccess.ReadWrite, NativeIO.FileShare.ReadWrite);
            using (var zip = new ZipFile(zipFullPath))
            {
                var currentFile = 0;
                var multipleElements = elementList.Count > 1;
                var filenameBuilder = new StringBuilder();
                foreach (var element in elementList)
                {
                    filenameBuilder.Clear();
                    filenameBuilder.Append(zipFilenameWithoutExtension);

                    if (multipleElements)
                    {
                        filenameBuilder.Append(currentFile);
                    }

                    filenameBuilder.Append(".");
                    filenameBuilder.Append("pdf");
                    zip.AddEntry(filenameBuilder.ToString(), element);
                    currentFile++;
                }

                zip.Save(fs);
            }

            return fs.AsByteArray().ToMemoryStream();
        }


        /// <summary>
        /// Try to Create asynchronously a zip file with specified element.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <param name="itemExtension">File elements extension.</param>
        /// <param name="outputPath">Zip output path.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="IResult"/> than contains zipped file.
        /// </returns>
        public static async Task<IResult> TrySaveAsZipAsync(this byte[] item, string itemExtension, string outputPath, CancellationToken cancellationToken = default) =>
            await new[] { item }.TrySaveAsZipAsync(itemExtension, outputPath, cancellationToken);

        /// <summary>
        /// Try to Create asynchronously a zip file with specified elements.
        /// </summary>
        /// <param name="items">Element to compress.</param>
        /// <param name="itemsExtension">File elements extension.</param>
        /// <param name="outputPath">Zip output path.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="string"/> than contains the path to created file.
        /// </returns>
        public static async Task<IResult> TrySaveAsZipAsync(this IEnumerable<byte[]> items, string itemsExtension, string outputPath, CancellationToken cancellationToken = default)
        {
            var elementList = items as IList<byte[]> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(items));

            try
            {
                var zipFilenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(outputPath);

                var existPath = NativeIO.File.Exists(outputPath);
                if (existPath)
                {
                    NativeIO.File.Delete(outputPath);
                }

                var outputDirectoryName = NativeIO.Path.GetDirectoryName(outputPath);
                var di = NativeIO.Directory.CreateDirectory(outputDirectoryName);

                using (var zip = new ZipFile(outputPath))
                {
                    var currentFile = 0;
                    var multipleElements = elementList.Count > 1;
                    var filenameBuilder = new StringBuilder();
                    foreach (var element in elementList)
                    {
                        filenameBuilder.Clear();
                        filenameBuilder.Append(zipFilenameWithoutExtension);

                        if (multipleElements)
                        {
                            filenameBuilder.Append(currentFile);
                        }

                        filenameBuilder.Append(".");
                        filenameBuilder.Append(itemsExtension);
                        zip.AddEntry(filenameBuilder.ToString(), element);
                        currentFile++;
                    }

                    zip.Save();
                }

                return await Task.FromResult(BooleanResult.SuccessResult);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(BooleanResult.FromException(ex));
            }
        }

        /// <summary>
        /// Returns a stream with with specified element compressed asynchronously.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> than contains zipped file.
        /// </returns>
        public static async Task<NativeIO.Stream> ToZipStreamAsync(this byte[] item, CancellationToken cancellationToken = default) =>
            await new[] { item }.ToZipStreamAsync(cancellationToken);

        /// <summary>
        /// Returns a stream with with specified element compressed asynchronously.
        /// </summary>
        /// <param name="elements">Zip elements.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> than contains zipped file.
        /// </returns>
        public static async Task<NativeIO.Stream> ToZipStreamAsync(this IEnumerable<byte[]> elements, CancellationToken cancellationToken = default)
        {
            var elementList = elements as IList<byte[]> ?? elements.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elements));

            File.CleanOrCreateTemporaryDirectory();

            var zipNameTempUri = File.GetUniqueTempRandomFile();
            var zipNameLocalPath = NativeIO.Path.GetFileName(zipNameTempUri.LocalPath);
            var tempDirectory = File.TempDirectoryFullName;
            var zipFullPath = NativeIO.Path.Combine(tempDirectory, zipNameLocalPath);
            var zipFilenameWithExtension = NativeIO.Path.GetFileName(zipFullPath);
            var zipFilenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(zipFullPath);

            var existPath = NativeIO.File.Exists(zipFullPath);
            if (existPath)
            {
                NativeIO.File.Delete(zipFullPath);
            }

            var outputStreamName = $"_{zipFilenameWithExtension}";
            var outputStreamFullPath = NativeIO.Path.Combine(tempDirectory, outputStreamName);
            var fs = new NativeIO.FileStream(outputStreamFullPath, NativeIO.FileMode.OpenOrCreate, NativeIO.FileAccess.ReadWrite, NativeIO.FileShare.ReadWrite);
            using (var zip = new ZipFile(zipFullPath))
            {
                var currentFile = 0;
                var multipleElements = elementList.Count > 1;
                var filenameBuilder = new StringBuilder();
                foreach (var element in elementList)
                {
                    filenameBuilder.Clear();
                    filenameBuilder.Append(zipFilenameWithoutExtension);

                    if (multipleElements)
                    {
                        filenameBuilder.Append(currentFile);
                    }

                    filenameBuilder.Append(".");
                    filenameBuilder.Append("pdf");
                    zip.AddEntry(filenameBuilder.ToString(), element);
                    currentFile++;
                }

                zip.Save(fs);
            }

            return (await fs.AsByteArrayAsync(cancellationToken)).ToMemoryStream();
        }
    }
}
