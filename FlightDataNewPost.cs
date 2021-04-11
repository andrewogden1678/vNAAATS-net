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
                string callsign = req.Query["callsign"];
                int assigned_level = Convert.ToInt32(req.Query["level"]);
                int assigned_mach = Convert.ToInt32(req.Query["mach"]);
                string track = req.Query["track"];
                string route = req.Query["route"];
                string routeEtas = req.Query["routeEtas"];
                string departure = req.Query["departure"];
                string arrival = req.Query["arrival"];
                bool isEquipped = req.Query["isEquipped"] == "1" ? true : false;
                string tracked_by = req.Query["trackedBy"];

                // TODO: Add ETAs
                // Create data object
                FlightData fdata = new FlightData 
                {
                    callsign = callsign,
                    assignedLevel = assigned_level,
                    assignedMach = assigned_mach,
                    track = track,
                    route = route,
                    routeEtas = routeEtas,
                    departure = departure,
                    arrival = arrival,
                    isEquipped = isEquipped,
                    trackedBy = tracked_by,
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

