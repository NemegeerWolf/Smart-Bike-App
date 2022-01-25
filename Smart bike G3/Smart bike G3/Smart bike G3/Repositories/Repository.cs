using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smart_bike_G3.Models;
using Smart_bike_G3.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        public async static Task AddResultsVideo(int videoid, string user, int distance )
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/video/{videoid}/{user}/{distance}";

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
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid}/{distance}/{speed}";

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
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/name/{name}/{id}";

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
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/null/{id}";

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

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/last/user";
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
                
                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid} ";
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

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/null/{gameid} ";
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
                
                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/video/{videoid} ";
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
                
                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/video/{videoid}/{user} ";
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

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid}/{user} ";
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

                string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/check/{username} ";
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
        public async static Task<int> CheckRank(string id, int score, string kind)
        {
            int rank = 0;
            //if (kind == "video")
            //{
            //    int videoid = ChooseVideo.VideoId;
            //    List<Video> list = await GetAllscoresVideoAsync(videoid);
            //    foreach (var i in list)
            //    {
            //        if (i.id == id & i.Distance == score)
            //        {
            //            rank = int.Parse(i.Rank);
            //            Debug.WriteLine(rank);
            //        }
            //    }
            //    return rank;
            //}
            //else
            if (kind == "game")
            {
                int gameid = ChooseGame.gameId;
                List<Game> list = await GetAllscoresGameWithNullAsync(gameid);
                foreach (var i in list)
                {
                    if (gameid == 3 & i.id == id & i.Distance == score)
                    {
                        rank = int.Parse(i.Rank);

                    }
                    else if (Enumerable.Range(1, 2).Contains(gameid) & i.id == id & i.Speed == score)
                    {
                        rank = int.Parse(i.Rank);
                        
                    }
                }
                return rank;

            }
            else
            {
                return 0;
            }
        }
    }
}
