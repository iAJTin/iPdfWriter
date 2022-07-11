
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        #region [public] {static} (IResult) TrySaveAsZip(this byte[], string, string): Try to Create a zip file with specified element
        /// <summary>
        /// Try to Create a zip file with specified element.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <param name="itemExtension">File elements extension.</param>
        /// <param name="outputPath">Zip output path.</param>
        /// <returns>
        /// A <see cref="IResult"/> than contains zipped file.
        /// </returns>
        public static IResult TrySaveAsZip(this byte[] item, string itemExtension, string outputPath) => new[] {item}.TrySaveAsZip(itemExtension, outputPath);
        #endregion

        #region [public] {static} (IResult) TrySaveAsZip(this IEnumerable<byte[]>, string, string): Try to Create a zip file with specified elements
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
            IList<byte[]> elementList = items as IList<byte[]> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(items));

            try
            {
                string zipFilenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(outputPath);

                bool existPath = NativeIO.File.Exists(outputPath);
                if (existPath)
                {
                    NativeIO.File.Delete(outputPath);
                }

                var outputDirectoryName = NativeIO.Path.GetDirectoryName(outputPath);
                var di = NativeIO.Directory.CreateDirectory(outputDirectoryName);

                using (var zip = new ZipFile(outputPath))
                {
                    int currentFile = 0;
                    bool multipleElements = elementList.Count > 1;
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
        #endregion

        #region [public] {static} (NativeIO.Stream) ToZipStream(this byte[]): Returns a stream with with specified element compressed
        /// <summary>
        /// Returns a stream with with specified element compressed.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> than contains zipped file.
        /// </returns>
        public static NativeIO.Stream ToZipStream(this byte[] item) => new[] {item}.ToZipStream();
        #endregion

        #region [public] {static} (NativeIO.Stream) ToZipStream(this IEnumerable<byte[]>): Returns a stream with with specified elements compressed
        /// <summary>
        /// Returns a stream with with specified element compressed
        /// </summary>
        /// <param name="elements">Zip elements.</param>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> than contains zipped file.
        /// </returns>
        public static NativeIO.Stream ToZipStream(this IEnumerable<byte[]> elements)
        {
            IList<byte[]> elementList = elements as IList<byte[]> ?? elements.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elements));

            File.CleanOrCreateTemporaryDirectory();

            Uri zipNameTempUri = File.GetUniqueTempRandomFile();
            string zipNameLocalPath = NativeIO.Path.GetFileName(zipNameTempUri.LocalPath);
            string tempDirectory = File.TempDirectoryFullName;
            string zipFullPath = NativeIO.Path.Combine(tempDirectory, zipNameLocalPath);
            string zipFilenameWithExtension = NativeIO.Path.GetFileName(zipFullPath);
            string zipFilenameWithoutExtension = NativeIO.Path.GetFileNameWithoutExtension(zipFullPath);

            bool existPath = NativeIO.File.Exists(zipFullPath);
            if (existPath)
            {
                NativeIO.File.Delete(zipFullPath);
            }

            string outputStreamName = $"_{zipFilenameWithExtension}";
            string outputStreamFullPath = NativeIO.Path.Combine(tempDirectory, outputStreamName);
            NativeIO.FileStream fs = new(outputStreamFullPath, NativeIO.FileMode.OpenOrCreate, NativeIO.FileAccess.ReadWrite, NativeIO.FileShare.ReadWrite);
            using (var zip = new ZipFile(zipFullPath))
            {
                int currentFile = 0;
                bool multipleElements = elementList.Count > 1;
                StringBuilder filenameBuilder = new StringBuilder();
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
        #endregion
    }
}
