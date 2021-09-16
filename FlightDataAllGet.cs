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
    public static class FlightDataAllGet
    {
        [FunctionName("FlightDataAllGet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "flights",
                ConnectionStringSetting = "DbConnectionString")] 
                DocumentClient client,
            ILogger log)
        {
            try {
                /// PARAMETERS
                int sorting = -1; // Sorting -> -1: not found in request, 0: ascending, 1: descending
                bool relevant = false; // We get all by default

                // Grab the sort parameter if it exists
                string queryTerm = req.Query["sort"];
                if (!string.IsNullOrWhiteSpace(queryTerm) && int.TryParse(queryTerm, out var res))
                {
                    // Parse to sorting if it exists
                    if (res > -1 && res < 2) {
                        sorting = Int32.Parse(queryTerm);
                    }
                }

                // Get all only relevant
                queryTerm = req.Query["relevant"];
                if (!string.IsNullOrWhiteSpace(queryTerm)) {
                    if (queryTerm.ToLower() == "true") {
                        relevant = true;
                    }
                }

                // URI for flight data collection
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "flights");

                // LINQ Query
                IDocumentQuery<FlightData> query = client.CreateDocumentQuery<FlightData>(collectionUri)
                    .AsDocumentQuery();
                
                // Get results
                List<FlightData> results = new List<FlightData>();
                foreach (FlightData result in await query.ExecuteNextAsync()) {
                    if (relevant && result.relevant) // if relevant then only add if the property is true in the flight object
                    {
                        results.Add(result);
                        continue;
                    }
                    results.Add(result); // add them all
                }
                
                // Sort if needed
                if (sorting != -1) {
                    if (sorting == 0) // ascending
                    {
                        results.Sort((x, y) => x.assignedLevel.CompareTo(y.assignedLevel));
                    } 
                    else // descending
                    {
                        results.Sort((x, y) => y.assignedLevel.CompareTo(x.assignedLevel));
                    }
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

