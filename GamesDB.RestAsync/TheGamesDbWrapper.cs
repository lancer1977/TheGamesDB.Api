using GamesDB.RestAsync.Model;

namespace GamesDB.RestAsync;

public class TheGamesDbWrapper : RestServiceBase
{
    private (string, string) Id(string id) => ("id", id.ToString());
    private readonly (string, string) Fields = ("fields","players,publishers,genres,overview,last_updated,rating,platform,coop,youtube,os,processor,ram,hdd,video,sound,alternates");
    private readonly (string, string) ImageFields = ("fields", "fanart,banner,boxart,screenshot,clearlogo,titlescreen");
    private (string, string) Filter(string filter) => ("filter", filter);
    private readonly (string, string) Include = ("include", "boxart,platform,genres,publishers,developers");
    private (string, string) Name(string name) => ("name", name);
    private (string, string) Platform(int id) => ("platform", id.ToString());
    private (string, string) Page(int id) => ("page", id.ToString());

    private string Key { get; set; }
    public static string AddressRoot = "https://api.thegamesdb.net";
    public TheGamesDbWrapper(IGamesDbAuthentication factory, IHttpService httpService) : base(factory, httpService)
    {
        Key = factory.ApiKey;
    }

    private (string, string) GetAuthTupple()
    {
        return ("apikey", Key);
    }
    protected override string Service => "v1";



    //public Task<Games> Games(string name) => Get<Games>(GetAuthTupple(), ("name", name));
    public Task<Developers> Developers() => Get<Developers>(GetAuthTupple());
    public Task<Genres> Genres() => Get<Genres>(GetAuthTupple());
    public Task<Publishers> Publishers() => Get<Publishers>(GetAuthTupple());
    public Task<Platforms> Platforms() => Get<Platforms>(GetAuthTupple());

    public Task<Games> ByGameID(IEnumerable<int> id, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Fields,
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Games>(parms, "Games/" + nameof(ByGameID));
    }

    public Task<Games> ByGameName(string name,int? platformId = null, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            (nameof(name), name.ToString()),
            Fields,
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        if(platformId != null)
        {
            parms.Add(Platform(platformId.Value));
        }
        return Get<Games>(parms, "Games/" + nameof(ByGameName));
    }

    public Task<Games> ByPlatformID(int id, int offset = 0)=> ByPlatformID([id], offset);

    public Task<Games> ByPlatformID(List<int> id, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Fields,
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Games>(parms, "Games/" + nameof(ByPlatformID));
    }

    public Task<Games> Images(int id, ImageType type, int offset = 0) => Images([id], type, offset);

    public Task<Games> Images(List<int> id, ImageType type, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Filter(GetImageType(type)),
            //Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Games>(parms, "Games/" + nameof(ByPlatformID));
    }

    public static string GetImageType(ImageType type)
    {
        string result = "";
        List<string> art = new List<string>();
 
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

        return string.Join(',',art);
    }

}