namespace BookCatalog.Application.DTOs.Genres.Responses;

public record GenreDto(
        Guid GenreId,
        string Name,
        string Description);