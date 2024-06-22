using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesDB.RestAsync;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace GamesDB.RestAsync.Test;

[TestFixture]
public class Tests
{
    protected TheGamesDbWrapper _search;

    public Tests()
    { 

    }

    [SetUp]
    public async Task Setup()
    {
        //Get Client Secret
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Set the base path to the test project
            .AddUserSecrets("d1921150-b78b-40bf-ba16-9dcf02692536") // Use the UserSecretsId generated earlier
            .Build();
        //_search = new TheGamesDbWrapper(configuration["Twitch:ClientId"], configuration["Twitch:Secret"], "https://api.igdb.com/v4/", TestHelpers.GetLogger<IgdbService>());
        _search = new TheGamesDbWrapper(new GamesDBEndpointFactory(configuration["GamesDB:ApiKey"]), new HttpService());


    }

    [Test]
    public async Task Platforms()
    {
        var result = await _search.Platforms();
        var items = result.Data.Platforms;
        Assert.That(items.Any());
    }

    [Test]
    public async Task Publishers()
    {
        var result = await _search.Publishers();
        var items = result.Data.Publishers;
        Assert.That(items.Any());
    }

    [Test]
    public async Task Genres()
    {
        var result = await _search.Genres();
        var items = result.Data.Genres;
        Assert.That(items.Any());
    }

    [Test]
    public async Task Developers()
    {
        var result = await _search.Developers();
        var items = result.Data.Developers;
        Assert.That(items.Any());
    }
    [Test]
    public async Task GamesByName()
    {
        var result = await _search.ByGameName("Mario Kart");
        var items = result.Data.Games;
        foreach(var item in items)
        {
            Console.WriteLine(item.Id + ": " + item.GameTitle );
        }
        Console.WriteLine();
        Assert.That(items.Any());
    }
}