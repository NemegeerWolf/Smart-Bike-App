using Newtonsoft.Json;
using System.Diagnostics;

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

        public string ScoreBordString
        {
            get
            {
                return $"{this.Speed} s";
                //if (GameId != null)
                //{
                    
                //}
                //else
                //{
                //    return $"{this.Distance} m";
                //}
            }
        }

        [JsonProperty("speed")]
        public float Speed { get; set; }


        [JsonProperty("rank")]
        public string Rank { get; set; }

        public string RankDot
        {
            get
            {
                return $"{this.Rank}";
            }
        }

        [JsonProperty("id")]
        public string id { get; set; }
    }
}
