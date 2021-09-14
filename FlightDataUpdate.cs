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
using Newtonsoft.Json;

namespace vNAAATS.API
{
    public static class FlightDataUpdate
    {
        [FunctionName("FlightDataUpdate")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "vnaaats-container",
                ConnectionStringSetting = "DbConnectionString")] 
                DocumentClient client,
            ILogger log)
        {
            try {
                // Check callsign
                string callsignQuery = req.Query["callsign"];
                if (string.IsNullOrWhiteSpace(callsignQuery))
                {
                    return (ActionResult)new NotFoundResult();
                }

                // URI for flight data collection
                Uri collectionUri = UriFactory.CreateDocumentCollectionUri("vnaaats-net", "vnaaats-container");

                // LINQ Query
                var doc = client.CreateDocumentQuery<FlightDataDocument>(collectionUri)
                    .Where(p => p.callsign == callsignQuery)
                    .AsEnumerable()
                    .FirstOrDefault();
                
                // Null check on doc
                if (doc == null) {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);  
                }

                /// Now we update all found values
                string type = req.Query["type"];
                if (!string.IsNullOrWhiteSpace(type))
                {
                    doc.type = type;
                }
                string newLevel = req.Query["level"];
                if (!string.IsNullOrWhiteSpace(newLevel) && newLevel.Length == 4)
                {
                    doc.assignedLevel = Int32.Parse(newLevel);
                }
                string newMach = req.Query["mach"];
                if (!string.IsNullOrWhiteSpace(newMach) && newMach.Length == 4)
                {
                    doc.assignedMach = Int32.Parse(newMach);
                }
                string track = req.Query["track"];
                if (!string.IsNullOrWhiteSpace(track))
                {
                    doc.track = track;
                }
                string route = req.Query["route"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.route = route;
                }
                string routeEtas = req.Query["routeEtas"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.routeEtas = routeEtas;
                }
                string departure = req.Query["departure"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.departure = departure;
                }
                string arrival = req.Query["arrival"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.arrival = arrival;
                }
                string newDirection = req.Query["direction"];
                if (!string.IsNullOrWhiteSpace(newDirection) && (newDirection == "0" || newDirection == "1"))
                {
                    doc.direction = newDirection == "1" ? true : false;
                }
                string newEtd = req.Query["etd"];
                if (!string.IsNullOrWhiteSpace(newEtd) && newEtd.Length == 4)
                {
                    doc.etd = newEtd;
                }
                string newSelcal = req.Query["selcal"];
                if (!string.IsNullOrWhiteSpace(newSelcal) && newSelcal.Length == 4)
                {
                    doc.selcal = newSelcal;
                }
                string newDatalink = req.Query["connectedDatalink"];
                if (!string.IsNullOrWhiteSpace(newDatalink) && (newDatalink == "0" || newDatalink == "1"))
                {
                    doc.connectedDatalink = newDatalink == "1" ? true : false;
                }
                string isEquipped = req.Query["isEquipped"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.isEquipped = isEquipped == "1" ? true : false;
                }
                string newState = req.Query["state"];
                if (!string.IsNullOrWhiteSpace(newState))
                {
                    doc.state = newState;
                }
                string newRelevant = req.Query["relevant"];
                if (!string.IsNullOrWhiteSpace(newRelevant) && (newRelevant == "0" || newRelevant == "1"))
                {
                    doc.relevant = newRelevant == "1" ? true : false;
                }
                string targetMode = req.Query["targetMode"];
                if (!string.IsNullOrWhiteSpace(targetMode))
                {
                    doc.targetMode = (TargetMode)Convert.ToInt32(targetMode);
                }
                string trackedBy = req.Query["trackedBy"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.trackedBy = trackedBy;
                }
                string trackedById = req.Query["trackedById"];
                if (!string.IsNullOrWhiteSpace(callsignQuery))
                {
                    doc.trackedById = trackedById;
                }

                // Update last updated
                doc.lastUpdated = DateTime.UtcNow;

                // And finally we replace
                await client.ReplaceDocumentAsync(doc);
                
                // Return 200 code
                return new StatusCodeResult(StatusCodes.Status200OK);  
            } 
            catch (Exception ex) 
            {
                // Catch any errors
                log.LogError($"Could not update flight data. Exception thrown: {ex.Message}.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);  
            }
            
        }
    }
}

