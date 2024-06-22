namespace GamesDB.RestAsync.Model
{
    public class DeveloperResponse
    {
        public int Count { get; set; }
        public Dictionary<string, Developer> Developers { get; set; }
    }
}