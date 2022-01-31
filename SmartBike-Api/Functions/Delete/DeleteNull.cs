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

namespace SmartBike_Api.Functions.Delete
{
    public static class DeleteNull
    {
        [FunctionName("DeleteNull")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "smartbike/game/null/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            try
            {

                string connectionString = Environment.GetEnvironmentVariable("cosmos");
                CosmosClient cosmosclient = new CosmosClient(connectionString);
                Database database = cosmosclient.GetDatabase("GroupProject");
                Container container = database.GetContainer("Games");

                //data ophalen

                var sql = "SELECT * FROM events c WHERE c.id = @id";
                QueryDefinition queryDefinition = new QueryDefinition(sql).WithParameter("@id", id);
                FeedIterator<Game> feedIterator = container.GetItemQueryIterator<Game>(queryDefinition);

                Game gameInfo = null;

                while (feedIterator.HasMoreResults)
                {
                    FeedResponse<Game> result = await feedIterator.ReadNextAsync();
                    foreach (var ev in result)
                    {
                        gameInfo = ev;
                        break;
                    }
                }

                // let op partitionkey --> moet overeenkomen
                await container.DeleteItemAsync<Game>(gameInfo.id, new PartitionKey(gameInfo.GameId));



                return new OkObjectResult("Deleted");
            }
            catch (Exception ex)
            {
                return new OkObjectResult(ex);
            }
        }
    }
}
