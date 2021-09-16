using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using vNAAATS.NET.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using vNAAATS.NET.Utils;

namespace vNAAATS_net
{
    public static class GetAllNatTracks
    {
        [FunctionName("GetAllNatTracks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req, ILogger log)
        {
            // Get query terms
            bool si = false;
            bool eventTracks = false;
            bool concorde = false;

            // Check SI
            if (!string.IsNullOrWhiteSpace(req.Query["si"]) &&
                bool.TryParse(req.Query["si"], out var res))
            {
                if (res || !res)
                {
                    si = res;
                }
            }
            // Check event tracks
            if (string.IsNullOrWhiteSpace(req.Query["concorde"]) &&
                !string.IsNullOrWhiteSpace(req.Query["event"]) &&
                bool.TryParse(req.Query["event"], out var res1))
            {
                if (res1 || !res1)
                {
                    eventTracks = res1;
                }
            }
            // Check SI
            if (string.IsNullOrWhiteSpace(req.Query["event"]) &&
                !string.IsNullOrWhiteSpace(req.Query["concorde"]) &&
                bool.TryParse(req.Query["concorde"], out var res2))
            {
                if (res2 || !res2)
                {
                    concorde = res2;
                }
            }

            // Get the tracks
            List<Track> parsedTracks = new List<Track>();

            // Return the results
            if (eventTracks)
            {
                // Event path
                string path = "https://cdn.ganderoceanic.com/resources/data/eventTracks.json";
                // Return
                using (WebClient client = new WebClient())
                {
                    return new OkObjectResult(client.DownloadString(path));
                }
            }
            else if (concorde)
            {
                // Concorde track path
                string path = "https://ams3.digitaloceanspaces.com/ganderoceanicoca/resources/data/concordeTracks.json";

                // Return
                using (WebClient client = new WebClient())
                {
                    return new OkObjectResult(client.DownloadString(path));
                }
            }
            else
            {
                // Assign and return
                parsedTracks = TrackUtils.ParseTracks(si);
                return new OkObjectResult(parsedTracks);
            }
        }
    }
}
