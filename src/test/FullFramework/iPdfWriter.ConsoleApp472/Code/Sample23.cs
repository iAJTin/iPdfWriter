
using System.Linq;
using iTin.Utilities.Pdf.Writer;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;

namespace iPdfWriter.Code
{
    /// <summary>
    /// Show how to extract pages from a pdf document by search text
    /// </summary>
    internal static class Sample23
    {
        // Generates partial document(s)
        public static void Generate()
        {
            // Creates pdf file reference
            var doc = new PdfInput
            {
                Input = "~/Resources/Sample-22/file-sample.pdf"
            };

            // Extract pages and save result
            var docPages = doc.NumberOfPages();
            var matches = doc.SearchText("#TITLE#").ToList();
            var matchesCount = matches.Count;
            for (var i = 0; i < matchesCount; i++)
            {
                var currentMatch = matches[i];
                int? to = currentMatch.Page; ;

                var hasNextMatch = i != matchesCount - 1;
                if (hasNextMatch)
                {
                    var nextMatch = matches[i + 1];
                    to = nextMatch.Page - 1;
                }
                else if (currentMatch.Page == docPages)
                {
                    to = null;
                }

                var matchInput = doc.ExtractPages(currentMatch.Page, to);
                var result = matchInput.CreateResult();
                result.Result.Action(new SaveToFile { OutputPath = $"~/Output/Sample23/Sample-23-{i + 1}" });
            }
        }
    }
}
