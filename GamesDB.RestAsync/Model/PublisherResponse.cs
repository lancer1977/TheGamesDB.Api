namespace GamesDB.RestAsync.Model;

public class PublisherResponse
{
    public int Count { get; set; }
    public   Dictionary<string, Publisher> Publishers { get; set; }
}