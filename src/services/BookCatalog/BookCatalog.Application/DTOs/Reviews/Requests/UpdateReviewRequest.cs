namespace BookCatalog.Application.DTOs.Reviews.Requests;

public record UpdateReviewRequest(
    decimal Rating,
    string Text);