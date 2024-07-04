namespace GamesApi.Dto;

public record class CreateGamesDto(
    string Name,
     string Genre,
      decimal Price,
       DateOnly ReleaseDate);