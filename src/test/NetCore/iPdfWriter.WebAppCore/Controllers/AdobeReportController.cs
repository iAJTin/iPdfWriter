
using Microsoft.AspNetCore.Mvc;

using iTin.Core.ComponentModel;

using iTin.Utilities.Pdf.Writer.Operations.Result.Actions;

namespace iPdfWriter.WebAppCore.Controllers
{
    using Code;

    [ApiController]
    [Route("[controller]")]
    public class AdobeReportController : ControllerBase
    {
        private readonly ILogger<AdobeReportController> _logger;


        public AdobeReportController(ILogger<AdobeReportController> logger)
        {
            _logger = logger;
        }


        [HttpGet(Name = "GetAdobeReport")]
        public async void GetAsync()
        {
            _logger.LogInformation($"[{GetType().Name}][{nameof(GetAsync)}] Begin action.");

            var result = await Sample01.GenerateAsync();
            if (result.Success)
            {
                var safeOutputData = result.Result;
                var downloadResult = await safeOutputData.Action(new DownloadAsync { Context = HttpContext });
                if (!downloadResult.Success)
                {
                    _logger.LogInformation($"[{GetType().Name}][{nameof(GetAsync)}] > Error downloading file.");
                    _logger.LogInformation($"[{GetType().Name}][{nameof(GetAsync)}]   > Error: {downloadResult.Errors.AsMessages().ToStringBuilder()}");
                }
                else
                {
                    _logger.LogInformation($"[{GetType().Name}][{nameof(GetAsync)}] > Report created successfully with {(safeOutputData.IsZipped ? (await safeOutputData.GetOutputStreamAsync()).Length : (await safeOutputData.GetUnCompressedOutputStreamAsync()).Length)} bytes.");
                }

                return;
            }

            _logger.LogInformation($"[{GetType().Name}][{nameof(GetAsync)}] > Error creating output result.");
            _logger.LogInformation($"[{GetType().Name}][{nameof(GetAsync)}]   > Error: {result.Errors.AsMessages().ToStringBuilder()}");
        }
    }
}
