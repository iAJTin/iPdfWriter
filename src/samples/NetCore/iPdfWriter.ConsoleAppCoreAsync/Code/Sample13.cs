
using System.Diagnostics;
using System.Drawing;

using iTin.Core.ComponentModel;
using iTin.Core.Models.Design.Enums;

using iTin.Logging.ComponentModel;

using iTin.Utilities.Pdf.Design.Image;
using iTin.Utilities.Pdf.Design.Styles;
using iTin.Utilities.Pdf.Design.Table;

using iPdfWriter.Operations.Actions;
using iPdfWriter.Operations.Replace;
using iPdfWriter.Operations.Replace.Replacement.Text;

namespace iPdfWriter.ConsoleApp.Code
{
    using ComponentModel.Helpers;

    /// <summary>
    /// Shows the use of text and image replacement in a pdf document asynchronously.
    /// </summary>
    internal static class Sample13
    {
        public static async Task GenerateAsync(ILogger logger, YesNo useTestMode = YesNo.No, CancellationToken cancellationToken = default)
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

            #region Section: Header

            doc.Replace(
                new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#TITLE#",
                        NewText = "Lorem ipsum",
                        UseTestMode = useTestMode,
                        Offset = new PointF(0.0f, -10.0f),
                        Style = StylesHelper.Sample13.TextStylesTable["MainTitle"],
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
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["RazonSocial"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }));

            #endregion

            #region Section: Applicant data

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#RAZONSOCIAL#",
                            NewText = "Sample razón social",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["RazonSocial"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#PERSONASOLICITANTE#",
                            NewText = "Nombre persona",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["PersonaSolicitante"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#TELEFONOSOLICITANTE#",
                            NewText = "932645687",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["TelefonoSolicitante"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#EMAILSOLICITANTE#",
                            NewText = "some@domain.com",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["EmailSolicitante"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }));

            #endregion

            #region Section: Performance data

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#FECHAINICIO#",
                            NewText = "01/01/2022",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["FechaInicio"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#FECHAACABADO#",
                            NewText = "31/12/2022",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["FechaAcabado"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#HORARIO#",
                        NewText = "09:00h - 18:30h",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = StylesHelper.Sample13.TextStylesTable["Horario"],
                        ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                    }));

            #endregion

            #region Section: Enterprise data

            doc
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#RAZONSOCIALEMPRESA#",
                            NewText = "Empresa test",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["RazonSocialEmpresa"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#CONTACTOEMPRESA#",
                            NewText = "Persona contacto empresa",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["ContactoEmpresa"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                    new ReplaceText(
                        new WithTextObject
                        {
                            Text = "#TELEFONOEMPRESA#",
                            NewText = "699233665",
                            UseTestMode = useTestMode,
                            Offset = PointF.Empty,
                            Style = StylesHelper.Sample13.TextStylesTable["TelefonoEmpresa"],
                            ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                        }))
                .Replace(
                new ReplaceText(
                    new WithTextObject
                    {
                        Text = "#EMAILEMPRESA#",
                        NewText = "some@companydomain.com",
                        UseTestMode = useTestMode,
                        Offset = PointF.Empty,
                        Style = StylesHelper.Sample13.TextStylesTable["EmailEmpresa"],
                        ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement
                    }));

            #endregion

            #region Section: HTML table

            doc.Replace(new ReplaceText(
                new WithTableObject
                {
                    Text = "#SAMPLETABLE#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = PdfTableStyle.Default,
                    ReplaceOptions = ReplaceTextOptions.FromPositionToNextElement,
                    Table = await PdfTable.CreateFromHtmlAsync(
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
                        config: new PdfTableConfig { HeightStrategy = TableHeightStrategy.Auto })
                }));

            #endregion

            #region Section: Firmas

            doc.Replace(new ReplaceText(
                new WithImageObject
                {
                    Text = "#FIRMA1#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample13.ImagesStylesTable["Default"],
                    ReplaceOptions = ReplaceTextOptions.Default,
                    Image = PdfImage.FromFile("~/Resources/Sample-13/Images/Firma1.png").ScaleTo(200, ScaleStrategy.Horizontal)
                }));

            doc.Replace(new ReplaceText(
                new WithImageObject
                {
                    Text = "#FIRMA2#",
                    UseTestMode = useTestMode,
                    Offset = PointF.Empty,
                    Style = StylesHelper.Sample13.ImagesStylesTable["Default"],
                    ReplaceOptions = ReplaceTextOptions.Default,
                    Image = PdfImage.FromFile("~/Resources/Sample-13/Images/Firma2.jpg").ScalePercent(90)
                }));

            #endregion

            #endregion

            #region Create output result

            var result = await doc.CreateResultAsync(cancellationToken: cancellationToken);
            if (!result.Success)
            {
                logger.Info("   > Error creating output result");
                logger.Info($"     > Error: {result.Errors.AsMessages().ToStringBuilder()}");

                return;
            }

            #endregion

            #region Saves output result

            var saveResult = await result.Result.Action(new SaveToFileAsync { OutputPath = "~/Output/Sample13/Sample-13" }, cancellationToken);
            
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
