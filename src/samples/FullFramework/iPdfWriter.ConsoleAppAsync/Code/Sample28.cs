﻿
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using iTin.Core.ComponentModel;
using iTin.Core.Models.Design.Enums;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Design.Image;
using iTin.Utilities.Pdf.Design.Styles;

using iPdfWriter.Operations.Insert;
using iPdfWriter.Operations.Actions;
using iPdfWriter.Operations.Replace;
using iPdfWriter.Operations.Replace.Replacement.Text;
using iPdfWriter.Operations.Set;

namespace iPdfWriter.ConsoleApp.Code
{
    using ComponentModel.Helpers;

    /// <summary>
    /// Shows how to insert an image into document asynchronously.
    /// </summary>
    internal static class Sample28
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
                Input = "~/Resources/Sample-27/file-sample.pdf"
            };

            #endregion

            #region Replace actions

            // report title
            doc
                .Replace(new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#TITLE#",
                        NewText = "Lorem ipsum",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = (PdfTextStyle)StylesHelper.Sample28.StylesTable["ReportTitle"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                    }))
                // bar-chart image
                .Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#BAR-CHART#",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = (PdfImageStyle)StylesHelper.Sample28.StylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.Default,
                        Image = PdfImage.FromFile("~Resources/Sample-28/Images/bar-chart.png")
                    }))
                // image
                .Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#IMAGE1#",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = (PdfImageStyle)StylesHelper.Sample28.StylesTable["Center"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = PdfImage.FromFile("~/Resources/Sample-28/Images/image-1.jpg")
                    }));

            #endregion

            #region Insert actions

            doc.Insert(new InsertImage
            {
                Page = 1,
                UseTestMode = useTestMode,
                Offset = new PointF(450.0f, 200.0f),
                Image = PdfImage.FromFile("~/Resources/Sample-28/Images/pirate.png")
            });

            #endregion

            #region Set actions

            doc
                .Set(new SetCreator { Value = "iPdfWriter" })
                .Set(new SetTitle { Value = "Hello from iPdfWriter" })
                .Set(new SetSubject { Value = "Subject changed from iPdfWriter" })
                .Set(new SetAuthor { Value = "iPdfWriter" })
                .Set(new SetKeywords { Value = "Samples, iPdfWriter, pdf" });

            #endregion

            #region Create output result

            var outputResult = await doc.CreateResultAsync(cancellationToken: cancellationToken);
            if (!outputResult.Success)
            {
                logger.Info("   > Error creating output result");
                logger.Info($"     > Error: {outputResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region Saves output result

            var saveResult = await outputResult.Result.Action(new SaveToFileAsync { OutputPath = "~/Output/Sample28/Sample-28" }, cancellationToken);

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
            logger.Info("     > Path: ~/Output/Sample28/Sample-28.pdf");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
