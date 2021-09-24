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
using vNAAATS.NET.Models;

namespace vNAAATS.NET
{
    public static class GetAllNamedFixes
    {
        [FunctionName("GetAllNamedFixes")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "fixes",
                ConnectionStringSetting = "DbConnectionString")] 
                DocumentClient client,
            ILogger log)
        {
            try {
                // URI for fixes collection
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "fixes");

                // LINQ Query
                IDocumentQuery<DBFix> query = client.CreateDocumentQuery<DBFix>(collectionUri, new FeedOptions { MaxItemCount = 1000 })
                    .AsDocumentQuery();

                // Get results
                List<DBFix> results = new List<DBFix>();
                foreach (DBFix fix in await query.ExecuteNextAsync()) {
                    results.Add(new DBFix(fix.name, fix.latitude, fix.longitude)); // add them all                    
                }

                // Return okay if found
                return new OkObjectResult(results);
            } 
            catch (Exception ex) 
            {
                // Catch any errors
                log.LogError($"Could not retrieve flight data. Exception thrown: {ex.Message}.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

