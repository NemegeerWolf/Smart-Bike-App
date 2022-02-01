using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using SmartBike_Api.Models;
using System.Diagnostics;

namespace SmartBike_Api.Functions.Gets
{
    public static class GetAllScoresGamewithNull
    {
        

        [FunctionName("GetAllScoresGameWithNull")]
        public static async Task<IActionResult> GetGameScoreAllWithNull(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "smartbike/game/null/{gameid}")] HttpRequest req, int gameid,
            ILogger log)
        {

            log.LogInformation("Calling GetAllScoresGame");
            if (gameid == 1)
            {
                QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId order by i.speed asc").WithParameter("@gameId", gameid);
                List<Game> items = await GetScoresAsync(query);
                return new OkObjectResult(AddRank(items));
                
            }
            else if (gameid == 2)
            {
                QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId  order by i.speed desc").WithParameter("@gameId", gameid);
                List<Game> items = await GetScoresAsync(query);
                return new OkObjectResult(AddRank(items));
            }
            else if (gameid == 3)
            {
                QueryDefinition query = new QueryDefinition("select * from Games i where i.gameId = @gameId  order by i.speed asc").WithParameter("@gameId", gameid);
                List<Game> items = await GetScoresAsync(query);
                return new OkObjectResult(AddRank(items));
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
        public static List<GameRank> AddRank(List<Game> scores)
        {
            int count = 0;
            List<GameRank> ranks = new List<GameRank>();
            foreach (var i in scores)
            {
                count += 1;
                ranks.Add(new GameRank { GameId = i.GameId, Distance = i.Distance, id = i.id, Speed = i.Speed, User = i.User, Rank = count});
            }
            
            return ranks;
        }
    }
}
