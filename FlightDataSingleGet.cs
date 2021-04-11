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

namespace vNAAATS.API
{
    public static class FlightDataSingleGet
    {
        [FunctionName("FlightDataSingleGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "vnaaats-container",
                ConnectionStringSetting = "DbConnectionString")] 
                DocumentClient client,
            ILogger log)
        {
            try {
                // Get term and check null
                string queryTerm = req.Query["callsign"];
                if (string.IsNullOrWhiteSpace(queryTerm))
                {
                    return (ActionResult)new NotFoundResult();
                }

                // URI for flight data collection
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "vnaaats-container");

                // LINQ Query
                IDocumentQuery<FlightData> query = client.CreateDocumentQuery<FlightData>(collectionUri)
                    .Where(p => p.callsign == queryTerm)
                    .AsDocumentQuery();
                
                // Get results
                List<FlightData> results = new List<FlightData>();
                foreach (FlightData result in await query.ExecuteNextAsync()) {
                    results.Add(result);
                }

                // Return okay if found and 404 if not
                if (results.Count == 0) {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }
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

