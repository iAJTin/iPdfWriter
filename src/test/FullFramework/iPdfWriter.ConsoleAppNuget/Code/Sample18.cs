﻿
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using iTin.Core;
using iTin.Core.ComponentModel;
using iTin.Core.Models.Design.Enums;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Design.Styles;
using iTin.Utilities.Pdf.Design.Table;

using iTin.Utilities.Pdf.Writer;
using iTin.Utilities.Pdf.Writer.ComponentModel;
using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;
using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;

using iPdfWriter.ComponentModel;

namespace iPdfWriter.Code
{
    /// <summary>
    /// Shows the use of add a datatable (render as html) in a pdf document.
    /// </summary>
    internal static class Sample18
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
                Input = "~/Resources/Sample-18/file-sample.pdf"
            };

            #endregion

            #region Replace actions

            // Inserts a datatable object
            doc.Replace(new ReplaceText(
                new WithTableObject
                {
                    Text = "#DATA-TABLE#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = PdfTableStyle.Default,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
                    Table = PdfTable.CreateFromDataTable(
                        data: 
                            new List<Person>
                            {
                                new Person {Name = "Name-01", Surname = "Surname-01"},
                                new Person {Name = "Name-02", Surname = "Surname-02"},
                                new Person {Name = "Name-03", Surname = "Surname-03"},
                                new Person {Name = "Name-04", Surname = "Surname-04"},
                                new Person {Name = "Name-05", Surname = "Surname-05"},
                                new Person {Name = "Name-06", Surname = "Surname-06"},
                                new Person {Name = "Name-07", Surname = "Surname-07"},
                                new Person {Name = "Name-08", Surname = "Surname-08"},
                            }
                            .ToDataTable<Person>("People"),
                        css: @"
                            table { 
                             border-spacing: 0px;
                             border-collapse: collapse;  
                            }

                            tr {
                              font-size: 9pt;
                              font-family: Arial; 
                              color: #AC1198;
                              text-align: left;
                              overflow: hidden;
                            }

                            td {
                              padding: 6px;
                            }")
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

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample18/Sample-18" });
            var ts = sw.Elapsed;
            sw.Stop();

            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample18/Sample-18.pdf");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
