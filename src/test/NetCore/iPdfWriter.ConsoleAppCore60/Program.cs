﻿
namespace iPdfWriter
{
    using System;

    using iTin.Logging;
    using iTin.Logging.ComponentModel;

    using iTin.Core.Models.Design.Enums;

    using Code;
    using ComponentModel;

    static class Program
    {
        static void Main(string[] args)
        {
            Console.Title = Constants.AppName;

            ILogger logger = new Logger(Constants.AppName, new ILog[] { new FileLog(), new ColoredConsoleLog() }) { Deep = LogDeep.AnyCall, Status = LogStatus.Running };
            logger.Debug(">Start Logging<");

            // 01. Generate sample 01 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 01");
            logger.Info(" > Shows the use of text and image replacement");
            Sample01.Generate(logger);

            // 02. Generate sample 02 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 02");
            logger.Info(" > Shows the use of html table replacement in a pdf document");
            Sample02.Generate(logger);

            // 03. Generate sample 03 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 03");
            logger.Info(" > Shows the use of merge action");
            Sample03.Generate(logger);

            // 04. Generate sample 04 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 04");
            logger.Info(" > Shows the header replacement after merge action");
            Sample04.Generate(logger);

            // 05. Generate sample 05 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 05");
            logger.Info(" > Shows the use of System Tags such as page number");
            Sample05.Generate(logger);

            // 06. Generate sample 06 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 06");
            logger.Info(" > Shows the use of test mode");
            Sample06.Generate(logger, useTestMode: YesNo.Yes);

            // 07.Generate sample 07 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 07");
            logger.Info(" > Shows the use of save as zip a input with user-defined filename");
            Sample07.Generate(logger);

            // 08. Generate sample 08 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 08");
            logger.Info(" > Shows the use of save as zip merged output");
            Sample08.Generate(logger);

            // 09. Generate sample 09 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 09");
            logger.Info(" > Shows the use of compression threshold (>2MB) when merge and zip files (NO zipped)");
            Sample09.Generate(logger);

            // 10.Generate sample 10 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 10");
            logger.Info(" > Shows the use of save as zip a input with random name");
            Sample10.Generate(logger);

            // 12.Generate sample 12 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 12");
            logger.Info(" >  Shows the use of how serialize and deserialize text, image and table styles");
            Sample12.Generate(logger);

            // 13. Generate sample 13 report
            logger.Info("");
            logger.Info("> Start Pdf Sample 13");
            logger.Info(" > Shows the use of text and image replacement");
            Sample13.Generate(logger, YesNo.No);

            logger.Info("");
            logger.Debug(">End Logging<");
            Console.ReadKey();
        }
    }
}
