using Newtonsoft.Json;
using System;
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
                
                if (Speed >= 60)
                {
                    double minuten = Speed / 60;
                    int minuten1 = (int)Math.Truncate(minuten);
                    int seconden = (int)Speed - (minuten1 * 60);
                    return $"{minuten1} min {seconden} s";


                }
                else
                {
                    return $"{Speed} s";
                }
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
