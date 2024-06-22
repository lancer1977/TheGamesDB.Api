namespace GamesDB.RestAsync.Model
{
    public class Games_v1 : Games
    {
        public GameResponse Boxart { get; set; }
        public PlatformResponse Platform { get; set; }
    }
}