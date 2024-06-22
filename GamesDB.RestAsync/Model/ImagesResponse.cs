namespace GamesDB.RestAsync.Model;

public class ImagesResponse
{
    [JsonPropertyName("base_url")]
    public ImageBaseUrlMeta BaseUrl { get; set; }
    public Dictionary<string,List<Image>> Images { get; set; } 
}