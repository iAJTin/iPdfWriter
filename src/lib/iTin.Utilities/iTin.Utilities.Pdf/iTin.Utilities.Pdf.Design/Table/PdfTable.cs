
using System.Linq;

namespace iTin.Utilities.Pdf.Design.Table
{
    using System;
    using System.IO;
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
            var safeConfiguration = configuration;
            if (configuration == null)
            {
                safeConfiguration = PdfTableConfig.Default;
            }

            Table = reference;
            Configuration = safeConfiguration.Clone();
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

        #region [public] {static} (PdfTable) CreateFromHtml(string, string = default, PdfTableConfig = null): Creates a PdfTable object from specified HTML code.
        /// <summary>
        /// Creates a <see cref="PdfTable"/> object from specified <b>HTML</b> code.
        /// </summary>
        /// <param name="html">A reference to input html code to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> that contains a 
        /// </returns>
        public static PdfTable CreateFromHtml(string html, string css = default, PdfTableConfig config = null)
        {
            var hasCss = !string.IsNullOrEmpty(css);
            if (!hasCss)
            {
                css = " ";
            }

            // css
            StyleAttrCSSResolver cssResolver = new StyleAttrCSSResolver();
            ICssFile cssFile = XMLWorkerHelper.GetCSS(css.AsStream());
            cssResolver.AddCss(cssFile);

            // html
            XMLWorkerFontProvider fontProvider = new XMLWorkerFontProvider(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts)));
            CssAppliers cssAppliers = new CssAppliersImpl(fontProvider);
            HtmlPipelineContext htmlContext = new HtmlPipelineContext(cssAppliers);
            htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
            
            // pipelines
            ElementList elements = new ElementList();
            ElementHandlerPipeline pdf = new ElementHandlerPipeline(elements, null);
            HtmlPipeline htmlPipeline = new HtmlPipeline(htmlContext, pdf);
            CssResolverPipeline cssPipeline = new CssResolverPipeline(cssResolver, htmlPipeline);

            // XML Worker
            XMLWorker worker = new XMLWorker(cssPipeline, true);
            XMLParser parser = new XMLParser(worker, Encoding.UTF8);
            parser.Parse(html.AsStream(Encoding.UTF8), true);

            PdfPTable nativeTable = elements.OfType<PdfPTable>().FirstOrDefault();

            return new PdfTable(nativeTable, config);
        }
        #endregion

        #endregion
    }
}
