using System.Collections.Generic;
using GamesDB.RestAsync.Catalog;
using GamesDB.RestAsync.Model;
using NUnit.Framework;

namespace GamesDB.RestAsync.Test;

[TestFixture]
public sealed class CatalogMapperTests
{
    [Test]
    public void MapGames_ReturnsCanonicalCandidateWithMediaAndProvenance()
    {
        var result = TheGamesDbCatalogMapper.MapGames(
            [
                new Game
                {
                    Id = 93527,
                    GameTitle = "Super Mario World",
                    Platform = 6,
                    ReleaseDate = "1990-11-21",
                    Overview = "A platform game.",
                    Developers = [1],
                    Genres = [2],
                    Publishers = [3]
                }
            ],
            new ImagesResponse
            {
                Images = new Dictionary<string, List<Image>>
                {
                    ["93527"] =
                    [
                        new Image { Type = "boxart", Side = "front", Filename = "boxart/smw-front.png" },
                        new Image { Type = "screenshot", Side = "", Filename = "screenshots/smw.png" }
                    ]
                }
            });

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(TheGamesDbCatalogMappingStatus.Mapped));
            Assert.That(result.Candidates, Has.Count.EqualTo(1));
            Assert.That(result.Candidates[0].Provider, Is.EqualTo("thegamesdb"));
            Assert.That(result.Candidates[0].ProviderGameId, Is.EqualTo("93527"));
            Assert.That(result.Candidates[0].Title, Is.EqualTo("Super Mario World"));
            Assert.That(result.Candidates[0].PlatformId, Is.EqualTo(6));
            Assert.That(result.Candidates[0].ReleaseDate, Is.EqualTo("1990-11-21"));
            Assert.That(result.Candidates[0].DeveloperIds, Is.EquivalentTo(new[] { 1 }));
            Assert.That(result.Candidates[0].GenreIds, Is.EquivalentTo(new[] { 2 }));
            Assert.That(result.Candidates[0].PublisherIds, Is.EquivalentTo(new[] { 3 }));
            Assert.That(result.Candidates[0].Media["boxart:front"], Is.EqualTo("boxart/smw-front.png"));
            Assert.That(result.Candidates[0].Media["screenshot"], Is.EqualTo("screenshots/smw.png"));
            Assert.That(result.Candidates[0].Provenance, Is.EqualTo("thegamesdb.games"));
            Assert.That(result.Candidates[0].Confidence, Is.EqualTo(1.00m));
        });
    }

    [Test]
    public void MapGames_ReturnsEmptyWhenRecordsDoNotContainIdentity()
    {
        var result = TheGamesDbCatalogMapper.MapGames([
            new Game { Id = 0, GameTitle = "Missing ID" },
            new Game { Id = 1, GameTitle = "" }
        ]);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(TheGamesDbCatalogMappingStatus.Empty));
            Assert.That(result.Candidates, Is.Empty);
            Assert.That(result.Reason, Does.Contain("no usable"));
        });
    }

    [Test]
    public void MapGames_ReturnsAmbiguousWhenTopCandidatesTie()
    {
        var result = TheGamesDbCatalogMapper.MapGames([
            new Game { Id = 1, GameTitle = "Metroid", Platform = 7, ReleaseDate = "1986" },
            new Game { Id = 2, GameTitle = "Metroid Alternate", Platform = 7, ReleaseDate = "1986" }
        ]);

        Assert.Multiple(() =>
        {
            Assert.That(result.Status, Is.EqualTo(TheGamesDbCatalogMappingStatus.Ambiguous));
            Assert.That(result.Candidates, Has.Count.EqualTo(2));
            Assert.That(result.Reason, Does.Contain("Multiple"));
        });
    }
}
