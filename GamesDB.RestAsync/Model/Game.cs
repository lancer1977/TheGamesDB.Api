using System.Text.Json;
namespace GamesDB.RestAsync.Model
{
    public class Game
    {
        public int Id { get; set; }
        [JsonPropertyName("game_title")]
        public string GameTitle { get; set; }
        public string ReleaseDate { get; set; }
        public int Platform { get; set; }
        public int Players { get; set; }
        public string Overview { get; set; }
        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }
        public string Rating { get; set; }
        public string Coop { get; set; }
        public string Youtube { get; set; }
        public string Os { get; set; }
        public string Processor { get; set; }
        public string Ram { get; set; }
        public string Hdd { get; set; }
        public string Video { get; set; }
        public string Sound { get; set; }
        public List<int> Developers { get; set; }
        public List<int> Genres { get; set; }
        public List<int> Publishers { get; set; }
        //public List<Developer> Developers { get; set; }
        //public List<Genre> Genres { get; set; }
        //public List<Publisher> Publishers { get; set; }
    }
}