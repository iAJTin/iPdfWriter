
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Writer;
using iTin.Utilities.Pdf.Writer.Operations.Result.Actions;

namespace iPdfWriter.Code
{
    /// <summary>
    /// Show how to extract pages from a pdf document asynchronously.
    /// </summary>
    internal static class Sample22
    {
        public static async Task GenerateAsync(ILogger logger, CancellationToken cancellationToken = default)
        {
            #region Initialize timer

            var sw = new Stopwatch();
            sw.Start();

            #endregion

            #region Creates pdf file reference

            var doc = new PdfInput
            {
                Input = "~/Resources/Sample-22/file-sample.pdf"
            };

            #endregion

            #region Extract pages

            var partialInput = await doc.ExtractPagesAsync(1, 2, cancellationToken);

            #endregion

            #region Create output result

            var result = await partialInput.CreateResultAsync(cancellationToken: cancellationToken);
            if (!result.Success)
            {
                logger.Info("   > Error creating output result");
                logger.Info($"     > Error: {result.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region Saves output result

            var saveResult = await result.Result.Action(new SaveToFileAsync { OutputPath = "~/Output/Sample22/Sample-22" }, cancellationToken);
            
            var ts = sw.Elapsed;
            sw.Stop();

            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample22/Sample-22.pdf");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
