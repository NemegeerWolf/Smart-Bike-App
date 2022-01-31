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
using Microsoft.Azure.Cosmos.Linq;
using SmartBike_Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartBike_Api.Functions.Gets
{
    public static class GetIdLastUser
    {
        [FunctionName("GetIdLastUser")]
        public static async Task<IActionResult> GetId(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "smartbike/game/last/user")] HttpRequest req,
            ILogger log)
        {
            try
            {

                QueryDefinition query = new QueryDefinition("select * from Games");
                List<Game> items = await GetScoresAsync(query);
                var lastItem = items.Last();
                return new OkObjectResult(lastItem);
  
            }
            catch (Exception ex)
            {
                
                return new OkObjectResult(ex);
            }
            
        }

        public static async Task<List<Game>> GetScoresAsync(QueryDefinition query)
        {
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
