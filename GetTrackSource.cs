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

/*using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vNAAATS.NET
{
    public static class GetTrackSource
    {
        [FunctionName("GetTrackSource")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "config",
                ConnectionStringSetting = "DbConnectionString", Id = "{Query.content}")] 
                DocumentClient client,
            ILogger log)
        {

            // URI for config collection
            Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "config");

            // LINQ Query
            IDocumentQuery<ConfigItemDocument> query = client.CreateDocumentQuery<ConfigItemDocument>(collectionUri)
                .Where(p => p.id == "use_event_tracks")
                .AsDocumentQuery().Equals;


            return (ActionResult)new OkObjectResult((string)obj.source);
        }
    }
}
*/