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
        string? Edition);