
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using iTin.Core;
using iTin.Core.ComponentModel;
using iTin.Core.ComponentModel.Results;
using iTin.Core.Helpers;
using iTin.Core.IO;

using iTin.Utilities.Pdf.Design.Text;
using iTin.Utilities.Pdf.Writer.ComponentModel;
using iTin.Utilities.Pdf.Writer.ComponentModel.Input;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Insert;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Output;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Replace;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Set;

using iTextSharp.text;

using iTinIO = iTin.Core.IO;
using NativeIO = System.IO;
using NativePdf = iTextSharp.text.pdf;
using NativePdfParser = iTextSharp.text.pdf.parser;

namespace iTin.Utilities.Pdf.Writer
{
    /// <summary>
    /// Represents a pdf file.
    /// </summary>
    /// <seealso cref="IInput"/>
    /// <seealso cref="ICloneable"/>
    public sealed class PdfInput : IInput, ICloneable
    {
        #region private constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string PdfExtension = "pdf";
        #endregion

        #region constructor/s

        #region [public] PdfInput(): Initializes a new instance of the class
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfInput"/> class.
        /// </summary>
        public PdfInput()
        {
            AutoUpdateChanges = false;
            DeletePhysicalFilesAfterMerge = true;
        }
        #endregion

        #endregion

        #region interfaces

        #region ICloneable

        #region private methods

        #region [private] (object) ICloneable.Clone(): Create a new object that is a copy of the current instance
        /// <inheritdoc/>
        /// <summary>
        /// Create a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="object"/> that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone() => Clone();
        #endregion

        #endregion

        #endregion

        #region IInput

        #region public properties

        #region [public] (bool) AutoUpdateChanges: Gets or sets a Result indicating whether automatic updates for changes
        /// <summary>
        /// Gets or sets a Result indicating whether automatic updates for changes.
        /// </summary>
        /// <Result>
        /// <b>true</b> if automatic update changes; otherwise, <b>false</b>.
        /// </Result>
        public bool AutoUpdateChanges { get; set; }
        #endregion

        #region [public] (bool) DeletePhysicalFilesAfterMerge: Gets or sets a Result indicating whether delete physical files after merge
        /// <summary>
        /// Gets or sets a Result indicating whether delete physical files after merge.
        /// </summary>
        /// <Result>
        /// <b>true</b> if delete physical files after merge; otherwise, <b>false</b>.
        /// </Result>
        public bool DeletePhysicalFilesAfterMerge { get; set; }
        #endregion

        #region [public] (int) Index: Gets or sets a Result that contains input index
        /// <summary>
        /// Gets or sets a Result that contains input index.
        /// </summary>
        /// <Result>
        /// A <see cref="int"/> that contains input index.
        /// </Result>
        public int Index { get; set; }
        #endregion

        #region [public] (object) Input: Gets or sets the input object
        /// <summary>
        /// Gets or sets the input object.
        /// </summary>
        /// <Result>
        /// The input.
        /// </Result>
        public object Input { get; set; }
        #endregion

        #region [public] (KnownInputType) InputType: Gets the input type
        /// <summary>
        /// Gets input type.
        /// </summary>
        /// <Result>
        /// An Result of enumeration <see cref="KnownInputType"/> indicating type of the input.
        /// </Result>
        public KnownInputType InputType
        {
            get
            {
                Type inputType = Input.GetType();

                if (inputType == typeof(string))
                {
                    return KnownInputType.Filename;
                }

                if (inputType == typeof(PdfInput))
                {
                    return KnownInputType.PdfInput;
                }

                if (inputType == typeof(NativeIO.MemoryStream) || inputType == typeof(NativeIO.Stream))
                {
                    return KnownInputType.Stream;
                }

                if (inputType == typeof(byte[]))
                {
                    return KnownInputType.ByteArray;
                }

                return KnownInputType.NotSupported;

            }
        }
        #endregion

        #endregion

        #region public methods

