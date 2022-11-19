
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using iTin.Utilities.Pdf.Writer;
using iTin.Utilities.Pdf.Writer.Operations.Result.Actions;

namespace iPdfWriter.Code
{
    /// <summary>
    /// Show how to extract pages from a pdf document by search text asynchronously.
    /// </summary>
    internal static class Sample23
    {
        public static async Task GenerateAsync(CancellationToken cancellationToken = default)
        {
            // Creates pdf file reference
            var doc = new PdfInput
            {
                Input = "~/Resources/Sample-23/file-sample.pdf"
            };

            // Extract pages and save result
            var docPages = await doc.NumberOfPagesAsync(cancellationToken);
            var matches = (await doc.SearchTextAsync("#TITLE#", cancellationToken)).ToList();
            var matchesCount = matches.Count;
            for (var i = 0; i < matchesCount; i++)
            {
                var currentMatch = matches[i];
                int? to = currentMatch.Page;

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

                var matchInput = await doc.ExtractPagesAsync(currentMatch.Page, to, cancellationToken);
                var result = await matchInput.CreateResultAsync(cancellationToken: cancellationToken);
                await result.Result.Action(new SaveToFileAsync { OutputPath = $"~/Output/Sample23/Sample-23-{i + 1}" }, cancellationToken);
            }
        }
    }
}
