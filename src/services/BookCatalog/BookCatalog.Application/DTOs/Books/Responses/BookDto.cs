using BookCatalog.Application.DTOs.Genres.Responses;
using BookCatalog.Application.DTOs.Publishers.Responses;
using BookCatalog.Application.DTOs.Reviews.Responses;

namespace BookCatalog.Application.DTOs.Books.Responses;

public record BookDto(
        Guid BookId,
        string Title,
        string Author,
        int Pages,
        string? ShelfLocation,
        decimal Price,
        decimal? Weight,
        decimal ShippingCost,
        string? FileFormat,
        string? DownloadLink,
        string? Illustrator,
        string? Edition,
        PublisherDto[] Publishers,
        GenreDto[] Genres,
        ReviewDto[] Reviews);