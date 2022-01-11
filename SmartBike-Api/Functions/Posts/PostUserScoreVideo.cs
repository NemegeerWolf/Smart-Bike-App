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
using System.Diagnostics;
using SmartBike_Api.Models;

namespace SmartBike_Api.Functions.Posts
{
    public static class PostUserScoreVideo
    {
        [FunctionName("PostUserScoreVideo")]
        public static async Task<IActionResult> PostVideo(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "smartbike/video/{videoid}/{user}/{distance}")] HttpRequest req, string user, int videoid, int distance,
            ILogger log)
        {
            log.LogInformation("Processing post request (video)");

            try
            {
                Video data = new Video
                {
                    VideoId = videoid,
                    User = user,
                    Distance = distance,
                    id = Guid.NewGuid().ToString()

                };
                await AddToCosmosAsync(data);
                return new OkObjectResult("Added");

            }
            catch (Exception ex)
            {
                log.LogError($"{ex}");
                return new StatusCodeResult(500);
            }


        }
        public static async Task AddToCosmosAsync(Video data)
        {
            try
            {
                CosmosClientOptions options = new CosmosClientOptions();
                options.ConnectionMode = ConnectionMode.Gateway;
                CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
                Container container = client.GetContainer("GroupProject", "Videos");
                ItemResponse<Video> response = await container.CreateItemAsync(data, new PartitionKey(data.VideoId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
