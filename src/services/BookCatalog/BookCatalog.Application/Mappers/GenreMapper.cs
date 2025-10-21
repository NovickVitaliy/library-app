using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Domain.Models;

namespace BookCatalog.Application.Mappers;

public static class GenreMapper
{
    public static GenreDto ToDto(Genre genre) =>
        new GenreDto(
            genre.GenreId,
            genre.Name,
            genre.Description);
}