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
    public static class GetUserScoresVideo
    {
        [FunctionName("GetUserScoresVideo")]
        public static async Task<IActionResult> GetVideoScoreUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "smartbike/video/{videoid}/{user}")] HttpRequest req, int videoid, string user,
            ILogger log)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            Container container = client.GetContainer("GroupProject", "Videos");
            QueryDefinition query = new QueryDefinition("select * from Videos i where i.videoId = @videoId and i.user = @user").WithParameter("@videoId", videoid).WithParameter("@user",user);

            List<Video> items = new List<Video>();
            using (FeedIterator<Video> resultSet = container.GetItemQueryIterator<Video>(queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<Video> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }

            return new OkObjectResult(items);
        }

    }
}
