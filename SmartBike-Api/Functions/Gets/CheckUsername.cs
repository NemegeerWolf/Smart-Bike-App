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
    public static class CheckUsername
    {
        [FunctionName("CheckUserName")]
        public static async Task<IActionResult> CheckUserName(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "smartbike/check/{username}")] HttpRequest req, string username,
            ILogger log)
        {
            List<string> usernames = new List<string>();
            var itemsProcessedGame = 0;
            var itemsProcessedVideo = 0;

            QueryDefinition queryGame = new QueryDefinition("select * from Games");
            List<Game> itemsGame = await GetScoresGamesAsync(queryGame);
            log.LogInformation(itemsGame.Count.ToString());
            foreach (var item in itemsGame)
            {
                itemsProcessedGame++;
                //log.LogInformation(item.User);
                usernames.Add(item.User);
            }

            QueryDefinition queryVideo = new QueryDefinition("select * from Videos");
            List<Video> itemsVideo = await GetScoresVideoAsync(queryVideo);
            log.LogInformation(itemsVideo.Count.ToString());
            foreach (var item in itemsVideo)
            {
                itemsProcessedVideo++;
                //log.LogInformation(item.User);
                usernames.Add(item.User);

            }
            

            if ((itemsVideo.Count + itemsGame.Count) == (itemsProcessedGame + itemsProcessedVideo))
            {
                if (usernames.Contains(username))
                {
                    return new OkObjectResult(true);
                }
                else
                {

                    return new OkObjectResult(false);
                }

            }



            return null;
          

        }



        

        public static async Task<List<Game>> GetScoresGamesAsync(QueryDefinition query)
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

        public static async Task<List<Video>> GetScoresVideoAsync(QueryDefinition query)
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
    }
}
