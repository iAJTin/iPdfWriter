
namespace iPdfWriter.Code
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;

    using iTin.Core.ComponentModel;
    using iTin.Core.Models.Design.Enums;

    using iTin.Logging.ComponentModel;

    using iTin.Utilities.Pdf.Design.Image;
    using iTin.Utilities.Pdf.Design.Styles;

    using iTin.Utilities.Pdf.Writer;
    using iTin.Utilities.Pdf.Writer.ComponentModel;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;

    /// <summary>
    /// Shows the use of save as zip an input.
    /// </summary>
    internal static class Sample07
    {
        // Image styles
        private static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new Dictionary<string, PdfImageStyle>
        {
            {
                "Center",
                new PdfImageStyle
                {
                    Content =
                    {
                        Alignment =
                        {
                            Horizontal = KnownHorizontalAlignment.Center
                        }
                    }
                }
            },
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

        // Text styles
        private static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new Dictionary<string, PdfTextStyle>
        {
            {
                "ReportTitle",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 28.0f,
                        Bold = YesNo.Yes,
                        Italic = YesNo.Yes,
                        Color = "Blue"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Center,
                            Horizontal = KnownHorizontalAlignment.Center
                        }
                    }
                }
            },
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
                    TextOffset = PointF.Empty,
                    Style = TextStylesTable["ReportTitle"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                }));


            // Inserts bar-chart image
            using (var barGraph = PdfImage.FromFile("~/Resources/Sample-07/Images/bar-chart.png"))
            {
                doc.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#BAR-CHART#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = ImagesStylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.Default,
                        Image = barGraph
                    }));
            }

            // Inserts image
            using (var image = PdfImage.FromFile("~/Resources/Sample-07/Images/image-1.jpg"))
            {
                doc.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#IMAGE1#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = ImagesStylesTable["Center"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = image
                    }));
            }

            #endregion

            #region Create output result

            var result = doc.CreateResult(new OutputResultConfig { Filename = "Sample-07.pdf", Zipped = true });
            if (!result.Success)
            {
                logger.Info("   > Error creating output result");
                logger.Info($"     > Error: {result.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region Saves output result

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = $"~/Output/Sample07/Sample-07" });
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
            logger.Info("     > Path: ~/Output/Sample07/Sample-07.zip");
            logger.Info($"   > Elapsed time: { ts.Hours:00}:{ ts.Minutes:00}:{ ts.Seconds:00}.{ ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
