namespace GamesDB.RestAsync.Test
{
    public class GamesDBEndpointFactory : IGamesDbAuthentication
    {
        public string ApiKey { get; set; }
        public string GetEndpoint()=> TheGamesDbWrapper.AddressRoot;

        public GamesDBEndpointFactory(string key)
        {
            ApiKey = key;
        }
    }
}