using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using SmartBike_Api.Models;

namespace SmartBike_Api.Functions.Gets
{
    public static class GetAllScoresGame
    {
        [FunctionName("GetAllScoresGame")]
        public static async Task<IActionResult> GetGameScoreAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smartbike/game/{gameid}")] HttpRequest req, int gameid,
            ILogger log)
        {
            if (gameid == 1)
            {
                QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId order by i.speed desc").WithParameter("@gameId", gameid);
                List<Game> items = await GetScoresAsync(query);
                return new OkObjectResult(items);
            }
            else if (gameid == 2)
            {
                QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId order by speed desc").WithParameter("@gameId", gameid);
                return new OkObjectResult(GetScoresAsync(query));
            }
            else if (gameid == 3)
            {
                QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId order by distance desc").WithParameter("@gameId", gameid);
                return new OkObjectResult(GetScoresAsync(query));
            }
            return new BadRequestObjectResult($"no game found with id:{gameid}");


        }
        public static async Task<List<Game>> GetScoresAsync(QueryDefinition query) {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            Container container = client.GetContainer("GroupProject", "Games");

            List<Game> items = new List<Game>();
            using (FeedIterator<Game> resultSet = container.GetItemQueryIterator<Game>(queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<Game> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            return items;
        }
    }
}
