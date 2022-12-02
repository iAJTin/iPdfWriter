
using System.Diagnostics;
using System.Drawing;

using iTin.Core.ComponentModel;
using iTin.Core.Models.Design.Enums;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Design.Image;

using iPdfWriter.Config;
using iPdfWriter.Operations.Actions;
using iPdfWriter.Operations.Replace;
using iPdfWriter.Operations.Replace.Replacement.Text;

namespace iPdfWriter.ConsoleApp.Code
{
    using ComponentModel.Helpers;

    /// <summary>
    /// Shows the use of save as zip an input asynchronously.
    /// </summary>
    internal static class Sample07
    {
        public static async Task GenerateAsync(ILogger logger, YesNo useTestMode = YesNo.No, CancellationToken cancellationToken = default)
        {
            #region Initialize timer

            var sw = new Stopwatch();
            sw.Start();

            #endregion

            #region Creates pdf file reference

            var doc = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-07/file-sample.pdf"
            };

            #endregion

            #region Replace actions

            // Inserts report title
            doc.Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#TITLE#",
                    NewText = "Lorem ipsum",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample07.TextStylesTable["ReportTitle"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                }));


            // Inserts bar-chart image
            doc.Replace(new ReplaceText(
                new WithImageObject
                {
                    Text = "#BAR-CHART#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample07.ImagesStylesTable["Default"],
                    ReplaceOptions = ReplaceTextOptions.Default,
                    Image = PdfImage.FromFile("~Resources/Sample-01/Images/bar-chart.png")
                }));

            // Inserts image
            doc.Replace(new ReplaceText(
                new WithImageObject
                {
                    Text = "#IMAGE1#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample07.ImagesStylesTable["Center"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                    Image = PdfImage.FromFile("~/Resources/Sample-01/Images/image-1.jpg")
                }));

            #endregion

            #region Create output result

            var result = await doc.CreateResultAsync(new PdfOutputResultConfig { Filename = "Sample-07.pdf", Zipped = true }, cancellationToken);
            if (!result.Success)
            {
                logger.Info("   > Error creating output result");
                logger.Info($"     > Error: {result.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region Saves output result

            var saveResult = await result.Result.Action(new SaveToFileAsync { OutputPath = "~/Output/Sample07/Sample-07" }, cancellationToken);
            
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
            logger.Info("     > Path: ~/Output/Sample07/Sample-07.zip");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
