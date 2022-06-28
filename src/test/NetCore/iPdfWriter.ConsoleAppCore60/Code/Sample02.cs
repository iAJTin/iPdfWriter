
namespace iPdfWriter.Code
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;

    using iTin.Core.ComponentModel;
    using iTin.Core.Models.Design.Enums;

    using iTin.Logging.ComponentModel;

    using iTin.Utilities.Pdf.Design.Styles;
    using iTin.Utilities.Pdf.Design.Table;

    using iTin.Utilities.Pdf.Writer;
    using iTin.Utilities.Pdf.Writer.ComponentModel;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;


    /// <summary>
    /// Shows the use of html table replacement in a pdf document.
    /// </summary>
    internal static class Sample02
    {
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
                Input = "~/Resources/Sample-02/file-sample.pdf"
            };

            #endregion

            #region Replace actions

            // Inserts html table
            doc.Replace(new ReplaceText(
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

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample02/Sample-02" });
            var ts = sw.Elapsed;
            sw.Stop();

            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample02/Sample-02.pdf");
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
