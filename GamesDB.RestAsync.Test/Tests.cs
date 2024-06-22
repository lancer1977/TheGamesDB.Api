using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GamesDB.RestAsync.Enums;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PolyhydraGames.Extensions;

namespace GamesDB.RestAsync.Test;

[TestFixture]
public class Tests
{
    protected TheGamesDbWrapper _search;

    public Tests()
    { 

    }

    [SetUp]
    public void Setup()
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
        //Console.WriteLine(String.Join(',', items.Select(x=>x.Id)));
        foreach (var item in items)
        {
            Console.WriteLine(item.GameTitle);
        }
        Console.WriteLine();
        Assert.That(items.Any());
    }

    [Test]
    public async Task GamesById()
    {
        var ids = new[] { 93527,111047,266,47050,55187,113990,114167,96970,64547,76971,12733,114674,119134,127248,93194,17444,106592,119053,75652,99856 }
            .ToList();
        var result = await _search.ByGameID(ids);
        var items = result.Data.Games;
        //Console.WriteLine(String.Join(',', items.Select(x=>x.Id)));
        foreach (var item in items)
        {
            Console.WriteLine(item.GameTitle );
        }
        Console.WriteLine();
        Assert.That(items.Any());
    }

    [Test]
    public async Task GameByPlatformId()
    {
        var result = await _search.ByPlatformID(21);
        var items = result.Data.Games;
        foreach (var item in items)
        {
            Console.WriteLine(item.GameTitle);
        }
        Assert.That(items.Any());
    }

    //https://api.thegamesdb.net/v1/Games/Images?id=93527&filter=banner,boxart&page=0&apikey=c50e16377f86531d153e6d686f7604a5e606d6b4155181b6edd48b221d895c4d
    //https://api.thegamesdb.net/v1/Games/Images?apikey=c50e16377f86531d153e6d686f7604a5e606d6b4155181b6edd48b221d895c4d&games_id=93527&filter%5Btype%5D=banner%2Cboxart
    // 
    [Test]
    public async Task GetImages()
    {
        var result = await _search.Images(93527,ImageType.Banner | ImageType.Boxart);
        var items = result.Data.Images;
        foreach (var item in items.Values)
        {
            foreach (var image in item)
            {
                Console.WriteLine(image.Filename);
            }
        }
        Assert.That(items.Any());
    }

    [Test]
    public  void TypeCheck()
    {
        var imageTypes = ImageType.Boxart | ImageType.Clearlogo;
        var result = imageTypes.GetImageType();
        Console.WriteLine(result);
        Assert.That(result == "boxart,clearlogo");
    }

    [Test]
    public void GameFieldCheck()
    {
        var imageTypes = GameField.Platform | GameField.Publishers;
        var result = imageTypes.GetFieldType();
        Console.WriteLine(result);
        Assert.That(result == "platform,publishers");
    }


    [Test]
    public void GameFieldCheckAll()
    {
        var imageTypes = GameField.All;
        var result = imageTypes.GetFieldType();
        Console.WriteLine(result);
        Assert.That(!string.IsNullOrEmpty(result));
    }

    [Test]
    public void GameFieldCheck_None()
    {
        var imageTypes = GameField.None;
        var result = imageTypes.GetFieldType();
        Console.WriteLine(result);
        Assert.That(string.IsNullOrEmpty(result));
    }

}