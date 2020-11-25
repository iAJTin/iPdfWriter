
namespace iPdfWriter.Code
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;

    using iTin.Core.ComponentModel;
    using iTin.Core.Models.Design.Enums;

    using iTin.Logging.ComponentModel;

    using iTin.Utilities.Pdf.Design.Image;
    using iTin.Utilities.Pdf.Design.Styles;
    using iTin.Utilities.Pdf.Design.Table;

    using iTin.Utilities.Pdf.Writer;
    using iTin.Utilities.Pdf.Writer.ComponentModel;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;

    /// <summary>
    /// Shows the use of merge action.
    /// </summary>
    internal class Sample03
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
                    TextOffset = PointF.Empty,
                    Style = TextStylesTable["ReportTitle"],
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                }));


            // Inserts bar-chart image
            using (var barGraph = PdfImage.FromFile("~/Resources/Sample-03/Images/bar-chart.png"))
            {
                page1.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#BAR-CHART#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = ImagesStylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = barGraph
                    }));
            }

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
                    TableOffset = PointF.Empty,
                    Style = PdfTableStyle.Default,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
                    Table = PdfTable.CreateFromHtml(GenerateHtmlDatatable(), config: new PdfTableConfig { HeightStrategy = TableHeightStrategy.Exact })
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
            using (var image = PdfImage.FromFile("~/Resources/Sample-03/Images/image-1.jpg"))
            {
                page4.Replace(new ReplaceText(
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

            #region merge

            var files = new PdfObject
            {
                Items = new List<PdfInput>
                {
                    new PdfInput {Index = 0, Input = page1},
                    new PdfInput {Index = 1, Input = page2},
                    new PdfInput {Index = 2, Input = page3},
                    new PdfInput {Index = 3, Input = page4},
                }
            };

            var mergeResult = files.TryMergeInputs();
            if (!mergeResult.Success)
            {
                logger.Info("   > Error creating merge result");
                logger.Info($"     > Error: {mergeResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region save

            var saveResult = mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample03/Sample-03" });
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
            logger.Info("     > Path: ~/Output/Sample03/Sample-03.pdf");
            logger.Info($"   > Elapsed time: { ts.Hours:00}:{ ts.Minutes:00}:{ ts.Seconds:00}.{ ts.Milliseconds / 10:00}");

            #endregion
        }


        // Generates html table
        private static string GenerateHtmlDatatable()
        {
            var result = new StringBuilder();

            result.AppendLine($"<table border='1' cellspacing='0' cellpadding='6' style='width:100%'>");
            result.AppendLine($" <tbody>");
            result.AppendLine($"  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine($"    <td>&nbsp;</td>");
            result.AppendLine($"    <td>Lorem ipsum</td>");
            result.AppendLine($"    <td>Lorem ipsum</td>");
            result.AppendLine($"    <td>Lorem ipsum</td>");
            result.AppendLine($" </tr>");
            result.AppendLine($"  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine($"    <td>1</td>");
            result.AppendLine($"    <td>In eleifend velit vitae libero sollicitudin euismod.</td>");
            result.AppendLine($"    <td>Lorem</td>");
            result.AppendLine($"    <td>&nbsp;</td>");
            result.AppendLine($" </tr>");
            result.AppendLine($"  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine($"    <td>2</td>");
            result.AppendLine($"    <td>Cras fringilla ipsum magna, in fringilla dui commodo a.</td>");
            result.AppendLine($"    <td>Lorem</td>");
            result.AppendLine($"    <td>&nbsp;</td>");
            result.AppendLine($" </tr>");
            result.AppendLine($"  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine($"    <td>3</td>");
            result.AppendLine($"    <td>LAliquam erat volutpat.</td>");
            result.AppendLine($"    <td>Lorem</td>");
            result.AppendLine($"    <td>&nbsp;</td>");
            result.AppendLine($" </tr>");
            result.AppendLine($"  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine($"    <td>4</td>");
            result.AppendLine($"    <td>Fusce vitae vestibulum velit. </td>");
            result.AppendLine($"    <td>Lorem</td>");
            result.AppendLine($"    <td>&nbsp;</td>");
            result.AppendLine($" </tr>");
            result.AppendLine($"  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine($"    <td>5</td>");
            result.AppendLine($"    <td>Etiam vehicula luctus fermentum.</td>");
            result.AppendLine($"    <td>Ipsum</td>");
            result.AppendLine($"    <td>&nbsp;</td>");
            result.AppendLine($" </tr>");
            result.AppendLine(" </tbody>");
            result.AppendLine("</table>");

            return result.ToString();
        }
    }
}
