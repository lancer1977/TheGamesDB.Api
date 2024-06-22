namespace GamesDB.RestAsync.Model;

public class GamesImages : BaseApiResponse
{
    public PagesResponse Pages { get; set; }
    public ImagesResponse Data { get; set; }
}