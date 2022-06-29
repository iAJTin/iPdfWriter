
namespace iTin.Utilities.Pdf.Design.Table
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using iTextSharp.text.pdf;
    using iTextSharp.tool.xml;
    using iTextSharp.tool.xml.css;
    using iTextSharp.tool.xml.html;
    using iTextSharp.tool.xml.parser;
    using iTextSharp.tool.xml.pipeline.css;
    using iTextSharp.tool.xml.pipeline.end;
    using iTextSharp.tool.xml.pipeline.html;

    using iTin.Core;
    using iTin.Logging;

    /// <summary>
    /// Defines a <b>pdf</b> table object.
    /// </summary>
    public sealed class PdfTable
    {
        #region constructor/s

        #region [private] PdfTable(PdfPTable, PdfTableConfig = null): Initializes a new instance of the class with a native pdf table reference.
        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTable"/> class with a native pdf table reference.
        /// </summary>
        /// <param name="reference">A reference to native pdf table.</param>
        /// <param name="configuration">Table configuration reference.</param>
        private PdfTable(PdfPTable reference, PdfTableConfig configuration = null)
        {
            Logger.Instance.Debug("");
            Logger.Instance.Debug($" Assembly: {typeof(PdfTable).Assembly.GetName().Name}, v{typeof(PdfTable).Assembly.GetName().Version}, Namespace: {typeof(PdfTable).Namespace}, Class: {nameof(PdfTable)}");
            Logger.Instance.Debug($" Initializes a new instance of the {typeof(PdfTable)} class");
            Logger.Instance.Debug($" > Signature: #ctor({typeof(PdfPTable)}, {typeof(PdfTableConfig)} = null)");

            var safeConfiguration = configuration;
            if (configuration == null)
            {
                safeConfiguration = PdfTableConfig.Default;
            }

            Table = reference;
            Configuration = safeConfiguration.Clone();

            Logger.Instance.Debug($"   -> Table: {Table}");
            Logger.Instance.Debug($"   -> Configuration: {Configuration}");
        }
        #endregion

        #endregion

        #region public readonly properties

        #region [public] (PdfTableConfig) Configuration: Gets a reference to table configuration information
        /// <summary>
        /// Gets a reference to table configuration information.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableConfig"/> that contains the configuration information.
        /// </value>
        public PdfTableConfig Configuration { get; }
        #endregion

        #region [public] (PdfPTable) Table: Gets or sets a reference to a native pdf table.
        /// <summary>
        /// Gets or sets a reference to a native pdf table.
        /// </summary>
        /// <value>
        /// A <see cref="PdfPTable"/> object that contains a native pdf table.
        /// </value>
        public PdfPTable Table { get; }
        #endregion

        #endregion

        #region public static methods

        #region [public] {static} (PdfTable) CreateFromHtml(string, string = default, PdfTableConfig = null): Creates a PdfTable object from specified HTML code, HTML5 tags not supported.
        /// <summary>
        /// Creates a <see cref="PdfTable"/> object from specified <b>HTML</b> code, <b>HTML5 tags not supported</b> 
        /// </summary>
        /// <param name="html">A reference to input html code to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from <b>HTML</b>.
        /// </returns>
        public static PdfTable CreateFromHtml(string html, string css = default, PdfTableConfig config = null)
        {
            var hasCss = !string.IsNullOrEmpty(css);
            if (!hasCss)
            {
                css = " ";
            }

            // css
            var cssResolver = new StyleAttrCSSResolver();
            var cssFile = XMLWorkerHelper.GetCSS(css.AsStream());
            cssResolver.AddCss(cssFile);

            // html
            var fontProvider = new XMLWorkerFontProvider(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts)));
            var cssAppliers = new CssAppliersImpl(fontProvider);
            var htmlContext = new HtmlPipelineContext(cssAppliers);
            htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
            
            // pipelines
            var elements = new ElementList();
            var pdf = new ElementHandlerPipeline(elements, null);
            var htmlPipeline = new HtmlPipeline(htmlContext, pdf);
            var cssPipeline = new CssResolverPipeline(cssResolver, htmlPipeline);

            // XML Worker
            var worker = new XMLWorker(cssPipeline, true);
            var parser = new XMLParser(worker, Encoding.UTF8);
            parser.Parse(html.AsStream(Encoding.UTF8), true);

            var nativeTable = elements.OfType<PdfPTable>().FirstOrDefault();

            return new PdfTable(nativeTable, config);
        }
        #endregion

        #endregion
    }
}