        #region [public] (OutputResult) CreateResult(OutputResultConfig = null): Returns a new reference OutputResult that complies with what is indicated in its configuration object. By default, this PdfInput will not be zipped
        /// <summary>
        /// Returns a new reference <see cref="OutputResult"/> that complies with what is indicated in its configuration object. By default, this <see cref="PdfInput"/> will not be zipped.
        /// </summary>
        /// <param name="config">The output result configuration.</param>
        /// <returns>
        /// <para>
        /// A <see cref="OutputResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="OutputResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public OutputResult CreateResult(OutputResultConfig config = null)
        {
            ProcessInput();

            var configToApply = OutputResultConfig.Default;
            if (config != null)
            {
                configToApply = config;
                configToApply.Filename = NativeIO.Path.ChangeExtension(
                    string.IsNullOrEmpty(config.Filename)
                        ? iTinIO.File.GetUniqueTempRandomFile().Segments.LastOrDefault()
                        : config.Filename,
                    PdfExtension);
            }

            try
            {
                if (!configToApply.Zipped)
                {
                    return OutputResult.CreateSuccessResult(
                        new OutputResultData
                        {
                            Zipped = false,
                            Configuration = configToApply,
                            UncompressOutputStream = Clone().ToStream()
                        });
                }

                OutputResult zippedOutputResult = new[] { Clone() }.CreateJoinResult(new[] { configToApply.Filename });
                zippedOutputResult.Result.Configuration = configToApply;

                return zippedOutputResult;
            }
            catch (Exception e)
            {
                return OutputResult.FromException(e);
            }
        }
        #endregion

        #region [public] (InsertResult) Insert(IInsert): Try to insert an element in this input
        /// <summary>
        /// Try to insert an element in this input.
        /// </summary>
        /// <param name="data">Reference to insertable object information</param>
        /// <returns>
        /// <para>
        /// A <see cref="InsertResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="InsertResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public InsertResult Insert(IInsert data)
        {
            InsertResult result = InsertImplStrategy(data, this);

            if (AutoUpdateChanges)
            {
                Input = result.Result.OutputStream;
            }

            return result;
        }
        #endregion

        #region [public] (InputReplaceAction) Replace(IReplace): 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public PdfInputReplaceAction Replace(IReplace data)
        {
            PdfInputCache.Cache.AddTextReplacement(this, data);

            return new PdfInputReplaceAction(this);
        }
        #endregion

        #region [public] (SetResult) Set(ISet): Try to set an element in this input
        /// <summary>
        /// Try to set an element in this input.
        /// </summary>
        /// <param name="data">Reference to seteable object information</param>
        /// <returns>
        /// <para>
        /// A <see cref="SetResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="SetResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public SetResult Set(ISet data)
        {
            SetResult result = SetImplStrategy(data, this);

            if (AutoUpdateChanges)
            {
                Input = result.Result.OutputStream;
            }

            return result;
        }
        #endregion

        #region [public] (IResult) SaveToFile(string, SaveOptions = null): Saves this input into a file
        /// <summary>
        /// Saves this input into a file.
        /// </summary>
        /// <param name="outputPath">The output path. The use of the <b>~</b> character is allowed to indicate relative paths, and you can also use <b>UNC</b> path.</param>
        /// <param name="options">Save options</param>
        /// <returns>
        /// <para>
        /// A <see cref="BooleanResult"/> which implements the <see cref="IResult"/> interface reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="bool"/>, which contains the operation result
        /// </para>
        /// </returns>
        public IResult SaveToFile(string outputPath, SaveOptions options = null)
        {
            try
            {
                return ToStream().SaveToFile(iTinIO.Path.PathResolver(outputPath), options ?? SaveOptions.Default);
            }
            catch (Exception ex)
            {
                return BooleanResult.FromException(ex);
            }
        }
        #endregion

        #region [public] (NativeIO.Stream) ToStream(): Convert this input into a stream object
        /// <summary>
        /// Convert this input into a stream object.
        /// </summary>
        /// <returns>
        /// A <see cref="NativeIO.Stream"/> that represents this input file.
        /// </returns>
        public NativeIO.Stream ToStream()
        {
            switch (InputType)
            {
                case KnownInputType.Filename:
                    return new NativeIO.MemoryStream(NativeIO.File.ReadAllBytes(iTinIO.Path.PathResolver(TypeHelper.ToType<string>(Input))));

                case KnownInputType.ByteArray:
                    return new NativeIO.MemoryStream(TypeHelper.ToType<byte[]>(Input));

                case KnownInputType.PdfInput:
                    return TypeHelper.ToType<PdfInput>(Input).CreateResult().Result.UncompressOutputStream;

                case KnownInputType.Stream:
                    NativeIO.Stream stream = TypeHelper.ToType<NativeIO.Stream>(Input);
                    stream.Position = 0;
                    return stream;

                default:
                case KnownInputType.NotSupported:
                    return null;
            }
        }
        #endregion    

