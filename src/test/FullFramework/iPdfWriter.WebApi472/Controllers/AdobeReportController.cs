
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using iTin.Utilities.Pdf.Writer.Operations.Result.Actions;

namespace iPdfWriter.WebApi.Controllers
{
    using Code;

    public class AdobeReportController : ApiController
    { 
        public async Task GetAsync()
        {
            var result = await Sample01.GenerateAsync();
            if (result.Success)
            {
                var safeOutputData = result.Result;
                var downloadResult = await safeOutputData.Action(new DownloadAsync { Context = HttpContext.Current });
                if (!downloadResult.Success)
                {
                    // Handle error(s)
                }
            }
        }
    }
}
