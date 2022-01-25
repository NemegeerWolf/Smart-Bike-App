using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_bike_G3.Models
{
    public class Vid
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("audio")]
        public int Audio { get; set; }
    }

    public class VideoSettings
    {
        [JsonProperty("video")]
        public Vid vid { get; set; }
    }

}
