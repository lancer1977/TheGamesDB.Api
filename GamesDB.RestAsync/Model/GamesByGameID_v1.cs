namespace GamesDB.RestAsync.Model
{
    public class GamesByGameID_v1 : GamesByGameID
    {
        public GameResponse Boxart { get; set; }
        public PlatformResponse Platform { get; set; }
    }
}