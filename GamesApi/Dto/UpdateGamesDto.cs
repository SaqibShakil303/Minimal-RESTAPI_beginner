namespace GamesApi.Dto;

public record class UpdateGamesDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
       );