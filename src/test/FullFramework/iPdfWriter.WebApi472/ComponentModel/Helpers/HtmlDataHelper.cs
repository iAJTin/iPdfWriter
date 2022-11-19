
using System.Text;

namespace iPdfWriter.WebApi.ComponentModel.Helpers
{
    internal static class HtmlDataHelper
    { 
        public static string GenerateHtmlDatatable()
        {
            var result = new StringBuilder();

            result.AppendLine("<table border='1' cellspacing='0' cellpadding='6' style='width:100%'>");
            result.AppendLine(" <tbody>");
            result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine("    <td>&nbsp;</td>");
            result.AppendLine("    <td>Lorem ipsum</td>");
            result.AppendLine("    <td>Lorem ipsum</td>");
            result.AppendLine("    <td>Lorem ipsum</td>");
            result.AppendLine(" </tr>");
            result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine("    <td>1</td>");
            result.AppendLine("    <td>In eleifend velit vitae libero sollicitudin euismod.</td>");
            result.AppendLine("    <td>Lorem</td>");
            result.AppendLine("    <td>&nbsp;</td>");
            result.AppendLine(" </tr>");
            result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine("    <td>2</td>");
            result.AppendLine("    <td>Cras fringilla ipsum magna, in fringilla dui commodo a.</td>");
            result.AppendLine("    <td>Lorem</td>");
            result.AppendLine("    <td>&nbsp;</td>");
            result.AppendLine(" </tr>");
            result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine("    <td>3</td>");
            result.AppendLine("    <td>LAliquam erat volutpat.</td>");
            result.AppendLine("    <td>Lorem</td>");
            result.AppendLine("    <td>&nbsp;</td>");
            result.AppendLine(" </tr>");
            result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine("    <td>4</td>");
            result.AppendLine("    <td>Fusce vitae vestibulum velit. </td>");
            result.AppendLine("    <td>Lorem</td>");
            result.AppendLine("    <td>&nbsp;</td>");
            result.AppendLine(" </tr>");
            result.AppendLine("  <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>");
            result.AppendLine("    <td>5</td>");
            result.AppendLine("    <td>Etiam vehicula luctus fermentum.</td>");
            result.AppendLine("    <td>Ipsum</td>");
            result.AppendLine("    <td>&nbsp;</td>");
            result.AppendLine(" </tr>");
            result.AppendLine(" </tbody>");
            result.AppendLine("</table>");

            return result.ToString();
        }
    }
}
