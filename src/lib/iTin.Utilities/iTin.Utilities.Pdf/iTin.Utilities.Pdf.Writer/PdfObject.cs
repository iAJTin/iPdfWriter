
namespace iTin.Utilities.Pdf.Writer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using iTextSharp.text;
    using iTextSharp.text.pdf;

    using iTin.Core;
    using iTin.Core.Helpers;

    using iTin.Logging;

    using iTin.Utilities.Pdf.Writer.ComponentModel;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output;

    /// <summary>
    /// Represents a generic pdf object, this allows add pdf files to <see cref="PdfObject.Items"/> property and specify a user custom configuration.
    /// </summary>
    public class PdfObject : IDisposable
    {
        #region private field members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;

        #endregion

        #region constructor/s

        #region [public] PdfObject(): Initializes a new instance of the class with default configuration
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfObject"/> class with default configuration.
        /// </summary>
        public PdfObject() : this(PdfObjectConfig.Default)
        {            
        }
        #endregion

        #region [public] PdfObject(PdfObjectConfig): Initializes a new instance of the class with specified configuration
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfObject"/> class with specified configuration
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public PdfObject(PdfObjectConfig configuration)
        {
            Logger.Instance.Debug("External Call");
            Logger.Instance.Info("  Initializes a new instance of the PdfObject class with specified configuration");
            Logger.Instance.Info("  > Library: iTin.Utilities.Pdf");
            Logger.Instance.Info("  > Class: PdfObject");
            Logger.Instance.Info("  > Method: ctor(PdfObjectConfig)");

            Configuration = configuration;
            Items = new List<PdfInput>();
        }
        #endregion

        #endregion

        #region finalizer

        #region [~] PdfObject(): Finalizer
        /// <summary>
        /// Finalizer
        /// </summary>
        ~PdfObject()
        {
            Dispose(false);
        }
        #endregion

        #endregion

        #region interfaces

        #region IDisposable

        #region public methods

        #region [public] (void) Dispose(): Clean managed resources
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

        #endregion

        #region public properties

        #region [public] (IEnumerable<PdfInput>) Items: Gets or sets the pdf input list
        /// <summary>
        /// Gets or sets the pdf input list.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<PdfInput> Items { get; set; }
        #endregion

        #region [public] (PdfObjectConfig) Configuration: Gets the configuration settings
        /// <summary>
        /// Gets the configuration settings.
        /// </summary>
        /// <value>
        /// The object configuration.
        /// </value>
        public PdfObjectConfig Configuration { get; }
        #endregion

        #endregion

        #region public methods

        #region [public] (OutputResult) TryMergeInputs(): Merges all PdfInput entries into a new Stream
        /// <summary>
        /// Merges all <see cref="PdfInput"/> entries.
        /// </summary>
        /// <returns>
        /// <para>
        /// A <see cref="OutputResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Value</b> property will contain the value; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return value is <see cref="OutputResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public OutputResult TryMergeInputs()
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug(" Assembly: iTin.Utilities.Pdf.Writer, Namespace: iTin.Utilities.Pdf.Writer, Class: PdfObject");
            Logger.Instance.Debug($" Merges all {typeof(PdfInput)} entries into a new {typeof(PdfObject)}");
            Logger.Instance.Debug($" > Signature: ({typeof(OutputResult)}) TryMergeInputs()");

            var items = Items.ToList();
            if (Configuration.UseIndex)
            {
                items = items.OrderBy(i => i.Index).ToList();
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

                        foreach (var item in items)
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

                bool hasSystemTags = false;
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
                        rawMergedResult = pdfRawMerged.Replace(new ReplaceSystemTag(tag.BuildReplacementObject()));
                    }
                    
                    if (rawMergedResult.Success)
                    {
                        outStream = new MemoryStream((byte[])rawMergedResult.Value.OutputStream.AsByteArray().Clone());
                    }
                }

                bool hasGlobalReplacements = false;
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
                        rawMergedWithTagsResult = pdfRawMergedWithTags.Replace(new ReplaceText(replacement));
                    }

                    if (rawMergedWithTagsResult.Success)
                    {
                        if (rawMergedWithTagsResult.Value.OutputStream.Position != 0)
                        {
                            rawMergedWithTagsResult.Value.OutputStream.Position = 0;
                        }

                        outStream = new MemoryStream((byte[])rawMergedWithTagsResult.Value.OutputStream.AsByteArray().Clone());
                    }
                }

                if (Configuration.DeletePhysicalFilesAfterMerge)
                {
                    foreach (var item in items)
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
                        new OutputResultData
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

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a string than represents the current object
        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString() => $"Count={Items.Count()}";
        #endregion

        #endregion

        #region protected virtual methods

        #region [protected] {virtual} (void) Dispose(bool): Cleans managed and unmanaged resources
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

        #endregion
    }
}
