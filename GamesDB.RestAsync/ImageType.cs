namespace GamesDB.RestAsync
{
    [Flags]
    public enum ImageType 
    {
        FanArt = 1,
        Banner = 2,
        Boxart = 4,
        Screenshot = 8,
        Clearlogo = 16,
        TitleScreen = 32
    }

    public static class EnumExtensions
    {
        //MOVE ME TO CORE

        public static bool Is<T>(this T enumValue) where T : Enum
        {
            return enumValue.HasFlag(Type.GetTypeCode(typeof(T)));
        }
    }
}