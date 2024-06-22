namespace GamesDB.RestAsync.Model
{
    public class GenreResponse
    {
        public int Count { get; set; }
        public Dictionary<string, Genre> Genres { get; set; }
    }
}