using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace vNAAATS.NET
{
    public static class PostFlightData
    {
        [FunctionName("PostFlightData")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, 
            [CosmosDB("vnaaats-net", "flights",
                ConnectionStringSetting = "DbConnectionString")] IAsyncCollector<object> flights,
            ILogger log)
        {
            try {
                // Deserialise the request
                string callsign = req.Query["callsign"];
                string ac_type = req.Query["type"];
                int assignedLevel = Convert.ToInt32(req.Query["level"]);
                int assignedMach = Convert.ToInt32(req.Query["mach"]);
                string track = req.Query["track"];
                string route = req.Query["route"];
                string routeEtas = req.Query["routeEtas"];
                string departure = req.Query["departure"];
                string arrival = req.Query["arrival"];
                bool direction = req.Query["direction"] == "1" ? true : false;
                string etd = req.Query["etd"];
                string selcal = req.Query["selcal"];
                bool datalink = req.Query["connected_datalink"] == "1" ? true : false;
                if (string.IsNullOrWhiteSpace(req.Query["connectedDatalink"]))
                    datalink = false;
                else
                    datalink = req.Query["datalink"] == "1" ? true : false;
                bool isEquipped = req.Query["isEquipped"] == "1" ? true : false;
                string state = req.Query["state"];
                
                bool relevant = req.Query["relevant"] == "1" ? true : false;
                TargetMode targetMode = (TargetMode)Convert.ToInt32(req.Query["targetMode"]);
                string trackedBy = req.Query["trackedBy"];
                string trackedId = req.Query["trackedById"];

                // Create data object
                FlightData fdata = new FlightData
                {
                    callsign = callsign,
                    type = ac_type,
                    etd = etd,
                    assignedLevel = assignedLevel,
                    assignedMach = assignedMach,
                    track = track,
                    route = route,
                    routeEtas = routeEtas,
                    departure = departure,
                    arrival = arrival,
                    direction = direction,
                    selcal = selcal,
                    datalinkConnected = datalink,
                    isEquipped = isEquipped,
                    state = state,
                    relevant = relevant,
                    targetMode = targetMode,
                    trackedBy = trackedBy,
                    trackedById = trackedId,
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

