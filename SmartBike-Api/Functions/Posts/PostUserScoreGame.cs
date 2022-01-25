using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.Azure.Cosmos;
using SmartBike_Api.Models;
using System.Collections.Generic;

namespace SmartBike_Api
{
    public static class PostUserScoreGame
    {
        [FunctionName("PostUserScoreGame")]
        public static async Task<IActionResult> PostGame(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "smartbike/game/{gameid}/{distance}/{speed}")] HttpRequest req, int gameid, int distance, float speed, //add authentication
            ILogger log)
        {
            log.LogInformation("Processing post request (game)");
     
            try
            {
                if (gameid > 0 || gameid < 3)
                {
                   
                    Game data = new Game
                    {
                        GameId = gameid,
                        //User = user,
                        Distance = distance,
                        Speed = speed,
                        id = Guid.NewGuid().ToString()

                    };
                    await AddToCosmosAsync(data);
                    return new OkObjectResult("Added");
                }
                return new BadRequestObjectResult($"no game found with id:{gameid}");
            }
            catch (Exception ex)
            {
                log.LogError($"{ex}");
                return new StatusCodeResult(500);
            }

            
        }
        public static async Task AddToCosmosAsync(Game data)
        {
            try
            {
                CosmosClientOptions options = new CosmosClientOptions();
                options.ConnectionMode = ConnectionMode.Gateway;
                CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
                Container container = client.GetContainer("GroupProject", "Games");
                ItemResponse<Game> response = await container.CreateItemAsync(data, new PartitionKey(data.GameId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
