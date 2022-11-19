
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;
using iTin.Core.Models.Design.Enums;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Design.Image;
using iTin.Utilities.Pdf.Design.Styles;
using iTin.Utilities.Pdf.Design.Table;

using iTin.Utilities.Pdf.Writer;
using iTin.Utilities.Pdf.Writer.Operations.Replace;
using iTin.Utilities.Pdf.Writer.Operations.Replace.Replacement.Text;
using iTin.Utilities.Pdf.Writer.Operations.Result.Actions;

namespace iPdfWriter.Code
{
    using ComponentModel.Helpers;

    /// <summary>
    /// Shows the use of merge action asynchronously.
    /// </summary>
    internal static class Sample03
    {
        public static async Task GenerateAsync(ILogger logger, YesNo useTestMode = YesNo.No, CancellationToken cancellationToken = default)
        {
            #region Initialize timer

            var sw = new Stopwatch();
            sw.Start();

            #endregion

            #region page-1

            var page1 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-03/file-sample-1.pdf"
            };

            // Inserts report title
            page1.Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#TITLE#",
                    NewText = "Lorem ipsum",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample03.TextStylesTable["ReportTitle"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                }));


            // Inserts bar-chart image
            page1.Replace(new ReplaceText(
                new WithImageObject
                {
                    Text = "#BAR-CHART#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample03.ImagesStylesTable["Default"],
                    ReplaceOptions = ReplaceTextOptions.Default,
                    Image = PdfImage.FromFile("~Resources/Sample-01/Images/bar-chart.png")
                }));

            #endregion

            #region page-2

            var page2 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-03/file-sample-2.pdf"
            };

            // Inserts html table
            page2.Replace(new ReplaceText(
                new WithTableObject
                {
                    Text = "#DATA-TABLE#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = PdfTableStyle.Default,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
                    Table = await PdfTable.CreateFromHtmlAsync(HtmlDataHelper.GenerateHtmlDatatable(), config: new PdfTableConfig { HeightStrategy = TableHeightStrategy.Exact })
                }));

            #endregion

            #region page-3

            var page3 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-03/file-sample-3.pdf"
            };

            #endregion

            #region page-4

            var page4 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-03/file-sample-4.pdf"
            };

            // Inserts image
            page4.Replace(new ReplaceText(
                new WithImageObject
                {
                    Text = "#IMAGE1#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample03.ImagesStylesTable["Center"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                    Image = PdfImage.FromFile("~/Resources/Sample-01/Images/image-1.jpg")
                }));

            #endregion

            #region merge

            var files = new PdfObject
            {
                Items = new List<PdfInput>
                {
                    new() { Index = 0, Input = page1 },
                    new() { Index = 1, Input = page2 },
                    new() { Index = 2, Input = page3 },
                    new() { Index = 3, Input = page4 }
                }
            };

            var mergeResult = await files.TryMergeInputsAsync(cancellationToken);
            if (!mergeResult.Success)
            {
                logger.Info("   > Error creating merge result");
                logger.Info($"     > Error: {mergeResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region save

            var saveResult = await mergeResult.Result.Action(new SaveToFileAsync { OutputPath = "~/Output/Sample03/Sample-03" }, cancellationToken);

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
            logger.Info("     > Path: ~/Output/Sample03/Sample-03.pdf");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
