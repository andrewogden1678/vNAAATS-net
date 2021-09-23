using Microsoft.AspNetCore.Http;
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
    public static class UpdateConfigEntry
    {
        [FunctionName("UpdateConfigEntry")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "config",
                ConnectionStringSetting = "DbConnectionString")]
            ILogger log)
        {
            try {
                // URI for flight data collection
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "config");
                // Log and return success code
                //log.LogInformation();
                return new StatusCodeResult(StatusCodes.Status200OK);   
            }
            catch (Exception ex) 
            {
                // Catch any errors
                log.LogError($"Could not insert waypoint. Exception thrown: {ex.Message}.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);   
            }
        }
    }
}

