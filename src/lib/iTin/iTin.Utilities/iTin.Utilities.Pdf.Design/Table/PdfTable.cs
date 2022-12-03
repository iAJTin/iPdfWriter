﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTin.Core;
using iTin.Core.Helpers;

using iTin.Utilities.Pdf.Design.Styles;

using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;

namespace iTin.Utilities.Pdf.Design.Table
{
    /// <summary>
    /// Defines a <b>pdf</b> table object.
    /// </summary>
    public sealed class PdfTable
    {
        #region constructor/s

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

        #region public readonly properties

        /// <summary>
        /// Gets a reference to table configuration information.
        /// </summary>
        /// <value>
        /// A <see cref="PdfTableConfig"/> that contains the configuration information.
        /// </value>
        public PdfTableConfig Configuration { get; }

        /// <summary>
        /// Gets or sets a reference to a native pdf table.
        /// </summary>
        /// <value>
        /// A <see cref="PdfPTable"/> object that contains a native pdf table.
        /// </value>
        public PdfPTable Table { get; }

        #endregion

        #region public static methods

        /// <summary>
        /// Creates a <see cref="PdfTable"/> object from specified typed enumerable. Render the table as a native <strong>PDF</strong> table by using custom styles.
        /// </summary>
        /// <param name="data">A reference to input typed enumerable to convert.</param>
        /// <param name="styles">A reference to styles to use.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified typed enumerable.
        /// </returns>
        public static PdfTable CreateFromEnumerable<TI>(IEnumerable<TI> data, PdfTextStyle styles, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return CreateFromDataTable(data.ToDataTable<TI>(nameof(data)), styles, config);
        }

        /// <summary>
        /// Creates a <see cref="PdfTable"/> object from specified typed enumerable. Render the table as an <strong>HTML</strong> table element using <strong>css</strong> styles.
        /// </summary>
        /// <param name="data">A reference to input typed enumerable to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified typed enumerable.
        /// </returns>
        public static PdfTable CreateFromEnumerable<TI>(IEnumerable<TI> data, string css = null, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return CreateFromDataTable(data.ToDataTable<TI>(nameof(data)), css, config);
        }

        /// <summary>
        /// Creates a <see cref="PdfTable"/> object from specified <see cref="T:System.Data.DataTable"/>. Render the table as a native <strong>PDF</strong> table by using custom styles.
        /// </summary>
        /// <param name="data">A reference to input <see cref="T:System.Data.DataTable"/> to convert.</param>
        /// <param name="styles">A reference to styles to use.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static PdfTable CreateFromDataTable(DataTable data, PdfTextStyle styles, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return CreateFromHtml(data.ToHtmlTable(), css: default, config: config);
        }

        /// <summary>
        /// Creates a <see cref="PdfTable"/> object from specified <see cref="T:System.Data.DataTable"/>. Render the table as an <strong>HTML</strong> table element using <strong>css</strong> styles.
        /// </summary>
        /// <param name="data">A reference to input <see cref="T:System.Data.DataTable"/> to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static PdfTable CreateFromDataTable(DataTable data, string css = null, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return CreateFromHtml(data.ToHtmlTable(), css: css, config: config);
        }

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
                css = @"
                        table { 
                            border-spacing: 0px;
                            border-collapse: collapse;  
                        }

                        tr {
                            font-size: 9pt;
                            font-family: Arial; 
                            color: #000000;
                            text-align: left;
                            overflow: hidden;
                        }

                        td {
                            padding: 4px;
                        }";
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

        #region public static async methods

        /// <summary>
        /// Creates asynchronously a <see cref="PdfTable"/> object from specified typed enumerable. Render the table as a native <strong>PDF</strong> table by using custom styles.
        /// </summary>
        /// <param name="data">A reference to input typed enumerable to convert.</param>
        /// <param name="styles">A reference to styles to use.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified typed enumerable.
        /// </returns>
        public static async Task<PdfTable> CreateFromEnumerableAsync<TI>(IEnumerable<TI> data, PdfTextStyle styles, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return await CreateFromDataTableAsync(data.ToDataTable<TI>(nameof(data)), styles, config);
        }

        /// <summary>
        /// Creates asynchronously a <see cref="PdfTable"/> object from specified typed enumerable. Render the table as an <strong>HTML</strong> table element using <strong>css</strong> styles.
        /// </summary>
        /// <param name="data">A reference to input typed enumerable to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified typed enumerable.
        /// </returns>
        public static async Task<PdfTable> CreateFromEnumerableAsync<TI>(IEnumerable<TI> data, string css = null, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return await CreateFromDataTableAsync(data.ToDataTable<TI>(nameof(data)), css, config);
        }

        /// <summary>
        /// Creates asynchronously a <see cref="PdfTable"/> object from specified <see cref="T:System.Data.DataTable"/>. Render the table as a native <strong>PDF</strong> table by using custom styles.
        /// </summary>
        /// <param name="data">A reference to input <see cref="T:System.Data.DataTable"/> to convert.</param>
        /// <param name="styles">A reference to styles to use.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static async Task<PdfTable> CreateFromDataTableAsync(DataTable data, PdfTextStyle styles, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return await CreateFromHtmlAsync(data.ToHtmlTable(), css: default, config: config);
        }

        /// <summary>
        /// Creates asynchronously a <see cref="PdfTable"/> object from specified <see cref="T:System.Data.DataTable"/>. Render the table as an <strong>HTML</strong> table element using <strong>css</strong> styles.
        /// </summary>
        /// <param name="data">A reference to input <see cref="T:System.Data.DataTable"/> to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from specified <see cref="T:System.Data.DataTable"/>.
        /// </returns>
        public static async Task<PdfTable> CreateFromDataTableAsync(DataTable data, string css = null, PdfTableConfig config = null)
        {
            SentinelHelper.ArgumentNull(data, nameof(data));

            return await CreateFromHtmlAsync(data.ToHtmlTable(), css: css, config: config);
        }

        /// <summary>
        /// Creates asynchronously a <see cref="PdfTable"/> object from specified <b>HTML</b> code, <b>HTML5 tags not supported</b> 
        /// </summary>
        /// <param name="html">A reference to input html code to convert.</param>
        /// <param name="css">A reference to css styles to apply.</param>
        /// <param name="config">Table configuration reference.</param>
        /// <returns>
        /// A new <see cref="PdfPTable"/> instance that contains the table from <b>HTML</b>.
        /// </returns>
        public static async Task<PdfTable> CreateFromHtmlAsync(string html, string css = default, PdfTableConfig config = null)
        {
            var hasCss = !string.IsNullOrEmpty(css);
            if (!hasCss)
            {
                css = @"
                        table { 
                            border-spacing: 0px;
                            border-collapse: collapse;  
                        }

                        tr {
                            font-size: 9pt;
                            font-family: Arial; 
                            color: #000000;
                            text-align: left;
                            overflow: hidden;
                        }

                        td {
                            padding: 4px;
                        }";
            }

            // css
            var cssResolver = new StyleAttrCSSResolver();
            var cssFile = XMLWorkerHelper.GetCSS(await css.AsStreamAsync());
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
            parser.Parse(await html.AsStreamAsync(Encoding.UTF8), true);

            var nativeTable = elements.OfType<PdfPTable>().FirstOrDefault();

            return new PdfTable(nativeTable, config);
        }

        #endregion
    }
}