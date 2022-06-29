
namespace iTin.Utilities.Pdf.Writer
{
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

    using iTin.Logging;

    using ComponentModel;
    using ComponentModel.Result.Insert;
    using ComponentModel.Result.Output;
    using ComponentModel.Result.Replace;
    using ComponentModel.Result.Set;

    using Design.Text;

    using iTextSharp.text;

    using NativePdf = iTextSharp.text.pdf;
    using NativePdfParser = iTextSharp.text.pdf.parser;

    using NativeIO = System.IO;
    using iTinIO = iTin.Core.IO;

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
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfInput).Assembly.GetName().Name}, v{typeof(PdfInput).Assembly.GetName().Version}, Namespace: {typeof(PdfInput).Namespace}, Class: {nameof(PdfInput)}");
            Logger.Instance.Debug($" Initializes a new instance of the {typeof(PdfInput)} class");
            Logger.Instance.Debug($" > Signature: #ctor()");

            AutoUpdateChanges = false;
            DeletePhysicalFilesAfterMerge = true;

            Logger.Instance.Debug($"   -> AutoUpdateChanges: {AutoUpdateChanges}");
            Logger.Instance.Debug($"   -> DeletePhysicalFilesAfterMerge: {DeletePhysicalFilesAfterMerge}");
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
            OutputResultConfig configToApply = OutputResultConfig.Default;
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
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfInput).Assembly.GetName().Name}, v{typeof(PdfInput).Assembly.GetName().Version}, Namespace: {typeof(PdfInput).Namespace}, Class: {nameof(PdfInput)}");
            Logger.Instance.Debug(" Try to replace an element in this input");
            Logger.Instance.Debug($" > Signature: ({typeof(bool)}) Replace({typeof(IInsert)}, out {typeof(NativeIO.Stream)})");
            Logger.Instance.Debug($"   > data: {data}");

            InsertResult result = InsertImplStrategy(data, this);

            if (AutoUpdateChanges)
            {
                Input = result.Result.OutputStream;
            }

            Logger.Instance.Debug($" > Output: Inserted = {result.Success}");

            return result;
        }
        #endregion

        #region [public] (ReplaceResult) Replace(IReplace): Try to replace an element in this input
        /// <summary>
        /// Try to replace an element in this input.
        /// </summary>
        /// <param name="data">Reference to replacement object information</param>
        /// <returns>
        /// <para>
        /// A <see cref="ReplaceResult"/> reference that contains the result of the operation, to check if the operation is correct, the <b>Success</b>
        /// property will be <b>true</b> and the <b>Result</b> property will contain the Result; Otherwise, the the <b>Success</b> property
        /// will be false and the <b>Errors</b> property will contain the errors associated with the operation, if they have been filled in.
        /// </para>
        /// <para>
        /// The type of the return Result is <see cref="ReplaceResultData"/>, which contains the operation result
        /// </para>
        /// </returns>
        public ReplaceResult Replace(IReplace data)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfInput).Assembly.GetName().Name}, v{typeof(PdfInput).Assembly.GetName().Version}, Namespace: {typeof(PdfInput).Namespace}, Class: {nameof(PdfInput)}");
            Logger.Instance.Debug(" Try to replace an element in this input");
            Logger.Instance.Debug($" > Signature: ({typeof(ReplaceResult)}) Replace({typeof(IReplace)})");
            Logger.Instance.Debug($"   > data: {data}");

            ReplaceResult result = ReplaceImplStrategy(data, this);

            if (AutoUpdateChanges)
            {
                Input = result.Result.OutputStream;
            }

            Logger.Instance.Debug($" > Output: Replacement = {result.Success}");

            return result;
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
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfInput).Assembly.GetName().Name}, v{typeof(PdfInput).Assembly.GetName().Version}, Namespace: {typeof(PdfInput).Namespace}, Class: {nameof(PdfInput)}");
            Logger.Instance.Debug(" Try to set an element in this input");
            Logger.Instance.Debug($" > Signature: ({typeof(SetResult)}) Set({typeof(ISet)})");
            Logger.Instance.Debug($"   > data: {data}");

            SetResult result = SetImplStrategy(data, this);

            if (AutoUpdateChanges)
            {
                Input = result.Result.OutputStream;
            }

            Logger.Instance.Debug($" > Output: Setted = {result.Success}");

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
            byte[] bytes;
            using (var ms = new NativeIO.MemoryStream())
            {
                using (var document = new Document())
                {
                    using (var writer = NativePdf.PdfWriter.GetInstance(document, ms))
                    {
                        document.Open();

                        if (css == null)
                        {
                            using (var htmlStream = new NativeIO.MemoryStream(Encoding.UTF8.GetBytes(html)))
                            {
                                iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, encoding ?? Encoding.UTF8);
                            }
                        }
                        else
                        {
                            using (var cssStream = new NativeIO.MemoryStream(Encoding.UTF8.GetBytes(css)))
                            {
                                using (var htmlStream = new NativeIO.MemoryStream(Encoding.UTF8.GetBytes(html)))
                                {
                                    if (encoding == null)
                                    {
                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, cssStream);
                                    }
                                    else
                                    {
                                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, htmlStream, encoding);
                                    }
                                }
                            }
                        }

                        document.Close();
                    }
                }

                bytes = ms.ToArray();
            }

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
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfInput).Assembly.GetName().Name}, v{typeof(PdfInput).Assembly.GetName().Version}, Namespace: {typeof(PdfInput).Namespace}, Class: {nameof(PdfInput)}");
            Logger.Instance.Debug(" Create a new object that is a copy of the current instance");
            Logger.Instance.Debug($" > Signature: ({typeof(PdfInput)}) Clone()");

            PdfInput clonned = (PdfInput)MemberwiseClone();

            NativeIO.Stream innerStream = ToStream().Clone();
            clonned.Input = innerStream;

            Logger.Instance.Debug($" > Output: Cloned correctly");

            return clonned;
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
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfInput).Assembly.GetName().Name}, v{typeof(PdfInput).Assembly.GetName().Version}, Namespace: {typeof(PdfInput).Namespace}, Class: {nameof(PdfInput)}");
            Logger.Instance.Debug(" Search specified text into this input file");
            Logger.Instance.Debug($" > Signature: ({typeof(IEnumerable<PdfText>)}) SearchText({typeof(string)})");
            Logger.Instance.Debug($"   > text: {text}");

            var matchs = new List<PdfText>();

            try
            {
                NativePdf.PdfReader pdfReader = new NativePdf.PdfReader(ToStream());
                int count = pdfReader.NumberOfPages;
                for (int page = 1; page <= count; page++)
                {
                    NativePdfParser.ITextExtractionStrategy strategy = new NativePdfParser.SimpleTextExtractionStrategy();
                    string currentText = NativePdfParser.PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
                    currentText = Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));

                    var absolutePosition = currentText.IndexOf(text, StringComparison.OrdinalIgnoreCase);
                    if (absolutePosition != -1)
                    {
                        matchs.Add(new PdfText(text, page, absolutePosition));
                    }
                }

                pdfReader.Close();
            }
            catch
            {
                Logger.Instance.Debug($" > Output: Error, no match item(s)");
            }

            Logger.Instance.Debug($" > Output: {matchs.Count} item(s)");

            return matchs;
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

        #region private methods

        private InsertResult InsertImplStrategy(IInsert data, IInput context)
            => data == null ? InsertResult.CreateErroResult("Missing data") : data.Apply(ToStream(), context);

        private ReplaceResult ReplaceImplStrategy(IReplace data, IInput context)
            => data == null ? ReplaceResult.CreateErroResult("Missing data") : data.Apply(ToStream(), context);

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
