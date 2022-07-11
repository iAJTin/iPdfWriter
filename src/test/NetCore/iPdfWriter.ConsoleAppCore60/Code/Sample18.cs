
using System.Diagnostics;
using System.Drawing;

using iTin.Core.ComponentModel;
using iTin.Core.Models;
using iTin.Core.Models.Design.Enums;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Design.Image;
using iTin.Utilities.Pdf.Design.Styles;

using iTin.Utilities.Pdf.Writer;
using iTin.Utilities.Pdf.Writer.ComponentModel;
using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;

namespace iPdfWriter.Code
{
    /// <summary>
    /// Shows the use of text and image replacement with styles from file.
    /// </summary>
    internal static class Sample18
    {
        // Image styles
        private static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new()
        {
            {
                "Default",
                new PdfImageStyle
                {
                    Content =
                    {
                        Alignment =
                        {
                            Horizontal = KnownHorizontalAlignment.Left
                        }
                    }
                }
            }
        };


        // Generates document
        public static void Generate(ILogger logger, YesNo useTestMode = YesNo.No)
        {
            #region Initialize timer
            var sw = new Stopwatch();
            sw.Start();
            #endregion

            #region Creates pdf file reference

            var doc = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-01/file-sample.pdf"
            };

            #endregion

            #region Replace actions

            // report title
            doc.Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#TITLE#",
                    NewText = "Lorem ipsum",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                    Style = (PdfTextStyle) PdfTextStyle.LoadFromFile("~Resources/Sample-18/Styles/TextStyle.json", format: KnownFileFormat.Json)
                }))
                // bar-chart image
                .Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#BAR-CHART#",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = ImagesStylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.Default,
                        Image = PdfImage.FromFile("~Resources/Sample-18/Images/bar-chart.png")
                    }))
                // image
                .Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#IMAGE1#",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = PdfImage.FromFile("~/Resources/Sample-18/Images/image-1.jpg"),
                        Style = (PdfImageStyle) PdfImageStyle.LoadFromFile("~Resources/Sample-18/Styles/ImageStyle.Xml")
                    }));

            #endregion

            #region Create output result

            var result = doc.CreateResult();
            if (!result.Success)
            {
                logger.Info("   > Error creating output result");
                logger.Info($"     > Error: {result.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region Saves output result

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample18/Sample-18" });
            var ts = sw.Elapsed;
            sw.Stop();

            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                logger.Info($"   > Elapsed time: { ts.Hours:00}:{ ts.Minutes:00}:{ ts.Seconds:00}.{ ts.Milliseconds / 10:00}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample18/Sample-18.pdf");
            logger.Info($"   > Elapsed time: { ts.Hours:00}:{ ts.Minutes:00}:{ ts.Seconds:00}.{ ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
