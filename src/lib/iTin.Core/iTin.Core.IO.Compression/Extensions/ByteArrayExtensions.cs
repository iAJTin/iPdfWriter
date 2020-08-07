
namespace iTin.Core.IO.Compression
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Ionic.Zip;

    using iTin.Core.ComponentModel;
    using iTin.Core.ComponentModel.Results;
    using iTin.Core.Helpers;

    using iTinIO = iTin.Core.IO;

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
        /// A <see cref="Stream"/> than contains zipped file.
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
                string zipFilenameWithoutExtension = Path.GetFileNameWithoutExtension(outputPath);

                File.Delete(outputPath);

                using (ZipFile zip = new ZipFile(outputPath))
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

        #region [public] {static} (Stream) ToZipStream(this byte[]): Returns a stream with with specified element compressed
        /// <summary>
        /// Returns a stream with with specified element compressed.
        /// </summary>
        /// <param name="item">Element to compress.</param>
        /// <returns>
        /// A <see cref="Stream"/> than contains zipped file.
        /// </returns>
        public static Stream ToZipStream(this byte[] item) => new[] {item}.ToZipStream();
        #endregion

        #region [public] {static} (Stream) ToZipStream(this IEnumerable<byte[]>): Returns a stream with with specified elements compressed
        /// <summary>
        /// Returns a stream with with specified element compressed
        /// </summary>
        /// <param name="elements">Zip elements.</param>
        /// <returns>
        /// A <see cref="Stream"/> than contains zipped file.
        /// </returns>
        public static Stream ToZipStream(this IEnumerable<byte[]> elements)
        {
            IList<byte[]> elementList = elements as IList<byte[]> ?? elements.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elements));

            iTinIO.File.CleanOrCreateTemporaryDirectory();

            Uri zipNameTempUri = iTinIO.File.GetUniqueTempRandomFile();
            string zipNameLocalPath = Path.GetFileName(zipNameTempUri.LocalPath);
            string tempDirectory = iTinIO.File.TempDirectoryFullName;
            string zipFullPath = Path.Combine(tempDirectory, zipNameLocalPath);
            string zipFilenameWithExtension = Path.GetFileName(zipFullPath);
            string zipFilenameWithoutExtension = Path.GetFileNameWithoutExtension(zipFullPath);

            File.Delete(zipFullPath);

            string outputStreamName = string.Format(CultureInfo.InvariantCulture, "_{0}", zipFilenameWithExtension);
            string outputStreamFullPath = Path.Combine(tempDirectory, outputStreamName);
            FileStream fs = new FileStream(outputStreamFullPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
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
