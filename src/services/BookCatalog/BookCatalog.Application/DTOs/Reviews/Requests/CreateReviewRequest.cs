namespace BookCatalog.Application.DTOs.Reviews.Requests;

public record CreateReviewRequest(
        Guid BookId,
        Guid UserId,
        decimal Rating,
        string Text);