using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBike_Api.Models
{
    public class VideoRank
    {
        [JsonProperty("videoId")]
        public int VideoId { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }
    }
}
