using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace vNAAATS.NET
{
    public static class GetTrackSource
    {
        [FunctionName("GetTrackSource")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Download and parse from config file
            string reqLink = "https://cdn.ganderoceanic.ca/resources/data/track_source.json";
            string json =  new WebClient().DownloadString(reqLink);
            dynamic obj = JObject.Parse(json);

            return (ActionResult)new OkObjectResult((string)obj.source);
        }
    }
}

