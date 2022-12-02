
using iTin.Logging.ComponentModel;

namespace iPdfWriter.ConsoleApp.Code
{
    /// <summary>
    /// Show how to extract text lines from a pdf document asynchronously.
    /// </summary>
    internal static class Sample24
    {
        public static async Task GenerateAsync(ILogger logger, CancellationToken cancellationToken = default)
        {
            // Creates pdf file reference
            var doc = new PdfInput
            {
                Input = "~/Resources/Sample-24/file-sample.pdf"
            };

            // Extract text lines (Remove empty lines)
            try
            {
                var textLines = await doc.TextLinesAsync(cancellationToken: cancellationToken);

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
                var textLines = await doc.TextLinesAsync(removeEmptyLines: false, cancellationToken: cancellationToken);

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
                var textLines = await doc.TextLinesAsync(line => line.Text.Trim() == "#TITLE#", cancellationToken);

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
