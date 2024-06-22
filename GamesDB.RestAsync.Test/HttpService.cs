using System.Net.Http;
using System.Threading.Tasks;
using PolyhydraGames.Core.Interfaces;

namespace GamesDB.RestAsync.Test;

public class HttpService : IHttpService
{
    public Task<string> GetAuthToken()
    {
        return Task.FromResult("NO TOKEN PROVIDED");
    }

    public HttpClient GetClient { get; set; } = new HttpClient();
}