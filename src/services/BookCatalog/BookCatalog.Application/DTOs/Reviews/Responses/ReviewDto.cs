namespace BookCatalog.Application.DTOs.Reviews.Responses;

public record ReviewDto(
        Guid ReviewId,
        Guid BookId,
        Guid UserId,
        decimal Rating,
        string Text,
        DateTimeOffset CreatedDate);