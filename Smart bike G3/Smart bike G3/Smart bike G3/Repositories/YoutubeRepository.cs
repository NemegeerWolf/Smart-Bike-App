using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smart_bike_G3.Models;
using Newtonsoft.Json.Linq;


namespace Smart_bike_G3.Repositories
{
    public class YoutubeRepository
    {
        private static HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            return client;
        }

        public async static Task<List<string>> GetPlaylist(int playlistId)
        {

            List<string> ids = new List<string>();
            string secretKey = "AIzaSyAzqXN9nivfePgfnh_f1F1ziXhYGXmmUks";
            string shorts = "PL65ceuuCPcAwtCNR0oHVsWqcmCSWgWcz3";

            if (playlistId == 2)
            {
                using (HttpClient client = GetHttpClient())
                {
                    string url = $"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={shorts}&key={secretKey}";
                    try
                    {
                        string json = await client.GetStringAsync(url);
                        if (json != null)
                        {
                            var data = JsonConvert.DeserializeObject<PlaylistItems>(json);
                            foreach (var i in data.Items)
                            {
                                ids.Add(i.Snippet.ResourceId.VideoId);
                            }
                            return ids;
                        }
                        return null;
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            else
            {
                return null;
            }
        }
        

    }
}
