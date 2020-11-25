﻿
namespace iPdfWriter.Code
{
    using System.Collections.Generic;
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
    /// Shows the use of <b>System Tags</b> such as page number of a document and the use of merge of several entries to compose a complete document.
    /// </summary>
    internal class Sample05
    {
        // Text styles
        private static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new Dictionary<string, PdfTextStyle>
        {
            {
                "Header",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Verdana",
                        Size = 8.0f,
                        Bold = YesNo.No,
                        Color = "Gray"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Center,
                            Horizontal = KnownHorizontalAlignment.Left
                        }
                    }
                }
            },
            {
                "PageNumber",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Verdana",
                        Size = 8.0f,
                        Bold = YesNo.No,
                        Color = "Gray"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Center,
                            Horizontal = KnownHorizontalAlignment.Right
                        }
                    }
                }
            },
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
            #region page-1

            var page1 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-05/file-sample-1.pdf"
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
            using (var barGraph = PdfImage.FromFile("~/Resources/Sample-05/Images/bar-chart.png"))
            {
                page1.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#BAR-CHART#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = PdfImageStyle.Center,
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = barGraph
                    }));
            }

            #endregion

            #region page-2

            var page2 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-05/file-sample-2.pdf"
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
                    Table = PdfTable.CreateFromHtml(GenerateHtmlDatatable())
                }));

            #endregion

            #region page-3

            var page3 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-05/file-sample-3.pdf"
            };

            #endregion

            #region page-4

            var page4 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-05/file-Sample-4.pdf"
            };

            // Inserts image
            using (var image = PdfImage.FromFile("~/Resources/Sample-05/Images/image-1.jpg"))
            {
                page4.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#IMAGE1#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = PdfImageStyle.Default,
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = image
                    }));
            }

            #endregion

            #region merge

            // Defines system tags to replace > page number
            var systemTags = new SystemTagsCollection
            {
                new PageNumberSystemTag
                {
                    UseTestMode = useTestMode,
                    TextOffset = PointF.Empty,
                    Style = TextStylesTable["PageNumber"],
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                }
            };

            // Defines global text replacements to replace > header text
            var globalReplacements = new GlobalReplacementsCollection
            {
                new WithTextObject
                {
                    Text = "#HEADER-TEXT#",
                    NewText = "Report Name - Lorem ipsum dolor",
                    Style = TextStylesTable["Header"],
                    ReplaceOptions = ReplaceTextOptions.FromLeftMarginToNextElement,
                    UseTestMode = useTestMode,
                    TextOffset = PointF.Empty
                }
            };

            // Defines merge configuration, includes tags, global replacements and allow compress the merged output 
            var files = new PdfObject(new PdfObjectConfig { Tags = systemTags, GlobalReplacements = globalReplacements })
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
                logger.Info("   > Error creating output merge result");
                logger.Info($"     > Error: {mergeResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            #endregion

            #region save

            // Saves merged result to disk
            var saveResult = mergeResult.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample05/Sample-05" });
            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample05/Sample-05.pdf");

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
