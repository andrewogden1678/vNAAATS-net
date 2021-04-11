using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace vNAAATS.API
{
    public static class FlightDataNewPost
    {
        [FunctionName("FlightDataNewPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, 
            [CosmosDB("vnaaats-net", "vnaaats-container",
                ConnectionStringSetting = "DbConnectionString")] IAsyncCollector<object> flights,
            ILogger log)
        {
            try {
                // Deserialise the request
                string request = req.Query["data"];
                dynamic data = JsonConvert.DeserializeObject(request);

                // Create data object
                FlightData fdata = new FlightData 
                {
                    callsign = (string)data.callsign,
                    assignedLevel = (int)data.assigned_level,
                    assignedMach = (int)data.assigned_mach,
                    track = (string)data.track,
                    route = (string)data.route,
                    routeEtas = (string)data.routeEtas,
                    departure = (string)data.departure,
                    arrival = (string)data.arrival,
                    isEquipped = (bool)data.is_equipped,
                    trackedBy = (string)data.tracked_by,
                    lastUpdated = DateTime.UtcNow
                };

                // Add data object
                await flights.AddAsync(fdata);
                
                // Log and return success code
                log.LogInformation($"Item {fdata.callsign} inserted successfully.");
                return new StatusCodeResult(StatusCodes.Status200OK);   
            }
            catch (Exception ex) 
            {
                // Catch any errors
                log.LogError($"Could not insert flight data. Exception thrown: {ex.Message}.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);   
            }
        }
    }
}

