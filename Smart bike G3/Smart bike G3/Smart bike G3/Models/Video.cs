using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_bike_G3.Models
{
    public class Video
    {
        [JsonProperty("videoId")]
        public int VideoId { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }
    }
}
