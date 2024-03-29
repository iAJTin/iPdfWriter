﻿
using System;
using System.Threading.Tasks;

using iTin.Core.Models.Design.Enums;

using iTin.Logging;
using iTin.Logging.ComponentModel;

namespace iPdfWriter.ConsoleApp
{
    using Code;
    using ComponentModel;

    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            Console.Title = Constants.AppName;

            ILogger logger = new Logger(Constants.AppName, new ILog[] { new FileLog(), new ColoredConsoleLog() }) { Deep = LogDeep.OnlyApplicationCalls, Status = LogStatus.Running };
            logger.Debug(">Start Logging<");

            // 01. Generate sample 01 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 01");
            logger.Info(" > Shows the use of text and image replacement asynchronously");
            await Sample01.GenerateAsync(logger);

            // 02. Generate sample 02 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 02");
            logger.Info(" > Shows the use of html table replacement in a pdf document asynchronously");
            await Sample02.GenerateAsync(logger);

            // 03. Generate sample 03 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 03");
            logger.Info(" > Shows the use of merge action asynchronously");
            await Sample03.GenerateAsync(logger);

            // 04. Generate sample 04 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 04");
            logger.Info(" > Shows the header replacement after merge action asynchronously");
            await Sample04.GenerateAsync(logger);

            //05.Generate sample 05 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 05");
            logger.Info(" > Shows the use of System Tags such as page number asynchronously");
            await Sample05.GenerateAsync(logger);

            // 06. Generate sample 06 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 06");
            logger.Info(" > Shows the use of test mode asynchronously");
            await Sample06.GenerateAsync(logger, useTestMode: YesNo.Yes);

            // 07.Generate sample 07 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 07");
            logger.Info(" > Shows the use of save as zip a input with user-defined filename asynchronously");
            await Sample07.GenerateAsync(logger);

            // 08. Generate sample 08 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 08");
            logger.Info(" > Shows the use of save as zip merged output asynchronously");
            await Sample08.GenerateAsync(logger);

            // 09. Generate sample 09 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 09");
            logger.Info(" > Shows the use of compression threshold (>2MB) when merge and zip files (NO zipped) asynchronously");
            await Sample09.GenerateAsync(logger);

            // 10.Generate sample 10 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 10");
            logger.Info(" > Shows the use of save as zip a input with random name asynchronously");
            await Sample10.GenerateAsync(logger);

            // 12.Generate sample 12 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 12");
            logger.Info(" >  Shows the use of how serialize and deserialize text, image and table styles asynchronously");
            await Sample12.GenerateAsync(logger);

            // 13. Generate sample 13 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 13");
            logger.Info(" > Shows the use of text and image replacement asynchronously");
            await Sample13.GenerateAsync(logger, YesNo.No);

            // 16. Shows the use of add an enumerable (render as html) in a pdf document asynchronously.
            logger.Info("");
            logger.Info("> Start Pdf Sample 16");
            logger.Info(" > Shows the use of add an enumerable (render as html) in a pdf document asynchronously");
            await Sample16.GenerateAsync(logger, YesNo.No);

            //// 17. Shows the use of add an enumerable (native render) in a pdf document asynchronously.
            //logger.Info("");
            //logger.Info("> Start Pdf Sample 17");
            //logger.Info(" > Shows the use of add an enumerable (native render) in a pdf document asynchronously");
            //Sample17.GenerateAsync(logger, YesNo.No);

            // 18. Shows the use of add a datatable (render as html) in a pdf document asynchronously.
            logger.Info("");
            logger.Info("> Start Pdf Sample 18");
            logger.Info(" > Shows the use of add a datatable (render as html) in a pdf document asynchronously");
            await Sample18.GenerateAsync(logger, YesNo.No);

            //// 19. Shows the use of add a datatable (native render) in a pdf document asynchronously.
            //logger.Info("");
            //logger.Info("> Start Pdf Sample 19");
            //logger.Info(" > Shows the use of add a datatable (native render) in a pdf document asynchronously");
            //await Sample19.GenerateAsync(logger, YesNo.No);

            // 20. Shows the use of add a datatable in a pdf document asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 20");
            logger.Info(" > Shows the use of text and image replacement with styles from file asynchronously");
            await Sample20.GenerateAsync(logger, YesNo.No);

            // 21. Shows the use of text and image replacement in a pdf document with custom font asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 21");
            logger.Info(" > Shows the use of text and image replacement in a pdf document with custom font asynchronously");
            await Sample21.GenerateAsync(logger, YesNo.No);

            // 22. Show how to extract pages from a pdf document asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 22");
            logger.Info(" > Show how to extract pages from a pdf document asynchronously");
            await Sample22.GenerateAsync(logger);

            // 23. Show how to extract pages from a pdf document by search text asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 23");
            logger.Info(" > Show how to extract pages from a pdf document by search text asynchronously");
            await Sample23.GenerateAsync();

            // 24.Show how to extract text lines from a pdf document asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 24");
            logger.Info(" > Show how to extract text lines from a pdf document asynchronously");
            await Sample24.GenerateAsync(logger);

            // 25. Show how to creates a pdf input from html asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 25");
            logger.Info(" > Show how to creates a pdf input from html asynchronously");
            await Sample25.GenerateAsync(logger);

            // 26. Generate sample 26 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 26");
            logger.Info(" > Shows how to add or modify pdf metadata information asynchronously");
            await Sample26.GenerateAsync(logger);

            // 27. Generate sample 27 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 27");
            logger.Info(" > Shows how to add a password to output file asynchronously");
            await Sample27.GenerateAsync(logger);

            // 28. Generate sample 28 report asynchronously
            logger.Info("");
            logger.Info("> Start Pdf Sample 28");
            logger.Info(" > Shows how to insert an image into document asynchronously");
            await Sample28.GenerateAsync(logger, YesNo.No);

            logger.Info("");
            logger.Debug(">End Logging<");
            Console.ReadKey();
        }
    }
}
