
namespace iPdfWriter.Code
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;

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
    /// Shows the use of text and image replacement in a pdf document.
    /// </summary>
    internal static class Sample13
    {
        // Image styles
        private static readonly Dictionary<string, PdfImageStyle> ImagesStylesTable = new Dictionary<string, PdfImageStyle>
        {
            {
                "Default",
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
            }
        };

        // Text styles
        private static readonly Dictionary<string, PdfTextStyle> TextStylesTable = new Dictionary<string, PdfTextStyle>
        {
            {
                "MainTitle",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 18.0f,
                        Bold = YesNo.Yes,
                        Italic = YesNo.No,
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
            {
                "RazonSocial",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Green"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "PersonaSolicitante",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Green"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "TelefonoSolicitante",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Green"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "EmailSolicitante",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Green"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "FechaInicio",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Red",
                        Bold = YesNo.Yes
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "FechaAcabado",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Red",
                        Bold = YesNo.Yes
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "Horario",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "Red",
                        Bold = YesNo.Yes
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "RazonSocialEmpresa",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "#AC1198"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "ContactoEmpresa",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "#AC1198"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "TelefonoEmpresa",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "#AC1198"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            },
            {
                "EmailEmpresa",
                new PdfTextStyle
                {
                    Font =
                    {
                        Name = "Arial",
                        Size = 10.0f,
                        Color = "#AC1198"
                    },
                    Content =
                    {
                        Alignment =
                        {
                            Vertical = KnownVerticalAlignment.Top
                        }
                    }
                }
            }
       };


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
                Input = "~/Resources/Sample-13/file-sample.pdf"
            };

            #endregion

            #region Replace actions

            #region Section: Cabecera

            doc.Replace(
                new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#TITLE#",
                        NewText = "Lorem ipsum",
                        UseTestMode = useTestMode,
                        TextOffset = new PointF(0.0f, -10.0f),
                        Style = TextStylesTable["MainTitle"],
                        ReplaceOptions = ReplaceTextOptions.AccordingToMargins
                    }));

            #endregion

            #region Section: Date

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#DATE_ENTREGA#",
                            NewText = DateTime.Now.ToShortDateString(),
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["RazonSocial"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }));

            #endregion

            #region Section: Datos solicitante

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#RAZONSOCIAL#",
                            NewText = "Sample razón social",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["RazonSocial"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#PERSONASOLICITANTE#",
                            NewText = "Nombre persona",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["PersonaSolicitante"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#TELEFONOSOLICITANTE#",
                            NewText = "932645687",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["TelefonoSolicitante"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#EMAILSOLICITANTE#",
                            NewText = "some@domain.com",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["EmailSolicitante"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }));

            #endregion

            #region Section: Datos actuación

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#FECHAINICIO#",
                            NewText = "01/01/2022",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["FechaInicio"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#FECHAACABADO#",
                            NewText = "31/12/2022",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["FechaAcabado"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#HORARIO#",
                        NewText = "09:00h - 18:30h",
                        UseTestMode = useTestMode,
                        TextOffset = PointF.Empty,
                        Style = TextStylesTable["Horario"],
                        ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                    }));

            #endregion

            #region Section: Datos empresa

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#RAZONSOCIALEMPRESA#",
                            NewText = "Empresa test",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["RazonSocialEmpresa"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#CONTACTOEMPRESA#",
                            NewText = "Persona contacto empresa",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["ContactoEmpresa"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#TELEFONOEMPRESA#",
                            NewText = "699233665",
                            UseTestMode = useTestMode,
                            TextOffset = PointF.Empty,
                            Style = TextStylesTable["TelefonoEmpresa"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#EMAILEMPRESA#",
                        NewText = "some@companydomain.com",
                        UseTestMode = useTestMode,
                        TextOffset = PointF.Empty,
                        Style = TextStylesTable["EmailEmpresa"],
                        ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                    }));

            #endregion

            #region Section: Tabla HTML

            doc.Replace(new ReplaceText(
                new WithTableObject
                {
                    Text = "#SAMPLETABLE#",
                    UseTestMode = useTestMode,
                    TableOffset = PointF.Empty,
                    Style = PdfTableStyle.Default,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement,
                    Table = PdfTable.CreateFromHtml(
                        html: @"
                             <table>
                              <tbody>
                               <tr style='font-size: 14pt; font-family: Arial; font-weight: bold; color: #12EAE3; text-align: left;'>
                                <td>&nbsp;</td>
                                <td>Lorem</td>
                               </tr>
                               <tr>
                                <td>1</td>
                                <td>In eleifend velit vitae libero sollicitudin.</td>
                               </tr>
                               <tr>
                                <td>2</td>
                                <td>Cras fringilla ipsum magna, in fringilla dui.</td>
                               </tr>
                               <tr>
                                <td>3</td>
                                <td>LAliquam erat volutpat.</td>
                               </tr>
                               <tr>
                                <td>4</td>
                                <td>Fusce vitae vestibulum velit. </td>
                               </tr>
                               <tr>
                                <td>5</td>
                                <td>Etiam vehicula luctus fermentum.</td>
                               </tr>
                              </tbody>
                             </table>",
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
                            }",
                        config: new PdfTableConfig {HeightStrategy = TableHeightStrategy.Auto})
                }));

            #endregion

            #region Section: Firmas

            using (var sign1 = PdfImage.FromFile("~/Resources/Sample-13/Images/Firma1.png").ScaleTo(200, ScaleStrategy.Horizontal))
            {
                doc.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#FIRMA1#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = ImagesStylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.Default,
                        Image = sign1
                    }));
            }

            using (var sign2 = PdfImage.FromFile("~/Resources/Sample-13/Images/Firma2.jpg").ScalePercent(90))
            {
                doc.Replace(new ReplaceText(
                    new WithImageObject
                    {
                        Text = "#FIRMA2#",
                        UseTestMode = useTestMode,
                        ImageOffset = PointF.Empty,
                        Style = ImagesStylesTable["Default"],
                        ReplaceOptions = ReplaceTextOptions.Default,
                        Image = sign2
                    }));
            }

            #endregion

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

            var saveResult = result.Result.Action(new SaveToFile { OutputPath = "~/Output/Sample13/Sample-13" });
            var ts = sw.Elapsed;
            sw.Stop();

            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample13/Sample-13.pdf");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
