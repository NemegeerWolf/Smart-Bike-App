using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using SmartBike_Api.Models;
using Microsoft.Azure.Cosmos;

namespace SmartBike_Api.Functions.Gets
{
    public static class GetAllScoresVideo
    {
        [FunctionName("GetAllScoresVideo")]
        public static async Task<IActionResult> GetVideoScoreAll(
            [HttpTrigger(AuthorizationLevel.Function, "get",  Route = "smartbike/video/{videoid}")] HttpRequest req, int videoid,
            ILogger log)
        {
            log.LogInformation("Calling GetAllScoresVideo");
            QueryDefinition query = new QueryDefinition("select * from Videos i where i.videoId = @videoId order by i.distance desc").WithParameter("@videoId", videoid);
            List<Video> items = await GetScoresAsync(query);
            return new OkObjectResult(AddRank(items));
        }
        public static async Task<List<Video>> GetScoresAsync(QueryDefinition query)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            Container container = client.GetContainer("GroupProject", "Videos");

            List<Video> items = new List<Video>();
            using (FeedIterator<Video> resultSet = container.GetItemQueryIterator<Video>(queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<Video> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            return items;
        }
        public static List<VideoRank> AddRank(List<Video> scores)
        {
            int count = 0;
            List<VideoRank> ranks = new List<VideoRank>();
            foreach (var i in scores)
            {
                count += 1;
                ranks.Add(new VideoRank { VideoId = i.VideoId, Distance = i.Distance, id = i.id, User = i.User, Rank = count });
            }

            return ranks;
        }
    }
    
}
