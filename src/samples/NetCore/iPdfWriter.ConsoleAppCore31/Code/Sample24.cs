
using System.Linq;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Writer;

namespace iPdfWriter.Code
{
    /// <summary>
    /// Show how to extract text lines from a pdf document
    /// </summary>
    internal static class Sample24
    {
        public static void Generate(ILogger logger)
        {
            // Creates pdf file reference
            var doc = new PdfInput
            {
                Input = "~/Resources/Sample-24/file-sample.pdf"
            };

            // Extract text lines (Remove empty lines)
            try
            {
                var textLines = doc.TextLines();

                logger.Info("   > Document lines (Remove empty lines)");
                logger.Info($"     > Count: {textLines.Count()}");
            }
            catch
            {
                logger.Info("   > Error while extract text lines");
            }

            // Extract text lines (Include empty lines)
            try
            {
                var textLines = doc.TextLines(removeEmptyLines: false);

                logger.Info("   > Document lines (Include empty lines)");
                logger.Info($"     > Count: {textLines.Count()}");
            }
            catch
            {
                logger.Info("   > Error while extract text lines");
            }

            // Extract text lines (predicate)
            try
            {
                var textLines = doc.TextLines(line => line.Text.Trim() == "#TITLE#");

                logger.Info("   > Document lines (predicate)");
                logger.Info($"     > Count: {textLines.Count()}");
            }
            catch
            {
                logger.Info("   > Error while extract text lines");
            }
        }
    }
}
