namespace GamesDB.RestAsync;

public interface IGamesDbAuthentication : IEndpointFactory
{
    string ApiKey { get;  }
}