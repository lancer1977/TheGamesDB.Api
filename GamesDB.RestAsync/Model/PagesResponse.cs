namespace GamesDB.RestAsync.Model;

//public interface IGameDBModel 
//{
//    int GameId { get; }
//    string Condition { get; }
//    string Platform { get; }
//    string ReleaseDate { get; }
//    string ImageUrl { get; }
//}

public class PagesResponse
{
    public string Previous { get; set; }
    public string Current { get; set; }
    public string Next { get; set; }

}