        #endregion

        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (PdfInput) CreateFromHtml(string, string = null, Encoding = null): Creates a new PdfInput object from HTML code
        /// <summary>
        /// Creates a new <see cref="PdfInput"/> object from <b>HTML</b> code.
        /// </summary>
        /// <param name="html">HTML code to convert</param>
        /// <param name="css">CSS code to use with HTML code</param>
        /// <param name="encoding">Encoding to use</param>
        /// <returns>
        /// A new <see cref="PdfInput"/> object which contains the HTML code.
        /// </returns>
        public static PdfInput CreateFromHtml(string html, string css = null, Encoding encoding = null)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            byte[] bytes;
            using var ms = new NativeIO.MemoryStream();
            using var document = new Document();
            using var writer = NativePdf.PdfWriter.GetInstance(document, ms);
            document.Open();

            if (css == null)
            {
                using var htmlStream = new NativeIO.MemoryStream(Encoding.Unicode.GetBytes(html));
                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, encoding ?? Encoding.Unicode);
            }
            else
            {
                using var cssStream = new NativeIO.MemoryStream(Encoding.UTF8.GetBytes(css));
                using var htmlStream = new NativeIO.MemoryStream(Encoding.UTF8.GetBytes(html));
                if (encoding == null)
                {
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, cssStream);
                }
                else
                {
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, encoding);
                }
            }

            document.Close();

            bytes = ms.ToArray();

            return new PdfInput { Input = bytes };
        }
        #endregion

        #endregion

        #region public methods

        #region [public] (PdfInput) Clone(): Create a new object that is a copy of the current instance
        /// <summary>
        /// Create a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new <see cref="PdfInput"/> that is a copy of this instance.
        /// </returns>
        public PdfInput Clone()
        {
            PdfInput clonned = (PdfInput)MemberwiseClone();

            NativeIO.Stream innerStream = ToStream().Clone();
            clonned.Input = innerStream;

            return clonned;
        }
        #endregion

        #region [public] (PdfInput) ExtractPages(int, int? = null): Create a new PdfInput containing the selected pages
        /// <summary>
        /// Create a new <see cref="PdfInput"/> containing the selected pages.
        /// </summary>
        /// <param name="from">Start page</param>
        /// <param name="to">End page. If is <see langword="null"/> the last page will be used</param>
        /// <returns>
        /// A new instance of <see cref="PdfInput"/> containing a document containing the specified pages.
        /// </returns>
        /// <exception cref="ArgumentException">If document has no pages</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="from"/> is less than one or is greater than the total number of pages of the document</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="to"/> is less than one or is greater than the total number of pages of the document</exception>
        public PdfInput ExtractPages(int from, int? to = null)
        {
            using var reader = new NativePdf.PdfReader(this.ToStream());
            using var source = new Document(reader.GetPageSizeWithRotation(from));
            using var target = new NativeIO.MemoryStream();
            using var pdfCopyProvider = new NativePdf.PdfCopy(source, target);
            source.Open();

            var pages = reader.NumberOfPages;
            if (pages == 0)
            {
                throw new ArgumentException("Document has not pages");
            }
            
            SentinelHelper.ArgumentOutOfRange(nameof(from), from, 1, pages);

            var safeTo = to;
            if (to.HasValue)
            {
                SentinelHelper.ArgumentOutOfRange(nameof(to), to.Value, 1, pages);
            }
            else
            {
                safeTo = pages;
            }

            for (var i = from; i <= safeTo; i++)
            {
                var importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                pdfCopyProvider.AddPage(importedPage);
            }

            source.Close();
            reader.Close();

            return new PdfInput
            {
                Input = target.ToArray()
            };
        }
        #endregion

        #region [public] (int) NumberOfPages(): Returns total pages of this PdfInput
        /// <summary>
        /// Returns total pages of this <see cref="PdfInput"/>.
        /// </summary>
        /// <returns>
        /// Total pages of this <see cref="PdfInput"/>.
        /// </returns>
        public int NumberOfPages()
        {
            using var reader = new NativePdf.PdfReader(ToStream());

            return reader.NumberOfPages;
        }
        #endregion

        #region [public] (IEnumerable<PdfText>) SearchText(string): Search specified text into this input file
        /// <summary>
        /// Search specified text into this input file.
        /// </summary>
        /// <param name="text">Text to search.</param>
        /// <returns>
        /// A <see cref="PdfText"/> list of matches. 
        /// </returns>
        public IEnumerable<PdfText> SearchText(string text)
        {
            var matchs = new List<PdfText>();

            var pdfReader = new NativePdf.PdfReader(ToStream());
            var count = pdfReader.NumberOfPages;
            for (var page = 1; page <= count; page++)
            {
                NativePdfParser.ITextExtractionStrategy strategy = new NativePdfParser.SimpleTextExtractionStrategy();
                var currentText = NativePdfParser.PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                var absolutePosition = currentText.IndexOf(text, StringComparison.OrdinalIgnoreCase);
                if (absolutePosition != -1)
                {
                    matchs.Add(new PdfText(text, page, absolutePosition));
                }
            }

            pdfReader.Close();
            
            return matchs;
        }
        #endregion

        #region [public] (IEnumerable<PdfTextLine>) TextLines(int? = null, int? = null, bool = true): Gets the lines of text for this PdfInput
        /// <summary>
        /// Gets the lines of text for this <see cref="PdfInput"/>, optionally you can set both the start and end pages and a value indicating whether blank lines are included in the result.
        /// </summary>
        /// <param name="fromPage">Defines start page. If a value is not set, it will default to 1</param>
        /// <param name="toPage">Defines end page. If a value is not set, it will default to total document pages</param>
        /// <param name="removeEmptyLines">Indicates whether blank lines are included in the result. By default they are not included</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If document has no pages</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="fromPage"/> is less than one or is greater than the total number of pages of the document</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="toPage"/> is less than one or is greater than the total number of pages of the document</exception>
        public IEnumerable<PdfTextLine> TextLines(int? fromPage = null, int? toPage = null, bool removeEmptyLines = true)
        {
            var result = new List<PdfTextLine>();

            using var reader = new NativePdf.PdfReader(ToStream());
            using var stamper = new NativePdf.PdfStamper(reader, new NativeIO.MemoryStream());
            var pages = reader.NumberOfPages;

            if (pages == 0)
            {
                throw new ArgumentException("Document has not pages");
            }

            var safeFrom = fromPage;
            if (fromPage.HasValue)
            {
                SentinelHelper.ArgumentOutOfRange(nameof(fromPage), fromPage.Value, 1, pages);
            }
            else
            {
                safeFrom = 1;
            }

            var safeTo = toPage;
            if (toPage.HasValue)
            {
                SentinelHelper.ArgumentOutOfRange(nameof(toPage), toPage.Value, 1, pages);
            }
            else
            {
                safeTo = pages;
            }

            for (var page = safeFrom; page <= safeTo; page++)
            {
                var currentPage = page.Value;
                var strategy = new LocationTextExtractionStrategy();
                var cb = stamper.GetOverContent(currentPage);
                
                // Send some data contained in PdfContentByte, looks like the first is always cero for me and the second 100, 
                // but i'm not sure if this could change in some cases.
                strategy.UndercontentCharacterSpacing = cb.CharacterSpacing;
                strategy.UndercontentHorizontalScaling = cb.HorizontalScaling;

                // It's not really needed to get the text back, but we have to call this line ALWAYS,
                // because it triggers the process that will get all chunks from PDF into our strategy Object
                var pageLines = 
                    NativePdfParser.PdfTextExtractor.GetTextFromPage(reader, currentPage, strategy)
                    .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(text => new PdfTextLine(text, currentPage));

                if (removeEmptyLines)
                {
                    pageLines = pageLines.Where(textLine => textLine.Text != " ");
                }

                result.AddRange(pageLines);
            }

            return result;
        }

        #endregion

        #region [public] (IEnumerable<PdfTextLine>) TextLines(Func<PdfTextLine, bool>): Gets the lines of text for this PdfInput, filtered values based on a predicate
        /// <summary>
        /// Gets the lines of text for this <see cref="PdfInput"/>, filtered values based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If document has no pages</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="predicate"/> is <see langword="null"/></exception>
        public IEnumerable<PdfTextLine> TextLines(Func<PdfTextLine, bool> predicate)
        {
            SentinelHelper.ArgumentNull(predicate, nameof(predicate));

            var result = new List<PdfTextLine>();

            using var reader = new NativePdf.PdfReader(ToStream());
            using var stamper = new NativePdf.PdfStamper(reader, new NativeIO.MemoryStream());

            var pages = reader.NumberOfPages;
            if (pages == 0)
            {
                throw new ArgumentException("Document has not pages");
            }

            for (var page = 1; page <= pages; page++)
            {
                var currePage = page;
                var strategy = new LocationTextExtractionStrategy();
                var cb = stamper.GetOverContent(currePage);

                // Send some data contained in PdfContentByte, looks like the first is always cero for me and the second 100, 
                // but i'm not sure if this could change in some cases.
                strategy.UndercontentCharacterSpacing = cb.CharacterSpacing;
                strategy.UndercontentHorizontalScaling = cb.HorizontalScaling;

                // It's not really needed to get the text back, but we have to call this line ALWAYS,
                // because it triggers the process that will get all chunks from PDF into our strategy Object
                var pageLines =
                    NativePdfParser.PdfTextExtractor.GetTextFromPage(reader, currePage, strategy)
                    .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(text => new PdfTextLine(text, currePage));
                
                pageLines = pageLines.Where(predicate);

                result.AddRange(pageLines);
            }

            return result;
        }

        #endregion

        #endregion

        #region public override methods

        #region [public] {override} (string) ToString(): Returns a string than represents the current object.
        /// <summary>
        /// Returns a string that represents the current data type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/> than represents the current object.
        /// </returns>
        public override string ToString()
        {
            switch (InputType)
            {
                case KnownInputType.Filename:
                    return $"Index={Index}, Input='{Input}', Type={InputType}, Updatable={AutoUpdateChanges}";

                case KnownInputType.PdfInput:
                    return $"Index={Index}, Input='PdfInput', Type={InputType}, Updatable={AutoUpdateChanges}";

                case KnownInputType.Stream:
                    return $"Index={Index}, Input='Stream', Type={InputType}, Updatable={AutoUpdateChanges}";

                case KnownInputType.ByteArray:
                    return $"Index={Index}, Input='Byte[]', Type={InputType}, Updatable={AutoUpdateChanges}";

                case KnownInputType.NotSupported:
                    return "Input type not supported";

                default:
                    return $"Index={Index}, Type={InputType}, Updatable={AutoUpdateChanges}";
            }
        }
        #endregion

        #endregion

        #region internal methods

        internal ReplaceResult ProcessInput()
        {
            var hasItem = PdfInputCache.Cache.ExistTextReplacementInput(this);
            if (!hasItem)
            {
                var stream = ToStream();

                return ReplaceResult.CreateSuccessResult(new ReplaceResultData
                {
                    Context = this,
                    InputStream = stream,
                    OutputStream = stream
                });
            }

            var result = PdfInputRender.TextReplacementsRender(this);

            if (AutoUpdateChanges)
            {
                Input = result.Result.OutputStream;
            }

            return result;
        }

        #endregion

        #region private methods

        private InsertResult InsertImplStrategy(IInsert data, IInput context)
            => data == null ? InsertResult.CreateErroResult("Missing data") : data.Apply(ToStream(), context);

        private SetResult SetImplStrategy(ISet data, IInput context)
            => data == null ? SetResult.CreateErroResult("Missing data") : data.Apply(ToStream(), context);

        #endregion
    }
}

///// <summary>
///// 
///// </summary>
///// <returns></returns>
//public IEnumerable<System.Drawing.Image> ExtractImages() 
//    => ImageExtractor.ExtractImages(ToStream().AsByteArray());
