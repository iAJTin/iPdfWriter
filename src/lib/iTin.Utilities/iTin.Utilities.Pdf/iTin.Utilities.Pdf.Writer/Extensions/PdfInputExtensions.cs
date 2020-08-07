﻿
namespace iTin.Utilities.Pdf.Writer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using iTin.Core.IO.Compression;
    using iTin.Core.Helpers;

    using iTin.Utilities.Pdf.Writer.ComponentModel;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output;

    /// <summary>
    /// Static class than contains common extension methods for objects of type <see cref="PdfInput"/>.
    /// </summary>
    public static class PdfInputExtensions
    {
        #region public static methods

        #region [public] {static} (IEnumerable<PdfInput>) Clone(this IEnumerable<PdfInput>): Clones the specified items
        /// <summary>
        /// Clones the specified items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>IEnumerable&lt;PdfInput&gt;.</returns>
        public static IEnumerable<PdfInput> Clone(this IEnumerable<PdfInput> items)
        {
            IList<PdfInput> elementList = items as IList<PdfInput> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            return elementList.Select(element => element.Clone()).ToList();
        }
        #endregion

        #region [public] {static} (OutputResult) CreateJoinResult(this IEnumerable<PdfInput>, IEnumerable<string>): Returns a new OutputResult reference thats represents a one unique zip stream that contains the same entries in items but compressed individually using the names in filenames list
        /// <summary>
        /// Returns a new <see cref="T:iTin.Utilities.Pdf.ComponentModel.OutputResult" /> reference thats represents a one <b>unique zip stream</b> that contains the same entries in <param ref="items"/> 
        /// but compressed individually using the name in <param ref="filenames"/>.         
        /// </summary>
        /// <param name="items">Items</param>
        /// <param name="filenames">Item filenames.</param>
        /// <returns>
        /// A <see cref="T:iTin.Core.ComponentModel.OutputResult" /> reference that contains action result.
        /// </returns>
        public static OutputResult CreateJoinResult(this IEnumerable<PdfInput> items, IEnumerable<string> filenames)
        {
            IList<PdfInput> elementList = items as IList<PdfInput> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            IList<string> filenameList = filenames as IList<string> ?? filenames.ToList();
            SentinelHelper.ArgumentNull(filenameList, nameof(filenameList));

            try
            {
                Stream zippedStream = elementList.ToStreamEnumerable().AsZipStream(filenameList);
                zippedStream.Position = 0;
                return
                    OutputResult.CreateSuccessResult(
                        new OutputResultData
                        {
                            Zipped = true,
                            Configuration = null,
                            UncompressOutputStream = zippedStream
                        });
            }
            catch (Exception e)
            {
                return OutputResult.FromException(e);
            }
        }
        #endregion

        #region [public] {static} (IEnumerable<OutputResult>) ToOutputResults(this IEnumerable<IEnumerable<PdfInput>>): Merges every entry list in multiInputItems parameter into a unique OutputResult item
        /// <summary>
        /// Merges every entry list in <paramref name="multiInputItems"/> parameter into a unique <see cref="OutputResult"/> item.
        /// </summary>
        /// <param name="multiInputItems">The list of input items list to merge.</param>
        /// <returns>
        /// An <see cref="IEnumerable{OutputResult}"/>.
        /// </returns>
        public static IEnumerable<OutputResult> ToOutputResults(this IEnumerable<IEnumerable<PdfInput>> multiInputItems)
        {
            IEnumerable<IEnumerable<PdfInput>> elementList = multiInputItems as IList<IList<PdfInput>> ?? multiInputItems;
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            return multiInputItems
                .Select(items =>new PdfObject(new PdfObjectConfig { AllowCompression = false, CompressionThreshold = 0.1f }) { Items = items })
                .Select(pdfObject => pdfObject.TryMergeInputs()).ToList();
        }
        #endregion

        #endregion

        #region private static methods

        #region [private] {static} (IEnumerable<Stream>) ToStreamEnumerable(this IEnumerable<PdfInput>): Converts the enumerated type PdfInput to enumerated streams
        private static IEnumerable<Stream> ToStreamEnumerable(this IEnumerable<PdfInput> items)
        {
            IList<PdfInput> elementList = items as IList<PdfInput> ?? items.ToList();
            SentinelHelper.ArgumentNull(elementList, nameof(elementList));

            return elementList.Select(item => item.Clone().ToStream());
        }
        #endregion

        #endregion
    }
}
