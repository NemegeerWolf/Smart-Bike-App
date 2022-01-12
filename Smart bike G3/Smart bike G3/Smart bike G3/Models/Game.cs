using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Smart_bike_G3.Models
{
    public class Game
    {
        [JsonProperty("gameId")]
        public int GameId { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        public string DistanceString
        {
            get
            {
                return $"{this.Distance} km";
            }
        }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }
    }
}
