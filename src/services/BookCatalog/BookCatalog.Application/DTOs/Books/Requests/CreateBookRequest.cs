namespace BookCatalog.Application.DTOs.Books.Requests;

public record CreateBookRequest(
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
        Guid[] GenresIds,
        Guid[] PublishersIds);