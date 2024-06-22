namespace GamesDB.RestAsync.Model
{
    public class GamesUpdates : BaseApiResponse
    {
        public PagesResponse Pages { get; set; }
        public GameResponse Data { get; set; }
    }
}