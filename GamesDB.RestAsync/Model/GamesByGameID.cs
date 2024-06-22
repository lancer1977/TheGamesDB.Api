namespace GamesDB.RestAsync.Model
{
    public class Games : BaseApiResponse
    {
        public PagesResponse Pages { get; set; }
        public GameResponse Data { get; set; }
        public GameResponse? Include { get; set; }
    }
}