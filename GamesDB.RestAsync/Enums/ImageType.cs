namespace GamesDB.RestAsync.Enums;

[Flags]
public enum ImageType
{

    None = 0,
    FanArt = 1,
    Banner = 2,
    Boxart = 4,
    Screenshot = 8,
    Clearlogo = 16,
    TitleScreen = 32,
    All = ~0
}