using System;
using System.Collections.Generic;
using System.Text;
using SmartBike_Api.Models;
using Newtonsoft.Json;


namespace SmartBike_Api.Models
{
    public class GameRank
    {

        [JsonProperty("gameId")]
        public int GameId { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        [JsonProperty("speed")]
        public float Speed { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }


    }
}
