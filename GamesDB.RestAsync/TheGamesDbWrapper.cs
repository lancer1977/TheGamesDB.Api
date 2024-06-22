using GamesDB.RestAsync.Enums;
using GamesDB.RestAsync.Model;

namespace GamesDB.RestAsync;

public class TheGamesDbWrapper : RestServiceBase
{
    protected override string Service => "v1";
    private (string, string) Id(string id) => ("id", id.ToString());
    private (string, string) GamesId(string id) => ("games_id", id.ToString());

    private   (string, string) Fields(string val) => ("fields", val);
    //private readonly (string, string) Fields = ("fields","players,publishers,genres,overview,last_updated,rating,platform,coop,youtube,os,processor,ram,hdd,video,sound,alternates");
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
    //public Task<Games> Games(string name) => Get<Games>(GetAuthTupple(), ("name", name));
    public Task<Developers> Developers() => Get<Developers>(GetAuthTupple());
    public Task<Genres> Genres() => Get<Genres>(GetAuthTupple());
    public Task<Publishers> Publishers() => Get<Publishers>(GetAuthTupple());
    public Task<Platforms> Platforms() => Get<Platforms>(GetAuthTupple());

    public Task<Games> ByGameID(IEnumerable<int> id, GameField field = GameField.All, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),

            Fields(field.GetFieldType()),
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Games>(parms, "Games/" + nameof(ByGameID));
    }

    public Task<Games> ByGameName(string name,GameField field = GameField.All, int? platformId = null, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            (nameof(name), name.ToString()),
            Fields(field.GetFieldType()),
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

    public Task<Games> ByPlatformID(int id, GameField field = GameField.All, int offset = 0)=> ByPlatformID([id],field, offset);

    public Task<Games> ByPlatformID(List<int> id, GameField field = GameField.All, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Fields(field.GetFieldType()),
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Games>(parms, "Games/" + nameof(ByPlatformID));
    }

    public Task<GamesImages> Images(int id, ImageType type, int offset = 0) => Images([id], type, offset);

    public Task<GamesImages> Images(IEnumerable<int> id, ImageType type, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            GamesId(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Filter(type.GetImageType()), 
            Page(offset),
            GetAuthTupple(),

        };
        return Get<GamesImages>(parms, "Games/" + nameof(Images));
    }

    
}