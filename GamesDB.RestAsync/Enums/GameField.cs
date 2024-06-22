namespace GamesDB.RestAsync.Enums
{
    [Flags]
    public enum GameField
    {
        None = 0,

        Alternates = 1,
        Coop = 2,
        Hdd = 4,
        Genres = 8,
        LastUpdated = 16,
        Os = 32,
        Overview = 64,
        Platform = 128,
        Players = 256,
        Publishers = 512,
        Processor = 1024,
        Ram = 2048,
        Rating = 4096,
        Sound = 8192,
        Youtube = 16384,
        Video = 32768,

        All = ~0
    }
}