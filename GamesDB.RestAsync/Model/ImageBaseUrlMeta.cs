namespace GamesDB.RestAsync.Model;

public class ImageBaseUrlMeta
{
    public string Original { get; set; }
    public string Small { get; set; }
    public string Thumb { get; set; }
    [JsonPropertyName("cropped_center_thumb")]
    public string CroppedCenterThumb { get; set; }
    public string Medium { get; set; }
    public string Large { get; set; }
}