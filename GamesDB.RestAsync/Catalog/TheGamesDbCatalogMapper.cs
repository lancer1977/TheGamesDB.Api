using GamesDB.RestAsync.Model;

namespace GamesDB.RestAsync.Catalog;

public sealed record TheGamesDbCatalogCandidate(
    string Provider,
    string ProviderGameId,
    string Title,
    int PlatformId,
    string? ReleaseDate,
    IReadOnlyList<int> DeveloperIds,
    IReadOnlyList<int> GenreIds,
    IReadOnlyList<int> PublisherIds,
    IReadOnlyDictionary<string, string> Media,
    string Provenance,
    decimal Confidence,
    string? Overview);

public sealed record TheGamesDbCatalogMappingResult(
    string Status,
    string? Reason,
    IReadOnlyList<TheGamesDbCatalogCandidate> Candidates);

public static class TheGamesDbCatalogMappingStatus
{
    public const string Mapped = "mapped";
    public const string Empty = "empty";
    public const string Ambiguous = "ambiguous";
}

public static class TheGamesDbCatalogMapper
{
    public static TheGamesDbCatalogMappingResult MapGames(
        IEnumerable<Game> games,
        ImagesResponse? images = null)
    {
        var imageLookup = BuildImageLookup(images);
        var candidates = games
            .Where(game => game.Id > 0 && !string.IsNullOrWhiteSpace(game.GameTitle))
            .Select(game => MapGame(game, imageLookup))
            .OrderByDescending(candidate => candidate.Confidence)
            .ThenBy(candidate => candidate.Title, StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (candidates.Count == 0)
        {
            return new TheGamesDbCatalogMappingResult(
                TheGamesDbCatalogMappingStatus.Empty,
                "TheGamesDB returned no usable game identity records.",
                Array.Empty<TheGamesDbCatalogCandidate>());
        }

        var topConfidence = candidates[0].Confidence;
        var topCandidateCount = candidates.Count(candidate => candidate.Confidence == topConfidence);

        return new TheGamesDbCatalogMappingResult(
            topCandidateCount > 1 ? TheGamesDbCatalogMappingStatus.Ambiguous : TheGamesDbCatalogMappingStatus.Mapped,
            topCandidateCount > 1 ? "Multiple TheGamesDB records have the same top confidence." : null,
            candidates);
    }

    private static TheGamesDbCatalogCandidate MapGame(
        Game game,
        IReadOnlyDictionary<int, Dictionary<string, string>> imageLookup)
    {
        imageLookup.TryGetValue(game.Id, out var media);
        media ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        return new TheGamesDbCatalogCandidate(
            Provider: "thegamesdb",
            ProviderGameId: game.Id.ToString(),
            Title: game.GameTitle.Trim(),
            PlatformId: game.Platform,
            ReleaseDate: NormalizeOptional(game.ReleaseDate),
            DeveloperIds: CleanIds(game.Developers),
            GenreIds: CleanIds(game.Genres),
            PublisherIds: CleanIds(game.Publishers),
            Media: media,
            Provenance: "thegamesdb.games",
            Confidence: CalculateConfidence(game, media.Count),
            Overview: NormalizeOptional(game.Overview));
    }

    private static IReadOnlyDictionary<int, Dictionary<string, string>> BuildImageLookup(ImagesResponse? images)
    {
        var result = new Dictionary<int, Dictionary<string, string>>();
        if (images?.Images == null)
            return result;

        foreach (var (gameIdText, gameImages) in images.Images)
        {
            if (!int.TryParse(gameIdText, out var gameId))
                continue;

            var media = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var image in gameImages.Where(image => !string.IsNullOrWhiteSpace(image.Filename)))
            {
                var key = string.IsNullOrWhiteSpace(image.Side) ? image.Type : $"{image.Type}:{image.Side}";
                media[key] = image.Filename.Trim();
            }

            if (media.Count > 0)
                result[gameId] = media;
        }

        return result;
    }

    private static decimal CalculateConfidence(Game game, int mediaCount)
    {
        var confidence = 0.50m;

        if (game.Platform > 0)
            confidence += 0.20m;
        if (!string.IsNullOrWhiteSpace(game.ReleaseDate))
            confidence += 0.10m;
        if (mediaCount > 0)
            confidence += 0.10m;
        if (game.Genres?.Count > 0 || game.Developers?.Count > 0 || game.Publishers?.Count > 0)
            confidence += 0.05m;
        if (!string.IsNullOrWhiteSpace(game.Overview))
            confidence += 0.05m;

        return Math.Min(confidence, 1.00m);
    }

    private static string? NormalizeOptional(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    private static IReadOnlyList<int> CleanIds(IReadOnlyList<int>? ids)
    {
        if (ids == null)
            return Array.Empty<int>();

        return ids.Where(id => id > 0).ToList();
    }
}
