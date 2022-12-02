
using System.Diagnostics;

using iTin.Core.ComponentModel;
using iTin.Logging.ComponentModel;

using iPdfWriter.Operations.Actions;

namespace iPdfWriter.ConsoleApp.Code
{
    /// <summary>
    /// Show how to creates a pdf input from html asynchronously.
    /// </summary>
    internal static class Sample25
    {
        public static async Task GenerateAsync(ILogger logger, CancellationToken cancellationToken = default)
        {
            #region Initialize timer

            var sw = new Stopwatch();
            sw.Start();

            #endregion

            #region Creates pdf input

            var doc = PdfInput.CreateFromHtml(
                html: @"
                        <div>
                          <p style='font-size:26pt; font-family:Arial; text-align: center;'><strong>#TITLE#</strong></p>
                        </div>
                        <div style='font-size:10.5pt; font-family:Arial; text-align: left;'>
                          <p style='font-size:18pt; font-family:Arial; text-align: left;'><strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac faucibus odio.</strong></p>
                          <p>
                            Vestibulum neque massa, scelerisque sit amet ligula eu, congue molestie mi. Praesent ut varius sem. Nullam at porttitor arcu, nec lacinia nisi. Ut ac dolor vitae odio interdum condimentum. 
                            <strong>Vivamus dapibus sodales ex, vitae malesuada ipsum cursus convallis. Maecenas sed egestas nulla, ac condimentum orci.</strong> Mauris diam felis, vulputate ac suscipit et, iaculis non est. 
                            Curabitur semper arcu ac ligula semper, nec luctus nisl blandit. Integer lacinia ante ac libero lobortis imperdiet. Nullam mollis convallis ipsum, ac accumsan nunc vehicula vitae. 
                            Nulla eget justo in felis tristique fringilla. Morbi sit amet tortor quis risus auctor condimentum. Morbi in ullamcorper elit. Nulla iaculis tellus sit amet mauris tempus fringilla
                          </p>
                          <p>Maecenas mauris lectus, lobortis et purus mattis, blandit dictum tellus.</p>
                          <p>
                            <ul>
                              <li><strong>Maecenas non lorem quis tellus placerat varius.</strong></li>
                              <li><em>Nulla facilisi.</em></li>
                              <li style='text-decoration: underline;'>Aenean congue fringilla justo ut aliquam.</li>
                              <li><span style='text-decoration: underline;'>Mauris id ex erat.</span> Nunc vulputate neque vitae justo facilisis, non condimentum ante sagittis.</li>
                              <li>Morbi viverra semper lorem nec molestie</li>
                              <li>Maecenas tincidunt est efficitur ligula euismod, sit amet ornare est vulputate</li>
                            </ul>
                          </p>
                        </div>
                        <div style='height: 420px;'>
                          <p style='font-size:12pt; font-family:Arial; text-align: left;'>#BAR-CHART#</p>
                        </div>
                        <div style='font-size:10.5pt; font-family:Arial; text-align: left;'>
                          <p>
                            In non mauris justo. Duis vehicula mi vel mi pretium, a viverra erat efficitur. Cras aliquam est ac eros
                            varius, id iaculis dui auctor. Duis pretium neque ligula, et pulvinar mi placerat et. Nulla nec nunc sit
                            amet nunc posuere vestibulum. Ut id neque eget tortor mattis tristique. Donec ante est, blandit sit amet
                            tristique vel, lacinia pulvinar arcu. Pellentesque scelerisque fermentum erat, id posuere justo pulvinar
                            ut. Cras id eros sed enim aliquam lobortis. Sed lobortis nisl ut eros efficitur tincidunt. Cras justo mi, 
                            porttitor quis mattis vel, ultricies ut purus. Ut facilisis et lacus eu cursus.
                          </p>
                          <p>
                            In eleifend velit vitae libero sollicitudin euismod. Fusce vitae vestibulum velit. Pellentesque vulputate
                            lectus quis pellentesque commodo. Aliquam erat volutpat. Vestibulum in egestas velit. Pellentesque 
                            fermentum nisl vitae fringilla venenatis. Etiam id mauris vitae orci maximus ultricies.
                          </p> 
                        </div>
                        <div>
                          <p style='font-size:18pt; font-family:Arial; text-align: left;'><strong>Cras fringilla ipsum magna, in fringilla dui commodo a.</strong></p>
                        </div>
                        <div style='font-size:10.5pt; font-family:Arial; text-align: left;'>
                            <table border='1' cellspacing='0' cellpadding='6' style='width:100%'>
                              <tbody>
                                <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
                                  <td>&nbsp;</td>
                                  <td>Lorem ipsum</td>
                                  <td>Lorem ipsum</td>
                                  <td>Lorem ipsum</td>
                                </tr>
                                <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
                                  <td>1</td>
                                  <td>In eleifend velit vitae libero sollicitudin euismod.</td>
                                  <td>Lorem</td>
                                  <td>&nbsp;</td>
                                </tr>
                                <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
                                  <td>2</td>
                                  <td>Cras fringilla ipsum magna, in fringilla dui commodo a.</td>
                                  <td>Lorem</td>
                                  <td>&nbsp;</td>
                                </tr>
                                <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
                                  <td>3</td>
                                  <td>LAliquam erat volutpat.</td>
                                  <td>Lorem</td>
                                  <td>&nbsp;</td>
                                </tr>
                                <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
                                  <td>4</td>
                                  <td>Fusce vitae vestibulum velit. </td>
                                  <td>Lorem</td>
                                  <td>&nbsp;</td>
                                </tr>
                                <tr style='font-size:10.5pt; font-family:Arial; color:#404040; text-align: left;'>
                                  <td>5</td>
                                  <td>Etiam vehicula luctus fermentum.</td>
                                  <td>Ipsum</td>
                                  <td>&nbsp;</td>
                                </tr>
                              </tbody>
                            </table>
                        </div>
                        <div style='font-size:10.5pt; font-family:Arial; text-align: left;'>
                          <p>
                            Etiam vehicula luctus fermentum. In vel metus congue, pulvinar lectus vel, fermentum dui. Maecenas 
                            ante orci, egestas ut aliquet sit amet, sagittis a magna. Aliquam ante quam, pellentesque ut dignissim 
                            quis, laoreet eget est. Aliquam erat volutpat. Class aptent taciti sociosqu ad litora torquent per conubia
                            nostra, per inceptos himenaeos. Ut ullamcorper justo sapien, in cursus libero viverra eget. Vivamus
                            auctor imperdiet urna, at pulvinar leo posuere laoreet. Suspendisse neque nisl, fringilla at iaculis
                            scelerisque, ornare vel dolor. Ut et pulvinar nunc. Pellentesque fringilla mollis efficitur. Nullam venenatis
                            commodo imperdiet. Morbi velit neque, semper quis lorem quis, efficitur dignissim ipsum. Ut ac lorem
                            sed turpis imperdiet eleifend sit amet id sapien
                          </p>
                        </div>                
                        <div style='font-size:10.5pt; font-family:Arial; text-align: left;'>
                          <p style='font-size:18pt; font-family:Arial; text-align: left;'><strong>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</strong></p>
                          <p>
                            Nunc ac faucibus odio. Vestibulum neque massa, scelerisque sit amet ligula eu, congue molestie mi. 
                            Praesent ut varius sem. Nullam at porttitor arcu, nec lacinia nisi. Ut ac dolor vitae odio interdum 
                            condimentum. Vivamus dapibus sodales ex, vitae malesuada ipsum cursus convallis. Maecenas sed 
                            egestas nulla, ac condimentum orci. Mauris diam felis, vulputate ac suscipit et, iaculis non est.
                            Curabitur semper arcu ac ligula semper, nec luctus nisl blandit. Integer lacinia ante ac libero lobortis
                            imperdiet. Nullam mollis convallis ipsum, ac accumsan nunc vehicula vitae. Nulla eget justo in felis
                            tristique fringilla. Morbi sit amet tortor quis risus auctor condimentum. Morbi in ullamcorper elit. Nulla 
                            iaculis tellus sit amet mauris tempus fringilla.
                          </p>
                        </div>
                        <div style='font-size:10.5pt; font-family:Arial; text-align: left;'>
                          <p style='font-size:18pt; font-family:Arial; text-align: left;'><strong>Maecenas mauris lectus, lobortis et purus mattis, blandit dictum tellus.</strong></p>
                          <p>
                            Maecenas non lorem quis tellus placerat varius. Nulla facilisi. Aenean congue fringilla justo ut aliquam.
                            Mauris id ex erat. Nunc vulputate neque vitae justo facilisis, non condimentum ante sagittis. Morbi 
                            viverra semper lorem nec molestie. Maecenas tincidunt est efficitur ligula euismod, sit amet ornare est 
                            vulputate.
                          </p>
                          <p>
                            In non mauris justo. Duis vehicula mi vel mi pretium, a viverra erat efficitur. Cras aliquam est ac eros 
                            varius, id iaculis dui auctor. Duis pretium neque ligula, et pulvinar mi placerat et. Nulla nec nunc sit 
                            amet nunc posuere vestibulum. Ut id neque eget tortor mattis tristique. Donec ante est, blandit sit amet 
                            tristique vel, lacinia pulvinar arcu. Pellentesque scelerisque fermentum erat, id posuere justo pulvinar
                            ut. Cras id eros sed enim aliquam lobortis. Sed lobortis nisl ut eros efficitur tincidunt. Cras justo mi,
                            porttitor quis mattis vel, ultricies ut purus. Ut facilisis et lacus eu cursus.
                          </p>
                        </div>
                        <div style='height: 1000px; font-size:10.5pt; font-family:Arial; text-align: left;'>
                          <p style='font-size:18pt; font-family:Arial; text-align: left;'><strong>In eleifend velit vitae libero sollicitudin euismod.</strong></p>
                          <p>
                            Fusce vitae vestibulum velit. Pellentesque vulputate lectus quis pellentesque commodo. Aliquam erat 
                            volutpat. Vestibulum in egestas velit. Pellentesque fermentum nisl vitae fringilla venenatis. Etiam id 
                            mauris vitae orci maximus ultricies. Cras fringilla ipsum magna, in fringilla dui commodo a.
                          </p>
                          <p>
                            Etiam vehicula luctus fermentum. In vel metus congue, pulvinar lectus vel, fermentum dui. Maecenas 
                            ante orci, egestas ut aliquet sit amet, sagittis a magna. Aliquam ante quam, pellentesque ut dignissim
                            quis, laoreet eget est. Aliquam erat volutpat. Class aptent taciti sociosqu ad litora torquent per conubia 
                            nostra, per inceptos himenaeos. Ut ullamcorper justo sapien, in cursus libero viverra eget. Vivamus 
                            auctor imperdiet urna, at pulvinar leo posuere laoreet. Suspendisse neque nisl, fringilla at iaculis 
                            scelerisque, ornare vel dolor. Ut et pulvinar nunc. Pellentesque fringilla mollis efficitur. Nullam venenatis 
                            commodo imperdiet. Morbi velit neque, semper quis lorem quis, efficitur dignissim ipsum. Ut ac lorem 
                            sed turpis imperdiet eleifend sit amet id sapien.
                          </p>
                        </div>
                        <p style='page-break-after: always'></p>
                        <br>
                        <div>
                          <p style='font-size:12pt; font-family:Arial; text-align: left;'>#IMAGE1#</p>
                        </div>");
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

            var saveResult = await result.Result.Action(new SaveToFileAsync { OutputPath = "~/Output/Sample25/Sample-25" }, cancellationToken);
            
            var ts = sw.Elapsed;
            sw.Stop();

            if (!saveResult.Success)
            {
                logger.Info("   > Error while saving to disk");
                logger.Info($"     > Error: {saveResult.Errors.AsMessages().ToStringBuilder()}");
                return;
            }

            logger.Info("   > Saved to disk correctly");
            logger.Info("     > Path: ~/Output/Sample25/Sample-25.pdf");
            logger.Info($"   > Elapsed time: {ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}");

            #endregion
        }
    }
}
