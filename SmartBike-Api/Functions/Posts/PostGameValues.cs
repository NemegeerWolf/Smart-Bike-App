using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SmartBike_Api
{
    public static class PostGameValues
    {
        [FunctionName("PostGame")]
        public static async Task<IActionResult> PostGame(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "smartbike/{gameId}/{user}/{distance}/{speed}")] HttpRequest req, int gameId,string user, int distance, float speed, //add authentication
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger PostGame processed a request.");

            try
            {
                if (gameId > 0 || gameId < 3)
                {
                    if (gameId == 1) //piano
                    {
                        return new OkObjectResult($"{user} survived {distance}% and had a speed of {speed}");
                    }
                    else if (gameId == 2) //overflow
                    {
                        return new OkObjectResult("");
                    }
                    else if (gameId == 3) //hillclimber
                    {
                        return new OkObjectResult("");
                    }
                }
                return new BadRequestObjectResult($"no game found with id:{gameId}");
            }
            catch (Exception ex)
            {
                log.LogError($"{ex}");
                return new StatusCodeResult(500);
            }

            
        }
    }
}
