using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Net;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using vNAAATS.NET.Models;

namespace vNAAATS.NET
{
    public static class PostNamedFix
    {
        [FunctionName("PostNamedFix")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB("vnaaats-net", "fixes",
                ConnectionStringSetting = "DbConnectionString")] IAsyncCollector<object> fixes,
            ILogger log)
        {
            try {
                // Get name
                string name = "";
                if (!string.IsNullOrWhiteSpace(req.Query["name"])) {
                    name = req.Query["name"].ToString().ToUpper();
                }
                // Get lat
                string lat = "0.0";
                if (!string.IsNullOrWhiteSpace(req.Query["lat"]) &&
                    double.TryParse(req.Query["lat"], out var res)) {
                    lat = res.ToString();
                }
                // Get lon
                string lon = "0.0";
                if (!string.IsNullOrWhiteSpace(req.Query["lon"]) &&
                    double.TryParse(req.Query["lon"], out var res1)) {
                    lon = res1.ToString();
                }
                
                // Catch bad format
                if (name == "" || lat == "0.0" || lon == "0.0") {
                    return new BadRequestObjectResult("Invalid request. Ensure correct format: ?name=[string]&lat=[double]&lon=[double].");
                }

                // We got here so generate new fix and add data object
                DBFix fix = new DBFix(name, lat, lon);
                await fixes.AddAsync(fix);
                
                // Log and return success code
                //log.LogInformation();
                return new OkObjectResult($"Fix successfully added.\nName: {fix.name} \nLatitude: {fix.latitude}\nLongitude: {fix.longitude}");   
            }
            catch (Exception ex) 
            {
                // Catch any errors
                log.LogError($"Could not insert waypoint. Exception thrown: {ex.Message}.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);   
            }
        }
    }
}

