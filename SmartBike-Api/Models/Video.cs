using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartBike_Api.Models
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
