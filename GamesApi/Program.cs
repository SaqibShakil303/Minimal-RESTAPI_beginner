using GamesApi;
using GamesApi.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpoint = "GetGame";
List<GamesDto> games = [
    new(
        1,
        "GTA Online",
        "Open World",
        169.9M,
        new DateOnly(2012, 9,30)), 
        new(
            2,
            "Valorant",
            "Competetive",
            0.0M,
            new DateOnly(2020,7,25)),
        new(
            3,
            "BatMan Arkham Knight",
            "Open World Missions",
            90.8M,
            new DateOnly(2015,8,10)),
        new(
            4,
            "FiFa 24",
            "Football",
            230.54M,
            new DateOnly(2024,2,15)),
        new(
            5,
            "Watch Dogs 2",
            "Open World",
            4.99M,
            new DateOnly(2016,11,15)),
        new(
            6,
            "Asphalt 9: Legends",
            "Racing",
            0.00M,
            new DateOnly(2018,7,25)),
        new(
            7,
            "PUBG",
            "Competetive",
            0.00M,
            new DateOnly(2019,6,25))
];
//GET /Games
app.MapGet("Games", () => games);

//GET /Games/1
app.MapGet("Games/{Id}", (int id) =>games.Find(games => games.Id == id))
.WithName(GetGameEndpoint);

//POST /Games
app.MapPost("Games", (CreateGamesDto newGame) =>
{
    GamesDto game =  new(
        games.Count+1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate);

        games.Add(game);
        return Results.CreatedAtRoute(GetGameEndpoint,new {id = game.Id}, game);
});

app.MapPut("Games/{Id}",(int id, UpdateGamesDto updatedGame)=>
{
     // Find the index of the game with the specified ID
    var index = games.FindIndex(g=> g.Id == id);


  // Check if the game exists
    if (index == -1)
    {
        // Return a 404 Not Found response if the game does not exist
        return Results.NotFound();
    }

// Update the game with the new details
    games[index] = new GamesDto(
      id,
      updatedGame.Name,
      updatedGame.Genre,
      updatedGame.Price,
      updatedGame.ReleaseDate
    );
// Return a 204 No Content response to indicate the update was successful
    return Results.NoContent();
});


app.Run();
