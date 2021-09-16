using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
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
using Newtonsoft.Json;

namespace vNAAATS.NET
{
    public static class PurgeFlightDatas
    {
        [FunctionName("PurgeFlightDatas")]
        public static async void Run([TimerTrigger("0 */10 * * * *")]TimerInfo Timer,
        [CosmosDB("vnaaats-net", "flights",
                ConnectionStringSetting = "DbConnectionString")] 
                DocumentClient client,
                ILogger log)
        {
            try {
                // URI for flight data collection
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "flights");

                // LINQ Query
                IDocumentQuery<FlightDataDocument> query = client.CreateDocumentQuery<FlightDataDocument>(collectionUri)
                    .AsDocumentQuery();
                
                // Check all results
                List<FlightData> results = new List<FlightData>();
                int counter = 0;
                foreach (FlightDataDocument result in await query.ExecuteNextAsync()) {
                    // Get time span
                    TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - result.lastUpdated.Ticks);
                    double timeDelta = Math.Abs(ts.TotalSeconds);

                    // Check the total seconds and if it is longer than 4 hours
                    if (timeDelta > 18000) {
                        // Delete
                        await client.DeleteDocumentAsync(result.SelfLink, new RequestOptions { PartitionKey = new PartitionKey(result.callsign) });
                        // Increment counter
                        counter++;
                    }
                }

                log.LogInformation($"{counter} flight data structures purged at: {DateTime.Now}");
            } 
            catch (Exception ex) 
            {
                // Catch any errors
                log.LogError($"Could not purge flight data. Exception thrown: {ex.Message}.");
            }
        }
    }
}
