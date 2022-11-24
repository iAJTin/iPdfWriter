
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using iTextSharp.text;
using iTextSharp.text.pdf;

using iTin.Core;
using iTin.Core.Helpers;

using iTin.Utilities.Abstractions.Writer.Operations.Results;

using iTin.Utilities.Pdf.Writer.ComponentModel;
using iTin.Utilities.Pdf.Writer.Operations.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Result.Output;

namespace iTin.Utilities.Pdf.Writer
{
    /// <summary>
    /// Represents a generic pdf object, this allows add pdf files to <see cref="PdfObject.Items"/> property and specify a user custom configuration.
    /// </summary>

    public class PdfObject : IDisposable

#if NETCOREAPP3_1 || NETSTANDARD2_1 || NET5_0_OR_GREATER

    , IAsyncDisposable

#endif 

    {
        #region private field members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;

        #endregion

        #region constructor/s

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfObject"/> class with default configuration.
        /// </summary>
        public PdfObject() : this(PdfObjectConfig.Default)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfObject"/> class with specified configuration
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public PdfObject(PdfObjectConfig configuration)
        {
            Configuration = configuration;
            Items = new List<PdfInput>();
        }

        #endregion

        #region finalizer

        /// <summary>
        /// Finalizer
        /// </summary>
        ~PdfObject()
        {
            Dispose(false);
        }

        #endregion

        #region interfaces

#if NETCOREAPP3_1 || NETSTANDARD2_1 || NET5_0_OR_GREATER

        #region IAsyncDisposable

        #region public async methods

        /// <summary>
        /// Clean managed resources
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

#endif

        #region IDisposable

        #region public methods

        /// <summary>
        /// Clean managed resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion

        #endregion

        #region public properties

        /// <summary>
        /// Gets or sets the pdf input list.
        /// </summary>
        /// <Result>
        /// The items.
        /// </Result>
        public IEnumerable<PdfInput> Items { get; set; }

        /// <summary>
        /// Gets the configuration settings.
        /// </summary>
        /// <Result>
        /// The object configuration.
        /// </Result>
        public PdfObjectConfig Configuration { get; }

        #endregion

        #region public methods

        /// <summary>
        /// Merges all <see cref="PdfInput"/> entries.
        /// </summary>
        /// <returns>
        /// <para>
        /// A <see cref="OutputResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="IOutputResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public OutputResult TryMergeInputs()
        {
            Items.ForEach(item => item.ProcessInput());

            if (Configuration.UseIndex)
            {
                Items = Items.OrderBy(i => i.Index).ToList();
            }

            try
            {
                var outStream = new MemoryStream();
                using (var document = new Document())
                {
                    using (var copy = new PdfCopy(document, outStream))
                    {
                        document.Open();
                        
                        PdfReader.unethicalreading = true;

                        foreach (var item in Items)
                        {
                            var itemAsStream = item.Clone().ToStream();
                            if (itemAsStream == null)
                            {
                                continue;
                            }

                            itemAsStream.Position = 0;
                            copy.AddDocument(new PdfReader(itemAsStream));
                        }
                    }
                }

                var hasSystemTags = false;
                if (Configuration.Tags.Any())
                {
                    hasSystemTags = true;

                    var pdfRawMerged = new PdfInput
                    {
                        AutoUpdateChanges = true,
                        Input = outStream.GetBuffer().ToMemoryStream().Clone()
                    };

                    ReplaceResult rawMergedResult = null;
                    foreach (var tag in Configuration.Tags)
                    {
                        pdfRawMerged.Replace(new ReplaceSystemTag(tag.BuildReplacementObject()));
                        rawMergedResult = pdfRawMerged.ProcessInput();
                    }
                    
                    if (rawMergedResult.Success)
                    {
                        outStream = new MemoryStream((byte[])rawMergedResult.Result.OutputStream.AsByteArray().Clone());
                    }
                }

                var hasGlobalReplacements = false;
                if (Configuration.GlobalReplacements.Any())
                {
                    hasGlobalReplacements = true;

                    var pdfRawMergedWithTags = new PdfInput
                    {
                        AutoUpdateChanges = true,
                        Input = hasSystemTags ? outStream.ToMemoryStream().Clone() : outStream.GetBuffer().ToMemoryStream().Clone()
                    };

                    ReplaceResult rawMergedWithTagsResult = null;
                    foreach (var replacement in Configuration.GlobalReplacements)
                    {
                        pdfRawMergedWithTags.Replace(new ReplaceText(replacement));
                        rawMergedWithTagsResult = pdfRawMergedWithTags.ProcessInput();
                    }

                    if (rawMergedWithTagsResult.Success)
                    {
                        if (rawMergedWithTagsResult.Result.OutputStream.Position != 0)
                        {
                            rawMergedWithTagsResult.Result.OutputStream.Position = 0;
                        }

                        outStream = new MemoryStream((byte[])rawMergedWithTagsResult.Result.OutputStream.AsByteArray().Clone());
                    }
                }

                if (Configuration.DeletePhysicalFilesAfterMerge)
                {
                    foreach (var item in Items)
                    {
                        var inputType = item.InputType;
                        if (inputType != KnownInputType.Filename)
                        {
                            continue;
                        }

                        if (item.DeletePhysicalFilesAfterMerge)
                        {
                            File.Delete(TypeHelper.ToType<string>(item.Input));
                        }
                    }
                }

                var safeOutAsByteArray = (hasSystemTags || hasGlobalReplacements) ? outStream.AsByteArray() : outStream.GetBuffer();
                var outputInMegaBytes = (float)safeOutAsByteArray.Length / PdfObjectConfig.OneMegaByte;
                var generateOutputAsZip = outputInMegaBytes > Configuration.CompressionThreshold;
                var zipped = Configuration.AllowCompression && generateOutputAsZip;

                return
                    OutputResult.CreateSuccessResult(
                        new PdfOutputResultData
                        {
                            Zipped = zipped,
                            Configuration = Configuration,
                            UncompressOutputStream = safeOutAsByteArray.ToMemoryStream()
                        });
            }    
            catch (Exception ex)
            {
                return OutputResult.FromException(ex);
            }
        }

        #endregion

        #region public async methods

        /// <summary>
        /// Merges all <see cref="PdfInput"/> entries asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>
        /// <para>
        /// A <see cref="OutputResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="IOutputResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public async Task<OutputResult> TryMergeInputsAsync(CancellationToken cancellationToken = default)
        {
            Items.ForEach(async item => await item.ProcessInputAsync(cancellationToken));

            if (Configuration.UseIndex)
            {
                Items = Items.OrderBy(i => i.Index);
            }

            try
            {
                var outStream = new MemoryStream();
                using (var document = new Document())
                {
                    using var copy = new PdfCopy(document, outStream);
                    document.Open();

                    PdfReader.unethicalreading = true;

                    foreach (var item in Items)
                    {
                        var itemAsStream = await (await item.CloneAsync(cancellationToken)).ToStreamAsync(cancellationToken);
                        if (itemAsStream == null)
                        {
                            continue;
                        }

                        itemAsStream.Position = 0;
                        copy.AddDocument(new PdfReader(itemAsStream));
                    }
                }

                var hasSystemTags = false;
                if (Configuration.Tags.Any())
                {
                    hasSystemTags = true;

                    var pdfRawMerged = new PdfInput
                    {
                        AutoUpdateChanges = true,
                        Input = await outStream.GetBuffer().ToMemoryStream().CloneAsync(cancellationToken)
                    };

                    ReplaceResult rawMergedResult = null;
                    foreach (var tag in Configuration.Tags)
                    {
                        pdfRawMerged.Replace(new ReplaceSystemTag(tag.BuildReplacementObject()));
                        rawMergedResult = await pdfRawMerged.ProcessInputAsync(cancellationToken);
                    }

                    if (rawMergedResult.Success)
                    {
                        outStream = new MemoryStream((byte[]) (await rawMergedResult.Result.OutputStream.AsByteArrayAsync(cancellationToken)).Clone());
                    }
                }

                var hasGlobalReplacements = false;
                if (Configuration.GlobalReplacements.Any())
                {
                    hasGlobalReplacements = true;

                    var pdfRawMergedWithTags = new PdfInput
                    {
                        AutoUpdateChanges = true,
                        Input = hasSystemTags
                            ? await outStream.ToMemoryStream().CloneAsync(cancellationToken)
                            : await outStream.GetBuffer().ToMemoryStream().CloneAsync(cancellationToken)
                    };

                    ReplaceResult rawMergedWithTagsResult = null;
                    foreach (var replacement in Configuration.GlobalReplacements)
                    {
                        pdfRawMergedWithTags.Replace(new ReplaceText(replacement));
                        rawMergedWithTagsResult = await pdfRawMergedWithTags.ProcessInputAsync(cancellationToken);
                    }

                    if (rawMergedWithTagsResult.Success)
                    {
                        if (rawMergedWithTagsResult.Result.OutputStream.Position != 0)
                        {
                            rawMergedWithTagsResult.Result.OutputStream.Position = 0;
                        }

                        outStream = new MemoryStream((byte[]) (await rawMergedWithTagsResult.Result.OutputStream.AsByteArrayAsync()).Clone());
                    }
                }

                if (Configuration.DeletePhysicalFilesAfterMerge)
                {
                    foreach (var item in Items)
                    {
                        var inputType = item.InputType;
                        if (inputType != KnownInputType.Filename)
                        {
                            continue;
                        }

                        if (item.DeletePhysicalFilesAfterMerge)
                        {
                            File.Delete(TypeHelper.ToType<string>(item.Input));
                        }
                    }
                }

                var safeOutAsByteArray = (hasSystemTags || hasGlobalReplacements) ? await outStream.AsByteArrayAsync(cancellationToken) : outStream.GetBuffer();
                var outputInMegaBytes = (float)safeOutAsByteArray.Length / PdfObjectConfig.OneMegaByte;
                var generateOutputAsZip = outputInMegaBytes > Configuration.CompressionThreshold;
                var zipped = Configuration.AllowCompression && generateOutputAsZip;

                return
                    OutputResult.CreateSuccessResult(
                        new PdfOutputResultData
                        {
                            Zipped = zipped,
                            Configuration = Configuration,
                            UncompressOutputStream = safeOutAsByteArray.ToMemoryStream()
                        });
            }
            catch (Exception ex)
            {
                return OutputResult.FromException(ex);
            }
        }

        #endregion

        #region public override methods

        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() =>
            $"Count={Items.Count()}";

        #endregion

        #region protected virtual methods

        /// <summary>
        /// Cleans managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// If it is <b>true</b>, the method is invoked directly or indirectly from the user code.
        /// If it is <b>false</b>, the method is called the finalizer and only unmanaged resources are finalized.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // free managed resources
            if (disposing)
            {
                foreach (var item in Items)
                {
                    switch (item.InputType)
                    {
                        case KnownInputType.Stream:
                            ((Stream)item.Input)?.Dispose();
                            break;

                        case KnownInputType.ByteArray:
                            item.Input = null;
                            break;

                        case KnownInputType.Filename:
                            item.Input = null;
                            break;

                        case KnownInputType.NotSupported:
                            // nothing to do
                            break;
                    }
                }

                Items = null;
            }

            // free native resources

            // avoid seconds calls 
            _isDisposed = true;
        }

        #endregion

#if NETCOREAPP3_1 || NETSTANDARD2_1 || NET5_0_OR_GREATER

        #region protected virtual async methods

        /// <summary>
        /// Cleans managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// If it is <b>true</b>, the method is invoked directly or indirectly from the user code.
        /// If it is <b>false</b>, the method is called the finalizer and only unmanaged resources are finalized.
        /// </param>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // free managed resources
            if (disposing)
            {
                foreach (var item in Items)
                {
                    switch (item.InputType)
                    {
                        case KnownInputType.Stream:

                            var inputAsStream = (Stream)item.Input;
                            if (inputAsStream != null)
                            {
                                await inputAsStream.DisposeAsync();
                            }
                            break;

                        case KnownInputType.ByteArray:
                            item.Input = null;
                            break;

                        case KnownInputType.Filename:
                            item.Input = null;
                            break;

                        case KnownInputType.NotSupported:
                            // nothing to do
                            break;
                    }
                }

                Items = null;
            }

            // free native resources

            // avoid seconds calls 
            _isDisposed = true;
        }

        #endregion

#endif
    }
}
