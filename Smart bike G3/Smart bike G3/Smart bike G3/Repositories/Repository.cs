using Newtonsoft.Json;
using Smart_bike_G3.Models;
using Smart_bike_G3.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Smart_bike_G3.Repositories
{
    public class Repository
    {
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            return client;
        }
        private const string _secretKey = "4oJiv77gg/tM2the1oUk2CZdNbAahGkSnaaaBw7d0Mmr5tyenxo4dg==";
        public async static Task AddResultsVideo(int videoid, string user, int distance )
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/video/{videoid}/{user}/{distance}?code={_secretKey}";

                    var response = await client.PostAsync(url, null);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong");
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task AddResultsGame(int gameid, int speed, int distance)
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid}/{distance}/{speed}?code={_secretKey}";

                    var response = await client.PostAsync(url, null);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong");
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task UpdateName(string name, string id)
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/name/{name}/{id}?code={_secretKey}";

                    var response = await client.PutAsync(url, null);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong");
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task DeleteAsync(string id)
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/null/{id}?code={_secretKey}";

                    var response = await client.DeleteAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("Something went wrong");
                    }

                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<Game> GetLastUserAsync()
        {
            using (HttpClient client = GetHttpClient())
            {

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/last/user?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<Game>(json);

                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<List<Game>> GetAllscoresGameAsync(int gameid)
        {
            using (HttpClient client = GetHttpClient())
            {
                
                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid}?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<List<Game>>(json);

                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<List<Game>> GetAllscoresGameWithNullAsync(int gameid)
        {
            using (HttpClient client = GetHttpClient())
            {

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/null/{gameid}?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<List<Game>>(json);

                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<List<Video>> GetAllscoresVideoAsync(int videoid)
        {
            using (HttpClient client = GetHttpClient())
            {
                
                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/video/{videoid}?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<List<Video>>(json);

                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<List<Video>> GetUserscoresVideoAsync(int videoid, string user)
        {
            using (HttpClient client = GetHttpClient())
            {
                
                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/video/{videoid}/{user}?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<List<Video>>(json);

                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<List<Game>> GetUserscoresGameAsync(int gameid, string user)
        {
            using (HttpClient client = GetHttpClient())
            {

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid}/{user}?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<List<Game>>(json);

                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }

        public async static Task<bool> CheckUsernameAsync(string username)
        {
            using (HttpClient client = GetHttpClient())
            {

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/check/{username}?code={_secretKey}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        return JsonConvert.DeserializeObject<bool>(json);

                    }
                    return false;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }
        public async static Task<int> CheckRank(string id, int score)
        {
            int rank = 0;
            int gameid = ChooseGame.gameId;
            List<Game> list = await GetAllscoresGameWithNullAsync(gameid);
            foreach (var i in list)
            {
                if (Enumerable.Range(1, 3).Contains(gameid) & i.id == id & i.Speed == score)
                {
                    rank = int.Parse(i.Rank);
                        
                }
            }
            return rank; 
        }
    }
}
