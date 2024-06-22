namespace GamesDB.RestAsync.Enums
{
    public static class EnumExtensions
    {
 
        //public static string GetImageType(this ImageType type)
        //{
        //    var result = new List<string>();

        //    foreach (var field in Enum.GetValues(typeof(ImageType)))
        //    {
        //        if ( type.HasFlag((ImageType)field))
        //        {
        //            result.Add(((ImageType)(object)field).ToString().ToLower());
        //        }
        //    }

        //    return string.Join(',', result);
        //}

        public static string GetImageType(this ImageType type)
        {
            var result = "";
            var art = new List<string>();

            if (type.HasFlag(ImageType.FanArt))
            {
                art.Add("fanart");
            }

            if (type.HasFlag(ImageType.Banner))
            {
                art.Add("banner");
            }

            if (type.HasFlag(ImageType.Boxart))
            {
                art.Add("boxart");
            }

            if (type.HasFlag(ImageType.Screenshot))
            {
                art.Add("screenshot");
            }

            if (type.HasFlag(ImageType.Clearlogo))
            {
                art.Add("clearlogo");
            }

            if (type.HasFlag(ImageType.TitleScreen))
            {
                art.Add("titlescreen");
            }

            return string.Join(',', art);
        }

        public static string GetFieldType(this GameField type)
        {
            var result = "";
            var art = new List<string>();

            if (type.HasFlag(GameField.Alternates))
            {
                art.Add("alternates");
            }
            if (type.HasFlag(GameField.Coop))
            {
                art.Add("coop");
            }
            if (type.HasFlag(GameField.Hdd))
            {
                art.Add("hdd");
            }
            if (type.HasFlag(GameField.Genres))
            {
                art.Add("genres");
            }
            if (type.HasFlag(GameField.LastUpdated))
            {
                art.Add("last_updated");
            }
            if (type.HasFlag(GameField.Os))
            {
                art.Add("os");
            }
            if (type.HasFlag(GameField.Overview))
            {
                art.Add("overview");
            }
            if (type.HasFlag(GameField.Platform))
            {
                art.Add("platform");
            }
            if (type.HasFlag(GameField.Players))
            {
                art.Add("players");
            }
            if (type.HasFlag(GameField.Publishers))
            {
                art.Add("publishers");
            }
            if (type.HasFlag(GameField.Processor))
            {
                art.Add("processor");
            }
            if (type.HasFlag(GameField.Ram))
            {
                art.Add("ram");
            }
            if (type.HasFlag(GameField.Rating))
            {
                art.Add("rating");
            }
            if (type.HasFlag(GameField.Sound))
            {
                art.Add("sound");
            }
            if (type.HasFlag(GameField.Youtube))
            {
                art.Add("youtube");
            }
            if (type.HasFlag(GameField.Video))
            {
                art.Add("video");
            }

            return string.Join(',', art);
        }
    }
}