namespace GamesDB.RestAsync.Model
{
    public class PlatformResponse
    {
        public int Count { get; set; }
        public Dictionary<string,Platform> Platforms { get; set; }
    }
}