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
    public static class GetSingleNatTrack
    {
        [FunctionName("GetSingleNatTrack")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
        HttpRequest req, ILogger log)
        {
            // Get query terms
            char track = '@'; // we will never see this in a nat message so we use it as our default
            bool si = false;

            // Check track arg
            if (!string.IsNullOrWhiteSpace(req.Query["id"]) && req.Query["id"].Count == 1)
            {
                track =  req.Query["id"].ToString()[0];
            }
            // Check SI
            if (!string.IsNullOrWhiteSpace(req.Query["si"]) &&
                bool.TryParse(req.Query["si"], out var res))
            {
                if (res || !res)
                {
                    si = res;
                }
            }

            // Get the tracks
            List<Track> parsedTracks = TrackUtils.ParseTracks(si);
            Track returnObj = parsedTracks.Where(t => t.Id == track).FirstOrDefault();

            // Error if not found
            if (returnObj == null)
            {
                return new NotFoundObjectResult("The requested track was not found in the message.");
            }

            // Return the result if it is found
            return new OkObjectResult(returnObj);            
        }
    }
}
