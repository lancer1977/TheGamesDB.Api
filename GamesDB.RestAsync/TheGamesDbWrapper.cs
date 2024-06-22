using GamesDB.RestAsync.Model;

namespace GamesDB.RestAsync;

public class TheGamesDbWrapper : RestServiceBase
{
    private (string, string) Id(string id) => ("id", id.ToString());
    private readonly (string, string) Fields = ("fields","players,publishers,genres,overview,last_updated,rating,platform,coop,youtube,os,processor,ram,hdd,video,sound,alternates");
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

    public Task<Game> ByGameID(List<int> id, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Fields,
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Game>(parms, "Games/" + nameof(ByGameID));
    }

    public Task<GamesByGameID> ByGameName(string name,int? platformId = null, int offset = 0)
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
        return Get<GamesByGameID>(parms, "Games/" + nameof(ByGameName));
    }

    public Task<Game> ByPlatformID(int id, int offset = 0)=> ByPlatformID(new List<int>(id), offset);

    public Task<Game> ByPlatformID(List<int> id, int offset = 0)
    {
        var parms = new List<(string, string)>()
        {
            Id(id.Select(x=>x.ToString()).Aggregate((x,y)=>x +"," + y)),
            Fields,
            Include,
            Page(offset),
            GetAuthTupple(),

        };
        return Get<Game>(parms, "Games/" + nameof(ByGameID));
    }

}