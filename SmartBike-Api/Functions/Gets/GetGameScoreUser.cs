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
using SmartBike_Api.Models;
using System.Collections.Generic;

namespace SmartBike_Api.Functions.Gets
{
    public static class GetGameScoreUser
    {
        [FunctionName("GetGameScoreUser")]
        public static async Task<IActionResult> GetGamScoreUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smartbike/game/{gameid}/{user}")] HttpRequest req, int gameid, string user,
            ILogger log)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            Container container = client.GetContainer("GroupProject", "Games");
            QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId and i.user = @user").WithParameter("@gameId", gameid).WithParameter("@user", user);

            List<Game> items = new List<Game>();
            using (FeedIterator<Game> resultSet = container.GetItemQueryIterator<Game>(queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<Game> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return new OkObjectResult(items);
        }
    }
}
