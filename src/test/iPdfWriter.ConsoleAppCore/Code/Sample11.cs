
namespace iPdfWriter.Code
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;

    using iTin.Core.ComponentModel;
    using iTin.Core.Models.Design;

    using iTin.Logging.ComponentModel;

    using iTin.Core.Models.Design.Enums;
    using iTin.Core.Models.Design.Styling;

    using iTin.Utilities.Pdf.Design.Image;
    using iTin.Utilities.Pdf.Design.Styles;

    using iTin.Utilities.Pdf.Writer;
    using iTin.Utilities.Pdf.Writer.ComponentModel;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Replacement.Text;
    using iTin.Utilities.Pdf.Writer.ComponentModel.Result.Action.Save;

    /// <summary>
    /// Shows the use with a real sample report.
    /// </summary>
    internal class Sample11
    {

        // Generates document
        public static void Generate(ILogger logger, YesNo useTestMode = YesNo.No)
        {
            #region timer
            var sw = new Stopwatch();
            sw.Start();
            #endregion

            #region styles

            var imageStylesTable = new Dictionary<string, PdfImageStyle>
            {
                {
                    "Cards",
                    new PdfImageStyle
                    {
                        Borders = new BordersCollection
                        {
                            new BaseBorder { Position = KnownBorderPosition.Left, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes },
                            new BaseBorder { Position = KnownBorderPosition.Top, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes },
                            new BaseBorder { Position = KnownBorderPosition.Right, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes },
                            new BaseBorder { Position = KnownBorderPosition.Bottom, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes }
                        },
                        Content = new PdfImageContent
                        {
                            Alignment = new PdfImageContentAlignment
                            {
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "Default",
                    new PdfImageStyle
                    {
                        Content = new PdfImageContent
                        {
                            Alignment = new PdfImageContentAlignment
                            {
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "DefaultCenter",
                    new PdfImageStyle
                    {
                        Content = new PdfImageContent
                        {
                            Alignment = new PdfImageContentAlignment
                            {
                                Horizontal = KnownHorizontalAlignment.Center
                            }
                        }
                    }
                },
                {
                    "CenteredCard",
                    new PdfImageStyle
                    {
                        Borders = new BordersCollection
                        {
                            new BaseBorder { Position = KnownBorderPosition.Left, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes },
                            new BaseBorder { Position = KnownBorderPosition.Top, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes },
                            new BaseBorder { Position = KnownBorderPosition.Right, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes },
                            new BaseBorder { Position = KnownBorderPosition.Bottom, Color = "#00DB90", Width = 10.0f, Show = YesNo.Yes }
                        },
                        Content = new PdfImageContent
                        {
                            Alignment = new PdfImageContentAlignment
                            {
                                Horizontal = KnownHorizontalAlignment.Center
                            }
                        }
                    }
                }
            };

            var textStylesTable = new Dictionary<string, PdfTextStyle>
            {
                {
                    "Muli-Medium-Left",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Bold = YesNo.Yes,
                            Size = 12.0f,
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "Muli-Medium-Bold-Left",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Bold = YesNo.Yes,
                            Size = 12.0f,
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "Muli-Medium-Colored-Left",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 12.0f,
                            Color = "Red",
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "Muli-Normal-Bold-Left",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Bold = YesNo.Yes,
                            Size = 14.0f,
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "Header",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 8.0f,
                            Color = "Gray"
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
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
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 8.0f,
                            Color = "Gray"
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Right
                            }
                        }
                    }
                },
                {
                    "Title",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli SemiBold",
                            Size = 18.0f,
                            Bold = YesNo.Yes,
                            Color = "#44546A"
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "CenteredTitle",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli SemiBold",
                            Size = 18.0f,
                            Bold = YesNo.Yes,
                            Color = "#44546A"
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Center,
                            }
                        }
                    }
                },
                {
                    "GraphTitle",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Grava Display Roman",
                            Size = 15.0f,
                            Bold = YesNo.Yes,
                            Color = "#808080"
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "ReportDate",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 12.0f,
                            Bold = YesNo.No,
                            Color = "#44546A"
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "ValueName",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 14.0f,
                            Bold = YesNo.Yes,
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "ValueText",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 12.0f,
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "ValueNumber",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Bold = YesNo.Yes,
                            Size = 12.0f,
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "ColoredComments",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 10.5f,
                            Color = "Red",
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
                {
                    "BoldColoredComments",
                    new PdfTextStyle
                    {
                        Font = new FontModel
                        {
                            Name = "Muli",
                            Size = 10.5f,
                            Bold = YesNo.Yes,
                            Color = "Red",
                        },
                        Content = new PdfTextContent
                        {
                            Alignment = new PdfTextContentAlignment
                            {
                                Vertical = KnownVerticalAlignment.Center,
                                Horizontal = KnownHorizontalAlignment.Left
                            }
                        }
                    }
                },
            };

            #endregion

            #region report

            #region cover

            var cover = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/01-Cover.pdf"
            };

            cover.Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#FLL_NM#",
                    NewText = "Antonio Pérez",
                    Style = textStylesTable["Title"],
                    TextOffset = new PointF(-2.0f, -4.0f),
                    UseTestMode = useTestMode,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                })).Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#DATE_REPORT#",
                    NewText = "18/09/2020",
                    Style = textStylesTable["ReportDate"],
                    TextOffset = new PointF(-2.0f, -4.0f),
                    UseTestMode = useTestMode,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                }));


            using (var logo = PdfImage.FromFile("~/Resources/Sample-11/Images/logo.png").ScaleTo(80, ScaleStrategy.Vertical))
            {
                cover.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#LG_NTRPRS#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = imageStylesTable["DefaultCenter"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = logo
                    }));
            }

            cover.Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#FLL_NM_NTRPRS#",
                    NewText = "Desperta’t Centro de coaching",
                    Style = textStylesTable["CenteredTitle"],
                    TextOffset = PointF.Empty,
                    UseTestMode = useTestMode,
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                })).Replace(new ReplaceText(
                new WithTextObject
                {
                    Text = "#FLL_NM_CLNT#",
                    NewText = "Antonio Pérez",
                    Style = textStylesTable["CenteredTitle"],
                    TextOffset = PointF.Empty,
                    UseTestMode = useTestMode,
                    ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                }));

            #endregion

            #region index

            var index = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/02-Index.pdf"
            };

            #endregion

            #region introduction

            var introduction = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/03-Introduction.pdf"
            };

            #endregion

            #region step 01

            var step1 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/04-Step-01.pdf"
            };

            using (var card = PdfImage.FromFile("~/Resources/Sample-11/Images/3_ImagenColorPath.jpg").ScaleTo(170))
            {
                for (int i = 1; i <= 5; i++)
                {
                    step1.Replace(new ReplaceText(
                        new WithImageObject
                        {
                            Text = $"#ST1_CRD{i}#",
                            ImageOffset = PointF.Empty,
                            Style = imageStylesTable["Cards"],
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
                            Image = card
                        }));
                }
            }

            #endregion

            #region step 02

            var step2 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/05-Step-02.pdf"
            };

            using (var card = PdfImage.FromFile("~/Resources/Sample-11/Images/9_ImagenPath.jpg").ScaleTo(100))
            {
                for (int i = 1; i <= 6; i++)
                {
                    step2.Replace(new ReplaceText(
                        new WithImageObject
                        {
                            Text = $"#VL_IMG_{i}#",
                            UseTestMode = useTestMode,
                            ImageOffset = PointF.Empty,
                            Style = imageStylesTable["Cards"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin,
                            Image = card
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#VL_NM_{i}#",
                            NewText = $"Nombre del valor {i}",
                            Style = textStylesTable["ValueName"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#VL_TXT_{i}#",
                            NewText = $"Significado del valor {i}",
                            Style = textStylesTable["ValueText"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                        }));
                }
            }

            #endregion

            #region step 03

            var step3 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/06-Step-03.pdf"
            };

            using (var card = PdfImage.FromFile("~/Resources/Sample-11/Images/9_ImagenPath.jpg").ScaleTo(120))
            {
                for (int i = 1; i <= 6; i++)
                {
                    step3.Replace(new ReplaceText(
                        new WithImageObject
                        {
                            Text = $"#ST3_CRD_{i}#",
                            UseTestMode = useTestMode,
                            ImageOffset = PointF.Empty,
                            Style = imageStylesTable["Cards"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement,
                            Image = card
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#ST3_TTL_{i}#",
                            NewText = $"Valor {i}",
                            Style = textStylesTable["ValueName"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#ST3_TXT_{i}_1#",
                            NewText = "Satisfacción actual:",
                            Style = textStylesTable["ValueText"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#ST3_VL_{i}_1#",
                            NewText = $"{i}",
                            Style = textStylesTable["ValueNumber"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#ST3_TXT_{i}_2#",
                            NewText = "Satisfacción deseada:",
                            Style = textStylesTable["ValueText"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        })).Replace(new ReplaceText(
                        new WithTextObject
                        {
                            Text = $"#ST3_VL_{i}_2#",
                            NewText = $"{10 - i}",
                            Style = textStylesTable["ValueNumber"],
                            TextOffset = PointF.Empty,
                            UseTestMode = useTestMode,
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }));
                }
            }

            #endregion

            #region step 04

            #region step 04a

            var step4a = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/07-Step-04A.pdf"
            };

            #endregion

            #region step 04b

            var step4b = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/07-Step-04B.pdf"
            };

            using (var graph = PdfImage.FromFile("~/Resources/Sample-11/Images/graph.png").ScaleTo(400))
            {
                step4b.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#ST4_GRPH#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = imageStylesTable["DefaultCenter"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = graph
                    })).Replace(new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#ST4_NSWR_1#",
                        NewText = "Comentarios de la herramienta",
                        Style = textStylesTable["ColoredComments"],
                        TextOffset = PointF.Empty,
                        UseTestMode = useTestMode,
                        ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                    })).Replace(new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#ST4_NSWR_2#",
                        NewText = "Comentarios de la herramienta",
                        Style = textStylesTable["ColoredComments"],
                        TextOffset = PointF.Empty,
                        UseTestMode = useTestMode,
                        ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                    }));
            }

            #endregion

            #endregion

            #region step 05

            var step5 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/08-Step-05.pdf"
            };

            using (var card = PdfImage.FromFile("~/Resources/Sample-11/Images/9_ImagenPath.jpg").ScaleTo(250))
            {
                step5.Replace(new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#ST5_OPTION#",
                        NewText = "Has escogido la opción 1",
                        Style = textStylesTable["BoldColoredComments"],
                        TextOffset = PointF.Empty,
                        UseTestMode = useTestMode,
                        ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                    })).Replace(new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#ST5_VALUE#",
                        NewText = "El valor que has escogido es:",
                        Style = textStylesTable["BoldColoredComments"],
                        TextOffset = PointF.Empty,
                        UseTestMode = useTestMode,
                        ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                    })).Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#ST5_CARD#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = imageStylesTable["CenteredCard"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins,
                        Image = card
                    }));
            }

            #endregion

            #region step 06

            var step6 = new PdfInput
            {
                AutoUpdateChanges = true,
                Input = "~/Resources/Sample-11/09-Step-06.pdf"
            };

            #endregion

            #endregion

            #region merge

            // Defines system tags to replace > page number
            var systemTags = new SystemTagsCollection
            {
                new PageNumberSystemTag
                {
                    UseTestMode = useTestMode,
                    TextOffset = PointF.Empty,
                    Style = textStylesTable["PageNumber"],
                    ReplaceOptions = ReplaceTextOptions.FromPositionToRightMargin
                }
            };

            // Defines global text replacements to replace > header text
            var globalReplacements = new GlobalReplacementsCollection
            {
                new WithTextObject
                {
                    Text = "#HEADER_FULL_NAME",
                    NewText = "Report Name - Lorem ipsum dolor",
                    Style = textStylesTable["Header"],
                    ReplaceOptions = ReplaceTextOptions.FromLeftMarginToNextElement,
                    UseTestMode = useTestMode,
                    TextOffset = PointF.Empty
                }
            };

            var files = new PdfObject(new PdfObjectConfig { Tags = systemTags, GlobalReplacements = globalReplacements })
            {
                Items = new List<PdfInput>
                {
                    new PdfInput {Index = 0, Input = cover},
                    new PdfInput {Index = 1, Input = index},
                    new PdfInput {Index = 2, Input = introduction},
                    new PdfInput {Index = 3, Input = step1},
                    new PdfInput {Index = 4, Input = step2},
                    new PdfInput {Index = 5, Input = step3},
                    new PdfInput {Index = 6, Input = step4a},
                    new PdfInput {Index = 7, Input = step4b},
                    new PdfInput {Index = 8, Input = step5},
                    new PdfInput {Index = 9, Input = step6}
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

            var saveResult = mergeResult.Value.Action(new SaveToFile { OutputPath = "~/Output/Sample11/Sample-11" });
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
            logger.Info("     > Path: ~/Output/Sample11/Sample-11.pdf");
            logger.Info($"   > Elapsed time: { ts.Hours:00}:{ ts.Minutes:00}:{ ts.Seconds:00}.{ ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
