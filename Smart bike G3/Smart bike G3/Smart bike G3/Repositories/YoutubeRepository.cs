using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Smart_bike_G3.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

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
        private static string GetPlaylistId(int bin)
        {
            string shorts = "PL65ceuuCPcAwtCNR0oHVsWqcmCSWgWcz3";
            string envs = "PL65ceuuCPcAzKQ9rwq3-AWRi9nYjmojoc";
            if (bin == 0)
            {
                return envs;

            }
            else
            {
                return shorts;
            }
        }

        public async static Task<List<string>> GetPlaylist(int bin)
        {

            List<string> ids = new List<string>();
            string secretKey = "AIzaSyAzqXN9nivfePgfnh_f1F1ziXhYGXmmUks";
            string playlist = GetPlaylistId(bin);
            using (HttpClient client = GetHttpClient())
            {
                string url = $"https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={playlist}&key={secretKey}";
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
       

        

    }
}
