using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Smart_bike_G3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
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
        public async static Task AddResultsGame(int gameid, string user, int distance, int speed)
        {
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string url = $"https://smartbikeapi.azurewebsites.net/api/smartbike/game/{gameid}/{user}/{distance}/{speed}";

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
    }
}
