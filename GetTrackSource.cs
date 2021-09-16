using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace vNAAATS.NET
{
    public static class GetTrackSource
    {
        [FunctionName("GetTrackSource")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // TODO: config file for this
            string res = "1"; // 1 = event, 0 = normal

            return (ActionResult)new OkObjectResult(res);
        }
    }
}

