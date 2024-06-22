namespace GamesDB.RestAsync.Model;

public class PaginatedApiResponse : BaseApiResponse
{
    public PagesResponse Pages { get; set; }
}