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

namespace SmartBike_Api.Functions.Puts
{
    public static class PutNameScore
    {
        [FunctionName("PutNameScore")]
        public static async Task<IActionResult> UpdateNameScore(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "smartbike/game/name/{name}/{id}")] HttpRequest req, string name,string id,
            ILogger log)
        {
            try
            {
                string connectionString = Environment.GetEnvironmentVariable("cosmos");
                CosmosClient cosmosclient = new CosmosClient(connectionString);
                Database database = cosmosclient.GetDatabase("GroupProject");
                Container container = database.GetContainer("Games");

                //data ophalen

                var sql = "SELECT * FROM Games c WHERE c.id = @id";
                QueryDefinition queryDefinition = new QueryDefinition(sql).WithParameter("@id", id);

                FeedIterator<Game> feedIterator = container.GetItemQueryIterator<Game>(queryDefinition);

                Game gameInfo = null;

                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<Game> result = await feedIterator.ReadNextAsync();
                    foreach (var game in result)
                    {
                        gameInfo = game;
                        break;
                    }
                }

                gameInfo.User = name;
                await container.ReplaceItemAsync<Game>(gameInfo, gameInfo.id);

                return new OkObjectResult("Gelukt");
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }


            
        }
    }
}
