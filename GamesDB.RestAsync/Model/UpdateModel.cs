namespace GamesDB.RestAsync.Model
{
    public class UpdateModel
    {
        public int EditId { get; set; }
        public int GameId { get; set; }
        public string Timestamp { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}