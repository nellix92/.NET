using System.Dynamic;
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

public class GamesClient
{
   private readonly List<GameSummary> games =
   [
    new(){
        Id = 1,
        Name = "Street Fighter II",
        Genre = "Fighting",
        Price = 19.99M,
        ReleaseDate = new DateOnly(1992, 7, 15)
       },

     new(){
        Id = 2,
        Name = "Final Fantasy XIV",
        Genre = "Roleplaying",
        Price = 59.99M,
        ReleaseDate = new DateOnly(2010, 9, 30)
        },
     new(){
        Id = 3,
        Name = "Fifa 23",
        Genre = "Sports",
        Price = 69.99M,
        ReleaseDate = new DateOnly(2022, 9, 27)
        }
   ];

   private readonly Genre[] genres = new GenresClient().GetGenres();

   public GameSummary[] GetGames() => [.. games];
   // public GameSummary[] GetGames()=>games.ToString();

   public void AddGame(GameDetails game)
   {
      Genre genre = GetGenreById(game.GenreId);

      var gameSummary = new GameSummary
      {
         Id = games.Count + 1,
         Name = game.Name,
         Genre = genre.Name,
         Price = game.Price,
         ReleaseDate = game.ReleaseDate
      };
      games.Add(gameSummary);
   }


   public GameDetails GetGame(int Id)
    {
        GameSummary? game = GetGameSummaryById(Id);

        var genre = genres.Single(genre => string.Equals(
           genre.Name,
           game.Genre,
           StringComparison.OrdinalIgnoreCase));

        return new GameDetails
        {
            Id = game.Id,
            Name = game.Name,
            GenreId = genre.Id.ToString(),
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public void UpdateGame (GameDetails updateGame)
    {
      var genre = GetGenreById(updateGame.GenreId);
      GameSummary existingGame = GetGameSummaryById(updateGame.Id);

      existingGame.Name = updateGame.Name;
      existingGame.Genre = genre.Name;
      existingGame.Price = updateGame.Price;
      existingGame.ReleaseDate = updateGame.ReleaseDate;
    }

    public void DeleteGame(int Id)
    {
      var game = GetGameSummaryById(Id);
      games.Remove(game);
    }

    private GameSummary GetGameSummaryById(int Id)
    {
        GameSummary? game = games.Find(game => game.Id == Id);
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }

    private Genre GetGenreById(string? id)
   {
      ArgumentException.ThrowIfNullOrWhiteSpace(id);
      return genres.Single(genre => genre.Id == int.Parse(id));
   }

}
