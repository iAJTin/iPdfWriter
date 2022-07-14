
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

namespace iPdfWriter.Code
{
    /// <summary>
    /// Shows the use of text and image replacement in a pdf document with custom font.
    /// </summary>
    internal static class Sample21
    {
        // Styles
        private static readonly Dictionary<string, PdfBaseStyle> StylesTable = new()
        {
            {
                "ReportTitle",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Pacifico",
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


        // Generates document
        public static void Generate(ILogger logger, YesNo useTestMode = YesNo.No)
        {
            #region Initialize timer
            var sw = new Stopwatch();
            sw.Start();
            #endregion

            #region Register pdf fonts from file

            var registerResult = PdfFonts.RegisterFont("Pacifico", @"~Resources/Sample-21/Fonts/Pacifico/Pacifico.ttf");
            if (!registerResult.Success)
            {
                logger.Info("   > Can not create specified font. The default font will be used");

                return;
            }

            #endregion

            #region Creates pdf file reference

            var doc = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-21/file-sample.pdf"
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
                    Style = (PdfTextStyle) StylesTable["ReportTitle"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                }))
                // bar-chart image
                .Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#BAR-CHART#",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = (PdfImageStyle) StylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.Default,
                        Image = PdfImage.FromFile("~Resources/Sample-21/Images/bar-chart.png")
                    }))
                // image
                .Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#IMAGE1#",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = (PdfImageStyle) StylesTable["Center"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = PdfImage.FromFile("~/Resources/Sample-21/Images/image-1.jpg")
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

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample21/Sample-21" });
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
            logger.Info("     > Path: ~/Output/Sample21/Sample-21.pdf");
            logger.Info($"   > Elapsed time: { ts.Hours:00}:{ ts.Minutes:00}:{ ts.Seconds:00}.{ ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
