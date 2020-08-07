
namespace Syntec.Core.IO.Compression
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    using Ionic.Zip;

    using ComponentModel;
    using Helpers;

    using SyntecIO = Syntec.Core.IO;

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

        #region [public] {static} (Stream) AsZipStream(this Stream, string = null): Returns a stream which contains specified stream compressed. If name is null (Nothing in Visual Basic) a random name will be used
        /// <summary>
        /// Returns a <see cref="T:System.IO.Stream" /> which contains specified stream compressed. If <paramref name="name"/> is <b>null</b> (<b> Nothing </b> in Visual Basic) a random name will be used.
        /// </summary>
        /// <param name="target">Target to compress.</param>
        /// <param name="name">Name of output <see cref="T:System.IO.Stream" />.</param>
        /// <returns>
        /// A <see cref="T:System.IO.Stream" /> which contains this stream compressed.
        /// </returns>
        public static Stream AsZipStream(this Stream target, string name = null)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));

            string streamName;
            if (name == null)
            {
                Uri tempFilenameUri = SyntecIO.File.GetUniqueTempRandomFile();
                string tempFilename = Path.GetFileName(tempFilenameUri.AbsolutePath);
                streamName = tempFilename;
            }
            else
            {
                streamName = name;
            }

            return new[] { target }.AsZipStream(new[] { streamName });
        }
        #endregion

        #region [public] {static} (Stream) AsZipStream(this IEnumerable<Stream>, IEnumerable<string>): Returns a stream which contains specified streams list compressed. The itemNames contains the names for every stream entry in streams list
        /// <summary>
        /// Returns a <see cref="T:System.IO.Stream"/> which contains specified streams list compressed. The <paramref name="itemNames"/> contains the names for every stream entry in streams list.
        /// </summary>
        /// <param name="items">Elements to compress.</param>
        /// <param name="itemNames">The names for every stream entry in streams list.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> than contains the path to created file.
        /// </returns>
        public static Stream AsZipStream(this IEnumerable<Stream> items, IEnumerable<string> itemNames)
        {
            IList<Stream> elementList = items as IList<Stream> ?? items.Clone().ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            IList<string> elementNamesList = itemNames as IList<string> ?? itemNames.ToList();
            SentinelHelper.ArgumentNull(elementNamesList, nameof(elementNamesList));

            SentinelHelper.IsTrue(elementList.Count != elementNamesList.Count, "The parameter 'itemsNames' must have the same number of elements as the 'items' list");

            try
            {
                Stream zippedStream = new MemoryStream();

                string tempDirectory = SyntecIO.File.TempDirectoryFullName;
                Uri tempFilenameUri = SyntecIO.File.GetUniqueTempRandomFile();
                string tempFilename = Path.GetFileName(tempFilenameUri.AbsolutePath);
                string fullTempPath = Path.Combine(tempDirectory, tempFilename);
                using (ZipFile zip = new ZipFile(fullTempPath))
                {
                    int currentFile = 0;
                    foreach (Stream element in elementList)
                    {
                        element.Position = 0;
                        zip.AddEntry(elementNamesList[currentFile], element);
                        currentFile++;
                    }

                    zip.Save(zippedStream);
                }

                return zippedStream;
            }
            catch
            {
                return Stream.Null;
            }
        }
        #endregion

        #region [public] {static} (IResult) SaveAsZip(this Stream, string, string = null): Try saves a stream into a zip file. If filename is null (Nothing in Visual Basic) a random name will be used.
        /// <summary>
        /// Try saves a <see cref="T:System.IO.Stream" /> into a zip file. If <paramref name="filename"/> is <b>null</b> (<b>Nothing</b> in Visual Basic) a random name will be used.
        /// </summary>
        /// <param name="target">Target to saves as zipped.</param>
        /// <param name="zipfileOutputPath">The zipfile output path.</param>
        /// <param name="filename">The name of stream into zipfile.</param>
        /// <returns>
        /// <c>true</c> if zip compressed file has been created; <c>false</c> otherwise.
        /// </returns>
        public static IResult SaveAsZip(this Stream target, string zipfileOutputPath, string filename = null)
        {
            SentinelHelper.ArgumentNull(target, nameof(target));
            SentinelHelper.ArgumentNull(zipfileOutputPath, nameof(zipfileOutputPath));

            return target
                .AsZipStream(filename)
                .SaveToFile(Path.ChangeExtension(zipfileOutputPath, ZipExtension));
        }
        #endregion

        #region [public] {static} (IResult) SaveAsZip(this IEnumerable<Stream>, string, IEnumerable<string>): Try saves a specified streams list compressed into a zip file
        /// <summary>
        /// Try saves a specified streams list compressed into a zip file.
        /// </summary>
        /// <param name="items">Target to saves as zipped.</param>
        /// <param name="zipfileOutputPath">The zipfile output path.</param>
        /// <param name="itemNames">The name of stream into zipfile.</param>
        /// <returns>
        /// <c>true</c> if zip compressed file has been created; <c>false</c> otherwise.
        /// </returns>
        public static IResult SaveAsZip(this IEnumerable<Stream> items, string zipfileOutputPath, IEnumerable<string> itemNames)
        {
            IList<Stream> elementList = items as IList<Stream> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            SentinelHelper.ArgumentNull(zipfileOutputPath, nameof(zipfileOutputPath));

            IList<string> namesList = itemNames as IList<string> ?? itemNames.ToList();
            SentinelHelper.ArgumentNull(namesList, nameof(namesList));

            return elementList
                .AsZipStream(namesList)
                .SaveToFile(Path.ChangeExtension(zipfileOutputPath, ZipExtension));
        }
        #endregion

        #region [public] {static} (bool) TrySaveAsZip(this Stream, string, string): Returns a value indicating whether zip compressed file has been created
        /// <summary>
        /// Returns a value indicating whether zip compressed file has been created.
        /// </summary>
        /// <param name="target">Target to compress.</param>
        /// <param name="streamExtension">The stream extension.</param>
        /// <param name="outputPath">The output path.</param>
        /// <returns>
        /// <c>true</c> if zip compressed file has been created; <c>false</c> otherwise.
        /// </returns>
        public static bool TrySaveAsZip(this Stream target, string streamExtension, string outputPath)
        {
            return target.AsByteArray().TrySaveAsZip(streamExtension, outputPath);
        }
        #endregion

        #endregion
    }
}
